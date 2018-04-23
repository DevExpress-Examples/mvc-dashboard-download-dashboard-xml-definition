using System;
using System.Linq;
using System.Web.Mvc;
using DevExpress.DashboardWeb;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;

namespace dxMvcDashboardSample {
    public partial class HomeController : Controller {
        public ActionResult Index(string workingMode, string dashboardId) {
            return View("Index", new DashboardControlModel {
                DashboardId = dashboardId,
                WorkingMode = string.IsNullOrEmpty(workingMode) ? WorkingMode.Designer : (WorkingMode)Enum.Parse(typeof(WorkingMode), workingMode)
            });
        }
        public ActionResult Viewer(string dashboardId) {
            return View("Index", new DashboardControlModel {
                DashboardId = dashboardId,
                WorkingMode = WorkingMode.ViewerOnly
            });
        }

        public ActionResult Xml(string dashboardId) {
            IDashboardStorage st = DevExpress.PublicDemo.SessionDashboardStorage.Instance as IDashboardStorage;
            if(string.IsNullOrEmpty(dashboardId)) {
                IEnumerable<DashboardInfo> dashboards = st.GetAvailableDashboardsInfo();
                dashboardId = ((DashboardInfo)dashboards.ElementAt(0)).ID;
            }
            XDocument xdoc = st.LoadDashboard(dashboardId);
            MemoryStream stream = new MemoryStream();
            xdoc.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, dashboardId + ".xml");
        }
    }
    public class DashboardControlModel {
    	public string DashboardId { get; set; }
        public WorkingMode WorkingMode { get; set; }
    }
}