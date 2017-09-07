using System.Web;
using System.Web.Optimization;

namespace HouseOfSynergy.AffinityDms.WebRole
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //====================================================================================================
            #region Styles.
            //====================================================================================================


            bundles.Add
            (
                new StyleBundle("~/bundles/DiscoursesCSS").Include("~/Content/Discourses.css")
            );

            bundles.Add
            (
                new StyleBundle("~/bundles/DocumentsCSS").Include("~/Content/Documents.css")
            );
            bundles.Add
            (
                new StyleBundle("~/bundles/DocumentViewerCSS").Include("~/Content/DocumentViewer.css")
            );
            bundles.Add
            (
                new StyleBundle("~/bundles/FormsCSS").Include("~/Content/Forms.css")
            );
            bundles.Add
            (
                new StyleBundle("~/bundles/OcrClassificationsCSS").Include("~/Content/OcrClassifications.css")
            );
            bundles.Add
            (
                new StyleBundle("~/bundles/UsersCSS").Include("~/Content/Users.css")
            );
            bundles.Add
           (
               new StyleBundle("~/bundles/ChatThemeCSS").Include("~/Content/ChatCustomTheme.css")
           );


            bundles.Add
            (
                new StyleBundle("~/bundles/kendoCSS").Include//"~/Content/kendo/css"
                (
                    //"~/Content/kendo/2016.1.406/kendo.common-bootstrap.min.css",
                    //"~/Content/kendo/2016.1.406/kendo.bootstrap.min.css"
                    "~/Content/kendo/2016.1.406/kendo.common.core.min.css",
                    "~/Content/kendo/2016.1.406/kendo.common.min.css",
                    "~/Content/kendo/kendo.custom.css"
                )
            );

            bundles.Add
            (
                new StyleBundle("~/bundles/css").Include//"~/Content/css"
                (
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/Template.css"
                )
            );
            bundles.Add
            (
                new StyleBundle("~/bundles/SiteCSS").Include//"~/Content/css"
                (
                      "~/Content/Site.css",
                      "~/Content/Template.css"
                )
            );



            bundles.Add
            (
                new StyleBundle("~/bundles/ThemeCSS").Include//"~/Content/Theme/css"
                (
                    //"~/Theme/css/bootstrap-colorpicker.min.css",
                    //"~/Theme/css/bootstrap-datepicker3.min.css",
                    //"~/Theme/css/bootstrap-datetimepicker.min.css",
                    //"~/Theme/css/bootstrap-duallistbox.min.css",
                    //"~/Theme/css/bootstrap-editable.min.css",
                    //"~/Theme/css/bootstrap-multiselect.min.css",
                    //"~/Theme/css/bootstrap-timepicker.min.css",
                    //"~/Theme/css/chosen.min.css",
                    //"~/Theme/css/colorbox.min.css",
                    //"~/Theme/css/daterangepicker.min.css",
                    //"~/Theme/css/dropzone.min.css",
                    //"~/Theme/css/fullcalendar.min.css",
                    //"~/Theme/css/jquery.gritter.min.css",
                    //"~/Theme/css/prettify.min.css",
                    //"~/Theme/css/select2.min.css",
                    //"~/Theme/css/ui.jqgrid.min.css",
                    // "~/Theme/css/ace-skins.min.css",
                    //"~/Theme/css/ace-part2.min.css",
                    "~/Theme/css/bootstrap.min.css",
                    "~/Theme/font-awesome/4.5.0/css/font-awesome.min.css",
                    "~/Theme/css/ace.min.css",
                    "~/Theme/css/custeme.css",
                    "~/Theme/css/optiscroll.css"

                )
            );
            //====================================================================================================
            #endregion Styles.
            //====================================================================================================

            //====================================================================================================
            #region Scripts.
            //====================================================================================================


            bundles.Add
            (
                new ScriptBundle("~/bundles/DiscoursesChatCustomJs").Include("~/Scripts/Tenant/DiscoursesChatCustom.js")
            );

            bundles.Add
            (
                new ScriptBundle("~/bundles/DiscoursesCustomJs").Include("~/Scripts/Tenant/DiscoursesCustom.js")
            );
            bundles.Add
            (
                new ScriptBundle("~/bundles/DocumentsCustomJs").Include("~/Scripts/Tenant/DocumentsCustom.js")
            );
            bundles.Add
            (
                new ScriptBundle("~/bundles/DocumentViewerCustomJs").Include("~/Scripts/Tenant/DocumentViewerCustom.js")
            );
            bundles.Add
            (
                new ScriptBundle("~/bundles/OcrClassificationsCustomJs").Include("~/Scripts/Tenant/OcrClassificationsCustom.js")
            );
            bundles.Add
            (
                new ScriptBundle("~/bundles/SignInCustomJs").Include("~/Scripts/Tenant/SignInCustom.js")
            );
            bundles.Add
            (
                new ScriptBundle("~/bundles/TemplateCustomJs").Include("~/Scripts/Tenant/TemplateCustom.js")
            );
            bundles.Add
            (
                new ScriptBundle("~/bundles/UsersCustomJS").Include("~/Scripts/Tenant/UsersCustom.js")
            );







            bundles.Add
            (
                new ScriptBundle("~/bundles/jQuery").Include //"~/Scripts/jQuery"
                (
                    "~/Scripts/jquery-2.2.1.min.js",
                    "~/Scripts/jquery-ui-1.11.4.min.js",
                    "~/Scripts/jquery.validate.min.js"


                )
            );
            //"~/Theme/js/jquery.mobile.custom.min.js",
            bundles.Add
            (
                new ScriptBundle("~/bundles/ThemeJS").Include
                (
                    "~/Theme/js/tooltip-popup.js",
                   "~/Theme/js/jquery-ui.custom.min.js",
                    "~/Theme/js/jquery.ui.touch-punch.min.js",
                     "~/Theme/js/nice-scroll.js",
                    "~/Theme/js/ace-elements.min.js",
                    "~/Theme/js/ace.min.js",
                    "~/Theme/js/pager.js",
                     "~/Theme/js/togel-menu.js",
                     "~/Theme/js/n-scroll.js",
                     "~/Theme/js/tooltip.js",
                     "~/Theme/js/pie-chart-graph.js",
                     "~/Theme/js/pie-chart.js"
                     
                )
            );


            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add
            (
                new ScriptBundle("~/bundles/bootstrapJS").Include
                (
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js"
                    
                )
            );

            bundles.Add
            (
                new ScriptBundle("~/bundles/TypeScript").Include
                (
                     "~/TypeScript/Entities.js",
                      "~/TypeScript/CustomKendo.js",
                     "~/TypeScript/Settings.js",
                     "~/TypeScript/Tenants/TemplateDesigner.js",
                     "~/TypeScript/Tenants/TemplateTestDesigner.js",
                     "~/TypeScript/Tenants/Documents.js",
                     "~/TypeScript/Tenants/Folders.js",
                     "~/TypeScript/Master/TenantSubscriptionTS.js",
                     "~/TypeScript/Tenants/TenantUsers.js",
                     "~/TypeScript/Tenants/Discourses.js",
                     "~/TypeScript/Leadtools/DocumentViewer.js",
                     "~/TypeScript/Tenants/ScanSessions.js"
                )
            );

            bundles.Add
            (
                new ScriptBundle("~/bundles/PublicScripts").Include
                (
                     "~/TypeScript/Settings.js"
                )
            );

            bundles.Add
            (
                new ScriptBundle("~/bundles/kendoJS").Include
                (
                    "~/Scripts/kendo/2016.1.406/kendo.all.min.js",
                    "~/Scripts/kendo/2016.1.406/kendo.aspnetmvc.min.js"
                )
            );
            bundles.Add
           (
               new ScriptBundle("~/bundles/LeadtoolsJS").Include //"~/Scripts/Leadtools"
               (
                   "~/Scripts/LeadTools/Leadtools.js",
                   "~/Scripts/LeadTools/Leadtools.Controls.js",
                   "~/Scripts/LeadTools/Leadtools.Documents.js",
                   "~/Scripts/LeadTools/Leadtools.*"
               )
           );
            bundles.Add
            (
                new ScriptBundle("~/bundles/GreenSock").Include
                (
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/TimelineMax.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/TweenMax.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/utils/Draggable.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/utils/SplitText.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/ThrowPropsPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/AttrPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/BezierPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/ColorPropsPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/CSSPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/CSSRulePlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/DirectionalRotationPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/DrawSVGPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/EaselPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/EndArrayPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/KineticPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/MorphSVGPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/Physics2DPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/PhysicsPropsPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/RaphaelPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/RoundPropsPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/ScrambleTextPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/ScrollToPlugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/TEMPLATE_Plugin.js",
                    "~/Scripts/GreenSock/GreenSock Business 1.18.2/src/uncompressed/plugins/TextPlugin.js"
                )
            );

            //====================================================================================================
            #endregion Scripts.
            //====================================================================================================
        }
    }
}