Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing
Public Class Add_transfer
    Dim indicator As Integer
    Dim position As String
    Public u_id As String = ""
    Public pw As String = ""
    Dim ip As String
    Dim oracle_sid As String = "orcl"
    Dim rn As Double
    Dim cmd_rn As New OracleCommand
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            Try


                indicator = 0

                'Dim u_id, pw, ip As String


                'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


                'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

                'ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")


                Label6.Text = ""

                Dim conn As New OracleConnection
                Dim cmd As New OracleCommand
                Dim cmd1 As New OracleCommand


                cmd.Connection = conn
                cmd.CommandText = "select * from transfer_manager where transfer_number=" & TextBox1.Text & ""


                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"


                conn.Open()
                Dim rd As OracleDataReader = cmd.ExecuteReader
                If rd.Read Then
                    TextBox1.Text = rd.GetValue(0).ToString
                    ComboBox1.Text = rd.GetValue(1).ToString
                    ComboBox2.Text = rd.GetValue(2).ToString
                    DateTimePicker1.Text = rd.GetValue(3).ToString
                    TextBox2.Text = rd.GetValue(4).ToString
                    TextBox1.Enabled = False
                    indicator = 1
                End If
                rd.Close()
                conn.Close()


                ComboBox1.Focus()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try


        End If

    End Sub

    Private Sub Add_transfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            cmd.CommandText = "select * from transfer_manager"
            Dim oraada As New OracleDataAdapter(cmd)
            Dim ds As New DataSet
            oraada.Fill(ds, "TRANSFER_MANAGER")
            DataGridView1.DataSource = ds.Tables(0)

            rn = RN_ID()
            TextBox1.Text = rn
            TextBox1.Focus()
            '-----------------------------------------------------
            Dim cmd_location As New OracleCommand



            cmd_location.Connection = conn
            cmd_location.CommandText = "select Location_name || '-' || location_tel from locations_manager"


            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"


            conn.Open()
            Dim rd_location As OracleDataReader = cmd_location.ExecuteReader
            While rd_location.Read
                ComboBox1.Items.Add(rd_location.GetValue(0))
                ComboBox2.Items.Add(rd_location.GetValue(0))
            End While
            rd_location.Close()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Function RN_ID() As Double

        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim RN As Double
        'Dim u_id, pw, ip As String


        ' u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


        'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

        ' ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")




        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"

        cmd.CommandText = "select max(t_s) from transfer_serial"
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
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            Try
                ' Dim u_id, pw, ip As String


                'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


                ' pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

                'ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")


                'Dim oradb As String = "Data Source=10.64.61.233:1521/orcl;User Id=07980;Password=8091;" 'example for connection string to monsoon aseer database
                Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                Dim conn As New OracleConnection(oradb)
                If indicator = 0 Then
                    '=======================================Save code
                    conn.Open()
                    cmd_RN.Connection = conn
                    cmd_RN.CommandText = "insert into transfer_serial values(" + TextBox1.Text + ",sysdate)"
                    cmd_RN.ExecuteNonQuery()
                    conn.Close()

                    Dim usr_insert As New OracleCommand

                    usr_insert.Connection = conn
                    usr_insert.CommandText = "insert into transfer_manager values(" & TextBox1.Text & ",'" & ComboBox1.Text & "','" & ComboBox2.Text & "','" & DateTimePicker1.Text & "','" & TextBox2.Text & "','pending')"



                    If TextBox1.Text <> "" Then
                        If ComboBox1.Text <> "" Then
                            If ComboBox2.Text <> "" Then

                                conn.Open()
                                Dim ins As Integer = usr_insert.ExecuteNonQuery()
                                conn.Close()

                                If ins > 0 Then
                                    Label6.ForeColor = Color.Green
                                    Label6.Text = "Transfer information added."
                                    TextBox1.Clear()
                                    TextBox2.Clear()
                                    ComboBox1.Text = ""
                                    ComboBox2.Text = ""
                                    DateTimePicker1.Refresh()
                                    rn = RN_ID()
                                    TextBox1.Text = rn
                                    TextBox1.Enabled = True
                                    TextBox1.Focus()



                                End If
                            End If
                        End If



                    End If
                End If
                If indicator = 1 Then
                    Dim usr_update As New OracleCommand


                    usr_update.Connection = conn
                    usr_update.CommandText = "update transfer_manager set transfer_number=" & TextBox1.Text & ",transfer_from='" & ComboBox1.Text & "',transfer_to='" & ComboBox2.Text & "',transfer_date='" & DateTimePicker1.Text & "',contact_person='" & TextBox2.Text & "'"

                    If TextBox1.Text <> "" Then
                        If ComboBox1.Text <> "" Then
                            If ComboBox2.Text <> "" Then

                                conn.Open()
                                Dim upd As Integer = usr_update.ExecuteNonQuery
                                conn.Close()

                                If upd > 0 Then
                                    Label6.ForeColor = Color.Green
                                    Label6.Text = "Transfer information updated."
                                    TextBox1.Clear()
                                    TextBox2.Clear()
                                    ComboBox1.Text = ""
                                    ComboBox2.Text = ""
                                    DateTimePicker1.Refresh()
                                    rn = RN_ID()
                                    TextBox1.Text = rn
                                    TextBox1.Enabled = True
                                    TextBox1.Focus()

                                End If

                            End If
                        End If
                    End If


                End If
                'Fill datagridview with users data
                ' Dim oradb As String = "Data Source=" + ip + ":1521/orcl;User Id=" + u_id + ";password=" + pw + ";"
                'Dim conn As New OracleConnection(oradb)

                Dim cmd As New OracleCommand
                cmd.Connection = conn
                cmd.CommandText = "select * from transfer_manager"
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "TRANSFER_MANAGER")
                DataGridView1.DataSource = ds.Tables(0)
            Catch ex As Exception
                MessageBox.Show(ex.Message)

            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim conn As New OracleConnection(oradb)
            Dim user_del As New OracleCommand
            user_del.Connection = conn
            user_del.CommandText = "delete from transfer_manager where transfer_number='" + TextBox1.Text + "'"

            conn.Open()
            Dim ins As Integer = user_del.ExecuteNonQuery()
            conn.Close()
            If ins > 0 Then
                Label6.ForeColor = Color.Green

                Label6.Text = "Transfer has been deleted"
                TextBox1.Clear()
                TextBox2.Clear()
                ComboBox1.Text = ""
                ComboBox2.Text = ""
                DateTimePicker1.Refresh()
                rn = RN_ID()
                TextBox1.Text = rn
                TextBox1.Enabled = True
                TextBox1.Focus()

            End If

            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.CommandText = "select * from transfer_manager"
            Dim oraada As New OracleDataAdapter(cmd)
            Dim ds As New DataSet
            oraada.Fill(ds, "transfer_MANAGER")
            DataGridView1.DataSource = ds.Tables(0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox2.Focus()
        End If

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            DateTimePicker1.Focus()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        DateTimePicker1.Refresh()
        rn = RN_ID()
        TextBox1.Text = rn
        TextBox1.Enabled = True
        TextBox1.Focus()
    End Sub
End Class