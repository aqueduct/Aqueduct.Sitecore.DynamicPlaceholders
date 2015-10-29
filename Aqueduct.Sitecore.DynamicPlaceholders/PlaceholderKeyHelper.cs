using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aqueduct.Sitecore.DynamicPlaceholders
{
    internal static class PlaceholderKeyHelper
    {
        public static bool IsDynamicPlaceholder(string placeholderKey)
        {
            if (string.IsNullOrEmpty(placeholderKey))
                throw new ArgumentNullException(nameof(placeholderKey));

            return placeholderKey.Contains("_");
        }

        public static string ExtractPlaceholderKey(string placeholderKey)
        {
            if (string.IsNullOrEmpty(placeholderKey))
                throw new ArgumentNullException(nameof(placeholderKey));

            var lastKeyPos = placeholderKey.LastIndexOf('/');
            if (IsDynamicPlaceholder(placeholderKey))
            {
                if (lastKeyPos > 0)
                {
                    var start = placeholderKey.Substring(0, lastKeyPos);
                    var end = placeholderKey.Substring(lastKeyPos);

                    return $"{start}{end.Substring(0, end.IndexOf('_'))}";
                }

                return placeholderKey.Substring(0, placeholderKey.IndexOf('_'));
            }
            return placeholderKey;
        }
    }
}
