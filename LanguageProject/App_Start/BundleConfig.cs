using System.Web;
using System.Web.Optimization;

namespace LanguageProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862

        public static void RegisterBundles(BundleCollection bundles){

            bundles.Add(new ScriptBundle("~/bundles/core-library").Include(
                     "~/Scripts/library/jquery-1.11.0.min.js",
                     "~/Scripts/library/bootstrap.min.js",
                     "~/Scripts/library/owl.carousel.js",
                     "~/Scripts/library/jquery.appear.min.js",
                     "~/Scripts/library/perfect-scrollbar.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/main-script").Include(
                    "~/Scripts/scripts.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/library/bootstrap.min.css",
                    "~/Content/library/font-awesome.min.css",
                    "~/Content/library/owl.carousel.css",
                     "~/Content/library/owl.theme.default.css",
                    "~/Content/md-font.css",
                    "~/Content/style.css"

            ));
        }
    }
}