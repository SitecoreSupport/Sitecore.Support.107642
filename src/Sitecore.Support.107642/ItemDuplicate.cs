using Sitecore.Data.LanguageFallback;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.Support.Shell.Framework.Pipelines
{
    public class ItemDuplicate : Sitecore.Buckets.Pipelines.UI.ItemDuplicate
    {
        public new void Execute(ClientPipelineArgs args)
        {
            using (new LanguageFallbackItemSwitcher(new bool?(false)))
                base.Execute(args);
        }
    }
}
