using System.Web;
using System.Web.Optimization;

namespace service_station
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/intlTelInput").Include(
                      "~/Scripts/intlTelInput.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/jq").Include(
                      "~/Scripts/JQScripts.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryinputmask").Include(
                   "~/Scripts/jquery.inputmask/inputmask.js",
                   "~/Scripts/jquery.inputmask/inputmask.regex.extensions.js",
                    "~/Scripts/jquery.inputmask/jquery.inputmask.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/intlTelInput.css",
                      "~/Content/themes/base/datepicker.css",
                      "~/Content/themes/base/jquery-ui.css"));
        }
    }
}
