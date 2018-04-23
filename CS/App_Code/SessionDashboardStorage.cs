using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using System.Web;
using System.Web.SessionState;

namespace DevExpress.PublicDemo {
    public class SessionDashboardStorage : DashboardStorageBase {
        const string DashboardStorageKey = "DashboardStorage";

        private static SessionDashboardStorage instance = null;

        public static SessionDashboardStorage Instance {
            get {
                if(instance == null) {
                    instance = new SessionDashboardStorage();
                }
                return instance;
            }
        }

        Dictionary<string, XDocument> Storage {
            get {
                HttpSessionState session = HttpContext.Current.Session;
                if(session != null) {
                    Dictionary<string, XDocument> storage = session[DashboardStorageKey] as Dictionary<string, XDocument>;
                    if(storage == null) {
                        storage = new Dictionary<string, XDocument>();
                        session[DashboardStorageKey] = storage;
                        return storage;
                    }
                    return storage;
                }
                throw new ArgumentNullException("Session is not available");
            }
        }

        protected SessionDashboardStorage() : base() {

        }

        protected override IEnumerable<string> GetAvailableDashboardsID() {
            return Storage.Keys;
        }
        protected override XDocument LoadDashboard(string dashboardID) {
            return Storage[dashboardID];
        }
        protected override void SaveDashboard(string dashboardID, XDocument dashboard, bool createNew) {
            if(createNew ^ Storage.ContainsKey(dashboardID))
                Storage[dashboardID] = dashboard;
        }
        public void RegisterDashboard(string dashboardID, XDocument dashboard) {
            SaveDashboard(dashboardID, dashboard, true);
        }
    }
}