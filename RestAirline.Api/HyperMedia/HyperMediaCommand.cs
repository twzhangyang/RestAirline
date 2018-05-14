using System;

namespace RestAirline.Api.HyperMedia
{
    public class HyperMediaCommand<TResponse>
    {
        [Obsolete("For serialization")]
        public HyperMediaCommand()
        {
            
        }

        public HyperMediaCommand(Link<TResponse> postUrl)
        {
            PostUrl = postUrl;
        }

        public Link<TResponse> PostUrl { get; set; }
    }
}