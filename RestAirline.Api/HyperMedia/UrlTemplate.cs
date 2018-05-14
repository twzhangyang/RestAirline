using Newtonsoft.Json;

namespace RestAirline.Api.HyperMedia
{
    public class LinkTemplate1<TTargetResource, TArgument1>
    {
        [JsonProperty]
        public string UrlTemplate { get; private set; }

        public LinkTemplate1(string urlTemplate)
        {
            UrlTemplate = urlTemplate;
        }

        public Link<TTargetResource> CreateLink(TArgument1 argument1)
        {
            return new Link<TTargetResource>(UrlTemplate.FormatWith(argument1));
        }
    }

    public class LinkTemplate2<TTargetResource, TArgument1, TArgument2>
    {
        [JsonProperty]
        public string UrlTemplate { get; private set; }

        public LinkTemplate2(string urlTemplate)
        {
            UrlTemplate = urlTemplate;
        }

        public Link<TTargetResource> CreateLink(TArgument1 argument1, TArgument2 argument2)
        {
            return new Link<TTargetResource>(UrlTemplate.FormatWith(argument1, argument2));
        }
    }

    public class LinkTemplate3<TTargetResource, TArgument1, TArgument2, TArgument3>
    {
        [JsonProperty]
        public string UrlTemplate { get; private set; }

        public LinkTemplate3(string urlTemplate)
        {
            UrlTemplate = urlTemplate;
        }

        public Link<TTargetResource> CreateLink(TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            return new Link<TTargetResource>(UrlTemplate.FormatWith(argument1, argument2, argument3));
        }
    }
}