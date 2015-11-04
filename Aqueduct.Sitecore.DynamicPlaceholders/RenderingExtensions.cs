using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Layouts;

namespace Aqueduct.Sitecore.DynamicPlaceholders
{
    internal static class RenderingExtensions
    {
        internal static IEnumerable<string> GetParentRenderingIdsForRendering(this RenderingReference rendering)
        {
            return GetParentRenderingIdsForRendering(rendering.Placeholder);            
        }

        internal static IEnumerable<string> GetParentRenderingIdsForRendering(string placeholderKey)
        {
            if (!PlaceholderKeyHelper.IsDynamicPlaceholder(placeholderKey)) return Enumerable.Empty<string>();

            return placeholderKey.Split(new[] { '/' }, StringSplitOptions.None)
                .Where(key => key.Contains("_"))
                .Select(x => x.Substring(x.IndexOf('_') + 1));
        }
    }
}