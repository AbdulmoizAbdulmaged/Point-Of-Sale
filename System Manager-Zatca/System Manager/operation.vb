Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing
Imports Microsoft.Reporting.WinForms
Public Class Operation
    Public u_id As String = ""
    Public pw As String = ""
    Dim ip As String
    Dim oracle_sid As String
    Dim rn As Double
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Operation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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


            rn = RN_ID()
            txt1.Text = rn
            txt1.Focus()
            ' ComboBox1.Focus()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Function RN_ID() As Double

        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim RN As Double


        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"

        cmd.CommandText = "select max(ops) from ops_fees_serial"
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

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        txtd.Text = DateTimePicker1.Value.ToString("dd-MM-yyyy")
        txt3.Focus()
    End Sub

    Private Sub txt3_TextChanged(sender As Object, e As EventArgs) Handles txt3.TextChanged

    End Sub

    Private Sub txt3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt3.KeyPress
        Dim txt As TextBox = CType(sender, TextBox)

        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True

        If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

        If e.KeyChar = "." And txt.Text.IndexOf(".") = -1 Then e.Handled = False 'allow single decimal point

        If e.KeyChar = "-" And txt.SelectionStart = 0 Then e.Handled = False 'allow negative number
        If e.KeyChar = Convert.ToChar(13) Then


            e.Handled = True
            Try


                If txt3.Text <> "" Then
                    If txt3.Text <> "" Then
                        DataGridView1.Rows.Add(txt1.Text, ComboBox1.Text, RichTextBox1.Text, txt2.Text, txtd.Text, txt3.Text)

                        txt3.Enabled = True
                        txt3.Clear()


                        txt3.Focus()


                    Else
                        MsgBox("Null Quantity is not accepted..", MsgBoxStyle.Information)
                        txt3.Focus()

                    End If


                End If
                btn1.Focus()
            Catch ex As Exception
                MessageBox.Show(ex.Message)

            End Try

        End If
    End Sub

    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        Try


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






                    ' cmd.CommandText = "insert into stock values(" + TextBox1.Text + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value + "," + DataGridView1.Rows(i).Cells(3).Value + "," + DataGridView1.Rows(i).Cells(4).Value + ",sysdate)"

                    'DateTimePicker1.Value.ToShortDateString("dd-MM-yyyy") change date to string.

                    conn.Open()
                    cmd.Connection = conn
                    cmd.CommandText = "insert into ops_fees_serial values(" + txt1.Text + ",sysdate)"
                    Dim ar As Integer = cmd.ExecuteNonQuery()
                    conn.Close()
                    If ar > 0 Then
                        conn.Open()
                        cmd1.Connection = conn
                        For i As Integer = 0 To DataGridView1.Rows.Count - 1


                            cmd1.CommandText = "insert into ops_fees values('" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "','" + DataGridView1.Rows(i).Cells(2).Value + "','" + DataGridView1.Rows(i).Cells(3).Value + "','" + DataGridView1.Rows(i).Cells(4).Value + "','" + DataGridView1.Rows(i).Cells(5).Value + "',sysdate,'" + user_name + "')"
                            ar1 = cmd1.ExecuteNonQuery()
                        Next
                        conn.Close()

                        If ar1 > 0 Then
                            MsgBox("Added Successfully", MsgBoxStyle.Information)

                            DataGridView1.Rows.Clear()


                            rn = RN_ID()
                            txt1.Text = rn
                            btn3.Enabled = False
                            btn2.Enabled = False
                            btn1.Enabled = True

                            ComboBox1.Focus()





                            RichTextBox1.Clear()
                            txtd.Clear()
                            txt2.Clear()
                            txt3.Clear()

                        End If





                    End If
                Else
                    MsgBox("Please enter all required data.")
                    txt1.Focus()
                End If

            End If






        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

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
            cmd.CommandText = "select * from ops_fees where bill_number = '" + txt1.Text + "'"

            '  Dim adapter As New OracleDataAdapter(cmd)
            ' Dim ds As New DataSet
            'adapter.Fill(ds, "stock")
            'DataGridView1.DataSource = ds.Tables(0)
            Dim rd As OracleDataReader = cmd.ExecuteReader
            DataGridView1.Rows.Clear()

            While rd.Read

                'TextBox1.Text = rd.GetValue(0)

                DataGridView1.Rows.Add(rd.GetValue(0), rd.GetValue(1), rd.GetValue(2), rd.GetValue(3), rd.GetValue(4), rd.GetValue(5))
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




            ElseIf txt1.Text = rn Then
                btn3.Enabled = False
                btn2.Enabled = False
                btn1.Enabled = True

            Else
                btn3.Enabled = False
                btn2.Enabled = False
                btn1.Enabled = False

            End If

        Catch ex As Exception

            'MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click
        Try


            If DataGridView1.Rows.Count > 0 Then





                'Dim oradb As String = "Data Source=10.64.61.233:1521/orcl;User Id=07980;Password=8091;" 'example for connection string to monsoon aseer database
                Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                'Dim oradb As String = "Data Source=ORACLE:1521/orcl;User Id=10000;password=10111;"
                Dim conn As New OracleConnection(oradb)





                Dim cmd As New OracleCommand
                Dim cmd1 As New OracleCommand



                Dim ar1 As Integer


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


                conn.Open()
                cmd.Connection = conn
                cmd.CommandText = "delete from ops_fees where bill_number='" + txt1.Text + "'"
                Dim ar As Integer = cmd.ExecuteNonQuery()
                conn.Close()



                ' cmd.CommandText = "insert into stock values(" + TextBox1.Text + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value + "," + DataGridView1.Rows(i).Cells(3).Value + "," + DataGridView1.Rows(i).Cells(4).Value + ",sysdate)"

                'DateTimePicker1.Value.ToShortDateString("dd-MM-yyyy") change date to string.

                If ar > 0 Then
                    conn.Open()
                    cmd1.Connection = conn
                    For i As Integer = 0 To DataGridView1.Rows.Count - 1


                        cmd1.CommandText = "insert into ops_fees values('" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "','" + DataGridView1.Rows(i).Cells(2).Value + "','" + DataGridView1.Rows(i).Cells(3).Value + "','" + DataGridView1.Rows(i).Cells(4).Value + "','" + DataGridView1.Rows(i).Cells(5).Value + "',sysdate,'" + user_name + "')"
                        ar1 = cmd1.ExecuteNonQuery()
                    Next
                    conn.Close()

                    If ar1 > 0 Then
                        MsgBox("Updated Successfully", MsgBoxStyle.Information)

                        DataGridView1.Rows.Clear()


                        rn = RN_ID()
                        txt1.Text = rn
                        btn3.Enabled = False
                        btn2.Enabled = False
                        btn1.Enabled = True

                        ComboBox1.Focus()





                        RichTextBox1.Clear()
                        txtd.Clear()
                        txt2.Clear()
                        txt3.Clear()

                    End If




                End If
            Else
                MsgBox("Please enter all required data.")
                txt1.Focus()


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click
        Try
            If txt1.Text <> "" Then



                If DataGridView1.Rows.Count > 0 Then





                    'Dim oradb As String = "Data Source=10.64.61.233:1521/orcl;User Id=07980;Password=8091;" 'example for connection string to monsoon aseer database
                    Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                    'Dim oradb As String = "Data Source=ORACLE:1521/orcl;User Id=10000;password=10111;"
                    Dim conn As New OracleConnection(oradb)





                    Dim cmd As New OracleCommand
                    Dim cmd1 As New OracleCommand



                    Dim ar1 As Integer


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


                    conn.Open()
                    cmd.Connection = conn
                    cmd.CommandText = "delete from ops_fees where bill_number='" + txt1.Text + "'"
                    Dim ar As Integer = cmd.ExecuteNonQuery()
                    conn.Close()



                    ' cmd.CommandText = "insert into stock values(" + TextBox1.Text + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + DataGridView1.Rows(i).Cells(0).Value + "','" + DataGridView1.Rows(i).Cells(1).Value + "'," + DataGridView1.Rows(i).Cells(2).Value + "," + DataGridView1.Rows(i).Cells(3).Value + "," + DataGridView1.Rows(i).Cells(4).Value + ",sysdate)"

                    'DateTimePicker1.Value.ToShortDateString("dd-MM-yyyy") change date to string.

                    If ar > 0 Then



                        MsgBox("Deleted Successfully", MsgBoxStyle.Information)

                        DataGridView1.Rows.Clear()


                            rn = RN_ID()
                            txt1.Text = rn
                            btn3.Enabled = False
                            btn2.Enabled = False
                            btn1.Enabled = True

                            ComboBox1.Focus()





                            RichTextBox1.Clear()
                            txtd.Clear()
                            txt2.Clear()
                            txt3.Clear()






                    End If

                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        rn = RN_ID()
        txt1.Text = rn
        btn3.Enabled = False
        btn2.Enabled = False
        btn1.Enabled = True

        txt1.Focus()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            RichTextBox1.Focus()

        End If
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub RichTextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles RichTextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            txt2.Focus()

        End If
    End Sub

    Private Sub txt2_TextChanged(sender As Object, e As EventArgs) Handles txt2.TextChanged

    End Sub

    Private Sub txt2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            DateTimePicker1.Focus()

        End If
    End Sub

    Private Sub txt1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            ComboBox1.Focus()

        End If
    End Sub
End Class