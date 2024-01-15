Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing
Imports Excel = Microsoft.Office.Interop.Excel
Public Class Promotions
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
    Private _success As Boolean = False
    Private Sub Promotions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            '-------------------------

            txt1.Enabled = False
            txt5.Enabled = False
            txt2.Enabled = False
            txt3.Enabled = False
            '-------------------
            Dim readValue2 = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SID", "POSKeyValue", Nothing)

            'MessageBox.Show(readValue.ToString)
            If readValue2.ToString <> "" Then
                oracle_sid = readValue2.ToString

            Else
                MsgBox("Please update DB SID...", MsgBoxStyle.Information)
            End If
            '------------------------------------------
            '-------------------------------
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

    Private Sub Rad1_CheckedChanged(sender As Object, e As EventArgs) Handles Rad1.CheckedChanged
        Try
            If Rad1.Checked = True Then

                txt4.Enabled = False








                DataGridView1.Visible = True
                    DataGridView2.Visible = False


                'ProgressBar1.Visible = True

                Dim itm_name As String = ""
                    Dim itm_barcode As String = ""
                    Dim itm_validity As String = ""
                    Dim itm_price As String = ""

                'Dim u_id, pw, ip As String
                ' u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


                ' pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

                'ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")



                Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                Dim conn As New OracleConnection
                    conn.ConnectionString = oradb

                    conn.Open()

                    Dim cmd As New OracleCommand
                    Dim cmd1 As New OracleCommand
                    Dim id As Double = get_id()
                    cmd.Connection = conn
                    cmd.CommandText = "select id_itm,de_itm from as_itm"
                    cmd1.Connection = conn
                    cmd1.CommandText = "select mo_chn_prn_un_prc from TR_CHN_PRN_PRC"


                    '  Dim adapter As New OracleDataAdapter(cmd)
                    ' Dim ds As New DataSet
                    'adapter.Fill(ds, "stock")
                    'DataGridView1.DataSource = ds.Tables(0)
                    Dim rd As OracleDataReader = cmd.ExecuteReader
                    Dim rd1 As OracleDataReader = cmd1.ExecuteReader

                    DataGridView1.Rows.Clear()

                    While rd.Read And rd1.Read
                        itm_barcode = rd.GetValue(0).ToString
                        itm_name = rd.GetValue(1).ToString
                    itm_price = rd1.GetValue(0)
                    DataGridView1.Rows.Add(itm_barcode, itm_name, itm_price, itm_price)
                    'ProgressBar1.Value = ProgressBar1.Value + 1
                End While

                    rd.Close()
                    rd1.Close()

                'ProgressBar1.Visible = True
                'ProgressBar1.Style = ProgressBarStyle.Continuous
                'ProgressBar1.Visible = True
                'ProgressBar1.Minimum = 0
                'ProgressBar1.Maximum = DataGridView1.Rows.Count





                conn.Close()


                End If



        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub
    Public Function get_id() As Double
        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim id As Double
        ' Dim u_id, pw, ip As String


        ' u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


        ' pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

        'ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")

        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
        cmd.CommandText = "select id_ev from MA_ITM_PRN_PRC_ITM where id_itm='" + txt4.Text + "'"
        cmd.Connection = conn

        Try
            conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader
            If rd.Read Then

                id = rd.GetValue(0)

            End If
            Return id

        Catch ex As Exception
            'MessageBox.Show(ex.Message)

        End Try
        'select id_ev from MA_ITM_PRN_PRC_ITM where id_itm='8904150313909'
    End Function

    Private Sub Rad4_CheckedChanged(sender As Object, e As EventArgs) Handles Rad2.CheckedChanged
        If Rad2.Checked = True Then


            DataGridView1.Rows.Clear()


            txt4.Enabled = True
            txt4.Focus()

        Else
            txt2.Enabled = False






        End If
    End Sub

    Private Sub txt4_TextChanged(sender As Object, e As EventArgs) Handles txt4.TextChanged

    End Sub

    Private Sub txt4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            Try

            DataGridView1.Visible = True
            DataGridView2.Visible = False





            ' Dim u_id, pw, ip As String


            'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


            'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

            'ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")



            txt4.Focus()
            Dim conn As New OracleConnection
            Dim cmd As New OracleCommand
            Dim cmd1 As New OracleCommand
            Dim id As Double = get_id()
            Dim itm_name As String = ""
            Dim itm_price As String = ""
            Dim itm_barcode As String = ""
            Dim itm_validity As String = ""

            cmd.Connection = conn
            cmd.CommandText = "select de_itm from as_itm where id_itm = '" + txt4.Text.Trim + "'"
            cmd1.Connection = conn
            cmd1.CommandText = "select mo_chn_prn_un_prc from TR_CHN_PRN_PRC where id_ev = :param1"
            cmd1.Parameters.Add(":param1", OracleDbType.Double).Value = id

                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"



                conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader
                If rd.Read Then
                    itm_name = rd.GetValue(0).ToString

                Else
                    MsgBox("The item not found", MsgBoxStyle.Critical)
                    txt4.Clear()
                    txt4.Focus()
                    Exit Sub
                End If
            Dim rd1 As OracleDataReader = cmd1.ExecuteReader
            If rd1.Read Then
                itm_price = rd1.GetValue(0).ToString

            End If
            conn.Close()



                For i = 0 To DataGridView1.Rows.Count - 1

                    If DataGridView1.Rows(i).Cells(0).Value = txt4.Text Then
                        MsgBox("The item is avialable in the list", MsgBoxStyle.Critical)
                        txt4.Clear()
                        txt4.Focus()
                        Exit Sub

                    End If
                Next

                DataGridView1.Rows.Add(txt4.Text, itm_name.ToString, itm_price, itm_price)
                txt4.Clear()
                txt4.Focus()

            Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        End If





    End Sub

    Private Sub txt5_TextChanged(sender As Object, e As EventArgs) Handles txt5.TextChanged

    End Sub

    Private Sub txt5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt5.KeyPress
        Try
            Dim txt As TextBox = CType(sender, TextBox)
            If Not Char.IsDigit(e.KeyChar) Then e.Handled = True

            If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

            If e.KeyChar = "." And txt.Text.IndexOf(".") = -1 Then e.Handled = False 'allow single decimal point

            If e.KeyChar = "-" And txt.SelectionStart = 0 Then e.Handled = False 'allow negative number

            If e.KeyChar = Convert.ToChar(13) Then
                Try
                    If CheckBox2.Checked = True Then
                        If txt5.Text <> "" Then
                            For i = 0 To DataGridView1.Rows.Count - 1
                                DataGridView1.Rows(i).Cells(2).Value = DataGridView1.Rows(i).Cells(3).Value
                                DataGridView1.Rows(i).Cells(3).Value = Double.Parse(txt5.Text)


                            Next
                        End If
                    End If



                Catch ex As Exception

                End Try
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txt1_TextChanged(sender As Object, e As EventArgs) Handles txt1.TextChanged

    End Sub

    Private Sub txt1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt1.KeyPress
        Dim txt As TextBox = CType(sender, TextBox)
        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True

        If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

        If e.KeyChar = "." And txt.Text.IndexOf(".") = -1 Then e.Handled = False 'allow single decimal point

        If e.KeyChar = "-" And txt.SelectionStart = 0 Then e.Handled = False 'allow negative number
        If e.KeyChar = Convert.ToChar(13) Then
            If Double.Parse(txt1.Text) > -100 And Double.Parse(txt1.Text) < 100 And txt1.Text <> "" Then
                Try

                    For i As Integer = 0 To DataGridView1.Rows.Count - 1
                        DataGridView1.Rows(i).Cells(2).Value = DataGridView1.Rows(i).Cells(3).Value
                        DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(3).Value - ((DataGridView1.Rows(i).Cells(3).Value * Double.Parse(txt1.Text) / 100))

                    Next

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            End If
        End If
    End Sub
    Public Function P_ID() As Double

        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim PID As Double
        ' Dim u_id, pw, ip As String


        'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


        'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

        ' ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")




        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"

        cmd.CommandText = "select max(PROMOTION_ID) from PROMOTIONS_ID"

        cmd.Connection = conn
        Try
            conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader

            If rd.Read Then
                If rd.GetValue(0).ToString <> "" Then
                    PID = rd.GetValue(0)

                Else
                    PID = 999
                End If



            End If

            PID = PID + 1

            'If ID_ev Then
            'ID_ev = ID_ev + 1
            Return PID
            ' Else
            ' ID_ev = 1000
            ' Return ID_ev
            ' End If






        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Public Function P_ID1() As Double

        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim PID As Double
        ' Dim u_id, pw, ip As String


        'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


        'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

        ' ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")




        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"

        cmd.CommandText = "select max(PROMOTION_ID) from B2G1"

        cmd.Connection = conn
        Try
            conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader

            If rd.Read Then
                If rd.GetValue(0).ToString <> "" Then
                    PID = rd.GetValue(0)

                Else
                    PID = 99
                End If



            End If

            PID = PID + 1

            'If ID_ev Then
            'ID_ev = ID_ev + 1
            Return PID
            ' Else
            ' ID_ev = 1000
            ' Return ID_ev
            ' End If






        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then

            CheckBox2.Checked = False
            CheckBox3.Checked = False
            CheckBox4.Checked = False
            txt1.Enabled = True
            txt5.Enabled = False
            txt2.Enabled = False
            txt3.Enabled = False

        Else
            txt1.Enabled = False
            For i = 0 To DataGridView1.Rows.Count - 1
                DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(2).Value

            Next
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            CheckBox1.Checked = False
            CheckBox3.Checked = False
            CheckBox4.Checked = False
            txt1.Enabled = False
            txt5.Enabled = True
            txt2.Enabled = False
            txt3.Enabled = False
        Else
            txt5.Enabled = False
            For i = 0 To DataGridView1.Rows.Count - 1
                DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(2).Value

            Next
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            txt1.Enabled = False
            txt5.Enabled = False
            txt2.Enabled = True
            txt3.Enabled = True
        Else
            txt2.Enabled = False
            txt3.Enabled = False
            For i = 0 To DataGridView1.Rows.Count - 1
                DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(2).Value

            Next

        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox4.Checked = False
            txt1.Enabled = False
            txt5.Enabled = False
            txt2.Enabled = True
            txt3.Enabled = True
        Else
            txt2.Enabled = False
            txt3.Enabled = False
            For i = 0 To DataGridView1.Rows.Count - 1
                DataGridView1.Rows(i).Cells(3).Value = DataGridView1.Rows(i).Cells(2).Value

            Next

        End If
    End Sub

    Private Sub Radp3_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try




            If DataGridView1.Visible = True Then



                Dim proc As Process = New Process
                'Dim u_id, pw, ip As String


                'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


                'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

                'ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")

                'Dim oradb As String = "Data Source=10.64.61.233:1521/orcl;User Id=07980;Password=8091;" 'example for connection string to monsoon aseer database
                Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                Dim conn As New OracleConnection(oradb)
                Dim id As Double
                id = P_ID()

                If CheckBox2.Checked = True Then
                    _success = False
                    ProgressBar1.Style = ProgressBarStyle.Continuous
                    ProgressBar1.Visible = True
                    ProgressBar1.Minimum = 0
                    ProgressBar1.Maximum = DataGridView1.Rows.Count

                    ProgressBar1.Value = 0
                    BackgroundWorker1.RunWorkerAsync()


                    Dim cmd1 As New OracleCommand
                    Dim ar1 As Integer





                    Try





                        ' cmd.CommandText = "insert into stock values(" + TextBox1.Text + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value + "," + DataGridView1.Rows(i).Cells(3).Value + "," + DataGridView1.Rows(i).Cells(4).Value + ",sysdate)"




                        conn.Open()
                        cmd1.Connection = conn



                        cmd1.CommandText = "insert into promotions_id values('" + id.ToString + "','ONE PRCIE  & " + txt5.Text + "','" & Login.POS_USER & "',sysdate)"
                        ar1 = cmd1.ExecuteNonQuery()

                        conn.Close()



                        '--------------------apply promotion one prcie for all

                        'MOVE c:\originalfolder\* c:\destinationfolder


                        conn.Open()
                        Dim Promo_oneprice As New OracleCommand

                        Dim indication As Integer
                        Dim cout As Integer = 0

                        Promo_oneprice.Connection = conn
                        For i = 0 To DataGridView1.Rows.Count - 1

                            Promo_oneprice.CommandText = "insert into promo_oneprice values('" + DataGridView1.Rows(i).Cells(0).Value.ToString + "','0','" + id.ToString + "','" + DateTimePicker1.Text + "','" + DateTimePicker2.Text + "',sysdate," & DataGridView1.Rows(i).Cells(2).Value & "," & DataGridView1.Rows(i).Cells(3).Value & ")"

                            indication = Promo_oneprice.ExecuteNonQuery
                            ProgressBar1.Value = ProgressBar1.Value + 1
                            cout = cout + indication
                            If i = DataGridView1.Rows.Count - 1 Then
                                MsgBox("Promotion:" & id & "  has been updated", MsgBoxStyle.Information)
                                DataGridView1.Rows.Clear()
                                ProgressBar1.Value = 0
                                conn.Close()

                                With proc.StartInfo
                                    .FileName = "cmd.exe"
                                    .Arguments = "/C MOVE c:\POS\Promotions\* c:\POS\Archive "
                                    .UseShellExecute = False
                                    .RedirectStandardOutput = True
                                    .RedirectStandardError = True
                                    .CreateNoWindow = True
                                    .WindowStyle = ProcessWindowStyle.Hidden
                                End With
                                proc.Start()
                                'Exit For

                            End If


                        Next

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try




                End If

                If CheckBox1.Checked = True Then

                    _success = False
                    ProgressBar1.Style = ProgressBarStyle.Continuous
                    ProgressBar1.Visible = True
                    ProgressBar1.Minimum = 0
                    ProgressBar1.Maximum = DataGridView1.Rows.Count

                    ProgressBar1.Value = 0
                    BackgroundWorker1.RunWorkerAsync()



                    Dim cmd1 As New OracleCommand
                        Dim ar1 As Integer









                        ' cmd.CommandText = "insert into stock values(" + TextBox1.Text + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value + "," + DataGridView1.Rows(i).Cells(3).Value + "," + DataGridView1.Rows(i).Cells(4).Value + ",sysdate)"




                        conn.Open()
                        cmd1.Connection = conn



                    cmd1.CommandText = "insert into promotions_id values('" + id.ToString + "','discount %  & " + txt1.Text + "','" & Login.POS_USER & "',sysdate)"
                    ar1 = cmd1.ExecuteNonQuery()

                        conn.Close()



                    '--------------------apply promotion Pecentage %




                    conn.Open()
                        Dim Promo_perc As New OracleCommand

                        Dim indication As Integer
                        Dim cout As Integer
                        Promo_perc.Connection = conn

                    For i = 0 To DataGridView1.Rows.Count - 1

                        Promo_perc.CommandText = "insert into PROMO_PER values('" + DataGridView1.Rows(i).Cells(0).Value.ToString + "','" + txt1.Text + "','" + id.ToString + "','" + DateTimePicker1.Text + "','" + DateTimePicker2.Text + "',sysdate," & DataGridView1.Rows(i).Cells(2).Value & "," & DataGridView1.Rows(i).Cells(3).Value & ")"

                        indication = Promo_perc.ExecuteNonQuery
                        ProgressBar1.Value = ProgressBar1.Value + 1
                        cout = cout + indication
                        If i = DataGridView1.Rows.Count - 1 Then
                            MsgBox("Promotion:" & id & "  has been updated", MsgBoxStyle.Information)
                            DataGridView1.Rows.Clear()
                            ProgressBar1.Value = 0
                            conn.Close()
                            With proc.StartInfo
                                .FileName = "cmd.exe"
                                .Arguments = "/C MOVE c:\POS\Promotions\* c:\POS\Archive "
                                .UseShellExecute = False
                                .RedirectStandardOutput = True
                                .RedirectStandardError = True
                                .CreateNoWindow = True
                                .WindowStyle = ProcessWindowStyle.Hidden
                            End With
                            proc.Start()
                            'Exit For

                        End If



                    Next




                End If


                '---------------------Buy X Get y Disc.
                If CheckBox3.Checked = True Then

                    _success = False
                    ProgressBar1.Style = ProgressBarStyle.Continuous
                    ProgressBar1.Visible = True
                    ProgressBar1.Minimum = 0
                    ProgressBar1.Maximum = DataGridView1.Rows.Count

                    ProgressBar1.Value = 0
                    BackgroundWorker1.RunWorkerAsync()



                    Dim cmd1 As New OracleCommand
                    Dim ar1 As Integer









                    ' cmd.CommandText = "insert into stock values(" + TextBox1.Text + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value + "," + DataGridView1.Rows(i).Cells(3).Value + "," + DataGridView1.Rows(i).Cells(4).Value + ",sysdate)"




                    conn.Open()
                    cmd1.Connection = conn



                    cmd1.CommandText = "insert into promotions_id values('" + id.ToString + "','buy x get y Discount','" & Login.POS_USER & "',sysdate)"
                    ar1 = cmd1.ExecuteNonQuery()

                    conn.Close()



                    '--------------------apply promotion 




                    conn.Open()
                    Dim Promo_bxgy As New OracleCommand

                    Dim indication As Integer
                    Dim cout As Integer
                    Promo_bxgy.Connection = conn

                    For i = 0 To DataGridView1.Rows.Count - 1

                        Promo_bxgy.CommandText = "insert into buy_x_get_y_discount values('" + DataGridView1.Rows(i).Cells(0).Value.ToString + "','" & txt2.Text & "','" & txt3.Text & "','0','" & id.ToString & "','" + DateTimePicker1.Text + "','" + DateTimePicker2.Text + "')"

                        indication = Promo_bxgy.ExecuteNonQuery
                        ProgressBar1.Value = ProgressBar1.Value + 1
                        cout = cout + indication
                        If i = DataGridView1.Rows.Count - 1 Then
                            MsgBox("Promotion:" & id & "  has been updated", MsgBoxStyle.Information)
                            DataGridView1.Rows.Clear()
                            ProgressBar1.Value = 0
                            conn.Close()
                            With proc.StartInfo
                                .FileName = "cmd.exe"
                                .Arguments = "/C MOVE c:\POS\Promotions\* c:\POS\Archive "
                                .UseShellExecute = False
                                .RedirectStandardOutput = True
                                .RedirectStandardError = True
                                .CreateNoWindow = True
                                .WindowStyle = ProcessWindowStyle.Hidden
                            End With
                            proc.Start()
                            'Exit For

                        End If



                    Next




                End If


            End If

            ''''''''''''Browse Excel file
            If DataGridView2.Visible = True Then
                Dim proc As Process = New Process
                'Dim u_id, pw, ip As String


                'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


                'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

                'ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")

                'Dim oradb As String = "Data Source=10.64.61.233:1521/orcl;User Id=07980;Password=8091;" 'example for connection string to monsoon aseer database
                Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                Dim conn As New OracleConnection(oradb)
                Dim id As Double
                id = P_ID()

                If CheckBox2.Checked = True Then
                    _success = False
                    ProgressBar1.Style = ProgressBarStyle.Continuous
                    ProgressBar1.Visible = True
                    ProgressBar1.Minimum = 0
                    ProgressBar1.Maximum = DataGridView2.Rows.Count

                    ProgressBar1.Value = 0
                    BackgroundWorker1.RunWorkerAsync()


                    Dim cmd1 As New OracleCommand
                    Dim ar1 As Integer





                    Try





                        ' cmd.CommandText = "insert into stock values(" + TextBox1.Text + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value + "," + DataGridView1.Rows(i).Cells(3).Value + "," + DataGridView1.Rows(i).Cells(4).Value + ",sysdate)"




                        conn.Open()
                        cmd1.Connection = conn



                        cmd1.CommandText = "insert into promotions_id values('" + id.ToString + "','ONE PRCIE  & " + txt5.Text + "','" & Login.POS_USER & "',sysdate)"
                        ar1 = cmd1.ExecuteNonQuery()

                        conn.Close()



                        '--------------------apply promotion one prcie for all

                        'MOVE c:\originalfolder\* c:\destinationfolder


                        conn.Open()
                        Dim Promo_oneprice As New OracleCommand

                        Dim indication As Integer
                        Dim cout As Integer = 0

                        Promo_oneprice.Connection = conn
                        For i = 0 To DataGridView1.Rows.Count - 1

                            Promo_oneprice.CommandText = "insert into promo_oneprice values('" + DataGridView2.Rows(i).Cells(0).Value.ToString + "','0','" + id.ToString + "','" + DateTimePicker1.Text + "','" + DateTimePicker2.Text + "',sysdate," & DataGridView2.Rows(i).Cells(2).Value & "," & DataGridView2.Rows(i).Cells(3).Value & ")"

                            indication = Promo_oneprice.ExecuteNonQuery
                            ProgressBar1.Value = ProgressBar1.Value + 1
                            cout = cout + indication
                            If i = DataGridView1.Rows.Count - 1 Then
                                MsgBox("Promotion:" & id & "  has been updated", MsgBoxStyle.Information)
                                DataGridView1.Rows.Clear()
                                ProgressBar1.Value = 0
                                conn.Close()

                                With proc.StartInfo
                                    .FileName = "cmd.exe"
                                    .Arguments = "/C MOVE c:\POS\Promotions\* c:\POS\Archive "
                                    .UseShellExecute = False
                                    .RedirectStandardOutput = True
                                    .RedirectStandardError = True
                                    .CreateNoWindow = True
                                    .WindowStyle = ProcessWindowStyle.Hidden
                                End With
                                proc.Start()
                                'Exit For

                            End If


                        Next

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try




                End If

                If CheckBox1.Checked = True Then

                    _success = False
                    ProgressBar1.Style = ProgressBarStyle.Continuous
                    ProgressBar1.Visible = True
                    ProgressBar1.Minimum = 0
                    ProgressBar1.Maximum = DataGridView2.Rows.Count

                    ProgressBar1.Value = 0
                    BackgroundWorker1.RunWorkerAsync()



                    Dim cmd1 As New OracleCommand
                    Dim ar1 As Integer









                    ' cmd.CommandText = "insert into stock values(" + TextBox1.Text + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value + "," + DataGridView1.Rows(i).Cells(3).Value + "," + DataGridView1.Rows(i).Cells(4).Value + ",sysdate)"




                    conn.Open()
                    cmd1.Connection = conn



                    cmd1.CommandText = "insert into promotions_id values('" + id.ToString + "','discount %  & " + txt1.Text + "','" & Login.POS_USER & "',sysdate)"
                    ar1 = cmd1.ExecuteNonQuery()

                    conn.Close()



                    '--------------------apply promotion Pecentage %




                    conn.Open()
                    Dim Promo_perc As New OracleCommand

                    Dim indication As Integer
                    Dim cout As Integer
                    Promo_perc.Connection = conn

                    For i = 0 To DataGridView1.Rows.Count - 1

                        Promo_perc.CommandText = "insert into PROMO_PER values('" + DataGridView2.Rows(i).Cells(0).Value.ToString + "','" + txt1.Text + "','" + id.ToString + "','" + DateTimePicker1.Text + "','" + DateTimePicker2.Text + "',sysdate," & DataGridView2.Rows(i).Cells(2).Value & "," & DataGridView2.Rows(i).Cells(3).Value & ")"

                        indication = Promo_perc.ExecuteNonQuery
                        ProgressBar1.Value = ProgressBar1.Value + 1
                        cout = cout + indication
                        If i = DataGridView1.Rows.Count - 1 Then
                            MsgBox("Promotion:" & id & "  has been updated", MsgBoxStyle.Information)
                            DataGridView1.Rows.Clear()
                            ProgressBar1.Value = 0
                            conn.Close()
                            With proc.StartInfo
                                .FileName = "cmd.exe"
                                .Arguments = "/C MOVE c:\POS\Promotions\* c:\POS\Archive "
                                .UseShellExecute = False
                                .RedirectStandardOutput = True
                                .RedirectStandardError = True
                                .CreateNoWindow = True
                                .WindowStyle = ProcessWindowStyle.Hidden
                            End With
                            proc.Start()
                            'Exit For

                        End If



                    Next




                End If




                If CheckBox3.Checked = True Then

                    _success = False
                    ProgressBar1.Style = ProgressBarStyle.Continuous
                    ProgressBar1.Visible = True
                    ProgressBar1.Minimum = 0
                    ProgressBar1.Maximum = DataGridView1.Rows.Count

                    ProgressBar1.Value = 0
                    BackgroundWorker1.RunWorkerAsync()



                    Dim cmd1 As New OracleCommand
                    Dim ar1 As Integer









                    ' cmd.CommandText = "insert into stock values(" + TextBox1.Text + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value + "," + DataGridView1.Rows(i).Cells(3).Value + "," + DataGridView1.Rows(i).Cells(4).Value + ",sysdate)"




                    conn.Open()
                    cmd1.Connection = conn



                    cmd1.CommandText = "insert into promotions_id values('" + id.ToString + "','buy x get y Discount','" & Login.POS_USER & "',sysdate)"
                    ar1 = cmd1.ExecuteNonQuery()

                    conn.Close()



                    '--------------------apply promotion 




                    conn.Open()
                    Dim Promo_bxgy As New OracleCommand

                    Dim indication As Integer
                    Dim cout As Integer
                    Promo_bxgy.Connection = conn

                    For i = 0 To DataGridView1.Rows.Count - 1

                        Promo_bxgy.CommandText = "insert into buy_x_get_y_discount values('" + DataGridView1.Rows(i).Cells(0).Value.ToString + "','" & txt2.Text & "','" & txt3.Text & "','0','" & id.ToString & "','" + DateTimePicker1.Text + "','" + DateTimePicker2.Text + "')"

                        indication = Promo_bxgy.ExecuteNonQuery
                        ProgressBar1.Value = ProgressBar1.Value + 1
                        cout = cout + indication
                        If i = DataGridView1.Rows.Count - 1 Then
                            MsgBox("Promotion:" & id & "  has been updated", MsgBoxStyle.Information)
                            DataGridView1.Rows.Clear()
                            ProgressBar1.Value = 0
                            conn.Close()
                            With proc.StartInfo
                                .FileName = "cmd.exe"
                                .Arguments = "/C MOVE c:\POS\Promotions\* c:\POS\Archive "
                                .UseShellExecute = False
                                .RedirectStandardOutput = True
                                .RedirectStandardError = True
                                .CreateNoWindow = True
                                .WindowStyle = ProcessWindowStyle.Hidden
                            End With
                            proc.Start()
                            'Exit For

                        End If



                    Next




                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)


        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        _success = True
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Cancel_Promotion.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class