using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Nest;

namespace RestAirline.ReadModel.Elasticsearch
{
    public interface IElasticSearchReadModelIndexer
    {
        void PrepareIndexes();
    }
    
    public class BookingReadModelIndexer: IElasticSearchReadModelIndexer
    {
        private readonly IElasticClient _elasticClient;

        public BookingReadModelIndexer(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public void PrepareIndexes()
        {
            var readModelTypes =
                GetReadModelTypes<ElasticsearchTypeAttribute>(typeof(BookingReadModel).Assembly);

            foreach (var readModelType in readModelTypes)
            {
                var esType = readModelType.GetTypeInfo()
                    .GetCustomAttribute<ElasticsearchTypeAttribute>();

                var aliasResponse = _elasticClient.GetAlias(x => x.Name(esType.Name));

                if (aliasResponse.ApiCall.Success)
                {
                    if (aliasResponse.Indices != null)
                    {
                        foreach (var indice in aliasResponse?.Indices)
                        {
                            _elasticClient.DeleteAlias(indice.Key, esType.Name);

                            _elasticClient.DeleteIndex(indice.Key,
                                d => d.RequestConfiguration(c => c.AllowedStatusCodes((int)HttpStatusCode.NotFound)));
                        }

                        _elasticClient.DeleteIndex(esType.Name,
                            d => d.RequestConfiguration(c => c.AllowedStatusCodes((int)HttpStatusCode.NotFound)));
                    }
                }

                var indexName = GetIndexName(esType.Name);

                _elasticClient.CreateIndex(indexName, c => c
                    .Settings(s => s
                        .NumberOfShards(1)
                        .NumberOfReplicas(0))
                    .Aliases(a => a.Alias(esType.Name))
                    .Mappings(m => m
                        .Map(TypeName.Create(readModelType), d => d
                            .AutoMap())));
            }
        }
        
        private string GetIndexName(string name)
        {
            return $"restailine-{name}-{Guid.NewGuid():D}".ToLowerInvariant();
        }

        private IEnumerable<Type> GetReadModelTypes<T>(params Assembly[] assemblies)
        {
            IEnumerable<Type> availableTypes;

            if (assemblies == null || !assemblies.Any()) throw new ArgumentNullException(nameof(assemblies));
            try
            {
                availableTypes = assemblies.SelectMany(x => x.GetTypes());
            }
            catch (ReflectionTypeLoadException e)
            {
                availableTypes = e.Types.Where(t => t != null);
            }

            foreach (Type type in availableTypes)
            {
                if (type.GetCustomAttributes(typeof(T), true).Length > 0)
                {
                    yield return type;
                }
            }
        }
    }
}