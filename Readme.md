<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128579069/21.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T585658)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# Dashboard for MVC - How to enable users to download a dashboard XML definition

The example shows how to allow end users to save a dashboard to a [session](https://docs.microsoft.com/en-us/dotnet/api/system.web.sessionstate.httpsessionstate) and download a dashboard XML definition on the client side. For this, use the corresponding **Download** and **Save As...** buttons in the [Toolbox](https://docs.devexpress.com/Dashboard/117442/web-dashboard/ui-elements/toolbox).

![](web-dashboard.png)

## Files to Review

* [SessionDashboardStorage.cs](./CS/App_Code/SessionDashboardStorage.cs)
* [downloadAndSaveAsExtension.js](./CS/Content/downloadAndSaveAsExtension.js) 
* [scripts.js](./CS/Content/scripts.js)
* [HomeController.cs](./CS/Controllers/HomeController.cs)
* [Global.asax.cs](./CS/Global.asax.cs)
* [Index.cshtml](./CS/Views/Home/Index.cshtml)

## Documentation

* [Save a Dashboard](https://docs.devexpress.com/Dashboard/116992/web-dashboard/create-dashboards-on-the-web/save-a-dashboard)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=mvc-dashboard-download-dashboard-xml-definition&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=mvc-dashboard-download-dashboard-xml-definition&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
