using System.Web;
using System.Web.Optimization;

namespace EmployeeMgmt.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js").Include("~/Scripts/jquery-ui-1.12.1.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                       "~/Content/CustomJS/Employee.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/CustomCss/customnavigation.css",
                       "~/Content/CustomCss/general.css",
                        "~/Content/themes/base/jquery-ui.min.css",
                        "~/Content/themes/base/datepicker.css",
                      "~/Content/site.css"));
        }
    }
}
