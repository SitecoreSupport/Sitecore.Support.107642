using Sitecore.Data.LanguageFallback;
using Sitecore.Shell.Framework.Pipelines;

namespace Sitecore.Support.Shell.Framework.Pipelines
{
    public class CopyItems : Sitecore.Shell.Framework.Pipelines.CopyItems
    {
        public override void Execute(CopyItemsArgs args)
        {
            using (new LanguageFallbackItemSwitcher(new bool?(false)))
                base.Execute(args);
        }
    }
}
