Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO.Ports
Imports System.Management
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types

Public Class LK
    Dim A As String = System.DateTime.Now.Year
    Dim L_K As String
    Public u_id As String = ""
    Public pw As String = ""
    Dim ip As String
    Dim cpu_id As String
    Dim oracle_sid As String

    Dim conn As New Oracle.ManagedDataAccess.Client.OracleConnection


    Private Sub LK_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            TextBox1.Focus()

            '-------------------
            Dim readValue2 = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SID", "POSKeyValue", Nothing)

            'MessageBox.Show(readValue.ToString)
            If readValue2.ToString <> "" Then
                oracle_sid = readValue2.ToString

            Else
                MsgBox("Please update DB SID...", MsgBoxStyle.Information)
            End If
            '------------------------------------------
            '''''''''''''''''''''''''''''''''''''
            Dim readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\DBHOST", "DBHOSTKeyValue", Nothing)

            If readValue.ToString <> "" Then
                ip = readValue.ToString

            Else
                MessageBox.Show("Please update server host name...")
            End If
        

            '''''''''''''''''''''''''''''''''''''
            cpu_id = CpuId()
            ' MessageBox.Show(A & cpu_id)
            TextBox1.Focus()
            TextBox2.Text = cpu_id

            '-------------------------------------
            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim cmd As New OracleCommand
            Dim flag As String = ""
            cmd.Connection = conn
            cmd.CommandText = "select  T_T_Flag from T_T"
            conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader
            If rd.Read Then
                flag = rd.GetValue(0).ToString
            End If
            rd.Close()
            conn.Close()
            If flag = "1" Then
                RadioButton1.Enabled = False
                RadioButton2.Checked = True
                TextBox1.Focus()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try


            'Form1.Close()
            Me.Close()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If RadioButton1.Checked = True Then
                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                Dim cmd As New OracleCommand
                Dim anyn As String = ""
                cmd.Connection = conn
                cmd.CommandText = "select T_T_flag from T_T"
                conn.Open()
                Dim rd As OracleDataReader = cmd.ExecuteReader


                If rd.Read Then
                    anyn = rd.GetValue(0).ToString
                End If
                rd.Close()
                conn.Close()
                If anyn = "1984" Then
                    Dim sysdate As String
                    Dim m As Integer
                    Dim sum As Double = 0


                    For i = 0 To 9


                        sysdate = "sysdate + " & i




                        Dim anyn1 As String = ""
                        Dim cmd1 As New OracleCommand
                        cmd1.CommandText = "select to_Date(" & sysdate & ",'dd-MON-yy') from dual"
                        cmd1.Connection = conn
                        conn.Open()
                        Dim rd1 As OracleDataReader = cmd1.ExecuteReader
                        If rd1.Read Then
                            anyn1 = rd1.GetValue(0).ToString


                        End If
                        rd1.Close()
                        conn.Close()
                        anyn1 = GetHash(anyn1)

                        '--------------------------
                        Dim ws_cmd As New OracleCommand
                        Dim ws As Integer = 0

                        ws_cmd.Connection = conn
                        ws_cmd.CommandText = "select max(ws) from serial"

                        conn.Open()

                        Dim rd_ws As OracleDataReader = ws_cmd.ExecuteReader

                        If rd_ws.Read Then
                            If rd_ws.GetValue(0).ToString <> "" Then


                                ws = rd_ws.GetValue(0)
                                ws = ws + 1

                            Else
                                ws = 1

                            End If

                        End If


                        rd_ws.Close()
                        conn.Close()


                        Dim cmd3 As New OracleCommand
                        cmd3.Connection = conn
                        cmd3.CommandText = "insert into serial values('" + anyn1 + "','1','2')"
                        conn.Open()
                        m = cmd3.ExecuteNonQuery()
                        sum = sum + m

                        conn.Close()

                        If sum = 10 Then
                            Dim cmd4 As New OracleCommand
                            cmd4.Connection = conn
                            cmd4.CommandText = "update T_T set T_T_flag='1'"

                            conn.Open()
                            cmd4.ExecuteNonQuery()
                            conn.Close()
                            Form1.Show()

                        End If

                    Next
                    Me.Close()
                End If


            End If

            If RadioButton2.Checked = True Then


                L_K = TextBox1.Text
                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                Dim ws_cmd As New OracleCommand
                Dim ws As Integer = 0

                ws_cmd.Connection = conn
                ws_cmd.CommandText = "select max(ws) from serial"

                conn.Open()

                Dim rd_ws As OracleDataReader = ws_cmd.ExecuteReader

                If rd_ws.Read Then
                    If rd_ws.GetValue(0).ToString <> "" Then


                        ws = rd_ws.GetValue(0)
                        ws = ws + 1

                    Else
                        ws = 1

                    End If

                End If


                rd_ws.Close()
                conn.Close()
                If GetHash(cpu_id).Substring(20) = L_K Then

                    Dim cmd_del As New OracleCommand
                    cmd_del.Connection = conn
                    cmd_del.CommandText = "delete from serial"
                    conn.Open()
                    'cmd_del.ExecuteNonQuery()
                    conn.Close()

                    Dim cmd As New OracleCommand
                    cmd.Connection = conn
                    cmd.CommandText = "insert into serial values('" + TextBox1.Text + "'," + ws.ToString + ",'2')"
                    conn.Open()
                    Dim i As Integer = cmd.ExecuteNonQuery()
                    conn.Close()

                    If i > 0 Then

                        Form1.Show()
                        Me.Close()
                        'MessageBox.Show("License key has been seccessfully updated..")
                    End If

                Else

                    MsgBox("License key is not valid,Please contact the Vendor for support.", MsgBoxStyle.Information)
                    'Process.Start("https://abdulmoiz1984.wixsite.com/website")
                    TextBox1.Clear()
                    TextBox1.Focus()



                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function CpuId() As String
        Dim computer As String = "."
        Dim wmi As Object = GetObject("winmgmts:" & _
            "{impersonationLevel=impersonate}!\\" & _
            computer & "\root\cimv2")
        Dim processors As Object = wmi.ExecQuery("Select * from " & _
            "Win32_Processor")

        Dim cpu_ids As String = ""
        For Each cpu As Object In processors
            cpu_ids = cpu_ids & ", " & cpu.ProcessorId
        Next cpu
        If cpu_ids.Length > 0 Then cpu_ids = _
            cpu_ids.Substring(2)

        Return cpu_ids
    End Function

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            Panel1.Enabled = True

            TextBox1.Focus()


        End If

        If RadioButton1.Checked = True Then
            Panel1.Enabled = False

        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton2.Checked = True Then
            Panel1.Enabled = True
            TextBox1.Focus()

        End If

        If RadioButton1.Checked = True Then
            Panel1.Enabled = False

        End If
    End Sub
End Class