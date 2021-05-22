Public Class pacienteAgenda

    Public Property id As Integer

    Public Property nome As String

    Public Property dataNasc As Date

    Public Property endereco As String

    Public Property numero As String

    Public Property complemento As String

    Public Property bairro As String

    Public Property estado As String

    Public Property cidade As String

    Public Sub New()

    End Sub

    Public Sub New(id As Integer, nome As String, dataNasc As Date, endereco As String, numero As String, complemento As String, bairro As String, estado As String, cidade As String)
        Me.id = id
        Me.nome = nome
        Me.dataNasc = dataNasc
        Me.endereco = endereco
        Me.numero = numero
        Me.complemento = complemento
        Me.bairro = bairro
        Me.estado = estado
        Me.cidade = cidade
    End Sub
End Class

Public Class pacienteAgendaList
    Inherits List(Of pacienteAgenda)

    Public Sub New()
    End Sub

    Public Shared Function GetPacienteAgenda(Nome As String) As pacienteAgendaList
        Return Get_PacienteAgenda(Nome)
    End Function

End Class