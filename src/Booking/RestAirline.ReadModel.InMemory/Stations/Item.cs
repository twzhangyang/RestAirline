using System;
using System.ComponentModel.DataAnnotations;

namespace RestAirline.ReadModel.InMemory.Stations
{
    public class StationItem
    {
        [Key]
        public string Id { get; set; }
            
        public DateTime DepartureDate { get; set; }

        public string DepartureStation { get; set; }

        public string ArriveStation { get; set; }
    }
}