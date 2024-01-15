Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing

Public Class Login
    Public u_id As String = ""
    Public pw As String = ""
    Public POS_USER As String
    Public position As Integer
    Dim ip As String
    Dim increment As Integer = 0

    Dim A As String = System.DateTime.Now.Day
    Dim B As String = System.DateTime.Now.Month
    Dim C As String = System.DateTime.Now.Year
    Dim L_K As String

    Dim cpu_id As String = CpuId()
    Dim conn As New OracleConnection
    Dim oracle_sid As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Dim u_id, pw, ip As String


            'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


            'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

            ' ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")


            Dim password As String = ""
            Dim EN As String = ""

            Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim conn As New OracleConnection(oradb)
            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.CommandText = "select pw_acs_em,NM_EM,id_gp_wrk from pa_em where id_login = '" + TextBox1.Text + "'"
            Dim cmd1 As New OracleCommand

            Try
                conn.Open()
                Dim rd As OracleDataReader = cmd.ExecuteReader
                If rd.Read Then

                    password = rd.GetValue(0)
                    EN = rd.GetValue(1)
                    position = rd.GetValue(2)
                    POS_USER = TextBox1.Text

                End If
                rd.Close()
                conn.Close()

                ' PasswordTextBox.Text = GetHash(PasswordTextBox.Text)

                If password.ToString = GetHash(TextBox2.Text) Then
                    If position = 8 Then
                        MMenu.Show()
                        MMenu.pos = 8
                        Me.Close()

                        Dim Ldate As String
                        Ldate = System.DateTime.Today.Year
                        'Dim sn

                        Dim cmd2 As New OracleCommand
                        cmd2.Connection = conn
                        cmd2.CommandText = "select sn from serial"
                        conn.Open()
                        Dim rd1 As OracleDataReader = cmd2.ExecuteReader

                        ' If rd1.Read Then
                        'sn = rd1.GetValue(0)
                        'If sn = (GetHash(Ldate)) Then

                        ' Else
                        ' MessageBox.Show("Your license has been expired,Please contact the vendor.")

                        ' End If

                        'End If

                        rd1.Close()
                        conn.Close()





                        conn.Open()
                        cmd1.Connection = conn
                        cmd1.CommandText = "insert into log_in(user_name,user_id,log_time) values('" + EN + "','" + TextBox1.Text + "',sysdate)"
                        cmd1.ExecuteNonQuery()
                        conn.Close()
                        Me.Close()
                    ElseIf position = 6 Or position = 2 Then
                        MMenu.Show()
                        MMenu.PictureBox1.Enabled = False
                        MMenu.PictureBox2.Enabled = False
                        MMenu.PictureBox3.Enabled = False
                        MMenu.PictureBox4.Enabled = False
                        MMenu.PictureBox5.Enabled = False
                        MMenu.PictureBox6.Enabled = False
                        Me.Close()


                    Else

                        MsgBox("Only system administrator or store manager allow to access..", MsgBoxStyle.Information)
                    End If



                Else

                    MsgBox("Wrong User & Password..!", MsgBoxStyle.Critical)
                    TextBox1.Focus()

                End If


                '------------------------------------application license.





            Catch ex As Exception

            End Try




            'Add_Prices.Show()
            'Me.Close()

        Catch ex As Exception

        End Try
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

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            ' Dim u_id, pw, ip As String
            ' u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


            ' pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

            ' ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")

            Dim readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\DBHOST", "DBHOSTKeyValue", Nothing)

            'MessageBox.Show(readValue.ToString)
            If readValue.ToString <> "" Then
                ip = readValue.ToString

            Else
                MsgBox("Please update server host name...", MsgBoxStyle.Information)
            End If
            '-------------------
            Dim readValue2 = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SID", "POSKeyValue", Nothing)

            'MessageBox.Show(readValue.ToString)
            If readValue2.ToString <> "" Then
                oracle_sid = readValue2.ToString

            Else
                MsgBox("Please update DB SID...", MsgBoxStyle.Information)
            End If
            '------------------------------------------
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
            '----------------------------
            Dim L As Integer = 0
            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            '-------------------------------------
            Dim anyn1 As String = ""
            Dim cmd1 As New OracleCommand
            cmd1.CommandText = "select to_Date(sysdate,'dd-MON-yy') from dual"
            cmd1.Connection = conn
            conn.Open()
            Dim rd1 As OracleDataReader = cmd1.ExecuteReader
            If rd1.Read Then
                anyn1 = rd1.GetValue(0).ToString


            End If
            rd1.Close()
            conn.Close()
            anyn1 = GetHash(anyn1)
            '---------------------------------------------------
            Dim cmd_l As New OracleCommand
            cmd_l.Connection = conn
            cmd_l.CommandText = "select * from serial"
            conn.Open()

            Dim rd As OracleDataReader = cmd_l.ExecuteReader
            While rd.Read
                L_K = rd.GetValue(0)
                Dim D As String = C & B & A
                'LK.TextBox1.Text = GetHash(C & cpu_id).Substring(20)
                If GetHash(cpu_id).Substring(20) = L_K Then

                    L = 1



                    If Convert.ToDecimal(D) > 201811 Then

                    Else
                        'MsgBox("Please correct your system date and time..", MsgBoxStyle.Information)
                    End If

                ElseIf anyn1 = L_K Then
                    L = 1






                Else
                    'MsgBox(" Please update your License key..", MsgBoxStyle.Information)
                    'Process.Start("https://abdulmoiz1984.wixsite.com/website")
                    'Me.Close()



                End If

            End While

            rd.Close()
            conn.Close()
            If L = 0 Then
                MsgBox(" Please update your License key..", MsgBoxStyle.Information)
                'Process.Start("https://abdulmoiz1984.wixsite.com/website")
                Me.Close()
            End If




            Dim cmd As New OracleCommand



            cmd.Connection = conn
            cmd.CommandText = "delete from inventory_temp"


            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"

            conn.Open()
            cmd.ExecuteNonQuery()

            conn.Close()

            TextBox1.Focus()
        Catch ex As Exception
            'MessageBox.Show("1")
        End Try
    End Sub
    Private Function CpuId() As String
        Dim computer As String = "."
        Dim wmi As Object = GetObject("winmgmts:" &
            "{impersonationLevel=impersonate}!\\" &
            computer & "\root\cimv2")
        Dim processors As Object = wmi.ExecQuery("Select * from " &
            "Win32_Processor")

        Dim cpu_ids As String = ""
        For Each cpu As Object In processors
            cpu_ids = cpu_ids & ", " & cpu.ProcessorId
        Next cpu
        If cpu_ids.Length > 0 Then cpu_ids =
            cpu_ids.Substring(2)

        Return cpu_ids
    End Function

    Private Sub Login_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        Try
            Dim g As Graphics = e.Graphics
            Dim pen As New Pen(Color.Blue, 4.0)


            g.DrawRectangle(pen, New Rectangle(TextBox1.Location, TextBox1.Size))
            g.DrawRectangle(pen, New Rectangle(TextBox2.Location, TextBox2.Size))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox2.Focus()
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Button1.PerformClick()
            e.Handled = True
        End If
    End Sub
End Class