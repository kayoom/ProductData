using System.Collections.Generic;
using System.Linq;
using ProductData.DOM;

namespace ProductData
{
    public static class Extensions
    {
        public static string Get(this IEnumerable<Name> names, string langCode)
        {
            return names.Where(n => n.LangCode == langCode).Select(n => n.Content).FirstOrDefault();
        }

        public static string Get(this IEnumerable<Description> descriptions, string langCode)
        {
            return descriptions.Where(n => n.LangCode == langCode).Select(n => n.Content).FirstOrDefault();
        }
    }
}
