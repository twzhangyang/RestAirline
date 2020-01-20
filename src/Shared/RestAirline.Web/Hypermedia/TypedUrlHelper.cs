using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestAirline.Web.Hypermedia
{
    public static class TypedUrlHelper
    {
        public static Link<TResult> Link<TController, TResult>(this IUrlHelper @this, Expression<Func<TController, Task<TResult>>> actionSelector)
            where TController : Controller
        {
            return new Link<TResult>(GenerateLinkUrl(@this, actionSelector));
        }
        
//        public static LinkTemplate1<TResult, TArgument1> LinkTemplate<TController, TArgument1, TResult>(this IUrlHelper @this,
//            Expression<Func<TController, TArgument1, TResult>> actionSelector) where TController : Controller
//        {
//            return new LinkTemplate1<TResult, TArgument1>(GenerateLinkUrl<TController, TResult>(@this, actionSelector));
//        }

        private static string GenerateLinkUrl<TController, TResult>(IUrlHelper helper, Expression<Func<TController, Task<TResult>>> actionSelector) where TController : Controller
        {
            var controllerName = GetControllerName(typeof(TController));
            var methodCallExpression = GetMethodCallExpression(actionSelector);
            var actionName = methodCallExpression.Method.Name;

            var routes = RouteValueExtractor.GetRouteValues(methodCallExpression);

            var link = helper.Action(action: actionName, controller: controllerName, values: routes);

            return link;
        }
        
        private static string GetControllerName(Type controllerType)
        {
            var controllerName = controllerType.Name.EndsWith("Controller")
                ? controllerType.Name.Substring(0, controllerType.Name.Length - "Controller".Length)
                : controllerType.Name;
            return controllerName;
        }

        private static MethodCallExpression GetMethodCallExpression<TController, TResult>(
            Expression<Func<TController, Task<TResult>>> actionSelector)
        {
            var call = actionSelector.Body as MethodCallExpression;
            if (call == null)
            {
                throw new ArgumentException("You must call a method on " + typeof(TController).Name, "actionSelector");
            }

            return call;
        }
    }
}