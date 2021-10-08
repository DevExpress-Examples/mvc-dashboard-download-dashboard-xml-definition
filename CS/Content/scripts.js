function onBeforeRender(sender, eventArgs) {   
    var dashboardControl = sender.getDashboardControl();
    dashboardControl.registerExtension(new DevExpress.Dashboard.DashboardPanelExtension(dashboardControl));
    dashboardControl.registerExtension(new DownloadAndSaveAsDashboardExtension(dashboardControl))
}