Public Class comboValue

    Public Property text As String

    Public Property value As Integer



    Public Sub New()

    End Sub

    Public Sub New(text As String, value As Integer)
        Me.text = text
        Me.value = value
    End Sub
End Class

Public Class comboValueList
    Inherits List(Of comboValue)

    Public Sub New()
    End Sub

End Class