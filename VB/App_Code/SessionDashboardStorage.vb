Imports System.Web.SessionState
Imports DevExpress.DashboardWeb

Namespace DevExpress.PublicDemo
    Public Class SessionDashboardStorage
        Inherits DashboardStorageBase

        Private Const DashboardStorageKey As String = "DashboardStorage"


        Private Shared instance_Renamed As SessionDashboardStorage = Nothing

        Public Shared ReadOnly Property Instance() As SessionDashboardStorage
            Get
                If instance_Renamed Is Nothing Then
                    instance_Renamed = New SessionDashboardStorage()
                End If
                Return instance_Renamed
            End Get
        End Property

        Private ReadOnly Property Storage() As Dictionary(Of String, XDocument)
            Get
                Dim session As HttpSessionState = HttpContext.Current.Session
                If session IsNot Nothing Then

                    Dim storage_Renamed As Dictionary(Of String, XDocument) = TryCast(session(DashboardStorageKey), Dictionary(Of String, XDocument))
                    If storage_Renamed Is Nothing Then
                        storage_Renamed = New Dictionary(Of String, XDocument)()
                        session(DashboardStorageKey) = storage_Renamed
                        Return storage_Renamed
                    End If
                    Return storage_Renamed
                End If
                Throw New ArgumentNullException("Session is not available")
            End Get
        End Property

        Protected Sub New()
            MyBase.New()

        End Sub

        Protected Overrides Function GetAvailableDashboardsID() As IEnumerable(Of String)
            Return Storage.Keys
        End Function
        Protected Overrides Function LoadDashboard(ByVal dashboardID As String) As XDocument
            Return Storage(dashboardID)
        End Function
        Protected Overrides Sub SaveDashboard(ByVal dashboardID As String, ByVal dashboard As XDocument, ByVal createNew As Boolean)
            If createNew Xor Storage.ContainsKey(dashboardID) Then
                Storage(dashboardID) = dashboard
            End If
        End Sub
        Public Sub RegisterDashboard(ByVal dashboardID As String, ByVal dashboard As XDocument)
            SaveDashboard(dashboardID, dashboard, True)
        End Sub
    End Class
End Namespace