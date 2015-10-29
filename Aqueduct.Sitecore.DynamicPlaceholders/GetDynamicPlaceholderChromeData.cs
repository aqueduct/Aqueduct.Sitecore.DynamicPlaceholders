using System;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.GetChromeData;
using Sitecore.Web.UI.PageModes;

namespace Aqueduct.Sitecore.DynamicPlaceholders
{
    /// <summary>
    /// Replaces the Displayname of the Placeholder rendering with the dynamic "parent"
    /// </summary>
    public class GetDynamicPlaceholderChromeData : GetChromeDataProcessor
    {
        public override void Process(GetChromeDataArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.IsNotNull(args.ChromeData, "Chrome Data");
            if ("placeholder".Equals(args.ChromeType, StringComparison.OrdinalIgnoreCase))
            {
                var argument = args.CustomData["placeHolderKey"] as string;

                var placeholderKey = argument;
                if (PlaceholderKeyHelper.IsDynamicPlaceholder(placeholderKey))
                {
                    placeholderKey = PlaceholderKeyHelper.ExtractPlaceholderKey(placeholderKey);
                }
                else
                {
                    return;
                }

                // Handles replacing the displayname of the placeholder area to the master reference
                if (args.Item != null)
                {
                    var layout = ChromeContext.GetLayout(args.Item);
                    var item = global::Sitecore.Client.Page.GetPlaceholderItem(placeholderKey, args.Item.Database, layout);
                    if (item != null)
                    {
                        args.ChromeData.DisplayName = item.DisplayName;
                    }
                    if (!string.IsNullOrEmpty(item?.Appearance.ShortDescription))
                    {
                        args.ChromeData.ExpandedDisplayName = item.Appearance.ShortDescription;
                    }
                }
            }
        }
    }
}