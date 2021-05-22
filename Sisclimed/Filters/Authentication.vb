Imports System.Net.Http
Imports System.ServiceModel.Web
Imports Sisclimed.UsuarioMembership

Public Class ActionAuthenticationAttribute
    Inherits System.Web.Http.Filters.ActionFilterAttribute

    Public Overrides Async Function OnActionExecutingAsync(actionContext As Http.Controllers.HttpActionContext, cancellationToken As Threading.CancellationToken) As Threading.Tasks.Task
        Try
            Await MyBase.OnActionExecutingAsync(actionContext, cancellationToken)

            Dim user_status As UserStatus = UserStatus.Unauthorized, auth As String = ""

            If SkipAuthorization(actionContext) Then
                Return
            End If

            Dim Token As String = UserAccessControl.GetCookieSession()

            If (actionContext.ControllerContext.RouteData.Values.Keys.Contains(Token)) Then
                actionContext.ControllerContext.RouteData.Values.Add("ValidToken", Token)
                actionContext.ControllerContext.RouteData.Values.Add("Validated", "OK")
                Return
            ElseIf Not actionContext.Request.Headers.Authorization Is Nothing Then
                auth = actionContext.Request.Headers.Authorization.Parameter.ToString()
                If auth <> "Q1NT" Then
                    user_status = UserAccessControl.Authentication(auth)
                End If
            End If

            If actionContext.Request.Headers.Authorization Is Nothing OrElse auth <> "Q1NT" Then
                If user_status = UserStatus.ValidLogin Then
                    actionContext.ControllerContext.RouteData.Values.Add(Token, True)
                    actionContext.ControllerContext.RouteData.Values.Add("ValidToken", Token)
                    actionContext.ControllerContext.RouteData.Values.Add("Validated", "OK")
                Else
                    actionContext.Response = New System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
                    actionContext.Response.Content = New StringContent([Enum].GetName(GetType(UserStatus), user_status))
                End If
            ElseIf Not String.IsNullOrEmpty(Token) Then
                actionContext.ControllerContext.RouteData.Values.Add("ValidToken", Token)
                actionContext.ControllerContext.RouteData.Values.Add("Validated", "OK")
            End If
        Catch ex As Exception
        End Try
    End Function

    Private Shared Function SkipAuthorization(actionContext As Http.Controllers.HttpActionContext) As Boolean
        Return actionContext.ActionDescriptor.GetCustomAttributes(Of AllowAnonymousAttribute)().Any() OrElse actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes(Of AllowAnonymousAttribute)().Any()
    End Function

End Class