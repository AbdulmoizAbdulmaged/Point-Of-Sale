Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing
Public Class Add_Location
    Dim indicator As Integer
    Dim position As String
    Public u_id As String = ""
    Public pw As String = ""
    Dim ip As String
    Dim oracle_sid As String = "orcl"
    Private Sub Add_Location_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            cmd.CommandText = "select location_number as ID ,location_name as Name,Location_address as Address,location_tel as Telephone  from locations_manager"
            Dim oraada As New OracleDataAdapter(cmd)
            Dim ds As New DataSet
            oraada.Fill(ds, "LOCATIONS_MANAGER")
            DataGridView1.DataSource = ds.Tables(0)

        Catch ex As Exception

        End Try
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
                cmd.CommandText = "select location_number,location_name,location_address,location_tel from locations_manager where location_number='" + TextBox3.Text + "'"


                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"


                conn.Open()
                Dim rd As OracleDataReader = cmd.ExecuteReader
                If rd.Read Then
                    TextBox3.Text = rd.GetValue(0).ToString
                    TextBox2.Text = rd.GetValue(1).ToString
                    TextBox1.Text = rd.GetValue(2).ToString
                    TextBox4.Text = rd.GetValue(3).ToString


                    TextBox3.Enabled = False
                    indicator = 1


                End If
                rd.Close()






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

                    usr_insert.Connection = conn
                    usr_insert.CommandText = "INSERT INTO locations_manager (location_number,location_name,location_address,location_tel) VALUES('" + TextBox3.Text + "','" + TextBox2.Text + "','" + TextBox1.Text + "','" + TextBox4.Text + "')"



                    If TextBox1.Text <> "" Then
                        If TextBox2.Text <> "" Then
                            If TextBox4.Text <> "" Then

                                conn.Open()
                                    Dim ins As Integer = usr_insert.ExecuteNonQuery()
                                    conn.Close()

                                    If ins > 0 Then
                                        Label6.ForeColor = Color.Green

                                    Label6.Text = "Location has been inserted"
                                    TextBox1.Clear()
                                        TextBox2.Clear()
                                        TextBox3.Clear()
                                        TextBox4.Clear()
                                        TextBox3.Enabled = True

                                    TextBox3.Focus()
                                    End If


                            End If
                        End If
                    End If



                End If

                If indicator = 1 Then
                    Dim usr_update As New OracleCommand


                    usr_update.Connection = conn
                    usr_update.CommandText = "update locations_manager set location_tel = '" + TextBox4.Text + "' ,location_name= '" + TextBox2.Text + "', location_address = '" + TextBox1.Text + "' where location_number = '" + TextBox3.Text + "'"

                    If TextBox1.Text <> "" Then
                        If TextBox2.Text <> "" Then
                            If TextBox4.Text <> "" Then

                                conn.Open()
                                    Dim upd As Integer = usr_update.ExecuteNonQuery
                                    conn.Close()

                                    If upd > 0 Then
                                        Label6.ForeColor = Color.Green
                                    Label6.Text = "Location information updated."
                                    TextBox1.Clear()
                                        TextBox2.Clear()
                                        TextBox3.Clear()
                                        TextBox4.Clear()
                                        TextBox3.Enabled = True


                                    TextBox3.Focus()
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
                cmd.CommandText = "select location_number as ID ,location_name as Name,Location_address as Address,location_tel as Telephone  from locations_manager"
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "LOCATIONS_MANAGER")
                DataGridView1.DataSource = ds.Tables(0)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
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
            TextBox4.Focus()

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox3.Enabled = True


        TextBox3.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim conn As New OracleConnection(oradb)
            Dim user_del As New OracleCommand
            user_del.Connection = conn
            user_del.CommandText = "delete from locations_manager where location_number='" + TextBox3.Text + "'"

            conn.Open()
            Dim ins As Integer = user_del.ExecuteNonQuery()
            conn.Close()
            If ins > 0 Then
                Label6.ForeColor = Color.Green

                Label6.Text = "Location has been deleted"
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
                TextBox3.Enabled = True

                TextBox3.Focus()
            End If

            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.CommandText = "select location_number as ID ,location_name as Name,Location_address as Address,location_tel as Telephone  from locations_manager"
            Dim oraada As New OracleDataAdapter(cmd)
            Dim ds As New DataSet
            oraada.Fill(ds, "LOCATIONS_MANAGER")
            DataGridView1.DataSource = ds.Tables(0)
        Catch ex As Exception

        End Try
    End Sub
End Class