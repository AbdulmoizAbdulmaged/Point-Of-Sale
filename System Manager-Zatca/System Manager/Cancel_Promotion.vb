Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Public Class Cancel_Promotion
    Dim indicator As Integer
    Dim position As String
    Public u_id As String = ""
    Public pw As String = ""
    Dim ip As String
    Dim sdata As String
    Dim newXL As Excel.Application
    Dim newWB As Excel.Workbook
    Dim newWS As Excel.Worksheet
    Dim year As String
    Dim day As String
    Dim month As String
    Dim hour As String
    Dim minute As String
    Dim second As String
    Dim rr As String
    Dim oracle_sid As String
    Dim CF As Double
    Dim S As Double = 0
    Dim promo As New OracleCommand


    Private Sub Cancel_Promotion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            'Fill datagridview with users data
            Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim conn As New OracleConnection(oradb)
            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.CommandText = "select promotion_id  ""Promotion ID"",promotion_name ""Promotion Name"",created_by ""Created By"",creation_date ""Creation Date"" from promotions_id"
            Dim oraada As New OracleDataAdapter(cmd)
            Dim ds As New DataSet
            oraada.Fill(ds, "PA_EM")
            DataGridView1.DataSource = ds.Tables(0)



            '--------------------------
            'Dim oradb As String = "Data Source=" + ip + ":1521/orcl;User Id=" + u_id + ";password=" + pw + ";"
            'Dim conn As New OracleConnection(oradb)
            conn.Open()




            promo.Connection = conn

            promo.CommandText = "select promotion_id from promotions_id"

            Dim rd As OracleDataReader = promo.ExecuteReader
            While rd.Read

                ComboBox1.Items.Add(rd.GetValue(0).ToString)

            End While


            rd.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ' Dim u_id, pw, ip As String


            ' u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


            ' pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

            'ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")

            'Dim oradb As String = "Data Source=10.64.61.233:1521/orcl;User Id=07980;Password=8091;" 'example for connection string to monsoon aseer database
            Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim conn As New OracleConnection(oradb)
            Dim cmd_per As New OracleCommand
            cmd_per.Connection = conn
            cmd_per.CommandText = "delete from promo_per where promotion_id = '" + ComboBox1.Text + "'"
            conn.Open()
            Dim promo1 As Integer = cmd_per.ExecuteNonQuery()
            conn.Close()

            Dim cmd_oneprice As New OracleCommand
            cmd_oneprice.Connection = conn
            cmd_oneprice.CommandText = "delete from promo_oneprice where promotion_id = '" + ComboBox1.Text + "'"
            conn.Open()
            Dim promo2 As Integer = cmd_oneprice.ExecuteNonQuery()
            conn.Close()

            Dim cmd_bxgy As New OracleCommand
            cmd_bxgy.Connection = conn
            cmd_bxgy.CommandText = "delete from buy_x_get_y_discount where promotion_id = '" + ComboBox1.Text + "'"

            conn.Open()
            Dim promo3 As Integer = cmd_bxgy.ExecuteNonQuery()
            conn.Close()


            If promo1 Or promo2 Or promo3 Then

                    MsgBox("Promomtion has been removed successfully", MsgBoxStyle.Information)

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class