using System;

namespace RestAirline.Booking.Api.HyperMedia
{
    public class Link<TResource>
    {
        public string Uri { get; set; }

        [Obsolete("For serialization")]
        public Link()
        {
            
        }

        public Link(string uri)
        {
            Uri = uri;
        }

        public override string ToString()
        {
            return Uri;
        }
    }
}