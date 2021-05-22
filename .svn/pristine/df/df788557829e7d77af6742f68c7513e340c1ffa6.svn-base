<DataContract()>
Public Class Jornada

    <DataMember()>
    Public Property DataHora As Date

    <DataMember()>
    Public Property Data As String

    <DataMember()>
    Public Property Hora As String

    <DataMember()>
    Public Property DataHoraValue As String

    <DataMember()>
    Public Property DiaSemana As String

    Public Sub New()

    End Sub

End Class

Public Class JornadaList
    Inherits List(Of Jornada)

    Public Loaded As Boolean

    Public Sub New()

    End Sub

    Public Sub LoadInfo(MedicoId As Integer)
        Dim DataAccess As New DataAccess, Vals As New List(Of Object), parametros As New List(Of IDbDataParameter)

        Try
            DataAccess.setConection(SisclimedConnectionString, ApplicationName)

            parametros.Add(DataAccess.CreateInParam("@Medico_Id", SqlDbType.Int, MedicoId))

            If DataAccess.execReaderProcedure("usp_Sisclimed_GetJornada", parametros, Vals) AndAlso Not Vals Is Nothing Then
                Me.Clear()
                For Each v In Vals
                    Me.Add(New Jornada With {.DataHora = v(0), .DiaSemana = v(1), .Data = v(2), .Hora = v(3), .DataHoraValue = v(2) & " " & v(3)})
                Next
                Me.Loaded = True
            End If
        Catch ex As Exception
        End Try
    End Sub


End Class