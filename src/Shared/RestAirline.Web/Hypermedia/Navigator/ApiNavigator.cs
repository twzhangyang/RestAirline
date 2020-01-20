using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestAirline.Web.Hypermedia.Navigator
{
    public class ApiNavigator<TResource>
    {
        public Link<TResource> Link { get; set; }

        public ApiNavigator() { }

        public ApiNavigator(Link<TResource> startLink)
        {
            Link = startLink;
        }

        public virtual Task<TResource> Execute()
        {
            return  FetchUriAsync(Link);
        }

        protected async Task<TResourceToFetch> FetchUriAsync<TResourceToFetch>(Link<TResourceToFetch> link)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, link.Uri))
                {
                    using (var response = httpClient.SendAsync(request))
                    {
                        using (var result = response.Result)
                        {
                            using (var httpContent = result.Content)
                            {
                                var resultData = await httpContent.ReadAsStringAsync();

                                var fetchedResource = DeserializeJson<TResourceToFetch>(resultData);

                                return fetchedResource;
                            }
                        }
                    }
                }
            }
        }

        public async Task<TResourceToFetch> PostCommand<TResourceToFetch>(HypermediaCommand<TResourceToFetch> command)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, command.PostUrl.Uri))
                {
                    using (var response = httpClient.SendAsync(request))
                    {
                        using (var result = response.Result)
                        {
                            using (var httpContent = result.Content)
                            {
                                var resultData = await httpContent.ReadAsStringAsync();

                                var fetchedResource = DeserializeJson<TResourceToFetch>(resultData);

                                return fetchedResource;
                            }
                        }
                    }
                }
            }
        }

        public SubApiNavigator<TTargetResource, TResource> FollowLink<TTargetResource>(Func<TResource, Link<TTargetResource>> navigator)
        {
            return new SubApiNavigator<TTargetResource, TResource>(this, navigator);
        }

        public SubApiNavigator<TTargetResource, TResource> FollowLinkTemplate<TTargetResource, TArgument>(Func<TResource, LinkTemplate1<TTargetResource, TArgument>> navigator, TArgument argument)
        {
            return new SubApiNavigator<TTargetResource, TResource>(this, (resource) => navigator(resource).CreateLink(argument));
        }

        private TResourceToFetch DeserializeJson<TResourceToFetch>(string responseText)
        {
            return JsonConvert.DeserializeObject<TResourceToFetch>(responseText);
        }
    }
}