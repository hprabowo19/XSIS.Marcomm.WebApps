using System.Web;
using System.Web.Optimization;

namespace XSIS.Marcomm.WebApps
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/csstemplate").Include(
                        "~/Content/css/app_1.min.css",
                        "~/Content/css/app_2.min.css"));

            bundles.Add(new StyleBundle("~/Content/cssvendortemplate").Include(
                        "~/Content/vendors/bower_components/fullcalendar/dist/fullcalendar.min.css",
                        "~/Content/vendors/bower_components/animate.css/animate.min.css",
                        "~/Content/vendors/bower_components/material-design-iconic-font/dist/css/material-design-iconic-font.min.css",
                        "~/Content/vendors/bower_components/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.min.css",
                        "~/Content/vendors/bower_components/bootstrap-select/dist/css/bootstrap-select.css",
                        "~/Content/vendors/bower_components/nouislider/distribute/nouislider.min.css",
                        "~/Content/vendors/bower_components/fullcalendar/dist/fullcalendar.min.css",
                        "~/Content/vendors/bower_components/eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css",
                        "~/Content/vendors/bower_components/dropzone/dist/min/dropzone.min.css",
                        "~/Content/vendors/farbtastic/farbtastic.css",
                        "~/Content/vendors/bower_components/chosen/chosen.css",
                        "~/Content/vendors/summernote/dist/summernote.css",
                        "~/Content/vendors/bower_components/sweetalert2/dist/sweetalert2.min.css"));

            bundles.Add(new StyleBundle("~/Content/cssmystyle").Include(
                "~/Content/css/my-style.css"));

            bundles.Add(new ScriptBundle("~/bundles/menu-access").Include(
                "~/Scripts/js/menu-access.js"));

            bundles.Add(new ScriptBundle("~/bundles/transaction-souvenir").Include(
                "~/Scripts/js/transaction-souvenir.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerytemplate").Include(
                        "~/Content/vendors/bower_components/jquery/dist/jquery.min.js",
                        "~/Content/vendors/bower_components/flot/jquery.flot.js",
                        "~/Content/vendors/bower_components/flot/jquery.flot.resize.js",
                        "~/Content/vendors/sparklines/jquery.sparkline.min.js",
                        "~/Content/vendors/bower_components/jquery/dist/jquery.min.js",
                        "~/Content/vendors/bower_components/jquery.easy-pie-chart/dist/jquery.easypiechart.min.js",
                        "~/Content/vendors/bower_components/simpleWeather/jquery.simpleWeather.min.js",
                        "~/Content/vendors/bower_components/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js",
                        "~/Content/vendors/bower_components/chosen/chosen.jquery.js",
                        "~/Content/vendors/bower_components/jquery-mask-plugin/dist/jquery.mask.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstraptemplate").Include(
                "~/Content/vendors/bower_components/bootstrap/dist/js/bootstrap.min.js",
                "~/Content/vendors/bower_components/flot.curvedlines/curvedLines.js",
                "~/Content/vendors/bower_components/moment/min/moment.min.js",
                "~/Content/vendors/bower_components/fullcalendar/dist/fullcalendar.min.js",
                "~/Content/vendors/bower_components/Waves/dist/waves.min.js",
                "~/Content/vendors/bootstrap-growl/bootstrap-growl.min.js",
                "~/Content/vendors/bower_components/bootstrap-select/dist/js/bootstrap-select.js",
                "~/Content/vendors/bower_components/nouislider/distribute/nouislider.min.js",
                "~/Content/vendors/bower_components/eonasdan-bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js",
                "~/Content/vendors/bower_components/typeahead.js/dist/typeahead.bundle.min.js",
                "~/Content/vendors/bower_components/dropzone/dist/min/dropzone.min.js",
                "~/Content/vendors/summernote/dist/summernote-updated.min.js",
                "~/Content/vendors/bower_components/sweetalert2/dist/sweetalert2.min.js",
                "~/Content/vendors/fileinput/fileinput.min.js",
                "~/Content/vendors/farbtastic/farbtastic.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/apptemplate").Include(
                      "~/Scripts/js/app.min.js"));
        }
    }
}
