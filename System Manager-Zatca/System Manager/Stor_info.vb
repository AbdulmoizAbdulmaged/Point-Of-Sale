Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing
Public Class Stor_info
    Dim indicator As Integer
    Dim position As String
    Public u_id As String = ""
    Public pw As String = ""
    Dim ip As String
    Dim oracle_sid As String
    Dim rn As Double
    Private Sub Stor_info_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            'Fill  with company data
            Dim conn As New OracleConnection
            Dim cmd As New OracleCommand
            Dim cmd1 As New OracleCommand


            cmd.Connection = conn
            cmd.CommandText = "select comp_name,store_name,address,telephone,policy,vat,vat_limit,cf,vat_number from pos_info"


            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"


            conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader
            If rd.Read Then
                txt1.Text = rd.GetValue(0).ToString
                txt2.Text = rd.GetValue(1).ToString
                txt3.Text = rd.GetValue(2).ToString
                txt4.Text = rd.GetValue(3).ToString
                RichTextBox1.Text = rd.GetValue(4).ToString
                txt5.Text = rd.GetValue(5).ToString
                txt6.Text = rd.GetValue(6).ToString
                txt7.Text = rd.GetValue(7).ToString
                txt8.Text = rd.GetValue(8).ToString

            End If
            rd.Close()
            conn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try


            Dim conn As New OracleConnection
            Dim cmd As New OracleCommand
            Dim cmd1 As New OracleCommand
            Dim upd As Integer

            cmd.Connection = conn
            cmd.CommandText = "update pos_info set comp_name='" & txt1.Text & "',store_name='" & txt2.Text & "',address='" & txt3.Text & "',telephone='" & txt4.Text & "',policy='" & RichTextBox1.Text & "',vat='" & txt5.Text & "',vat_limit='" & txt6.Text & "',CF='" & txt7.Text & "',vat_number= '" & txt8.Text & "' "


            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"


            conn.Open()
            upd = cmd.ExecuteNonQuery()
            conn.Close()

            If upd > 0 Then
                lpl.ForeColor = Color.Green
                lpl.Text = "Company information updated."
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        lpl.Text = ""
        txt1.Clear()
        txt2.Clear()
        txt3.Clear()
        txt4.Clear()
        txt5.Clear()
        txt6.Clear()
        txt7.Clear()
        txt8.Clear()
        RichTextBox1.Clear()
        txt1.Focus()

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim cmd1 As New OracleCommand


        cmd.Connection = conn
        cmd.CommandText = "select * from pos_info"


        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"


        conn.Open()
        Dim rd As OracleDataReader = cmd.ExecuteReader
        If rd.Read Then
            txt1.Text = rd.GetValue(0).ToString
            txt2.Text = rd.GetValue(1).ToString
            txt3.Text = rd.GetValue(2).ToString
            txt4.Text = rd.GetValue(3).ToString
            RichTextBox1.Text = rd.GetValue(4).ToString
            txt5.Text = rd.GetValue(6).ToString
            txt6.Text = rd.GetValue(5).ToString
            txt7.Text = rd.GetValue(7).ToString
            txt8.Text = rd.GetValue(8).ToString

        End If
        rd.Close()
        conn.Close()
    End Sub

    Private Sub txt1_TextChanged(sender As Object, e As EventArgs) Handles txt1.TextChanged

    End Sub

    Private Sub txt1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            txt2.Focus()

        End If
    End Sub

    Private Sub txt3_TextChanged(sender As Object, e As EventArgs) Handles txt3.TextChanged

    End Sub

    Private Sub txt3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            txt4.Focus()

        End If
    End Sub

    Private Sub txt4_TextChanged(sender As Object, e As EventArgs) Handles txt4.TextChanged

    End Sub

    Private Sub txt4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            txt5.Focus()

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

    Private Sub txt6_TextChanged(sender As Object, e As EventArgs) Handles txt6.TextChanged

    End Sub

    Private Sub txt6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt6.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            txt7.Focus()

        End If
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub RichTextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles RichTextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            'Button2.Focus()

        End If
    End Sub

    Private Sub txt7_TextChanged(sender As Object, e As EventArgs) Handles txt7.TextChanged

    End Sub

    Private Sub txt7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt7.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            'RichTextBox1.Focus()

            txt8.Focus()
        End If
    End Sub

    Private Sub txt2_TextChanged(sender As Object, e As EventArgs) Handles txt2.TextChanged

    End Sub

    Private Sub txt2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            txt3.Focus()
        End If
    End Sub

    Private Sub txt8_KeyUp(sender As Object, e As KeyEventArgs) Handles txt8.KeyUp

    End Sub

    Private Sub txt8_TextChanged(sender As Object, e As EventArgs) Handles txt8.TextChanged

    End Sub

    Private Sub txt8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt8.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            RichTextBox1.Focus()


        End If
    End Sub
End Class