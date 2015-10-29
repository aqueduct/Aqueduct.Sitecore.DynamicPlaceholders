using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Events;

namespace Aqueduct.Sitecore.DynamicPlaceholders
{
    public class RemoveUnusedRenderingsHandler
    {
        public void OnItemSaved(object sender, EventArgs args)
        {
            var item = Event.ExtractParameter(args, 0) as Item;

            if (item != null && item.HasLayout() && Context.Device != null)
            {
                var renderings = item.Visualization.GetRenderings(Context.Device, false);
                var renderingsToRemove = new List<string>();
                var renderingKeys = renderings.Select(x => new Guid(x.UniqueId));
                foreach (var rendering in renderings)
                {
                    var parents = rendering.GetParentRenderingIdsForRendering();
                    if (parents.Any(x => !renderingKeys.Contains(new Guid(x))))
                    {
                        renderingsToRemove.Add(rendering.UniqueId);
                    }
                }

                if (renderingsToRemove.Any())
                    item.RemoveRenderingReferences(renderingsToRemove.ToArray());
            }
        }
    }
}