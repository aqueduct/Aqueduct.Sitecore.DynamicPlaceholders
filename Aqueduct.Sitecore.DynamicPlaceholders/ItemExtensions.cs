using System.Xml;
using Sitecore;
using Sitecore.Data.Items;

namespace Aqueduct.Sitecore.DynamicPlaceholders
{
    internal static class ItemExtensions
    {
        public static void RemoveRenderingReferences(this Item item, params string[] renderingReferenceUids)
        {
            var doc = new XmlDocument();
            doc.LoadXml(item[FieldIDs.LayoutField]);
            var changed = false;

            foreach (var renderingReferenceUid in renderingReferenceUids)
            {
                var node = doc.SelectSingleNode($"//r[@uid='{renderingReferenceUid}']");
                if (node?.ParentNode != null)
                {
                    changed = true;
                    node.ParentNode.RemoveChild(node);
                }
            }

            if (changed)
            {
                using (new EditContext(item))
                {
                    item[FieldIDs.LayoutField] = doc.OuterXml;
                }
            }
        }

        public static bool HasLayout(this Item item)
        {
            return item.Fields[FieldIDs.LayoutField] != null
                   && !string.IsNullOrEmpty(item.Fields[FieldIDs.LayoutField].Value);
        }
    }
}