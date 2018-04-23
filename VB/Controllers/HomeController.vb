Imports System.IO
Imports System.Web.Mvc
Imports DevExpress.DashboardWeb

Namespace dxMvcDashboardSample
    Partial Public Class HomeController
        Inherits Controller

        Public Function Index(ByVal _workingMode As String, ByVal dashboardId As String) As ActionResult
            Return View("Index", New DashboardControlModel With {
                .DashboardId = dashboardId,
                .WorkingMode = If(String.IsNullOrEmpty(_workingMode), WorkingMode.Designer, DirectCast(System.Enum.Parse(GetType(WorkingMode), _workingMode), WorkingMode))
            })
        End Function
        Public Function Viewer(ByVal dashboardId As String) As ActionResult
            Return View("Index", New DashboardControlModel With { _
                .DashboardId = dashboardId, _
                .WorkingMode = WorkingMode.ViewerOnly _
            })
        End Function

        Public Function Xml(ByVal dashboardId As String) As ActionResult
            Dim st As IDashboardStorage = TryCast(DevExpress.PublicDemo.SessionDashboardStorage.Instance, IDashboardStorage)
            If String.IsNullOrEmpty(dashboardId) Then
                Dim dashboards As IEnumerable(Of DashboardInfo) = st.GetAvailableDashboardsInfo()
                dashboardId = CType(dashboards.ElementAt(0), DashboardInfo).ID
            End If
            Dim xdoc As XDocument = st.LoadDashboard(dashboardId)
            Dim stream As New MemoryStream()
            xdoc.Save(stream)
            stream.Seek(0, SeekOrigin.Begin)

            Return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, dashboardId & ".xml")
        End Function
    End Class
    Public Class DashboardControlModel
        Public Property DashboardId() As String
        Public Property WorkingMode() As WorkingMode
    End Class
End Namespace