using Sitecore.Buckets.Managers;
using Sitecore.Data.Items;
using Sitecore.Data.LanguageFallback;
using Sitecore.Diagnostics;
using Sitecore.Shell.Applications.Dialogs.ProgressBoxes;
using Sitecore.Shell.Framework.Pipelines;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Support.Buckets.Pipelines.UI
{
    internal class ItemCopy : Sitecore.Buckets.Pipelines.UI.ItemCopy
    {
        public override void Execute(CopyItemsArgs args)
        {
            Assert.ArgumentNotNull((object)args, "args");
            if (!BucketManager.IsBucket(this.GetItemByParameter(args, "destination")) && !ItemCopy.ShouldBeHandledWithinItemBuckets((IEnumerable<Item>)CopyItems.GetItems(args)))
                return;
            ProgressBox.ExecuteSync("Copying Items", "~/icon/Core3/32x32/copy_to_folder.png", 
                new Action<ClientPipelineArgs>(this.StartCopyingIntoBucket), 
                new Action<ClientPipelineArgs>((this.EndCopyingIntoBucket)));
            args.AbortPipeline();
        }

        public static bool ShouldBeHandledWithinItemBuckets(IEnumerable<Item> items)
        {
            Assert.ArgumentNotNull((object)items, "items");
            return items.Any<Item>((Func<Item, bool>)(item =>
            {
                if (!BucketManager.IsBucket(item))
                    return BucketManager.IsItemContainedWithinBucket(item);
                return true;
            }));
        }

        protected new void StartCopyingIntoBucket(ClientPipelineArgs args)
        {
            using (new LanguageFallbackItemSwitcher(new bool?(false)))
                base.StartCopyingIntoBucket(args);
        }
    }
}
