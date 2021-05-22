Public Class ModelosRS

    Public Function DataAccess_execReaderProcedure(IdMedico As Integer) As String
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), Parameters As New List(Of IDbDataParameter), returnValue = "ERROR"
        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            Parameters.Add(DataAccess.CreateInParam("@IdMedico", SqlDbType.Int, IdMedico))

            If DataAccess.execReaderProcedure("ModeloRS_GetMedicos", Parameters, Vals) AndAlso Not Vals Is Nothing Then
                For Each v In Vals
                    returnValue = "OK"
                Next
            End If
        Catch ex As Exception
        Finally
            If Not DataAccess Is Nothing Then DataAccess.Dispose() : DataAccess = Nothing
            Vals = Nothing
        End Try
        Return returnValue
    End Function

    Public Function DataAccess_execReaderSQLCommand(IdMedico As Integer) As String
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), SQLCommand As New StringBuilder, returnValue = "ERROR"
        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            SQLCommand.AppendLine("SELECT [id_medico],[id_pessoa],[crm],[Especialidade] FROM [Medico] ")

            If IdMedico > 0 Then
                SQLCommand.AppendLine(" WHERE ID_MEDICO = " & IdMedico)
            End If

            If DataAccess.execReaderSQLCommand(SQLCommand.ToString, Vals) AndAlso Not Vals Is Nothing Then
                For Each v In Vals
                    returnValue = "OK"
                Next
            End If
        Catch ex As Exception
        Finally
            If Not DataAccess Is Nothing Then DataAccess.Dispose() : DataAccess = Nothing
            Vals = Nothing
        End Try
        Return returnValue
    End Function

    Public Function DataAccess_execReaderSQLCommandScalar() As String
        Dim DataAccess As New DataAccess, Val As New Object, SQLCommand As New StringBuilder, returnValue = "-1"
        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            SQLCommand.AppendLine("SELECT Count(*) FROM Medico ")

            If DataAccess.execReaderSQLCommandScalar(SQLCommand.ToString, Val) AndAlso Not Val Is Nothing Then
                returnValue = Val
            End If
        Catch ex As Exception
        Finally
            If Not DataAccess Is Nothing Then DataAccess.Dispose() : DataAccess = Nothing
            Val = Nothing
        End Try
        Return returnValue
    End Function

End Class
