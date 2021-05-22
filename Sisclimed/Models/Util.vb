Imports System.Web
Public Module Util
    <System.Runtime.CompilerServices.Extension> _
    Public Function IsActive(html As HtmlHelper, control As String, action As String) As String
        Dim routeData = html.ViewContext.RouteData

        Dim routeAction = DirectCast(routeData.Values("action"), String)
        Dim routeControl = DirectCast(routeData.Values("controller"), String)

        ' both must match
        Dim returnActive = control = routeControl AndAlso action = routeAction

        Return If(returnActive, "active", "")
    End Function
End Module
