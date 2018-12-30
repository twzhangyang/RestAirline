using System;

namespace RestAirline.ReadModel
{
    public class StationItem
    {
        public string Id { get; set; }
            
        public DateTime DepartureDate { get; set; }

        public string DepartureStation { get; set; }

        public string ArriveStation { get; set; }
    }
}