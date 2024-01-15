Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing
Public Class Add_User
    Dim indicator As Integer
    Dim position As String
    Public u_id As String = ""
    Public pw As String = ""
    Dim ip As String
    Dim oracle_sid As String
    Private Sub Add_User_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            TextBox3.Focus()

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
            cmd.CommandText = "select id_login as Employee_ID ,pw_acs_em as Password ,FN_EM||' '||LN_EM as Name from pa_em"
            Dim oraada As New OracleDataAdapter(cmd)
            Dim ds As New DataSet
            oraada.Fill(ds, "PA_EM")
            DataGridView1.DataSource = ds.Tables(0)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Add_User_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MMenu.Show()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click, Label6.Click, Label5.Click, Label4.Click, Label3.Click, Label2.Click

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            Try


                indicator = 0

                'Dim u_id, pw, ip As String


                'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


                'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

                'ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")


                Label6.Text = ""
                TextBox2.Focus()
                Dim conn As New OracleConnection
                Dim cmd As New OracleCommand
                Dim cmd1 As New OracleCommand


                cmd.Connection = conn
                cmd.CommandText = "select FN_em,ln_em,id_gp_wrk from pa_em where id_login  = '" + TextBox3.Text.Trim + "'"


                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"


                conn.Open()
                Dim rd As OracleDataReader = cmd.ExecuteReader
                If rd.Read Then
                    TextBox1.Text = rd.GetValue(0).ToString
                    TextBox2.Text = rd.GetValue(1).ToString
                    position = rd.GetValue(2).ToString


                    TextBox3.Enabled = False
                    indicator = 1


                End If
                rd.Close()


                If position = 6 Then
                    ComboBox1.Text = "Store Manager"
                ElseIf position = 8 Then
                    ComboBox1.Text = "System administrator"
                ElseIf position = 2 Then
                    ComboBox1.Text = "Cashier"
                ElseIf position = 5 Then
                    ComboBox1.Text = "Sales Person"

                End If



                conn.Close()



            Catch ex As Exception
                MessageBox.Show(ex.Message)

            End Try
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
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


                    Dim usr_insert As New OracleCommand
                    Dim position As Integer
                    If ComboBox1.Text = "Store Manager" Then
                        position = 6
                    ElseIf ComboBox1.Text = "Cashier" Then
                        position = 2
                    ElseIf ComboBox1.Text = "Sales Person" Then
                        position = 5
                    ElseIf ComboBox1.Text = "System administrator" Then
                        position = 8


                    End If
                    usr_insert.Connection = conn
                    usr_insert.CommandText = "INSERT INTO PA_EM (ID_EM, ID_PRTY, ID_LOGIN, ID_ALT, PW_ACS_EM, NM_EM, LN_EM,FN_EM, MD_EM, UN_NMB_SCL_SCTY, SC_EM, ID_GP_WRK, LCL, NUMB_DYS_VLD, TYPE_EMP, FL_PW_NW_REQ, TS_CRT_PW, NUMB_FLD_PW) VALUES('" + TextBox3.Text + "', '1', '" + TextBox3.Text + "', '" + TextBox3.Text + "', '" + GetHash(TextBox4.Text) + "', '" + TextBox1.Text + "','" + TextBox2.Text + "', '" + TextBox1.Text + "', '', '736352826', '1', '" + position.ToString + "', 'en_US', '0', '0', '1', TO_TIMESTAMP('24-SEP-13 09.08.56.741000000 AM', 'DD-MON-RR HH.MI.SS.FF AM'), '0')"



                    If TextBox1.Text <> "" Then
                        If TextBox2.Text <> "" Then
                            If TextBox4.Text <> "" Then
                                If ComboBox1.Text <> "" Then
                                    conn.Open()
                                    Dim ins As Integer = usr_insert.ExecuteNonQuery()
                                    conn.Close()

                                    If ins > 0 Then
                                        Label6.ForeColor = Color.Green

                                        Label6.Text = "User has been inserted"
                                        TextBox1.Clear()
                                        TextBox2.Clear()
                                        TextBox3.Clear()
                                        TextBox4.Clear()
                                        TextBox3.Enabled = True
                                        ComboBox1.Text = ""
                                        TextBox3.Focus()
                                    End If

                                End If
                            End If
                        End If
                    End If



                End If

                If indicator = 1 Then
                    Dim usr_update As New OracleCommand
                    Dim position As Integer
                    If ComboBox1.Text = "Store Manager" Then
                        position = 6
                    ElseIf ComboBox1.Text = "Cashier" Then
                        position = 2
                    ElseIf ComboBox1.Text = "Sales Person" Then
                        position = 5
                    ElseIf ComboBox1.Text = "System administrator" Then
                        position = 8


                    End If

                    usr_update.Connection = conn
                    usr_update.CommandText = "update pa_em set pw_acs_em = '" + GetHash(TextBox4.Text) + "' ,ln_em = '" + TextBox2.Text + "', fn_em = '" + TextBox1.Text + "',id_gp_wrk = '" + position.ToString + "' where id_login = '" + TextBox3.Text + "'"

                    If TextBox1.Text <> "" Then
                        If TextBox2.Text <> "" Then
                            If TextBox4.Text <> "" Then
                                If ComboBox1.Text <> "" Then
                                    conn.Open()
                                    Dim upd As Integer = usr_update.ExecuteNonQuery
                                    conn.Close()

                                    If upd > 0 Then
                                        Label6.ForeColor = Color.Green
                                        Label6.Text = "User information updated."
                                        TextBox1.Clear()
                                        TextBox2.Clear()
                                        TextBox3.Clear()
                                        TextBox4.Clear()
                                        TextBox3.Enabled = True
                                        ComboBox1.Text = ""

                                        TextBox3.Focus()
                                    End If
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
                cmd.CommandText = "select id_login as Employee_ID ,pw_acs_em as Password ,LN_EM||' '||FN_EM as Name from pa_em"
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "PA_EM")
                DataGridView1.DataSource = ds.Tables(0)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
    Private Shared Function GetHash(ByVal pwd As String) As String

        Using managed As SHA1Managed = New SHA1Managed
            Dim buffer As Byte() = managed.ComputeHash(Encoding.UTF8.GetBytes(pwd))
            Dim builder As New StringBuilder((buffer.Length * 2))
            Dim num As Byte
            For Each num In buffer
                builder.Append(num.ToString("x2"))
            Next
            Return builder.ToString
        End Using
    End Function
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox3.Enabled = True
        ComboBox1.Text = ""

        TextBox3.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim conn As New OracleConnection(oradb)
            Dim user_del As New OracleCommand
            user_del.Connection = conn
            user_del.CommandText = "delete from pa_em where id_login='" + TextBox3.Text + "'"

            conn.Open()
            Dim ins As Integer = user_del.ExecuteNonQuery()
            conn.Close()
            If ins > 0 Then
                Label6.ForeColor = Color.Green

                Label6.Text = "User has been deleted"
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
                TextBox3.Enabled = True
                ComboBox1.Text = ""
                TextBox3.Focus()
            End If
            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.CommandText = "select id_login as Employee_ID ,pw_acs_em as Password ,LN_EM||' '||FN_EM as Name from pa_em"
            Dim oraada As New OracleDataAdapter(cmd)
            Dim ds As New DataSet
            oraada.Fill(ds, "PA_EM")
            DataGridView1.DataSource = ds.Tables(0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            TextBox1.Focus()

        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            ComboBox1.Focus()

        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            TextBox4.Focus()

        End If
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub
End Class