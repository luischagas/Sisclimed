Public Class ActionAuthorizationAttribute
    Inherits System.Web.Mvc.AuthorizeAttribute

    'https://www.simple-talk.com/dotnet/asp.net/thoughts-on-asp.net-mvc-authorization-and-security-/

    Public Overrides Sub OnAuthorization(filterContext As AuthorizationContext)
        Try
            MyBase.OnAuthorization(filterContext)

            If SkipAuthorization(filterContext) OrElse Not filterContext.HttpContext.Response.Headers.GetValues(UserAccessControl.GetCookieSession()) Is Nothing Then
                Return
            End If

            If TypeOf filterContext.Result Is HttpUnauthorizedResult Then
                filterContext.Result = New RedirectResult("/")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Overrides Function AuthorizeCore(httpContext__1 As HttpContextBase) As Boolean
        Try
            Dim tkStatus As Integer = UserAccessControl.ValidToken()

            If tkStatus = 1 Then
                httpContext__1.Response.AddHeader(UserAccessControl.GetCookieSession(), "true")
                Return True
            End If

            httpContext__1.Response.Headers.Remove(UserAccessControl.GetCookieSession())
        Catch ex As Exception
        End Try

        Return False
    End Function

    Private Shared Function SkipAuthorization(filterContext As AuthorizationContext) As Boolean
        Return filterContext.ActionDescriptor.GetCustomAttributes(GetType(AllowAnonymousAttribute), True).Any() OrElse filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(GetType(AllowAnonymousAttribute), True).Any()
    End Function

End Class
