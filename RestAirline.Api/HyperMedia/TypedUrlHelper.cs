using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace RestAirline.Api.Hypermedia
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

        private static string GenerateLinkUrl<TController>(IUrlHelper @this, LambdaExpression actionSelector) where TController : Controller
        {
           // resolve lambda tree expression and generate link

            return string.Empty;
        }
    }
}