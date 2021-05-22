'RSilva v. 07/08/2014
'RSilva v. 21/10/2015
'RSilva v. 23/10/2015
'RSilva v. 03/03/2016
'RSilva v. 11/07/2016

Imports System.Data.SqlClient
Imports System.IO
Imports System.ComponentModel
Imports System.Text

Public Class DataAccess
    Implements IDisposable

    Public Enum DataAccessStatus
        OK
        Deadlock
    End Enum

    Public Shared Event SendError(Proceeding As String, ex As Exception)

    Public Shared AppPrefix As String = ""

    Private Shared _Error As Boolean

    Private Shared _connectionTimeOut As Integer = 10

    Public Shared ReadOnly MinValue As String = "#1/1/1900#"

    Public Shared Property SaveLog As Boolean = True

    Private Shared _pathLogFile As String = ""
    Public Shared Property PathLogFile As String
        Set(value As String)
            Try
                _pathLogFile = value
                If Not Directory.Exists(value & "\LOG") Then
                    Directory.CreateDirectory(value & "\LOG")
                End If
            Catch ex As Exception
            End Try
        End Set
        Get
            Return _pathLogFile
        End Get
    End Property
    Public Property SaveLogFile As String = "LOG\LogDataAccess.txt"

    Public Property ShowMessages As Boolean

    Public ConnectionInfoMessage As New StringBuilder

    Public Property CommandTimeOut As Integer = 30
    Public Property ConnectionTimeOut As Integer
        Get
            Return _connectionTimeOut
        End Get
        Set(ByVal value As Integer)
            _connectionTimeOut = value
        End Set
    End Property

    Public Property ApplicationName As String = ""

    Public Property Connection As New SqlConnection

    Public Sub New(Optional ShowMessages As Boolean = False)
        Me.ShowMessages = ShowMessages
    End Sub

    Public Sub New(ByVal SqlConnection As SqlConnection)
        Me.Connection = SqlConnection
    End Sub

    Public Sub New(ByVal ConnectionString As String)
        Me.Connection.ConnectionString = ConnectionString
    End Sub

    Public Shared Function GetError() As Boolean
        Return _Error
    End Function

    Public Shared Sub ResetError()
        _Error = False
    End Sub

    Public Sub setConection(ByVal server As String, ByVal database As String, ByVal user As String, ByVal pass As String, ApplicationName As String)
        Me.Connection = New SqlConnection(String.Format("Server={0};Initial Catalog={1};User ID={2};Password={3};Connection Timeout = {4}{5}", server, database, user, pass, Me.ConnectionTimeOut, IIf(String.IsNullOrEmpty(ApplicationName), "", ";Application Name = " & AppPrefix & ApplicationName & ";")))
    End Sub

    Public Sub setConection(ByVal ConnectionString As String, ApplicationName As String)
        Me.Connection = New SqlConnection(ConnectionString & IIf(String.IsNullOrEmpty(ApplicationName), "", ";Application Name = " & AppPrefix & ApplicationName & ";"))
    End Sub

    Public Shared Function getConectionString(ByVal server As String, ByVal database As String, ByVal user As String, ByVal pass As String, ApplicationName As String) As String
        Return String.Format("Server={0};Initial Catalog={1};User ID={2};Password={3};Connection Timeout = {4}{5}", server, database, user, pass, _connectionTimeOut, IIf(String.IsNullOrEmpty(ApplicationName), "", ";Application Name = " & AppPrefix & ApplicationName & ";"))
    End Function

    Public Sub setConection(Connection As SqlConnection)
        Me.Connection = Connection
    End Sub

    Public Function createCommand(CommandText As String, CommandType As CommandType, ByRef conn As SqlConnection) As SqlCommand
        Dim cmd As SqlCommand = conn.CreateCommand

        cmd.CommandTimeout = CommandTimeOut
        cmd.CommandText = CommandText
        cmd.CommandType = CommandType

        Return cmd
    End Function

    Public Sub createSqlCommand(ByRef SqlCommand As SqlCommand, ByVal SQLTextCommand As String, ByRef conn As SqlConnection)
        SqlCommand = createCommand(SQLTextCommand, CommandType.Text, conn)
    End Sub

    Public Sub createSqlProcedureCommand(ByRef SqlCommand As SqlCommand, ByVal Procedure As String, ByVal Parameters As IList(Of IDbDataParameter), ByRef conn As SqlConnection)
        SqlCommand = createCommand(Procedure, CommandType.StoredProcedure, conn)

        SqlCommand.Parameters.Clear()

        With SqlCommand.Parameters
            For Each p As IDbDataParameter In Parameters
                .Add(p)
            Next
        End With
    End Sub

    Public Function CreateInParam(ByVal param As String, ByVal type As SqlDbType, ByVal value As Object) As SqlParameter
        Dim p As New SqlParameter(param, type)
        p.Value = value
        Return p
    End Function

    Public Sub CreateInParam(ByVal param As String, ByVal type As SqlDbType, ByVal value As Object, ByRef list As List(Of IDbDataParameter))
        Dim p As New SqlParameter(param, type)
        p.Value = value
        list.Add(p)
    End Sub

    Public Function CreateOutPutParam(ByVal param As String, ByVal type As SqlDbType, ByRef list As List(Of IDbDataParameter), Optional Size As Integer = 0) As SqlParameter
        Dim p As New SqlParameter(param, type)
        p.Direction = ParameterDirection.Output
        p.Size = Size
        list.Add(p)
        Return p
    End Function

    Public Function CreateOutPutParam(ByVal param As String, ByVal type As SqlDbType, Optional Size As Integer = 0) As SqlParameter
        Dim p As New SqlParameter(param, type)
        p.Direction = ParameterDirection.Output
        p.Size = Size
        Return p
    End Function

    Public Function CreateReturnParam(ByVal list As List(Of IDbDataParameter)) As SqlParameter
        Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
        p.Direction = ParameterDirection.ReturnValue
        list.Add(p)
        Return p
    End Function

    Public Function CreateReturnParam() As SqlParameter
        Dim p As New SqlParameter("@ReturnValue", SqlDbType.Int)
        p.Direction = ParameterDirection.ReturnValue
        Return p
    End Function

    Public Function execProcedure(ByVal Procedure As String, Optional ByVal Parameters As IList(Of IDbDataParameter) = Nothing, Optional withTransaction As Boolean = False, Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean

        Dim trans As SqlTransaction = Nothing, cmd As SqlCommand = Nothing, conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            If Parameters Is Nothing Then Parameters = New List(Of IDbDataParameter)

            createSqlProcedureCommand(cmd, Procedure, Parameters, conn)

            Using conn

                conn.Open()

                If ShowMessages Then
                    ConnectionInfoMessage.Clear()

                    AddHandler conn.InfoMessage, AddressOf ConnectionInfoMessageSub
                End If

                If withTransaction Then
                    trans = conn.BeginTransaction

                    cmd.Transaction = trans

                    cmd.ExecuteNonQuery()

                    trans.Commit()
                Else
                    cmd.CommandTimeout = CommandTimeOut
                    cmd.ExecuteNonQuery()
                End If
            End Using

            Return True
        Catch ex As Exception
            If withTransaction AndAlso Not trans Is Nothing Then
                Try
                    trans.Rollback()
                Catch ex2 As Exception
                End Try
            End If

            Dim pText As String = ""

            If Not Parameters Is Nothing AndAlso Parameters.Count > 0 Then
                pText = " ==> "
                For Each P In Parameters.ToArray
                    pText &= P.ParameterName & " = " & P.Value & " | "
                Next
            End If

            LogFile("execProcedure: " & Procedure, ex, pText)

            If ex.Message.ToLower().Contains("deadlock") Then Status = DataAccessStatus.Deadlock

            Return False
        Finally
            If Not cmd Is Nothing AndAlso Not cmd.Parameters Is Nothing Then cmd.Parameters.Clear() : cmd.Dispose() : cmd = Nothing
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
            If withTransaction AndAlso Not trans Is Nothing Then trans.Dispose() : trans = Nothing
        End Try
    End Function

    Public Function execReaderProcedure(ByVal Procedure As String, ByRef Vals As List(Of Object), Optional withTransaction As Boolean = False, Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean

        Dim trans As SqlTransaction = Nothing, cmd As SqlCommand = Nothing, conn As SqlConnection = Nothing, Parameters As New List(Of IDbDataParameter)

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            createSqlProcedureCommand(cmd, Procedure, Parameters, conn)

            Using conn

                conn.Open()

                If ShowMessages Then
                    ConnectionInfoMessage.Clear()

                    AddHandler conn.InfoMessage, AddressOf ConnectionInfoMessageSub
                End If

                If withTransaction Then
                    trans = conn.BeginTransaction

                    cmd.Transaction = trans

                    Using dr As IDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Dim val(dr.FieldCount - 1) As Object
                            dr.GetValues(val)
                            Vals.Add(val)
                        End While
                    End Using

                    trans.Commit()
                Else
                    Using dr As IDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Dim val(dr.FieldCount - 1) As Object
                            dr.GetValues(val)
                            Vals.Add(val)
                        End While
                    End Using
                End If

            End Using
            Return True
        Catch ex As Exception
            If withTransaction AndAlso Not trans Is Nothing Then
                Try
                    trans.Rollback()
                Catch ex2 As Exception
                End Try
            End If

            Dim pText As String = ""

            If Not Parameters Is Nothing AndAlso Parameters.Count > 0 Then
                pText = " ==> "
                For Each P In Parameters.ToArray
                    pText &= P.ParameterName & " = " & P.Value & " | "
                Next
            End If

            LogFile("execReaderProcedure: " & Procedure, ex, pText)

            If ex.Message.ToLower().Contains("deadlock") Then
                Status = DataAccessStatus.Deadlock
                Vals = New List(Of Object)
            Else
                Vals = Nothing
            End If

            Return False
        Finally
            If Not cmd Is Nothing AndAlso Not cmd.Parameters Is Nothing Then cmd.Parameters.Clear() : cmd.Dispose() : cmd = Nothing
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
            If withTransaction AndAlso Not trans Is Nothing Then trans.Dispose() : trans = Nothing
        End Try
    End Function

    Public Function execReaderProcedure(ByVal Procedure As String, ByVal Parameters As IList(Of IDbDataParameter), ByRef Vals As List(Of Object), Optional withTransaction As Boolean = False, Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean

        Dim trans As SqlTransaction = Nothing, cmd As SqlCommand = Nothing, conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            createSqlProcedureCommand(cmd, Procedure, Parameters, conn)

            Using conn

                conn.Open()

                If ShowMessages Then
                    ConnectionInfoMessage.Clear()

                    AddHandler conn.InfoMessage, AddressOf ConnectionInfoMessageSub
                End If

                If withTransaction Then
                    trans = conn.BeginTransaction

                    cmd.Transaction = trans

                    Using dr As IDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Dim val(dr.FieldCount - 1) As Object
                            dr.GetValues(val)
                            Vals.Add(val)
                        End While
                    End Using

                    trans.Commit()
                Else
                    Using dr As IDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Dim val(dr.FieldCount - 1) As Object
                            dr.GetValues(val)
                            Vals.Add(val)
                        End While
                    End Using
                End If
            End Using

            Return True
        Catch ex As Exception
            If withTransaction AndAlso Not trans Is Nothing Then
                Try
                    trans.Rollback()
                Catch ex2 As Exception
                End Try
            End If

            Dim pText As String = ""

            If Not Parameters Is Nothing AndAlso Parameters.Count > 0 Then
                pText = " ==> "
                For Each P In Parameters.ToArray
                    pText &= P.ParameterName & " = " & P.Value & " | "
                Next
            End If

            LogFile("execReaderProcedure: " & Procedure, ex, pText)

            If ex.Message.ToLower().Contains("deadlock") Then
                Status = DataAccessStatus.Deadlock
                Vals = New List(Of Object)
            Else
                Vals = Nothing
            End If

            Return False
        Finally
            If Not cmd Is Nothing AndAlso Not cmd.Parameters Is Nothing Then cmd.Parameters.Clear() : cmd.Dispose() : cmd = Nothing
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
            If withTransaction AndAlso Not trans Is Nothing Then trans.Dispose() : trans = Nothing
        End Try
    End Function

    Public Function execSQLCommand(ByVal SQLTextCommand As String, Optional withTransaction As Boolean = False, Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean

        Dim trans As SqlTransaction = Nothing, cmd As SqlCommand = Nothing, conn As SqlConnection = Nothing, Parameters As New List(Of IDbDataParameter)

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            createSqlCommand(cmd, SQLTextCommand, conn)

            Using conn

                conn.Open()

                If ShowMessages Then
                    ConnectionInfoMessage.Clear()

                    AddHandler conn.InfoMessage, AddressOf ConnectionInfoMessageSub
                End If

                If withTransaction Then
                    trans = conn.BeginTransaction

                    cmd.Transaction = trans

                    cmd.ExecuteNonQuery()

                    trans.Commit()
                Else
                    cmd.ExecuteNonQuery()
                End If

            End Using

            Return True
        Catch ex As Exception
            If withTransaction AndAlso Not trans Is Nothing Then
                Try
                    trans.Rollback()
                Catch ex2 As Exception
                End Try
            End If

            LogFile("execSQLCommand: " & SQLTextCommand, ex)

            If ex.Message.ToLower().Contains("deadlock") Then Status = DataAccessStatus.Deadlock

            Return False
        Finally
            If Not cmd Is Nothing AndAlso Not cmd.Parameters Is Nothing Then cmd.Parameters.Clear() : cmd.Dispose() : cmd = Nothing
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
            If withTransaction AndAlso Not trans Is Nothing Then trans.Dispose() : trans = Nothing
        End Try
    End Function

    Public Function execReaderSQLCommand(ByVal SQLTextCommand As String, ByRef Vals As List(Of Object), Optional withTransaction As Boolean = False, Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean

        Dim trans As SqlTransaction = Nothing, cmd As SqlCommand = Nothing, conn As SqlConnection = Nothing, Parameters As New List(Of IDbDataParameter)

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            createSqlCommand(cmd, SQLTextCommand, conn)

            Using conn

                conn.Open()

                If ShowMessages Then
                    ConnectionInfoMessage.Clear()

                    AddHandler conn.InfoMessage, AddressOf ConnectionInfoMessageSub
                End If

                If withTransaction Then
                    trans = conn.BeginTransaction

                    cmd.Transaction = trans

                    Using dr As IDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Dim val(dr.FieldCount - 1) As Object
                            dr.GetValues(val)
                            Vals.Add(val)
                        End While
                    End Using

                    trans.Commit()
                Else
                    Using dr As IDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Dim val(dr.FieldCount - 1) As Object
                            dr.GetValues(val)
                            Vals.Add(val)
                        End While
                    End Using
                End If

            End Using

            Return True
        Catch ex As Exception
            If withTransaction AndAlso Not trans Is Nothing Then
                Try
                    trans.Rollback()
                Catch ex2 As Exception
                End Try
            End If

            LogFile("execReaderSQLCommand: " & SQLTextCommand, ex)

            If ex.Message.ToLower().Contains("deadlock") Then
                Status = DataAccessStatus.Deadlock
                Vals = New List(Of Object)
            Else
                Vals = Nothing
            End If

            Return False
        Finally
            If Not cmd Is Nothing AndAlso Not cmd.Parameters Is Nothing Then cmd.Parameters.Clear() : cmd.Dispose() : cmd = Nothing
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
            If withTransaction AndAlso Not trans Is Nothing Then trans.Dispose() : trans = Nothing
        End Try
    End Function

    Private Sub ConnectionInfoMessageSub(sender As Object, e As System.Data.SqlClient.SqlInfoMessageEventArgs)
        ConnectionInfoMessage.AppendLine(e.Message)
    End Sub

    Public Function execReaderSQLCommandScalar(ByVal SQLTextCommand As String, ByRef Val As Object, Optional withTransaction As Boolean = False, Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean

        Dim trans As SqlTransaction = Nothing, cmd As SqlCommand = Nothing, conn As SqlConnection = Nothing, Parameters As New List(Of IDbDataParameter)

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            createSqlCommand(cmd, SQLTextCommand, conn)

            Using conn

                conn.Open()

                If ShowMessages Then
                    ConnectionInfoMessage.Clear()

                    AddHandler conn.InfoMessage, AddressOf ConnectionInfoMessageSub
                End If

                If withTransaction Then
                    trans = conn.BeginTransaction

                    cmd.Transaction = trans

                    Val = cmd.ExecuteScalar()

                    trans.Commit()
                Else
                    Val = cmd.ExecuteScalar()
                End If

            End Using

            If IsDBNull(Val) Then Val = Nothing

            Return True
        Catch ex As Exception
            If withTransaction AndAlso Not trans Is Nothing Then
                Try
                    trans.Rollback()
                Catch ex2 As Exception
                End Try
            End If

            LogFile("execReaderSQLCommandScalar: " & SQLTextCommand, ex)

            If ex.Message.ToLower().Contains("deadlock") Then
                Status = DataAccessStatus.Deadlock
                Val = New Object
            Else
                Val = Nothing
            End If

            Return False
        Finally
            If Not cmd Is Nothing AndAlso Not cmd.Parameters Is Nothing Then cmd.Parameters.Clear() : cmd.Dispose() : cmd = Nothing
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
            If withTransaction AndAlso Not trans Is Nothing Then trans.Dispose() : trans = Nothing
        End Try
    End Function

    Public Function execProceduresInTransaction(ByVal Procedure As String, ByVal Parameters As List(Of IList(Of IDbDataParameter)), Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean

        Dim trans As SqlTransaction = Nothing, cmd As SqlCommand = Nothing, conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            Using conn

                conn.Open()

                If ShowMessages Then
                    ConnectionInfoMessage.Clear()

                    AddHandler conn.InfoMessage, AddressOf ConnectionInfoMessageSub
                End If

                trans = conn.BeginTransaction

                For Each p In Parameters
                    createSqlProcedureCommand(cmd, Procedure, p, conn)
                    cmd.Transaction = trans
                    cmd.ExecuteNonQuery()
                Next

                trans.Commit()
            End Using

            Return True
        Catch ex As Exception
            If Not trans Is Nothing Then trans.Rollback()
            LogFile("execProceduresInTransaction: " & Procedure, ex)
            If ex.Message.ToLower().Contains("deadlock") Then Status = DataAccessStatus.Deadlock
            Return False
        Finally
            If Not cmd Is Nothing AndAlso Not cmd.Parameters Is Nothing Then cmd.Parameters.Clear() : cmd.Dispose() : cmd = Nothing
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
            If Not trans Is Nothing Then trans.Dispose() : trans = Nothing
        End Try
    End Function

    '---

    Public Function execReaderProcedure(ByVal Procedure As String, ByVal DataSet As DataSet, Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean

        Dim conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            Using da As New SqlDataAdapter(Procedure, conn)
                Dim Parameters As New List(Of IDbDataParameter)
                da.SelectCommand.CommandTimeout = CommandTimeOut
                createSqlProcedureCommand(da.SelectCommand, Procedure, Parameters, conn)
                da.Fill(DataSet)
            End Using

            Return True
        Catch ex As Exception
            LogFile("execReaderProcedure: " & Procedure, ex)

            If ex.Message.ToLower().Contains("deadlock") Then
                Status = DataAccessStatus.Deadlock
                DataSet = New DataSet
            Else
                DataSet = Nothing
            End If

            Return False
        Finally
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
        End Try
    End Function

    Public Function execReaderSQLCommand(ByVal SQLTextCommand As String, ByVal Parameters As IList(Of IDbDataParameter), ByVal DataSet As DataSet, Optional DsName As String = "DataSet", Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean

        Dim cmd As SqlCommand = Nothing, conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            Using da As New SqlDataAdapter(SQLTextCommand, conn)
                da.SelectCommand.CommandTimeout = CommandTimeOut
                createSqlCommand(da.SelectCommand, SQLTextCommand, conn)

                For Each z In Parameters
                    da.SelectCommand.Parameters.Add(z)
                Next

                da.Fill(DataSet)
            End Using

            Return True
        Catch ex As Exception

            Dim pText As String = ""

            If Not Parameters Is Nothing AndAlso Parameters.Count > 0 Then
                pText = " ==> "
                For Each P In Parameters.ToArray
                    pText &= P.ParameterName & " = " & P.Value & " | "
                Next
            End If

            LogFile("execReaderProcedure: " & SQLTextCommand, ex, pText)

            If ex.Message.ToLower().Contains("deadlock") Then
                Status = DataAccessStatus.Deadlock
                DataSet = New DataSet
            Else
                DataSet = Nothing
            End If

            Return False
        Finally

            If Not cmd Is Nothing AndAlso Not cmd.Parameters Is Nothing Then cmd.Parameters.Clear() : cmd.Dispose() : cmd = Nothing
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
        End Try
    End Function

    Public Function execReaderProcedure(ByVal Procedure As String, ByVal DataTable As DataTable, Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean
        Dim conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            Using da As New SqlDataAdapter(Procedure, conn)
                Dim Parameters As New List(Of IDbDataParameter)
                da.SelectCommand.CommandTimeout = CommandTimeOut
                createSqlProcedureCommand(da.SelectCommand, Procedure, Parameters, conn)
                da.Fill(DataTable)
            End Using

            Return True
        Catch ex As Exception
            LogFile("execReaderProcedure: " & Procedure, ex)

            If ex.Message.ToLower().Contains("deadlock") Then
                Status = DataAccessStatus.Deadlock
                DataTable = New DataTable
            Else
                DataTable = Nothing
            End If

            Return False
        Finally
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
        End Try
    End Function

    Public Function execReaderSQLCommand(ByVal SQLCommand As String, ByVal DataSet As DataSet, Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean
        Dim conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            Using da As New SqlDataAdapter(SQLCommand, conn)
                Dim Parameters As New List(Of IDbDataParameter)
                createSqlCommand(da.SelectCommand, SQLCommand, conn)
                da.Fill(DataSet)
            End Using

            Return True
        Catch ex As Exception
            LogFile("execReaderSQLCommand: " & SQLCommand, ex)

            If ex.Message.ToLower().Contains("deadlock") Then
                Status = DataAccessStatus.Deadlock
                DataSet = New DataSet
            Else
                DataSet = Nothing
            End If

            Return False
        Finally
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
        End Try
    End Function

    Public Function execReaderSQLCommand(ByVal SQLCommand As String, ByVal DataTable As DataTable, Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean
        Dim conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            Using da As New SqlDataAdapter(SQLCommand, conn)
                Dim Parameters As New List(Of IDbDataParameter)
                createSqlCommand(da.SelectCommand, SQLCommand, conn)
                da.Fill(DataTable)
            End Using

            Return True
        Catch ex As Exception
            LogFile("execReaderSQLCommand: " & SQLCommand, ex)

            If ex.Message.ToLower().Contains("deadlock") Then
                Status = DataAccessStatus.Deadlock
                DataTable = New DataTable
            Else
                DataTable = Nothing
            End If

            Return False
        Finally
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
        End Try
    End Function

    Public Function execReaderProcedure(ByVal Procedure As String, ByVal Parameters As IList(Of IDbDataParameter), ByVal DataSet As DataSet, Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean

        Dim trans As SqlTransaction = Nothing, cmd As SqlCommand = Nothing, conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            Using da As New SqlDataAdapter(Procedure, conn)
                createSqlProcedureCommand(da.SelectCommand, Procedure, Parameters, conn)
                da.Fill(DataSet)
            End Using

            Return True
        Catch ex As Exception

            Dim pText As String = ""

            If Not Parameters Is Nothing AndAlso Parameters.Count > 0 Then
                pText = " ==> "
                For Each P In Parameters.ToArray
                    pText &= P.ParameterName & " = " & P.Value & " | "
                Next
            End If

            LogFile("execReaderProcedure: " & Procedure, ex, pText)

            If ex.Message.ToLower().Contains("deadlock") Then
                Status = DataAccessStatus.Deadlock
                DataSet = New DataSet
            Else
                DataSet = Nothing
            End If

            Return False
        Finally
            If Not cmd Is Nothing AndAlso Not cmd.Parameters Is Nothing Then cmd.Parameters.Clear() : cmd.Dispose() : cmd = Nothing
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
        End Try
    End Function

    Public Function execReaderProcedure(ByVal Procedure As String, ByVal Parameters As IList(Of IDbDataParameter), ByVal DataTable As DataTable, Optional ByRef Status As DataAccessStatus = DataAccessStatus.OK) As Boolean

        Dim trans As SqlTransaction = Nothing, cmd As SqlCommand = Nothing, conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(Me.Connection.ConnectionString)

            Using da As New SqlDataAdapter(Procedure, conn)
                createSqlProcedureCommand(da.SelectCommand, Procedure, Parameters, conn)
                da.Fill(DataTable)
            End Using

            Return True
        Catch ex As Exception

            Dim pText As String = ""

            If Not Parameters Is Nothing AndAlso Parameters.Count > 0 Then
                pText = " ==> "
                For Each P In Parameters.ToArray
                    pText &= P.ParameterName & " = " & P.Value & " | "
                Next
            End If

            LogFile("execReaderProcedure: " & Procedure, ex, pText)

            If ex.Message.ToLower().Contains("deadlock") Then
                Status = DataAccessStatus.Deadlock
                DataTable = New DataTable
            Else
                DataTable = Nothing
            End If

            Return False
        Finally
            If Not cmd Is Nothing AndAlso Not cmd.Parameters Is Nothing Then cmd.Parameters.Clear() : cmd.Dispose() : cmd = Nothing
            If Not conn Is Nothing Then conn.Dispose() : conn = Nothing
        End Try
    End Function

    Public Function getConnectionString(ByVal IP As String, ByVal Catalog As String, ByVal UserID As String, ByVal Password As String) As String
        Return String.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Application Name={4};", IP, Catalog, UserID, Password, "SmartTrackWCF")
    End Function

    Private Sub LogFile(Proceeding As String, ex As Exception, Optional Text As String = "")
        Try

            If Not ex.Message.ToLower().Contains("deadlock") Then
                _Error = True

                RaiseEvent SendError(Proceeding, ex)
            End If

            If DataAccess.SaveLog AndAlso Not (String.IsNullOrEmpty(Me.SaveLogFile)) Then
                Dim path As String = PathLogFile & Me.SaveLogFile, sw As StreamWriter
                If File.Exists(path) = False Then
                    sw = File.CreateText(path)
                    sw.WriteLine(String.Format("{0} - {1}", Date.UtcNow.ToString(), Proceeding & " - " & ex.Message & Text))
                    sw.WriteLine("====================|")
                    sw.Flush()
                    sw.Close()
                Else
                    sw = File.AppendText(path)
                    sw.WriteLine(String.Format("{0} - {1}", Date.UtcNow.ToString(), Proceeding & " - " & ex.Message & Text))
                    sw.WriteLine("====================|")
                    sw.Flush()
                    sw.Close()
                End If
            End If
        Catch ez As Exception
        End Try
    End Sub


#Region "Dispose"
    Private handle As IntPtr, component As New Component, disposed As Boolean = False
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then component.Dispose()

            If Not Me.Connection Is Nothing Then
                Me.Connection.Dispose()
                Me.Connection = Nothing
            End If

            NativeMethods.CloseHandle(handle)
            handle = IntPtr.Zero
            disposed = True
        End If
    End Sub

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
#End Region

End Class