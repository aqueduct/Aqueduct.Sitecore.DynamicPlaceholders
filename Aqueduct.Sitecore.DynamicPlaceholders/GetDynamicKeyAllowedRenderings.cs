using System.Collections.Generic;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.GetPlaceholderRenderings;

namespace Aqueduct.Sitecore.DynamicPlaceholders
{
    /// <summary>
    /// Handles changing context to the references dynamic "master" renderings settings for inserting the allowed controls for the placeholder and making it editable
    /// </summary>
    public class GetDynamicKeyAllowedRenderings : GetAllowedRenderings
    {

        public new void Process(GetPlaceholderRenderingsArgs args)
        {
            Assert.IsNotNull(args, "args");

            var placeholderKey = args.PlaceholderKey;
            if (PlaceholderKeyHelper.IsDynamicPlaceholder(placeholderKey))
            {
                placeholderKey = PlaceholderKeyHelper.ExtractPlaceholderKey(placeholderKey);
            }
            else
            {
                return;
            }
            // Same as Sitecore.Pipelines.GetPlaceholderRenderings.GetAllowedRenderings but with fake placeholderKey
            Item placeholderItem;
            if (ID.IsNullOrEmpty(args.DeviceId))
            {
                placeholderItem = Client.Page.GetPlaceholderItem(placeholderKey, args.ContentDatabase,
                                                                 args.LayoutDefinition);
            }
            else
            {
                using (new DeviceSwitcher(args.DeviceId, args.ContentDatabase))
                {
                    placeholderItem = Client.Page.GetPlaceholderItem(placeholderKey, args.ContentDatabase,
                                                                     args.LayoutDefinition);
                }
            }
            List<Item> collection = null;
            if (placeholderItem != null)
            {
                bool flag;
                args.HasPlaceholderSettings = true;
                collection = GetRenderings(placeholderItem, out flag);
                if (flag)
                {
                    args.CustomData["allowedControlsSpecified"] = true;
                    args.Options.ShowTree = false;
                }
            }
            if (collection != null)
            {
                if (args.PlaceholderRenderings == null)
                {
                    args.PlaceholderRenderings = new List<Item>();
                }
                args.PlaceholderRenderings.AddRange(collection);
            }
        }
    }
}