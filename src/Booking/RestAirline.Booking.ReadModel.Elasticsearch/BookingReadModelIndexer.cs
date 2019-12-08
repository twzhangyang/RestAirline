using System;
using System.Linq;
using System.Reflection;
using EventFlow.Elasticsearch.ReadStores;
using Nest;
using RestAirline.Booking.ReadModel.Elasticsearch.Booking;

namespace RestAirline.Booking.ReadModel.Elasticsearch
{
    public interface IElasticSearchReadModelIndexer
    {
        void PrepareIndex();
    }

    public class BookingReadModelIndexer : IElasticSearchReadModelIndexer
    {
        private readonly IElasticClient _elasticClient;
        private readonly IReadModelDescriptionProvider _descriptionProvider;

        public BookingReadModelIndexer(IElasticClient elasticClient, IReadModelDescriptionProvider descriptionProvider)
        {
            _elasticClient = elasticClient;
            _descriptionProvider = descriptionProvider;
        }

        public void PrepareIndex()
        {
            var modelDescription = _descriptionProvider.GetReadModelDescription<BookingReadModel>();
            
            var indexName = GetIndexName(modelDescription.IndexName.Value);

            var isExist = _elasticClient.IndexExists(modelDescription.IndexName.Value).Exists;

            if (isExist)
            {
                return;
            }

            _elasticClient.CreateIndex(indexName, c => c
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0))
                .Aliases(a => a.Alias(modelDescription.IndexName.Value))
                .Mappings(m => m
                    .Map<BookingReadModel>(map => map
                        .AutoMap()
                        .Properties(ps => ps
                            .Nested<Passenger>(n => n
                                .Name(p => p.Passengers.First())
                                .AutoMap()
                                .Properties(pps => pps
                                    .Text(t => t
                                        .Name(pn => pn.Name)
                                        .Fielddata()
                                        .Fields(fs => fs
                                            .Keyword(ss => ss
                                                .Name("raw")
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
            );
        }

        private string GetIndexName(string name)
        {
            return $"restailine-{name}-{001}".ToLowerInvariant();
        }
    }
}