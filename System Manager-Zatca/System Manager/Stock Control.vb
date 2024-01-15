Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing
Imports Microsoft.Reporting.WinForms
Public Class Stock_Control
    Public u_id As String = ""
    Public pw As String = ""
    Dim ip As String
    Dim oracle_sid As String
    Dim rn As Double
    Dim CF As Double
    Dim S As Double = 0
    Private Sub Stock_Control_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txt1.Focus()
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


            rn = RN_ID()
            txt1.Text = rn
            txt5.Enabled = False
            txt6.Enabled = False

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
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Function RN_ID() As Double

        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim RN As Double


        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"

        cmd.CommandText = "select max(R_ID) from r_i"
        cmd.Connection = conn
        Try
            conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader

            If rd.Read Then
                If rd.GetValue(0).ToString <> "" Then
                    RN = rd.GetValue(0)

                Else
                    RN = 99
                End If



            End If

            RN = RN + 1

            'If ID_ev Then
            'ID_ev = ID_ev + 1
            Return RN
            ' Else
            ' ID_ev = 1000
            ' Return ID_ev
            ' End If


            rd.Close()
            conn.Close()



        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub txt1_TextChanged(sender As Object, e As EventArgs) Handles txt1.TextChanged
        Try
            txt2.Clear()
            DateTimePicker1.Update()
            DataGridView1.Rows.Clear()
            Dim indecator As Integer = 0


            Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim conn As New OracleConnection
            conn.ConnectionString = oradb

            conn.Open()

            Dim cmd As New OracleCommand

            cmd.Connection = conn
            cmd.CommandText = "select * from stock where R_N = '" + txt1.Text + "'"

            '  Dim adapter As New OracleDataAdapter(cmd)
            ' Dim ds As New DataSet
            'adapter.Fill(ds, "stock")
            'DataGridView1.DataSource = ds.Tables(0)
            Dim rd As OracleDataReader = cmd.ExecuteReader
            DataGridView1.Rows.Clear()

            While rd.Read

                'TextBox1.Text = rd.GetValue(0)
                txt2.Text = rd.GetValue(1)
                txtd.Text = rd.GetValue(2)
                DataGridView1.Rows.Add(rd.GetValue(3), rd.GetValue(4), rd.GetValue(5) * CF, rd.GetValue(6), rd.GetValue(7))
                If txt1.Text = rd.GetValue(0) Then
                    indecator = 1

                End If
            End While

            rd.Close()
            conn.Close()

            If indecator = 1 Then
                btn2.Enabled = True
                btn3.Enabled = True
                btn1.Enabled = False
                MenuStrip1.Items(1).Enabled = True



            ElseIf txt1.Text = rn Then
                btn3.Enabled = False
                btn2.Enabled = False
                btn1.Enabled = True
                MenuStrip1.Items(1).Enabled = False
            Else
                btn3.Enabled = False
                btn2.Enabled = False
                btn1.Enabled = False
                MenuStrip1.Items(1).Enabled = False
            End If

        Catch ex As Exception

            'MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub txt3_TextChanged(sender As Object, e As EventArgs) Handles txt3.TextChanged
        txt5.Enabled = False
        txt6.Enabled = False
        txt4.Clear()

    End Sub

    Private Sub txt3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True

            Dim conn As New OracleConnection
            Dim cmd As New OracleCommand
            Dim cmd1 As New OracleCommand
            Dim cmd_pbw As New OracleCommand
            Dim id As Double = get_id()


            txt3.UseWaitCursor = True

            cmd.Connection = conn
            cmd.CommandText = "select de_itm from as_itm where id_itm = '" + txt3.Text.Trim + "'"
            cmd1.Connection = conn
            cmd1.CommandText = "select mo_chn_prn_un_prc from TR_CHN_PRN_PRC where id_ev = :param1"
            cmd1.Parameters.Add(":param1", OracleDbType.Double).Value = id
            cmd_pbw.Connection = conn
            cmd_pbw.CommandText = "select ITM_NAME,PRICE_UNIT FROM PRICEBASEDONWEIGHT WHERE ITM_ID = '" + txt3.Text.Trim + "'"
            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"




            conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader
            Dim rd_pbw As OracleDataReader = cmd_pbw.ExecuteReader
            If rd.Read Then

                txt4.Text = rd.GetValue(0).ToString
                txt5.Enabled = True
                txt6.Enabled = True
                txt5.Focus()
            ElseIf rd_pbw.Read Then
                txt4.Text = rd_pbw.GetValue(0).ToString
                txt5.Enabled = True
                txt6.Enabled = True
                txt5.Focus()
            Else
                MsgBox("Item not found..", MsgBoxStyle.Information)
            End If

            conn.Close()
            txt3.UseWaitCursor = False
        End If

    End Sub
    Public Function get_id() As Double
        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim id As Double


        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
        cmd.CommandText = "select id_ev from MA_ITM_PRN_PRC_ITM where id_itm='" + txt3.Text.Trim + "'"
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

    End Function

    Private Sub txt6_TextChanged(sender As Object, e As EventArgs) Handles txt6.TextChanged

    End Sub

    Private Sub txt6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt6.KeyPress
        Dim txt As TextBox = CType(sender, TextBox)

        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True

        If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

        If e.KeyChar = "." And txt.Text.IndexOf(".") = -1 Then e.Handled = False 'allow single decimal point

        If e.KeyChar = "-" And txt.SelectionStart = 0 Then e.Handled = False 'allow negative number
        If e.KeyChar = Convert.ToChar(13) Then


            e.Handled = True
            Try


                If txt5.Text <> "" Then
                    If txt6.Text <> "" Then
                        DataGridView1.Rows.Add(txt3.Text, txt4.Text, txt5.Text, txt6.Text, DateTimePicker2.Value.Date.ToString("dd-MM-yyyy"))

                        txt3.Enabled = True
                        txt3.Clear()
                        txt4.Clear()
                        txt5.Clear()
                        txt6.Clear()


                        txt3.Focus()
                        txt6.Enabled = False
                        txt5.Enabled = False

                    Else
                        MsgBox("Null Quantity is not accepted..", MsgBoxStyle.Information)
                        txt6.Focus()

                    End If
                Else
                    MsgBox("Null initial price is not accepted..", MsgBoxStyle.Information)
                    txt6.Focus()


                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)

            End Try

        End If
    End Sub

    Private Sub txt5_TextChanged(sender As Object, e As EventArgs) Handles txt5.TextChanged

    End Sub

    Private Sub txt5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt5.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            txt6.Focus()
        End If
    End Sub

    Private Sub txt1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            txt2.Focus()
        End If
    End Sub

    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        Try
            If txt1.Text <> "" Then
                If txt2.Text <> "" Then
                    If DateTimePicker1.Text <> "" Then

                        Dim rn As Double

                        rn = RN_ID()
                        If txt1.Text = rn Then

                            If DataGridView1.Rows.Count > 0 Then





                                'Dim oradb As String = "Data Source=10.64.61.233:1521/orcl;User Id=07980;Password=8091;" 'example for connection string to monsoon aseer database
                                Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                                'Dim oradb As String = "Data Source=ORACLE:1521/orcl;User Id=10000;password=10111;"
                                Dim conn As New OracleConnection(oradb)





                                Dim cmd As New OracleCommand
                                Dim cmd1 As New OracleCommand



                                Dim ar1 As Integer









                                ' cmd.CommandText = "insert into stock values(" + TextBox1.Text + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value + "," + DataGridView1.Rows(i).Cells(3).Value + "," + DataGridView1.Rows(i).Cells(4).Value + ",sysdate)"

                                'DateTimePicker1.Value.ToShortDateString("dd-MM-yyyy") change date to string.

                                conn.Open()
                                cmd.Connection = conn
                                cmd.CommandText = "insert into R_I values(" + txt1.Text + ",sysdate)"
                                Dim ar As Integer = cmd.ExecuteNonQuery()
                                conn.Close()
                                If ar > 0 Then
                                    conn.Open()
                                    cmd1.Connection = conn
                                    For i As Integer = 0 To DataGridView1.Rows.Count - 1
                                        Double.TryParse(DataGridView1.Rows(i).Cells(2).Value, S)
                                        S = S / CF

                                        cmd1.CommandText = "insert into stock values('" + txt1.Text + "','" + txt2.Text + "','" + txtd.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "','" + S.ToString + "','" + DataGridView1.Rows(i).Cells(3).Value + "','" + DataGridView1.Rows(i).Cells(4).Value + "',sysdate)"
                                        ar1 = cmd1.ExecuteNonQuery()
                                    Next
                                    conn.Close()

                                    If ar1 > 0 Then
                                        MsgBox("Added Successfully", MsgBoxStyle.Information)
                                        '--------------------stock history
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
                                        conn.Open()
                                        For i As Integer = 0 To DataGridView1.Rows.Count - 1
                                            cmd11.CommandText = "insert into stock_history values('" + txt1.Text + "','" + txt2.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value + "," + DataGridView1.Rows(i).Cells(3).Value + ",sysdate,'" + user_name + "','Add Receipt')"
                                            cmd11.ExecuteNonQuery()
                                        Next



                                        conn.Close()



                                        DataGridView1.Rows.Clear()


                                        rn = RN_ID()
                                        txt1.Text = rn
                                        btn3.Enabled = False
                                        btn2.Enabled = False
                                        btn1.Enabled = True

                                        txt2.Focus()



                                        txt2.Clear()
                                        DateTimePicker1.Update()
                                        txt4.Clear()
                                        txt5.Clear()
                                        txt6.Clear()
                                        txt2.Focus()
                                    End If




                                End If
                            Else
                                MsgBox("Please enter items into receipt..", MsgBoxStyle.Information)

                                txt4.Focus()

                            End If
                        ElseIf txt1.Text > rn Then
                            MsgBox("Receipt Number is " & rn.ToString, MsgBoxStyle.Information)

                            txt1.Text = rn
                            txt2.Focus()

                            txt2.Clear()
                            DateTimePicker1.Update()

                        Else
                            MsgBox("Receipt number:" & txt1.Text & " already existed.", MsgBoxStyle.Information)

                        End If
                    Else
                        MsgBox("Null receipt date is not accepted..", MsgBoxStyle.Information)
                        DateTimePicker1.Focus()

                    End If

                Else
                    MsgBox("Null receipt reference is not accepted..", MsgBoxStyle.Information)
                    txt2.Focus()

                End If

            Else

                MsgBox("Null receipt number is not accepted..", MsgBoxStyle.Information)
                txt1.Focus()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txt2_TextChanged(sender As Object, e As EventArgs) Handles txt2.TextChanged

    End Sub

    Private Sub txt2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            DateTimePicker1.Focus()

        End If
    End Sub

    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click
        Try
            If txt1.Text <> "" Then
                If txt2.Text <> "" Then
                    If DateTimePicker1.Text <> "" Then

                        Dim rn As Double

                        rn = RN_ID()
                        If txt1.Text <> rn Then

                            If DataGridView1.Rows.Count > 0 Then



                                'Dim oradb As String = "Data Source=10.64.61.233:1521/orcl;User Id=07980;Password=8091;" 'example for connection string to monsoon aseer database
                                Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                                'Dim oradb As String = "Data Source=ORACLE:1521/orcl;User Id=10000;password=10111;"
                                Dim conn As New OracleConnection(oradb)


                                Dim cmd As New OracleCommand
                                Dim cmd1 As New OracleCommand


                                Dim ar1 As Integer

                                conn.Open()
                                cmd.Connection = conn
                                cmd.CommandText = "delete from stock where R_N='" + txt1.Text + "'"
                                Dim ar As Integer = cmd.ExecuteNonQuery()
                                conn.Close()
                                ' If ar > 0 Then
                                conn.Open()
                                cmd1.Connection = conn
                                For i As Integer = 0 To DataGridView1.Rows.Count - 1

                                    Double.TryParse(DataGridView1.Rows(i).Cells(2).Value, S)
                                    S = S / CF

                                    cmd1.CommandText = "insert into stock values('" + txt1.Text + "','" + txt2.Text + "','" + DateTimePicker1.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "','" + S.ToString + "','" + DataGridView1.Rows(i).Cells(3).Value + "','" + DataGridView1.Rows(i).Cells(4).Value + "',sysdate)"
                                    ar1 = cmd1.ExecuteNonQuery()
                                Next
                                conn.Close()

                                If ar1 > 0 Then
                                    MsgBox("Updated Successfully", MsgBoxStyle.Information)
                                    '--------------------stock history
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
                                    conn.Open()
                                    For i As Integer = 0 To DataGridView1.Rows.Count - 1
                                        cmd11.CommandText = "insert into stock_history values('" + txt1.Text + "','" + txt2.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value.ToString + "," + DataGridView1.Rows(i).Cells(3).Value + ",sysdate,'" + user_name + "','Update Receipt')"
                                        cmd11.ExecuteNonQuery()
                                    Next



                                    conn.Close()



                                    DataGridView1.Rows.Clear()


                                    rn = RN_ID()
                                    txt1.Text = rn
                                    btn3.Enabled = False
                                    btn2.Enabled = False
                                    btn1.Enabled = True

                                    txt2.Focus()



                                    txt2.Clear()
                                    DateTimePicker1.Update()
                                    txt4.Clear()
                                    txt5.Clear()
                                    txt6.Clear()
                                    txt2.Focus()
                                End If




                                ' End If
                            Else
                                MsgBox("Please enter items into receipt..", MsgBoxStyle.Information)

                                txt4.Focus()

                            End If
                        ElseIf txt1.Text > rn Then
                            MsgBox("Receipt Number is " & rn.ToString, MsgBoxStyle.Information)

                            txt1.Text = rn
                            txt2.Focus()

                            txt2.Clear()
                            DateTimePicker1.Update()

                        Else
                            MsgBox("Receipt number:" & txt1.Text & " already existed.", MsgBoxStyle.Information)

                        End If
                    Else
                        MsgBox("Null receipt date is not accepted..", MsgBoxStyle.Information)
                        DateTimePicker1.Focus()

                    End If

                Else
                    MsgBox("Null receipt reference is not accepted..", MsgBoxStyle.Information)
                    txt2.Focus()

                End If

            Else

                MsgBox("Null receipt number is not accepted..", MsgBoxStyle.Information)
                txt1.Focus()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        txtd.Text = DateTimePicker1.Text
        txt3.Focus()

    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            txt3.Focus()

        End If
    End Sub

    Private Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click
        Try
            Dim rn As Double
            If txt1.Text <> "" Then

                'Dim oradb As String = "Data Source=10.64.61.233:1521/orcl;User Id=07980;Password=8091;" 'example for connection string to monsoon aseer database
                Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                'Dim oradb As String = "Data Source=ORACLE:1521/orcl;User Id=10000;password=10111;"
                Dim conn As New OracleConnection(oradb)


                Dim cmd As New OracleCommand
                Dim cmd1 As New OracleCommand



                Dim ar1 As Integer



                ' cmd.CommandText = "insert into stock values(" + TextBox1.Text + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value + "," + DataGridView1.Rows(i).Cells(3).Value + "," + DataGridView1.Rows(i).Cells(4).Value + ",sysdate)"




                conn.Open()
                cmd1.Connection = conn
                For i As Integer = 0 To DataGridView1.Rows.Count - 1


                    cmd1.CommandText = "Delete from stock where R_N='" + txt1.Text + "'"
                    ar1 = cmd1.ExecuteNonQuery()
                Next
                conn.Close()


                MsgBox("Deleted Successfully", MsgBoxStyle.Information)
                '--------------------stock history
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
                conn.Open()
                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    cmd11.CommandText = "insert into stock_history values('" + txt1.Text + "','" + txt2.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value.ToString + "," + DataGridView1.Rows(i).Cells(3).Value + ",sysdate,'" + user_name + "','Delete Receipt')"
                    cmd11.ExecuteNonQuery()
                Next



                conn.Close()



                DataGridView1.Rows.Clear()


                rn = RN_ID()
                txt1.Text = rn
                btn3.Enabled = False
                btn2.Enabled = False
                btn1.Enabled = True

                txt2.Focus()



                txt2.Clear()
                DateTimePicker1.Update()
                txt4.Clear()
                txt5.Clear()
                txt6.Clear()
                txt2.Focus()





            End If





        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ReceiptInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReceiptInfoToolStripMenuItem.Click
        Info.Show()
    End Sub

    Private Sub ReceiptInfoToolStripMenuItem_MouseHover(sender As Object, e As EventArgs) Handles ReceiptInfoToolStripMenuItem.MouseHover
        'Info.Show()

    End Sub

    Private Sub ReceiptInfoToolStripMenuItem_MouseLeave(sender As Object, e As EventArgs) Handles ReceiptInfoToolStripMenuItem.MouseLeave
        'Info.Hide()

    End Sub

    Private Sub PrintRecieptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintRecieptToolStripMenuItem2.Click
        Try
            Dim ds As New DataSet2
            Dim dt As New DataTable
            dt = ds.Tables("PacketDataTable2")
            For i = 0 To DataGridView1.Rows.Count - 1
                dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value * DataGridView1.Rows(i).Cells(3).Value)
            Next

            With Print_Receipt.ReportViewer1.LocalReport
                '.ReportPath = "C:\Users\User\source\repos\System Manager\System Manager\stock.rdlc"
                .DataSources.Clear()
                .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("myDataSet1", dt))
            End With
            Print_Receipt.Show()
            Print_Receipt.ReportViewer1.Refresh()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub AddStoreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddStoreToolStripMenuItem.Click
        Add_Location.Show()

    End Sub

    Private Sub AddTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddTransferToolStripMenuItem.Click
        Add_transfer.Show()

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        rn = RN_ID()
        txt1.Text = rn
        btn3.Enabled = False
        btn2.Enabled = False
        btn1.Enabled = True

    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        txt6.Focus()

    End Sub
End Class