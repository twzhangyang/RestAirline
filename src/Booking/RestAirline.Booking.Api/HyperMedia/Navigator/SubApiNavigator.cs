using System;
using System.Threading.Tasks;

namespace RestAirline.Booking.Api.HyperMedia.Navigator
{
    public class SubApiNavigator<TResource, TParentResource> : ApiNavigator<TResource>
    {
        private readonly ApiNavigator<TParentResource> _parent;
        private readonly Func<TParentResource, Link<TResource>> _navigator;

        public SubApiNavigator(ApiNavigator<TParentResource> parent, Func<TParentResource, Link<TResource>> navigator)
        {
            _parent = parent;
            _navigator = navigator;
        }

        public override async Task<TResource> Execute()
        {
            var parentResource = await _parent.Execute();
            return await FetchUriAsync(_navigator(parentResource));
        }
    }
}