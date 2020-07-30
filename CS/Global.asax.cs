using System.Web.Mvc;
using System.Web.Routing;
using DevExpress.DashboardWeb;
using DevExpress.DashboardWeb.Mvc;
using DevExpress.Web.Mvc;
using System.Xml.Linq;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.Excel;
using DevExpress.Security.Resources;

namespace DevExpress.Razor {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.MapDashboardRoute();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Session_Start() {
            RegisterDefaultDashboard("Presidents");
        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            
            ModelBinders.Binders.DefaultBinder = new DevExpressEditorsBinder();
            AccessSettings.StaticResources.TrySetRules(DirectoryAccessRule.Allow());

            // See https://documentation.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardFileStorage.class
            //DashboardConfigurator.Default.SetDashboardStorage(new DashboardFileStorage(@"~/App_Data/Dashboards"));
            DashboardConfigurator.Default.SetDashboardStorage(PublicDemo.SessionDashboardStorage.Instance);// this code based on DevExpress demo

            DashboardExcelDataSource ds = new DashboardExcelDataSource("Excel Data Source");
            ds.SourceOptions = new ExcelSourceOptions(new ExcelWorksheetSettings("List"));
            ds.FileName = @"|DataDirectory|Data\PresidentsData.xlsx";
            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
            dataSourceStorage.RegisterDataSource("dashboardExcelDataSource1", ds.SaveToXml());
            DashboardConfigurator.Default.SetDataSourceStorage(dataSourceStorage);
        }        

        void RegisterDefaultDashboard(string dashboardId) {
            string dashboardLocalPath = Server.MapPath(string.Format(@"~/App_Data/Dashboards/{0}.xml", dashboardId));
            PublicDemo.SessionDashboardStorage.Instance.RegisterDashboard(dashboardId, XDocument.Load(dashboardLocalPath));
        }

    }
}