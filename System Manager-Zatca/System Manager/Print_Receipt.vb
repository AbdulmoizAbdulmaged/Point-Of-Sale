Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing
Imports Microsoft.Reporting.WinForms

Public Class Print_Receipt
    Public store_code As String
    Public company_name As String
    Public store_name As String
    Public u_id As String = ""
    Public pw As String = ""
    Dim ip As String
    Dim oracle_sid As String
    Private Sub Print_Receipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
                store_code = SC
                pw = DB_password

            End If
            Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim conn As New OracleConnection(oradb)



            Dim cmd1 As New OracleCommand
            cmd1.Connection = conn
            cmd1.CommandText = "select store_name,comp_name from pos_info"
            conn.Open()
            Dim rd1 As OracleDataReader = cmd1.ExecuteReader
            While rd1.Read
                store_name = rd1.GetValue(0).ToString
                company_name = rd1.GetValue(1).ToString
            End While
            rd1.Close()
            conn.Close()

            Dim paramters As IList(Of ReportParameter) = New List(Of ReportParameter)
            paramters.Add(New ReportParameter("txt1", Stock_Control.txt1.Text))
            paramters.Add(New ReportParameter("txt2", Stock_Control.txt2.Text))
            paramters.Add(New ReportParameter("dtp", Stock_Control.txtd.Text))
            paramters.Add(New ReportParameter("storecode", store_code))
            paramters.Add(New ReportParameter("storename", store_name))
            paramters.Add(New ReportParameter("companyname", company_name))
            ReportViewer1.LocalReport.SetParameters(paramters)
            Me.ReportViewer1.RefreshReport()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class