using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Layouts;
using Sitecore.Rules.ConditionalRenderings;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Locksmith.Extensions
{
    public static class ItemExtensions
    {
        public static List<Item> GetDataSources(this Item root, bool includePersonalization = true, bool includeMultiVariateTests = true)
        {
            var list = new List<Item>();
            var references = root.GetRenderingReferences();

            for (int i = 0; i < references.Length; i++)
            {
                Item item = references[i].GetDataSourceItem();
                if (item != null && !list.Any(t => t.ID == item.ID))
                    list.Add(item);

                if (includePersonalization)
                {
                    var personalized = references[i].GetPersonalizationDataSourceItems();
                    foreach (var datasource in personalized)
                    {
                        if (datasource != null && !list.Any(t => t.ID == datasource.ID))
                            list.Add(datasource);
                    }
                }

                if (includeMultiVariateTests && item != null)
                {
                    foreach (Item datasource in item.GetMultiVariateTestDataSourceItems())
                    {
                        if (datasource != null && !list.Any(t => t.ID == datasource.ID))
                            list.Add(datasource);
                    }
                }
            }
            return list;
        }

        public static Item GetDataSourceItem(this RenderingReference reference)
        {
            if (reference != null)
            {
                return GetDataSourceItem(reference.Settings.DataSource, reference.Database);
            }
            return null;
        }

        private static Item GetDataSourceItem(string id, Database db)
        {
            Guid itemId;
            return Guid.TryParse(id, out itemId)
                                    ? db.GetItem(new ID(itemId))
                                    : db.GetItem(id);
        }

        public static Item GetInternalLinkFieldItem(this Item i, string internalLinkFieldName)
        {
            if (i == null) return null;
            InternalLinkField ilf = i.Fields[internalLinkFieldName];
            if (ilf != null && ilf.TargetItem != null)
            {
                return ilf.TargetItem;
            }
            return null;
        }

        public static List<Item> GetMultiVariateTestDataSourceItems(this Item i)
        {
            var list = new List<Item>();
            foreach (var reference in i.GetRenderingReferences())
            {
                list.AddRange(reference.GetMultiVariateTestDataSources());
            }
            return list;
        }

        private static IEnumerable<Item> GetMultiVariateTestDataSources(this RenderingReference reference)
        {
            var list = new List<Item>();
            if (reference != null && !string.IsNullOrEmpty(reference.Settings.MultiVariateTest))
            {
                using (new SecurityDisabler())
                {
                    var testStore = new Sitecore.ContentTesting.Data.SitecoreContentTestStore();
                    var tests = testStore.GetMultivariateTestVariable(reference, reference.Language);

                    if (tests != null && tests.InnerItem != null)
                    {
                        foreach (Item child in tests.InnerItem.Children)
                        {
                            var datasource = child.GetInternalLinkFieldItem("Datasource");
                            if (datasource != null)
                                list.Add(datasource);
                        }
                    }
                }
            }
            return list;
        }

        private static IEnumerable<Item> GetPersonalizationDataSourceItems(this RenderingReference reference)
        {
            var list = new List<Item>();
            if (reference != null && reference.Settings.Rules != null && reference.Settings.Rules.Count > 0)
            {
                list.AddRange(reference.Settings.Rules.Rules.SelectMany(r => r.Actions).OfType<SetDataSourceAction<ConditionalRenderingsRuleContext>>().Select(setDataSourceAction => GetDataSourceItem(setDataSourceAction.DataSource, reference.Database)).Where(dataSourceItem => dataSourceItem != null));
            }
            return list;
        }

        public static RenderingReference[] GetRenderingReferences(this Item i)
        {
            return i == null ? new RenderingReference[0] : i.Visualization.GetRenderings(Sitecore.Context.Device, false);
        }

        public static bool HasPresentation(this Item item)
        {
            return item.Fields[Sitecore.FieldIDs.LayoutField] != null && !String.IsNullOrEmpty(item.Fields[Sitecore.FieldIDs.LayoutField].Value);
        }
    }
}