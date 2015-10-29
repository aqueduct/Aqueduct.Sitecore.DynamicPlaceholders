using System;
using System.Web;
using Sitecore.Mvc.Helpers;
using Sitecore.Mvc.Presentation;

namespace Aqueduct.Sitecore.DynamicPlaceholders
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString DynamicPlaceholder(this SitecoreHelper helper, string dynamicKey)
        {
            return helper.Placeholder($"{dynamicKey}_{RenderingContext.Current.Rendering.UniqueId}");
        }
    }
}
