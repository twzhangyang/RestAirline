using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace RestAirline.Api.HyperMedia
{
    public static class TypedUrlHelper
    {
        public static Link<TResult> Link<TController, TResult>(this IUrlHelper @this, Expression<Func<TController, TResult>> actionSelector)
            where TController : Controller
        {
            return new Link<TResult>(GenerateLinkUrl<TController>(@this, actionSelector));
        }

        public static LinkTemplate1<TResult, TArgument1> LinkTemplate<TController, TArgument1, TResult>(this IUrlHelper @this,
            Expression<Func<TController, TArgument1, TResult>> actionSelector) where TController : Controller
        {
            return new LinkTemplate1<TResult, TArgument1>(GenerateLinkUrl<TController>(@this, actionSelector));
        }

        public static LinkTemplate2<TResult, TArgument1, TArgument2> LinkTemplate<TController, TArgument1, TArgument2, TResult>(this IUrlHelper @this, Expression<Func<TController, TArgument1, TArgument2, TResult>> actionSelector)
             where TController : Controller
        {
            return new LinkTemplate2<TResult, TArgument1, TArgument2>(GenerateLinkUrl<TController>(@this, actionSelector));
        }

        public static LinkTemplate3<TResult, TArgument1, TArgument2, TArgument3> LinkTemplate<TController, TArgument1, TArgument2, TArgument3, TResult>(this IUrlHelper @this, Expression<Func<TController, TArgument1, TArgument2, TArgument3, TResult>> actionSelector)
             where TController : Controller
        {
            return new LinkTemplate3<TResult, TArgument1, TArgument2, TArgument3>(GenerateLinkUrl<TController>(@this, actionSelector));
        }

        private static string GenerateLinkUrl<TController>(IUrlHelper @this, LambdaExpression actionSelector) where TController : Controller
        {
           // resolve lambda tree expression and generate link

            return string.Empty;
        }
    }
}