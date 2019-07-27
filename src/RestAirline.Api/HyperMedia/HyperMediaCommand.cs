using System;

namespace RestAirline.Api.Hypermedia
{
    public class HypermediaCommand<TResponse>
    {
        public HypermediaCommand()
        {
        }

        public HypermediaCommand(Link<TResponse> postUrl)
        {
            PostUrl = postUrl;
        }

        public Link<TResponse> PostUrl { get; set; }
    }
}