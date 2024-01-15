Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing
Public Class Info
    Public u_id As String = ""
    Public pw As String = ""
    Dim ip As String
    Dim oracle_sid As String
    Private Sub Info_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            '-------------------
            Dim readValue2 = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SID", "POSKeyValue", Nothing)

            'MessageBox.Show(readValue.ToString)
            If readValue2.ToString <> "" Then
                oracle_sid = readValue2.ToString

            Else
                MsgBox("Please update DB SID...", MsgBoxStyle.Information)
            End If
            '------------------------------------------

            Dim readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\DBHOST", "DBHOSTKeyValue", Nothing)

            'MessageBox.Show(readValue.ToString)
            If readValue.ToString <> "" Then
                ip = readValue.ToString

            Else
                MsgBox("Please update server host name...", MsgBoxStyle.Information)
            End If

            Dim readValue1 = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\POS", "POSKeyValue", Nothing)
            If readValue1.ToString <> "" Then
                Dim DQ As String = Chr(34)
                Dim SC As String
                Dim PSW As Double
                SC = readValue1.ToString

                PSW = Double.Parse(SC) + 111
                Dim DB_password As String = PSW.ToString

                u_id = SC
                pw = DB_password

            End If
            Dim conn As New OracleConnection


            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"

            conn.Open()

            Dim cmd2 As New OracleCommand

            cmd2.Connection = conn
            cmd2.CommandText = "select * from stock_history where RN=" + Stock_Control.txt1.Text + ""


            Dim rd2 As OracleDataReader = cmd2.ExecuteReader
            DataGridView1.Rows.Clear()

            While rd2.Read

                DataGridView1.Rows.Add(rd2.GetValue(1), rd2.GetValue(2), rd2.GetValue(3), rd2.GetValue(4), rd2.GetValue(5), rd2.GetValue(6), rd2.GetValue(7), rd2.GetValue(8))

            End While
            rd2.Close()

            conn.Close()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class