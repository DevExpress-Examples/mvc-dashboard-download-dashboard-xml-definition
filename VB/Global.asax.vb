Imports System.Web.Mvc
Imports System.Web.Routing
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports DevExpress.DashboardWeb.Mvc
Imports DevExpress.DataAccess.Excel
Imports DevExpress.Utils
Imports DevExpress.Web.Mvc

Namespace DevExpress.Razor
    ' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    ' visit http://go.microsoft.com/?LinkId=9394801

    Public Class MvcApplication
        Inherits System.Web.HttpApplication

        Public Shared Sub RegisterGlobalFilters(ByVal filters As GlobalFilterCollection)
            filters.Add(New HandleErrorAttribute())
        End Sub

        Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
            routes.MapDashboardRoute()

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
            routes.MapRoute("Default", "{controller}/{action}/{id}", New With { _
                Key .controller = "Home", _
                Key .action = "Index", _
                Key .id = UrlParameter.Optional _
            })
        End Sub

        Protected Sub Session_Start()
            RegisterDefaultDashboard("Presidents")
        End Sub

        Protected Sub Application_Start()
            AreaRegistration.RegisterAllAreas()

            RegisterGlobalFilters(GlobalFilters.Filters)
            RegisterRoutes(RouteTable.Routes)

            ModelBinders.Binders.DefaultBinder = New DevExpressEditorsBinder()
            UrlAccessSecurityLevelSetting.SecurityLevel = UrlAccessSecurityLevel.FilesFromBaseDirectory

            ' See https://documentation.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardFileStorage.class
            'DashboardConfigurator.Default.SetDashboardStorage(new DashboardFileStorage(@"~/App_Data/Dashboards"));
            DashboardConfigurator.Default.SetDashboardStorage(PublicDemo.SessionDashboardStorage.Instance) ' this code based on DevExpress demo

            Dim ds As New DashboardExcelDataSource("Excel Data Source")
            ds.SourceOptions = New ExcelSourceOptions(New ExcelWorksheetSettings("List"))
            ds.FileName = "|DataDirectory|Data\PresidentsData.xlsx"
            Dim dataSourceStorage As New DataSourceInMemoryStorage()
            dataSourceStorage.RegisterDataSource("dashboardExcelDataSource1", ds.SaveToXml())
            DashboardConfigurator.Default.SetDataSourceStorage(dataSourceStorage)
        End Sub

        Private Sub RegisterDefaultDashboard(ByVal dashboardId As String)
            Dim dashboardLocalPath As String = Server.MapPath(String.Format("~/App_Data/Dashboards/{0}.xml", dashboardId))
            PublicDemo.SessionDashboardStorage.Instance.RegisterDashboard(dashboardId, XDocument.Load(dashboardLocalPath))
        End Sub

    End Class
End Namespace