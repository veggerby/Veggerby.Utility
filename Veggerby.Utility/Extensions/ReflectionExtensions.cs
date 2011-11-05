using System;

namespace Veggerby.Utility.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool IsNullableType(this Type theType)
        {
            return (theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }
    }
}
