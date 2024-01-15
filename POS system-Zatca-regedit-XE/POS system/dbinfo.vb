Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.IO
Imports IWshRuntimeLibrary
Imports Microsoft.Win32



Public Class dbinfo

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub dbinfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
          


            MaskedTextBox1.Text = "system"
            
            MaskedTextBox3.Text = System.Windows.Forms.SystemInformation.ComputerName
            RadioButton1.Checked = True


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            If MaskedTextBox6.Text <> "" Then
                If MaskedTextBox6.Text.Length = 5 Then


                    Dim conn As New OracleConnection


                    Dim conn_string As String
                    '"Data Source=" + ip + ":1521/orcl;User Id=" + u_id + ";password=" + pw + ";"
                    conn_string = "Data Source=" + MaskedTextBox3.Text + ":1521/" + MaskedTextBox5.Text + ";User Id=" + MaskedTextBox1.Text + ";password=" + MaskedTextBox2.Text + ";"
                    conn.ConnectionString = conn_string


                    conn.Open()



                    If conn.State = ConnectionState.Open Then

                        Button4.Enabled = True
                        MaskedTextBox1.Enabled = False
                        MaskedTextBox2.Enabled = False
                        MaskedTextBox3.Enabled = False
                        MsgBox("Database connection has been created successfully", MsgBoxStyle.Information)
                        Button4.Focus()

                        conn.Close()
                    Else
                        MsgBox("Faild to create database connection", MsgBoxStyle.Information)

                    End If




                Else

                    MsgBox("Store Code must be 5 Digits", MsgBoxStyle.Critical)
                    MaskedTextBox6.Focus()


                End If
            Else

                MsgBox("Store Code is missing", MsgBoxStyle.Critical)
                MaskedTextBox6.Focus()
            End If




        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)

        End Try
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            ' If RadioButton1.Checked = True Then
            '
            '
            '     My.Computer.Registry.CurrentUser.CreateSubKey("DBHOST")
            '     My.Computer.Registry.SetValue("HKEY_CURRENT_USER\DBHOST", "DBHOSTKeyValue", MaskedTextBox3.Text)
            '     My.Computer.Registry.CurrentUser.CreateSubKey("POS")
            '     My.Computer.Registry.SetValue("HKEY_CURRENT_USER\POS", "POSKeyValue", MaskedTextBox6.Text)
            '     My.Computer.Registry.CurrentUser.CreateSubKey("SID")
            '     My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SID", "POSKeyValue", MaskedTextBox5.Text)
            '     '''''''''''''''''''''
            '     Dim path As String = "C:\POS"
            '     If Not Directory.Exists(path) Then
            '         Directory.CreateDirectory(path)
            '         Directory.CreateDirectory("C:\POS\Receipts")
            '         Directory.CreateDirectory("C:\POS\Prices")
            '         Directory.CreateDirectory("C:\POS\Promotions")
            '         Directory.CreateDirectory("C:\POS\DB")
            '         Directory.CreateDirectory("C:\POS\Archive")
            '         Directory.CreateDirectory("C:\POS\IN")
            '         Directory.CreateDirectory("C:\POS\Images")
            '         Directory.CreateDirectory("C:\POS\OUT")
            '         Directory.CreateDirectory("C:\POS\Buy X Get Y free")
            '         Directory.CreateDirectory("C:\POS\Promo_per")
            '         Directory.CreateDirectory("C:\POS\Buy x For Y")
            '         'Directory.CreateDirectory("C:\POS\Images")
            '
            '     End If
            '     My.Computer.FileSystem.CopyFile("System_Config.cap", "C:\POS\System Manager.exe", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '     My.Computer.FileSystem.CopyFile("POS.cap", "C:\POS\Retail Plus.exe", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '     My.Computer.FileSystem.CopyFile("GenCode128.dll", "C:\POS\GenCode128.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '     My.Computer.FileSystem.CopyFile("IDAutomation.LinearBarCode.dll", "C:\POS\IDAutomation.LinearBarCode.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '     My.Computer.FileSystem.CopyFile("Microsoft.ReportViewer.Common.dll", "C:\POS\Microsoft.ReportViewer.Common.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '     My.Computer.FileSystem.CopyFile("Microsoft.ReportViewer.DataVisualization.dll", "C:\POS\Microsoft.ReportViewer.DataVisualization.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '     My.Computer.FileSystem.CopyFile("Microsoft.ReportViewer.Design.dll", "C:\POS\Microsoft.ReportViewer.Design.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '     My.Computer.FileSystem.CopyFile("Microsoft.ReportViewer.ProcessingObjectModel.dll", "C:\POS\Microsoft.ReportViewer.ProcessingObjectModel.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '     My.Computer.FileSystem.CopyFile("Microsoft.ReportViewer.WinForms.dll", "C:\POS\Microsoft.ReportViewer.WinForms.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '     'My.Computer.FileSystem.CopyFile("POS Screen.xml", "POS Screen.xml", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '     My.Computer.FileSystem.CopyDirectory("Images", "C:\POS\Images", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '
            '     Dim WshShell As WshShellClass = New WshShellClass
            '
            '     Dim MyShortcut As IWshRuntimeLibrary.IWshShortcut
            '
            '     'The shortcut will be created on the desktop
            '
            '     Dim DesktopFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
            '
            '     MyShortcut = CType(WshShell.CreateShortcut(DesktopFolder & "\System Manager.lnk"), IWshRuntimeLibrary.IWshShortcut)
            '
            '     MyShortcut.TargetPath = "C:\POS\System Manager.exe" 'Specify target app full path
            '
            '     MyShortcut.Save()
            '
            '     ''''''''''''''''''''''''''''''''''''''
            '
            '
            '
            '     ' The shortcut will be created on the desktop
            '
            '     Dim DesktopFolder1 As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
            '
            '     MyShortcut = CType(WshShell.CreateShortcut(DesktopFolder1 & "\Retail Plus.lnk"), IWshRuntimeLibrary.IWshShortcut)
            '
            '     MyShortcut.TargetPath = "C:\POS\Retail Plus.exe" 'Specify target app full path
            '
            '     MyShortcut.Save()
            '
            '
            '
            '     ''''''''''''''''''''''''''''''''''''''
            '
            '
            '
            '
            '     ''''''''''''''''''''''''''''''''''''''
            '     'FileCopy("C:\POS\POS Screen.exe", Environment.GetFolderPath(Environment.SpecialFolder.Startup) & "\POS Screen.exe")
            '     ''''''''''''''''''''''''''''''''''''''
            '     create_pos.store_code = MaskedTextBox6.Text.ToString
            '     create_pos.Show()
            '     Me.Close()
            '
            ' End If
            '
            'If RadioButton2.Checked = True Then

            My.Computer.Registry.CurrentUser.CreateSubKey("DBHOST")
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\DBHOST", "DBHOSTKeyValue", MaskedTextBox3.Text)
            My.Computer.Registry.CurrentUser.CreateSubKey("POS")
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\POS", "POSKeyValue", MaskedTextBox6.Text)
            My.Computer.Registry.CurrentUser.CreateSubKey("SID")
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\SID", "POSKeyValue", MaskedTextBox5.Text)
            '''''''''''''''''''''
            '  Dim path As String = "C:\POS"
            '  If Not Directory.Exists(path) Then
            '      Directory.CreateDirectory(path)
            '      Directory.CreateDirectory("C:\POS\Receipts")
            '      Directory.CreateDirectory("C:\POS\Prices")
            '      Directory.CreateDirectory("C:\POS\Promotions")
            '      Directory.CreateDirectory("C:\POS\DB")
            '      Directory.CreateDirectory("C:\POS\Archive")
            '      Directory.CreateDirectory("C:\POS\IN")
            '      Directory.CreateDirectory("C:\POS\Images")
            '      Directory.CreateDirectory("C:\POS\OUT")
            '      Directory.CreateDirectory("C:\POS\Buy X Get Y free")
            '      Directory.CreateDirectory("C:\POS\Promo_per")
            '      Directory.CreateDirectory("C:\POS\Buy x For Y")
            '
            '  End If
            '  My.Computer.FileSystem.CopyFile("System_Config.cap", "C:\POS\System Manager.exe", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("POS.cap", "C:\POS\Retail Plus.exe", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("GenCode128.dll", "C:\POS\GenCode128.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("IDAutomation.LinearBarCode.dll", "C:\POS\IDAutomation.LinearBarCode.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("Microsoft.ReportViewer.Common.dll", "C:\POS\Microsoft.ReportViewer.Common.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("Microsoft.ReportViewer.DataVisualization.dll", "C:\POS\Microsoft.ReportViewer.DataVisualization.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("Microsoft.ReportViewer.Design.dll", "C:\POS\Microsoft.ReportViewer.Design.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("Microsoft.ReportViewer.ProcessingObjectModel.dll", "C:\POS\Microsoft.ReportViewer.ProcessingObjectModel.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("Microsoft.ReportViewer.WinForms.dll", "C:\POS\Microsoft.ReportViewer.WinForms.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("GenCode128.dll", "C:\POS\GenCode128.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("Oracle.ManagedDataAccess.dll", "C:\POS\Oracle.ManagedDataAccess.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("Oracle.ManagedDataAccessIOP.dll", "C:\POS\Oracle.ManagedDataAccessIOP.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("QRCoder.dll", "C:\POS\QRCoder.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("zxing.dll", "C:\POS\zxing.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("zxing.presentation.dll", "C:\POS\zxing.presentation.dll", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("zxing.presentation.xml", "C:\POS\zxing.presentation.xml", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyFile("zxing.xml", "C:\POS\zxing.xml", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  'My.Computer.FileSystem.CopyFile("POS Screen.xml", "POS Screen.xml", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  My.Computer.FileSystem.CopyDirectory("Images", "C:\POS\Images", Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
            '  Dim WshShell As WshShellClass = New WshShellClass
            '
            '  Dim MyShortcut As IWshRuntimeLibrary.IWshShortcut
            '
            '  'The shortcut will be created on the desktop
            '
            '  Dim DesktopFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
            '
            '  MyShortcut = CType(WshShell.CreateShortcut(DesktopFolder & "\System Manager.lnk"), IWshRuntimeLibrary.IWshShortcut)
            '
            '  MyShortcut.TargetPath = "C:\POS\System Manager.exe" 'Specify target app full path
            '
            '  MyShortcut.Save()
            '
            '  ''''''''''''''''''''''''''''''''''''''
            '
            '
            '
            '  ' The shortcut will be created on the desktop
            '
            '  Dim DesktopFolder1 As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
            '
            '  MyShortcut = CType(WshShell.CreateShortcut(DesktopFolder1 & "\Retail Plus.lnk"), IWshRuntimeLibrary.IWshShortcut)
            '
            '  MyShortcut.TargetPath = "C:\POS\Retail Plus.exe" 'Specify target app full path
            '
            '  MyShortcut.Save()
            '
            '  Dim DesktopFolder2 As String = Environment.GetFolderPath(Environment.SpecialFolder.Programs)
            '
            '  MyShortcut = CType(WshShell.CreateShortcut(DesktopFolder2 & "\Retail Plus.lnk"), IWshRuntimeLibrary.IWshShortcut)
            '
            '  MyShortcut.TargetPath = "C:\POS\Retail Plus.exe" 'Specify target app full path
            '
            '  MyShortcut.Save()
            '
            '  ''''''''''''''''''''''''''''''''''''''
            '
            '
            '
            '
            '  ''''''''''''''''''''''''''''''''''''''
            '  'FileCopy("C:\POS\POS Screen.exe", Environment.GetFolderPath(Environment.SpecialFolder.Startup) & "\POS Screen.exe")
            '  ''''''''''''''''''''''''''''''''''''''
            '  'create_pos.store_code = MaskedTextBox6.Text.ToString
            '  'create_pos.Show()
            '  'Me.Close()
            '
            '
            '  ''''''''''''''''''''''''''''''''''''''
            '  'FileCopy(DesktopFolder1 & "\POS Screen.lnk", Environment.GetFolderPath(Environment.SpecialFolder.Startup) & "\POS Screen.lnk")
            '  ''''''''''''''''''''''''''''''''''''''
            '  'create_pos.Show()
            MsgBox("POS Client has been created successfully.", MsgBoxStyle.Information)
                Me.Close()
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub MaskedTextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MaskedTextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True

            MaskedTextBox2.Focus()

        End If
    End Sub

    Private Sub MaskedTextBox1_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles MaskedTextBox1.MaskInputRejected

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
           

            MaskedTextBox1.Enabled = True
            MaskedTextBox2.Enabled = True
            MaskedTextBox3.Enabled = True
            MaskedTextBox1.Focus()


            Button4.Enabled = False

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub MaskedTextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MaskedTextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True

            MaskedTextBox3.Focus()

        End If
    End Sub

    Private Sub MaskedTextBox2_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles MaskedTextBox2.MaskInputRejected

    End Sub

    Private Sub MaskedTextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MaskedTextBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True

            MaskedTextBox6.Focus()


        End If
    End Sub

    Private Sub MaskedTextBox3_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles MaskedTextBox3.MaskInputRejected

    End Sub

    Private Sub MaskedTextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MaskedTextBox6.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            Button3.Focus()
        End If
    End Sub

    Private Sub MaskedTextBox6_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles MaskedTextBox6.MaskInputRejected

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged

    End Sub
End Class
