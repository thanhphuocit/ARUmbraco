using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Optimization;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Structure.Core.Composing
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class BundleComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<BundleComponent>();
        }
        public class BundleComponent : IComponent
        {
            public void Initialize()
            {
                AreaRegistration.RegisterAllAreas();
                BundleConfig.RegisterBundles(BundleTable.Bundles);
            }

            public class BundleConfig
            {
                public static void RegisterBundles(BundleCollection bundles)
                {
                    bundles.Add(new ScriptBundle("~/bundles/JSLib.js").Include(
                         "~/lib/jquery/jquery.js",
                          "~/lib/bootstrap/dist/js/bootstrap.js"));

                    bundles.Add(new ScriptBundle("~/bundles/Site.js").Include("~/scripts/site.min.js"));

                    bundles.Add(new StyleBundle("~/bundles/Style.css").Include(
                        "~/lib/bootstrap/dist/css/bootstrap.css")
                        .Include("~/fonts/css/all.css", new CssRewriteUrlTransform())
                        .Include("~/fonts/line-icons.css", new CssRewriteUrlTransform()));

                    bundles.Add(new StyleBundle("~/bundles/Site.css")
                        .Include("~/css/site.min.css", new CssRewriteUrlTransform()));

                    BundleTable.EnableOptimizations = false;
                }
            }

            public void Terminate()
            {
            }
        }
    }
}
