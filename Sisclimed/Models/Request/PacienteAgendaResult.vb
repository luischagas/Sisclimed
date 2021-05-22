Imports System.Runtime.Serialization

<DataContract()>
Public Class pacienteAgendaResult

    <DataMember()>
    Public Property value As pacienteAgendaList

    Public Sub New()
    End Sub

    Public Sub New(value As pacienteAgendaList)
        Me.value = value

    End Sub

End Class