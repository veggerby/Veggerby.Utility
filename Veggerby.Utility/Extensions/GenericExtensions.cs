using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veggerby.Utility
{
    public static class GenericExtensions
    {
        public static TResult IfNotDefault<T, TResult>(this T obj, Func<T, TResult> func, TResult @default = default(TResult))
        {
            if (obj == null)
            {
                return @default;
            }

            return func(obj);                
        }
    }
}
