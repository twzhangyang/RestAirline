using System.ComponentModel.DataAnnotations;
using EventFlow.ReadStores;

namespace RestAirline.ReadModel.EntityFramework
{
    public class VersionedReadModel : IReadModel
    {
        public string Id { get; protected set; }

        [ConcurrencyCheck] public long Version { get; set; }
    }
}