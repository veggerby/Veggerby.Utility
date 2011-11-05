using System.Web.Script.Serialization;

namespace Veggerby.Utility.Mvc.Extensions
{
    public static class JsonExtensions
    {
        private static readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();

        public static string ToJson(this object o)
        {
            return _serializer.Serialize(o);
        }
    }
}