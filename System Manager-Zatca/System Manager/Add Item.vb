Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing
Imports System.IO
Imports Microsoft.Office.Interop

Public Class Add_Item
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

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim conn As New OracleConnection
        Try

            ProgressBar1.Visible = True
            ProgressBar1.Style = ProgressBarStyle.Continuous
            ProgressBar1.Visible = True
            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = DataGridView2.Rows.Count

            ProgressBar1.Value = 1
            'BackgroundWorker1.RunWorkerAsync()
            For i = 0 To DataGridView2.Rows.Count - 1
                'MessageBox.Show(DataGridView1.Rows(i).Cells(0).Value.ToString)
                '=======================================Save code


                ' Dim u_id, pw, ip As String


                'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


                'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

                'ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")
                Try
                    Double.TryParse(DataGridView2.Rows(i).Cells(3).Value, S)
                    S = S / CF


                    Dim id As Double
                    id = CO_ID()

                    'Dim oradb As String = "Data Source=10.64.61.233:1521/orcl;User Id=07980;Password=8091;" 'example for connection string to monsoon aseer database
                    Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                    conn.ConnectionString = oradb

                    conn.Open()



                    Dim cmd As New OracleCommand
                    Dim cmd1 As New OracleCommand
                    Dim cmd2 As New OracleCommand
                    Dim cmd3 As New OracleCommand
                    Dim cmd33 As New OracleCommand
                    Dim cmd4 As New OracleCommand
                    Dim cmd5 As New OracleCommand
                    Dim cmd6 As New OracleCommand
                    Dim cmd7 As New OracleCommand
                    Dim cmd8 As New OracleCommand
                    Dim cmd9 As New OracleCommand

                    ' Dim ping As New System.Net.NetworkInformation.Ping
                    ' Dim pingreply As System.Net.NetworkInformation.PingReply
                    cmd.Connection = conn
                    cmd.CommandText = "insert into as_itm values(:param1,'','1','1','','0','1601','1','1','','',:param2,'28510553_Pearlescent Mermaid:XA53_CHAMPA','STCK','0','0','',-1,'','','','','UNDF','',0,'','','','','','','','','','','','','5:160112865008',0,sysdate,sysdate)"
                    cmd.Parameters.Add(":param1", OracleDbType.Varchar2).Value = DataGridView2.Rows(i).Cells(0).Value.ToString
                    cmd.Parameters.Add(":param2", OracleDbType.NVarchar2).Value = DataGridView2.Rows(i).Cells(1).Value.ToString
                    cmd1.Connection = conn
                    cmd1.CommandText = "insert into AS_ITM_STK values(:param1,'UN','MONC138394','MONS56800','','0',0,'339','','0','','',0,'',0,'',0,0,'','',0,'','','',0,'01-JAN-70','',0,'01-JAN-70','','','','','','','','','0',0,'',0,0,0,'01-JAN-70','','','','','','','',0,0,0,0,'01-JAN-70','','','','0',sysdate,sysdate)"
                    cmd1.Parameters.Add(":param1", OracleDbType.Varchar2).Value = DataGridView2.Rows(i).Cells(0).Value.ToString

                    cmd2.Connection = conn
                    cmd2.CommandText = "insert into CO_BRK_SPR_ITM_BS values('339',:param1,'SLU',0,0,'17-JAN-17 02.48.23.000000000 AM','17-JAN-17 02.48.23.000000000 AM')"
                    cmd2.Parameters.Add(":param1", OracleDbType.Varchar2).Value = DataGridView2.Rows(i).Cells(0).Value.ToString

                    cmd3.Connection = conn
                    cmd3.CommandText = "insert into  ID_IDN_PS values('" + u_id + "',:param1,:param2,'',0,'',0,'',0,'',0,'',1,-1,'','','1','0','','0','0','0','','0','0','','','','','0',0,'','','1',0,0,0,'17-JAN-17 02.48.23.000000000 AM','17-JAN-17 02.48.23.000000000 AM')"

                    cmd3.Parameters.Add(":param1", OracleDbType.Varchar2).Value = DataGridView2.Rows(i).Cells(0).Value.ToString
                    cmd3.Parameters.Add(":param2", OracleDbType.Varchar2).Value = DataGridView2.Rows(i).Cells(0).Value.ToString


                    ' cmd33.Connection = conn
                    'cmd33.CommandText = "insert into  ID_IDN_PS values('10000','5052155571114','5052155571114','',0,'',0,'',0,'',0,'',1,-1,'','','1','0','','0','0','0','','0','0','','','','','0',0,'','','1',0,0,0,'17-JAN-17 02.48.23.000000000 AM','17-JAN-17 02.48.23.000000000 AM')"
                    'cmd33.Parameters.Add(":param1", OracleDbType.Varchar2).Value = "100100"
                    'cmd33.Parameters.Add(":param2", OracleDbType.Varchar2).Value = "100100"

                    cmd4.Connection = conn
                    cmd4.CommandText = "insert into  MA_ITM_PRN_PRC_ITM values(:param2,'" + u_id + "',:param1,'','','17-JAN-17 02.48.22.000000000 AM','17-JAN-17 02.48.22.000000000 AM')"
                    cmd4.Parameters.Add(":param2", OracleDbType.Double).Value = id
                    cmd4.Parameters.Add(":param1", OracleDbType.Varchar2).Value = DataGridView2.Rows(i).Cells(0).Value.ToString

                    cmd5.Connection = conn
                    cmd5.CommandText = "insert into MA_PRC_ITM values(:param1,'" + u_id + "','PPC',0,'','','17-JAN-17 02.48.22.000000000 AM','17-JAN-17 02.48.22.000000000 AM')"
                    cmd5.Parameters.Add(":param1", OracleDbType.Double).Value = id

                    cmd6.Connection = conn
                    cmd6.CommandText = "insert into TR_CHN_PRN_PRC values(:param1,'" + u_id + "',:param2,'5','17-JAN-17 02.48.22.000000000 AM','17-JAN-17 02.48.22.000000000 AM')"
                    cmd6.Parameters.Add(":param1", OracleDbType.Int32).Value = id
                    cmd6.Parameters.Add(":param2", OracleDbType.Double).Value = S

                    cmd7.Connection = conn
                    cmd7.CommandText = "insert into co_ev values(:param1,'" + u_id + "','177684219 initial price','','PPC','','','','','','','','17-JAN-17 02.48.22.000000000 AM','','','','','17-JAN-17 02.48.22.000000000 AM','17-JAN-17 02.48.22.000000000 AM')"
                    cmd7.Parameters.Add(":param1", OracleDbType.Double).Value = id

                    cmd8.Connection = conn
                    cmd8.CommandText = "insert into  co_ev_mnt values(:param1,'" + u_id + "','177684219 initial price','','17-JAN-17 12.00.00.000000000 AM','','PPC','','','','','','','','','','','','17-JAN-17 02.48.22.000000000 AM','17-JAN-17 02.48.22.000000000 AM')"
                    cmd8.Parameters.Add(":param1", OracleDbType.Double).Value = id

                    cmd9.Connection = conn
                    cmd9.CommandText = "insert into  AS_ITM_RTL_STR values('" + u_id + "',:param1,'','01-JAN-70','','0','01-JAN-70',0,0,'01-JAN-70','01-JAN-70','','','','01-JAN-70',0,'','','',0,'','18-JAN-17 07.27.03.000000000 PM','18-JAN-17 07.27.03.000000000 PM')"
                    cmd9.Parameters.Add(":param1", OracleDbType.Varchar2).Value = DataGridView2.Rows(i).Cells(0).Value.ToString
                    'If (pingreply.Status = System.Net.NetworkInformation.IPStatus.Success) Then
                    Dim ar As Integer = cmd.ExecuteNonQuery()
                    Dim ar1 As Integer = cmd1.ExecuteNonQuery()
                    Dim ar2 As Integer = cmd2.ExecuteNonQuery()
                    Dim ar3 As Integer = cmd3.ExecuteNonQuery()
                    'Dim ar33 As Integer = cmd33.ExecuteNonQuery
                    Dim ar4 As Integer = cmd4.ExecuteNonQuery
                    Dim ar5 As Integer = cmd5.ExecuteNonQuery
                    Dim ar6 As Integer = cmd6.ExecuteNonQuery
                    Dim ar7 As Integer = cmd7.ExecuteNonQuery
                    Dim ar8 As Integer = cmd8.ExecuteNonQuery
                    Dim ar9 As Integer = cmd9.ExecuteNonQuery
                    'conn.Close()

                    If ar > 0 Then
                        ar = ar + ar1 + ar2 + ar3 + ar4 + ar5 + ar6 + ar7 + ar8 + ar9

                        If ar = 10 Then
                            DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.Green




                            'conn.Open()



                            Dim cmd10 As New OracleCommand
                            cmd10.Connection = conn
                            cmd10.CommandText = "select user_name from log_in"

                            Dim rd As OracleDataReader = cmd10.ExecuteReader
                            Dim user_name As String = ""

                            While rd.Read
                                user_name = rd.GetValue(0)

                            End While
                            rd.Close()
                            'conn.Close()

                            Dim cmd11 As New OracleCommand
                            cmd11.Connection = conn
                            cmd11.CommandText = "insert into prices_history(barcode,description,sell_price,entry_time,created_by,operation_type) values('" + DataGridView2.Rows(i).Cells(0).Value.ToString + "','" + DataGridView2.Rows(i).Cells(1).Value.ToString + "','" + DataGridView2.Rows(i).Cells(3).Value.ToString + "',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh24:mi:ss'),'" + user_name + "','Add new item')"
                            'conn.Open()
                            cmd11.ExecuteNonQuery()
                            'conn.Close()
                        End If
                        '-----------------------------------------------------Validity
                        Dim cmd_val As New OracleCommand
                        cmd_val.Connection = conn
                        cmd_val.CommandText = "insert into validity(Barcode,Validity_Date) values('" + DataGridView2.Rows(i).Cells(0).Value.ToString + "','" + DataGridView2.Rows(i).Cells(2).Value.ToString + "')"
                        'conn.Open()
                        cmd_val.ExecuteNonQuery()
                        'conn.Close()
                        '--------------------------------------original prices
                        Dim cmd_org As New OracleCommand
                        cmd_org.Connection = conn
                        cmd_org.CommandText = "insert into original_prices(Barcode,I_D,S_P) values('" + DataGridView2.Rows(i).Cells(0).Value.ToString + "','" + DataGridView2.Rows(i).Cells(1).Value.ToString + "','" + S.ToString + "')"
                        'conn.Open()
                        cmd_org.ExecuteNonQuery()
                        conn.Close()


                    Else



                    End If

                Catch ex As Exception
                    conn.Close()
                    MsgBox("Row: " & (i + 1).ToString & " : " & ex.Message)

                    Exit For
                End Try






                TextBox1.Clear()


                TextBox1.Focus()
                If ProgressBar1.Value < ProgressBar1.Maximum Then
                    ProgressBar1.Value = ProgressBar1.Value + 1
                End If





                Label6.Text = (i + 1).ToString & " Rows imported"


            Next
            'ProgressBar1.Visible = False
            conn.Close()

        Catch ex As Exception
            conn.Close()
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try


            Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application()

            If xlApp Is Nothing Then
                MsgBox("Excel is not properly installed!!", MsgBoxStyle.Information)
                Return
            End If




            year = Date.Today.Year.ToString
            day = Date.Today.Day.ToString
            month = Date.Today.Month.ToString
            hour = Date.Now.Hour.ToString
            minute = Date.Now.Minute.ToString
            second = Date.Now.Second.ToString

            rr = year + month + day + hour + minute + second



            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("sheet1")
            xlWorkSheet.Cells(1, 1) = "Barcode"
            xlWorkSheet.Cells(1, 2) = "Description"
            xlWorkSheet.Cells(1, 3) = "Validity"
            xlWorkSheet.Cells(1, 4) = "Price"
            xlWorkBook.SaveAs("c:\POS\Prices\PRICES_LIST" & rr & ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
             Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            'xlWorkBook = xlWorkBook.Open("c:\receipts\csharp-Excel.xls")

            xlWorkBook.Close(True, misValue, misValue)
            xlApp.Quit()

            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)

            'MessageBox.Show("Excel file created , you can find the file c:\receipts\csharp-Excel.xls")
            '

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally


            Dim excelFileName As String = "c:\POS\Prices\PRICES_LIST" & rr & ".xls"
            newXL = New Microsoft.Office.Interop.Excel.Application
            newXL.Visible = True
            newWB = newXL.Workbooks.Open(excelFileName)
            newWS = newWB.Worksheets(1)


        End Try
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try

    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            DataGridView1.Visible = False
            DataGridView2.Visible = True

            DataGridView1.Rows.Clear()

            'opens the Excel file
            OpenFileDialog1.Title = ""
            OpenFileDialog1.InitialDirectory = "C:\POS\prices"
            'OpenFileDialog1.Filter = "Excel files (*.xls)"
            OpenFileDialog1.FileName = ""
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim excelFileName As String = OpenFileDialog1.FileName.ToString

                '
                'MsgBox(excelFileName)
                Dim MyConnection As System.Data.OleDb.OleDbConnection
                Dim DtSet As System.Data.DataSet
                Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
                MyConnection = New System.Data.OleDb.OleDbConnection _
                ("provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + excelFileName + "';  Extended Properties=Excel 8.0;")
                MyCommand = New System.Data.OleDb.OleDbDataAdapter _
                    ("select * from [Sheet1$]", MyConnection)
                MyCommand.TableMappings.Add("Table", "TestTable")
                DtSet = New System.Data.DataSet
                MyCommand.Fill(DtSet)
                'DataGridView2.Visible = True
                'DataGridView1.Visible = False
                DataGridView2.DataSource = DtSet.Tables(0)


                MyConnection.Close()


            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Add_Item_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Label6.Text = ""
            Dim readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\DBHOST", "DBHOSTKeyValue", Nothing)
            '-------------
            '-------------------
            Dim readValue2 = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SID", "POSKeyValue", Nothing)

            'MessageBox.Show(readValue.ToString)
            If readValue2.ToString <> "" Then
                oracle_sid = readValue2.ToString

            Else
                MsgBox("Please update DB SID...", MsgBoxStyle.Information)
            End If
            '------------------------------------------
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
            ''''''''''''''''''''''''

            Dim conn As New OracleConnection
            Dim cmd As New OracleCommand
            Dim cmd1 As New OracleCommand


            cmd.Connection = conn
            cmd.CommandText = "select * from pos_info"


            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"


            conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader
            If rd.Read Then

                CF = rd.GetValue(7)

            End If
            rd.Close()
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TextBox1.Text = TextBox1.Text.Trim
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            DataGridView1.Visible = True
            DataGridView2.Visible = False
            DataGridView1.Rows.Clear()


            indicator = 0

            ' Dim u_id, pw, ip As String


            'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


            'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

            'ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")
            Dim conn As New OracleConnection
            Dim cmd As New OracleCommand
            Dim cmd1 As New OracleCommand
            Dim cmd_pbw As New OracleCommand
            Dim id As Double = get_id()

            Label6.Text = ""
            TextBox2.Focus()


            cmd.Connection = conn
            cmd.CommandText = "select de_itm from as_itm where id_itm = '" + TextBox1.Text.Trim + "'"
            cmd1.Connection = conn
            cmd1.CommandText = "select mo_chn_prn_un_prc from TR_CHN_PRN_PRC where id_ev = :param1"
            cmd1.Parameters.Add(":param1", OracleDbType.Double).Value = id
            cmd_pbw.Connection = conn
            cmd_pbw.CommandText = "select ITM_NAME,PRICE_UNIT FROM PRICEBASEDONWEIGHT WHERE ITM_ID = '" + TextBox1.Text.Trim + "'"
            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"



            Try
                conn.Open()
                Dim rd As OracleDataReader = cmd.ExecuteReader
                If rd.Read Then
                    TextBox2.Text = rd.GetValue(0).ToString
                    TextBox1.Enabled = False
                    CheckBox1.Checked = False
                    CheckBox1.Enabled = False
                    indicator = 1

                End If
                Dim rd_pbw As OracleDataReader = cmd_pbw.ExecuteReader
                If rd_pbw.Read Then
                    TextBox2.Text = rd_pbw.GetValue(0).ToString
                    TextBox3.Text = rd_pbw.GetValue(1) * CF
                    TextBox1.Enabled = False
                    CheckBox1.Checked = True
                    CheckBox1.Enabled = False
                    indicator = 1
                End If
                Dim rd1 As OracleDataReader = cmd1.ExecuteReader
                If rd1.Read Then
                    TextBox3.Text = rd1.GetValue(0) * CF

                End If
                conn.Close()
                '-------------------------------------------------------get validity
                conn.Open()
                Dim cmd_val As New OracleCommand
                cmd_val.Connection = conn
                cmd_val.CommandText = "select validity_date from validity where barcode = '" + TextBox1.Text + "'"
                Dim rd_val As OracleDataReader = cmd_val.ExecuteReader
                While rd_val.Read
                    If rd_val.GetValue(0).ToString <> "" Then
                        DateTimePicker1.Value = rd_val.GetValue(0).ToString
                    End If

                    DataGridView2.Visible = False
                    DataGridView1.Visible = True

                End While
                rd_val.Close()
                conn.Close()

                '-------------------------------------------------------prices history

                conn.Open()

                Dim cmd2 As New OracleCommand

                cmd2.Connection = conn
                cmd2.CommandText = "select * from prices_history where barcode= '" + TextBox1.Text + "'"

                '  Dim adapter As New OracleDataAdapter(cmd)
                ' Dim ds As New DataSet
                'adapter.Fill(ds, "stock")
                'DataGridView1.DataSource = ds.Tables(0)
                Dim rd2 As OracleDataReader = cmd2.ExecuteReader
                DataGridView1.Rows.Clear()

                While rd2.Read

                    DataGridView1.Rows.Add(rd2.GetValue(0).ToString, rd2.GetValue(1).ToString, rd2.GetValue(2).ToString, rd2.GetValue(3).ToString, rd2.GetValue(4).ToString, rd2.GetValue(5).ToString)

                End While
                rd2.Close()

                conn.Close()



            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            'If TextBox1.Text.Length > 14 Then

            'TextBox1.Clear()
            ' TextBox1.Focus()
            'Label6.ForeColor = Color.Red

            ' Label6.Text = "Please scan 13 digit barcode.."


            'End If



        End If
    End Sub
    Public Function get_id() As Double
        Dim conn As New OracleConnection
        Dim cmd_getid As New OracleCommand
        Dim id As Double
        'Dim u_id, pw, ip As String


        'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


        'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

        ' ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")

        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
        cmd_getid.CommandText = "select id_ev from MA_ITM_PRN_PRC_ITM where id_itm='" + TextBox1.Text.Trim + "'"
        cmd_getid.Connection = conn

        Try
            conn.Open()
            Dim rd As OracleDataReader = cmd_getid.ExecuteReader
            If rd.Read Then

                id = rd.GetValue(0)

            End If
            rd.Close()
            conn.Close()
            Return id

        Catch ex As Exception
            'MessageBox.Show(ex.Message)

        End Try
        'select id_ev from MA_ITM_PRN_PRC_ITM where id_itm='8904150313909'
    End Function

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        Dim txt As TextBox = CType(sender, TextBox)

        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True

        If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

        If e.KeyChar = "." And txt.Text.IndexOf(".") = -1 Then e.Handled = False 'allow single decimal point

        If e.KeyChar = "-" And txt.SelectionStart = 0 Then e.Handled = False 'allow negative number

        If e.KeyChar = Convert.ToChar(13) Then

            e.Handled = True
            DataGridView1.Visible = True
            DataGridView2.Visible = False
            Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim conn As New OracleConnection(oradb)
            Try

                If TextBox1.Text <> "" Then

                    If TextBox2.Text <> "" Then
                        If TextBox3.Text <> "" Then
                            Double.TryParse(TextBox3.Text, S)
                            S = S / CF

                            If indicator = 0 Then
                                '=======================================Save code
                                If CheckBox1.Checked = False Then '==============NOT PBW
                                    Try
                                        ' Dim u_id, pw, ip As String


                                        'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


                                        'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

                                        'ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")
                                        Dim id As Double
                                        id = CO_ID()

                                        'Dim oradb As String = "Data Source=10.64.61.233:1521/orcl;User Id=07980;Password=8091;" 'example for connection string to monsoon aseer database


                                        conn.Open()



                                        Dim cmd As New OracleCommand
                                        Dim cmd1 As New OracleCommand
                                        Dim cmd2 As New OracleCommand
                                        Dim cmd3 As New OracleCommand
                                        Dim cmd33 As New OracleCommand
                                        Dim cmd4 As New OracleCommand
                                        Dim cmd5 As New OracleCommand
                                        Dim cmd6 As New OracleCommand
                                        Dim cmd7 As New OracleCommand
                                        Dim cmd8 As New OracleCommand
                                        Dim cmd9 As New OracleCommand


                                        ' Dim ping As New System.Net.NetworkInformation.Ping
                                        ' Dim pingreply As System.Net.NetworkInformation.PingReply
                                        cmd.Connection = conn
                                        cmd.CommandText = "insert into as_itm values(:param1,'','1','1','','0','1601','1','1','','',:param2,'28510553_Pearlescent Mermaid:XA53_CHAMPA','STCK','0','0','',-1,'','','','','UNDF','',0,'','','','','','','','','','','','','5:160112865008',0,sysdate,sysdate)"
                                        cmd.Parameters.Add(":param1", OracleDbType.Varchar2).Value = TextBox1.Text.Trim
                                        cmd.Parameters.Add(":param2", OracleDbType.NVarchar2).Value = TextBox2.Text
                                        cmd1.Connection = conn
                                        cmd1.CommandText = "insert into AS_ITM_STK values(:param1,'UN','MONC138394','MONS56800','','0',0,'339','','0','','',0,'',0,'',0,0,'','',0,'','','',0,'01-JAN-70','',0,'01-JAN-70','','','','','','','','','0',0,'',0,0,0,'01-JAN-70','','','','','','','',0,0,0,0,'01-JAN-70','','','','0',sysdate,sysdate)"
                                        cmd1.Parameters.Add(":param1", OracleDbType.Varchar2).Value = TextBox1.Text

                                        cmd2.Connection = conn
                                        cmd2.CommandText = "insert into CO_BRK_SPR_ITM_BS values('339',:param1,'SLU',0,0,'17-JAN-17 02.48.23.000000000 AM','17-JAN-17 02.48.23.000000000 AM')"
                                        cmd2.Parameters.Add(":param1", OracleDbType.Varchar2).Value = TextBox1.Text

                                        cmd3.Connection = conn
                                        cmd3.CommandText = "insert into  ID_IDN_PS values('" + u_id + "',:param1,:param2,'',0,'',0,'',0,'',0,'',1,-1,'','','1','0','','0','0','0','','0','0','','','','','0',0,'','','1',0,0,0,'17-JAN-17 02.48.23.000000000 AM','17-JAN-17 02.48.23.000000000 AM')"

                                        cmd3.Parameters.Add(":param1", OracleDbType.Varchar2).Value = TextBox1.Text
                                        cmd3.Parameters.Add(":param2", OracleDbType.Varchar2).Value = TextBox1.Text


                                        ' cmd33.Connection = conn
                                        'cmd33.CommandText = "insert into  ID_IDN_PS values('10000','5052155571114','5052155571114','',0,'',0,'',0,'',0,'',1,-1,'','','1','0','','0','0','0','','0','0','','','','','0',0,'','','1',0,0,0,'17-JAN-17 02.48.23.000000000 AM','17-JAN-17 02.48.23.000000000 AM')"
                                        'cmd33.Parameters.Add(":param1", OracleDbType.Varchar2).Value = "100100"
                                        'cmd33.Parameters.Add(":param2", OracleDbType.Varchar2).Value = "100100"

                                        cmd4.Connection = conn
                                        cmd4.CommandText = "insert into  MA_ITM_PRN_PRC_ITM values(:param2,'" + u_id + "',:param1,'','','17-JAN-17 02.48.22.000000000 AM','17-JAN-17 02.48.22.000000000 AM')"
                                        cmd4.Parameters.Add(":param2", OracleDbType.Double).Value = id
                                        cmd4.Parameters.Add(":param1", OracleDbType.Varchar2).Value = TextBox1.Text

                                        cmd5.Connection = conn
                                        cmd5.CommandText = "insert into MA_PRC_ITM values(:param1,'" + u_id + "','PPC',0,'','','17-JAN-17 02.48.22.000000000 AM','17-JAN-17 02.48.22.000000000 AM')"
                                        cmd5.Parameters.Add(":param1", OracleDbType.Double).Value = id

                                        cmd6.Connection = conn
                                        cmd6.CommandText = "insert into TR_CHN_PRN_PRC values(:param1,'" + u_id + "',:param2,'5','17-JAN-17 02.48.22.000000000 AM','17-JAN-17 02.48.22.000000000 AM')"
                                        cmd6.Parameters.Add(":param1", OracleDbType.Int32).Value = id
                                        cmd6.Parameters.Add(":param2", OracleDbType.Double).Value = S

                                        cmd7.Connection = conn
                                        cmd7.CommandText = "insert into co_ev values(:param1,'" + u_id + "','177684219 initial price','','PPC','','','','','','','','17-JAN-17 02.48.22.000000000 AM','','','','','17-JAN-17 02.48.22.000000000 AM','17-JAN-17 02.48.22.000000000 AM')"
                                        cmd7.Parameters.Add(":param1", OracleDbType.Double).Value = id

                                        cmd8.Connection = conn
                                        cmd8.CommandText = "insert into  co_ev_mnt values(:param1,'" + u_id + "','177684219 initial price','','17-JAN-17 12.00.00.000000000 AM','','PPC','','','','','','','','','','','','17-JAN-17 02.48.22.000000000 AM','17-JAN-17 02.48.22.000000000 AM')"
                                        cmd8.Parameters.Add(":param1", OracleDbType.Double).Value = id

                                        cmd9.Connection = conn
                                        cmd9.CommandText = "insert into  AS_ITM_RTL_STR values('" + u_id + "',:param1,'','01-JAN-70','','0','01-JAN-70',0,0,'01-JAN-70','01-JAN-70','','','','01-JAN-70',0,'','','',0,'','18-JAN-17 07.27.03.000000000 PM','18-JAN-17 07.27.03.000000000 PM')"
                                        cmd9.Parameters.Add(":param1", OracleDbType.Varchar2).Value = TextBox1.Text
                                        'If (pingreply.Status = System.Net.NetworkInformation.IPStatus.Success) Then
                                        Dim ar As Integer = cmd.ExecuteNonQuery()
                                        Dim ar1 As Integer = cmd1.ExecuteNonQuery()
                                        Dim ar2 As Integer = cmd2.ExecuteNonQuery()
                                        Dim ar3 As Integer = cmd3.ExecuteNonQuery()
                                        'Dim ar33 As Integer = cmd33.ExecuteNonQuery
                                        Dim ar4 As Integer = cmd4.ExecuteNonQuery
                                        Dim ar5 As Integer = cmd5.ExecuteNonQuery
                                        Dim ar6 As Integer = cmd6.ExecuteNonQuery
                                        Dim ar7 As Integer = cmd7.ExecuteNonQuery
                                        Dim ar8 As Integer = cmd8.ExecuteNonQuery
                                        Dim ar9 As Integer = cmd9.ExecuteNonQuery
                                        conn.Close()

                                        If ar > 0 Then
                                            ar = ar + ar1 + ar2 + ar3 + ar4 + ar5 + ar6 + ar7 + ar8 + ar9

                                            If ar = 10 Then
                                                Label6.ForeColor = Color.Green
                                                Label6.Text = "Added Successfully..."

                                                conn.Open()



                                                Dim cmd10 As New OracleCommand
                                                cmd10.Connection = conn
                                                cmd10.CommandText = "select user_name from log_in"

                                                Dim rd As OracleDataReader = cmd10.ExecuteReader
                                                Dim user_name As String = ""

                                                While rd.Read
                                                    user_name = rd.GetValue(0)

                                                End While
                                                rd.Close()
                                                conn.Close()

                                                Dim cmd11 As New OracleCommand
                                                cmd11.Connection = conn
                                                cmd11.CommandText = "insert into prices_history(barcode,description,sell_price,entry_time,created_by,operation_type) values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh24:mi:ss'),'" + user_name + "','Add new item')"
                                                conn.Open()
                                                cmd11.ExecuteNonQuery()
                                                conn.Close()
                                            End If
                                            '-----------------------------------------------------Validity
                                            If CheckBox2.Checked = True Then
                                                Dim cmd_val As New OracleCommand
                                                cmd_val.Connection = conn
                                                cmd_val.CommandText = "insert into validity(Barcode,Validity_Date) values('" + TextBox1.Text + "','" + DateTimePicker1.Value + "')"
                                                conn.Open()
                                                cmd_val.ExecuteNonQuery()
                                                conn.Close()
                                            End If
                                            '--------------------------------------original prices
                                            Dim cmd_org As New OracleCommand
                                            cmd_org.Connection = conn
                                            cmd_org.CommandText = "insert into original_prices(Barcode,I_D,S_P) values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + S.ToString + "')"
                                            conn.Open()
                                            cmd_org.ExecuteNonQuery()
                                            conn.Close()


                                        Else



                                        End If








                                        'TextBox1.Clear()
                                        TextBox2.Clear()
                                        TextBox3.Clear()
                                        DateTimePicker1.Update()

                                        TextBox1.Focus()

                                    Catch ex As Exception
                                        'MessageBox.Show(ex.Message)
                                    End Try
                                    '==============================================================


                                End If
                                If CheckBox1.Checked = True Then '============Add PBW ITEM
                                    If TextBox1.Text.Trim.Length = 7 Then


                                        Try
                                            Dim cmd_pbw As New OracleCommand
                                            cmd_pbw.Connection = conn
                                            cmd_pbw.CommandText = "insert into pricebasedonweight values('" + TextBox1.Text + "','" + TextBox2.Text + "','Gram','" + S.ToString + "')"
                                            conn.Open()
                                            cmd_pbw.ExecuteNonQuery()




                                            Dim cmd10 As New OracleCommand
                                            cmd10.Connection = conn
                                            cmd10.CommandText = "select user_name from log_in"

                                            Dim rd As OracleDataReader = cmd10.ExecuteReader
                                            Dim user_name As String = ""

                                            While rd.Read
                                                user_name = rd.GetValue(0)

                                            End While
                                            rd.Close()
                                            conn.Close()

                                            Dim cmd11 As New OracleCommand
                                            cmd11.Connection = conn
                                            cmd11.CommandText = "insert into prices_history(barcode,description,sell_price,entry_time,created_by,operation_type) values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh24:mi:ss'),'" + user_name + "','Add new item')"
                                            conn.Open()
                                            cmd11.ExecuteNonQuery()
                                            conn.Close()

                                            '-----------------------------------------------------Validity
                                            If CheckBox2.Checked = True Then
                                                Dim cmd_val As New OracleCommand
                                                cmd_val.Connection = conn
                                                cmd_val.CommandText = "insert into validity(Barcode,Validity_Date) values('" + TextBox1.Text + "','" + DateTimePicker1.Value + "')"
                                                conn.Open()
                                                cmd_val.ExecuteNonQuery()
                                                conn.Close()
                                            End If
                                            '--------------------------------------original prices
                                            Dim cmd_org As New OracleCommand
                                            cmd_org.Connection = conn
                                            cmd_org.CommandText = "insert into original_prices(Barcode,I_D,S_P) values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + S.ToString + "')"
                                            conn.Open()
                                            cmd_org.ExecuteNonQuery()
                                            conn.Close()
                                            'TextBox1.Clear()
                                            TextBox2.Clear()
                                            TextBox3.Clear()
                                            DateTimePicker1.Update()
                                            TextBox1.Focus()
                                            CheckBox1.Checked = False
                                            Label6.ForeColor = Color.Green
                                            Label6.Text = "Added Successfully..."
                                        Catch ex As Exception
                                            MessageBox.Show(ex.Message)
                                        End Try
                                    Else
                                        TextBox1.Focus()
                                        Label6.ForeColor = Color.Red
                                        Label6.Text = "Item ID must be 7 digits for PBW."
                                    End If



                                End If
                            End If

                            If indicator = 1 Then
                                If CheckBox1.Checked = False Then '============NOT PBW ITEM


                                    '======================================update code
                                    Try


                                        'Dim u_id, pw, ip As String


                                        ' u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


                                        'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

                                        'ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")

                                        Dim id As Double = get_id()

                                        'Dim oradb As String = "Data Source=10.64.61.233:1521/orcl;User Id=07980;Password=8091;" 'example for connection string to monsoon aseer database
                                        ' Dim oradb As String = "Data Source=" + ip + ":1521/orcl;User Id=" + u_id + ";password=" + pw + ";"
                                        'Dim conn As New OracleConnection(oradb)

                                        conn.Open()



                                        Dim cmd As New OracleCommand

                                        Dim cmd6 As New OracleCommand


                                        ' Dim ping As New System.Net.NetworkInformation.Ping
                                        ' Dim pingreply As System.Net.NetworkInformation.PingReply
                                        cmd.Connection = conn
                                        cmd.CommandText = "update as_itm set de_itm = '" + TextBox2.Text + "' where id_itm = '" + TextBox1.Text.Trim + "'"
                                        ' cmd.Parameters.Add(":param1", OracleDbType.Varchar2).Value = TextBox1.Text.Trim
                                        'cmd.Parameters.Add(":param2", OracleDbType.NVarchar2).Value = TextBox2.Text








                                        cmd6.Connection = conn
                                        cmd6.CommandText = "update TR_CHN_PRN_PRC set mo_chn_prn_un_prc = :param2 where id_ev = :param1"
                                        cmd6.Parameters.Add(":param2", OracleDbType.Double).Value = S.ToString
                                        cmd6.Parameters.Add(":param1", OracleDbType.Double).Value = id



                                        'If (pingreply.Status = System.Net.NetworkInformation.IPStatus.Success) Then
                                        Dim ar As Integer = cmd.ExecuteNonQuery()

                                        Dim ar6 As Integer = cmd6.ExecuteNonQuery



                                        If ar > 0 Then
                                            ar = ar + ar6
                                            If ar = 2 Then
                                                Label6.ForeColor = Color.Green
                                                Label6.Text = "Updated Successfully..."
                                                '-------------------------------Validity
                                                If CheckBox2.Checked = True Then
                                                    Dim cmd_val As New OracleCommand
                                                    cmd_val.Connection = conn
                                                    cmd_val.CommandText = "update validity set Validity_Date = '" + DateTimePicker1.Value + "' where Barcode = '" + TextBox1.Text + "'"

                                                    cmd_val.ExecuteNonQuery()
                                                End If
                                                '-------------------------------------------login
                                                Dim cmd10 As New OracleCommand
                                                cmd10.Connection = conn
                                                cmd10.CommandText = "select user_name from log_in"

                                                Dim rd As OracleDataReader = cmd10.ExecuteReader
                                                Dim user_name As String = ""

                                                While rd.Read
                                                    user_name = rd.GetValue(0)

                                                End While
                                                rd.Close()

                                                '--------------------------------------------------------------------------Prices history
                                                Dim cmd11 As New OracleCommand
                                                cmd11.Connection = conn
                                                cmd11.CommandText = "insert into prices_history(barcode,description,sell_price,entry_time,created_by,operation_type) values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh24:mi:ss'),'" + user_name + "','Update existed item')"

                                                cmd11.ExecuteNonQuery()

                                                '---------------------------------------------------update original price

                                                Dim upd_org As New OracleCommand
                                                upd_org.Connection = conn
                                                upd_org.CommandText = "update original_prices set I_D='" + TextBox2.Text + "',S_P='" + S.ToString + "' where Barcode='" + TextBox1.Text + "'"
                                                upd_org.ExecuteNonQuery()



                                            End If

                                        Else

                                        End If








                                        conn.Close()

                                        'TextBox1.Clear()
                                        TextBox2.Clear()
                                        TextBox3.Clear()
                                        DateTimePicker1.Update()
                                        CheckBox1.Enabled = True


                                        TextBox1.Focus()
                                        TextBox1.Enabled = True

                                    Catch ex As Exception
                                        MessageBox.Show(ex.Message)
                                    End Try

                                    '====================================== 
                                End If
                                If CheckBox1.Checked = True Then '============UPDATE PBW ITEM
                                    Try
                                        Dim cmd_pbw As New OracleCommand
                                        cmd_pbw.Connection = conn
                                        cmd_pbw.CommandText = "update pricebasedonweight set ITM_NAME = '" + TextBox2.Text + "', UNIT = 'KG', PRICE_UNIT = '" + S.ToString + "' where itm_id = '" + TextBox1.Text + "' "
                                        conn.Open()
                                        cmd_pbw.ExecuteNonQuery()
                                        conn.Close()
                                        Label6.ForeColor = Color.Green
                                        Label6.Text = "Updated Successfully..."

                                        conn.Open()



                                        Dim cmd10 As New OracleCommand
                                        cmd10.Connection = conn
                                        cmd10.CommandText = "select user_name from log_in"

                                        Dim rd As OracleDataReader = cmd10.ExecuteReader
                                        Dim user_name As String = ""

                                        While rd.Read
                                            user_name = rd.GetValue(0)

                                        End While
                                        rd.Close()
                                        conn.Close()

                                        Dim cmd11 As New OracleCommand
                                        cmd11.Connection = conn
                                        cmd11.CommandText = "insert into prices_history(barcode,description,sell_price,entry_time,created_by,operation_type) values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh24:mi:ss'),'" + user_name + "','Update existed item')"
                                        conn.Open()
                                        cmd11.ExecuteNonQuery()
                                        conn.Close()

                                        '-----------------------------------------------------Validity
                                        If CheckBox2.Checked = True Then
                                            Dim cmd_val As New OracleCommand
                                            cmd_val.Connection = conn
                                            cmd_val.CommandText = "insert into validity(Barcode,Validity_Date) values('" + TextBox1.Text + "','" + DateTimePicker1.Value + "')"
                                            conn.Open()
                                            cmd_val.ExecuteNonQuery()
                                            conn.Close()
                                        End If
                                        '--------------------------------------original prices
                                        Dim cmd_org As New OracleCommand
                                        cmd_org.Connection = conn
                                        cmd_org.CommandText = "insert into original_prices(Barcode,I_D,S_P) values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + S.ToString + "')"
                                        conn.Open()
                                        cmd_org.ExecuteNonQuery()
                                        conn.Close()

                                        'TextBox1.Clear()
                                        TextBox1.Enabled = True
                                        TextBox2.Clear()
                                        TextBox3.Clear()
                                        DateTimePicker1.Update()
                                        TextBox1.Focus()
                                        CheckBox1.Checked = False
                                        CheckBox1.Enabled = True
                                    Catch ex As Exception
                                        MessageBox.Show(ex.Message)
                                    End Try



                                End If
                                conn.Open()
                                Dim cmd2 As New OracleCommand

                                cmd2.Connection = conn
                                cmd2.CommandText = "select * from prices_history where barcode= '" + TextBox1.Text + "'"

                                '  Dim adapter As New OracleDataAdapter(cmd)
                                ' Dim ds As New DataSet
                                'adapter.Fill(ds, "stock")
                                'DataGridView1.DataSource = ds.Tables(0)
                                Dim rd2 As OracleDataReader = cmd2.ExecuteReader
                                DataGridView1.Rows.Clear()

                                While rd2.Read

                                    DataGridView1.Rows.Add(rd2.GetValue(0).ToString, rd2.GetValue(1).ToString, rd2.GetValue(2).ToString, rd2.GetValue(3).ToString, rd2.GetValue(4).ToString, rd2.GetValue(5).ToString)

                                End While
                                rd2.Close()

                                conn.Close()
                            End If
                            TextBox1.Clear()
                            TextBox1.Focus()

                        Else
                            MsgBox("Null sale price is not accepted..", MsgBoxStyle.Information)
                        End If
                    Else
                        MsgBox("Null Description is not accepted..", MsgBoxStyle.Information)
                    End If
                Else
                    MsgBox("Null barcode is not accepted..", MsgBoxStyle.Information)

                End If


            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            TextBox1.Focus()

        End If
    End Sub
    Public Function CO_ID() As Double

        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim ID_ev As Double
        'Dim u_id, pw, ip As String


        ' u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


        ' pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

        ' ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")

        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"

        cmd.CommandText = "select max(id_ev) from co_ev"
        cmd.Connection = conn
        Try
            conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader

            If rd.Read Then
                If rd.GetValue(0).ToString <> "" Then
                    ID_ev = rd.GetValue(0)

                Else
                    ID_ev = 999
                End If



            End If
            rd.Close()
            conn.Close()
            ID_ev = ID_ev + 1

            'If ID_ev Then
            'ID_ev = ID_ev + 1
            Return ID_ev
            ' Else
            ' ID_ev = 1000
            ' Return ID_ev
            ' End If






        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            If CheckBox2.Checked = True Then
                DateTimePicker1.Focus()
            ElseIf CheckBox2.Checked = False Then
                TextBox3.Focus()
            End If
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

    End Sub

    Private Sub CheckBox2_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckStateChanged
        If CheckBox2.Checked Then
            DateTimePicker1.Enabled = True
        Else
            DateTimePicker1.Enabled = False
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            TextBox1.Enabled = True
            TextBox1.Clear()
            TextBox1.Focus()
            TextBox2.Clear()
            TextBox3.Clear()
            CheckBox1.Enabled = True
            CheckBox1.Checked = False
            DataGridView1.Rows.Clear()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub CheckBox1_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckStateChanged
        If CheckBox1.Checked = True Then
            TextBox1.Focus()
        Else
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            Dim cmd As New OracleCommand
            Dim cmd1 As New OracleCommand
            Dim cmd2 As New OracleCommand
            Dim cmd3 As New OracleCommand

            Dim cmd4 As New OracleCommand
            Dim cmd5 As New OracleCommand
            Dim cmd6 As New OracleCommand
            Dim cmd7 As New OracleCommand
            Dim cmd8 As New OracleCommand
            Dim cmd9 As New OracleCommand
            Dim cmd_pbw As New OracleCommand
            Dim id As Double = get_id()

            ' Dim u_id, pw, ip As String


            ' u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


            ' pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

            ' ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")



            'Dim oradb As String = "Data Source=10.64.61.233:1521/orcl;User Id=07980;Password=8091;" 'example for connection string to monsoon aseer database
            Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim conn As New OracleConnection(oradb)



            cmd.Connection = conn
            cmd.CommandText = "delete from as_itm where id_itm='" + TextBox1.Text + "' "
            cmd1.Connection = conn
            cmd1.CommandText = "delete from AS_ITM_STK where id_itm='" + TextBox1.Text + "'"
            cmd2.Connection = conn
            cmd2.CommandText = "delete from CO_BRK_SPR_ITM_BS where id_itm_spr='" + TextBox1.Text + "'"
            cmd3.Connection = conn
            cmd3.CommandText = "delete from ID_IDN_PS where id_itm ='" + TextBox1.Text + "'"
            cmd4.Connection = conn
            cmd4.CommandText = "delete from MA_ITM_PRN_PRC_ITM where id_itm = '" + TextBox1.Text + "'"
            cmd5.Connection = conn
            cmd5.CommandText = "delete from MA_PRC_ITM where id_ev = :param1 "
            cmd5.Parameters.Add(":param1", OracleDbType.Double).Value = id
            cmd6.Connection = conn
            cmd6.CommandText = "delete from tr_chn_prn_prc where id_ev = :param1"
            cmd6.Parameters.Add(":param1", OracleDbType.Double).Value = id
            cmd7.Connection = conn
            cmd7.CommandText = "delete from co_ev_mnt where id_ev = :param1"
            cmd7.Parameters.Add(":param1", OracleDbType.Double).Value = id
            cmd8.Connection = conn
            cmd8.CommandText = "delete from AS_ITM_RTL_STR where id_itm = '" + TextBox1.Text + "'"
            cmd9.Connection = conn
            cmd9.CommandText = "delete from co_ev where id_ev = :param1"
            cmd9.Parameters.Add(":param1", OracleDbType.Double).Value = id
            cmd_pbw.Connection = conn
            cmd_pbw.CommandText = "delete from PRICEBASEDONWEIGHT where itm_id = '" + TextBox1.Text + "' "

            conn.Open()
            Dim ar As Integer = cmd.ExecuteNonQuery
            Dim ar1 As Integer = cmd1.ExecuteNonQuery
            Dim ar2 As Integer = cmd2.ExecuteNonQuery
            Dim ar3 As Integer = cmd3.ExecuteNonQuery
            Dim ar4 As Integer = cmd4.ExecuteNonQuery
            Dim ar5 As Integer = cmd5.ExecuteNonQuery
            Dim ar6 As Integer = cmd6.ExecuteNonQuery
            Dim ar7 As Integer = cmd7.ExecuteNonQuery
            Dim ar8 As Integer = cmd8.ExecuteNonQuery
            Dim ar9 As Integer = cmd9.ExecuteNonQuery
            Dim ar10 As Integer = cmd_pbw.ExecuteNonQuery
            conn.Close()

            ar = ar + ar1 + ar2 + ar3 + ar4 + ar5 + ar6 + ar7 + ar8 + ar9 + ar10

            If ar = 10 Then
                Label6.ForeColor = Color.Green
                Label6.Text = "Deleted Successfully..."

                DataGridView1.Rows.Clear()

                '-----------------------------------------------------Validity
                Dim cmd_val As New OracleCommand
                cmd_val.Connection = conn
                cmd_val.CommandText = "delete from Validity where Barcode = '" + TextBox1.Text + "'"
                conn.Open()
                cmd_val.ExecuteNonQuery()
                conn.Close()

                conn.Open()



                Dim cmd10 As New OracleCommand
                cmd10.Connection = conn
                cmd10.CommandText = "select user_name from log_in"

                Dim rd As OracleDataReader = cmd10.ExecuteReader
                Dim user_name As String = ""

                While rd.Read
                    user_name = rd.GetValue(0)

                End While
                rd.Close()
                conn.Close()

                Dim cmd11 As New OracleCommand
                cmd11.Connection = conn
                cmd11.CommandText = "insert into prices_history(barcode,description,sell_price,entry_time,created_by,operation_type) values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh24:mi:ss'),'" + user_name + "','Deleting item')"
                conn.Open()
                cmd11.ExecuteNonQuery()
                conn.Close()
                '------------------------------delete original
                conn.Open()
                Dim del_org As New OracleCommand
                del_org.Connection = conn
                del_org.CommandText = "delete from original_prices where Barcode = '" + TextBox1.Text + "'"
                del_org.ExecuteNonQuery()
                conn.Close()

            End If

            TextBox1.Enabled = True
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            DateTimePicker1.Update()
            CheckBox1.Enabled = True
            CheckBox1.Checked = False
            DataGridView1.Rows.Clear()

            TextBox1.Focus()



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
End Class