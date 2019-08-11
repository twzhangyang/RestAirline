using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RestAirline.Api.HyperMedia
{
    public class RouteValueExtractor
    {
        public static object GetRouteValues(MethodCallExpression call)
        {
            var routes = new Dictionary<string, object>();

            var parameters = call.Method.GetParameters();
            var pairs = call.Arguments.Select((a, i) => new
            {
                Argument = a,
                ParamName = parameters[i].Name
            });
            foreach (var item in pairs)
            {
                string name = item.ParamName;
                object value = GetValue(item.Argument);
                if (value != null)
                {
                    var valueType = value.GetType();
                    if (valueType.IsPrimitive || valueType == typeof(string))
                    {
                        routes.Add(name, value);
                    }
                    else
                    {
                        throw new NotSupportedException("Unsupported parameter type {0}");
                    }
                }
            }

            return DictionaryToObject(routes);
        }

        private static object GetValue(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Constant)
            {
                return ((ConstantExpression) expression).Value;
            }

            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                var me = (MemberExpression) expression;
                object obj = (me.Expression != null ? GetValue(me.Expression) : null);
                if (me.Member is FieldInfo)
                    return ((FieldInfo) me.Member).GetValue(obj);
                if (me.Member is PropertyInfo)
                    return ((PropertyInfo) me.Member).GetValue(obj, null);
                throw new NotSupportedException("Unsupported member access type");
            }

            throw new NotSupportedException("Unsupported parameter expression");
        }

        private static dynamic DictionaryToObject(IDictionary<string, object> dictionary)
        {
            var expandoObj = new ExpandoObject();
            var expandoObjCollection = (ICollection<KeyValuePair<string, object>>) expandoObj;

            foreach (var keyValuePair in dictionary)
            {
                expandoObjCollection.Add(keyValuePair);
            }

            dynamic eoDynamic = expandoObj;
            return eoDynamic;
        }
    }
}