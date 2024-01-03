using System.Web;
using System.Web.Optimization;

namespace POS_Online
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Estilos/css_layout").Include(
                        "~/Estilos/Principal.css",
                        "~/Estilos/fontello.css",
                        "~/Estilos/Medicamentos.css"));

            bundles.Add(new StyleBundle("~/Estilos/css_login").Include(
                        "~/Content/Login.css"));


            bundles.Add(new StyleBundle("~/Estilos/css_interfarm").Include(
                        "~/Estilos/IF.css",
                        "~/Estilos/fontello.css"));

            bundles.Add(new StyleBundle("~/Estilos/css").Include("~/Estilos/site.css"));

            bundles.Add(new StyleBundle("~/Estilos/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}