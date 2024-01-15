Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO.Ports
Imports System.Management
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types


Public Class Form1
    Dim increment As Integer = 0

    Dim A As String = System.DateTime.Now.Day
    Dim B As String = System.DateTime.Now.Month
    Dim C As String = System.DateTime.Now.Year
    Dim L_K As String
    Dim u_id As String = ""
    Dim pw As String = ""
    Dim ip As String
    Dim cpu_id As String = CpuId()
    Dim conn As New OracleConnection
    Dim ws_id As String
    Dim oracle_sid As String








    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

    End Sub

    Private Sub Form1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
            '''''''''''''''''''''''''''''''''''''
            Dim readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\DBHOST", "DBHOSTKeyValue", Nothing)

            If readValue.ToString <> "" Then
                ip = readValue.ToString

            Else
                MessageBox.Show("Please update server host name...")
            End If

            Dim readValue1 = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\POS", "POSKeyValue", Nothing)
            If readValue1.ToString <> "" Then
                Dim DQ As String = Chr(34)
                Dim SC As String
                Dim PSW As Double
                SC = readValue1.ToString

                PSW = Convert.ToDouble(SC) + 111
                Dim DB_password As String = PSW.ToString

                SC = SC


                POS_Screen.u_id = SC
                POS_Screen.pw = DB_password
                LK.u_id = SC
                LK.pw = DB_password
                u_id = SC
                pw = DB_password


            End If


            '''''''''''''''''''''''''''''''''''''



            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"


            '-----------------------
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
            '-----------------------
            Dim L As Integer = 0
            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.CommandText = "select * from serial"
            conn.Open()

            Dim rd As OracleDataReader = cmd.ExecuteReader
            While rd.Read
                L_K = rd.GetValue(0)
                Dim D As String = C & B & A
                'LK.TextBox1.Text = GetHash(C & cpu_id).Substring(20)
                If GetHash(cpu_id).Substring(20) = L_K Then

                    L = 1

                    POS_Screen.ws_id = rd.GetValue(1)


                    If Convert.ToDecimal(D) > 201811 Then
                        Timer1.Enabled = True

                        Timer1.Start()
                    Else
                        'MessageBox.Show("Please correct your system date and time..")
                    End If
                ElseIf anyn1 = L_K Then
                    L = 1
                    POS_Screen.ws_id = rd.GetValue(1)


                    If Convert.ToDecimal(D) > 201811 Then
                        Timer1.Enabled = True

                        Timer1.Start()
                    Else
                        'MessageBox.Show("Please correct your system date and time..")
                    End If

                Else
                    ' MessageBox.Show("Please update your License key..")

                    'LK.Show()
                    'Me.Close()


                End If




            End While
            rd.Close()
            conn.Close()

            If L = 0 Then
                MsgBox(" Please update your License key..", MsgBoxStyle.Information)
                'Process.Start("https://abdulmoiz1984.wixsite.com/website")


                LK.Show()
                Me.Close()
            End If
           
            Label1.Text = "Copyright" & Chr(169) & "2018,All rights reserved."



        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try





            ProgressBar1.PerformStep()
            If ProgressBar1.Value = 10 Then
                Timer1.Enabled = False
                Timer1.Stop()
                POS_Screen.Show()
            End If








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

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub
End Class