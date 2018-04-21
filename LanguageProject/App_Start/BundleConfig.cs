using System.Web;
using System.Web.Optimization;

namespace LanguageProject
{
    public class BundleConfig
    {
        private static readonly int assets;

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862

        public static void RegisterBundles(BundleCollection bundles){

            bundles.Add(new ScriptBundle("~/bundles/core-library").Include(
                     "~/Scripts/library/jquery-1.11.0.min.js",
                     "~/Scripts/library/bootstrap.min.js",
                     "~/Scripts/library/owl.carousel.js",
                     "~/Scripts/library/jquery.appear.min.js",
                     "~/Scripts/library/perfect-scrollbar.min.js"
            ));



            bundles.Add(new ScriptBundle("~/bundles/admin-core-libary").Include(
                        "~/Content/admin/assets/plugins/pace/pace.min.js",
                        "~/Content/admin/assets/plugins/jquery/jquery-1.11.3.min.js",
                        "~/Content/admin/assets/plugins/bootstrapv3/js/bootstrap.min.js",
                        "~/Content/admin/assets/plugins/jquery-block-ui/jqueryblockui.min.js",
                        "~/Content/admin/assets/plugins/jquery-unveil/jquery.unveil.min.js",
                        "~/Content/admin/assets/plugins/jquery-scrollbar/jquery.scrollbar.min.js",
                        "~/Content/admin/assets/plugins/jquery-numberAnimate/jquery.animateNumbers.js",
                        "~/Content/admin/assets/plugins/jquery-validation/js/jquery.validate.min.js",
                        "~/Content/admin/assets/plugins/bootstrap-select2/select2.min.js",
                        "~/Content/admin/webarch/js/webarch.js",
                        "~/Content/admin/assets/js/chat.js"

            ));

            bundles.Add(new ScriptBundle("~/bundles/main-script").Include(
                    "~/Scripts/scripts.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/admin-css").Include(
                    "~/Content/admin/assets/plugins/pace/pace-theme-flash.css",
                    "~/Content/admin/assets/plugins/bootstrapv3/css/bootstrap.min.css",
                    "~/Content/admin/assets/plugins/bootstrapv3/css/bootstrap-theme.min.css",
                    "~/Content/admin/assets/plugins/animate.min.css",
                    "~/Content/admin/assets/plugins/jquery-scrollbar/jquery.scrollbar.css",
                    "~/Content/admin/webarch/css/webarch.css"    
                
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