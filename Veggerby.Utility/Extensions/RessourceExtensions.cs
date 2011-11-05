using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Resources;
using Veggerby.Utility.Mvc.Extensions;

namespace Veggerby.Utility.Extensions
{
    public static class RessourceExtensions
    {
        public static IEnumerable<KeyValuePair<string, string>> AllRessources(this ResourceManager rm)
        {
            var set = rm.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            return set
                .Cast<DictionaryEntry>()
                .Where(x => x.Value is string)
                .Select(x => new KeyValuePair<string, string>(x.Key as string, x.Value as string));
        }

        public static object ToObject(this ResourceManager rm)
        {
            dynamic resources = new ExpandoObject();
            foreach (var kvp in rm.AllRessources())
            {
                ((IDictionary<string, object>)resources)[kvp.Key] = kvp.Value;
            }

            return resources;
        }

        public static Dictionary<string, string> ToDictionary(this ResourceManager rm)
        {
            var resources = new Dictionary<string, string>();
            foreach (var kvp in rm.AllRessources())
            {
                resources[kvp.Key] = kvp.Value;
            }

            return resources;
        }


        public static string ToJson(this ResourceManager rm)
        {
            return rm.ToDictionary().ToJson();
        }
    }
}