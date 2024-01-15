'last update 1/11/2023 4:18PM
Imports System.Data

Imports Oracle.ManagedDataAccess.Client
Imports System.Drawing.Printing 'for drawing QR and print
'Imports Oracle.ManagedDataAccess.EntityFramework
Imports Oracle.ManagedDataAccess.Types
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO.Ports
Imports System.Management
Imports System.Drawing.Imaging


Imports System.Drawing.Graphics
Imports System.Drawing.Drawing2D
Imports System.Drawing.Image
Imports QRCoder 'for QR

Imports ZXing ' for generating barcode
'Imports ZXing.Presentation

Imports System.Configuration
Imports System.ComponentModel

'Imports Generate_barcode




Public Class POS_Screen



    '----------------------------------
    Dim dbpool As Integer
    Dim autorestart As Integer
    Dim org As String = ""
    Dim receipt_status As Integer = 0
    Dim position As Integer
    Dim QTY_SUM As Double = 0
    Dim TOT_SUM As Double = 0
    Public u_id As String = ""
    Public pw As String = ""
    Dim ip As String
    Dim item_name As String
    Dim item_barcode As String
    Dim item_price As Double
    Dim rn As String
    Dim rr As String
    Dim rr_barcode As String
    Dim receipt_time As String
    Dim vat_number As String
    Dim vat As String
    Dim vat_value As String
    Dim receipt_value As String
    Dim AI_TRN As String
    Dim subtotal As String
    Dim TotalSum1 As Double = 0
    Dim totalqty1 As Double = 0
    Dim log_flag As Double
    Dim indicator As Integer = 0
    Dim user_id As String
    Dim user_password As String
    Dim float_amount As Double
    Dim reconcile_flag As Integer
    Dim pos_flag As Integer = 0
    Dim itm_desc As String
    Dim itm_id As String
    Dim itm_prc As String
    Dim itm_total As String
    Dim customer_display As Integer = 0
    Dim conn As New Oracle.ManagedDataAccess.Client.OracleConnection
    Dim conn_scan_item As New OracleConnection
    Dim conn_delete_item As New OracleConnection
    Dim conn_tender_cash As New OracleConnection
    Dim conn_tender_credit As New OracleConnection
    Dim conn_scan_item_refund As New OracleConnection
    Dim conn_scan_receipt_number As New OracleConnection


    Dim cmd As New Oracle.ManagedDataAccess.Client.OracleCommand
    Dim Re_Tot As Double
    Dim cmd2 As New Oracle.ManagedDataAccess.Client.OracleCommand
    Dim com_port As String
    Dim com_status As Integer
    Dim sale_flag As Integer = 0
    Dim admin_flag As Integer = 0
    Dim refund_flag As Integer = 0
    Dim rr_refund As String
    Dim return_flag As Integer = 0
    Dim exchange_flag As Integer = 0
    Dim business_date As String
    Dim login_id As String
    Dim company_name As String
    Dim store_name As String
    Dim address As String
    Dim telephone As String

    Dim policy As String
    Dim vat_limit As String
    Dim b_date As String = ""
    Dim user_flag As Integer
    Dim exchanged_receipt As String
    Dim printing As String
    Public ws_id As String
    Dim B2G1 As Integer = 0
    Dim B2G1_price As Integer
    Dim C As Integer = 0
    Dim L As Integer = 1
    Dim sum As Integer = 0
    Dim x As Integer = 0
    Dim y As Integer = 0
    Dim item_price1 As Integer = 0
    Dim trs As String = ""
    Dim sys_admin_flag As Integer = 0
    Dim printer_status As String
    Dim receipt_lang As String
    '------------------------------Buy x get y Discount

    Dim x_var, y_var, z_var As Double
    Dim BXGYD As Integer = 0



    '-------------------
    Dim cmd1_register As New Oracle.ManagedDataAccess.Client.OracleCommand
    Dim cmd2_register As New OracleCommand
    Dim cmd3_register As New OracleCommand
    Dim cmd4_register As New OracleCommand
    Dim cmd5_register As New OracleCommand
    Dim cmd6_register As New OracleCommand
    Dim cmd7_register As New OracleCommand
    Dim cmd8_register As New OracleCommand

    Dim total_Sales As String = ""
    Dim Net_Sales As String = ""
    Dim total_refund As String = ""
    Dim total_exchage As String = ""
    Dim total_cash As String = ""
    Dim total_crdt As String = ""
    Public store_code As String = ""
    Dim Total_Discount As Integer = 0
    Dim oracle_sid As String
    Dim CF As Double






















    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        Try
            e.Handled = True


            If e.KeyCode = Keys.F6 Then
                e.SuppressKeyPress = True
                If Label6.Visible = True And Label6.Enabled = True Then
                    Label6_Click(sender, e)
                End If


            End If
            If e.KeyCode = Keys.F1 Then
                e.SuppressKeyPress = True
                If Label16.Visible = True And Label16.Enabled = True Then
                    Label16_Click(sender, e)
                End If


            End If


            If e.KeyCode = Keys.F11 Then
                e.SuppressKeyPress = True
                If Label4.Visible = True And Label4.Enabled = True Then
                    Label4_Click(sender, e)
                End If


            End If
            If e.KeyCode = Keys.F12 Then
                e.SuppressKeyPress = True
                If Label7.Visible = True And Label7.Enabled = True Then
                    Label7_Click(sender, e)
                End If


            End If
            If e.KeyCode = Keys.Escape Then
                e.SuppressKeyPress = True
                If Label3.Visible = True And Label3.Enabled = True Then
                    Label3_Click(sender, e)
                End If

            End If
            If e.KeyCode = Keys.F5 Then
                e.SuppressKeyPress = True
                If Label9.Visible = True And Label9.Text = "" And Label9.Enabled = True Then
                    Label9_Click(sender, e)
                End If

            End If

            If e.KeyCode = Keys.F4 Then
                e.SuppressKeyPress = True
                If Label52.Visible = True And Label52.Enabled = True Then
                    Label52_Click(sender, e)
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub








    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then





            e.Handled = True

            If TextBox1.Text <> "" Then
                If ListView1.Items.Count < 50 Then


                    Dim discount As Double = 0
                    Dim conn As New OracleConnection


                    Try

                        ' If IsNumeric(Integer.Parse(ComboBox4.Text)) < 1 Then
                        'ComboBox4.Text = 1
                        ' ElseIf IsNumeric(Integer.Parse(ComboBox4.Text)) = False Then
                        'ComboBox4.Text = 1
                        ' End If



                        conn = New OracleConnection
                        Dim cmd As New OracleCommand
                        Dim cmd1 As New OracleCommand
                        Dim cmd_PBW As New OracleCommand
                        Dim id As Double = get_id()

                        Dim PBW As String = TextBox1.Text
                        Dim PBW_ID As String = ""
                        Dim PBW_Price As String = ""



                        If PBW.Length = 13 Then


                            PBW_ID = PBW.Substring(0, 7)
                            PBW_Price = PBW.Substring(PBW.Length - 6, 6)
                        End If


                        item_barcode = TextBox1.Text.Trim

                        cmd_PBW.Connection = conn_scan_item
                        cmd_PBW.CommandText = "select itm_name,price_unit from pricebasedonweight where itm_id='" + PBW_ID + "'"

                        cmd.Connection = conn_scan_item
                        cmd.CommandText = "select de_itm from as_itm where id_itm = '" + TextBox1.Text.Trim + "'"
                        cmd1.Connection = conn_scan_item
                        cmd1.CommandText = "select mo_chn_prn_un_prc from TR_CHN_PRN_PRC where id_ev = :param1"
                        cmd1.Parameters.Add(":param1", OracleDbType.Double).Value = id
                        If conn_scan_item.State = ConnectionState.Open Then
                            conn_scan_item.Open()
                            'conn.Dispose()
                        End If
                        conn_scan_item.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";Connection Timeout=10;Max Pool Size=100;"


                        conn_scan_item.Open()
                        Dim rd_PBW As OracleDataReader = cmd_PBW.ExecuteReader
                        If rd_PBW.Read Then

                            'PBW_Price = PBW_Price / 1000

                            With ListView1.Items.Add(PBW_ID)
                                .SubItems.Add(rd_PBW.GetValue(0).ToString & "(" & Double.Parse(rd_PBW.GetValue(1) * CF) & ")")
                                .SubItems.Add(Double.Parse(PBW_Price))
                                .SubItems.Add(1)
                                .SubItems.Add(0)
                                .SubItems.Add(0)
                                .SubItems.Add("")
                            End With

                            Dim TotalSum_PBW As Double = 0
                            Dim totalqty_PBW As Double = 0
                            Dim totaldis_PBW As Double = 0

                            Dim TempNode_PBW As ListViewItem
                            For Each TempNode_PBW In ListView1.Items
                                TotalSum_PBW += CDbl(TempNode_PBW.SubItems.Item(2).Text)
                                totalqty_PBW += CDbl(TempNode_PBW.SubItems.Item(3).Text)
                                totaldis_PBW += CDbl(TempNode_PBW.SubItems.Item(5).Text)
                            Next

                            ListView2.Items(0).SubItems(0).Text = TotalSum_PBW
                            'ListView2.Items(0).SubItems(1).Text = Total_Discount.ToString
                            If TotalSum_PBW > Convert.ToDecimal(vat_limit) Then
                                ListView2.Items(0).SubItems(2).Text = Math.Round((vat * (TotalSum_PBW - totaldis_PBW)) / 100, 1) 'Math.Round(vat * (TotalSum) / 100, 2)
                            Else
                                ListView2.Items(0).SubItems(2).Text = "0"
                            End If
                            ListView2.Items(0).SubItems(1).Text = totaldis_PBW
                            ListView2.Items(0).SubItems(3).Text = totalqty_PBW
                            ListView2.Items(0).SubItems(4).Text = Convert.ToDecimal(ListView2.Items(0).SubItems(0).Text) + Convert.ToDecimal(ListView2.Items(0).SubItems(2).Text) - Convert.ToDecimal(ListView2.Items(0).SubItems(1).Text)
                            rd_PBW.Close()
                            conn_scan_item.Close()
                            'conn.Dispose()
                            TextBox1.Clear()

                            QTY_SUM = 0
                            TOT_SUM = 0






                            PictureBox3.Enabled = True
                            Label6.Visible = True

                            TextBox1.Focus()

                            'itm_total = TotalSum

                            For k = 0 To ListView1.Items.Count - 1
                                If ListView1.Items(k).SubItems(3).Text = "1" Then

                                    ListView1.Items(k).ForeColor = Color.Blue
                                End If
                            Next

                            Label7.Enabled = True
                            PictureBox7.Enabled = True
                            Label4.Enabled = True
                            Label7.Visible = True
                            Label4.Visible = True
                            Label6.Enabled = True
                            PictureBox3.Enabled = True
                            Label6.Text = "F6-Tender"
                            Label5.Visible = True
                            Label53.Visible = True
                            Label54.Visible = False
                            Label9.Visible = False
                            Label52.Visible = False
                            PictureBox4.Enabled = True
                            Label16.Visible = False
                            Label24.Visible = False
                            Label25.Visible = False
                            Label31.Visible = False



                            If com_status = 1 Then


                                Dim sp = New SerialPort(com_port, 9600, Parity.None, 8, StopBits.One)

                                sp.Open()

                                ' to clear the display
                                sp.Write(Convert.ToString(Chr(12)))
                                'first line goes here
                                sp.WriteLine(itm_id + "       " + item_price.ToString + Chr(13))
                                '2nd line goes here
                                sp.WriteLine("Total" + "       " + itm_total)
                                sp.Dispose()
                                sp.Close()
                            End If
                            Exit Sub


                        End If
                        Dim rd As OracleDataReader = cmd.ExecuteReader
                        If rd.Read Then
                            item_name = rd.GetValue(0).ToString

                        Else
                            TextBox1.Clear()
                            'MessageBox.Show("ITEM Not found.")
                            'MsgBox("ITEM Not found.", MsgBoxStyle.Information, "ERROR")
                            Label17.Text = "ERROR1:ITEM not found."
                            ComboBox4.Visible = False
                            Label24.Visible = False
                            Label25.Visible = False
                            Label31.Visible = False
                            Label9.Visible = False
                            Label52.Visible = False
                            Label16.Visible = False
                            Label3.Visible = False
                            Label6.Visible = False
                            Label4.Visible = False
                            Label7.Visible = False
                            Label53.Visible = False
                            Label5.Visible = False

                            Label17.Visible = True
                            Button1.Visible = True
                            Button1.Enabled = True
                            ListView1.Visible = False
                            Panel1.Visible = False
                            Panel3.Visible = False
                            ListView2.Visible = False
                            TextBox1.Enabled = False
                            TextBox2.Enabled = False
                            TextBox3.Enabled = False
                            TextBox4.Enabled = False
                            TextBox5.Enabled = False
                            TextBox6.Enabled = False
                            TextBox7.Enabled = False
                            TextBox8.Enabled = False
                            TextBox9.Enabled = False
                            Label1.Visible = False
                            Label2.Enabled = False
                            Label3.Enabled = False
                            Label4.Enabled = False
                            Label5.Enabled = False
                            Label6.Enabled = False
                            Label7.Enabled = False
                            Label8.Enabled = False
                            Label9.Enabled = False
                            Label16.Enabled = False
                            Label24.Enabled = False
                            Label25.Enabled = False
                            Label31.Enabled = False
                            Label52.Enabled = False
                            Label53.Enabled = False







                            Exit Sub


                        End If
                        rd.Close()

                        conn_scan_item.Close() 'conn.Close()'************
                        'conn.Dispose()
                        conn_scan_item.Open()
                        Dim rd1 As OracleDataReader = cmd1.ExecuteReader
                        If rd1.Read Then
                            item_price = Double.Parse(rd1.GetValue(0)) * CF
                            item_price1 = item_price

                        End If
                        rd1.Close()
                        conn_scan_item.Close()
                        'conn.Dispose()
                        '--------------------------------apply promotion percentage



                        Dim cmd_PROMO_PER As New OracleCommand
                        cmd_PROMO_PER.Connection = conn_scan_item
                        cmd_PROMO_PER.CommandText = "select min(new_price) from promo_per where Barcode = '" & item_barcode & "' and to_date(sysdate,'DD-MM-YY') >= to_date(start_date,'DD-MM-YY') and to_date(sysdate,'DD-MM-YY') <= to_date(end_date,'DD-MM-YY')"
                        'Dim percentage As Integer = 0

                        'Dim disc As Integer = 0
                        conn_scan_item.Open()
                        Dim rd_per As OracleDataReader = cmd_PROMO_PER.ExecuteReader
                        While rd_per.Read

                            'If rd_per.GetValue(0).ToString <> "" Then
                            'percentage = Double.Parse(rd_per.GetValue(1))

                            'discount = (item_price * percentage / 100)

                            'item_name = item_name & " - " & "% Disc. (" & percentage & ")"

                            'Total_Discount = Total_Discount + discount

                            'End If
                            If Not IsDBNull(rd_per.GetValue(0)) Then

                                If rd_per.GetValue(0) * CF < item_price Then
                                    'Total_Discount = item_price - rd_per.GetValue(0) * CF
                                    item_price = rd_per.GetValue(0) * CF
                                    Total_Discount = item_price1 - item_price

                                End If

                            End If
                        End While
                        rd_per.Close()
                        conn_scan_item.Close()
                        'conn.Dispose()
                        '------------------------------------------------------------------apply one price promotion
                        Dim one_price As New OracleCommand
                        one_price.Connection = conn_scan_item
                        one_price.CommandText = "select min(new_price) from promo_oneprice where Barcode = '" & item_barcode & "' and to_date(sysdate,'DD-MM-YY') >= to_date(start_date,'DD-MM-YY') and to_date(sysdate,'DD-MM-YY') <= to_date(end_date,'DD-MM-YY')"

                        conn_scan_item.Open()
                        Dim rd_one_price As OracleDataReader = one_price.ExecuteReader
                        While rd_one_price.Read
                            If Not IsDBNull(rd_one_price.GetValue(0)) Then

                                If rd_one_price.GetValue(0) * CF < item_price Then
                                    Total_Discount = item_price - rd_one_price.GetValue(0) * CF
                                    item_price = rd_one_price.GetValue(0) * CF

                                End If

                            End If


                        End While
                        rd_one_price.Close()
                        conn_scan_item.Close()
                        'conn.Dispose()
                        '---------------------------------------------------------------------------------------------

                        '---------------------------------------Buy X GET Y Discount.

                        Dim CMD_BXGD As New OracleCommand
                        Dim CMD_UPDATE As New OracleCommand


                        CMD_BXGD.Connection = conn_scan_item
                        CMD_BXGD.CommandText = "select x,y,c  from Buy_X_GET_Y_Discount where itm = '" & item_barcode & "' and to_date(sysdate,'DD-MM-YY') >= to_date(start_date,'DD-MM-YY') and to_date(sysdate,'DD-MM-YY') <= to_date(end_date,'DD-MM-YY')"

                        conn_scan_item.Open()
                        Dim RD_BXGD As OracleDataReader = CMD_BXGD.ExecuteReader
                        If RD_BXGD.Read Then
                            x_var = RD_BXGD.GetValue(0)
                            y_var = RD_BXGD.GetValue(1)
                            z_var = RD_BXGD.GetValue(2)


                            If x_var = z_var Then
                                z_var = 0
                                item_price = item_price - y_var
                                Total_Discount = y_var
                                BXGYD = 1
                                For i = 0 To ListView1.Items.Count - 1
                                    If ListView1.Items(i).SubItems(0).Text = item_barcode Then
                                        ListView1.Items(i).BackColor = Color.Orange
                                        ListView1.Items(i).SubItems(6).Text = 2
                                    End If
                                Next
                            ElseIf z_var < x_var Then
                                z_var = z_var + 1
                            End If
                        End If

                        RD_BXGD.Close()
                        CMD_UPDATE.Connection = conn_scan_item
                        CMD_UPDATE.CommandText = "update Buy_X_GET_Y_Discount set c = :nodec where itm = '" & item_barcode & "'"
                        CMD_UPDATE.Parameters.Add("nodec", OracleDbType.Double).Value = Double.Parse(z_var)

                        CMD_UPDATE.ExecuteNonQuery()
                        conn_scan_item.Close()
                        'conn.Dispose()
                        'MsgBox(Total_Discount)

                        '---------------------------------------------------



                        With ListView1.Items.Add(item_barcode)
                            .SubItems.Add(item_name)
                            .SubItems.Add(Math.Round(item_price, 2) * Integer.Parse(ComboBox4.Text))
                            .SubItems.Add(ComboBox4.Text)
                            .SubItems.Add(0)
                            .SubItems.Add(Math.Round(Total_Discount, 2))
                            .SubItems.Add(BXGYD)

                        End With
                        ComboBox4.Text = 1
                        For i = 0 To ListView1.Items.Count - 1
                            If ListView1.Items(i).SubItems(6).Text = 1 Then
                                ListView1.Items(i).BackColor = Color.Red
                            End If
                        Next

                        BXGYD = 0
                        Total_Discount = 0






                        itm_id = TextBox1.Text
                        itm_desc = item_name
                        itm_prc = item_price

                        TextBox1.Clear()


                        QTY_SUM = 0
                        TOT_SUM = 0






                        PictureBox3.Enabled = True
                        Label6.Visible = True

                        TextBox1.Focus()

                        Dim TotalSum As Double = 0
                        Dim totalqty As Double = 0
                        Dim totaldis As Double = 0

                        Dim TempNode As ListViewItem
                        For Each TempNode In ListView1.Items
                            TotalSum += CDbl(TempNode.SubItems.Item(2).Text)
                            totalqty += CDbl(TempNode.SubItems.Item(3).Text)
                            totaldis += CDbl(TempNode.SubItems.Item(5).Text)
                        Next

                        ListView2.Items(0).SubItems(0).Text = Math.Round(TotalSum, 2)
                        'ListView2.Items(0).SubItems(1).Text = Total_Discount.ToString
                        If TotalSum > Convert.ToDecimal(vat_limit) Then
                            ListView2.Items(0).SubItems(2).Text = Math.Round(vat * (TotalSum) / 100, 3)
                        Else
                            ListView2.Items(0).SubItems(2).Text = "0"
                        End If
                        ListView2.Items(0).SubItems(1).Text = Math.Round(totaldis, 2)
                        ListView2.Items(0).SubItems(3).Text = totalqty
                        ListView2.Items(0).SubItems(4).Text = Math.Round(Double.Parse(ListView2.Items(0).SubItems(0).Text) + Double.Parse(ListView2.Items(0).SubItems(2).Text), 2)

                        itm_total = TotalSum

                        For k = 0 To ListView1.Items.Count - 1
                            If ListView1.Items(k).SubItems(3).Text = "1" Then

                                ListView1.Items(k).ForeColor = Color.Blue
                            End If
                        Next

                        Label7.Enabled = True
                        PictureBox7.Enabled = True
                        Label4.Enabled = True
                        Label7.Visible = True
                        Label4.Visible = True
                        Label6.Enabled = True
                        PictureBox3.Enabled = True
                        Label6.Text = "F6-Tender"
                        Label5.Visible = True
                        Label53.Visible = True
                        Label54.Visible = False
                        Label9.Visible = False
                        Label52.Visible = False
                        PictureBox4.Enabled = True
                        Label16.Visible = False
                        Label24.Visible = False
                        Label25.Visible = False
                        Label31.Visible = False



                        If com_status = 1 Then


                            Dim sp = New SerialPort(com_port, 9600, Parity.None, 8, StopBits.One)

                            sp.Open()

                            ' to clear the display
                            sp.Write(Convert.ToString(Chr(12)))
                            'first line goes here
                            sp.WriteLine(itm_id + "       " + item_price.ToString + Chr(13))
                            '2nd line goes here
                            sp.WriteLine("Total" + "       " + itm_total)
                            sp.Dispose()
                            sp.Close()
                        End If


                        If dbpool > 60 Then
                            autorestart = 1
                            Dim cmd_scanned As New OracleCommand
                            cmd_scanned.Connection = conn_scan_item
                            If conn_scan_item.State = ConnectionState.Closed Then
                                conn_scan_item.Open()
                            End If
                            For i = 0 To ListView1.Items.Count - 1

                                cmd_scanned.CommandText = "insert into auto_logon_scanned values('" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(1).Text + "','" + ListView1.Items(i).SubItems(2).Text + "','" + ListView1.Items(i).SubItems(3).Text + "','" + ListView1.Items(i).SubItems(4).Text + "')"

                                cmd_scanned.ExecuteNonQuery()

                            Next
                            conn_scan_item.Close()



                            Me.Close()
                        End If


                    Catch ex As Exception

                        MsgBox(ex.Message, MsgBoxStyle.Critical)
                    Finally
                        dbpool = dbpool + 1
                        conn_scan_item.Close()

                        'conn.Dispose()
                        'OracleConnection.ClearAllPools()
                    End Try
                Else
                    TextBox1.Clear()
                    TextBox1.Focus()
                    MsgBox("You proceed the maximun number of items per receipt", MsgBoxStyle.Information)
                End If
            End If
        End If

    End Sub

    Private Sub TextBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseUp

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

        TextBox1.Text = TextBox1.Text.Trim
        If ListView1.Items.Count = 0 Then
            Label3.Visible = True
        Else
            Label3.Visible = False
        End If
    End Sub

    Private Sub POS_Screen_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try


            e.Handled = True
            If ListView3.Visible = True Then
                If e.KeyCode = Keys.Delete Then



                    Dim cmd_row_del As New OracleCommand
                    If ListView3.Items.Count = 0 Then
                        'ind = 1

                    End If
                    'Remove Multiple Selected Items
                    Dim index As Integer
                    For Each item As ListViewItem In ListView3.SelectedItems
                        index = ListView3.SelectedIndices(0)


                        '------------------------------------------Print open report
                        cmd_row_del.CommandText = "delete from scanned_items where R_I = '" + ListView3.Items(index).SubItems(5).Text + "'"
                        cmd_row_del.Connection = conn

                        conn.Open()
                        cmd_row_del.ExecuteNonQuery()
                        conn.Close()
                        'conn.Dispose()
                        item.Remove()



                    Next
                    Dim cmd_sum As New OracleCommand
                    cmd_sum.Connection = conn
                    cmd_sum.CommandText = "select count( distinct box_id ), count (itm_barcode) from scanned_items where transfer_number = '" + trs + "'  "

                    conn.Open()
                    Dim rd_sum As OracleDataReader = cmd_sum.ExecuteReader
                    If rd_sum.Read Then
                        Label45.Text = "BOX's:" & rd_sum.GetValue(0).ToString & "                    " & "ITEMS:" & rd_sum.GetValue(1).ToString
                    End If

                    rd_sum.Close()
                    conn.Close()
                    'conn.Dispose()
                    TextBox11.Focus()

                End If
            End If
            If e.KeyCode = Keys.F9 Then
                e.SuppressKeyPress = True
                If Label24.Visible = True And Label2.Visible = False Then
                    Label24_Click(sender, e)
                End If


            End If

            If e.KeyCode = Keys.F10 Then
                e.SuppressKeyPress = True
                If Label25.Visible = True And Label8.Visible = False Then
                    Label25_Click(sender, e)
                End If


            End If

            If e.KeyCode = Keys.F7 Then
                e.SuppressKeyPress = True
                If Label31.Visible = True And Label6.Visible = False Then
                    Label31_Click(sender, e)
                End If


            End If
            If e.KeyCode = Keys.Down Then
                e.Handled = True
                e.SuppressKeyPress = True
                Dim x As Integer

                If ListView1.SelectedItems.Count > 0 Then


                    x = ListView1.FocusedItem.Index
                    ListView1.Items(x + 1).Focused = True
                    ListView1.Items(x).Selected = False
                    ListView1.Items(x + 1).Selected = True
                End If


            End If
            If e.KeyCode = Keys.Up Then
                e.Handled = True
                e.SuppressKeyPress = True
                Dim x As Integer
                x = ListView1.FocusedItem.Index
                ListView1.Items(x - 1).Focused = True
                ListView1.Items(x).Selected = False
                ListView1.Items(x - 1).Selected = True
                'MessageBox.Show(x.ToString)


            End If

            If (e.Alt AndAlso (e.KeyCode = Keys.Space)) Then
                ' When Alt + P is pressed, the Click event for the print
                ' button is raised.
                e.SuppressKeyPress = True
                Me.WindowState = FormWindowState.Minimized

            End If

            If (e.Alt AndAlso (e.KeyCode = Keys.F4)) Then
                ' When Alt + P is pressed, the Click event for the print
                ' button is raised.
                e.SuppressKeyPress = True
                Me.Close()

            End If

            If e.KeyCode = Keys.F12 Then

                e.SuppressKeyPress = True
                If Label7.Visible = True And Label7.Enabled = True Then
                    Label7_Click(sender, e)
                End If

                If Label57.Visible = True Then
                    Label57_Click(sender, e)
                End If


            End If
            If e.KeyCode = Keys.F11 Then
                e.SuppressKeyPress = True
                If Label4.Visible = True And Label4.Enabled = True Then
                    Label4_Click(sender, e)
                End If


            End If
            If e.KeyCode = Keys.F6 Then
                e.SuppressKeyPress = True
                If Label6.Visible = True And Label6.Enabled = True Then
                    Label6_Click(sender, e)
                End If

                If Label58.Visible = True Then
                    Label58_Click(sender, e)
                End If


            End If

            If e.KeyCode = Keys.F5 Then
                e.SuppressKeyPress = True
                If Label53.Visible = True And Label53.Enabled = True Then
                    Label53_Click(sender, e)
                End If


            End If

            If e.KeyCode = Keys.F2 Then
                e.SuppressKeyPress = True
                If Label2.Visible = True And Label2.Enabled = True Then
                    Label2_Click(sender, e)
                End If


            End If
            If e.KeyCode = Keys.F3 Then
                e.SuppressKeyPress = True
                If Label9.Visible = True And Label9.Text <> "F5-R.Receipt" And Label9.Enabled = True Then
                    Label9_Click(sender, e)
                End If


            End If

            If e.KeyCode = Keys.F5 Then
                e.SuppressKeyPress = True
                If Label9.Visible = True And Label9.Text = "F5-R.Receipt" And Label9.Enabled = True Then
                    Label9_Click(sender, e)
                End If


            End If

            If e.KeyCode = Keys.F4 Then
                e.SuppressKeyPress = True
                If Label52.Visible = True And Label52.Enabled = True Then
                    Label52_Click(sender, e)
                End If

                If Label55.Visible = True Then
                    Label55_Click(sender, e)
                End If
            End If
            If e.KeyCode = Keys.F4 Then
                e.SuppressKeyPress = True
                If Label5.Visible = True And Label5.Enabled = True Then
                    Label5_Click(sender, e)
                End If


            End If
            If e.KeyCode = Keys.F4 Then
                e.SuppressKeyPress = True
                If Label8.Visible = True And Label8.Enabled = True Then
                    Label8_Click(sender, e)
                End If


            End If
            If e.KeyCode = Keys.F1 Then
                e.SuppressKeyPress = True
                If Label16.Visible = True And Label16.Enabled = True Then
                    Label16_Click(sender, e)
                End If


            End If

            If e.KeyCode = Keys.F8 Then
                e.SuppressKeyPress = True
                If Label60.Visible = True Then
                    Label60_Click(sender, e)
                End If


            End If

            If e.KeyCode = Keys.F10 Then
                e.SuppressKeyPress = True
                If Label56.Visible = True Then
                    Label56_Click(sender, e)
                End If


            End If


            If e.KeyCode = Keys.Escape And Label3.Text = "ESC-Undo" And Label3.Visible = True Then
                e.Handled = True
                e.SuppressKeyPress = True
                If Label3.Visible = True And Label3.Enabled = True Then
                    Label3_Click(sender, e)

                End If

            End If

            If e.KeyCode = Keys.Escape And Label54.Visible = True Then
                e.Handled = True
                e.SuppressKeyPress = True
                If Label54.Visible = True And Label54.Enabled = True Then
                    Label54_Click(sender, e)

                End If

            End If
            If e.KeyCode = Keys.F10 And Label3.Text <> "ESC-Undo" Then
                e.SuppressKeyPress = True
                If Label3.Visible = True And Label3.Enabled = True Then
                    Label3_Click(sender, e)
                End If




            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub POS_Screen_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

    End Sub











    Private Sub POS_Screen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load





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
            'Panel1.BackColor = Color.FromArgb(25, Color.Red)
            ListView1.Columns(4).Width = 0
            ListView1.Columns(5).Width = 85
            '''''''''''''''''''''''''''''''''''''''''''''''



            ''''''''''''''''''''''
            Dim path As String = "C:\POS\Receipts"
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If

            '''''''''''''''''''''''''''''''''''''
            Dim readValue = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\DBHOST", "DBHOSTKeyValue", Nothing)


            If readValue.ToString <> "" Then
                ip = readValue.ToString

            Else
                MsgBox("Please update server host name...", MsgBoxStyle.Critical)
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



                u_id = SC
                pw = DB_password



            End If
            ''''''''''''''''''''''''''''''''''''''''''

            Dim conn As New OracleConnection
            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim cmd_printer_status As New OracleCommand
            cmd_printer_status.CommandText = "select status from printer_status"
            cmd_printer_status.Connection = conn
            conn.Open()
            Dim rd_printer_status As OracleDataReader = cmd_printer_status.ExecuteReader
            If rd_printer_status.Read Then
                printer_status = rd_printer_status.GetValue(0).ToString

            End If
            rd_printer_status.Close()
            conn.Close()

            ''''''''''''''''''''''''''''''''''''''''''
            Dim cmd_lang As New OracleCommand
            cmd_lang.Connection = conn
            cmd_lang.CommandText = "select lang  from receipt_languege"

            conn.Open()
            Dim rd_lang As OracleDataReader = cmd_lang.ExecuteReader
            If rd_lang.Read Then
                receipt_lang = rd_lang.GetValue(0).ToString

            End If
            rd_lang.Close()
            conn.Close()


            ''''''''''''''''''''''''''''''''''''''''''

            Dim color_background As String = ""
            Dim panel As String = ""
            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"

            Dim cmd_bg As New OracleCommand
            cmd_bg.CommandText = "Select color from background"
            cmd_bg.Connection = conn

            conn.Open()
            Dim rd_bg As OracleDataReader = cmd_bg.ExecuteReader
            If rd_bg.Read Then
                color_background = rd_bg.GetValue(0).ToString

            End If
            rd_bg.Close()
            conn.Close()

            If color_background = "Violet" Then
                color_background = "C:\POS\Images\b2.jpg"
                panel = "C:\POS\Images\p2.jpg"
                PictureBox9.ImageLocation = "C:\POS\Images\BG2.jpg"
            ElseIf (color_background = "Green") Then
                color_background = "C:\POS\Images\b1.jpg"
                panel = "C:\POS\Images\p12.jpg"
                PictureBox9.ImageLocation = "C:\POS\Images\BG1.jpg"
            ElseIf (color_background = "Yellow") Then
                color_background = "C:\POS\Images\b4.jpg"
                panel = "C:\POS\Images\p4.jpg"

                PictureBox9.ImageLocation = "C:\POS\Images\BG4.jpg"
            ElseIf (color_background = "Cyan") Then

                color_background = "C:\POS\Images\b3.jpg"
                panel = "C:\POS\Images\p3.jpg"
                PictureBox9.ImageLocation = "C:\POS\Images\BG3.jpg"
            ElseIf (color_background = "Red") Then

                color_background = "C:\POS\Images\b5.jpg"
                panel = "C:\POS\Images\p5.jpg"
                PictureBox9.ImageLocation = "C:\POS\Images\BG5.jpg"

            ElseIf (color_background = "Brown") Then
                color_background = "C:\POS\Images\b6.jpg"
                panel = "C:\POS\Images\p6.jpg"
                PictureBox9.ImageLocation = "C:\POS\Images\BG6.jpg"

            ElseIf (color_background = "Pink") Then
                color_background = "C:\POS\Images\b7.jpg"
                panel = "C:\POS\Images\p7.jpg"
                PictureBox9.ImageLocation = "C:\POS\Images\BG7.jpg"


            End If

            Dim image1 As Image
            Dim image2 As Image


            If File.Exists(color_background) And File.Exists(panel) Then
                image1 = Image.FromFile(color_background)
                image2 = Image.FromFile(panel)
                PictureBox2.ImageLocation = color_background
                Label2.Image = image1
                Label24.Image = image1
                PictureBox5.ImageLocation = color_background
                Label55.Image = image1
                Label8.Image = image1
                Label25.Image = image1
                PictureBox3.ImageLocation = color_background
                Label6.Image = image1
                Label31.Image = image1


                PictureBox4.ImageLocation = color_background
                Label9.Image = image1
                Label53.Image = image1


                PictureBox6.ImageLocation = color_background
                Label5.Image = image1
                Label52.Image = image1

                PictureBox10.ImageLocation = color_background
                Label16.Image = image1

                PictureBox8.ImageLocation = color_background
                Label3.Image = image1
                Label56.Image = image1

                PictureBox7.ImageLocation = color_background
                Label7.Image = image1

                PictureBox11.ImageLocation = color_background
                Label4.Image = image1

                Panel1.BackgroundImage = image2
                Panel2.BackgroundImage = image2
                Panel3.BackgroundImage = image2

                Label19.Image = image1
                Label20.Image = image1
                Label21.Image = image1
                Label22.Image = image1
                Label23.Image = image1
                Label48.Image = image1
                Label49.Image = image1
                Label50.Image = image1
                Label51.Image = image1
                Label47.Image = image1
                Label54.Image = image1
                Button1.Image = image1
                Label57.Image = image1
                Label58.Image = image1
                Label39.Image = image1
                Label40.Image = image1
                Label41.Image = image1
                Label42.Image = image1
                Label43.Image = image1
                Button2.Image = image1
                Button3.Image = image1
                Button4.Image = image1
                Button5.Image = image1
                Label60.Image = image1


            End If


            ''''''''''''''''''''''''''''''''''''''''''
            If File.Exists("C:\POS\Images\BG.jpg") Then
                PictureBox9.ImageLocation = "C:\POS\Images\BG.jpg"
            End If




            '''''''''''''''''''''''''''''''''''''

            Me.KeyPreview = True



            Dim cmd As New OracleCommand
            Dim cmd_date As New OracleCommand
            Dim printer As New OracleCommand

            cmd_date.CommandText = "select business_date from daily_ops"
            cmd_date.Connection = conn

            conn.Open()
            Dim rd_date As OracleDataReader = cmd_date.ExecuteReader
            If rd_date.Read Then
                b_date = rd_date.GetValue(0)


            End If
            rd_date.Close()
            conn.Close()
            '--------------------------------------------------------
            cmd.CommandText = "select com,status from pole_display"
            cmd.Connection = conn

            conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader

            While rd.Read
                com_port = rd.GetValue(0)
                com_status = rd.GetValue(1)

            End While
            rd.Close()
            conn.Close()

            printer.Connection = conn
            printer.CommandText = "select status from printer_status"

            'conn.Open()
            '  Dim rd_printer As OracleDataReader = printer.ExecuteReader

            'While rd_printer.Read
            'printing = rd_printer.GetValue(0)

            ' End While
            ' rd_printer.Close()
            ' conn.Close()


            log_flag = get_status()
            If log_flag = "1" Then
                Label9.Visible = True

            End If







            'PictureBox5.Enabled = False








            Label2.Visible = True


            PictureBox2.BackColor = Color.Black
            PictureBox8.BackColor = Color.Black
            PictureBox4.BackColor = Color.Black
            PictureBox5.BackColor = Color.Black
            PictureBox3.BackColor = Color.Black
            PictureBox7.BackColor = Color.Black
            PictureBox6.BackColor = Color.Black
            PictureBox11.BackColor = Color.Black

            With ListView2.Items.Add(0)
                .SubItems.Add(0)
                .SubItems.Add(0)
                .SubItems.Add(0)
                .SubItems.Add(0)

            End With


            ' Show all available COM ports.
            For Each sp As String In My.Computer.Ports.SerialPortNames
                ListBox1.Items.Add(sp)
            Next

            Dim pos_info As New OracleCommand

            pos_info.CommandText = "select * from pos_info"
            pos_info.Connection = conn

            conn.Open()
            Dim pos_info_rd As OracleDataReader = pos_info.ExecuteReader
            While pos_info_rd.Read

                company_name = pos_info_rd.GetValue(0).ToString
                store_name = pos_info_rd.GetValue(1).ToString
                address = pos_info_rd.GetValue(2).ToString
                telephone = pos_info_rd.GetValue(3).ToString
                policy = pos_info_rd.GetValue(4).ToString
                vat_limit = pos_info_rd.GetValue(5)
                vat = pos_info_rd.GetValue(6)
                vat_number = pos_info_rd.GetValue(8).ToString


            End While
            pos_info_rd.Close()
            conn.Close()


            Label2.Focus()
            ''''''''''''''''''''''''

            'Dim conn As New OracleConnection
            Dim cmd_CF As New OracleCommand


            cmd_CF.Connection = conn
            cmd_CF.CommandText = "select * from pos_info"


            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"


            conn.Open()
            Dim rd_CF As OracleDataReader = cmd_CF.ExecuteReader
            If rd_CF.Read Then

                CF = rd_CF.GetValue(7)

            End If
            rd_CF.Close()
            conn.Close()
            '-----------------------------------
            '---------------------Buy x get y free
            Dim cmd_update As New OracleCommand
            cmd_update.Connection = conn
            cmd_update.CommandText = "update Buy_X_GET_Y_Discount set c = :nodec"
            cmd_update.Parameters.Add("nodec", OracleDbType.Double).Value = 0
            conn.Open()
            cmd_update.ExecuteNonQuery()
            conn.Close()

            ''''''''''''''''''''''''''''''''''''add if statement
            Dim cmd_autologon As New OracleCommand
            cmd_autologon.Connection = conn
            cmd_autologon.CommandText = "select * from auto_logon"
            Dim rd_autologon As OracleDataReader

            conn.Open()
            rd_autologon = cmd_autologon.ExecuteReader

            While rd_autologon.Read
                user_id = rd_autologon.GetValue(0).ToString
                position = rd_autologon.GetValue(1).ToString
            End While
            rd_autologon.Close()
            conn.Close()

            If user_id <> "" Then
                '''''''''''''''''''''





                log_flag = 1
                reconcile_flag = 0
                user_flag = 1

                Label1.Text = "Enter an item number."
                Label1.Visible = True
                TextBox1.Visible = True
                ComboBox4.Visible = True
                TextBox1.Clear()
                TextBox4.Visible = False
                ListView1.Visible = True
                Panel1.Visible = True
                Panel3.Visible = True
                ListView2.Visible = True
                TextBox1.Enabled = True
                Label2.Visible = False
                Label5.Visible = False
                Label53.Visible = False
                Label7.Visible = False
                Label7.Text = "F12-Cancel"
                Label7.Location = New Point(249, 510)
                If ListView1.Items.Count = 0 Then
                    Label3.Visible = True
                End If


                TextBox1.Focus()
                pos_flag = 1
                ' MessageBox.Show("B")
                Label5.Text = "F4-E.Receipt"
                    Label5.Location = New Point(650, 420)
                    Label5.Visible = False
                    Label53.Visible = False
                    Label54.Visible = False '--------------------------updated
                    Label16.Visible = True
                    Label24.Visible = True
                    Label25.Visible = True
                    Label31.Visible = True

                    PictureBox10.Enabled = True
                    PictureBox2.Enabled = True



                    Label9.Text = "F5-R.Receipt"

                    Label9.Visible = True
                    Label52.Visible = True
                    PictureBox6.Enabled = True

                    PictureBox4.Enabled = True
                    PictureBox9.Visible = False

                    Label11.Visible = True
                    Label12.Visible = True
                    Label13.Visible = True
                    Label14.Visible = True
                    Label18.Visible = True

                    Label12.Text = "Cashier: " & user_id
                    Label14.Text = "Business Date: " & b_date
                    Label18.Text = "Cash Register# " & ws_id





                End If
            ''''''''''''''''''''''''''''''''''''''''''

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try


    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs)


    End Sub



    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Try
            If Label2.Visible = True Then
                Label2_Click(sender, e)
                Exit Sub

            End If

            If Label24.Visible = True Then
                Label24_Click(sender, e)

            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        Try
            If Label6.Visible = True Then
                Label6_Click(sender, e)
                Exit Sub
            End If

            If Label31.Visible = True Then
                Label31_Click(sender, e)
            End If


            If Label58.Visible = True Then
                Label58_Click(sender, e)
            End If


        Catch ex As Exception

        End Try









    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click
        Try
            If Label6.Text = "F6-Update" Then
                Try
                    My.Computer.Registry.CurrentUser.CreateSubKey("DBHOST")
                    My.Computer.Registry.SetValue("HKEY_CURRENT_USER\DBHOST", "DBHOSTKeyValue", MaskedTextBox1.Text)

                    Label3_Click(sender, e)



                Catch ex As Exception
                    'MessageBox.Show(ex.Message)
                End Try


            End If


            If Label6.Text = "F6-Tender" Or exchange_flag = 1 Then
                If Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text) < 0 Then
                    ' MessageBox.Show("Exchanged amount is less than the receipt..")
                    'MsgBox("Exchanged amount is less than the receipt..", MsgBoxStyle.Information, "ERROR")
                    Label17.Text = "ERROR10:Exchanged amount is less than the receipt."
                    Label54.Visible = False

                    Label17.Visible = True
                    Button1.Visible = True
                    ListView1.Visible = False
                    Panel1.Visible = False
                    Panel3.Visible = False
                    ListView2.Visible = False
                    TextBox1.Visible = False
                    ComboBox4.Visible = False
                    TextBox2.Visible = False
                    TextBox3.Visible = False
                    TextBox4.Visible = False
                    TextBox5.Visible = False
                    TextBox6.Visible = False
                    TextBox7.Visible = False
                    TextBox8.Visible = False
                    TextBox9.Visible = False
                    TextBox12.Visible = False
                    Label1.Visible = False
                    Label6.Visible = False
                    Label4.Visible = False
                    Label7.Visible = False
                    Label54.Visible = False


                    Exit Sub
                End If
                Label9.Visible = False
                Label52.Visible = False

                Label5.Visible = False
                Label53.Visible = False
                TextBox8.Visible = False

                subtotal = ListView2.Items(0).SubItems(0).Text
                Label6.Visible = False
                Label2.Text = "F2-Cash"
                Label8.Text = "F4-Credit"
                TextBox2.Enabled = True

                Label2.Visible = True
                Label8.Visible = True


                TextBox1.Visible = False
                ComboBox4.Visible = False
                TextBox12.Visible = False
                TextBox9.Visible = False
                TextBox2.Visible = True
                TextBox2.Text = ListView2.Items(0).SubItems(4).Text
                'Label4.Enabled = False
                Label3.Enabled = True
                Label3.Visible = True
                Label2.Enabled = True
                Label8.Enabled = True
                'Label7.Enabled = False
                Label4.Visible = False
                Label7.Visible = False
                TextBox9.Visible = False

                pos_flag = 0


                TextBox2.Focus()

                ListView1.Enabled = False

                Label1.Visible = True
                Label1.Text = "Enter tender value"

                ' MessageBox.Show("1")
            End If
            If return_flag = 1 Then
                'MessageBox.Show("2")
                return_flag = 0

                TextBox9.Visible = False
                TextBox12.Visible = False


                Label1.Text = "Enter an item number."
                Label1.Visible = True


                TextBox2.Clear()

                Label2.Visible = False

                Label8.Visible = False

                TextBox2.Visible = False
                TextBox1.Visible = True
                ComboBox4.Visible = True





                Label7.Visible = False
                Label4.Visible = False
                Label3.Visible = False

                TextBox7.Visible = False

                Label3.Visible = True

                TextBox1.Focus()

                ListView1.Enabled = True
                Label6.Text = "F6-Tender"
                Label6.Visible = False
                Label9.Visible = True
                Label52.Visible = True
                PictureBox6.Enabled = True
                PictureBox4.Enabled = True
                Label16.Visible = True
                Label24.Visible = True
                Label25.Visible = True
                Label31.Visible = True

                PictureBox10.Enabled = True







                ' --------------------------------------
                rn = RN_ID()
                Dim year As String
                Dim day As String
                Dim month As String
                Dim hour As String
                Dim minute As String


                year = Date.Today.Year.ToString
                day = Date.Today.Day.ToString
                month = Date.Today.Month.ToString
                hour = Date.Now.Hour.ToString
                minute = Date.Now.Minute.ToString


                rr = year + month + day + u_id + ws_id + rn.ToString
                rr_barcode = rr

                Dim conn As New OracleConnection
                Dim cmd As New OracleCommand
                Dim cmd1 As New OracleCommand
                Dim cmd2_1 As New OracleCommand
                Dim cmd2_2 As New OracleCommand
                Dim cmd2_3 As New OracleCommand
                Dim cmd3 As New OracleCommand
                Dim cmd4 As New OracleCommand
                Dim cmd5 As New OracleCommand
                Dim cmd_date As New OracleCommand
                Dim b_date As String = ""



                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                cmd_date.CommandText = "select business_date from daily_ops"
                cmd_date.Connection = conn

                conn.Open()
                Dim rd_date As OracleDataReader = cmd_date.ExecuteReader
                If rd_date.Read Then
                    b_date = rd_date.GetValue(0)

                End If
                rd_date.Close()
                conn.Close()


                cmd.CommandText = " insert into receipt_numbers values(TO_CHAR(SYSDATE, 'dd-mm-yy')," + rn + "," + rr + ",'" + ListView2.Items(0).SubItems(3).Text + "','" + ListView2.Items(0).SubItems(4).Text + "','" + ListView2.Items(0).SubItems(2).Text + "','" + user_id + "','CASH','REFUND',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh:mm:ss'),'" + b_date + "','') "
                cmd.Connection = conn

                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
                '--------------------------------
                cmd.CommandText = " insert into receipt_numbers_WS values(TO_CHAR(SYSDATE, 'dd-mm-yy')," + rn + "," + rr + ",'" + ListView2.Items(0).SubItems(3).Text + "','" + ListView2.Items(0).SubItems(4).Text + "','" + ListView2.Items(0).SubItems(2).Text + "','" + user_id + "','CASH','REFUND',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh:mm:ss'),'" + b_date + "',''," + ws_id + ") "
                cmd.Connection = conn

                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
                Label3.Visible = True
                Label2.Text = ""
                pos_flag = 1

                '------------------------------
                Dim cmd_DSF As New OracleCommand
                For i = 0 To ListView1.Items.Count - 1



                    cmd_DSF.CommandText = "insert into DSF values('" + u_id + "','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(1).Text + "','" + ListView1.Items(i).SubItems(2).Text + "','" + b_date + "','" + System.DateTime.Now + "','Refund','" + ListView1.Items(i).SubItems(5).Text + "','" + user_id + "')"
                    cmd_DSF.Connection = conn

                    conn.Open()
                    cmd_DSF.ExecuteNonQuery()
                    conn.Close()
                    Dim path As String = "C:\POS\OUT"
                    If Directory.Exists(path) Then
                        '------------------------------------------Print open report
                        Dim file_DSF As System.IO.StreamWriter
                        file_DSF = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\OUT\DSF_" + b_date + ".txt", True)

                        file_DSF.WriteLine(u_id & "|" & ListView1.Items(i).SubItems(0).Text & "|" & ListView1.Items(i).SubItems(1).Text & "|" & ListView1.Items(i).SubItems(2).Text & "|" & b_date & "|" & System.DateTime.Now & "|" & "Refund" & "|" & ListView1.Items(i).SubItems(5).Text & "|" & user_id)


                        file_DSF.Close()
                    End If
                Next
                '------------------------------------------

                For i = 0 To ListView1.Items.Count - 1
                    AI_TRN = rn & i.ToString


                    cmd1.CommandText = "insert into tr_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",'admin',1,'',sysdate,sysdate,0,0,2,'','',0,'','','-1','-1','-1',0,0,sysdate,sysdate,0)"
                    cmd1.Connection = conn

                    conn.Open()
                    cmd1.ExecuteNonQuery()
                    conn.Close()


                    cmd2_1.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'SR','','',0,-1,sysdate,sysdate)"
                    cmd2_1.Connection = conn

                    conn.Open()
                    cmd2_1.ExecuteNonQuery()
                    conn.Close()

                    cmd2_2.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",1,'TX','','',0,-1,sysdate,sysdate)"
                    cmd2_2.Connection = conn

                    conn.Open()
                    cmd2_2.ExecuteNonQuery()
                    conn.Close()

                    cmd2_3.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",2,'TN','','',0,-1,sysdate,sysdate)"
                    cmd2_3.Connection = conn

                    conn.Open()
                    cmd2_3.ExecuteNonQuery()
                    conn.Close()

                    Dim quan As Integer = ListView1.Items(i).SubItems(3).Text
                    cmd3.CommandText = "insert into tr_ltm_sls_rtn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'',0,'1601','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(0).Text + "','" + quan.ToString + "','" + (Double.Parse(ListView1.Items(i).SubItems(2).Text) / CF).ToString + "','" + (Double.Parse(ListView1.Items(i).SubItems(5).Text) / CF).ToString + "',0,'',0,'',0,'','KEY','','',0,'','','-1','','',0,'','',0,0,'" + rr_refund + "','','',0,0,0,0,'',0,'" + rr + "',1,-1,1,0,0,sysdate,sysdate,0,'" + (Double.Parse(ListView1.Items(i).SubItems(2).Text)).ToString + "')"
                    cmd3.Connection = conn

                    conn.Open()
                    cmd3.ExecuteNonQuery()
                    conn.Close()
                    cmd4.CommandText = "delete  from TR_LTM_SLS_RTN_RECEIPT where lu_prc_adj_rfn_id = '" + rr_refund + "'  and id_itm='" + ListView1.Items(i).SubItems(0).Text + "' and AI_TRN = '" + ListView1.Items(i).SubItems(4).Text + "' and '" + ListView1.Items(i).SubItems(3).Text + "' = '-1'"
                    cmd4.Connection = conn
                    conn.Open()

                    cmd4.ExecuteNonQuery()
                    conn.Close()


                Next



                rr_refund = 0


                '''''''''''''''''''generate barcode
                Try
                    PictureBox12.Image = Nothing

                    Dim year1 As String = Date.Today.Year.ToString
                    Dim day1 As String = Date.Today.Day.ToString
                    Dim month1 As String = Date.Today.Month.ToString
                    Dim hour1 As String = Date.Now.Hour.ToString
                    Dim minute1 As String = Date.Now.Minute.ToString
                    Dim Second As String = Date.Now.Second.ToString

                    Dim rr1 As String = year + month + day + hour + minute + Second


                    'ID Automation
                    'Free only with the Code39 and Code39Ext

                    '-----------------------------------------------------print receipt








                    '---------------
                    ' Create a linear barcode image object (BarcodeLib.Barcode.Linear)

                Catch ex As Exception

                End Try

                If receipt_lang = "1" Then


                    '-----------------------------------------------------print receipt
                    Dim file As System.IO.StreamWriter
                    file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\" + rr + ".txt", True)

                    file.WriteLine("    " & company_name)
                    file.WriteLine("------------------")
                    file.WriteLine("    " & store_name)
                    file.WriteLine("------------------")
                    file.WriteLine("    " & address)
                    file.WriteLine("    " & telephone)
                    file.WriteLine("------------------")
                    file.WriteLine("    REFUND")
                    'file.WriteLine("    ترجيع")
                    file.WriteLine("------------------")
                    file.WriteLine("    " & System.DateTime.Now)
                    file.WriteLine("------------------")
                    file.WriteLine("    " & rr.ToString)
                    file.WriteLine("    ORG:" & org)
                    file.WriteLine("------------------")
                    file.WriteLine("    VAT: ضَريبةِ القيمةِ المُضافةِ:")
                    file.WriteLine("    " & vat_number)
                    file.WriteLine("------------------")
                    file.WriteLine("    " & "فاتورة ضريبة مبسطة")
                    file.WriteLine("    Simplified Tax Invoice")
                    file.WriteLine("------------------")
                    file.WriteLine("                  ")

                    For i = 0 To ListView1.Items.Count - 1
                        file.WriteLine("    " & (i + 1).ToString & "- " & ListView1.Items(i).SubItems(0).Text & "      " & ListView1.Items(i).SubItems(2).Text & " (" & ListView1.Items(i).SubItems(3).Text & ")")
                        file.WriteLine("    " & ListView1.Items(i).SubItems(1).Text)
                        'file.WriteLine("                  ")
                    Next
                    file.WriteLine("------------------")
                    'file.WriteLine("")
                    'file.WriteLine()
                    'file.WriteLine("")
                    'file.WriteLine()
                    'file.WriteLine("")
                    'file.WriteLine()

                    'file.WriteLine("")
                    'file.WriteLine()
                    'file.WriteLine("")
                    'file.WriteLine("")
                    'file.WriteLine()
                    'file.WriteLine("")
                    'file.WriteLine("")
                    'file.WriteLine("")
                    'file.WriteLine()

                    'file.WriteLine("")
                    'file.WriteLine()
                    file.WriteLine("    Total Refund  " & ListView2.Items(0).SubItems(4).Text)
                    'file.WriteLine("VAT%    " & vat * Int32.Parse(ListView2.Items(0).SubItems(4).Text))
                    'file.WriteLine("Cash    " & TextBox2.Text)

                    'file.WriteLine("CHANGE  " & (Convert.ToDecimal(TextBox2.Text) - Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text)).ToString)
                    'file.WriteLine()

                    file.WriteLine("==============================")
                    file.WriteLine("    VAT Summary | ملخص الضرائب")
                    file.WriteLine("    % VAT Rates: " & vat & " :% معدل الضريبة")
                    file.WriteLine("    VAT: " & (Double.Parse(ListView2.Items(0).SubItems(2).Text) & " :الضرائب"))
                    file.WriteLine("    Inc VAT: " & Double.Parse(ListView2.Items(0).SubItems(4).Text) & " :متضمن للضريبة")
                    file.WriteLine("==============================")

                    'file.WriteLine("-----------------------")
                    file.WriteLine("    " & policy)
                    'file.WriteLine("    Thank You For Shopping")
                    file.WriteLine("                  ")



                    file.Close()

                    vat_value = (Double.Parse(ListView2.Items(0).SubItems(2).Text))
                    receipt_value = Double.Parse(ListView2.Items(0).SubItems(4).Text).ToString

                    '  Dim proc As New Process
                    '
                    '
                    '  proc.StartInfo.FileName = "C:\POS\Receipts\" + rr + ".txt"
                    '
                    '
                    '  proc.StartInfo.Verb = "Print"
                    '
                    '
                    '  proc.StartInfo.CreateNoWindow = True
                    '
                    If printer_status = "1" Then
                        Try
                            AddHandler PrintZatcaQR.PrintPage, AddressOf Me.PrintImage
                            PrintZatcaQR.Print()
                        Catch ex As Exception
                            MsgBox(ex, MsgBoxStyle.Critical, "Error during Print")
                        End Try

                        'proc.Start()

                        'proc.WaitForExit()
                        'PrintDocument1.Print()
                    End If

                End If

                If receipt_lang = "2" Then


                    '-----------------------------------------------------print receipt
                    Dim file As System.IO.StreamWriter
                    file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\" + rr + ".txt", True)

                    file.WriteLine("    " & company_name)
                    file.WriteLine("------------------")
                    file.WriteLine("    " & store_name)
                    file.WriteLine("------------------")
                    file.WriteLine("    " & address)
                    file.WriteLine("    " & telephone)
                    file.WriteLine("------------------")
                    file.WriteLine("    REFUND")
                    file.WriteLine("    ترجيع")
                    file.WriteLine("------------------")
                    file.WriteLine("    " & System.DateTime.Now)
                    file.WriteLine("------------------")
                    file.WriteLine("    " & rr.ToString)
                    file.WriteLine("    ORG:" & org)
                    file.WriteLine("------------------")
                    file.WriteLine("    VAT: ضَريبةِ القيمةِ المُضافةِ:")
                    file.WriteLine("    " & vat_number)
                    file.WriteLine("------------------")
                    file.WriteLine("    فاتورة ضريبة مبسطة")
                    file.WriteLine("    Simplified Tax Invoice")
                    file.WriteLine("------------------")
                    file.WriteLine("                  ")
                    For i = 0 To ListView1.Items.Count - 1
                        file.WriteLine("    " & (i + 1).ToString & "- " & ListView1.Items(i).SubItems(0).Text & "      " & ListView1.Items(i).SubItems(2).Text & " (" & ListView1.Items(i).SubItems(3).Text & ")")
                        file.WriteLine("    " & ListView1.Items(i).SubItems(1).Text)
                        'file.WriteLine("                  ")
                    Next
                    file.WriteLine("------------------")
                    'file.WriteLine("")
                    'file.WriteLine()
                    'file.WriteLine("")
                    'file.WriteLine()
                    'file.WriteLine("")
                    'file.WriteLine()

                    'file.WriteLine("")
                    'file.WriteLine()
                    'file.WriteLine("")
                    'file.WriteLine("")
                    'file.WriteLine()
                    'file.WriteLine("")
                    'file.WriteLine("")
                    'file.WriteLine("")
                    'file.WriteLine()

                    'file.WriteLine("")
                    'file.WriteLine()
                    file.WriteLine("    Total Refund  " & ListView2.Items(0).SubItems(4).Text & "  أجمالي المبلغ المسترد")
                    'file.WriteLine("VAT%    " & vat * Int32.Parse(ListView2.Items(0).SubItems(4).Text))
                    'file.WriteLine("Cash    " & TextBox2.Text)

                    'file.WriteLine("CHANGE  " & (Convert.ToDecimal(TextBox2.Text) - Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text)).ToString)
                    'file.WriteLine()


                    file.WriteLine("==============================")
                    file.WriteLine("    VAT Summary | ملخص الضرائب")
                    file.WriteLine("    % VAT Rates: " & vat & " :% معدل الضريبة")
                    file.WriteLine("    VAT: " & (Double.Parse(ListView2.Items(0).SubItems(2).Text) & " :الضرائب"))
                    file.WriteLine("    Inc VAT: " & Double.Parse(ListView2.Items(0).SubItems(4).Text) & " :متضمن للضريبة")
                    file.WriteLine("==============================")

                    file.WriteLine("    " & policy)
                    'file.WriteLine("    Thank You For Shopping")
                    file.WriteLine("                  ")



                    file.Close()
                    vat_value = (Double.Parse(ListView2.Items(0).SubItems(2).Text))
                    receipt_value = Double.Parse(ListView2.Items(0).SubItems(4).Text).ToString
                    '  Dim proc As New Process
                    '
                    '
                    '  proc.StartInfo.FileName = "C:\POS\Receipts\" + rr + ".txt"
                    '
                    '
                    '  proc.StartInfo.Verb = "Print"
                    '
                    '
                    '  proc.StartInfo.CreateNoWindow = True

                    If printer_status = "1" Then

                        Try
                            AddHandler PrintZatcaQR.PrintPage, AddressOf Me.PrintImage
                            PrintZatcaQR.Print()
                        Catch ex As Exception
                            MsgBox(ex, MsgBoxStyle.Critical, "Error during Print")
                        End Try
                        'proc.Start()

                        'proc.WaitForExit()
                        'PrintDocument1.Print()
                    End If

                End If
                ListView1.Items.Clear()
                ListView2.Items(0).SubItems(0).Text = "0"
                ListView2.Items(0).SubItems(1).Text = "0"
                ListView2.Items(0).SubItems(2).Text = "0"
                ListView2.Items(0).SubItems(3).Text = "0"
                ListView2.Items(0).SubItems(4).Text = "0"
                If com_status = 1 Then
                    Dim sp = New SerialPort(com_port, 9600, Parity.None, 8, StopBits.One)
                    sp.Open()
                    sp.Write(Convert.ToString(Chr(12)))
                    sp.Write("Thanks for shopping")
                    sp.Close()
                End If
                TextBox2.Enabled = True
                PictureBox2.Enabled = True
                PictureBox3.Enabled = True
                PictureBox5.Enabled = True
                PictureBox6.Enabled = True
                PictureBox10.Enabled = True
                Label54.Visible = False

            End If


        Catch ex As Exception

            MsgBox(ex.Message)
        End Try
    End Sub





    Public Function get_id() As Double
        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim id As Double
        'Dim u_id, pw, ip As String


        'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


        'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

        ' ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")

        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
        cmd.CommandText = "select id_ev from MA_ITM_PRN_PRC_ITM where id_itm='" + TextBox1.Text.Trim + "'"
        cmd.Connection = conn

        Try
            conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader
            If rd.Read Then

                id = rd.GetValue(0)

            End If
            Return id
            rd.Close()
            conn.Close()

        Catch ex As Exception
            'MessageBox.Show(ex.Message)

        End Try
        'select id_ev from MA_ITM_PRN_PRC_ITM where id_itm='8904150313909'
    End Function
    Public Function get_id1() As Double
        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim id As Double
        'Dim u_id, pw, ip As String


        'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


        'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

        ' ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")

        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
        cmd.CommandText = "select id_ev from MA_ITM_PRN_PRC_ITM where id_itm='" + TextBox9.Text.Trim + "'"
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
        'select id_ev from MA_ITM_PRN_PRC_ITM where id_itm='8904150313909'
    End Function
    Public Sub cls_display()

        Dim sp = New SerialPort("COM1", 9600, Parity.None, 8, StopBits.One)
        sp.Write(Convert.ToString(Chr(12)))
        'first line goes here
        sp.WriteLine(itm_id + "       " + "0" + Chr(13))
        '2nd line goes here
        sp.WriteLine("Total" + "       " + "0")
        sp.Dispose()
        sp.Close()


    End Sub
    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        Try
            If exchange_flag = 1 Then



                If Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text) >= 0 Then


                    If Convert.ToDecimal(TextBox2.Text) < Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text) Then
                        'MessageBox.Show("the amount of money is less than receipt total.")
                        'MsgBox("the amount of money is less than receipt total.", MsgBoxStyle.Information, "ERROR")
                        Label17.Text = "ERROR7:The amount of money is less than receipt total."
                        Label17.Visible = True
                        Label17.Visible = True
                        Button1.Visible = True
                        ListView1.Visible = False
                        Panel1.Visible = False
                        Panel3.Visible = False
                        ListView2.Visible = False
                        TextBox2.Clear()

                        TextBox1.Visible = False
                        ComboBox4.Visible = False
                        TextBox2.Visible = False
                        TextBox3.Visible = False
                        TextBox4.Visible = False
                        TextBox5.Visible = False
                        TextBox6.Visible = False
                        TextBox7.Visible = False
                        TextBox8.Visible = False
                        TextBox9.Visible = False
                        Label1.Visible = False
                        Label2.Visible = False
                        Label8.Visible = False
                        Label3.Visible = False
                        Label54.Visible = False
                        Exit Sub


                    Else
                        '
                        Label1.Text = "Close Cash Drawer...."



                        TextBox1.Focus()
                        Label2.Visible = False

                        Label8.Visible = False

                        'TextBox2.Visible = False
                        TextBox1.Visible = True
                        ComboBox4.Visible = True
                        TextBox1.Enabled = True





                        Label7.Visible = False
                        Label4.Visible = False
                        Label3.Visible = False
                        Label6.Visible = False
                        Label9.Visible = False
                        Label52.Visible = False


                        Label6.Text = "F6-Tender"
                        Label5.Visible = False
                        Label53.Visible = False
                        Label16.Visible = True
                        Label24.Visible = True
                        Label25.Visible = True
                        Label31.Visible = True
                        PictureBox10.Enabled = True
                        TextBox1.Focus()

                        ListView1.Enabled = True






                        ' --------------------------------------
                        rn = RN_ID()
                        Dim year As String
                        Dim day As String
                        Dim month As String
                        Dim hour As String
                        Dim minute As String


                        year = Date.Today.Year.ToString
                        day = Date.Today.Day.ToString
                        month = Date.Today.Month.ToString
                        hour = Date.Now.Hour.ToString
                        minute = Date.Now.Minute.ToString


                        rr = year + month + day + u_id + ws_id + rn.ToString
                        rr_barcode = rr

                        Dim conn As New OracleConnection
                        Dim cmd As New OracleCommand
                        Dim cmd1 As New OracleCommand
                        Dim cmd2_1 As New OracleCommand
                        Dim cmd2_2 As New OracleCommand
                        Dim cmd2_3 As New OracleCommand
                        Dim cmd3 As New OracleCommand
                        Dim cmd4 As New OracleCommand
                        Dim cmd5 As New OracleCommand
                        Dim cmd_date As New OracleCommand
                        Dim b_date As String = ""


                        Threading.Thread.Sleep(2000) ' 500 milliseconds = 0.5 seconds
                        If conn.State = ConnectionState.Open Then
                            conn.Close()
                        End If

                        'Buy x get y discount
                        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                        cmd_date.CommandText = "select business_date from daily_ops"
                        cmd_date.Connection = conn

                        conn.Open()
                        Dim rd_date As OracleDataReader = cmd_date.ExecuteReader
                        If rd_date.Read Then
                            b_date = rd_date.GetValue(0)

                        End If
                        rd_date.Close()
                        'conn.Close()

                        cmd.CommandText = " insert into receipt_numbers values(TO_CHAR(SYSDATE, 'dd-mm-yy')," + rn + "," + rr + ",'" + ListView2.Items(0).SubItems(3).Text + "','" + ListView2.Items(0).SubItems(4).Text + "','" + ListView2.Items(0).SubItems(2).Text + "','" + user_id + "','CASH','EXCHANGE',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh:mm:ss'),'" + b_date + "','') "
                        cmd.Connection = conn

                        'conn.Open()
                        cmd.ExecuteNonQuery()
                        'conn.Close()

                        cmd.CommandText = " insert into receipt_numbers_WS values(TO_CHAR(SYSDATE, 'dd-mm-yy')," + rn + "," + rr + ",'" + ListView2.Items(0).SubItems(3).Text + "','" + ListView2.Items(0).SubItems(4).Text + "','" + ListView2.Items(0).SubItems(2).Text + "','" + user_id + "','CASH','EXCHANGE',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh:mm:ss'),'" + b_date + "',''," + ws_id + ") "
                        cmd.Connection = conn

                        'conn.Open()
                        cmd.ExecuteNonQuery()
                        'conn.Close()
                        Label3.Visible = True
                        Label2.Text = ""
                        pos_flag = 1
                        Dim cmd_DSF As New OracleCommand
                        For i = 0 To ListView1.Items.Count - 1



                            cmd_DSF.CommandText = "insert into DSF values('" + u_id + "','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(1).Text + "','" + ListView1.Items(i).SubItems(2).Text + "','" + b_date + "','" + System.DateTime.Now + "','Exchange','" + ListView1.Items(i).SubItems(5).Text + "','" + user_id + "')"
                            cmd_DSF.Connection = conn

                            'conn.Open()
                            cmd_DSF.ExecuteNonQuery()
                            'conn.Close()
                            Dim path As String = "C:\POS\OUT"
                            If Directory.Exists(path) Then
                                '------------------------------------------Print open report
                                Dim file_DSF As System.IO.StreamWriter
                                file_DSF = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\OUT\DSF_" + b_date + ".txt", True)

                                file_DSF.WriteLine(u_id & "|" & ListView1.Items(i).SubItems(0).Text & "|" & ListView1.Items(i).SubItems(1).Text & "|" & ListView1.Items(i).SubItems(2).Text & "|" & b_date & "|" & System.DateTime.Now & "|" & "Exchange" & "|" & ListView1.Items(i).SubItems(5).Text & "|" & user_id)


                                file_DSF.Close()
                            End If
                        Next

                        For i = 0 To ListView1.Items.Count - 1
                            AI_TRN = rn & i.ToString


                            cmd1.CommandText = "insert into tr_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",'admin',1,'',sysdate,sysdate,0,0,2,'','',0,'','','-1','-1','-1',0,0,sysdate,sysdate,0)"
                            cmd1.Connection = conn

                            'conn.Open()
                            cmd1.ExecuteNonQuery()
                            'conn.Close()


                            cmd2_1.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'SR','','',0,-1,sysdate,sysdate)"
                            cmd2_1.Connection = conn

                            'conn.Open()
                            cmd2_1.ExecuteNonQuery()
                            'conn.Close()

                            cmd2_2.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",1,'TX','','',0,-1,sysdate,sysdate)"
                            cmd2_2.Connection = conn

                            'conn.Open()
                            cmd2_2.ExecuteNonQuery()
                            'conn.Close()

                            cmd2_3.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",2,'TN','','',0,-1,sysdate,sysdate)"
                            cmd2_3.Connection = conn

                            'conn.Open()
                            cmd2_3.ExecuteNonQuery()
                            'conn.Close()

                            Dim quan As Integer = ListView1.Items(i).SubItems(3).Text
                            cmd3.CommandText = "insert into tr_ltm_sls_rtn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'',0,'1601','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(0).Text + "','" + quan.ToString + "','" + (Double.Parse(ListView1.Items(i).SubItems(2).Text) / CF).ToString + "','" + (Double.Parse(ListView1.Items(i).SubItems(5).Text) / CF).ToString + "',0,'',0,'',0,'','KEY','','',0,'','','-1','','',0,'','',0,0,'','','',0,0,0,0,'',0,'" + rr + "',1,-1,1,0,0,sysdate,sysdate,0,'" + (Double.Parse(ListView1.Items(i).SubItems(2).Text) / CF).ToString + "')"
                            cmd3.Connection = conn

                            'conn.Open()
                            cmd3.ExecuteNonQuery()
                            'conn.Close()

                            If ListView1.Items(i).SubItems(3).Text.ToString = "1" Then
                                cmd4.CommandText = "insert into TR_LTM_SLS_RTN_RECEIPT values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'',0,'1601','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(0).Text + "','" + quan.ToString + "','" + Convert.ToDecimal(ListView1.Items(i).SubItems(2).Text).ToString + "','" + Convert.ToDecimal(ListView1.Items(i).SubItems(5).Text).ToString + "',0,'',0,'',0,'','KEY','','',0,'','','-1','','',0,'','',0,0,'','','',0,0,0,0,'',0,'" + rr + "',1,-1,1,0,0,sysdate,sysdate,0,'" + Convert.ToDecimal(ListView1.Items(i).SubItems(2).Text).ToString + "')"
                                cmd4.Connection = conn

                                'conn.Open()
                                cmd4.ExecuteNonQuery()
                                'conn.Close()
                                ' MessageBox.Show("done")
                            End If
                            If ListView1.Items(i).SubItems(3).Text.ToString = "-1" Then
                                cmd5.CommandText = "delete  from TR_LTM_SLS_RTN_RECEIPT where lu_prc_adj_rfn_id = '" + exchanged_receipt + "'  and id_itm='" + ListView1.Items(i).SubItems(0).Text + "' and AI_TRN = '" + ListView1.Items(i).SubItems(4).Text + "' "
                                cmd5.Connection = conn
                                'conn.Open()

                                cmd5.ExecuteNonQuery()
                                'conn.Close()
                                'MessageBox.Show("remove")
                            End If

                        Next

                        '''''''''''''''''''generate barcode
                        Try
                            PictureBox12.Image = Nothing

                            Dim year1 As String = Date.Today.Year.ToString
                            Dim day1 As String = Date.Today.Day.ToString
                            Dim month1 As String = Date.Today.Month.ToString
                            Dim hour1 As String = Date.Now.Hour.ToString
                            Dim minute1 As String = Date.Now.Minute.ToString
                            Dim Second As String = Date.Now.Second.ToString

                            Dim rr1 As String = year + month + day + hour + minute + Second


                            'ID Automation
                            'Free only with the Code39 and Code39Ext









                            '---------------
                            ' Create a linear barcode image object (BarcodeLib.Barcode.Linear)

                        Catch ex As Exception
                        Finally
                            conn.Close()

                        End Try

                        If receipt_lang = "1" Then


                            '-----------------------------------------------------print receipt
                            Dim file As System.IO.StreamWriter
                            file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\" + rr + ".txt", True)

                            file.WriteLine(company_name)
                            file.WriteLine(store_name)

                            file.WriteLine("------------------")
                            file.WriteLine("    " & address)
                            file.WriteLine("    " & telephone)
                            file.WriteLine("------------------")
                            file.WriteLine("    EXCHANGE")
                            'file.WriteLine("    تبــديل")
                            file.WriteLine("------------------")
                            file.WriteLine("    " & System.DateTime.Now)
                            file.WriteLine("------------------")
                            file.WriteLine("    " & rr.ToString)
                            file.WriteLine("    ORG:" & org)
                            file.WriteLine("------------------")
                            file.WriteLine("    VAT: ضَريبةِ القيمةِ المُضافةِ:")
                            file.WriteLine("    " & vat_number)
                            file.WriteLine("------------------")
                            file.WriteLine("    فاتورة ضريبة مبسطة")
                            file.WriteLine("    Simplified Tax Invoice")
                            file.WriteLine("------------------")
                            file.WriteLine("                  ")
                            For i = 0 To ListView1.Items.Count - 1
                                file.WriteLine("    " & (i + 1).ToString & "- " & ListView1.Items(i).SubItems(0).Text & "      " & ListView1.Items(i).SubItems(2).Text & " (" & ListView1.Items(i).SubItems(5).Text & ")" & " (" & ListView1.Items(i).SubItems(3).Text & ")")
                                file.WriteLine("    " & ListView1.Items(i).SubItems(1).Text)
                                'file.WriteLine("                  ")
                            Next
                            'file.WriteLine("------------------")
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine()

                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine()
                            file.WriteLine("-----------------------")


                            file.WriteLine("    Subtotal   " & subtotal)
                            file.WriteLine("    Discount   " & ListView2.Items(0).SubItems(1).Text)
                            'file.WriteLine("    VAT%    " & ListView2.Items(0).SubItems(2).Text)
                            file.WriteLine("    Total   " & ListView2.Items(0).SubItems(4).Text)


                            file.WriteLine("    Cash    " & TextBox2.Text)

                            file.WriteLine("    CHANGE  " & (Convert.ToDecimal(TextBox2.Text) - Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text)).ToString)
                            'file.WriteLine()

                            file.WriteLine("==============================")
                            file.WriteLine("    VAT Summary | ملخص الضرائب")
                            file.WriteLine("    % VAT Rates: " & vat & " :% معدل الضريبة")
                            file.WriteLine("    VAT: " & (Double.Parse(ListView2.Items(0).SubItems(2).Text) & " :الضرائب"))
                            file.WriteLine("    Inc VAT: " & Double.Parse(ListView2.Items(0).SubItems(4).Text) & " :متضمن للضريبة")
                            file.WriteLine("==============================")


                            file.WriteLine("    " & policy)
                            'file.WriteLine("    Thank You For Shopping")

                            file.WriteLine("                        ")



                            file.Close()
                            vat_value = (Double.Parse(ListView2.Items(0).SubItems(2).Text))
                            receipt_value = Double.Parse(ListView2.Items(0).SubItems(4).Text).ToString
                            ' Dim proc As New Process
                            '
                            '
                            ' proc.StartInfo.FileName = "C:\POS\Receipts\" + rr + ".txt"
                            '
                            '
                            ' proc.StartInfo.Verb = "Print"
                            '
                            '
                            ' proc.StartInfo.CreateNoWindow = True

                            If printer_status = "1" Then
                                Try
                                    AddHandler PrintZatcaQR.PrintPage, AddressOf Me.PrintImage
                                    PrintZatcaQR.Print()
                                Catch ex As Exception
                                    MsgBox(ex, MsgBoxStyle.Critical, "Error during Print")
                                End Try

                                'proc.Start()

                                ' proc.WaitForExit()
                                'PrintDocument1.Print()
                            End If

                        End If

                        If receipt_lang = "2" Then


                            '-----------------------------------------------------print receipt
                            Dim file As System.IO.StreamWriter
                            file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\" + rr + ".txt", True)

                            file.WriteLine(company_name)
                            file.WriteLine(store_name)

                            file.WriteLine("------------------")
                            file.WriteLine("    " & address)
                            file.WriteLine("    " & telephone)
                            file.WriteLine("------------------")
                            file.WriteLine("    EXCHANGE")
                            file.WriteLine("    تبــديل")
                            file.WriteLine("------------------")
                            file.WriteLine("    " & System.DateTime.Now)
                            file.WriteLine("------------------")
                            file.WriteLine("    " & rr.ToString)
                            file.WriteLine("    ORG:" & org)
                            file.WriteLine("------------------")
                            file.WriteLine("    VAT: ضَريبةِ القيمةِ المُضافةِ:")
                            file.WriteLine("    " & vat_number)
                            file.WriteLine("------------------")
                            file.WriteLine("    " & "فاتورة ضريبة مبسطة")
                            file.WriteLine("    Simplified Tax Invoice")
                            file.WriteLine("------------------")
                            file.WriteLine("                  ")
                            For i = 0 To ListView1.Items.Count - 1
                                file.WriteLine("    " & (i + 1).ToString & "- " & ListView1.Items(i).SubItems(0).Text & "      " & ListView1.Items(i).SubItems(2).Text & " (" & ListView1.Items(i).SubItems(5).Text & ")" & " (" & ListView1.Items(i).SubItems(3).Text & ")")
                                file.WriteLine("    " & ListView1.Items(i).SubItems(1).Text)
                                'file.WriteLine("                  ")
                            Next
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine()

                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine()
                            file.WriteLine("-----------------------")


                            file.WriteLine("    Subtotal   " & subtotal & "     المجموع الفرعي")
                            file.WriteLine("    Discount   " & ListView2.Items(0).SubItems(1).Text & "     مجموع التخفيضات")
                            'file.WriteLine("    VAT%    " & ListView2.Items(0).SubItems(2).Text & "     ملخص الضريبة")
                            file.WriteLine("    Total   " & ListView2.Items(0).SubItems(4).Text & "     المجموع الكلي")


                            file.WriteLine("    Cash    " & TextBox2.Text & "       نقد")

                            file.WriteLine("    CHANGE  " & (Convert.ToDecimal(TextBox2.Text) - Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text)).ToString & "        المتبقي")
                            'file.WriteLine()

                            file.WriteLine("==============================")
                            file.WriteLine("    VAT Summary | ملخص الضرائب")
                            file.WriteLine("    % VAT Rates: " & vat & " :% معدل الضريبة")
                            file.WriteLine("    VAT: " & (Double.Parse(ListView2.Items(0).SubItems(2).Text) & " :الضرائب"))
                            file.WriteLine("    Inc VAT: " & Double.Parse(ListView2.Items(0).SubItems(4).Text) & " :متضمن للضريبة")
                            file.WriteLine("==============================")



                            file.WriteLine("    " & policy)
                            'file.WriteLine("    Thank You For Shopping")

                            file.WriteLine("                        ")


                            file.Close()
                            vat_value = (Double.Parse(ListView2.Items(0).SubItems(2).Text))
                            receipt_value = Double.Parse(ListView2.Items(0).SubItems(4).Text).ToString
                            ' Dim proc As New Process
                            '
                            '
                            ' proc.StartInfo.FileName = "C:\POS\Receipts\" + rr + ".txt"
                            '
                            '
                            ' proc.StartInfo.Verb = "Print"
                            '
                            '
                            ' proc.StartInfo.CreateNoWindow = True

                            If printer_status = "1" Then

                                Try
                                    AddHandler PrintZatcaQR.PrintPage, AddressOf Me.PrintImage
                                    PrintZatcaQR.Print()
                                Catch ex As Exception
                                    MsgBox(ex, MsgBoxStyle.Critical, "Error during Print")
                                End Try
                                ' proc.Start()

                                ' proc.WaitForExit()
                                'PrintDocument1.Print()
                            End If

                        End If

                        ListView1.Items.Clear()

                        ListView2.Items(0).SubItems(0).Text = "0"
                        ListView2.Items(0).SubItems(1).Text = "0"
                        ListView2.Items(0).SubItems(2).Text = "0"
                        ListView2.Items(0).SubItems(3).Text = "0"
                        ListView2.Items(0).SubItems(4).Text = "0"
                        TextBox2.Clear()
                        TextBox2.Visible = False
                        Label1.Text = "Enter an item number."
                        If com_status = 1 Then
                            Dim sp = New SerialPort(com_port, 9600, Parity.None, 8, StopBits.One)
                            sp.Open()
                            sp.Write(Convert.ToString(Chr(12)))
                            sp.Write("Thanks for shopping")
                            sp.Close()
                        End If

                        TextBox7.Clear()
                        TextBox7.Visible = False

                        TextBox9.Visible = False
                        TextBox1.Visible = True
                        ComboBox4.Visible = True

                        TextBox1.Focus()
                        Label5.Text = "F4-E.Receipt"
                        Label5.Location = New Point(650, 420)
                        Label5.Visible = False
                        Label53.Visible = False


                        Label9.Visible = True
                        Label52.Visible = True

                        PictureBox4.Enabled = True
                        Label9.Text = "F5-R.Receipt"
                        exchange_flag = 0
                        PictureBox2.Enabled = True
                        PictureBox3.Enabled = True
                        PictureBox5.Enabled = True
                        PictureBox6.Enabled = True
                        PictureBox10.Enabled = True
                        Label54.Visible = False

                    End If
                Else
                    'MessageBox.Show("Exchanged amount is less than the receipt..")
                    'MsgBox("Exchanged amount is less than the receipt..", MsgBoxStyle.Information, "ERROR")
                    Label17.Text = "ERROR:Exchanged amount is less than the receipt.."
                    Label17.Visible = True
                    Label17.Visible = True
                    Button1.Visible = True
                    ListView1.Visible = False
                    Panel1.Visible = False
                    Panel3.Visible = False
                    ListView2.Visible = False
                    TextBox1.Enabled = False
                    TextBox2.Enabled = False
                    TextBox3.Enabled = False
                    TextBox4.Enabled = False
                    TextBox5.Enabled = False
                    TextBox6.Enabled = False
                    TextBox7.Enabled = False
                    TextBox8.Enabled = False
                    TextBox9.Enabled = False
                    Label1.Visible = False
                    Label2.Enabled = False
                    Label3.Enabled = False
                    Label4.Enabled = False
                    Label5.Enabled = False
                    Label6.Enabled = False
                    Label7.Enabled = False
                    Label8.Enabled = False
                    Label9.Enabled = False
                    Label16.Enabled = False


                End If
                Exit Sub
            End If




            log_flag = get_status()
            If Label2.Text = "F2-Pole" Then
                ComboBox1.Visible = True
                Label10.Visible = True
                Label8.Visible = False
                Label2.Visible = False

                Label55.Visible = False
                Label57.Visible = False
                Label58.Visible = False
                Label60.Visible = False
                Label7.Visible = False
                Label2.Text = ""
                Label3.Visible = True
                TextBox3.Visible = False
                Label1.Visible = True
                Label1.Text = "Select port of the pole display.."


                ' Show all available COM ports.
                ComboBox1.Items.Clear()

                For Each sp As String In My.Computer.Ports.SerialPortNames

                    ComboBox1.Items.Add(sp)



                Next
                'ComboBox1.Text = ComboBox1.Items(0).ToString

                If ComboBox1.Items.Count = 0 Then
                    ComboBox1.Items.Add("NO COM Port available")
                    Label3.Text = "F10-Exit"
                Else
                    Label3.Text = "F10-Save"
                End If


            End If

            If Label2.Text = "F2-POS" Then
                PictureBox9.Visible = False
                TextBox3.Enabled = True
                TextBox3.Visible = True
                TextBox3.Focus()
                Label2.Visible = False

                Label8.Visible = False
                Label5.Visible = False
                Label53.Visible = False
                Label3.Visible = False

                Label7.Text = "F12-Undo"
                Label7.Location = New Point(258, 510)
                Label7.Visible = True

                Label1.Visible = True
                Label1.Text = "Enter user name"
                Label9.Visible = False
                Label52.Visible = False

                sale_flag = 1
                user_flag = 1
                sys_admin_flag = 0



            End If


            If Label2.Text = "F2-Cash" And exchange_flag = 0 Then
                Threading.Thread.Sleep(2000) ' 500 milliseconds = 0.5 seconds

                If Convert.ToDecimal(TextBox2.Text) < Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text) Then
                    'MessageBox.Show("the amount of money is less than receipt total.")
                    ' MsgBox("the amount of money is less than receipt total..", MsgBoxStyle.Information, "ERROR")
                    Label17.Text = "ERROR7:The amount of money is less than receipt total."
                    Label17.Visible = True
                    Label17.Visible = True
                    Button1.Visible = True
                    ListView1.Visible = False
                    Panel1.Visible = False
                    Panel3.Visible = False
                    TextBox2.Clear()
                    ListView2.Visible = False
                    TextBox1.Visible = False
                    ComboBox4.Visible = False
                    TextBox2.Visible = False
                    TextBox3.Visible = False
                    TextBox4.Visible = False
                    TextBox5.Visible = False
                    TextBox6.Visible = False
                    TextBox7.Visible = False
                    TextBox8.Visible = False
                    TextBox9.Visible = False
                    Label1.Visible = False
                    Label2.Visible = False
                    Label8.Visible = False
                    Label3.Visible = False
                    Label54.Visible = False
                    Exit Sub
                Else

                    Label1.Text = "Close Cash Drawer..."
                    'TextBox2.Text = Int32.Parse(TextBox2.Text) - Int32.Parse(ListView2.Items(0).SubItems(4).Text)
                    'MsgBox(TextBox2.Text)

                    TextBox1.Focus()
                    Label2.Visible = False

                    Label8.Visible = False

                    TextBox2.Visible = False
                    TextBox1.Visible = True
                    ComboBox4.Visible = True
                    TextBox1.Enabled = True





                    Label7.Visible = False
                    Label4.Visible = False
                    Label3.Visible = True
                    Label6.Visible = False
                    Label9.Visible = False
                    Label52.Visible = False

                    Label6.Text = "F6-Tender"
                    Label5.Visible = False
                    Label53.Visible = False


                    TextBox1.Focus()

                    ListView1.Enabled = True
                    Label16.Visible = True
                    Label24.Visible = True
                    Label25.Visible = True
                    Label31.Visible = True

                    PictureBox10.Enabled = True








                    ' --------------------------------------
                    rn = RN_ID()
                    Dim year As String
                    Dim day As String
                    Dim month As String
                    Dim hour As String
                    Dim minute As String


                    year = Date.Today.Year.ToString
                    day = Date.Today.Day.ToString
                    month = Date.Today.Month.ToString
                    hour = Date.Now.Hour.ToString
                    minute = Date.Now.Minute.ToString


                    rr = year + month + day + u_id + ws_id + rn.ToString
                    rr_barcode = rr

                    Dim conn As New OracleConnection
                    Dim cmd As New OracleCommand
                    Dim cmd1 As New OracleCommand
                    Dim cmd2_1 As New OracleCommand
                    Dim cmd2_2 As New OracleCommand
                    Dim cmd2_3 As New OracleCommand
                    Dim cmd3 As New OracleCommand
                    Dim cmd4 As New OracleCommand
                    Dim cmd_date As New OracleCommand
                    Dim b_date As String = ""



                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                    cmd_date.CommandText = "select business_date from daily_ops"
                    cmd_date.Connection = conn

                    conn.Open()
                    Dim rd_date As OracleDataReader = cmd_date.ExecuteReader
                    If rd_date.Read Then
                        b_date = rd_date.GetValue(0)

                    End If
                    rd_date.Close()
                    'conn.Close()

                    cmd.CommandText = " insert into receipt_numbers values(TO_CHAR(SYSDATE, 'dd-mm-yy')," + rn + "," + rr + ",'" + ListView2.Items(0).SubItems(3).Text + "','" + ListView2.Items(0).SubItems(4).Text + "','" + ListView2.Items(0).SubItems(2).Text + "','" + user_id + "','CASH','sale',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh:mm:ss'),'" + b_date + "','') "
                    cmd.Connection = conn

                    'conn.Open()
                    cmd.ExecuteNonQuery()
                    'conn.Close()
                    cmd.CommandText = " insert into receipt_numbers_WS values(TO_CHAR(SYSDATE, 'dd-mm-yy')," + rn + "," + rr + ",'" + ListView2.Items(0).SubItems(3).Text + "','" + ListView2.Items(0).SubItems(4).Text + "','" + ListView2.Items(0).SubItems(2).Text + "','" + user_id + "','CASH','sale',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh:mm:ss'),'" + b_date + "',''," + ws_id + ") "
                    cmd.Connection = conn

                    'conn.Open()
                    cmd.ExecuteNonQuery()
                    'conn.Close()
                    '------------------------------------------
                    Label3.Visible = True
                    Label2.Text = ""
                    pos_flag = 1

                    '------------------------------
                    Dim cmd_DSF As New OracleCommand
                    For i = 0 To ListView1.Items.Count - 1



                        cmd_DSF.CommandText = "insert into DSF values('" + u_id + "','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(1).Text + "','" + ListView1.Items(i).SubItems(2).Text + "','" + b_date + "','" + System.DateTime.Now + "','Sale','" + ListView1.Items(i).SubItems(5).Text + "','" + user_id + "')"
                        cmd_DSF.Connection = conn

                        'conn.Open()
                        cmd_DSF.ExecuteNonQuery()
                        'conn.Close()
                        Dim path As String = "C:\POS\OUT"
                        If Directory.Exists(path) Then
                            '------------------------------------------Print open report
                            Dim file_DSF As System.IO.StreamWriter
                            file_DSF = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\OUT\DSF_" + b_date + ".txt", True)

                            file_DSF.WriteLine(u_id & "|" & ListView1.Items(i).SubItems(0).Text & "|" & ListView1.Items(i).SubItems(1).Text & "|" & ListView1.Items(i).SubItems(2).Text & "|" & b_date & "|" & System.DateTime.Now & "|" & "Sale" & "|" & ListView1.Items(i).SubItems(5).Text & "|" & user_id)


                            file_DSF.Close()
                        End If
                    Next
                    '------------------------------------------

                    For i = 0 To ListView1.Items.Count - 1
                        AI_TRN = rn & i.ToString


                        cmd1.CommandText = "insert into tr_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",'admin',1,'',sysdate,sysdate,0,0,2,'','',0,'','','-1','-1','-1',0,0,sysdate,sysdate,0)"
                        cmd1.Connection = conn

                        'conn.Open()
                        cmd1.ExecuteNonQuery()
                        'conn.Close()


                        cmd2_1.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'SR','','',0,-1,sysdate,sysdate)"
                        cmd2_1.Connection = conn

                        'conn.Open()
                        cmd2_1.ExecuteNonQuery()
                        'conn.Close()

                        cmd2_2.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",1,'TX','','',0,-1,sysdate,sysdate)"
                        cmd2_2.Connection = conn

                        'conn.Open()
                        cmd2_2.ExecuteNonQuery()
                        'conn.Close()

                        cmd2_3.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",2,'TN','','',0,-1,sysdate,sysdate)"
                        cmd2_3.Connection = conn

                        'conn.Open()
                        cmd2_3.ExecuteNonQuery()
                        'conn.Close()

                        Dim quan As Integer = ListView1.Items(i).SubItems(3).Text

                        cmd3.CommandText = "insert into tr_ltm_sls_rtn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'',0,'1601','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(0).Text + "','" + quan.ToString + "','" + (Double.Parse(ListView1.Items(i).SubItems(2).Text) / CF).ToString + "','" + (Double.Parse(ListView1.Items(i).SubItems(5).Text) / CF).ToString + "',0,'',0,'',0,'','KEY','','',0,'','','-1','','',0,'','',0,0,'','','',0,0,0,0,'',0,'" + rr + "',1,-1,1,0,0,sysdate,sysdate,0,'" + (Double.Parse(ListView1.Items(i).SubItems(2).Text) / CF).ToString + "')"
                        cmd3.Connection = conn

                        'conn.Open()
                        cmd3.ExecuteNonQuery()
                        'conn.Close()

                        cmd4.CommandText = "insert into TR_LTM_SLS_RTN_RECEIPT values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'',0,'1601','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(0).Text + "','" + quan.ToString + "','" + Double.Parse(ListView1.Items(i).SubItems(2).Text).ToString + "','" + Double.Parse(ListView1.Items(i).SubItems(5).Text).ToString + "',0,'',0,'',0,'','KEY','','',0,'','','-1','','',0,'','',0,0,'','','',0,0,0,0,'',0,'" + rr + "',1,-1,1,0,0,sysdate,sysdate,0,'" + Double.Parse(ListView1.Items(i).SubItems(2).Text).ToString + "')"
                        cmd4.Connection = conn

                        'conn.Open()
                        cmd4.ExecuteNonQuery()
                        'conn.Close()


                    Next
                    '''''''''''''''''''generate barcode
                    Try
                        PictureBox12.Image = Nothing

                        Dim year1 As String = Date.Today.Year.ToString
                        Dim day1 As String = Date.Today.Day.ToString
                        Dim month1 As String = Date.Today.Month.ToString
                        Dim hour1 As String = Date.Now.Hour.ToString
                        Dim minute1 As String = Date.Now.Minute.ToString
                        Dim Second As String = Date.Now.Second.ToString

                        Dim rr1 As String = year + month + day + hour + minute + Second


                        'ID Automation
                        'Free only with the Code39 and Code39Ext

                        '-----------------------------------------------------print receipt








                        '---------------
                        ' Create a linear barcode image object (BarcodeLib.Barcode.Linear)

                    Catch ex As Exception
                        'MsgBox(ex.Message)
                    Finally
                        conn.Close()
                    End Try


                    If receipt_lang = "1" Then


                        '-----------------------------------------------------print receipt
                        Dim file As System.IO.StreamWriter
                        file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\" + rr + ".txt", True)

                        file.WriteLine("    " & company_name)
                        file.WriteLine("    " & store_name)

                        file.WriteLine("------------------")
                        file.WriteLine("    " & address)
                        file.WriteLine("    " & telephone)
                        file.WriteLine("------------------")
                        file.WriteLine("    SALE")
                        'file.WriteLine("    بيع")
                        file.WriteLine("------------------")
                        file.WriteLine(System.DateTime.Now)
                        file.WriteLine("------------------")
                        file.WriteLine("    " & rr.ToString)
                        file.WriteLine("------------------")
                        file.WriteLine("VAT: ضَريبةِ القيمةِ المُضافةِ:")
                        file.WriteLine("    " & vat_number)
                        file.WriteLine("------------------")
                        file.WriteLine("فاتورة ضريبة مبسطة")
                        file.WriteLine("Simplified Tax Invoice")
                        file.WriteLine("------------------")
                        file.WriteLine("                  ")
                        For i = 0 To ListView1.Items.Count - 1
                            file.WriteLine("    " & (i + 1).ToString & "- " & ListView1.Items(i).SubItems(0).Text & "      " & ListView1.Items(i).SubItems(2).Text & " (" & ListView1.Items(i).SubItems(5).Text & ")" & " (" & ListView1.Items(i).SubItems(3).Text & ")")
                            file.WriteLine("    " & ListView1.Items(i).SubItems(1).Text)
                            'file.WriteLine("                  ")
                        Next
                        file.WriteLine("------------------")
                        'file.WriteLine("")
                        'file.WriteLine()
                        'file.WriteLine("")
                        'file.WriteLine()
                        'file.WriteLine("")
                        'file.WriteLine()

                        'file.WriteLine("")
                        'file.WriteLine()
                        'file.WriteLine("")
                        'file.WriteLine("")
                        'file.WriteLine()
                        'file.WriteLine("")
                        'file.WriteLine("")
                        'file.WriteLine("")
                        'file.WriteLine()

                        file.WriteLine("    Subtotal   " & subtotal)
                        file.WriteLine("    Discount   " & ListView2.Items(0).SubItems(1).Text)
                        'file.WriteLine("    VAT%    " & ListView2.Items(0).SubItems(2).Text)
                        file.WriteLine("    Total   " & ListView2.Items(0).SubItems(4).Text)


                        file.WriteLine("    Cash    " & TextBox2.Text)

                        file.WriteLine("    CHANGE  " & (Convert.ToDecimal(TextBox2.Text) - Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text)).ToString)
                        'file.WriteLine()

                        file.WriteLine("==============================")
                        file.WriteLine("    VAT Summary | ملخص الضرائب")
                        file.WriteLine("    % VAT Rates: " & vat & " :% معدل الضريبة")
                        file.WriteLine("    VAT: " & (Double.Parse(ListView2.Items(0).SubItems(2).Text) & " :الضرائب"))
                        file.WriteLine("    Inc VAT: " & Double.Parse(ListView2.Items(0).SubItems(4).Text) & " :متضمن للضريبة")
                        file.WriteLine("==============================")


                        file.WriteLine("    " & policy)
                        'file.WriteLine("    Thank You For Shopping")
                        file.WriteLine("                        ")




                        file.Close()
                        vat_value = (Double.Parse(ListView2.Items(0).SubItems(2).Text))
                        receipt_value = Double.Parse(ListView2.Items(0).SubItems(4).Text).ToString
                        ' Dim proc As New Process
                        '
                        '
                        ' proc.StartInfo.FileName = "C:\POS\Receipts\" + rr + ".txt"
                        '
                        '
                        ' proc.StartInfo.Verb = "Print"
                        '
                        '
                        ' proc.StartInfo.CreateNoWindow = True

                        If printer_status = "1" Then

                            Try
                                AddHandler PrintZatcaQR.PrintPage, AddressOf Me.PrintImage
                                PrintZatcaQR.Print()
                            Catch ex As Exception
                                MsgBox(ex, MsgBoxStyle.Critical, "Error during Print")
                            End Try
                            ' proc.Start()

                            ' proc.WaitForExit()
                            'PrintDocument1.Print()

                        End If

                    End If

                    If receipt_lang = "2" Then


                        '-----------------------------------------------------print receipt
                        Dim file As System.IO.StreamWriter
                        file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\" + rr + ".txt", True)

                        file.WriteLine("    " & company_name)
                        file.WriteLine("    " & store_name)

                        file.WriteLine("------------------")
                        file.WriteLine("    " & address)
                        file.WriteLine("    " & telephone)
                        file.WriteLine("------------------")
                        file.WriteLine("    SALE")
                        file.WriteLine("    بيع")
                        file.WriteLine("------------------")
                        file.WriteLine("    " & System.DateTime.Now)
                        file.WriteLine("------------------")
                        file.WriteLine("    " & rr.ToString)
                        file.WriteLine("------------------")
                        file.WriteLine("    VAT: ضَريبةِ القيمةِ المُضافةِ:")
                        file.WriteLine("    " & vat_number)
                        file.WriteLine("------------------")
                        file.WriteLine("    فاتورة ضريبة مبسطة")
                        file.WriteLine("    Simplified Tax Invoice")
                        file.WriteLine("------------------")
                        file.WriteLine("                  ")
                        For i = 0 To ListView1.Items.Count - 1
                            file.WriteLine("    " & (i + 1).ToString & "- " & ListView1.Items(i).SubItems(0).Text & "      " & ListView1.Items(i).SubItems(2).Text & " (" & ListView1.Items(i).SubItems(5).Text & ")" & " (" & ListView1.Items(i).SubItems(3).Text & ")")
                            file.WriteLine("    " & ListView1.Items(i).SubItems(1).Text)
                            'file.WriteLine("                  ")
                        Next
                        'file.WriteLine("")
                        'file.WriteLine()
                        'file.WriteLine("")
                        'file.WriteLine()
                        'file.WriteLine("")
                        'file.WriteLine()

                        'file.WriteLine("")
                        'file.WriteLine()
                        'file.WriteLine("")
                        'file.WriteLine("")
                        'file.WriteLine()
                        'file.WriteLine("")
                        'file.WriteLine("")
                        'file.WriteLine("")
                        'file.WriteLine()
                        file.WriteLine("-----------------------------")
                        file.WriteLine("    Subtotal   " & subtotal & "     المجموع الفرعي")
                        file.WriteLine("    Discount   " & ListView2.Items(0).SubItems(1).Text & "     مجموع التخفيضات")
                        'file.WriteLine("    VAT%    " & ListView2.Items(0).SubItems(2).Text & "     ملخص الضريبة")
                        file.WriteLine("    Total   " & ListView2.Items(0).SubItems(4).Text & "     المجموع الكلي")


                        file.WriteLine("    Cash    " & TextBox2.Text & "       نقد")

                        file.WriteLine("    CHANGE  " & (Convert.ToDecimal(TextBox2.Text) - Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text)).ToString & "        المتبقي")
                        'file.WriteLine()

                        file.WriteLine("==============================")
                        file.WriteLine("    VAT Summary | ملخص الضرائب")
                        file.WriteLine("    % VAT Rates: " & vat & " :% معدل الضريبة")
                        file.WriteLine("    VAT: " & (Double.Parse(ListView2.Items(0).SubItems(2).Text) & " :الضرائب"))
                        file.WriteLine("    Inc VAT: " & Double.Parse(ListView2.Items(0).SubItems(4).Text) & " :متضمن للضريبة")
                        file.WriteLine("==============================")



                        file.WriteLine("    " & policy & "    ")
                        'file.WriteLine("    Thank You For Shopping")
                        file.WriteLine("                        ")




                        file.Close()

                        vat_value = (Double.Parse(ListView2.Items(0).SubItems(2).Text))
                        receipt_value = Double.Parse(ListView2.Items(0).SubItems(4).Text).ToString

                        '  Dim proc As New Process
                        '
                        '
                        '  proc.StartInfo.FileName = "C:\POS\Receipts\" + rr + ".txt"
                        '
                        '
                        '  proc.StartInfo.Verb = "Print"
                        '
                        '
                        '  proc.StartInfo.CreateNoWindow = True

                        If printer_status = "1" Then


                            ' proc.Start()

                            ' proc.WaitForExit()
                            'PrintDocument1.Print()
                            Try
                                AddHandler PrintZatcaQR.PrintPage, AddressOf Me.PrintImage
                                PrintZatcaQR.Print()
                            Catch ex As Exception
                                MsgBox(ex, MsgBoxStyle.Critical, "Error during Print")
                            End Try
                        End If

                    End If

                    ListView1.Items.Clear()

                    ListView2.Items(0).SubItems(0).Text = "0"
                    ListView2.Items(0).SubItems(1).Text = "0"
                    ListView2.Items(0).SubItems(2).Text = "0"
                    ListView2.Items(0).SubItems(3).Text = "0"
                    ListView2.Items(0).SubItems(4).Text = "0"
                    TextBox2.Clear()
                    TextBox2.Visible = False
                    Label1.Text = "Enter an item number."
                    If com_status = 1 Then
                        Dim sp = New SerialPort(com_port, 9600, Parity.None, 8, StopBits.One)
                        sp.Open()
                        sp.Write(Convert.ToString(Chr(12)))
                        sp.Write("Thanks for shopping")
                        sp.Close()
                    End If


                    Label5.Text = "F4-E.Receipt"
                    Label5.Location = New Point(650, 420)
                    Label5.Visible = False
                    Label53.Visible = False

                    Label9.Visible = True

                    Label9.Text = "F5-R.Receipt"
                    Label52.Visible = True
                    Label54.Visible = False

                    '----------------------------------------------------
                    'Buy x get y discount
                    Dim cmd_update As New OracleCommand
                    cmd_update.Connection = conn
                    cmd_update.CommandText = "update Buy_X_GET_Y_Discount set c = :nodec"
                    cmd_update.Parameters.Add("nodec", OracleDbType.Double).Value = 0
                    conn.Open()
                    cmd_update.ExecuteNonQuery()
                    conn.Close()

                End If

            End If

            TextBox1.Clear()
            If dbpool > 30 Then
                autorestart = 1
                conn.Close()
                conn_scan_item.Close()
                OracleConnection.ClearPool(conn)
                OracleConnection.ClearPool(conn_scan_item)
                'conn.Dispose()
                'conn_scan_item.Dispose()


                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            conn.Close()
            'conn.Dispose()
            'OracleConnection.ClearAllPools()

        End Try



    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        Try
            e.Handled = True

            Try
                If e.KeyCode = Keys.Escape Then
                    e.SuppressKeyPress = True
                    If Label3.Visible = True And Label3.Enabled = True Then
                        Label3_Click(sender, e)
                    End If

                End If


                If e.KeyCode = Keys.F2 Then
                    e.SuppressKeyPress = True
                    If Label2.Visible = True And Label2.Enabled = True Then
                        Label2_Click(sender, e)
                    End If


                End If
                If e.KeyCode = Keys.F4 Then
                    e.SuppressKeyPress = True
                    If Label8.Visible = True And Label8.Enabled = True Then
                        Label8_Click(sender, e)
                    End If


                End If

            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        Try
            Dim txt As TextBox = CType(sender, TextBox)

            If Not Char.IsDigit(e.KeyChar) Then e.Handled = True

            If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

            If e.KeyChar = "." And txt.Text.IndexOf(".") = -1 Then e.Handled = False 'allow single decimal point
            'If e.KeyChar = "-" And txt.SelectionStart = 0 Then e.Handled = False 'allow negative number
            If e.KeyChar = Chr(13) Then

                e.Handled = True


            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TextBox2_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Try

            If TextBox2.Text <> "" And Decimal.Parse(TextBox2.Text) >= Decimal.Parse(ListView2.Items(0).SubItems(4).Text) Then

                ListView2.Items(0).SubItems(0).Text = Decimal.Parse(TextBox2.Text) - Decimal.Parse(ListView2.Items(0).SubItems(4).Text)
            End If

        Catch ex As Exception
            ' MsgBox(ex.Message)
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

        cmd.CommandText = " select max(receipt_number) from receipt_numbers where system_date = TO_CHAR(SYSDATE, 'dd-mm-yy') "
        cmd.Connection = conn
        Try
            conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader

            If rd.Read Then
                If rd.GetValue(0).ToString <> "" Then
                    RN = rd.GetValue(0)

                Else
                    RN = 0
                End If



            End If
            rd.Close()
            conn.Close()
            RN = RN + 1

            'If ID_ev Then
            'ID_ev = ID_ev + 1
            Return RN
            ' Else
            ' ID_ev = 1000
            ' Return ID_ev
            ' End If






        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        Try

            If Label26.Visible = True Or TextBox10.Visible = True Then
                Panel1.Visible = True
                Panel3.Visible = True
                ListView1.Visible = True
                ListView2.Visible = True
                Label9.Visible = True
                Label52.Visible = True
                PictureBox6.Enabled = True
                Label1.Text = "Enter an item number."
                TextBox1.Enabled = True
                Label24.Visible = True
                Label25.Visible = True
                Label31.Visible = True
                Label26.Visible = False
                Label27.Visible = False
                Label28.Visible = False
                Label29.Visible = False
                Label30.Visible = False
                TextBox10.Visible = False
                TextBox1.Visible = True
                ComboBox4.Visible = True
                TextBox1.Focus()
                PictureBox2.Enabled = True
                PictureBox3.Enabled = True
                PictureBox5.Enabled = True
                PictureBox6.Enabled = True
                PictureBox10.Enabled = True

                Exit Sub
            End If
            If Label6.Text = "F6-Update" Then
                Label7.Visible = True
                Label3.Visible = False
                Label15.Visible = False
                MaskedTextBox1.Visible = False
                Label6.Text = ""
                Label6.Visible = False
                Label2.Visible = True
                PictureBox2.Enabled = True
                Label8.Visible = True
                Label1.Visible = False



                '  MessageBox.Show("1")

            End If

            If TextBox8.Visible = True Then

                'ListView1.ForeColor = Color.Black
                TextBox8.Visible = False
                Label9.Visible = True
                Label52.Visible = True
                PictureBox6.Enabled = True
                PictureBox4.Enabled = True
                pos_flag = 1
                TextBox8.Clear()
                refund_flag = 0
                Label4.Visible = False
                Label6.Visible = False
                ''''''''''''''''''
                Label9.Visible = True
                PictureBox4.Enabled = True
                Label5.Visible = True
                Label53.Visible = True

                Label2.Text = ""
                Label8.Text = ""

                ListView1.Enabled = True
                Label1.Text = "Enter an item number."
                TextBox2.Visible = False
                TextBox1.Visible = True
                ComboBox4.Visible = True
                TextBox1.Enabled = True

                TextBox1.Focus()
                Label2.Enabled = False
                Label2.Visible = False

                Label8.Enabled = False
                Label8.Visible = False

                Label4.Enabled = True
                Label6.Visible = False
                Label7.Enabled = True

                Label9.Visible = True
                PictureBox4.Enabled = True

                Label5.Visible = False
                Label53.Visible = False
                Label16.Visible = True
                Label24.Visible = True
                Label25.Visible = True
                Label31.Visible = True

                PictureBox10.Enabled = True



                return_flag = 0

                pos_flag = 1

                PictureBox2.Enabled = True
                PictureBox3.Enabled = True
                PictureBox5.Enabled = True
                PictureBox6.Enabled = True
                PictureBox10.Enabled = True
                'MessageBox.Show("3")
                Exit Sub

            End If

            If TextBox12.Visible = True Then
                TextBox12.Visible = False


                TextBox8.Visible = False
                Label9.Visible = True
                Label52.Visible = True
                PictureBox6.Enabled = True
                PictureBox4.Enabled = True
                pos_flag = 1

                refund_flag = 0
                Label4.Visible = False
                Label6.Visible = False
                ''''''''''''''''''
                Label9.Visible = True
                PictureBox4.Enabled = True
                Label5.Visible = True
                Label53.Visible = True

                Label2.Text = ""
                Label8.Text = ""

                ListView1.Enabled = True
                Label1.Text = "Enter an item number."
                TextBox2.Visible = False
                TextBox1.Visible = True
                ComboBox4.Visible = True
                TextBox1.Enabled = True

                TextBox1.Focus()
                Label2.Enabled = False
                Label2.Visible = False

                Label8.Enabled = False
                Label8.Visible = False

                Label4.Enabled = True
                Label6.Visible = False
                Label7.Enabled = True

                Label9.Visible = True
                PictureBox4.Enabled = True

                Label5.Visible = False
                Label53.Visible = False
                Label16.Visible = True
                Label24.Visible = True
                Label25.Visible = True
                Label31.Visible = True

                PictureBox10.Enabled = True

                ListView1.Items.Clear()


                return_flag = 0

                pos_flag = 1

                PictureBox2.Enabled = True
                PictureBox3.Enabled = True
                PictureBox5.Enabled = True
                PictureBox6.Enabled = True
                PictureBox10.Enabled = True
                ListView2.Items(0).SubItems(0).Text = "0"
                ListView2.Items(0).SubItems(1).Text = "0"
                ListView2.Items(0).SubItems(2).Text = "0"
                ListView2.Items(0).SubItems(3).Text = "0"
                ListView2.Items(0).SubItems(4).Text = "0"
                'MessageBox.Show("3")
                Exit Sub

            End If

            Dim conn As New OracleConnection
            Dim p_d As New OracleCommand
            Dim p_d1 As New OracleCommand



            If ComboBox1.Text <> "" And ComboBox1.Visible = True Then

                If ComboBox1.SelectedItem.ToString <> "" Then



                    If Label3.Text = "F10-Save" Then
                        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                        p_d.CommandText = "update pole_display set COM = '" + ComboBox1.Text + "' , status = 1 "
                        p_d.Connection = conn

                        conn.Open()
                        p_d.ExecuteNonQuery()

                        conn.Close()
                        Label2.Text = "F2-Pole"
                        Label55.Visible = True
                        Label2.Visible = True
                        PictureBox2.Enabled = True
                        Label8.Visible = True
                        Label57.Visible = True
                        Label58.Visible = True
                        Label60.Visible = True
                        Label3.Visible = False
                        'Label3.Text = "ESC-Undo"
                        ComboBox1.Visible = False
                        Label10.Visible = False
                        Label1.Visible = False
                        'MessageBox.Show("5")
                    End If
                Else
                    'MessageBox.Show("Choose COM port of pole display")
                    'MsgBox("Choose COM port of pole display..", MsgBoxStyle.Information, "Info")
                    Label17.Text = "Choose COM port of pole display.."
                    Label17.Visible = True
                    Button1.Visible = True
                    ListView1.Visible = False
                    Panel1.Visible = False
                    Panel3.Visible = False
                    ListView2.Visible = False
                    TextBox1.Enabled = False
                    TextBox2.Enabled = False
                    TextBox3.Enabled = False
                    TextBox4.Enabled = False
                    TextBox5.Enabled = False
                    TextBox6.Enabled = False
                    TextBox7.Enabled = False
                    TextBox8.Enabled = False
                    TextBox9.Enabled = False
                    Label1.Visible = False
                    Label2.Enabled = False
                    Label3.Enabled = False
                    Label4.Enabled = False
                    Label5.Enabled = False
                    Label6.Enabled = False
                    Label7.Enabled = False
                    Label8.Enabled = False
                    Label9.Enabled = False
                    Label16.Enabled = False



                End If

            End If
            If Label3.Text = "F10-Exit" Then


                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                p_d1.CommandText = "update pole_display set COM = 'COM1',status = 0 where status=1"
                p_d1.Connection = conn
                conn.Open()
                p_d1.ExecuteNonQuery()
                conn.Close()
                Label2.Text = "F2-Pole"
                Label55.Visible = True
                Label2.Visible = True
                PictureBox2.Enabled = True
                Label8.Visible = True
                Label57.Visible = True
                Label58.Visible = True
                Label60.Visible = True
                Label3.Visible = False
                'Label3.Text = "ESC-Undo"
                ComboBox1.Visible = False
                Label10.Visible = False
                Label1.Visible = False
                'MessageBox.Show("6")
            End If


            If (user_flag = 1 Or admin_flag = 1) And TextBox4.Visible = False And ListView1.Visible = True And Label2.Text <> "F2-Cash" And TextBox12.Visible = False Then

                Label1.Visible = False
                TextBox1.Visible = False
                ComboBox4.Visible = False
                TextBox2.Visible = False
                TextBox3.Visible = False
                TextBox3.Clear()
                TextBox4.Clear()
                PictureBox9.Visible = True
                Label11.Visible = False
                Label12.Visible = False
                Label13.Visible = False
                Label14.Visible = False
                Label18.Visible = False

                ListView1.Visible = False
                Panel1.Visible = False
                Panel3.Visible = False
                ListView2.Visible = False
                Label2.Visible = True
                Label2.Enabled = True
                PictureBox2.Enabled = True
                'Label9.Visible = True
                Label5.Visible = True

                Label3.Visible = False
                Label6.Visible = False
                Label16.Visible = False
                Label24.Visible = False
                Label25.Visible = False
                Label31.Visible = False
                Label52.Visible = False

                ' Label4.Visible = False
                Label2.Visible = True

                Label2.Text = "F2-POS"
                Label2.Enabled = True
                log_flag = get_status()
                Label5.Text = "F4-Administrator"
                Label5.Location = New Point(637, 420)
                PictureBox9.Visible = True
                If log_flag = "1" Then
                    Label9.Visible = True
                    Label9.Text = "F3-Z Report"



                End If

                pos_flag = 0
                user_flag = 0
                ' MessageBox.Show("7")
            End If
            If Label2.Text = "F2-Cash" Then

                Label9.Visible = True
                PictureBox4.Enabled = True
                Label5.Visible = True
                Label53.Visible = True
                PictureBox6.Enabled = True
                Label2.Text = ""
                Label8.Text = ""
                Label7.Visible = True
                Label4.Visible = True

                ListView1.Enabled = True
                Label1.Text = "Enter an item number."
                TextBox2.Visible = False
                TextBox1.Visible = True
                ComboBox4.Visible = True
                TextBox1.Enabled = True

                TextBox1.Focus()
                Label2.Enabled = False
                Label2.Visible = False
                Label8.Enabled = False
                Label8.Visible = False

                Label4.Enabled = True
                Label6.Visible = True
                PictureBox3.Enabled = True
                Label7.Enabled = True
                Label3.Visible = False
                Label9.Visible = False
                Label52.Visible = False

                TextBox7.Clear()
                TextBox7.Visible = False
                TextBox8.Visible = False
                TextBox9.Visible = False

                For i = 0 To ListView1.Items.Count - 1
                    If ListView1.Items(i).SubItems(3).Text.ToString = "-1" Then
                        ListView1.Items.RemoveAt(i)
                    End If

                Next


                pos_flag = 1

                PictureBox2.Enabled = True
                PictureBox3.Enabled = True
                PictureBox5.Enabled = True
                PictureBox6.Enabled = True
                PictureBox10.Enabled = True
                'MessageBox.Show("8")
                Dim TotalSum As Double = 0
                Dim totalqty As Double = 0
                Dim totaldis As Double = 0

                Dim TempNode As ListViewItem
                For Each TempNode In ListView1.Items
                    TotalSum += CDbl(TempNode.SubItems.Item(2).Text)
                    totalqty += CDbl(TempNode.SubItems.Item(3).Text)
                    totaldis += CDbl(TempNode.SubItems.Item(5).Text)
                Next

                ListView2.Items(0).SubItems(0).Text = TotalSum
                'ListView2.Items(0).SubItems(1).Text = Total_Discount.ToString
                If TotalSum > Convert.ToDecimal(vat_limit) Then
                    ListView2.Items(0).SubItems(2).Text = Math.Round((vat * (TotalSum - totaldis)) / 100, 2) 'Math.Round
                Else
                    ListView2.Items(0).SubItems(2).Text = "0"
                End If
                ListView2.Items(0).SubItems(1).Text = totaldis
                ListView2.Items(0).SubItems(3).Text = totalqty
                ListView2.Items(0).SubItems(4).Text = Convert.ToDecimal(ListView2.Items(0).SubItems(0).Text) + Convert.ToDecimal(ListView2.Items(0).SubItems(2).Text) - Convert.ToDecimal(ListView2.Items(0).SubItems(1).Text)
            End If

            If RichTextBox1.Text = "wrong user name or password" And RichTextBox1.Visible = True Then
                Label1.Visible = False
                TextBox1.Visible = False
                ComboBox4.Visible = False
                TextBox2.Visible = False
                ListView1.Visible = False
                Panel1.Visible = False
                Panel3.Visible = False
                ListView2.Visible = False
                Label2.Visible = True
                PictureBox2.Enabled = True
                Label9.Visible = True
                PictureBox4.Enabled = True
                Label5.Visible = True

                Label3.Visible = False
                Label6.Visible = False
                Label7.Visible = False
                Label4.Visible = False
                Label2.Visible = True
                Label2.Text = "F2-POS"
                Label2.Enabled = True
                RichTextBox1.Clear()

                PictureBox9.Visible = True

                RichTextBox1.Visible = False
                'MessageBox.Show("9")
            End If
            If reconcile_flag = 1 Then
                Label1.Visible = False
                TextBox1.Visible = False
                ComboBox4.Visible = False
                TextBox2.Visible = False
                TextBox3.Visible = False
                ListView1.Visible = False
                Panel1.Visible = False
                Panel3.Visible = False
                ListView2.Visible = False
                Label2.Visible = True
                Label9.Visible = True
                PictureBox4.Enabled = True
                Label5.Visible = True

                Label3.Visible = False
                Label6.Visible = False
                Label7.Visible = False
                Label4.Visible = False
                Label2.Visible = True
                Label2.Text = "F2-POS"
                Label2.Enabled = True
                RichTextBox1.Visible = False
                TextBox6.Visible = False
                reconcile_flag = 0
                'MessageBox.Show("10")
                PictureBox9.Visible = True
            End If
            If TextBox5.Visible = True Then
                Label1.Visible = False
                TextBox1.Visible = False
                ComboBox4.Visible = False
                TextBox2.Visible = False
                ListView1.Visible = False
                Panel1.Visible = False
                Panel3.Visible = False
                ListView2.Visible = False
                Label2.Visible = True
                Label9.Visible = True
                PictureBox4.Enabled = True
                Label5.Visible = True

                Label3.Visible = False
                Label6.Visible = False
                Label7.Visible = False
                Label4.Visible = False
                Label2.Visible = True
                Label2.Text = "F2-POS"
                Label2.Enabled = True
                PictureBox2.Enabled = True
                Label9.Visible = False
                Label52.Visible = False

                RichTextBox1.Visible = False
                TextBox5.Visible = False
                PictureBox9.Visible = True
                'MessageBox.Show("11")
            End If

        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        Try
            For i = 0 To ListView1.Items.Count
                If ListView1.Items(i).BackColor = Color.Gray Or ListView1.Items(i).BackColor = Color.Red Or ListView1.Items(i).BackColor = Color.Orange Then
                    ListView1.Items(i).Selected = False

                End If

            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyDown
        Try
            If e.KeyCode = Keys.Up Then
                For i = 0 To ListView1.Items.Count
                    If ListView1.Items(i).BackColor = Color.Gray Or ListView1.Items(i).BackColor = Color.Red Or ListView1.Items(i).BackColor = Color.Orange Then
                        ListView1.Items(i).Selected = False

                    End If

                Next
            End If

            If e.KeyCode = Keys.F6 Then
                e.Handled = True
                e.SuppressKeyPress = True
                If Label6.Visible = True And Label6.Enabled = True Then
                    Label6_Click(sender, e)
                End If


            End If

            If e.KeyCode = Keys.F5 Then
                e.Handled = True
                e.SuppressKeyPress = True
                If Label9.Visible = True And Label9.Enabled = True Then
                    Label9_Click(sender, e)

                End If


            End If

            If e.KeyCode = Keys.F4 Then
                e.SuppressKeyPress = True
                If Label52.Visible = True And Label52.Enabled = True Then
                    Label52_Click(sender, e)
                End If

            End If

            If e.KeyCode = Keys.F11 Then
                e.Handled = True
                e.SuppressKeyPress = True
                If Label4.Visible = True And Label4.Enabled = True Then
                    Label4_Click(sender, e)
                End If


            End If
            If e.KeyCode = Keys.F12 Then
                e.SuppressKeyPress = True
                e.Handled = True
                If Label7.Visible = True And Label7.Enabled = True Then
                    Label7_Click(sender, e)
                End If


            End If

            If e.KeyCode = Keys.F6 Then
                If Label6.Visible = True And Label6.Enabled = True Then
                    Label6_Click(sender, e)
                End If


            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged



    End Sub


    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click
        Try
            If Convert.ToDecimal(TextBox2.Text) < Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text) Then
                'MessageBox.Show("the amount of money is less than receipt total.")
                ' MsgBox("the amount of money is less than receipt total..", MsgBoxStyle.Information, "ERROR")
                Label17.Text = "ERROR7:The amount of money is less than receipt total."
                Label17.Visible = True
                Label17.Visible = True
                Button1.Visible = True
                ListView1.Visible = False
                Panel1.Visible = False
                Panel3.Visible = False
                ListView2.Visible = False
                TextBox1.Visible = False
                ComboBox4.Visible = False
                TextBox2.Visible = False
                TextBox2.Clear()

                TextBox3.Visible = False
                TextBox4.Visible = False
                TextBox5.Visible = False
                TextBox6.Visible = False
                TextBox7.Visible = False
                TextBox8.Visible = False
                TextBox9.Visible = False
                Label1.Visible = False
                Label2.Visible = False
                Label8.Visible = False
                Label3.Visible = False
                Label54.Visible = False
                Exit Sub


            Else
                If Label8.Text = "F4-Credit" Then
                    TextBox7.Visible = True
                    TextBox7.Focus()
                    Label1.Text = "Please swip the credit card."
                    Label2.Visible = False
                    Label8.Visible = False
                    TextBox2.Visible = False
                    TextBox1.Visible = False
                    ComboBox4.Visible = False
                    TextBox8.Visible = False
                    TextBox9.Visible = False
                    TextBox10.Visible = False
                    TextBox11.Visible = False
                    TextBox12.Visible = False
                    TextBox6.Visible = False
                    TextBox3.Visible = False
                    TextBox4.Visible = False
                    TextBox5.Visible = False



                End If
            End If


            If Label8.Text = "F4-DB Server" Then
                MaskedTextBox1.Focus()
                Label15.Visible = True
                MaskedTextBox1.Visible = True

                Label6.Text = "F6-Update"
                Label6.Visible = True
                PictureBox3.Enabled = True
                Label2.Visible = False
                Label8.Visible = False
                Label7.Visible = False
                Label3.Visible = True

                TextBox3.Visible = False
                Label1.Visible = True
                Label1.Text = "Please enter Database host name.."



            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        Try
            If Label7.Text = "F12-Undo" Then
                If TextBox3.Visible = False Then
                    RichTextBox1.Visible = False
                    TextBox4.Visible = False
                    TextBox3.Visible = True
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox3.Focus()
                    RichTextBox1.Visible = False
                    Label1.Visible = True
                    Label1.Text = "Please enter user ID"
                    TextBox3.Focus()
                    'sys_admin_flag = 0
                    'MessageBox.Show("2")
                ElseIf Label7.Text = "F12-Undo" And TextBox3.Visible = True Then
                    RichTextBox1.Visible = False
                    Label1.Visible = False
                    TextBox1.Visible = False
                    ComboBox4.Visible = False
                    TextBox2.Visible = False
                    TextBox3.Visible = False
                    TextBox3.Clear()
                    TextBox4.Clear()
                    PictureBox9.Visible = True
                    Label11.Visible = False
                    Label12.Visible = False
                    Label13.Visible = False
                    Label14.Visible = False
                    Label18.Visible = False

                    ListView1.Visible = False
                    Panel1.Visible = False
                    Panel3.Visible = False

                    ListView2.Visible = False
                    Label2.Visible = True
                    'Label9.Visible = True
                    Label5.Visible = True
                    Label3.Visible = False
                    Label6.Visible = False
                    Label7.Visible = False
                    admin_flag = 0
                    user_flag = 0
                    sys_admin_flag = 0

                    ' Label4.Visible = False
                    Label2.Visible = True
                    Label2.Text = "F2-POS"
                    Label2.Enabled = True
                    PictureBox2.Enabled = True

                    log_flag = get_status()
                    Label5.Text = "F4-Administrator"
                    Label5.Location = New Point(637, 420)
                    PictureBox9.Visible = True
                    If log_flag = "1" Then
                        Label9.Visible = True
                        Label9.Text = "F3-Z Report"
                        PictureBox4.Enabled = True
                    End If

                    pos_flag = 0
                    user_flag = 0
                    reconcile_flag = 0
                    'MessageBox.Show("7A")
                End If
            End If



            If Label7.Text = "F12-Cancel" And Label55.Visible = False Then



                Label7.Visible = False
                TextBox9.Visible = False

                TextBox8.Visible = False
                TextBox12.Visible = False
                TextBox10.Visible = False
                TextBox7.Visible = False
                TextBox6.Visible = False
                TextBox5.Visible = False
                TextBox4.Visible = False
                TextBox3.Visible = False
                TextBox2.Visible = False



                Label1.Text = "Enter an item number."
                TextBox2.Clear()

                Label2.Visible = False
                Label8.Visible = False
                TextBox12.Clear()

                TextBox1.Visible = True
                ComboBox4.Visible = True
                TextBox1.Enabled = True
                TextBox1.Clear()
                TextBox1.Focus()


                Dim cmd_DSF As New OracleCommand
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                For i = 0 To ListView1.Items.Count - 1



                    cmd_DSF.CommandText = "insert into DSF values('" + u_id + "','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(1).Text + "','" + ListView1.Items(i).SubItems(2).Text + "','" + b_date + "','" + System.DateTime.Now + "','Cancel','0','" + user_id + "')"
                    cmd_DSF.Connection = conn

                    conn.Open()
                    cmd_DSF.ExecuteNonQuery()
                    conn.Close()
                    Dim path As String = "C:\POS\OUT"
                    If Directory.Exists(path) Then
                        '------------------------------------------Print open report
                        Dim file As System.IO.StreamWriter
                        file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\OUT\DSF_" + b_date + ".txt", True)

                        file.WriteLine(u_id & "|" & ListView1.Items(i).SubItems(0).Text & "|" & ListView1.Items(i).SubItems(1).Text & "|" & ListView1.Items(i).SubItems(2).Text & "|" & b_date & "|" & System.DateTime.Now & "|" & "Cancel" & "|" & "0" & "|" & user_id)


                        file.Close()
                    End If
                Next
                ListView1.Items.Clear()


                ListView2.Items(0).SubItems(0).Text = "0"
                ListView2.Items(0).SubItems(1).Text = "0"
                ListView2.Items(0).SubItems(2).Text = "0"
                ListView2.Items(0).SubItems(3).Text = "0"
                ListView2.Items(0).SubItems(4).Text = "0"



                TextBox1.Focus()

                ListView1.Enabled = True


                Label3.Visible = True
                TextBox9.Visible = False
                TextBox9.Clear()

                Label9.Visible = True
                PictureBox4.Enabled = True
                Label9.Text = "F5-R.Receipt"
                Label52.Visible = True
                PictureBox6.Enabled = True

                ListBox2.Items.Clear()
                ListBox3.Items.Clear()

                pos_flag = 1



                Label6.Visible = False
                Label4.Visible = False
                Label5.Visible = False
                Label53.Visible = False
                Label54.Visible = False
                Label3.Visible = True
                return_flag = 0
                exchange_flag = 0
                Label16.Visible = True
                Label24.Visible = True
                Label25.Visible = True
                Label31.Visible = True
                PictureBox10.Enabled = True
                PictureBox2.Enabled = True
                PictureBox3.Enabled = True
                PictureBox5.Enabled = True
                PictureBox6.Enabled = True
                PictureBox10.Enabled = True
                '----------------------------------------------------
                'Buy x get y discount
                Dim cmd_update As New OracleCommand
                cmd_update.Connection = conn
                cmd_update.CommandText = "update Buy_X_GET_Y_Discount set c = :nodec"
                cmd_update.Parameters.Add("nodec", OracleDbType.Double).Value = 0
                conn.Open()
                cmd_update.ExecuteNonQuery()
                conn.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        Try

            Dim TotalSum As Double = 0
            Dim totalqty As Double = 0
            Dim totaldis As Double = 0
            Dim TempNode As ListViewItem
            Dim ind As Integer
            ind = 0
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            conn.Open()
            Dim cmd_DSF As New OracleCommand
            If ListView1.SelectedItems.Count > 0 Then


                If ListView1.Items.Count = 1 Then
                    Label4.Visible = False
                    'TextBox9.Focus()
                    Label6.Visible = False

                    'TextBox1.Focus()

                    ListView2.Items(0).SubItems(0).Text = "0"
                    ListView2.Items(0).SubItems(1).Text = "0"
                    ListView2.Items(0).SubItems(2).Text = "0"
                    ListView2.Items(0).SubItems(3).Text = "0"
                    ListView2.Items(0).SubItems(4).Text = "0"
                    If com_status = 1 Then
                        Dim sp = New SerialPort(com_port, 9600, Parity.None, 8, StopBits.One)
                        sp.Open()
                        sp.Write(Convert.ToString(Chr(12)))
                        sp.Write("Thanks for shopping")
                        sp.Close()
                    End If


                End If
                'Remove single item
                'If ListView1.Items.Count > 1 Then


                ' Dim x As Integer
                'x = ListView1.FocusedItem.Index

                'ListView1.Items(x).Selected = False
                'ListView1.Items(x - 1).Selected = True
                'End If
                'ListView1.Items.RemoveAt(ListView1.SelectedIndices(0)) update on 21-4-2018
                'ListView1.Items(x).Focused = True
                If ListView1.Items.Count = 0 Then
                    ind = 1

                End If
                Try
                    Dim X_update As New OracleCommand
                    X_update.Connection = conn


                    'Remove Multiple Selected Items
                    Dim index As Integer
                    For Each item As ListViewItem In ListView1.SelectedItems
                        '................ update c parameter of buy X get Y discount
                        'MsgBox(item.SubItems(0).Text)
                        X_update.CommandText = "update Buy_X_GET_Y_Discount set c=c-1 where itm = '" & item.SubItems(0).Text & "'"
                        X_update.ExecuteNonQuery()
                        '.....................
                        If item.BackColor <> Color.Gray Or BackColor <> Color.Red Or BackColor = Color.Orange Then


                            index = ListView1.SelectedIndices(0)

                            cmd_DSF.CommandText = "insert into DSF values('" + u_id + "','" + ListView1.FocusedItem.SubItems(0).Text + "','" + ListView1.FocusedItem.SubItems(1).Text + "','" + ListView1.FocusedItem.SubItems(2).Text + "','" + b_date + "','" + System.DateTime.Now + "','Delete','0','" + user_id + "')"
                            cmd_DSF.Connection = conn


                            cmd_DSF.ExecuteNonQuery()

                            '------------------------------------------Print open report
                            Dim path As String = "C:\POS\OUT"
                            If Directory.Exists(path) Then
                                Dim file_delete As System.IO.StreamWriter
                                file_delete = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\OUT\DSF_" + b_date + ".txt", True)

                                file_delete.WriteLine(u_id & "|" & ListView1.FocusedItem.SubItems(0).Text & "|" & ListView1.FocusedItem.SubItems(1).Text & "|" & ListView1.FocusedItem.SubItems(2).Text & "|" & b_date & "|" & System.DateTime.Now & "|" & "Delete" & "|" & "0" & "|" & user_id)


                                file_delete.Close()
                            End If
                            If item.BackColor <> Color.Gray Or BackColor <> Color.Red Or BackColor = Color.Orange Then
                                If ListView1.Items.Count > 0 Then
                                    ListBox2.Items.Add(item.SubItems(0).Text)
                                    ListBox3.Items.Add(item.SubItems(4).Text)


                                    item.Remove()


                                End If

                            End If
                            Dim ex As Integer = 0
                            If exchange_flag = 1 Then
                                For i = 0 To ListView1.Items.Count - 1
                                    If ListView1.Items(i).SubItems(2).Text < 0 Then
                                        ex = 1
                                        Exit For
                                    End If
                                Next
                                If ex = 0 Then
                                    Label6.Visible = False
                                End If
                            End If

                            If return_flag = 1 Then
                                If ListView1.Items.Count > 0 Then
                                    Label6.Visible = True
                                Else
                                    Label6.Visible = False
                                    Label4.Visible = False
                                End If
                            End If
                        End If
                    Next

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                If ListView1.Items.Count < 1 Then


                    ListView2.Items(0).SubItems(0).Text = "0"
                    ListView2.Items(0).SubItems(1).Text = "0"
                    ListView2.Items(0).SubItems(2).Text = "0"
                    ListView2.Items(0).SubItems(3).Text = "0"
                    ListView2.Items(0).SubItems(4).Text = "0"
                End If




                '-------------------------------


                'MsgBox(sum.ToString)








                For Each TempNode In ListView1.Items
                    TotalSum += CDbl(TempNode.SubItems.Item(2).Text)
                    totalqty += CDbl(TempNode.SubItems.Item(3).Text)
                    totaldis += CDbl(TempNode.SubItems.Item(5).Text)
                Next

                ListView2.Items(0).SubItems(0).Text = Math.Round(TotalSum, 2)
                'ListView2.Items(0).SubItems(1).Text = Total_Discount.ToString
                If TotalSum > Convert.ToDecimal(vat_limit) Or TotalSum * -1 > Convert.ToDecimal(vat_limit) Then
                    ListView2.Items(0).SubItems(2).Text = Math.Round(vat * (TotalSum) / 100, 3)
                Else
                    ListView2.Items(0).SubItems(2).Text = "0"
                End If
                ListView2.Items(0).SubItems(1).Text = Math.Round(totaldis, 2)
                ListView2.Items(0).SubItems(3).Text = totalqty
                ListView2.Items(0).SubItems(4).Text = Math.Round(Double.Parse(ListView2.Items(0).SubItems(0).Text) + Double.Parse(ListView2.Items(0).SubItems(2).Text), 2)


                If ListView1.Items.Count = 0 Then
                    ind = 1
                End If


                'ListView1.Items(ListView1.Items.Count - 1).Focused = True
                If ind = 1 Then
                    If return_flag = 1 Or exchange_flag = 1 Then
                        'Label7_Click(sender, e)
                    End If
                    If return_flag = 0 And exchange_flag = 0 And TextBox12.Visible = False And TextBox9.Visible = False Then

                        ListView2.Items(0).SubItems(0).Text = "0"
                        ListView2.Items(0).SubItems(1).Text = "0"
                        ListView2.Items(0).SubItems(2).Text = "0"
                        ListView2.Items(0).SubItems(3).Text = "0"
                        ListView2.Items(0).SubItems(4).Text = "0"
                        TextBox1.Focus()
                        Label3.Visible = True

                        Label9.Visible = True

                        Label16.Visible = True
                        Label24.Visible = True
                        Label25.Visible = True
                        Label31.Visible = True
                        Label52.Visible = True



                        Label5.Visible = False
                        Label53.Visible = False
                        Label7.Visible = False
                        Label4.Visible = False
                        Label6.Visible = False
                        TextBox12.Visible = False
                        Label1.Text = "Enter an item number."
                        TextBox1.Visible = True
                        ComboBox4.Visible = True
                        TextBox1.Focus()
                        Label54.Visible = False


                    End If


                End If

                'Select Item at last index

                ListView1.Items(ListView1.Items.Count - 1).Selected = True

                'count the summation







                If ListView1.Items.Count > 0 Then



                    If com_status = 1 Then


                        Dim sp = New SerialPort(com_port, 9600, Parity.None, 8, StopBits.One)

                        sp.Open()

                        ' to clear the display
                        sp.Write(Convert.ToString(Chr(12)))
                        'first line goes here
                        sp.WriteLine(itm_id + "       " + item_price.ToString + Chr(13))
                        '2nd line goes here
                        sp.WriteLine("Total" + "       " + ListView2.Items(0).SubItems(4).Text)
                        sp.Dispose()
                        sp.Close()
                    End If

                End If



            End If




        Catch ex As Exception
            conn.Close()
            'TextBox12.Focus()
            If TextBox9.Visible = True Then
                TextBox9.Focus()
            ElseIf TextBox1.Visible = True Then
                ComboBox4.Visible = True
                TextBox1.Focus()
            ElseIf TextBox12.Visible = True Then
                TextBox12.Focus()
            End If


            'MsgBox(ex.Message)
        End Try
        'MessageBox.Show(exchange_flag)
    End Sub

    Private Sub PictureBox11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox11.Click
        Try
            If Label4.Visible = True Then
                Label4_Click(sender, e)
            End If

        Catch ex As Exception

        End Try


    End Sub

    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
        Try
            If Label7.Visible = True Then
                Label7_Click(sender, e)
            End If

            If Label57.Visible = True Then
                Label57_Click(sender, e)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PictureBox8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox8.Click
        Try
            If Label3.Visible = True And Label54.Visible = False Then
                Label3_Click(sender, e)
                Exit Sub
            End If

            If Label54.Visible = True Then
                Label54_Click(sender, e)
            End If

            If Label56.Visible = True Then
                Label56_Click(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        e.Handled = True
        e.SuppressKeyPress = True

    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Dim txt As TextBox = CType(sender, TextBox)



            If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace


            e.Handled = True

            If TextBox3.Text <> "" Then





                user_id = TextBox3.Text
                Label1.Text = "Enter user password"
                TextBox3.Visible = False
                TextBox4.Visible = True

                TextBox4.Focus()









            End If
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        TextBox3.Text = TextBox3.Text.Trim

    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If TextBox4.Text <> "" Then


                Try
                    e.Handled = True

                    ' If sale_flag = 1 Then
                    'sale_flag = 0

                    user_password = TextBox4.Text


                    Dim password As String = ""
                    Dim EN As String = ""
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                    Dim conn_log As New OracleConnection(oradb)
                    Dim cmd_log As New OracleCommand
                    cmd_log.Connection = conn_log
                    cmd_log.CommandText = "select pw_acs_em,NM_EM,id_gp_wrk from pa_em where id_login = '" + user_id + "'"



                    conn_log.Open()
                    Dim rd As OracleDataReader = cmd_log.ExecuteReader
                    If rd.Read Then

                        password = rd.GetValue(0)
                        EN = rd.GetValue(1)
                        position = rd.GetValue(2)


                    End If
                    rd.Close()
                    conn_log.Close()
                    ''''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim cmd_auto_logon As New OracleCommand
                    cmd_auto_logon.Connection = conn_log
                    cmd_auto_logon.CommandText = "delete from auto_logon"
                    conn_log.Open()
                    cmd_auto_logon.ExecuteNonQuery()
                    conn_log.Close()

                    log_flag = get_status()
                    If GetHash(TextBox4.Text.Trim) = password Then
                        If sys_admin_flag = 1 Then


                            login_id = EN & ":" & user_id

                            If position = "8" Then
                                'MessageBox.Show("c")
                                Label2.Text = "F2-Pole"
                                Label8.Text = ""
                                Label2.Visible = True
                                Label8.Visible = True
                                Label9.Visible = False
                                Label52.Visible = False

                                'Label7.Text = "F12-Exit"
                                Label57.Visible = True
                                Label58.Visible = True
                                Label60.Visible = True
                                Label7.Visible = False
                                Label5.Visible = False
                                Label53.Visible = False
                                Label3.Visible = False
                                Label1.Visible = False
                                TextBox4.Visible = False
                                Label55.Visible = True

                            Else

                                Label1.Visible = False
                                TextBox4.Visible = False
                                RichTextBox1.Visible = True
                                RichTextBox1.Text = "User ID doesn't has access rights."

                            End If
                        End If




                        If sys_admin_flag = 0 Then


                            If (position = "6" Or position = "8" Or position = "2") Then
                                If reconcile_flag = 1 Then
                                    ' MessageBox.Show("B")
                                    'RichTextBox1.Visible = True
                                    'RichTextBox1.Enabled = False
                                    Label1.Visible = True
                                    Label1.Text = "Enter amount of float money."
                                    RichTextBox1.BackColor = Color.White
                                    TextBox4.Visible = False


                                    TextBox6.Visible = True
                                    TextBox6.Clear()

                                    TextBox6.Focus()
                                    TextBox3.Visible = False
                                    Label3.Visible = True
                                    Label7.Visible = False
                                    Label7.Text = ("F12-Cancel")
                                    Label7.Location = New Point(249, 510)
                                    'MessageBox.Show("A")

                                End If
                                ' Else
                                '

                                'Exit Sub

                            End If
                        End If

                        If sys_admin_flag = 0 Then


                            If (position = "6" Or position = "8" Or position = "2") And user_flag = 1 Then
                                'MessageBox.Show("A")



                                If log_flag = 1 And reconcile_flag = 0 And user_flag = 1 Then
                                    ' MessageBox.Show("D")
                                    Label1.Text = "Enter an item number."
                                    Label1.Visible = True
                                    TextBox1.Visible = True
                                    ComboBox4.Visible = True
                                    TextBox1.Clear()
                                    TextBox4.Visible = False
                                    ListView1.Visible = True
                                    Panel1.Visible = True
                                    Panel3.Visible = True
                                    ListView2.Visible = True
                                    TextBox1.Enabled = True
                                    Label2.Visible = False
                                    Label5.Visible = False
                                    Label53.Visible = False
                                    Label7.Visible = False
                                    Label7.Text = "F12-Cancel"
                                    Label7.Location = New Point(249, 510)
                                    Label3.Visible = True

                                    TextBox1.Focus()
                                    pos_flag = 1
                                    ' MessageBox.Show("B")
                                    Label5.Text = "F4-E.Receipt"
                                    Label5.Location = New Point(650, 420)
                                    Label5.Visible = False
                                    Label53.Visible = False

                                    Label16.Visible = True
                                    Label24.Visible = True
                                    Label25.Visible = True
                                    Label31.Visible = True

                                    PictureBox10.Enabled = True
                                    PictureBox2.Enabled = True



                                    Label9.Text = "F5-R.Receipt"

                                    Label9.Visible = True
                                    Label52.Visible = True
                                    PictureBox6.Enabled = True

                                    PictureBox4.Enabled = True
                                    PictureBox9.Visible = False

                                    Label11.Visible = True
                                    Label12.Visible = True
                                    Label13.Visible = True
                                    Label14.Visible = True
                                    Label18.Visible = True

                                    Label12.Text = "Cashier: " & user_id
                                    Label14.Text = "Business Date: " & b_date
                                    Label18.Text = "Cash Register# " & ws_id

                                    Dim cmd_auto_logon_insert As New OracleCommand
                                    cmd_auto_logon_insert.Connection = conn_log
                                    cmd_auto_logon_insert.CommandText = "insert into auto_logon values('" + user_id + "','" + position.ToString + "')"
                                    conn_log.Open()
                                    cmd_auto_logon_insert.ExecuteNonQuery()
                                    conn_log.Close()


                                End If

                            End If
                        End If

                        If sys_admin_flag = 0 Then


                            If log_flag = 2 Then
                                'MessageBox.Show("d")
                                RichTextBox1.Visible = True
                                RichTextBox1.Text = "System's business date must be started before you proceed.please enter float amount to start."

                                Label1.Text = "Enter amount of float money."
                                RichTextBox1.BackColor = Color.White
                                TextBox4.Visible = False
                                TextBox5.Visible = True
                                TextBox5.Clear()

                                TextBox5.Focus()
                                TextBox3.Visible = False

                                Label3.Visible = True
                                Label7.Visible = False
                                Label7.Text = "F12-Cancel"
                                Label7.Location = New Point(249, 510)
                                PictureBox2.Enabled = True
                                'MessageBox.Show("C")
                            End If


                        End If
                    Else


                        Label1.Visible = False
                        TextBox4.Visible = False
                        RichTextBox1.Visible = True
                        RichTextBox1.Text = "ERROR:Wrong User ID or Password."


                    End If







                    'rd1.Close()
                    conn_log.Close()

                    TextBox4.Clear()
                    TextBox3.Clear()

                    'End If









                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        End If


    End Sub




    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        TextBox4.Text = TextBox4.Text.Trim

    End Sub

    Public Function get_status() As Double
        Dim conn As New OracleConnection
        Dim cmd As New OracleCommand
        Dim id As Double


        'Dim u_id, pw, ip As String


        'u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


        'pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

        ' ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")

        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
        cmd.CommandText = " select ws_status from serial where ws = '" + ws_id + "'"
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
        'select id_ev from MA_ITM_PRN_PRC_ITM where id_itm='8904150313909'
    End Function
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click
        Try
            If Label8.Visible = True Then
                Label8_Click(sender, e)
            End If

            If Label55.Visible = True Then
                Label55_Click(sender, e)
            End If

            If Label25.Visible = True Then
                Label25_Click(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        Dim txt As TextBox = CType(sender, TextBox)

        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True

        If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

        If e.KeyChar = "." And txt.Text.IndexOf(".") = -1 Then e.Handled = False 'allow single decimal point

        If e.KeyChar = "-" And txt.SelectionStart = 0 Then e.Handled = False 'allow negative number

        If e.KeyChar = Convert.ToChar(13) Then


            Try
                e.Handled = True

                If TextBox5.Text <> "" Then


                    Dim conn As New OracleConnection
                    Dim cmd As New OracleCommand
                    Dim cmd_date As New OracleCommand
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    'update daily_ops set status_flag = '1',start_date = TO_CHAR(SYSDATE, 'dd-mm-yy hh24:mi:ss'),user_id='admin',end_date = '',float_amount = '1000', business_date = TO_CHAR(SYSDATE, 'dd-mm-yyyy')
                    conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                    'TO_CHAR(SYSDATE, 'dd-mm-yyyy')
                    cmd.CommandText = "update daily_ops set status_flag = '1',start_date = TO_CHAR(SYSDATE, 'dd-mm-yy hh24:mi:ss'),user_id='" + login_id + "',end_date = '',float_amount = '" + TextBox5.Text + "', business_date = TO_DATE(SYSDATE-1/(24*60*60), 'dd-MON-yy')"
                    cmd.Connection = conn

                    conn.Open()

                    cmd.ExecuteNonQuery()

                    conn.Close()
                    '---------------------------
                    cmd.CommandText = "update serial set ws_status = '1' where ws = '" + ws_id + "' "
                    cmd.Connection = conn

                    conn.Open()

                    cmd.ExecuteNonQuery()

                    conn.Close()
                    PictureBox9.Visible = False

                    Label11.Visible = True
                    Label12.Visible = True
                    Label13.Visible = True
                    Label14.Visible = True
                    Label18.Visible = True
                    Label12.Text = "Cashier: " & user_id

                    Label18.Text = "Cash Register#" & ws_id

                    Label1.Text = "Enter an item number."
                    TextBox1.Visible = True
                    ComboBox4.Visible = True
                    TextBox4.Visible = False
                    ListView1.Visible = True
                    Panel1.Visible = True
                    Panel3.Visible = True
                    ListView2.Visible = True
                    TextBox1.Enabled = True
                    Label2.Visible = False
                    Label5.Visible = False
                    Label5.Text = "F4-E.Receipt"

                    Label5.Location = New Point(650, 420)
                    Label53.Visible = False

                    Label9.Visible = True
                    Label9.Text = "F5-R.Receipt"
                    Label52.Visible = True

                    TextBox1.Focus()
                    RichTextBox1.Visible = False
                    TextBox6.Visible = False
                    pos_flag = 1
                    TextBox5.Visible = False

                    Dim b_date As String = ""
                    Label16.Visible = True
                    Label24.Visible = True
                    Label25.Visible = True
                    Label31.Visible = True

                    PictureBox10.Enabled = True
                    exchange_flag = 0
                    refund_flag = 0









                    conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                    cmd_date.CommandText = "select business_date from daily_ops"
                    cmd_date.Connection = conn

                    conn.Open()
                    Dim rd_date As OracleDataReader = cmd_date.ExecuteReader
                    If rd_date.Read Then
                        b_date = rd_date.GetValue(0)
                        Label14.Text = "Business Date: " & b_date

                    End If
                    rd_date.Close()
                    conn.Close()
                    ''''''''''''''''''''auto_logon
                    Dim cmd_auto_logon_insert As New OracleCommand
                    cmd_auto_logon_insert.Connection = conn
                    cmd_auto_logon_insert.CommandText = "insert into auto_logon values('" + user_id + "')"
                    conn.Open()
                    cmd_auto_logon_insert.ExecuteNonQuery()
                    conn.Close()
                    If receipt_lang = "2" Then


                        '------------------------------------------Print open report
                        Dim file As System.IO.StreamWriter
                        file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\TILL_OPEN_REPORT" + b_date + ".txt", True)

                        file.WriteLine("Till Summary")
                        file.WriteLine("------------------")
                        file.WriteLine(company_name)
                        file.WriteLine(store_name)
                        file.WriteLine(user_id)
                        file.WriteLine("------------------")
                        file.WriteLine("                   ")
                        file.WriteLine("------------------")
                        file.WriteLine(System.DateTime.Now)
                        file.WriteLine("===================")
                        file.WriteLine("===================")
                        file.WriteLine("Starting Till Float Count")
                        file.WriteLine("===================")
                        file.WriteLine("===================")
                        file.WriteLine("Cash Denomination")
                        file.WriteLine(TextBox5.Text)
                        file.WriteLine("END OF REPORT ------")
                        file.Close()

                        Dim proc As New Process


                        proc.StartInfo.FileName = "C:\POS\Receipts\TILL_OPEN_REPORT" + b_date + ".txt"


                        proc.StartInfo.Verb = "Print"


                        proc.StartInfo.CreateNoWindow = True

                        If printer_status = "1" Then


                            proc.Start()

                            proc.WaitForExit()
                        End If
                        'Label1.Visible = False
                    End If


                    If receipt_lang = "1" Then


                        '------------------------------------------Print open report
                        Dim file As System.IO.StreamWriter
                        file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\TILL_OPEN_REPORT" + b_date + ".txt", True)

                        file.WriteLine("Till Summary")
                        file.WriteLine("------------------")
                        file.WriteLine(company_name)
                        file.WriteLine(store_name)
                        file.WriteLine(user_id)
                        file.WriteLine("------------------")
                        file.WriteLine("                   ")
                        file.WriteLine("------------------")
                        file.WriteLine(System.DateTime.Now)
                        file.WriteLine("===================")
                        file.WriteLine("===================")
                        file.WriteLine("Starting Till Float Count")
                        file.WriteLine("===================")
                        file.WriteLine("===================")
                        file.WriteLine("Cash Denomination")
                        file.WriteLine(TextBox5.Text)
                        file.WriteLine("END OF REPORT ------")
                        file.Close()

                        Dim proc As New Process


                        proc.StartInfo.FileName = "C:\POS\Receipts\TILL_OPEN_REPORT" + b_date + ".txt"


                        proc.StartInfo.Verb = "Print"


                        proc.StartInfo.CreateNoWindow = True

                        If printer_status = "1" Then


                            proc.Start()

                            proc.WaitForExit()
                        End If
                        'Label1.Visible = False
                    End If
                Else
                    RichTextBox1.Text = "Please enter float amount!"
                    RichTextBox1.Visible = True
                End If
                Me.Show()
                Me.Focus()
            Catch ex As Exception
                'MessageBox.Show(ex.Message)
            End Try

        End If

    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        'Dim digitsOnly 'As Regex = New Regex("[^\d]")
        'TextBox2.Text = digitsOnly.Replace(TextBox2.Text, "")
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        Try


            If Label5.Text = "F4-Administrator" Then
                PictureBox9.Visible = False
                TextBox3.Enabled = True
                TextBox3.Visible = True
                TextBox3.Focus()
                Label2.Visible = False
                Label8.Visible = False
                Label5.Visible = False
                Label53.Visible = False
                Label7.Text = "F12-Undo"
                Label7.Location = New Point(258, 510)
                Label7.Visible = True
                PictureBox7.Enabled = True

                Label1.Visible = True
                Label1.Text = "Enter user name"
                Label9.Visible = False
                Label52.Visible = False

                'pos_flag = 1
                admin_flag = 1
                sys_admin_flag = 1



            End If

            If Label5.Text = "F4-E.Receipt" Then

                If (position = "6" Or position = "8") Then


                    subtotal = ListView2.Items(0).SubItems(0).Text
                    receipt_status = 1
                    Label54.Visible = True
                    Label7.Visible = False
                    Label3.Visible = False
                    TextBox8.Visible = True
                    TextBox8.Clear()
                    TextBox8.Focus()
                    Label1.Visible = True
                    Label1.Text = "Enter receipt number."
                    Label6.Visible = False
                    Label9.Visible = False
                    Label52.Visible = False


                    Label5.Visible = False
                    Label53.Visible = False
                    Label4.Visible = False

                    TextBox1.Visible = False
                    ComboBox4.Visible = False
                    TextBox2.Visible = False
                    TextBox3.Visible = False
                    TextBox4.Visible = False
                    TextBox5.Visible = False
                    TextBox6.Visible = False
                    TextBox7.Visible = False
                    TextBox9.Visible = False
                    TextBox10.Visible = False
                    TextBox12.Visible = False




                    pos_flag = 0
                    '  MessageBox.Show("D")


                    exchange_flag = 1
                    For i = 0 To ListView1.Items.Count
                        ListView1.Items(i).BackColor = Color.Gray

                    Next




                Else
                    Label17.Text = "ERROR4:User ID dosn't has access rights."
                    Label17.Visible = True
                    Button1.Visible = True
                    ListView1.Visible = False
                    Panel1.Visible = False
                    Panel3.Visible = False
                    ListView2.Visible = False
                    TextBox1.Visible = False
                    ComboBox4.Visible = False
                    TextBox2.Visible = False
                    TextBox3.Visible = False
                    TextBox4.Visible = False
                    TextBox5.Visible = False
                    TextBox6.Visible = False
                    TextBox7.Visible = False
                    TextBox8.Visible = False
                    TextBox9.Visible = False
                    Label1.Visible = False
                    Label2.Visible = False
                    Label3.Visible = False
                    Label4.Visible = False
                    Label5.Visible = False
                    Label6.Visible = False
                    Label7.Visible = False
                    Label8.Visible = False
                    Label9.Visible = False
                    Label16.Visible = False
                    Label24.Visible = False
                    Label25.Visible = False
                    Label31.Visible = False
                    Label52.Visible = False
                    Label53.Visible = False


                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click
        Try

            If Label9.Text = "F3-Z Report" Then
                PictureBox9.Visible = False
                TextBox3.Visible = True
                TextBox3.Focus()
                Label2.Visible = False
                Label8.Visible = False
                Label5.Visible = False
                Label53.Visible = False
                Label7.Text = "F12-Undo"
                Label7.Location = New Point(258, 510)
                Label7.Visible = True
                PictureBox7.Enabled = True
                TextBox3.Clear()


                Label9.Visible = False
                Label52.Visible = False

                Label1.Visible = True
                Label1.Text = "Enter user name"
                reconcile_flag = 1

            End If

            If position = "8" Or position = "6" Then


                If Label9.Text = "F5-R.Receipt" Then


                    TextBox8.Visible = True
                    TextBox8.Enabled = True
                    TextBox8.Focus()
                    Label1.Visible = True

                    Label1.Text = "Enter receipt number."
                    Label6.Visible = False
                    Label9.Visible = False
                    Label52.Visible = False

                    Label5.Visible = False
                    Label53.Visible = False
                    TextBox1.Visible = False
                    ComboBox4.Visible = False

                    pos_flag = 0
                    '  MessageBox.Show("D")

                    TextBox8.Clear()
                    refund_flag = 1
                    return_flag = 1
                    Label16.Visible = False
                    Label24.Visible = False
                    Label25.Visible = False
                    Label31.Visible = False


                    PictureBox7.Enabled = True


                End If
            ElseIf Label9.Text <> "F3-Z Report" Then
                Label17.Text = "ERROR4:User ID dosn't has access rights."
                Label17.Visible = True
                Button1.Visible = True
                ListView1.Visible = False
                Panel1.Visible = False
                Panel3.Visible = False
                ListView2.Visible = False
                TextBox1.Visible = False
                ComboBox4.Visible = False
                TextBox2.Visible = False
                TextBox3.Visible = False
                TextBox4.Visible = False
                TextBox5.Visible = False
                TextBox6.Visible = False
                TextBox7.Visible = False
                TextBox8.Visible = False
                TextBox9.Visible = False
                Label1.Visible = False
                Label2.Visible = False
                Label3.Visible = False
                Label4.Visible = False
                Label5.Visible = False
                Label6.Visible = False
                Label7.Visible = False
                Label8.Visible = False
                Label9.Visible = False
                Label16.Visible = False
                Label24.Visible = False
                Label25.Visible = False
                Label31.Visible = False
                Label52.Visible = False


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress

        Dim txt As TextBox = CType(sender, TextBox)

        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True

        If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

        If e.KeyChar = "." And txt.Text.IndexOf(".") = -1 Then e.Handled = False 'allow single decimal point

        If e.KeyChar = "-" And txt.SelectionStart = 0 Then e.Handled = False 'allow negative number

        If e.KeyChar = Convert.ToChar(13) Then

            e.Handled = True
            Try


                If TextBox6.Text <> "" Then
                    Dim register As Integer = 0



                    Dim conn As New OracleConnection
                    Dim cmd As New OracleCommand
                    Dim cmd_BD As New OracleCommand
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    'update daily_ops set status_flag = '1',start_date = TO_CHAR(SYSDATE, 'dd-mm-yy hh24:mi:ss'),user_id='admin',end_date = '',float_amount = '1000', business_date = TO_CHAR(SYSDATE, 'dd-mm-yyyy')
                    conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"

                    cmd.CommandText = "update serial set ws_status = '2' where ws = '" + ws_id + "' "
                    cmd.Connection = conn

                    conn.Open()

                    cmd.ExecuteNonQuery()

                    conn.Close()
                    '-----------------------------

                    cmd.CommandText = "select ws_status from serial"

                    cmd.Connection = conn
                    conn.Open()
                    Dim rd_register As OracleDataReader = cmd.ExecuteReader
                    While rd_register.Read
                        If rd_register.GetValue(0).ToString = "1" Then
                            register = 1
                        End If
                    End While
                    rd_register.Close()
                    conn.Close()




                    '------------------------------------------------------
                    If register = 0 Then


                        cmd.CommandText = "update daily_ops set status_flag = '2',user_id='" + login_id + "',end_date = TO_CHAR(SYSDATE, 'dd-mm-yy hh24:mi:ss'),float_amount = '" + TextBox6.Text + "'"
                        cmd.Connection = conn

                        conn.Open()

                        cmd.ExecuteNonQuery()

                        conn.Close()
                        Label1.Visible = True

                        cmd_BD.Connection = conn
                        cmd_BD.CommandText = "insert into business_Date values('System',sysdate)"
                        conn.Open()
                        'cmd_BD.ExecuteNonQuery()
                        conn.Close()




                        ''''''''''''''''''''''''''''''''''''''''''calculate Z report values.




                        cmd1_register.CommandText = "select business_date from daily_ops"
                        cmd1_register.Connection = conn
                        conn.Open()

                        Dim rd_R As OracleDataReader = cmd1_register.ExecuteReader
                        If rd_R.Read Then
                            b_date = rd_R.GetValue(0)

                        End If
                        conn.Close()
                        rd_R.Close()
                        'total sales
                        cmd2.CommandText = "select sum(total) from   receipt_numbers where ops_type = 'sale'  and  business_date ='" + b_date + "'"
                        cmd2.Connection = conn

                        conn.Open()
                        Dim rd_total_sales_WS As OracleDataReader = cmd2.ExecuteReader
                        If rd_total_sales_WS.Read Then
                            total_Sales = rd_total_sales_WS.GetValue(0).ToString

                        End If
                        rd_total_sales_WS.Close()
                        conn.Close()

                        cmd3_register.CommandText = "select sum(total) from   receipt_numbers where     business_date ='" + b_date + "'"
                        cmd3_register.Connection = conn

                        conn.Open()
                        Dim rd_net_total_WS As OracleDataReader = cmd3_register.ExecuteReader
                        If rd_net_total_WS.Read Then
                            Net_Sales = rd_net_total_WS.GetValue(0).ToString

                        End If
                        rd_net_total_WS.Close()
                        conn.Close()

                        cmd4_register.CommandText = "select sum(total) from   receipt_numbers where ops_type = 'REFUND'  and  business_date ='" + b_date + "'"
                        cmd4_register.Connection = conn

                        conn.Open()
                        Dim rd_total_refund_WS As OracleDataReader = cmd4_register.ExecuteReader
                        If rd_total_refund_WS.Read Then
                            total_refund = rd_total_refund_WS.GetValue(0).ToString

                        End If
                        rd_total_refund_WS.Close()
                        conn.Close()




                        '
                        cmd5_register.CommandText = "select sum(total) from receipt_numbers where tender_type = 'CASH' and business_date ='" + b_date + "'"
                        cmd5_register.Connection = conn
                        conn.Open()
                        Dim rd_total_cash_WS As OracleDataReader = cmd5_register.ExecuteReader
                        If rd_total_cash_WS.Read Then
                            total_cash = rd_total_cash_WS.GetValue(0).ToString

                        End If
                        rd_total_cash_WS.Close()
                        conn.Close()

                        '
                        cmd6_register.CommandText = "select sum(total) from receipt_numbers where tender_type = 'CRDT' and business_date ='" + b_date + "'"
                        cmd6_register.Connection = conn
                        conn.Open()
                        Dim rd_total_crdt_WS As OracleDataReader = cmd6_register.ExecuteReader
                        If rd_total_crdt_WS.Read Then
                            total_crdt = rd_total_crdt_WS.GetValue(0).ToString

                        End If
                        rd_total_crdt_WS.Close()
                        conn.Close()

                        ''
                        cmd7_register.CommandText = "select sum(total) from receipt_numbers where ops_type='EXCHANGE'  and  business_date ='" + b_date + "'"
                        cmd7_register.Connection = conn

                        conn.Open()
                        Dim rd_total_exchange_WS As OracleDataReader = cmd7_register.ExecuteReader
                        If rd_total_exchange_WS.Read Then
                            total_exchage = rd_total_exchange_WS.GetValue(0).ToString

                        End If
                        rd_total_exchange_WS.Close()
                        conn.Close()
                        ''''''
                        cmd8_register.CommandText = "select sum(VAT) from receipt_numbers_WS where   business_date ='" + b_date + "'"
                        cmd8_register.Connection = conn

                        conn.Open()
                        Dim rd_VAT_WS As OracleDataReader = cmd8_register.ExecuteReader
                        If rd_VAT_WS.Read Then
                            vat = rd_VAT_WS.GetValue(0).ToString

                        End If
                        rd_VAT_WS.Close()
                        conn.Close()


                    End If
                    '''''''''''''''''''''''''''


                    If receipt_lang = "1" Then


                        '----------------------------------------print z report
                        Dim file1 As System.IO.StreamWriter
                        file1 = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\CLOSING_REPORT" + b_date + ".txt", True)
                        file1.WriteLine("                        ")
                        file1.WriteLine("Summary Report(Z-Report)")
                        file1.WriteLine("------------------")
                        file1.WriteLine(user_id)
                        file1.WriteLine(store_name)
                        file1.WriteLine("                   ")
                        file1.WriteLine("------------------")
                        file1.WriteLine(System.DateTime.Now)
                        file1.WriteLine("===================")
                        file1.WriteLine("===================")
                        ' file1.WriteLine("Closing float amount: " & TextBox6.Text)
                        file1.WriteLine("===================")
                        file1.WriteLine("===================")
                        file1.WriteLine("    " & "Total Sales: " & total_Sales)
                        file1.WriteLine("    " & "Net Sales :" & Net_Sales)
                        file1.WriteLine("    " & "Total Refund: " & total_refund)
                        file1.WriteLine("    " & "Total Exchage: " & total_exchage)
                        file1.WriteLine("    " & "Total Cash: " & total_cash)
                        file1.WriteLine("    " & "Total Credit: " & total_crdt)
                        file1.WriteLine("    " & "VAT%: " & vat)
                        file1.WriteLine("===================")
                        file1.WriteLine("===================")
                        file1.WriteLine("***")
                        file1.WriteLine("END OF REPORT ------")
                        file1.Close()

                        Dim proc1 As New Process


                        proc1.StartInfo.FileName = "C:\POS\Receipts\CLOSING_REPORT" + b_date + ".txt"


                        proc1.StartInfo.Verb = "Print"


                        proc1.StartInfo.CreateNoWindow = True

                        If printer_status = "1" Then


                            proc1.Start()

                            proc1.WaitForExit()

                        End If


                    End If

                    '''''''''''''''''''''''''''


                    If receipt_lang = "2" Then


                        '----------------------------------------print z report
                        Dim file1 As System.IO.StreamWriter
                        file1 = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\CLOSING_REPORT" + b_date + ".txt", True)

                        file1.WriteLine("                        ")
                        file1.WriteLine("Summary Report(Z-Report)")
                        file1.WriteLine("------------------")
                        file1.WriteLine(user_id)
                        file1.WriteLine(store_name)
                        file1.WriteLine("                   ")
                        file1.WriteLine("------------------")
                        file1.WriteLine(System.DateTime.Now)
                        file1.WriteLine("===================")
                        file1.WriteLine("===================")
                        ' file1.WriteLine("Closing float amount: " & TextBox6.Text)
                        file1.WriteLine("===================")
                        file1.WriteLine("===================")
                        file1.WriteLine("    " & "Total Sales: " & total_Sales & "    أجمالي المبيعات")
                        file1.WriteLine("    " & "Net Sales :" & Net_Sales & "   صافي المبيعات")
                        file1.WriteLine("    " & "Total Refund: " & total_refund & "    أجمالي المسترجع")
                        file1.WriteLine("    " & "Total Exchage: " & total_exchage & "   أجمالي الاستبدال")
                        file1.WriteLine("    " & "Total Cash: " & total_cash & "     أجمالي المبيعات النقدية")
                        file1.WriteLine("    " & "Total Credit: " & total_crdt & "   أجمالي المبيعات بطاقة إئتمانية")
                        file1.WriteLine("    " & "VAT%: " & vat & "      أجمالي ضريبة القيمة المضافة")
                        file1.WriteLine("===================")
                        file1.WriteLine("===================")
                        file1.WriteLine("***")
                        file1.WriteLine("END OF REPORT ------")
                        file1.Close()

                        Dim proc1 As New Process


                        proc1.StartInfo.FileName = "C:\POS\Receipts\CLOSING_REPORT" + b_date + ".txt"


                        proc1.StartInfo.Verb = "Print"


                        proc1.StartInfo.CreateNoWindow = True

                        If printer_status = "1" Then


                            proc1.Start()

                            proc1.WaitForExit()

                        End If


                    End If


                    '---------------------------
                    ''''''''''''''''''''''''''''''''''''''''''calculate Register report values.




                    cmd1_register.CommandText = "select business_date from daily_ops"
                    cmd1_register.Connection = conn
                    conn.Open()

                    Dim rd As OracleDataReader = cmd1_register.ExecuteReader
                    If rd.Read Then
                        b_date = rd.GetValue(0)

                    End If
                    conn.Close()
                    rd.Close()
                    'total sales
                    cmd2.CommandText = "select sum(total) from   receipt_numbers_WS where ops_type = 'sale'  and  business_date ='" + b_date + "' and WS = '" + ws_id + "'"
                    cmd2.Connection = conn

                    conn.Open()
                    Dim rd_total_sales As OracleDataReader = cmd2.ExecuteReader
                    If rd_total_sales.Read Then
                        total_Sales = rd_total_sales.GetValue(0).ToString

                    End If
                    rd_total_sales.Close()
                    conn.Close()

                    cmd3_register.CommandText = "select sum(total) from   receipt_numbers_WS where     business_date ='" + b_date + "' and WS='" + ws_id + "'"
                    cmd3_register.Connection = conn

                    conn.Open()
                    Dim rd_net_total As OracleDataReader = cmd3_register.ExecuteReader
                    If rd_net_total.Read Then
                        Net_Sales = rd_net_total.GetValue(0).ToString

                    End If
                    rd_net_total.Close()
                    conn.Close()

                    cmd4_register.CommandText = "select sum(total) from   receipt_numbers_WS where ops_type = 'REFUND'  and  business_date ='" + b_date + "' and WS = '" + ws_id + "'"
                    cmd4_register.Connection = conn

                    conn.Open()
                    Dim rd_total_refund As OracleDataReader = cmd4_register.ExecuteReader
                    If rd_total_refund.Read Then
                        total_refund = rd_total_refund.GetValue(0).ToString

                    End If
                    rd_total_refund.Close()
                    conn.Close()




                    '
                    cmd5_register.CommandText = "select sum(total) from receipt_numbers_WS where tender_type = 'CASH' and business_date ='" + b_date + "' and WS = '" + ws_id + "'"
                    cmd5_register.Connection = conn
                    conn.Open()
                    Dim rd_total_cash As OracleDataReader = cmd5_register.ExecuteReader
                    If rd_total_cash.Read Then
                        total_cash = rd_total_cash.GetValue(0).ToString

                    End If
                    rd_total_cash.Close()
                    conn.Close()

                    '
                    cmd6_register.CommandText = "select sum(total) from receipt_numbers_WS where tender_type = 'CRDT' and business_date ='" + b_date + "' and WS = '" + ws_id + "'"
                    cmd6_register.Connection = conn
                    conn.Open()
                    Dim rd_total_crdt As OracleDataReader = cmd6_register.ExecuteReader
                    If rd_total_crdt.Read Then
                        total_crdt = rd_total_crdt.GetValue(0).ToString

                    End If
                    rd_total_crdt.Close()
                    conn.Close()

                    ''
                    cmd7_register.CommandText = "select sum(total) from receipt_numbers_WS where ops_type='EXCHANGE'  and  business_date ='" + b_date + "' and WS = '" + ws_id + "'"
                    cmd7_register.Connection = conn

                    conn.Open()
                    Dim rd_total_exchange As OracleDataReader = cmd7_register.ExecuteReader
                    If rd_total_exchange.Read Then
                        total_exchage = rd_total_exchange.GetValue(0).ToString

                    End If
                    rd_total_exchange.Close()
                    conn.Close()
                    ''''''
                    cmd8_register.CommandText = "select sum(VAT) from receipt_numbers_WS where   business_date ='" + b_date + "' and WS = '" + ws_id + "'"
                    cmd8_register.Connection = conn

                    conn.Open()
                    Dim rd_VAT As OracleDataReader = cmd8_register.ExecuteReader
                    If rd_VAT.Read Then
                        vat = rd_VAT.GetValue(0).ToString

                    End If
                    rd_VAT.Close()
                    conn.Close()

                    '''''''''''''
                    If receipt_lang = "1" Then



                        '----------------------------------------print Register report
                        Dim file As System.IO.StreamWriter
                        file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\REGISTER_CLOSING_REPORT" + b_date + ".txt", True)

                        file.WriteLine("                        ")
                        file.WriteLine("Summary Report(Register-Report)")
                        file.WriteLine("Cash Register# " & ws_id)
                        file.WriteLine("------------------")
                        file.WriteLine(user_id)
                        file.WriteLine(store_name)
                        file.WriteLine("                   ")
                        file.WriteLine("------------------")
                        file.WriteLine(System.DateTime.Now)
                        file.WriteLine("===================")
                        file.WriteLine("===================")
                        file.WriteLine("Closing float amount: " & TextBox6.Text)
                        file.WriteLine("===================")
                        file.WriteLine("===================")
                        file.WriteLine("    " & "Total Sales: " & total_Sales)
                        file.WriteLine("    " & "Net Sales :" & Net_Sales)
                        file.WriteLine("    " & "Total Refund: " & total_refund)
                        file.WriteLine("    " & "Total Exchage: " & total_exchage)
                        file.WriteLine("    " & "Total Cash: " & total_cash)
                        file.WriteLine("    " & "Total Credit: " & total_crdt)
                        file.WriteLine("===================")
                        file.WriteLine("VAT Summary | ملخص الضرائب")
                        file.WriteLine("    " & "VAT%: " & vat)
                        file.WriteLine("    " & "VAT%: " & (vat * Net_Sales) / 100)
                        file.WriteLine("===================")
                        file.WriteLine("***")
                        file.WriteLine("END OF REPORT ------")
                        file.Close()

                        Dim proc As New Process


                        proc.StartInfo.FileName = "C:\POS\Receipts\REGISTER_CLOSING_REPORT" + b_date + ".txt"


                        proc.StartInfo.Verb = "Print"


                        proc.StartInfo.CreateNoWindow = True

                        If printer_status = "1" Then


                            proc.Start()

                            proc.WaitForExit()
                        End If

                    End If
                    '''''''''''''

                    If receipt_lang = "2" Then



                        '----------------------------------------print Register report
                        Dim file As System.IO.StreamWriter
                        file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\REGISTER_CLOSING_REPORT" + b_date + ".txt", True)

                        file.WriteLine("                        ")
                        file.WriteLine("Summary Report(Register-Report)")
                        file.WriteLine("Cash Register# " & ws_id)
                        file.WriteLine("------------------")
                        file.WriteLine(user_id)
                        file.WriteLine(store_name)
                        file.WriteLine("                   ")
                        file.WriteLine("------------------")
                        file.WriteLine(System.DateTime.Now)
                        file.WriteLine("===================")
                        file.WriteLine("===================")
                        file.WriteLine("Closing float amount: " & TextBox6.Text)
                        file.WriteLine("===================")
                        file.WriteLine("===================")
                        file.WriteLine("    " & "Total Sales: " & total_Sales & "    أجمالي المبيعات")
                        file.WriteLine("    " & "Net Sales :" & Net_Sales & "   صافي المبيعات")
                        file.WriteLine("    " & "Total Refund: " & total_refund & "    أجمالي المسترجع")
                        file.WriteLine("    " & "Total Exchage: " & total_exchage & "   أجمالي الاستبدال")
                        file.WriteLine("    " & "Total Cash: " & total_cash & "     أجمالي المبيعات النقدية")
                        file.WriteLine("    " & "Total Credit: " & total_crdt & "   أجمالي المبيعات بطاقة إئتمانية")
                        file.WriteLine("    " & "VAT%: " & vat & "      أجمالي ضريبة القيمة المضافة")
                        file.WriteLine("===================")
                        file.WriteLine("===================")
                        file.WriteLine("***")
                        file.WriteLine("END OF REPORT ------")
                        file.Close()

                        Dim proc As New Process


                        proc.StartInfo.FileName = "C:\POS\Receipts\REGISTER_CLOSING_REPORT" + b_date + ".txt"


                        proc.StartInfo.Verb = "Print"


                        proc.StartInfo.CreateNoWindow = True

                        If printer_status = "1" Then


                            proc.Start()

                            proc.WaitForExit()
                        End If

                    End If
                    TextBox6.Clear()
                    TextBox6.Visible = False
                    Label1.Visible = False
                    Label3.Visible = False
                    Label2.Visible = True
                    Label5.Visible = True
                    reconcile_flag = 0
                    PictureBox9.Visible = True





                Else
                    RichTextBox1.Text = "Please enter float amount before closing the business date!"
                    RichTextBox1.Visible = True
                End If





                'Me.Close()
                'End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            MsgBox("The business date has been closed,Press OK to close.", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        Try
            RichTextBox1.Visible = False
        Catch ex As Exception

        End Try


    End Sub

    Private Sub TextBox7_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox7.KeyDown
        Try


            If e.KeyCode = Keys.Escape Then
                If Label3.Visible = True And Label3.Enabled = True Then
                    Label3_Click(sender, e)
                End If


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Try
                e.Handled = True
                If TextBox7.Text <> "" Then

                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If

                    'Buy x get y discount
                    conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                    Dim cmd_update As New OracleCommand
                    cmd_update.Connection = conn
                    cmd_update.CommandText = "update Buy_X_GET_Y_Discount set c = :nodec"
                    cmd_update.Parameters.Add("nodec", OracleDbType.Double).Value = 0
                    conn.Open()
                    cmd_update.ExecuteNonQuery()
                    conn.Close()

                    If Label8.Text = "F4-Credit" And exchange_flag = 1 Then


                        'If Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text) >= 0 Then


                        'If Convert.ToDecimal(TextBox2.Text) < Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text) Then
                        ' MessageBox.Show("the amount of money is less than receipt total.")
                        'MsgBox("the amount of money is less than receipt total..", MsgBoxStyle.Information, "ERROR")

                        'Else

                        Label1.Text = "Enter an item number."

                        TextBox1.Focus()
                        Label2.Visible = False
                        Label8.Visible = False

                        TextBox2.Visible = False
                        TextBox1.Visible = True
                        ComboBox4.Visible = True
                        TextBox1.Enabled = True





                        Label7.Visible = False
                        Label4.Visible = False
                        Label3.Visible = False
                        Label6.Visible = False
                        Label9.Visible = False
                        Label52.Visible = False



                        Label6.Text = "F6-Tender"
                        Label5.Visible = False
                        Label53.Visible = False
                        Label16.Visible = False
                        Label24.Visible = False
                        Label25.Visible = False
                        Label31.Visible = False




                        TextBox1.Focus()

                        ListView1.Enabled = True






                        ' --------------------------------------
                        rn = RN_ID()
                        Dim year As String
                        Dim day As String
                        Dim month As String
                        Dim hour As String
                        Dim minute As String


                        year = Date.Today.Year.ToString
                        day = Date.Today.Day.ToString
                        month = Date.Today.Month.ToString
                        hour = Date.Now.Hour.ToString
                        minute = Date.Now.Minute.ToString


                        rr = year + month + day + u_id + ws_id + rn.ToString
                        rr_barcode = rr

                        Dim conn As New OracleConnection
                        Dim cmd As New OracleCommand
                        Dim cmd1 As New OracleCommand
                        Dim cmd2_1 As New OracleCommand
                        Dim cmd2_2 As New OracleCommand
                        Dim cmd2_3 As New OracleCommand
                        Dim cmd3 As New OracleCommand
                        Dim cmd4 As New OracleCommand
                        Dim cmd5 As New OracleCommand
                        Dim cmd_date As New OracleCommand
                        Dim b_date As String = ""


                        Threading.Thread.Sleep(2000) ' 500 milliseconds = 0.5 seconds
                        If conn.State = ConnectionState.Open Then
                            conn.Close()
                        End If
                        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                        cmd_date.CommandText = "select business_date from daily_ops"
                        cmd_date.Connection = conn

                        conn.Open()
                        Dim rd_date As OracleDataReader = cmd_date.ExecuteReader
                        If rd_date.Read Then
                            b_date = rd_date.GetValue(0)

                        End If
                        rd_date.Close()
                        conn.Close()

                        cmd.CommandText = " insert into receipt_numbers values(TO_CHAR(SYSDATE, 'dd-mm-yy')," + rn + "," + rr + ",'" + ListView2.Items(0).SubItems(3).Text + "','" + ListView2.Items(0).SubItems(4).Text + "','" + ListView2.Items(0).SubItems(2).Text + "','" + user_id + "','CRDT','EXCHANGE',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh:mm:ss'),'" + b_date + "','" + TextBox7.Text + "') "
                        cmd.Connection = conn

                        conn.Open()
                        cmd.ExecuteNonQuery()
                        conn.Close()
                        '--------------------
                        cmd.CommandText = " insert into receipt_numbers_WS values(TO_CHAR(SYSDATE, 'dd-mm-yy')," + rn + "," + rr + ",'" + ListView2.Items(0).SubItems(3).Text + "','" + ListView2.Items(0).SubItems(4).Text + "','" + ListView2.Items(0).SubItems(2).Text + "','" + user_id + "','CRDT','EXCHANGE',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh:mm:ss'),'" + b_date + "','" + TextBox7.Text + "'," + ws_id + ") "
                        cmd.Connection = conn

                        conn.Open()
                        cmd.ExecuteNonQuery()
                        conn.Close()
                        TextBox7.Clear()
                        Label3.Visible = True
                        Label2.Text = ""
                        pos_flag = 1

                        '------------------------------
                        Dim cmd_DSF As New OracleCommand
                        For i = 0 To ListView1.Items.Count - 1



                            cmd_DSF.CommandText = "insert into DSF values('" + u_id + "','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(1).Text + "','" + ListView1.Items(i).SubItems(2).Text + "','" + b_date + "','" + System.DateTime.Now + "','Exchange','" + ListView1.Items(i).SubItems(2).Text + "','" + user_id + "')"
                            cmd_DSF.Connection = conn

                            conn.Open()
                            cmd_DSF.ExecuteNonQuery()
                            conn.Close()
                            Dim path As String = "C:\POS\OUT"
                            If Directory.Exists(path) Then
                                '------------------------------------------Print open report
                                Dim file_DSF As System.IO.StreamWriter
                                file_DSF = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\OUT\DSF_" + b_date + ".txt", True)

                                file_DSF.WriteLine(u_id & "|" & ListView1.Items(i).SubItems(0).Text & "|" & ListView1.Items(i).SubItems(1).Text & "|" & ListView1.Items(i).SubItems(2).Text & "|" & b_date & "|" & System.DateTime.Now & "|" & "Exchange" & "|" & ListView1.Items(i).SubItems(2).Text & "|" & user_id)


                                file_DSF.Close()
                            End If
                        Next
                        '------------------------------------------

                        For i = 0 To ListView1.Items.Count - 1
                            AI_TRN = rn & i.ToString


                            cmd1.CommandText = "insert into tr_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",'admin',1,'',sysdate,sysdate,0,0,2,'','',0,'','','-1','-1','-1',0,0,sysdate,sysdate,0)"
                            cmd1.Connection = conn

                            conn.Open()
                            cmd1.ExecuteNonQuery()
                            conn.Close()


                            cmd2_1.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'SR','','',0,-1,sysdate,sysdate)"
                            cmd2_1.Connection = conn

                            conn.Open()
                            cmd2_1.ExecuteNonQuery()
                            conn.Close()

                            cmd2_2.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",1,'TX','','',0,-1,sysdate,sysdate)"
                            cmd2_2.Connection = conn

                            conn.Open()
                            cmd2_2.ExecuteNonQuery()
                            conn.Close()

                            cmd2_3.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",2,'TN','','',0,-1,sysdate,sysdate)"
                            cmd2_3.Connection = conn

                            conn.Open()
                            cmd2_3.ExecuteNonQuery()
                            conn.Close()

                            Dim quan As Integer = ListView1.Items(i).SubItems(3).Text
                            cmd3.CommandText = "insert into tr_ltm_sls_rtn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'',0,'1601','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(0).Text + "','" + quan.ToString + "','" + (Double.Parse(ListView1.Items(i).SubItems(2).Text) / CF).ToString + "' ,'" + (Double.Parse(ListView1.Items(i).SubItems(5).Text) / CF).ToString + "' ,0,'',0,'',0,'','KEY','','',0,'','','-1','','',0,'','',0,0,'','','',0,0,0,0,'',0,'" + rr + "',1,-1,1,0,0,sysdate,sysdate,0,'" + (Double.Parse(ListView1.Items(i).SubItems(2).Text) / CF).ToString + "' )"
                            cmd3.Connection = conn

                            conn.Open()
                            cmd3.ExecuteNonQuery()
                            conn.Close()

                            If ListView1.Items(i).SubItems(3).Text.ToString = "1" Then
                                cmd4.CommandText = "insert into TR_LTM_SLS_RTN_RECEIPT values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'',0,'1601','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(0).Text + "','" + quan.ToString + "','" + Double.Parse(ListView1.Items(i).SubItems(2).Text).ToString + "' ,'" + Double.Parse(ListView1.Items(i).SubItems(5).Text).ToString + "' ,0,'',0,'',0,'','KEY','','',0,'','','-1','','',0,'','',0,0,'','','',0,0,0,0,'',0,'" + rr + "',1,-1,1,0,0,sysdate,sysdate,0,'" + Double.Parse(ListView1.Items(i).SubItems(2).Text).ToString + "')"
                                cmd4.Connection = conn

                                conn.Open()
                                cmd4.ExecuteNonQuery()
                                conn.Close()
                                ' MessageBox.Show("done")
                            End If
                            If ListView1.Items(i).SubItems(3).Text.ToString = "-1" Then
                                cmd5.CommandText = "delete  from TR_LTM_SLS_RTN_RECEIPT where lu_prc_adj_rfn_id = '" + exchanged_receipt + "'  and id_itm='" + ListView1.Items(i).SubItems(0).Text + "' and AI_TRN = '" + ListView1.Items(i).SubItems(4).Text + "' "
                                cmd5.Connection = conn
                                conn.Open()

                                cmd5.ExecuteNonQuery()
                                conn.Close()
                                'MessageBox.Show("remove")
                            End If


                        Next

                        '''''''''''''''''''generate barcode
                        Try
                            PictureBox12.Image = Nothing

                            Dim year1 As String = Date.Today.Year.ToString
                            Dim day1 As String = Date.Today.Day.ToString
                            Dim month1 As String = Date.Today.Month.ToString
                            Dim hour1 As String = Date.Now.Hour.ToString
                            Dim minute1 As String = Date.Now.Minute.ToString
                            Dim Second As String = Date.Now.Second.ToString

                            Dim rr1 As String = year + month + day + hour + minute + Second


                            'ID Automation
                            'Free only with the Code39 and Code39Ext

                            '-----------------------------------------------------print receipt








                            '---------------
                            ' Create a linear barcode image object (BarcodeLib.Barcode.Linear)

                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try

                        If receipt_lang = "1" Then


                            '-----------------------------------------------------print receipt
                            Dim file As System.IO.StreamWriter
                            file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\" + rr + ".txt", True)

                            file.WriteLine("    " & company_name)
                            file.WriteLine("    " & store_name)

                            file.WriteLine("------------------")
                            file.WriteLine("    " & address)
                            file.WriteLine("    " & telephone)
                            file.WriteLine("------------------")
                            file.WriteLine("    EXCHANGE")
                            'file.WriteLine("    تــبديل")
                            file.WriteLine("------------------")
                            file.WriteLine("    " & System.DateTime.Now)
                            file.WriteLine("------------------")
                            file.WriteLine("    " & rr.ToString)
                            file.WriteLine("    ORG:" & org)
                            file.WriteLine("------------------")
                            file.WriteLine("    VAT: ضَريبةِ القيمةِ المُضافةِ:")
                            file.WriteLine("    " & vat_number)
                            file.WriteLine("------------------")
                            file.WriteLine("    " & "فاتورة ضريبة مبسطة ")
                            file.WriteLine("    Simplified Tax Invoice")
                            file.WriteLine("------------------")
                            file.WriteLine("                  ")
                            For i = 0 To ListView1.Items.Count - 1
                                file.WriteLine("    " & (i + 1).ToString & "- " & ListView1.Items(i).SubItems(0).Text & "      " & ListView1.Items(i).SubItems(2).Text & " (" & ListView1.Items(i).SubItems(5).Text & ")" & " (" & ListView1.Items(i).SubItems(3).Text & ")")
                                file.WriteLine("    " & ListView1.Items(i).SubItems(1).Text)
                                'file.WriteLine("                  ")
                            Next
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine()

                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine()
                            file.WriteLine("-----------------------")


                            file.WriteLine("    Subtotal   " & subtotal)
                            file.WriteLine("    Discount   " & ListView2.Items(0).SubItems(1).Text)
                            'file.WriteLine("    VAT%    " & ListView2.Items(0).SubItems(2).Text)
                            file.WriteLine("    Total   " & ListView2.Items(0).SubItems(4).Text)


                            file.WriteLine("    Credit Card    " & TextBox2.Text)

                            file.WriteLine("    CHANGE  " & (Convert.ToDecimal(TextBox2.Text) - Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text)).ToString)
                            'file.WriteLine()

                            file.WriteLine("==============================")
                            file.WriteLine("    VAT Summary | ملخص الضرائب")
                            file.WriteLine("    % VAT Rates: " & vat & " :% معدل الضريبة")
                            file.WriteLine("    VAT: " & (Double.Parse(ListView2.Items(0).SubItems(2).Text) & " :الضرائب"))
                            file.WriteLine("    Inc VAT: " & Double.Parse(ListView2.Items(0).SubItems(4).Text) & " :متضمن للضريبة")
                            file.WriteLine("==============================")


                            file.WriteLine("    " & policy)
                            'file.WriteLine("    Thank You For Shopping")


                            file.WriteLine("                        ")


                            file.Close()
                            vat_value = (Double.Parse(ListView2.Items(0).SubItems(2).Text))
                            receipt_value = Double.Parse(ListView2.Items(0).SubItems(4).Text).ToString

                            ' Dim proc As New Process


                            ' proc.StartInfo.FileName = "C:\POS\Receipts\" + rr + ".txt"
                            '
                            '
                            ' proc.StartInfo.Verb = "Print"
                            '
                            '
                            ' proc.StartInfo.CreateNoWindow = True
                            '
                            If printer_status = "1" Then

                                Try
                                    AddHandler PrintZatcaQR.PrintPage, AddressOf Me.PrintImage
                                    PrintZatcaQR.Print()
                                Catch ex As Exception
                                    MsgBox(ex, MsgBoxStyle.Critical, "Error during Print")
                                End Try

                                'proc.Start()

                                'proc.WaitForExit()
                                'PrintDocument1.Print()
                            End If
                        End If
                        If receipt_lang = "2" Then


                            '-----------------------------------------------------print receipt
                            Dim file As System.IO.StreamWriter
                            file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\" + rr + ".txt", True)

                            file.WriteLine("    " & company_name)
                            file.WriteLine("    " & store_name)

                            file.WriteLine("------------------")
                            file.WriteLine("    " & address)
                            file.WriteLine("    " & telephone)
                            file.WriteLine("------------------")
                            file.WriteLine("    EXCHANGE")
                            file.WriteLine("    تــبديل")
                            file.WriteLine("------------------")
                            file.WriteLine("    " & System.DateTime.Now)
                            file.WriteLine("------------------")
                            file.WriteLine("    " & rr.ToString)
                            file.WriteLine("    ORG:" & org)
                            file.WriteLine("------------------")
                            file.WriteLine("    VAT: ضَريبةِ القيمةِ المُضافةِ:")
                            file.WriteLine("    " & vat_number)
                            file.WriteLine("------------------")
                            file.WriteLine("    " & "فاتورة ضريبة مبسطة")
                            file.WriteLine("    Simplified Tax Invoice")
                            file.WriteLine("------------------")
                            file.WriteLine("                  ")
                            For i = 0 To ListView1.Items.Count - 1
                                file.WriteLine("    " & (i + 1).ToString & "- " & ListView1.Items(i).SubItems(0).Text & "      " & ListView1.Items(i).SubItems(2).Text & " (" & ListView1.Items(i).SubItems(5).Text & ")" & " (" & ListView1.Items(i).SubItems(3).Text & ")")
                                file.WriteLine("    " & ListView1.Items(i).SubItems(1).Text)
                                'file.WriteLine("                  ")
                            Next
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine()

                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine()
                            file.WriteLine("-----------------------")


                            file.WriteLine("    Subtotal   " & subtotal & "     المجموع الفرعي")
                            file.WriteLine("    Discount   " & ListView2.Items(0).SubItems(1).Text & "     مجموع التخفيضات")
                            'file.WriteLine("    VAT%    " & ListView2.Items(0).SubItems(2).Text & "     ملخص الضريبة")
                            file.WriteLine("    Total   " & ListView2.Items(0).SubItems(4).Text & "     المجموع الكلي")


                            file.WriteLine("    Credit Card    " & TextBox2.Text & "       بطاقة إئتمانية")

                            file.WriteLine("    CHANGE  " & (Convert.ToDecimal(TextBox2.Text) - Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text)).ToString & "        المتبقي")
                            'file.WriteLine()



                            file.WriteLine("==============================")
                            file.WriteLine("    VAT Summary | ملخص الضرائب")
                            file.WriteLine("    % VAT Rates: " & vat & " :% معدل الضريبة")
                            file.WriteLine("    VAT: " & (Double.Parse(ListView2.Items(0).SubItems(2).Text) & " :الضرائب"))
                            file.WriteLine("    Inc VAT: " & Double.Parse(ListView2.Items(0).SubItems(4).Text) & " :متضمن للضريبة")
                            file.WriteLine("==============================")

                            file.WriteLine("    " & policy)
                            'file.WriteLine("    Thank You For Shopping")


                            file.WriteLine("                        ")

                            file.Close()
                            vat_value = (Double.Parse(ListView2.Items(0).SubItems(2).Text))
                            receipt_value = Double.Parse(ListView2.Items(0).SubItems(4).Text).ToString
                            ' Dim proc As New Process
                            '
                            '
                            ' proc.StartInfo.FileName = "C:\POS\Receipts\" + rr + ".txt"
                            '
                            '
                            ' proc.StartInfo.Verb = "Print"
                            '
                            '
                            ' proc.StartInfo.CreateNoWindow = True

                            If printer_status = "1" Then
                                Try
                                    AddHandler PrintZatcaQR.PrintPage, AddressOf Me.PrintImage
                                    PrintZatcaQR.Print()
                                Catch ex As Exception
                                    MsgBox(ex, MsgBoxStyle.Critical, "Error during Print")
                                End Try


                                ' proc.Start()

                                'proc.WaitForExit()
                                'PrintDocument1.Print()
                            End If
                        End If

                        ListView1.Items.Clear()
                        TextBox7.Clear()
                        TextBox7.Visible = False

                        TextBox9.Visible = False
                        TextBox1.Visible = True
                        ComboBox4.Visible = True
                        TextBox1.Focus()
                        ListView2.Items(0).SubItems(0).Text = "0"
                        ListView2.Items(0).SubItems(1).Text = "0"
                        ListView2.Items(0).SubItems(2).Text = "0"
                        ListView2.Items(0).SubItems(3).Text = "0"
                        ListView2.Items(0).SubItems(4).Text = "0"
                        TextBox2.Clear()
                        If com_status = 1 Then
                            Dim sp = New SerialPort(com_port, 9600, Parity.None, 8, StopBits.One)
                            sp.Open()
                            sp.Write(Convert.ToString(Chr(12)))
                            sp.Write("Thanks for shopping")
                            sp.Close()
                        End If


                        Label5.Text = "F4-E.Receipt"
                        Label5.Location = New Point(650, 420)
                        Label5.Visible = False
                        Label53.Visible = False

                        Label9.Visible = True
                        Label52.Visible = True
                        Label16.Visible = True
                        Label24.Visible = True
                        Label25.Visible = True
                        Label31.Visible = True
                        Label54.Visible = False

                        PictureBox4.Enabled = True

                        Label9.Text = "F5-R.Receipt"
                        Label52.Visible = True
                        PictureBox6.Enabled = True

                        exchange_flag = 0
                        PictureBox2.Enabled = True
                        PictureBox3.Enabled = True
                        PictureBox5.Enabled = True
                        PictureBox6.Enabled = True
                        PictureBox10.Enabled = True
                        Exit Sub

                    End If

                    'MessageBox.Show("Exchanged amount is less than the receipt..")
                    'MsgBox("Exchanged amount is less than the receipt..", MsgBoxStyle.Information, "ERROR")


                    '--------------------------------------------------------------------
                    If Label8.Text = "F4-Credit" And exchange_flag = 0 Then















                        ' --------------------------------------
                        rn = RN_ID()
                        Dim year As String
                        Dim day As String
                        Dim month As String
                        Dim hour As String
                        Dim minute As String


                        year = Date.Today.Year.ToString
                        day = Date.Today.Day.ToString
                        month = Date.Today.Month.ToString
                        hour = Date.Now.Hour.ToString
                        minute = Date.Now.Minute.ToString


                        rr = year + month + day + u_id + ws_id + rn.ToString
                        rr_barcode = rr

                        Dim conn As New OracleConnection
                        Dim cmd As New OracleCommand
                        Dim cmd1 As New OracleCommand
                        Dim cmd2_1 As New OracleCommand
                        Dim cmd2_2 As New OracleCommand
                        Dim cmd2_3 As New OracleCommand
                        Dim cmd3 As New OracleCommand
                        Dim cmd4 As New OracleCommand
                        Dim cmd_date As New OracleCommand
                        Dim b_date As String = ""


                        Threading.Thread.Sleep(2000) ' 500 milliseconds = 0.5 seconds
                        If conn.State = ConnectionState.Open Then
                            conn.Close()
                        End If
                        conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                        cmd_date.CommandText = "select business_date from daily_ops"
                        cmd_date.Connection = conn

                        conn.Open()
                        Dim rd_date As OracleDataReader = cmd_date.ExecuteReader
                        If rd_date.Read Then
                            b_date = rd_date.GetValue(0)

                        End If
                        rd_date.Close()
                        conn.Close()

                        cmd.CommandText = " insert into receipt_numbers values(TO_CHAR(SYSDATE, 'dd-mm-yy')," + rn + "," + rr + ",'" + ListView2.Items(0).SubItems(3).Text + "','" + ListView2.Items(0).SubItems(4).Text + "','" + ListView2.Items(0).SubItems(2).Text + "','" + user_id + "','CRDT','sale',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh:mm:ss'),'" + b_date + "','" + TextBox7.Text.Trim.ToString + "') "
                        cmd.Connection = conn

                        conn.Open()
                        cmd.ExecuteNonQuery()
                        conn.Close()
                        '-------------------------
                        cmd.CommandText = " insert into receipt_numbers_WS values(TO_CHAR(SYSDATE, 'dd-mm-yy')," + rn + "," + rr + ",'" + ListView2.Items(0).SubItems(3).Text + "','" + ListView2.Items(0).SubItems(4).Text + "','" + ListView2.Items(0).SubItems(2).Text + "','" + user_id + "','CRDT','sale',TO_CHAR(SYSDATE, 'dd-mm-yyyy hh:mm:ss'),'" + b_date + "','" + TextBox7.Text.Trim.ToString + "'," + ws_id + ") "
                        cmd.Connection = conn

                        conn.Open()
                        cmd.ExecuteNonQuery()
                        conn.Close()
                        TextBox7.Clear()
                        Label3.Visible = True
                        Label2.Text = ""
                        pos_flag = 1

                        '------------------------------
                        Dim cmd_DSF As New OracleCommand
                        For i = 0 To ListView1.Items.Count - 1



                            cmd_DSF.CommandText = "insert into DSF values('" + u_id + "','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(1).Text + "','" + ListView1.Items(i).SubItems(2).Text + "','" + b_date + "','" + System.DateTime.Now + "','Exchange','" + ListView1.Items(i).SubItems(2).Text + "','" + user_id + "')"
                            cmd_DSF.Connection = conn

                            conn.Open()
                            cmd_DSF.ExecuteNonQuery()
                            conn.Close()

                            Dim path As String = "C:\POS\OUT"
                            If Directory.Exists(path) Then
                                '------------------------------------------Print open report
                                Dim file_DSF As System.IO.StreamWriter
                                file_DSF = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\OUT\DSF_" + b_date + ".txt", True)

                                file_DSF.WriteLine(u_id & "|" & ListView1.Items(i).SubItems(0).Text & "|" & ListView1.Items(i).SubItems(1).Text & "|" & ListView1.Items(i).SubItems(2).Text & "|" & b_date & "|" & System.DateTime.Now & "|" & "Sale" & "|" & ListView1.Items(i).SubItems(2).Text & "|" & user_id)


                                file_DSF.Close()
                            End If
                        Next
                        '------------------------------------------

                        For i = 0 To ListView1.Items.Count - 1
                            AI_TRN = rn & i.ToString


                            cmd1.CommandText = "insert into tr_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",'admin',1,'',sysdate,sysdate,0,0,2,'','',0,'','','-1','-1','-1',0,0,sysdate,sysdate,0)"
                            cmd1.Connection = conn

                            conn.Open()
                            cmd1.ExecuteNonQuery()
                            conn.Close()


                            cmd2_1.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'SR','','',0,-1,sysdate,sysdate)"
                            cmd2_1.Connection = conn

                            conn.Open()
                            cmd2_1.ExecuteNonQuery()
                            conn.Close()

                            cmd2_2.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",1,'TX','','',0,-1,sysdate,sysdate)"
                            cmd2_2.Connection = conn

                            conn.Open()
                            cmd2_2.ExecuteNonQuery()
                            conn.Close()

                            cmd2_3.CommandText = "insert into tr_ltm_rtl_trn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",2,'TN','','',0,-1,sysdate,sysdate)"
                            cmd2_3.Connection = conn

                            conn.Open()
                            cmd2_3.ExecuteNonQuery()
                            conn.Close()

                            Dim quan As Integer = ListView1.Items(i).SubItems(3).Text
                            cmd3.CommandText = "insert into tr_ltm_sls_rtn values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'',0,'1601','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(0).Text + "','" + quan.ToString + "','" + (Double.Parse(ListView1.Items(i).SubItems(2).Text) / CF).ToString + "','" + (Double.Parse(ListView1.Items(i).SubItems(5).Text) / CF).ToString + "',0,'',0,'',0,'','KEY','','',0,'','','-1','','',0,'','',0,0,'','','',0,0,0,0,'',0,'" + rr + "',1,-1,1,0,0,sysdate,sysdate,0,'" + (Double.Parse(ListView1.Items(i).SubItems(2).Text) / CF).ToString + "')"
                            cmd3.Connection = conn

                            conn.Open()
                            cmd3.ExecuteNonQuery()
                            conn.Close()

                            cmd4.CommandText = "insert into TR_LTM_SLS_RTN_RECEIPT values('" + u_id + "','" + ws_id + "',TO_CHAR(SYSDATE, 'yyyy-mm-dd')," + AI_TRN + ",0,'',0,'1601','" + ListView1.Items(i).SubItems(0).Text + "','" + ListView1.Items(i).SubItems(0).Text + "','" + quan.ToString + "','" + Double.Parse(ListView1.Items(i).SubItems(2).Text).ToString + "','" + Double.Parse(ListView1.Items(i).SubItems(5).Text).ToString + "',0,'',0,'',0,'','KEY','','',0,'','','-1','','',0,'','',0,0,'','','',0,0,0,0,'',0,'" + rr + "',1,-1,1,0,0,sysdate,sysdate,0,'" + Double.Parse(ListView1.Items(i).SubItems(2).Text).ToString + "')"
                            cmd4.Connection = conn

                            conn.Open()
                            cmd4.ExecuteNonQuery()
                            conn.Close()


                        Next


                        'Threading.Thread.Sleep(2000)
                        ' Label1.Text = "Close cash drawer..."
                        '''''''''''''''''''generate barcode
                        Try
                            PictureBox12.Image = Nothing

                            Dim year1 As String = Date.Today.Year.ToString
                            Dim day1 As String = Date.Today.Day.ToString
                            Dim month1 As String = Date.Today.Month.ToString
                            Dim hour1 As String = Date.Now.Hour.ToString
                            Dim minute1 As String = Date.Now.Minute.ToString
                            Dim Second As String = Date.Now.Second.ToString

                            Dim rr1 As String = year + month + day + hour + minute + Second


                            'ID Automation
                            'Free only with the Code39 and Code39Ext

                            '-----------------------------------------------------print receipt








                            '---------------
                            ' Create a linear barcode image object (BarcodeLib.Barcode.Linear)

                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try

                        If receipt_lang = "1" Then


                            '-----------------------------------------------------print receipt
                            Dim file As System.IO.StreamWriter
                            file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\" + rr + ".txt", True)




                            file.WriteLine("    " & company_name)
                            file.WriteLine("    " & store_name)

                            file.WriteLine("------------------")
                            file.WriteLine("    " & address)
                            file.WriteLine("    " & telephone)
                            file.WriteLine("------------------")
                            file.WriteLine("    SALE")
                            file.WriteLine("    بيع")
                            file.WriteLine("------------------")
                            file.WriteLine("    " & System.DateTime.Now)
                            file.WriteLine("------------------")
                            file.WriteLine("    " & rr.ToString)
                            file.WriteLine("------------------")
                            file.WriteLine("    VAT: ضَريبةِ القيمةِ المُضافةِ:")
                            file.WriteLine("    " & vat_number)
                            file.WriteLine("------------------")
                            file.WriteLine("    فاتورة ضريبة مبسطة")
                            file.WriteLine("    Simplified Tax Invoice")
                            file.WriteLine("------------------")
                            file.WriteLine("                  ")
                            For i = 0 To ListView1.Items.Count - 1
                                file.WriteLine("    " & (i + 1).ToString & "- " & ListView1.Items(i).SubItems(0).Text & "      " & ListView1.Items(i).SubItems(2).Text & " (" & ListView1.Items(i).SubItems(5).Text & ")" & " (" & ListView1.Items(i).SubItems(3).Text & ")")
                                file.WriteLine("    " & ListView1.Items(i).SubItems(1).Text)
                                'file.WriteLine("                  ")
                            Next
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine()

                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine()
                            file.WriteLine("-----------------------")


                            file.WriteLine("    Subtotal   " & subtotal)
                            file.WriteLine("    Discount   " & ListView2.Items(0).SubItems(1).Text)
                            'file.WriteLine("    VAT%    " & ListView2.Items(0).SubItems(2).Text)
                            file.WriteLine("    Total   " & ListView2.Items(0).SubItems(4).Text)


                            file.WriteLine("    Credit Card    " & TextBox2.Text)

                            file.WriteLine("    CHANGE  " & (Convert.ToDecimal(TextBox2.Text) - Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text)).ToString)
                            file.WriteLine("==============================")
                            file.WriteLine("    VAT Summary | ملخص الضرائب")
                            file.WriteLine("    % VAT Rates: " & vat & " :% معدل الضريبة")
                            file.WriteLine("    VAT: " & (Double.Parse(ListView2.Items(0).SubItems(2).Text) & " :الضرائب"))
                            file.WriteLine("    Inc VAT: " & Double.Parse(ListView2.Items(0).SubItems(4).Text) & " :متضمن للضريبة")
                            file.WriteLine("==============================")


                            file.WriteLine("    " & policy)
                            'file.WriteLine("    Thank You For Shopping")
                            file.WriteLine("                        ")



                            file.Close()
                            vat_value = (Double.Parse(ListView2.Items(0).SubItems(2).Text))
                            receipt_value = Double.Parse(ListView2.Items(0).SubItems(4).Text).ToString

                            ' Dim proc As New Process
                            '
                            '
                            ' proc.StartInfo.FileName = "C:\POS\Receipts\" + rr + ".txt"
                            '
                            '
                            ' proc.StartInfo.Verb = "Print"
                            '
                            '
                            ' proc.StartInfo.CreateNoWindow = True


                            If printer_status = "1" Then
                                Try
                                    AddHandler PrintZatcaQR.PrintPage, AddressOf Me.PrintImage
                                    PrintZatcaQR.Print()
                                Catch ex As Exception
                                    MsgBox(ex, MsgBoxStyle.Critical, "Error during Print")
                                End Try


                                'proc.Start()

                                'proc.WaitForExit()
                                'PrintDocument1.Print()

                            End If
                        End If

                        If receipt_lang = "2" Then


                            '-----------------------------------------------------print receipt
                            Dim file As System.IO.StreamWriter
                            file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\" + rr + ".txt", True)




                            file.WriteLine("    " & company_name)
                            file.WriteLine("    " & store_name)

                            file.WriteLine("------------------")
                            file.WriteLine("    " & address)
                            file.WriteLine("    " & telephone)
                            file.WriteLine("------------------")
                            file.WriteLine("    SALE")
                            file.WriteLine("    بيع")
                            file.WriteLine("------------------")
                            file.WriteLine("    " & System.DateTime.Now)
                            file.WriteLine("------------------")
                            file.WriteLine("    " & rr.ToString)
                            file.WriteLine("------------------")
                            file.WriteLine("    VAT: ضَريبةِ القيمةِ المُضافةِ:")
                            file.WriteLine("    " & vat_number)
                            file.WriteLine("------------------")
                            file.WriteLine("    فاتورة ضريبة مبسطة")
                            file.WriteLine("    Simplified Tax Invoice")
                            file.WriteLine("------------------")
                            file.WriteLine("                  ")
                            For i = 0 To ListView1.Items.Count - 1
                                file.WriteLine("    " & (i + 1).ToString & "- " & ListView1.Items(i).SubItems(0).Text & "      " & ListView1.Items(i).SubItems(2).Text & " (" & ListView1.Items(i).SubItems(5).Text & ")" & " (" & ListView1.Items(i).SubItems(3).Text & ")")
                                file.WriteLine("    " & ListView1.Items(i).SubItems(1).Text)
                                'file.WriteLine("                  ")
                            Next
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine()

                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine()
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine("")
                            'file.WriteLine()
                            file.WriteLine("-----------------------")


                            file.WriteLine("    Subtotal   " & subtotal & "     المجموع الفرعي")
                            file.WriteLine("    Discount   " & ListView2.Items(0).SubItems(1).Text & "     مجموع التخفيضات")
                            'file.WriteLine("    VAT%    " & ListView2.Items(0).SubItems(2).Text & "     ملخص الضريبة")
                            file.WriteLine("    Total   " & ListView2.Items(0).SubItems(4).Text & "     المجموع الكلي")


                            file.WriteLine("    Credit    " & TextBox2.Text & "       بطاقة إئتمانية")

                            file.WriteLine("    CHANGE  " & (Convert.ToDecimal(TextBox2.Text) - Convert.ToDecimal(ListView2.Items(0).SubItems(4).Text)).ToString & "        المتبقي")
                            file.WriteLine("==============================")
                            file.WriteLine("    VAT Summary | ملخص الضرائب")
                            file.WriteLine("    % VAT Rates: " & vat & " :% معدل الضريبة")
                            file.WriteLine("    VAT: " & (Double.Parse(ListView2.Items(0).SubItems(2).Text) & " :الضرائب"))
                            file.WriteLine("    Inc VAT: " & Double.Parse(ListView2.Items(0).SubItems(4).Text) & " :متضمن للضريبة")
                            file.WriteLine("==============================")


                            file.WriteLine("    " & policy)
                            'file.WriteLine("    Thank You For Shopping")
                            file.WriteLine("                        ")



                            file.Close()
                            vat_value = (Double.Parse(ListView2.Items(0).SubItems(2).Text))
                            receipt_value = Double.Parse(ListView2.Items(0).SubItems(4).Text).ToString
                            ' Dim proc As New Process
                            '
                            '
                            ' proc.StartInfo.FileName = "C:\POS\Receipts\" + rr + ".txt"
                            '
                            '
                            ' proc.StartInfo.Verb = "Print"
                            '
                            '
                            ' proc.StartInfo.CreateNoWindow = True

                            If printer_status = "1" Then
                                Try
                                    AddHandler PrintZatcaQR.PrintPage, AddressOf Me.PrintImage
                                    PrintZatcaQR.Print()
                                Catch ex As Exception
                                    MsgBox(ex, MsgBoxStyle.Critical, "Error during Print")
                                End Try

                                ' proc.Start()

                                ' proc.WaitForExit()
                                'PrintDocument1.Print()

                            End If
                        End If
                        Label1.Text = "Enter an item number."
                        TextBox2.Clear()
                        TextBox1.Focus()
                        Label2.Visible = False
                        Label8.Visible = False

                        TextBox2.Visible = False
                        TextBox1.Visible = True
                        ComboBox4.Visible = True
                        TextBox1.Enabled = True





                        Label7.Visible = False
                        Label4.Visible = False
                        Label3.Visible = True
                        Label6.Visible = False
                        Label16.Visible = True
                        Label24.Visible = True
                        Label25.Visible = True
                        Label31.Visible = True

                        PictureBox10.Enabled = True


                        Label6.Text = "F6-Tender"

                        TextBox1.Focus()

                        ListView1.Enabled = True
                        ListView1.Items.Clear()
                        ListView2.Items(0).SubItems(0).Text = "0"
                        ListView2.Items(0).SubItems(1).Text = "0"
                        ListView2.Items(0).SubItems(2).Text = "0"
                        ListView2.Items(0).SubItems(3).Text = "0"
                        ListView2.Items(0).SubItems(4).Text = "0"

                        If com_status = 1 Then
                            Dim sp = New SerialPort(com_port, 9600, Parity.None, 8, StopBits.One)
                            sp.Open()
                            sp.Write(Convert.ToString(Chr(12)))
                            sp.Write("Thanks for shopping")
                            sp.Close()
                        End If


                        TextBox7.Clear()
                        TextBox7.Visible = False

                        Label5.Text = "F4-E.Receipt"
                        Label5.Location = New Point(650, 420)
                        Label53.Visible = False


                        Label5.Visible = False
                        Label9.Visible = True
                        Label52.Visible = True
                        PictureBox4.Enabled = True
                        Label9.Text = "F5-R.Receipt"
                        PictureBox2.Enabled = True
                        PictureBox3.Enabled = True
                        PictureBox5.Enabled = True
                        PictureBox6.Enabled = True
                        PictureBox10.Enabled = True
                        Label54.Visible = False

                    End If

                    '----------------------------------------------------

                    If dbpool > 30 Then
                        autorestart = 1
                        Me.Close()
                    End If
                    TextBox1.Clear()


                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Escape Then
            e.SuppressKeyPress = True
            If Label3.Visible = True And Label3.Enabled = True Then
                Label3_Click(sender, e)
            End If

        End If
        If e.KeyCode = Keys.F12 Then
            e.SuppressKeyPress = True
            If Label7.Visible = True And Label7.Enabled = True Then
                Label7_Click(sender, e)
            End If

        End If
    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
        Try
            Dim txt As TextBox = CType(sender, TextBox)

            If Not Char.IsDigit(e.KeyChar) Then e.Handled = True

            If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace

            If e.KeyChar = "." And txt.Text.IndexOf(".") = -1 Then e.Handled = False 'allow single decimal point

            If e.KeyChar = "-" And txt.SelectionStart = 0 Then e.Handled = False 'allow negative number

            'If e.KeyChar = "." And txt.Text.IndexOf(".") = -1 Then e.Handled = False 'allow single decimal point
            'If e.KeyChar = "-" And txt.SelectionStart = 0 Then e.Handled = False 'allow negative number

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            If TextBox8.Text <> "" Then



                Try


                    Dim rn As String
                    Dim rr As String = TextBox8.Text
                    exchanged_receipt = TextBox8.Text
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"

                    cmd.CommandText = "select receipt_reference from receipt_numbers where receipt_reference = '" + rr + "' and ops_type <> 'Refund'"

                    cmd.Connection = conn

                    conn.Open()


                    Dim rd As OracleDataReader = cmd.ExecuteReader

                    If rd.Read Then

                        rn = rd.GetValue(0)

                        If rn <> "" Then
                            org = rr
                            TextBox9.Visible = True
                            TextBox9.Enabled = True
                            TextBox8.Visible = False
                            Label1.Text = "Scan Receipt's Item."
                            Label3.Visible = False
                            Label7.Visible = True
                            Label4.Visible = True




                            rr_refund = rr
                            ListBox2.Items.Clear()
                            ListBox3.Items.Clear()
                            PictureBox7.Enabled = True


                        End If
                        rd.Close()
                        rd.Dispose()


                    Else
                        'MessageBox.Show("Wrong receipt number..")
                        'MsgBox("Wrong receipt number.", MsgBoxStyle.Information, "ERROR")
                        Label17.Text = "ERROR8:Wrong receipt number."
                        Label17.Visible = True
                        Button1.Visible = True
                        TextBox8.Clear()
                        Label54.Visible = False
                        Label3.Visible = False
                        Label7.Visible = False
                        ListView1.Visible = False
                        Panel1.Visible = False
                        Panel3.Visible = False
                        ListView2.Visible = False
                        TextBox1.Visible = False
                        ComboBox4.Visible = False
                        TextBox2.Visible = False
                        TextBox3.Visible = False
                        TextBox4.Visible = False
                        TextBox5.Visible = False
                        TextBox6.Visible = False
                        TextBox7.Visible = False
                        TextBox8.Visible = False
                        TextBox9.Visible = False
                        Label1.Visible = False


                    End If


                    cmd2.CommandText = "select id_itm,ai_trn from TR_LTM_SLS_RTN_RECEIPT where lu_prc_adj_rfn_id = '" + rr + "' and mo_prn_prc <> 0 "
                    cmd2.Connection = conn

                    Dim rd3 As OracleDataReader = cmd2.ExecuteReader



                    While rd3.Read
                        ListBox2.Items.Add(rd3.GetValue(0))
                        ListBox3.Items.Add(rd3.GetValue(1))



                    End While
                    rd3.Close()
                    conn.Close()

                    PictureBox7.Enabled = True

                Catch ex As Exception
                    'MessageBox.Show(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged
        Try
            TextBox8.Text = TextBox8.Text.Trim
            TextBox9.Clear()
        Catch ex As Exception

        End Try


    End Sub

    Private Sub TextBox9_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyDown
        e.Handled = True

        If e.KeyCode = Keys.F12 Then
            e.Handled = True

            e.SuppressKeyPress = True
            If Label7.Visible = True And Label7.Enabled = True Then
                Label7_Click(sender, e)
            End If

        End If
        If e.KeyCode = Keys.F11 Then
            e.Handled = True
            e.SuppressKeyPress = True
            If Label4.Visible = True And Label4.Enabled = True Then
                Label4_Click(sender, e)
            End If

        End If
    End Sub

    Private Sub TextBox9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox9.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then


            If TextBox9.Text <> "" Then




                e.Handled = True



                Try

                    Dim conn As New OracleConnection
                    Dim cmd As New OracleCommand
                    Dim cmd1 As New OracleCommand
                    Dim ai_rn As New OracleCommand

                    Dim id As Double = get_id1()
                    item_barcode = TextBox9.Text
                    Dim x As Integer = 0
                    Dim y As Integer = 0

                    Dim ai_trn As String = ""






                    If ListBox2.Items.Count = 0 Then
                        'MessageBox.Show("No item found.")
                        ' MsgBox("No item found.", MsgBoxStyle.Information, "ERROR")
                        Label17.Text = "ERROR9:No item found."
                        Label17.Visible = True
                        Button1.Visible = True
                        ListView1.Visible = False
                        Panel1.Visible = False
                        Panel3.Visible = False
                        ListView2.Visible = False
                        TextBox1.Visible = False
                        ComboBox4.Visible = False
                        TextBox2.Visible = False
                        TextBox3.Visible = False
                        TextBox4.Visible = False
                        TextBox5.Visible = False
                        TextBox6.Visible = False
                        TextBox7.Visible = False
                        TextBox8.Visible = False
                        TextBox9.Visible = False
                        Label1.Visible = False
                        Label4.Visible = False
                        Label54.Visible = False
                        Label7.Visible = False
                        Label6.Visible = False


                        TextBox9.Clear()
                        Exit Sub

                    End If
                    Dim RFN As Integer = 0

                    For i = 0 To ListBox2.Items.Count - 1
                        If ListBox2.Items(i).ToString = item_barcode Then
                            RFN = RFN + 1

                        End If

                    Next

                    If RFN = 0 Then
                        Label17.Text = "ERROR9:No item found."
                        Label17.Visible = True
                        Button1.Visible = True
                        ListView1.Visible = False
                        Panel1.Visible = False
                        Panel3.Visible = False
                        ListView2.Visible = False
                        TextBox1.Visible = False
                        ComboBox4.Visible = False
                        TextBox2.Visible = False
                        TextBox3.Visible = False
                        TextBox4.Visible = False
                        TextBox5.Visible = False
                        TextBox6.Visible = False
                        TextBox7.Visible = False
                        TextBox8.Visible = False
                        TextBox9.Visible = False
                        Label1.Visible = False
                        Label4.Visible = False
                        Label54.Visible = False
                        Label6.Visible = False
                        Label7.Visible = False


                        TextBox9.Clear()


                        Exit Sub

                    Else


                        For i = 0 To ListBox3.Items.Count - 1
                            'Dim s As String = ListBox2.Items(i)

                            '' Split string based on spaces.
                            'Dim words As String() = s.Split(New Char() {" "c})

                            ' Use For Each loop over words and display them.
                            ' Dim word As String
                            'For Each word In words
                            'ai_trn = word
                            'Next


                            If ListBox2.Items(i).ToString = item_barcode Then


                                ai_rn.CommandText = "select ai_trn from TR_LTM_SLS_RTN_RECEIPT where id_itm =  '" + TextBox9.Text.Trim + "' and lu_prc_adj_rfn_id = '" + rr_refund + "'"
                                ai_rn.Connection = conn
                                cmd.Connection = conn
                                cmd.CommandText = "select de_itm from as_itm where id_itm = '" + item_barcode + "'"
                                cmd1.Connection = conn
                                cmd1.CommandText = "select mo_chn_prn_un_prc from TR_CHN_PRN_PRC where id_ev = :param1"
                                cmd1.Parameters.Add(":param1", OracleDbType.Double).Value = id
                                If conn.State = ConnectionState.Open Then
                                    conn.Close()
                                End If
                                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                                'conn.Open()
                                'Dim ai_rd As OracleDataReader = ai_rn.ExecuteReader
                                'If ai_rd.Read Then
                                'ai_trn = ai_rd.GetValue(0).ToString

                                'End If
                                'ai_rd.Close()
                                'conn.Close()
                                ai_trn = ListBox3.Items(i).ToString
                                Dim RTN_PRICE As New OracleCommand
                                RTN_PRICE.Connection = conn
                                RTN_PRICE.CommandText = "select mo_extn_ln_itm_rtn from TR_LTM_SLS_RTN_RECEIPT where id_itm = '" + item_barcode + "' and ai_trn =  '" + ListBox3.Items(i).ToString + "' and lu_prc_adj_rfn_id = '" + rr_refund + "'"




                                conn.Open()
                                Dim rd_rtn_price As OracleDataReader = RTN_PRICE.ExecuteReader
                                If rd_rtn_price.Read Then
                                    item_price = rd_rtn_price.GetValue(0) * -1

                                End If
                                rd_rtn_price.Close()

                                Dim rd As OracleDataReader = cmd.ExecuteReader
                                If rd.Read Then
                                    item_name = rd.GetValue(0).ToString





                                End If
                                rd.Close()

                                'Dim rd1 As OracleDataReader = cmd1.ExecuteReader
                                'If rd1.Read Then
                                'item_price = rd1.GetValue(0) * -1


                                'End If
                                'rd1.Close()
                                conn.Close()



                                With ListView1.Items.Add(item_barcode)
                                    .SubItems.Add(item_name)
                                    .SubItems.Add(item_price)
                                    .SubItems.Add(-1)
                                    .SubItems.Add(ai_trn)
                                    .SubItems.Add("0")
                                    .SubItems.Add("")
                                End With

                                itm_id = TextBox9.Text
                                itm_desc = item_name
                                itm_prc = item_price




                                QTY_SUM = 0
                                TOT_SUM = 0







                                Label6.Visible = True

                                TextBox9.Focus()

                                Dim TotalSum As Double = 0
                                Dim totalqty As Double = 0
                                Dim totaldis As Double = 0

                                Dim TempNode As ListViewItem
                                For Each TempNode In ListView1.Items
                                    TotalSum += CDbl(TempNode.SubItems.Item(2).Text)
                                    totalqty += CDbl(TempNode.SubItems.Item(3).Text)
                                    totaldis += CDbl(TempNode.SubItems.Item(5).Text)
                                Next

                                ListView2.Items(0).SubItems(0).Text = Math.Round(TotalSum, 2)
                                'ListView2.Items(0).SubItems(1).Text = Total_Discount.ToString
                                If TotalSum > Convert.ToDecimal(vat_limit) Or TotalSum * -1 > Convert.ToDecimal(vat_limit) Then
                                    ListView2.Items(0).SubItems(2).Text = Math.Round(vat * (TotalSum) / 100, 3)
                                Else
                                    ListView2.Items(0).SubItems(2).Text = "0"
                                End If
                                ListView2.Items(0).SubItems(1).Text = Math.Round(totaldis, 2)
                                ListView2.Items(0).SubItems(3).Text = totalqty
                                ListView2.Items(0).SubItems(4).Text = Math.Round(Double.Parse(ListView2.Items(0).SubItems(0).Text) + Double.Parse(ListView2.Items(0).SubItems(2).Text), 2)



                                itm_total = TotalSum

                                Label4.Visible = True



                                If com_status = 1 Then


                                    Dim sp = New SerialPort(com_port, 9600, Parity.None, 8, StopBits.One)

                                    sp.Open()

                                    ' to clear the display
                                    sp.Write(Convert.ToString(Chr(12)))
                                    'first line goes here
                                    sp.WriteLine(itm_id + "       " + item_price.ToString + Chr(13))
                                    '2nd line goes here
                                    sp.WriteLine("Total" + "       " + itm_total)
                                    sp.Dispose()
                                    sp.Close()
                                End If

                                If return_flag = 1 Then
                                    Label6.Visible = True
                                    PictureBox3.Enabled = True
                                    Label6.Text = "F6-Return"
                                    TextBox9.Focus()
                                    TextBox9.Clear()
                                End If


                                If exchange_flag = 1 Then
                                    Label6.Visible = True
                                    PictureBox3.Enabled = True
                                    Label6.Text = "F6-Tender"
                                    TextBox9.Focus()
                                    TextBox9.Clear()
                                End If


                                ListBox2.Items.RemoveAt(i)
                                ListBox3.Items.RemoveAt(i)
                                Exit Sub
                            Else
                                TextBox9.Focus()
                                TextBox9.Clear()

                            End If

                        Next


                        'MessageBox.Show("not found")



                    End If

                    'Label7.Enabled = True
                    'Label7.ForeColor = Color.White

                    'ListView1.ForeColor = Color.Black
                Catch ex As Exception
                    'MessageBox.Show(ex.Message)

                End Try
            End If
        End If

    End Sub





    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        Try


            TextBox9.Text = TextBox9.Text.Trim
            If ListView1.Items.Count = 0 Then
                'Label3.Visible = True
            Else
                ' Label3.Visible = False
            End If
            TextBox9.Focus()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub Button1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    End Sub

    Private Sub ListView2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView2.KeyDown

    End Sub

    Private Sub ListView2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ListView2.KeyPress

    End Sub

    Private Sub ListView2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView2.SelectedIndexChanged

    End Sub

    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click
        Try
            If Label5.Visible = True Then

                Label5_Click(sender, e)
            End If

            If Label52.Visible = True Then

                Label52_Click(sender, e)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try



        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label14.Click
        'MsgBox(position)

    End Sub

    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
        Try
            If Label9.Visible = True Then
                Label9_Click(sender, e)
            End If

            If Label53.Visible = True Then
                Label53_Click(sender, e)
            End If

            If Label60.Visible = True Then
                Label60_Click(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label16.Click
        Try
            Label16.Enabled = False

            Dim cmd1 As New OracleCommand
            Dim cmd2 As New OracleCommand
            Dim cmd3 As New OracleCommand
            Dim cmd4 As New OracleCommand
            Dim cmd5 As New OracleCommand
            Dim cmd6 As New OracleCommand
            Dim cmd7 As New OracleCommand
            Dim cmd8 As New OracleCommand
            Dim cmd9 As New OracleCommand
            Dim cmd10 As New OracleCommand
            Dim cmd11 As New OracleCommand
            Dim cmd12 As New OracleCommand


            Dim total_Sales As String = ""
            Dim Net_Sales As String = ""
            Dim total_refund As String = ""
            Dim total_exchage As String = ""
            Dim total_cash As String = ""
            Dim total_crdt As String = ""
            Dim VAT As String = ""


            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            cmd1.CommandText = "select business_date from daily_ops"
            cmd1.Connection = conn
            conn.Open()

            Dim rd As OracleDataReader = cmd1.ExecuteReader
            If rd.Read Then
                b_date = rd.GetValue(0)

            End If
            conn.Close()
            rd.Close()
            'total sales
            cmd2.CommandText = "select sum(total) from   receipt_numbers where ops_type = 'sale'  and  business_date ='" + b_date + "'  and    emp = '" + user_id + "'"
            cmd2.Connection = conn

            conn.Open()
            Dim rd_total_sales As OracleDataReader = cmd2.ExecuteReader
            If rd_total_sales.Read Then
                total_Sales = rd_total_sales.GetValue(0).ToString

            End If
            rd_total_sales.Close()
            conn.Close()

            cmd3.CommandText = "select sum(total) from   receipt_numbers where     business_date ='" + b_date + "' and    emp = '" + user_id + "'"
            cmd3.Connection = conn

            conn.Open()
            Dim rd_net_total As OracleDataReader = cmd3.ExecuteReader
            If rd_net_total.Read Then
                Net_Sales = rd_net_total.GetValue(0).ToString

            End If
            rd_net_total.Close()
            conn.Close()

            cmd4.CommandText = "select sum(total) from   receipt_numbers where ops_type = 'REFUND'  and  business_date ='" + b_date + "' and    emp = '" + user_id + "'"
            cmd4.Connection = conn

            conn.Open()
            Dim rd_total_refund As OracleDataReader = cmd4.ExecuteReader
            If rd_total_refund.Read Then
                total_refund = rd_total_refund.GetValue(0).ToString

            End If
            rd_total_refund.Close()
            conn.Close()




            '
            cmd5.CommandText = "select sum(total) from receipt_numbers where tender_type = 'CASH' and business_date ='" + b_date + "' and    emp = '" + user_id + "'"
            cmd5.Connection = conn
            conn.Open()
            Dim rd_total_cash As OracleDataReader = cmd5.ExecuteReader
            If rd_total_cash.Read Then
                total_cash = rd_total_cash.GetValue(0).ToString

            End If
            rd_total_cash.Close()
            conn.Close()

            '
            cmd6.CommandText = "select sum(total) from receipt_numbers where tender_type = 'CRDT' and business_date ='" + b_date + "' and    emp = '" + user_id + "'"
            cmd6.Connection = conn
            conn.Open()
            Dim rd_total_crdt As OracleDataReader = cmd6.ExecuteReader
            If rd_total_crdt.Read Then
                total_crdt = rd_total_crdt.GetValue(0).ToString

            End If
            rd_total_crdt.Close()
            conn.Close()
            ''''
            cmd7.CommandText = "select sum(total) from receipt_numbers where ops_type='EXCHANGE'  and  business_date ='" + b_date + "'and    emp = '" + user_id + "'"
            cmd7.Connection = conn

            conn.Open()
            Dim rd_total_exchange As OracleDataReader = cmd7.ExecuteReader
            If rd_total_exchange.Read Then
                total_exchage = rd_total_exchange.GetValue(0).ToString

            End If
            rd_total_exchange.Close()
            conn.Close()
            '''''
            cmd8.CommandText = "select sum(VAT) from receipt_numbers where   business_date ='" + b_date + "' and    emp = '" + user_id + "'"
            cmd8.Connection = conn

            conn.Open()
            Dim rd_VAT As OracleDataReader = cmd8.ExecuteReader
            If rd_VAT.Read Then
                VAT = rd_VAT.GetValue(0).ToString

            End If
            rd_VAT.Close()
            conn.Close()

            If receipt_lang = "1" Then


                '----------------------------------------print Summary report
                Dim file As System.IO.StreamWriter
                If My.Computer.FileSystem.FileExists("C:\POS\Receipts\SUMMARY_REPORT" + b_date + ".txt") Then
                    My.Computer.FileSystem.DeleteFile("C:\POS\Receipts\SUMMARY_REPORT" + b_date + ".txt")
                End If
                file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\SUMMARY_REPORT" + b_date + ".txt", True)
                file.WriteLine("                        ")
                file.WriteLine("                        ")
                file.WriteLine("                        ")
                file.WriteLine("                        ")
                file.WriteLine("    Summary Report")
                file.WriteLine("------------------")
                file.WriteLine("    " & user_id)
                file.WriteLine("    " & store_name)
                file.WriteLine("                   -------")
                file.WriteLine("--------------------------")
                file.WriteLine("    " & System.DateTime.Now)
                file.WriteLine("==========================")
                file.WriteLine("==========================")
                file.WriteLine("==========================")
                file.WriteLine("==========================")
                file.WriteLine("    " & "Total Sales: " & total_Sales)
                file.WriteLine("    " & "Net Sales :" & Net_Sales)
                file.WriteLine("    " & "Total Refund: " & total_refund)
                file.WriteLine("    " & "Total Exchage: " & total_exchage)
                file.WriteLine("    " & "Total Cash: " & total_cash)
                file.WriteLine("    " & "Total Credit: " & total_crdt)
                file.WriteLine("    " & "VAT%: " & VAT)
                file.WriteLine("==========================")
                file.WriteLine("==========================")
                file.WriteLine("        " & "***")
                file.WriteLine("    " & "END OF REPORT ------")
                file.Close()




                Dim proc As New Process


                proc.StartInfo.FileName = "C:\POS\Receipts\SUMMARY_REPORT" + b_date + ".txt"


                proc.StartInfo.Verb = "Print"


                proc.StartInfo.CreateNoWindow = True

                If printer_status = "1" Then


                    proc.Start()

                    proc.WaitForExit()
                End If

            End If

            If receipt_lang = "2" Then


                '----------------------------------------print Summary report
                Dim file As System.IO.StreamWriter
                If My.Computer.FileSystem.FileExists("C:\POS\Receipts\SUMMARY_REPORT" + b_date + ".txt") Then
                    My.Computer.FileSystem.DeleteFile("C:\POS\Receipts\SUMMARY_REPORT" + b_date + ".txt")
                End If
                file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\Receipts\SUMMARY_REPORT" + b_date + ".txt", True)
                file.WriteLine("                        ")
                file.WriteLine("                        ")
                file.WriteLine("                        ")
                file.WriteLine("                        ")
                file.WriteLine("    Summary Report")
                file.WriteLine("------------------")
                file.WriteLine("    " & user_id)
                file.WriteLine("    " & store_name)
                file.WriteLine("                   -------")
                file.WriteLine("--------------------------")
                file.WriteLine("    " & System.DateTime.Now)
                file.WriteLine("==========================")
                file.WriteLine("==========================")
                file.WriteLine("==========================")
                file.WriteLine("==========================")
                file.WriteLine("    " & "Total Sales: " & total_Sales & "    أجمالي المبيعات")
                file.WriteLine("    " & "Net Sales :" & Net_Sales & "   صافي المبيعات")
                file.WriteLine("    " & "Total Refund: " & total_refund & "    أجمالي المسترجع")
                file.WriteLine("    " & "Total Exchage: " & total_exchage & "   أجمالي الاستبدال")
                file.WriteLine("    " & "Total Cash: " & total_cash & "     أجمالي المبيعات النقدية")
                file.WriteLine("    " & "Total Credit: " & total_crdt & "   أجمالي المبيعات بطاقة إئتمانية")
                file.WriteLine("    " & "VAT%: " & VAT & "      أجمالي ضريبة القيمة المضافة")
                file.WriteLine("==========================")
                file.WriteLine("==========================")
                file.WriteLine("        " & "***")
                file.WriteLine("    " & "END OF REPORT ------")
                file.Close()




                Dim proc As New Process


                proc.StartInfo.FileName = "C:\POS\Receipts\SUMMARY_REPORT" + b_date + ".txt"


                proc.StartInfo.Verb = "Print"


                proc.StartInfo.CreateNoWindow = True

                If printer_status = "1" Then


                    proc.Start()

                    proc.WaitForExit()
                End If

            End If


            Label16.Enabled = True
            PictureBox10.Enabled = True
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub PictureBox10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox10.Click
        Try
            If Label16.Visible = True Then


                Label16_Click(sender, e)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub POS_Screen_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Form1.Timer1.Stop()
            Form1.Close()


            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                Dim cmd_auto_scanned_item As New OracleCommand
                cmd_auto_scanned_item.Connection = conn
                cmd_auto_scanned_item.CommandText = "select * from auto_logon_scanned"
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                Dim auto_scanned_rd As OracleDataReader = cmd_auto_scanned_item.ExecuteReader

                While auto_scanned_rd.Read
                    With ListView1.Items.Add(auto_scanned_rd.GetValue(0).ToString)
                        .SubItems.Add(auto_scanned_rd.GetValue(1).ToString)
                        .SubItems.Add(auto_scanned_rd.GetValue(2).ToString)
                        .SubItems.Add(auto_scanned_rd.GetValue(3).ToString)
                        .SubItems.Add(0)
                        .SubItems.Add(auto_scanned_rd.GetValue(4).ToString)
                        .SubItems.Add(0)


                    End With


                End While


                auto_scanned_rd.Close()
            conn.Close()
            If ListView1.Items.Count > 0 Then
                Dim TotalSum As Double = 0
                Dim totalqty As Double = 0
                Dim totaldis As Double = 0

                Dim TempNode As ListViewItem
                For Each TempNode In ListView1.Items
                    TotalSum += CDbl(TempNode.SubItems.Item(2).Text)
                    totalqty += CDbl(TempNode.SubItems.Item(3).Text)
                    totaldis += CDbl(TempNode.SubItems.Item(5).Text)


                Next


                ListView2.Items(0).SubItems(0).Text = Math.Round(TotalSum, 2)
                'ListView2.Items(0).SubItems(1).Text = Total_Discount.ToString
                If TotalSum > Convert.ToDecimal(vat_limit) Then
                    ListView2.Items(0).SubItems(2).Text = Math.Round(vat * (TotalSum) / 100, 3)
                Else
                    ListView2.Items(0).SubItems(2).Text = "0"
                End If
                ListView2.Items(0).SubItems(1).Text = Math.Round(totaldis, 2)
                ListView2.Items(0).SubItems(3).Text = totalqty
                ListView2.Items(0).SubItems(4).Text = Math.Round(Double.Parse(ListView2.Items(0).SubItems(0).Text) + Double.Parse(ListView2.Items(0).SubItems(2).Text), 2)




                PictureBox3.Enabled = True
                Label6.Visible = True

                TextBox1.Focus()
                Label7.Enabled = True
                PictureBox7.Enabled = True
                Label4.Enabled = True
                Label7.Visible = True
                Label4.Visible = True
                Label6.Enabled = True
                PictureBox3.Enabled = True
                Label6.Text = "F6-Tender"
                Label5.Visible = True
                Label53.Visible = True
                Label54.Visible = False
                Label9.Visible = False
                Label52.Visible = False
                PictureBox4.Enabled = True
                Label16.Visible = False
                Label24.Visible = False
                Label25.Visible = False
                Label31.Visible = False
                Label3.Visible = False
                Label54.Visible = False


                Dim cmd_scanned As New OracleCommand
                cmd_scanned.Connection = conn
                cmd_scanned.CommandText = "delete from auto_logon_scanned"
                conn.Open()
                cmd_scanned.ExecuteNonQuery()
                conn.Close()

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Label17.Text = "ERROR3:ITEM not found." Then
                Label17.Visible = False
                Button1.Visible = False
                If return_flag = 1 Then
                    If ListView1.Items.Count > 0 Then


                        Label6.Visible = True
                        Label4.Visible = True
                        Label3.Visible = True
                    Else
                        Label3.Visible = True
                    End If
                End If

                If exchange_flag = 1 Then
                    Label4.Visible = True
                    Label7.Visible = True
                    Label54.Visible = True

                    Dim RFN_ITM As Integer = 0

                    For i = 0 To ListView1.Items.Count - 1
                        If ListView1.Items(i).SubItems(2).Text < 0 Then
                            RFN_ITM = 1

                        End If
                    Next

                    If RFN_ITM = 1 Then
                        Label6.Visible = True
                    End If
                End If
                ListView1.Visible = True
                Panel1.Visible = True
                Panel3.Visible = True
                ListView2.Visible = True

                TextBox12.Visible = True
                TextBox12.Focus()
                Label1.Visible = True
            End If
            If Label17.Text = "ERROR2:ITEM not found." Then
                Label1.Visible = True
                Label16.Enabled = True
                Label3.Enabled = True
                'Label27.Visible = True
                'Label28.Visible = True
                'Label29.Visible = True
                'Label30.Visible = True
                TextBox10.Enabled = True
                Button1.Visible = False
                Label17.Visible = False

                Label16.Visible = True
                Label3.Visible = True

            End If
            If Label17.Text = "ERROR7:The amount of money is less than receipt total." Then
                TextBox2.Visible = True
                TextBox2.Focus()

                Label17.Visible = False
                Button1.Visible = False
                ListView1.Visible = True

                Panel1.Visible = True
                Panel3.Visible = True
                ListView2.Visible = True


                Label1.Visible = True
                Label2.Visible = True
                Label8.Visible = True
                Label3.Visible = True
                TextBox2.Focus()
                TextBox2.Text = ListView2.Items(0).SubItems(4).Text
                Label54.Visible = True
                Exit Sub


            End If
            If Label17.Text = "ERROR10:Exchanged amount is less than the receipt." Then

                Label17.Visible = False
                Button1.Visible = False
                ListView1.Visible = True
                Panel1.Visible = True
                Panel3.Visible = True
                ListView2.Visible = True
                If receipt_status = 1 Then
                    TextBox9.Visible = True
                    TextBox9.Focus()
                ElseIf receipt_status = 0 Then
                    TextBox12.Visible = True
                    TextBox12.Focus()
                End If

                Label54.Visible = True

                Label1.Visible = True
                Label6.Visible = True
                Label4.Visible = True
                Label7.Visible = True
                Label54.Visible = True
            End If
            If Label17.Text = "ERROR7:The amount of money is less than receipt total." Then
                TextBox2.Visible = True
                TextBox2.Clear()
                TextBox2.Focus()
                PictureBox2.Enabled = True
                PictureBox5.Enabled = True
                PictureBox8.Enabled = True
                Label17.Visible = False
                Button1.Visible = False
                ListView1.Visible = True
                Panel1.Visible = True
                Panel3.Visible = True
                ListView2.Visible = True

                Label1.Visible = True
                Label2.Visible = True
                Label8.Visible = True
                Label3.Visible = True
                TextBox2.Focus()

            End If
            '
            If Label17.Text = "ERROR:Wrong item number." Then
                TextBox12.Enabled = True
                TextBox12.Focus()
                If ListView1.Items.Count > 0 Then
                    PictureBox11.Enabled = True
                    PictureBox7.Enabled = True

                    PictureBox3.Enabled = True
                Else
                    PictureBox3.Enabled = True

                End If


                Label17.Visible = False
                Button1.Visible = False
                ListView1.Visible = True
                Panel1.Visible = True
                Panel3.Visible = True
                ListView2.Visible = True
                TextBox1.Enabled = True
                TextBox2.Enabled = True
                TextBox3.Enabled = True
                TextBox4.Enabled = True
                TextBox5.Enabled = True
                TextBox6.Enabled = True
                Label7.Visible = True
                TextBox7.Enabled = True
                TextBox8.Enabled = True
                TextBox9.Enabled = True
                Label1.Visible = True
                Label2.Enabled = True
                Label3.Enabled = True
                Label4.Enabled = True
                Label5.Enabled = True
                Label6.Enabled = True

                PictureBox3.Enabled = True
                Label7.Enabled = True
                Label8.Enabled = True
                Label9.Enabled = True
                Label16.Enabled = True

            End If
            If Label17.Text = "ERROR9:No item found." Then
                TextBox9.Visible = True
                TextBox9.Focus()

                If return_flag = 1 Then
                    If ListView1.Items.Count > 0 Then
                        Label7.Visible = True
                        Label6.Visible = True
                        Label4.Visible = True
                    Else
                        Label7.Visible = True
                    End If



                End If

                If exchange_flag = 1 Then
                    If ListView1.Items.Count > 0 Then
                        Label4.Visible = True
                        Label7.Visible = True
                        Label54.Visible = True

                        For i = 0 To ListView1.Items.Count - 1
                            If ListView1.Items(i).SubItems(2).Text < 0 Then
                                Label6.Visible = True
                                Exit For
                            End If
                        Next


                    Else
                        Label54.Visible = True
                    End If
                End If



                Label17.Visible = False
                Button1.Visible = False
                ListView1.Visible = True
                Panel1.Visible = True
                Panel3.Visible = True
                ListView2.Visible = True

                Label1.Visible = True


            End If
            If Label17.Text = "ERROR8:Wrong receipt number." Then
                TextBox8.Visible = True

                TextBox8.Focus()


                If ListView1.Items.Count > 0 Then
                    Label54.Visible = True
                ElseIf ListView1.Items.Count = 0 Then
                    Label3.Visible = True
                End If

                Label17.Visible = False
                Button1.Visible = False
                ListView1.Visible = True
                Panel1.Visible = True
                Panel3.Visible = True
                ListView2.Visible = True

                Label1.Visible = True




            End If


            Try
                If Label17.Text = "ERROR1:ITEM not found." Then
                    TextBox12.Enabled = True
                    TextBox1.Focus()
                    ComboBox4.Visible = True
                    If ListView1.Items.Count > 0 Then

                        Label6.Visible = True
                        Label4.Visible = True
                        Label7.Visible = True
                        Label53.Visible = True
                        Label5.Visible = True
                    ElseIf ListView1.Items.Count = 0 Then

                        Label24.Visible = True
                        Label25.Visible = True
                        Label31.Visible = True
                        Label9.Visible = True
                        Label52.Visible = True
                        Label16.Visible = True
                        Label3.Visible = True
                    End If

                    PictureBox2.Enabled = True
                    PictureBox5.Enabled = True
                    PictureBox6.Enabled = True
                    Label17.Visible = False
                    Button1.Visible = False
                    ListView1.Visible = True
                    Panel1.Visible = True
                    Panel3.Visible = True
                    ListView2.Visible = True

                    TextBox2.Enabled = True
                    TextBox3.Enabled = True
                    TextBox4.Enabled = True
                    TextBox5.Enabled = True
                    TextBox6.Enabled = True
                    TextBox7.Enabled = True
                    TextBox8.Enabled = True
                    TextBox9.Enabled = True
                    Label1.Visible = True
                    Label2.Enabled = True
                    Label3.Enabled = True
                    Label4.Enabled = True
                    Label5.Enabled = True
                    Label6.Enabled = True
                    PictureBox3.Enabled = True
                    Label7.Enabled = True
                    Label8.Enabled = True
                    Label9.Enabled = True
                    Label16.Enabled = True
                    Label24.Enabled = True
                    Label25.Enabled = True
                    Label31.Enabled = True
                    Label54.Enabled = True
                    Label53.Enabled = True
                    Label52.Enabled = True
                    TextBox12.Enabled = True
                    TextBox12.Focus()
                    TextBox1.Enabled = True
                    TextBox1.Focus()

                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try



            If Label17.Text = "ERROR4:User ID dosn't has access rights." Then
                TextBox1.Visible = True
                ComboBox4.Visible = True
                TextBox1.Clear()
                TextBox1.Focus()
                Label1.Visible = True
                If ListView1.Items.Count = 0 Then
                    Label24.Visible = True
                    Label25.Visible = True
                    Label31.Visible = True
                    Label9.Visible = True
                    Label52.Visible = True
                    Label16.Visible = True
                    Label3.Visible = True
                ElseIf ListView1.Items.Count > 0 Then
                    Label6.Visible = True
                    Label4.Visible = True
                    Label7.Visible = True
                    Label53.Visible = True
                    Label5.Visible = True
                End If


                Label17.Visible = False
                Button1.Visible = False
                ListView1.Visible = True
                Panel1.Visible = True
                Panel3.Visible = True
                ListView2.Visible = True


            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawImage(PictureBox12.Image, 0, 0)
    End Sub

    Private Sub Label24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label24.Click
        Try
            Panel1.Visible = False
            Panel3.Visible = False
            ListView1.Visible = False
            ListView2.Visible = False
            Label9.Visible = False
            Label52.Visible = False

            Label1.Text = "Daily Sales Report"
            TextBox1.Enabled = False
            ComboBox4.Visible = False
            Label24.Visible = False
            Label25.Visible = False
            Label31.Visible = False

            Label26.Visible = True
            Label27.Visible = True
            Label28.Visible = True
            Label29.Visible = True
            Label30.Visible = True


            Dim cmd1 As New OracleCommand
            Dim cmd2 As New OracleCommand
            Dim cmd3 As New OracleCommand
            Dim cmd4 As New OracleCommand
            Dim cmd5 As New OracleCommand
            Dim cmd6 As New OracleCommand
            Dim cmd7 As New OracleCommand
            Dim cmd8 As New OracleCommand
            Dim cmd9 As New OracleCommand
            Dim cmd10 As New OracleCommand
            Dim cmd11 As New OracleCommand
            Dim cmd12 As New OracleCommand


            Dim total_Sales As String = ""
            Dim Net_Sales As String = ""
            Dim total_refund As String = ""
            Dim total_exchage As String = ""
            Dim total_cash As String = ""
            Dim total_crdt As String = ""
            Dim VAT As String = ""


            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            cmd1.CommandText = "select business_date from daily_ops"
            cmd1.Connection = conn
            conn.Open()

            Dim rd As OracleDataReader = cmd1.ExecuteReader
            If rd.Read Then
                b_date = rd.GetValue(0)

            End If
            conn.Close()
            rd.Close()
            'total sales
            cmd2.CommandText = "select sum(total) from   receipt_numbers where ops_type = 'sale'  and  business_date ='" + b_date + "'  and    emp = '" + user_id + "'"
            cmd2.Connection = conn

            conn.Open()
            Dim rd_total_sales As OracleDataReader = cmd2.ExecuteReader
            If rd_total_sales.Read Then
                total_Sales = rd_total_sales.GetValue(0).ToString

                Label26.Text = "Total Sales : " & total_Sales


            End If
            rd_total_sales.Close()
            conn.Close()

            cmd3.CommandText = "select sum(total) from   receipt_numbers where     business_date ='" + b_date + "' and    emp = '" + user_id + "'"
            cmd3.Connection = conn

            conn.Open()
            Dim rd_net_total As OracleDataReader = cmd3.ExecuteReader
            If rd_net_total.Read Then
                Net_Sales = rd_net_total.GetValue(0).ToString
                Label27.Text = "Net Sales : " & Net_Sales


            End If
            rd_net_total.Close()
            conn.Close()

            cmd4.CommandText = "select sum(total) from   receipt_numbers where ops_type = 'REFUND'  and  business_date ='" + b_date + "' and    emp = '" + user_id + "'"
            cmd4.Connection = conn

            conn.Open()
            Dim rd_total_refund As OracleDataReader = cmd4.ExecuteReader
            If rd_total_refund.Read Then
                total_refund = rd_total_refund.GetValue(0).ToString
                Label28.Text = "Total Refund : " & total_refund


            End If
            rd_total_refund.Close()
            conn.Close()




            '
            cmd5.CommandText = "select sum(total) from receipt_numbers where tender_type = 'CASH' and business_date ='" + b_date + "' and    emp = '" + user_id + "'"
            cmd5.Connection = conn
            conn.Open()
            Dim rd_total_cash As OracleDataReader = cmd5.ExecuteReader
            If rd_total_cash.Read Then
                total_cash = rd_total_cash.GetValue(0).ToString
                Label29.Text = "Total Cash : " & total_cash

            End If
            rd_total_cash.Close()
            conn.Close()

            '
            cmd6.CommandText = "select sum(total) from receipt_numbers where tender_type = 'CRDT' and business_date ='" + b_date + "' and    emp = '" + user_id + "'"
            cmd6.Connection = conn
            conn.Open()
            Dim rd_total_crdt As OracleDataReader = cmd6.ExecuteReader
            If rd_total_crdt.Read Then
                total_crdt = rd_total_crdt.GetValue(0).ToString
                Label30.Text = "Total Credit Card : " & total_crdt


            End If
            rd_total_crdt.Close()
            conn.Close()
            ''''
            cmd7.CommandText = "select sum(total) from receipt_numbers where ops_type='EXCHANGE'  and  business_date ='" + b_date + "'and    emp = '" + user_id + "'"
            cmd7.Connection = conn

            conn.Open()
            Dim rd_total_exchange As OracleDataReader = cmd7.ExecuteReader
            If rd_total_exchange.Read Then
                total_exchage = rd_total_exchange.GetValue(0).ToString

            End If
            rd_total_exchange.Close()
            conn.Close()
            '''''
            cmd8.CommandText = "select sum(VAT) from receipt_numbers where   business_date ='" + b_date + "' and    emp = '" + user_id + "'"
            cmd8.Connection = conn

            conn.Open()
            Dim rd_VAT As OracleDataReader = cmd8.ExecuteReader
            If rd_VAT.Read Then
                VAT = rd_VAT.GetValue(0).ToString

            End If
            rd_VAT.Close()
            conn.Close()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Label25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label25.Click
        Try
            Panel1.Visible = False
            Panel3.Visible = False
            ListView1.Visible = False
            ListView2.Visible = False
            Label9.Visible = False
            Label52.Visible = False
            Label1.Text = "Enter an item number."
            TextBox1.Visible = False
            ComboBox4.Visible = False
            Label24.Visible = False
            Label25.Visible = False
            Label31.Visible = False
            TextBox10.Visible = True



            TextBox10.Focus()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox10.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True


            Try
                Label26.Visible = True
                Label27.Visible = True
                Label28.Visible = True
                Label29.Visible = True
                Label30.Visible = True
                Dim conn As New OracleConnection
                Dim cmd As New OracleCommand
                Dim cmd1 As New OracleCommand

                Dim id As Double

                'Dim conn As New OracleConnection
                Dim cmd_id As New OracleCommand
                ' Dim cmd1 As New OracleCommand
                ' Dim id As Double = get_id()
                item_barcode = TextBox10.Text.Trim
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"

                cmd_id.CommandText = "select id_ev from MA_ITM_PRN_PRC_ITM where id_itm='" + item_barcode + "'"
                cmd_id.Connection = conn

                conn.Open()

                Dim rd_id As OracleDataReader = cmd_id.ExecuteReader
                If rd_id.Read Then

                    id = Int32.Parse(rd_id.GetValue(0).ToString)

                End If
                rd_id.Close()



                cmd.Connection = conn
                cmd.CommandText = "select de_itm from as_itm where id_itm = '" + TextBox10.Text.Trim + "'"
                cmd1.Connection = conn
                cmd1.CommandText = "select mo_chn_prn_un_prc from TR_CHN_PRN_PRC where id_ev = :param1"
                cmd1.Parameters.Add(":param1", OracleDbType.Double).Value = id





                Dim rd As OracleDataReader = cmd.ExecuteReader
                If rd.Read Then
                    Label26.Text = "Item Name : " & rd.GetValue(0).ToString

                Else
                    TextBox10.Clear()
                    'MessageBox.Show("ITEM not found.")
                    'MsgBox("ITEM not found.", MsgBoxStyle.Information, "ERROR")
                    Label17.Text = "ERROR2:ITEM not found."
                    TextBox10.Enabled = False
                    Label1.Visible = False
                    Label16.Visible = False
                    Label3.Visible = False
                    Label26.Visible = False

                    Label27.Visible = False
                    Label28.Visible = False
                    Label29.Visible = False
                    Label30.Visible = False
                    Button1.Visible = True
                    Label17.Visible = True


                    Button1.Focus()









                    Exit Sub
                End If
                rd.Close()

                Dim rd1 As OracleDataReader = cmd1.ExecuteReader
                If rd1.Read Then
                    Label27.Text = "Original Price : " & rd1.GetValue(0).ToString


                End If
                rd1.Close()





                '------------------------Stock
                Label29.Text = "Stock : "

                Dim cmd_stock As New OracleCommand
                cmd_stock.CommandText = "select sum(quan) from stock where I_B = '" + TextBox10.Text + "'"
                cmd_stock.Connection = conn

                Dim rd_stock As OracleDataReader = cmd_stock.ExecuteReader
                Dim remain_stock As Decimal = 0
                If rd_stock.Read Then
                    Label29.Text = Label29.Text & rd_stock.GetValue(0)
                    remain_stock = rd_stock.GetValue(0)
                End If

                rd_stock.Close()
                rd_stock.Dispose()

                '-----------------------------------Sold item
                Label30.Text = "Sold/Remain Quantity : "


                Dim cmd_sold As New OracleCommand
                cmd_sold.CommandText = "select sum(qu_itm_lm_rtn_sls) from tr_ltm_sls_rtn t where id_itm = '" + TextBox10.Text + "'"
                cmd_sold.Connection = conn
                Dim sold_quan As Decimal = 0
                Dim rd_sold As OracleDataReader = cmd_sold.ExecuteReader

                If rd_sold.Read Then
                    sold_quan = rd_sold.GetValue(0)

                    Label30.Text = Label30.Text & rd_sold.GetValue(0) & "/" & (remain_stock - sold_quan)

                End If

                rd_sold.Close()
                rd_sold.Dispose()



                conn.Close()










                TextBox10.Clear()
                TextBox10.Focus()

            Catch ex As Exception
                '
                MsgBox(ex.Message)
            End Try

        End If
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged

    End Sub

    Private Sub Label31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label31.Click
        Try
            Panel1.Visible = False
            Panel3.Visible = False
            ListView1.Visible = False
            ListView2.Visible = False
            Label9.Visible = False
            Label52.Visible = False
            Label45.Text = "Select Transfer Number"
            Label45.Visible = True
            TextBox1.Enabled = False
            Label24.Visible = False
            Label25.Visible = False
            Label31.Visible = False
            Label16.Visible = False
            Label3.Visible = False
            Label1.Visible = False
            Label54.Visible = False




            Panel2.Visible = True
            ListView3.Visible = True
            Button2.Visible = True
            ComboBox4.Visible = False
            ComboBox2.Visible = True
            Button5.Visible = True
            Button4.Visible = True
            Button3.Visible = True

            ComboBox2.Items.Clear()
            Button5.Enabled = False
            Button4.Enabled = False
            ListView3.Items.Clear()
            ComboBox2.Text = ""
            ComboBox2.Focus()






            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim cmd As New OracleCommand
            cmd.Connection = conn
            cmd.CommandText = "select transfer_number from transfer_manager"

            conn.Open()
            Dim rd As OracleDataReader = cmd.ExecuteReader
            While rd.Read
                ComboBox2.Items.Add(rd.GetValue(0))


            End While
            rd.Close()
            conn.Close()


        Catch ex As Exception
            'MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim trans As String = ""
            conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim Start_trans As New OracleCommand
            Start_trans.Connection = conn
            Start_trans.CommandText = "select transfer_from,transfer_to,transfer_number,transfer_status from transfer_manager where transfer_number='103'"

            conn.Open()
            Dim start_rd As OracleDataReader = Start_trans.ExecuteReader
            While start_rd.Read
                Label32.Text = "FROM:" & start_rd.GetValue(0).ToString
                'Label34.Text = start_rd.GetValue(1).ToString
                Label33.Text = "TO:" & start_rd.GetValue(1).ToString
                'Label35.Text = start_rd.GetValue(3).ToString
                Label36.Text = "TRANSFER NUMBER:" & start_rd.GetValue(2).ToString
                Label44.Text = "STATUS:" & start_rd.GetValue(3).ToString
                trans = start_rd.GetValue(2).ToString
                MaskedTextBox2.Focus()

            End While
            start_rd.Close()
            conn.Close()

            If trans = "" Then
                Label33.Text = ""
                Label32.Text = ""
                Label44.Text = ""
                Label36.Text = ""
                MaskedTextBox2.Text = ""
                TextBox11.Text = ""
                ListView3.Items.Clear()
                Label45.Text = "ERROR: Transfer number not found..."
                MaskedTextBox2.Enabled = False
                TextBox11.Enabled = False
                Button4.Enabled = False
                Button5.Enabled = False

                Exit Sub
            End If
            If Label44.Text = "STATUS:done" Then
                Button5.Enabled = False
                Button4.Enabled = True
                MaskedTextBox2.Enabled = False
                TextBox11.Enabled = False
                ListView3.Enabled = False

            Else
                Button5.Enabled = True
                Button4.Enabled = False
                MaskedTextBox2.Enabled = True
                TextBox11.Enabled = True
                ListView3.Enabled = True
                MaskedTextBox2.Focus()

            End If
            ListView3.Items.Clear()

            Dim cmd_sum As New OracleCommand
            cmd_sum.Connection = conn
            cmd_sum.CommandText = "select count( distinct box_id ), count (itm_barcode) from scanned_items where transfer_number = '" + ComboBox2.Text + "'  "

            conn.Open()
            Dim rd_sum As OracleDataReader = cmd_sum.ExecuteReader
            If rd_sum.Read Then
                Label45.Text = "BOX's:" & rd_sum.GetValue(0).ToString & "                    " & "ITEMS:" & rd_sum.GetValue(1).ToString
            End If

            rd_sum.Close()
            conn.Close()


            Dim rd0 As String
            Dim rd1 As String
            Dim rd2 As String
            Dim rd3 As String
            Dim rd4 As String
            Dim rd5 As String
            Dim cmd_scanned As New OracleCommand
            cmd_scanned.Connection = conn
            cmd_scanned.CommandText = "select * from scanned_items where transfer_number = '" + ComboBox2.Text + "'"

            conn.Open()
            Dim rd_scanned As OracleDataReader = cmd_scanned.ExecuteReader
            While rd_scanned.Read


                rd0 = rd_scanned.GetValue(0).ToString
                rd1 = rd_scanned.GetValue(1).ToString
                rd2 = rd_scanned.GetValue(2).ToString
                rd3 = rd_scanned.GetValue(3).ToString
                rd4 = rd_scanned.GetValue(4).ToString
                rd5 = rd_scanned.GetValue(5).ToString
                With ListView3.Items.Add(rd0)
                    .SubItems.Add(rd1)
                    .SubItems.Add(rd2)
                    .SubItems.Add(rd3)
                    .SubItems.Add(rd4)
                    .SubItems.Add(rd5)

                End With
            End While
            rd_scanned.Close()
            conn.Close()

            trs = ComboBox2.Text

            Label32.Visible = True
            Label33.Visible = True
            Label34.Visible = True
            Label35.Visible = True
            Label36.Visible = True
            Label37.Visible = True
            Label38.Visible = True
            Label44.Visible = True
            MaskedTextBox2.Visible = True
            TextBox11.Visible = True
            Button3.Visible = True
            Button4.Visible = True
            Button5.Visible = True
            Label45.Visible = True



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TextBox11_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox11.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then

            e.Handled = True
            If MaskedTextBox2.Text <> "" Then






                'Dim u_id, pw, ip As String


                ' u_id = My.Computer.Registry.LocalMachine.GetValue("POS_id")


                ' pw = My.Computer.Registry.LocalMachine.GetValue("POS_password")

                ' ip = My.Computer.Registry.LocalMachine.GetValue("POS_ip")
                Try


                    Dim conn As New OracleConnection
                    Dim cmd As New OracleCommand



                    cmd.Connection = conn
                    cmd.CommandText = "select de_itm from as_itm where id_itm = '" + TextBox11.Text + "'"


                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                    Dim row_count As New OracleCommand
                    Dim row As Integer = 0

                    row_count.Connection = conn
                    row_count.CommandText = "select count(qty) from scanned_items"
                    conn.Open()
                    Dim rd_row As OracleDataReader = row_count.ExecuteReader
                    If rd_row.Read Then
                        row = rd_row.GetValue(0)
                    End If
                    rd_row.Close()
                    conn.Close()
                    row = row + 1

                    Dim itm_desc As String = ""

                    conn.Open()
                    Dim rd As OracleDataReader = cmd.ExecuteReader
                    If rd.Read Then
                        itm_desc = rd.GetValue(0).ToString

                        With ListView3.Items.Add(trs)
                            .SubItems.Add(MaskedTextBox2.Text)
                            .SubItems.Add(TextBox11.Text)
                            .SubItems.Add(rd.GetValue(0))
                            .SubItems.Add(1)
                            .SubItems.Add(row)

                        End With

                        'MsgBox(row.ToString)
                    End If
                    rd.Close()

                    conn.Close()
                    If itm_desc = "" Then
                        TextBox11.Focus()
                        Label45.Text = "ERROR: ITEM not found."
                        Exit Sub

                    End If

                    Dim cmd_insert As New OracleCommand
                    cmd_insert.Connection = conn
                    cmd_insert.CommandText = "insert into scanned_items values('" + trs + "','" + MaskedTextBox2.Text + "','" + TextBox11.Text + "','" + itm_desc + "','1','" + row.ToString + "')"
                    conn.Open()
                    cmd_insert.ExecuteNonQuery()
                    conn.Close()


                    Dim cmd_sum As New OracleCommand
                    cmd_sum.Connection = conn
                    cmd_sum.CommandText = "select count( distinct box_id ), count (itm_barcode) from scanned_items where transfer_number = '" + trs + "'  "

                    conn.Open()
                    Dim rd_sum As OracleDataReader = cmd_sum.ExecuteReader
                    If rd_sum.Read Then
                        Label45.Text = "BOX's:" & rd_sum.GetValue(0).ToString & "                    " & "ITEMS:" & rd_sum.GetValue(1).ToString
                    End If

                    rd_sum.Close()
                    conn.Close()

                    TextBox11.Clear()
                    TextBox11.Focus()




                Catch ex As Exception
                    'MessageBox.Show(ex.Message)
                End Try

            Else
                Label45.Text = "ERROR: BOX ID not found."
            End If

        End If
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Panel1.Visible = True
            Panel3.Visible = True
            ListView1.Visible = True
            ListView2.Visible = True
            Label9.Visible = True
            Label52.Visible = True
            PictureBox6.Enabled = True
            Label1.Text = "Enter an item number."
            TextBox1.Enabled = True
            Label24.Visible = True
            Label25.Visible = True
            Label31.Visible = True
            Label16.Visible = True
            Label3.Visible = True
            Label1.Visible = True
            PictureBox4.Enabled = True
            PictureBox10.Enabled = True


            Panel2.Visible = False
            ListView3.Visible = False
            Button2.Visible = False
            ComboBox4.Visible = True
            ComboBox2.Visible = False

            Label32.Visible = False
            Label33.Visible = False
            Label34.Visible = False
            Label35.Visible = False
            Label36.Visible = False
            Label37.Visible = False
            Label38.Visible = False
            Label44.Visible = False
            MaskedTextBox2.Visible = False
            TextBox11.Visible = False
            Button3.Visible = False
            Button4.Visible = False
            Button5.Visible = False
            Label45.Visible = False
            TextBox1.Focus()


        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Label1.Text = "Transfer Confirmation."
            Label1.Visible = True
            Label45.Visible = False
            ComboBox2.Visible = False
            Button2.Visible = False
            Label32.Visible = False
            Label33.Visible = False
            Label36.Visible = False
            Label44.Visible = False
            Label37.Visible = False
            Label38.Visible = False
            MaskedTextBox2.Visible = False
            TextBox11.Visible = False
            ListView3.Visible = False
            Panel2.Visible = False
            Button5.Visible = False
            Button4.Visible = False
            Button3.Visible = False
            Label46.Visible = True
            Button6.Visible = True
            Button7.Visible = True



        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Label45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label45.Click

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim box As String = ""
            Dim item As String = ""

            Dim cmd_sum As New OracleCommand
            cmd_sum.Connection = conn
            cmd_sum.CommandText = "select count( distinct box_id ), count (itm_barcode) from scanned_items where transfer_number = '" + trs + "'  "

            conn.Open()
            Dim rd_sum As OracleDataReader = cmd_sum.ExecuteReader
            If rd_sum.Read Then
                box = rd_sum.GetValue(0).ToString
                item = rd_sum.GetValue(1).ToString


            End If

            rd_sum.Close()
            conn.Close()

            Dim file As System.IO.StreamWriter
            If My.Computer.FileSystem.FileExists("C:\POS\IN\TRANSFER_REPORT" + trs + ".txt") Then
                My.Computer.FileSystem.DeleteFile("C:\POS\IN\TRANSFER_REPORT" + trs + ".txt")
            End If
            file = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\IN\TRANSFER_REPORT" + trs + ".txt", True)
            file.WriteLine("                        ")
            file.WriteLine("                        ")
            file.WriteLine("                        ")
            file.WriteLine("                        ")
            file.WriteLine("    TRANSFER CONFIRMATION")
            file.WriteLine("    " & "TRANSFER NUMBER: " & trs)
            file.WriteLine("------------------")
            file.WriteLine("    " & user_id)
            'file.WriteLine("    " & store_name)
            file.WriteLine("                   -------")
            file.WriteLine("--------------------------")
            file.WriteLine("    " & System.DateTime.Now)
            file.WriteLine("==========================")
            file.WriteLine("==========================")
            file.WriteLine("==========================")
            file.WriteLine("==========================")
            file.WriteLine("    " & Label32.Text)
            file.WriteLine("    " & Label33.Text)

            file.WriteLine("    " & "TOTAL BOX's: " & box)
            file.WriteLine("    " & "TOTAL ITEMS: " & item)

            file.WriteLine("==========================")
            file.WriteLine("==========================")
            file.WriteLine("    " & "ITEM#" & "        ")
            'For i = 0 To ListView3.Items.Count - 1
            'file.WriteLine("    " & ListView3.Items(i).SubItems(2).Text & "        " & ListView3.Items(i).SubItems(4).Text)
            'Next

            Dim cmd_ibt As New OracleCommand
            cmd_ibt.Connection = conn
            cmd_ibt.CommandText = "select transfer_number,itm_barcode,count(qty) from scanned_items where transfer_number = '" + trs + "' group by transfer_number,itm_barcode,qty"
            conn.Open()
            Dim rd_ibt As OracleDataReader = cmd_ibt.ExecuteReader
            While rd_ibt.Read
                file.WriteLine("    " & rd_ibt.GetValue(1).ToString & "                          " & rd_ibt.GetValue(2).ToString)
            End While
            rd_ibt.Close()
            conn.Close()

            file.WriteLine("        " & "***")
            file.WriteLine("    " & "END OF REPORT ------")
            file.Close()




            Dim proc As New Process


            proc.StartInfo.FileName = "C:\POS\IN\TRANSFER_REPORT" + trs + ".txt"


            proc.StartInfo.Verb = "Print"


            proc.StartInfo.CreateNoWindow = True
            If printer_status = "1" Then



                proc.Start()

                proc.WaitForExit()
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            Dim trn As String = ""
            Dim itm_bar As String = ""
            Dim itm_name As String = ""
            Dim itm_qty As String = ""
            Dim cmd1 As New OracleCommand
            Dim cmd2 As New OracleCommand
            Dim cmd3 As New OracleCommand
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            cmd1.Connection = conn
            cmd1.CommandText = "select transfer_number,itm_barcode,itm_name,(count(qty) * -1) from scanned_items where transfer_number = '" + ComboBox2.Text + "' group by transfer_number,itm_barcode,itm_name,qty"

            conn.Open()
            Dim rd1 As OracleDataReader = cmd1.ExecuteReader
            While rd1.Read
                trn = rd1.GetValue(0).ToString
                itm_bar = rd1.GetValue(1).ToString
                itm_name = rd1.GetValue(2).ToString
                itm_qty = rd1.GetValue(3).ToString

                cmd2.Connection = conn
                cmd2.CommandText = "insert into stock values('" + trn + "','T-'||'" + trn + "',sysdate,'" + itm_bar + "','" + rd1.GetValue(2).ToString + "','NULL','NULL','" + itm_qty + "',sysdate)"
                cmd2.ExecuteNonQuery()
                '------------------------------------------
                Dim path As String = "C:\POS\IN"
                If Directory.Exists(path) Then
                    Dim file_delete As System.IO.StreamWriter
                    file_delete = My.Computer.FileSystem.OpenTextFileWriter("C:\POS\IN\TRANSFER_" + b_date & trn + ".txt", True)

                    file_delete.WriteLine(trn & "|" & Label32.Text & "|" & Label33.Text & "|" & itm_bar & "|" & itm_name & "|  " & itm_qty & " | " & System.DateTime.Now & "|" & "Done" & "|" & user_id)


                    file_delete.Close()
                End If
            End While

            rd1.Close()
            conn.Close()

            cmd3.Connection = conn
            cmd3.CommandText = "update transfer_manager set status = 'done' where transfer_number = '" + ComboBox2.Text + "' "

            conn.Open()
            cmd3.ExecuteNonQuery()
            conn.Close()


            Label44.Text = "STATUS:done"
            'ListView3.Clear()
            Button5.Enabled = False
            ListView3.Visible = True
            MaskedTextBox2.Visible = True
            TextBox11.Visible = True


            ListView3.Enabled = False
            MaskedTextBox2.Enabled = False
            TextBox11.Enabled = False
            Label45.Visible = True
            Label1.Visible = False
            ComboBox2.Visible = True
            Button2.Visible = True
            Label32.Visible = True
            Label33.Visible = True
            Label36.Visible = True
            Label44.Visible = True
            Label37.Visible = True
            Label38.Visible = True

            Panel2.Visible = True
            Button5.Visible = True
            Button4.Visible = True
            Button3.Visible = True
            Label46.Visible = False
            Button6.Visible = False
            Button7.Visible = False
            Button4.Enabled = True


        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            Label45.Visible = True
            Label1.Visible = False
            ComboBox2.Visible = True
            Button2.Visible = True
            Label32.Visible = True
            Label33.Visible = True
            Label36.Visible = True
            Label44.Visible = True
            Label37.Visible = True
            Label38.Visible = True
            MaskedTextBox2.Visible = True
            TextBox11.Visible = True
            ListView3.Visible = True
            Panel2.Visible = True
            Button5.Visible = True
            Button4.Visible = True
            Button3.Visible = True
            Label46.Visible = False
            Button6.Visible = False
            Button7.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MaskedTextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MaskedTextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True

            TextBox11.Focus()

        End If
    End Sub

    Private Sub MaskedTextBox2_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles MaskedTextBox2.MaskInputRejected

    End Sub

    Private Sub Label52_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label52.Click
        Try
            If position = "8" Or position = "6" Then
                org = "***********"

                TextBox12.Visible = True
                TextBox12.Focus()
                Label1.Visible = True

                Label1.Text = "Scan refund item."
                Label6.Visible = False
                Label9.Visible = False
                Label52.Visible = False

                Label5.Visible = False
                Label53.Visible = False
                TextBox1.Visible = False
                ComboBox4.Visible = False

                pos_flag = 0
                '  MessageBox.Show("D")


                refund_flag = 1
                return_flag = 1
                Label16.Visible = False
                Label24.Visible = False
                Label25.Visible = False
                Label31.Visible = False


                'Label54.Visible = True

            Else
                Label17.Text = "ERROR4:User ID dosn't has access rights."
                Label17.Visible = True
                Button1.Visible = True
                ListView1.Visible = False
                Panel1.Visible = False
                Panel3.Visible = False
                ListView2.Visible = False
                TextBox1.Visible = False
                ComboBox4.Visible = False
                TextBox2.Visible = False
                TextBox3.Visible = False
                TextBox4.Visible = False
                TextBox5.Visible = False
                TextBox6.Visible = False
                TextBox7.Visible = False
                TextBox8.Visible = False
                TextBox9.Visible = False
                Label1.Visible = False
                Label2.Visible = False
                Label3.Visible = False
                Label4.Visible = False
                Label5.Visible = False
                Label6.Visible = False
                Label7.Visible = False
                Label8.Visible = False
                Label9.Visible = False
                Label16.Visible = False
                Label24.Visible = False
                Label25.Visible = False
                Label31.Visible = False
                Label52.Visible = False


            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox12_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox12.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then



            e.Handled = True

            If TextBox12.Text <> "" Then
                Dim discount As Double = 0



                Try





                    Dim conn As New OracleConnection
                    Dim cmd As New OracleCommand
                    Dim cmd1 As New OracleCommand
                    Dim id As Double
                    item_barcode = TextBox12.Text
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"

                    ''''''''''''
                    Dim cmd_id_rfn As New OracleCommand
                    cmd_id_rfn.CommandText = "select id_ev from MA_ITM_PRN_PRC_ITM where id_itm='" + TextBox12.Text + "'"
                    cmd_id_rfn.Connection = conn


                    conn.Open()
                    Dim rd_id_rfn As OracleDataReader = cmd_id_rfn.ExecuteReader
                    If rd_id_rfn.Read Then

                        id = rd_id_rfn.GetValue(0)

                    End If
                    rd_id_rfn.Close()
                    conn.Close()
                    ''''''''''''
                    cmd.Connection = conn
                    cmd.CommandText = "select de_itm from as_itm where id_itm = '" + TextBox12.Text.Trim + "'"
                    cmd1.Connection = conn
                    cmd1.CommandText = "select mo_chn_prn_un_prc from TR_CHN_PRN_PRC where id_ev = :param1"
                    cmd1.Parameters.Add(":param1", OracleDbType.Double).Value = id




                    conn.Open()
                    Dim rd As OracleDataReader = cmd.ExecuteReader
                    If rd.Read Then
                        item_name = rd.GetValue(0).ToString

                    Else
                        TextBox12.Clear()
                        'MessageBox.Show("ITEM not found.")
                        'MsgBox("ITEM not found.", MsgBoxStyle.Information, "ERROR")
                        Label17.Text = "ERROR3:ITEM not found."
                        Label17.Visible = True
                        Button1.Visible = True
                        Label6.Visible = False
                        Label4.Visible = False
                        Label1.Visible = False
                        Label3.Visible = False
                        Label54.Visible = False
                        Label7.Visible = False
                        ListView1.Visible = False
                        Panel1.Visible = False
                        Panel3.Visible = False
                        ListView2.Visible = False
                        TextBox1.Visible = False
                        ComboBox4.Visible = False
                        TextBox2.Visible = False
                        TextBox3.Visible = False
                        TextBox4.Visible = False
                        TextBox5.Visible = False
                        TextBox6.Visible = False
                        TextBox7.Visible = False
                        TextBox8.Visible = False
                        TextBox9.Visible = False
                        TextBox12.Visible = False


                        TextBox12.Clear()








                        Exit Sub


                    End If
                    rd.Close()
                    conn.Close()
                    conn.Open()
                    Dim rd1 As OracleDataReader = cmd1.ExecuteReader
                    If rd1.Read Then
                        item_price = Double.Parse(rd1.GetValue(0)) * CF
                        item_price1 = item_price
                        Label4.Visible = True
                        PictureBox11.Visible = True
                        'Label7.Visible = True
                        'PictureBox7.Visible = True
                        'Label3.Visible = True
                        PictureBox8.Visible = True

                        TextBox12.Clear()
                        TextBox12.Focus()

                    End If
                    rd1.Close()
                    conn.Close()


                    '--------------------------------apply promotion percentage



                    Dim cmd_PROMO_PER As New OracleCommand
                    cmd_PROMO_PER.Connection = conn
                    cmd_PROMO_PER.CommandText = "select new_price from promo_per where Barcode = '" & item_barcode & "' and to_date(sysdate,'DD-MM-YY') <= to_date(end_date,'DD-MM-YY')"
                    'Dim percentage As Integer = 0

                    'Dim disc As Integer = 0
                    conn.Open()
                    Dim rd_per As OracleDataReader = cmd_PROMO_PER.ExecuteReader
                    While rd_per.Read

                        'If rd_per.GetValue(0).ToString <> "" Then
                        'percentage = Double.Parse(rd_per.GetValue(1))

                        'discount = (item_price * percentage / 100)

                        'item_name = item_name & " - " & "% Disc. (" & percentage & ")"

                        'Total_Discount = Total_Discount + discount

                        'End If
                        If Not IsDBNull(rd_per.GetValue(0)) Then


                            If rd_per.GetValue(0) * CF < item_price Then
                                Total_Discount = item_price - rd_per.GetValue(0) * CF
                                item_price = rd_per.GetValue(0) * CF


                            End If
                        End If

                    End While
                    rd_per.Close()
                    conn.Close()
                    '------------------------------------------------------------------apply one price promotion
                    Dim one_price As New OracleCommand
                    one_price.Connection = conn
                    one_price.CommandText = "select min(new_price) from promo_oneprice where Barcode = '" & item_barcode & "' and to_date(sysdate,'DD-MM-YY') <= to_date(end_date,'DD-MM-YY')"

                    conn.Open()
                    Dim rd_one_price As OracleDataReader = one_price.ExecuteReader
                    While rd_one_price.Read
                        If Not IsDBNull(rd_one_price.GetValue(0)) Then

                            If rd_one_price.GetValue(0) * CF < item_price Then
                                Total_Discount = item_price - rd_one_price.GetValue(0) * CF
                                item_price = rd_one_price.GetValue(0) * CF
                            End If

                        End If


                    End While
                    rd_one_price.Close()
                    conn.Close()
                    '---------------------------------------------------------------------------------------------

                    Try

                        With ListView1.Items.Add(item_barcode)
                            .SubItems.Add(item_name)
                            .SubItems.Add(Math.Round(item_price * -1, 2))
                            .SubItems.Add(1 * -1)
                            .SubItems.Add(0)
                            .SubItems.Add(Math.Round(Total_Discount, 2))
                            .SubItems.Add("")
                        End With
                        Total_Discount = 0
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                    'With ListView1.Items.Add(item_barcode)
                    '.SubItems.Add(item_name)
                    '.SubItems.Add(item_price * -1)
                    'Items.Add(1 * -1)
                    '.SubItems.Add(0)
                    '.SubItems.Add(Total_Discount * -1)
                    'bItems.Add("")
                    'End With
                    '---------------------------------------




                    itm_id = TextBox12.Text
                    itm_desc = item_name
                    itm_prc = item_price

                    TextBox12.Clear()


                    QTY_SUM = 0
                    TOT_SUM = 0






                    PictureBox3.Enabled = True
                    Label6.Visible = True

                    TextBox12.Focus()
                    Dim TotalSum As Double = 0
                    Dim totalqty As Double = 0
                    Dim totaldis As Double = 0

                    Dim TempNode As ListViewItem
                    For Each TempNode In ListView1.Items
                        TotalSum += CDbl(TempNode.SubItems.Item(2).Text)
                        totalqty += CDbl(TempNode.SubItems.Item(3).Text)
                        totaldis += CDbl(TempNode.SubItems.Item(5).Text)
                    Next

                    ListView2.Items(0).SubItems(0).Text = Math.Round(TotalSum, 2)
                    'ListView2.Items(0).SubItems(1).Text = Total_Discount.ToString
                    If TotalSum * -1 > Convert.ToDecimal(vat_limit) Or TotalSum > Convert.ToDecimal(vat_limit) Then
                        ListView2.Items(0).SubItems(2).Text = Math.Round(vat * (TotalSum) / 100, 3)
                    Else
                        ListView2.Items(0).SubItems(2).Text = "0"
                    End If
                    ListView2.Items(0).SubItems(1).Text = Math.Round(totaldis, 2)
                    ListView2.Items(0).SubItems(3).Text = totalqty
                    ListView2.Items(0).SubItems(4).Text = Math.Round(Double.Parse(ListView2.Items(0).SubItems(0).Text) + Double.Parse(ListView2.Items(0).SubItems(2).Text), 2)


                    itm_total = TotalSum

                    For k = 0 To ListView1.Items.Count - 1
                        If ListView1.Items(k).SubItems(3).Text = "-1" Then

                            ListView1.Items(k).ForeColor = Color.Blue
                        End If


                    Next





                    If return_flag = 1 Then
                        Label6.Visible = True
                        PictureBox3.Enabled = True
                        Label6.Text = "F6-Return"
                        TextBox9.Focus()
                        TextBox9.Clear()
                    End If


                    If exchange_flag = 1 Then
                        Label6.Visible = True
                        PictureBox3.Enabled = True
                        Label6.Text = "F6-Tender"
                        TextBox9.Focus()
                        TextBox9.Clear()
                    End If


                    If com_status = 1 Then


                        Dim sp = New SerialPort(com_port, 9600, Parity.None, 8, StopBits.One)

                        sp.Open()

                        ' to clear the display
                        sp.Write(Convert.ToString(Chr(12)))
                        'first line goes here
                        sp.WriteLine(itm_id + "       " + item_price.ToString + Chr(13))
                        '2nd line goes here
                        sp.WriteLine("Total" + "       " + itm_total)
                        sp.Dispose()
                        sp.Close()
                    End If




                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical)

                End Try
            End If
        End If
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged

    End Sub

    Private Sub Label53_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label53.Click
        Try
            If position = "8" Or position = "6" Then
                org = "**********"
                subtotal = ListView2.Items(0).SubItems(0).Text
                receipt_status = 0
                Label54.Visible = True
                Label3.Visible = False

                TextBox12.Visible = True
                TextBox12.Focus()

                Label1.Visible = True

                Label1.Text = "Scan exchange item."
                Label6.Visible = False
                Label9.Visible = False
                Label52.Visible = False

                Label5.Visible = False
                Label53.Visible = False
                TextBox1.Visible = False
                ComboBox4.Visible = False
                TextBox2.Visible = False
                TextBox3.Visible = False
                TextBox4.Visible = False
                TextBox5.Visible = False
                TextBox6.Visible = False
                TextBox7.Visible = False
                TextBox8.Visible = False
                TextBox9.Visible = False
                TextBox10.Visible = False



                pos_flag = 0
                '  MessageBox.Show("D")


                'refund_flag = 1
                'return_flag = 1
                exchange_flag = 1

                Label16.Visible = False
                Label24.Visible = False
                Label25.Visible = False
                Label31.Visible = False

                For i = 0 To ListView1.Items.Count
                    ListView1.Items(i).BackColor = Color.Gray

                Next

            Else
                Label17.Text = "ERROR4:User ID dosn't has access rights."
                Label17.Visible = True
                Label53.Visible = False
                Button1.Visible = True
                ListView1.Visible = False
                Panel1.Visible = False
                Panel3.Visible = False
                ListView2.Visible = False
                TextBox1.Visible = False
                ComboBox4.Visible = False
                TextBox2.Visible = False
                TextBox3.Visible = False
                TextBox4.Visible = False
                TextBox5.Visible = False
                TextBox6.Visible = False
                TextBox7.Visible = False
                TextBox8.Visible = False
                TextBox9.Visible = False
                Label1.Visible = False
                Label2.Visible = False
                Label3.Visible = False
                Label4.Visible = False
                Label5.Visible = False
                Label6.Visible = False
                Label7.Visible = False
                Label8.Visible = False
                Label9.Visible = False
                Label16.Visible = False
                Label24.Visible = False
                Label25.Visible = False
                Label31.Visible = False
                Label52.Visible = False


            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label54_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label54.Click
        Try

            Label6.Visible = True
            Label5.Visible = True
            Label53.Visible = True
            Label54.Visible = False
            Label4.Visible = True
            Label3.Visible = False
            Label7.Visible = True
            ListView1.Enabled = True
            PictureBox3.Enabled = True
            PictureBox4.Enabled = True
            PictureBox6.Enabled = True
            PictureBox11.Enabled = True
            TextBox1.Visible = True
            ComboBox4.Visible = True
            TextBox1.Enabled = True
            TextBox1.Clear()
            TextBox1.Focus()
            TextBox2.Visible = False
            TextBox3.Visible = False
            TextBox4.Visible = False
            TextBox5.Visible = False
            TextBox6.Visible = False
            TextBox7.Visible = False
            TextBox8.Visible = False
            TextBox9.Visible = False
            TextBox10.Visible = False

            TextBox12.Visible = False

            Label1.Text = "Enter an item number."
            exchange_flag = 0

            For Each item As ListViewItem In ListView1.Items
                If item.SubItems(3).Text = "-1" Then
                    item.Remove()

                End If
            Next


            Dim TotalSum As Double = 0
            Dim totalqty As Double = 0
            Dim totaldis As Double = 0

            Dim TempNode As ListViewItem
            For Each TempNode In ListView1.Items
                TotalSum += CDbl(TempNode.SubItems.Item(2).Text)
                totalqty += CDbl(TempNode.SubItems.Item(3).Text)
                totaldis += CDbl(TempNode.SubItems.Item(5).Text)
            Next

            ListView2.Items(0).SubItems(0).Text = Math.Round(TotalSum, 2)
            'ListView2.Items(0).SubItems(1).Text = Total_Discount.ToString
            If TotalSum > Convert.ToDecimal(vat_limit) Then
                ListView2.Items(0).SubItems(2).Text = Math.Round(vat * (TotalSum) / 100, 3)
            Else
                ListView2.Items(0).SubItems(2).Text = "0"
            End If
            ListView2.Items(0).SubItems(1).Text = Math.Round(totaldis, 2)
            ListView2.Items(0).SubItems(3).Text = totalqty
            ListView2.Items(0).SubItems(4).Text = Math.Round(Double.Parse(ListView2.Items(0).SubItems(0).Text) + Double.Parse(ListView2.Items(0).SubItems(2).Text), 2)


            Label2.Visible = False
            Label8.Visible = False
            TextBox2.Clear()
            TextBox2.Visible = False
            TextBox1.Visible = True
            ComboBox4.Visible = True
            TextBox1.Focus()

            For i = 0 To ListView1.Items.Count

                If ListView1.Items(i).SubItems(6).Text = 1 Then
                    ListView1.Items(i).BackColor = Color.Red
                ElseIf ListView1.Items(i).SubItems(6).Text = 2 Then
                    ListView1.Items(i).BackColor = Color.Orange
                Else
                    ListView1.Items(i).BackColor = Color.White
                End If



            Next

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Label55_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label55.Click
        Try
            Label2.Visible = False
            'Label4.Visible = False
            Label1.Visible = True
            Label1.Text = "Select printer status."
            RadioButton1.Visible = True
            RadioButton2.Visible = True

            Label55.Visible = False
            Label56.Visible = True
            Label57.Visible = False
            Label58.Visible = False
            Label60.Visible = False
            RadioButton1.Checked = True


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label56_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label56.Click
        Try

            If RadioButton1.Visible = True Then


                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                Dim cmd_printer As New OracleCommand
                cmd_printer.Connection = conn

                If RadioButton1.Checked = True Then
                    cmd_printer.CommandText = "update printer_status set status = '2'"
                    conn.Open()
                    cmd_printer.ExecuteNonQuery()
                    conn.Close()
                    printer_status = "2"
                End If

                If RadioButton2.Checked = True Then
                    cmd_printer.CommandText = "update printer_status set status = '1'"
                    conn.Open()
                    cmd_printer.ExecuteNonQuery()
                    conn.Close()
                    printer_status = "1"
                End If

                Label2.Visible = True
                'Label4.Visible = True
                Label1.Visible = False
                Label1.Text = ""
                RadioButton1.Visible = False
                RadioButton2.Visible = False

                Label55.Visible = True
                Label56.Visible = False
                Label57.Visible = True
                Label58.Visible = True
                Label60.Visible = True

            End If

            If RadioButton4.Visible = True Then


                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                Dim cmd_printer As New OracleCommand
                cmd_printer.Connection = conn

                If RadioButton3.Checked = True Then
                    cmd_printer.CommandText = "update receipt_languege set lang = '2'"
                    conn.Open()
                    cmd_printer.ExecuteNonQuery()
                    conn.Close()
                    receipt_lang = "2"
                End If

                If RadioButton4.Checked = True Then
                    cmd_printer.CommandText = "update receipt_languege set lang = '1'"
                    conn.Open()
                    cmd_printer.ExecuteNonQuery()
                    conn.Close()
                    receipt_lang = "1"
                End If

                Label2.Visible = True
                'Label4.Visible = True
                Label1.Visible = False
                Label1.Text = ""
                RadioButton3.Visible = False
                RadioButton4.Visible = False

                Label55.Visible = True
                Label56.Visible = False
                Label57.Visible = True
                Label58.Visible = True
                Label60.Visible = True

            End If

            If ComboBox3.Visible = True Then
                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                Dim cmd_Color As New OracleCommand
                cmd_Color.Connection = conn

                cmd_Color.CommandText = "update background set color = '" + ComboBox3.Text + "'"
                conn.Open()
                cmd_Color.ExecuteNonQuery()
                conn.Close()


                Label2.Visible = True
                'Label4.Visible = True
                Label1.Visible = False
                Label1.Text = ""
                Label59.Visible = False
                ComboBox3.Visible = False

                Label55.Visible = True
                Label56.Visible = False
                Label57.Visible = True
                Label58.Visible = True
                Label60.Visible = True

            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Label57_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label57.Click
        Try



            'If Label2.Text = "F2-Pole" And Label2.Visible = True Then
            Label2.Visible = True
            Label2.Text = "F2-POS"
            Label3.Text = "ESC-Undo" 'ERROR-17-10-2018
            'Label2.Enabled = True
            PictureBox2.Enabled = True
            PictureBox6.Enabled = True
            Label55.Visible = False
            Label5.Visible = True
            Label8.Visible = False
            Label57.Visible = False
            Label58.Visible = False
            Label60.Visible = False

            RichTextBox1.Visible = False
            PictureBox9.Visible = True
            If log_flag = "1" Then
                Label9.Visible = True
                Label9.Text = "F3-Z Report"
                PictureBox4.Enabled = True
            End If

            admin_flag = 0



            'MessageBox.Show("4")
            'End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label58_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label58.Click
        Try
            Label2.Visible = False
            'Label4.Visible = False
            Label1.Visible = True
            Label1.Text = "Background Color."


            Label55.Visible = False
            Label56.Visible = True
            Label57.Visible = False

            Label59.Visible = True
            ComboBox3.Visible = True
            Label58.Visible = False
            Label60.Visible = False


        Catch ex As Exception

        End Try

    End Sub

    Private Sub Label60_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label60.Click
        Try
            Label2.Visible = False
            'Label4.Visible = False
            Label1.Visible = True
            Label1.Text = "Select receipt languege."
            RadioButton3.Visible = True
            RadioButton4.Visible = True

            Label55.Visible = False
            Label56.Visible = True
            Label57.Visible = False
            Label58.Visible = False
            Label60.Visible = False
            RadioButton4.Checked = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListView1_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        Try
            For i = 0 To ListView1.Items.Count
                If ListView1.Items(i).BackColor = Color.Gray Or ListView1.Items(i).BackColor = Color.Red Or ListView1.Items(i).BackColor = Color.Orange Then
                    ListView1.Items(i).Selected = False

                End If

            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PrintZatcaQR_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintZatcaQR.PrintPage
        'Dim xCustomSize As New PaperSize("Receipt", 72.2, 593.8)

        ' PrintZatcaQR.DefaultPageSettings.PaperSize = xCustomSize

        Dim f8 As New Font("Calibri", 11, FontStyle.Regular)
        Dim f9 As New Font("Calibri", 9, FontStyle.Regular)

        Dim leftmargin As Integer = PrintZatcaQR.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PrintZatcaQR.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PrintZatcaQR.DefaultPageSettings.PaperSize.Width

        Dim right As New StringFormat
        Dim Center As New StringFormat
        Dim left As New StringFormat

        Dim hight As Integer = 5

        right.Alignment = StringAlignment.Far
        Center.Alignment = StringAlignment.Center
        left.Alignment = StringAlignment.Near




        ' Dim line As String
        ' line = "*************************************************"
        ' 'Dim logoImage As Image = My.Resources.ResourceManager.GetObject("logo")
        ' 'e.Graphics.DrawImage(logoImage, CInt((e.PageBounds.Width - 150) / 2), 5, 150, 35)
        '
        ' e.Graphics.DrawString("LULU", f8, Brushes.Black, centermargin, 50, Center)
        ' e.Graphics.DrawString("Hypermarket  هايبرماركت", f8, Brushes.Black, centermargin, 71, Center)
        ' e.Graphics.DrawString("Tel +1763545473", f8, Brushes.Black, centermargin, 82, Center)
        ' e.Graphics.DrawString("Jeddah Madina Raod   جدة طريق المدينة", f8, Brushes.Black, centermargin, 103, Center)
        ' e.Graphics.DrawString("Invoice ID:079780012255230810", f8, Brushes.Black, centermargin, 124, Center)
        ' e.Graphics.DrawString("Cashier:Steve Jobs", f8, Brushes.Black, centermargin, 136, Center)
        ' e.Graphics.DrawString("الرقم الضريبي 01236549879", f8, Brushes.Black, centermargin, 157, Center)
        '
        '
        ' e.Graphics.DrawString("31-08-2023 15:53", f8, Brushes.Black, centermargin, 178, Center)
        ' 'qrcode


        Dim line As String
        Dim FilePath As String = "C:\POS\Receipts\" + rr + ".txt"
        ' Create new StreamReader instance with Using block.
        Dim reader As StreamReader = New StreamReader(FilePath)

        While reader.Read
            ' Read one line from file
            line = reader.ReadLine
            line = "            " & line
            hight = hight + 13
            e.Graphics.DrawString(line, f9, Brushes.Black, leftmargin, hight, Center)
            'MessageBox.Show(line)
            If reader.EndOfStream Then
                Exit While
            End If

        End While

        Try
            Dim value1 As String = getTLV(1, company_name)
            Dim value2 As String = getTLV(2, vat_number)
            Dim value3 As String = getTLV(3, System.DateTime.Now.ToString)
            Dim value4 As String = getTLV(4, receipt_value)
            Dim value5 As String = getTLV(5, vat_value)
            Dim b As Byte() = System.Text.Encoding.UTF8.GetBytes(value1 & value2 & value3 & value4 & value5)
            Dim t As String = Convert.ToBase64String(b)

            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(t, QRCodeGenerator.ECCLevel.M)
            Dim code As New QRCode(data)

            Dim qrImage As Image
            qrImage = New Bitmap(code.GetGraphic(6))
            e.Graphics.DrawImage(qrImage, CInt((e.PageBounds.Width - 170) / 2), hight + 11, 120, 120)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



        Dim writer As New BarcodeWriter
        writer.Format = BarcodeFormat.CODE_128
        Try
            ' Dim barcodeImage As Image

            PictureBox12.Image = New Bitmap(writer.Write(rr_barcode))
            e.Graphics.DrawImage(PictureBox12.Image, CInt((e.PageBounds.Width - 190) / 2), hight + 134)
        Catch ex As Exception

        End Try
    End Sub
    Private Function getTLV(tag As Integer, value As String) As String
        Return Chr(tag) & Chr(value.Length) & value
    End Function

    Private Sub Button8_Click(sender As Object, e As EventArgs)
        Try
            AddHandler PrintZatcaQR.PrintPage, AddressOf Me.PrintImage
            PrintZatcaQR.Print()
        Catch ex As Exception
            MsgBox(ex, MsgBoxStyle.Critical, "Error during Print")
        End Try
    End Sub
    Private Sub PrintImage(ByVal sender As Object, ByVal ppea As PrintPageEventArgs)
        ' ppea.Graphics.DrawImage(Image.FromFile("D:\QR\QR.jpg"), ppea.Graphics.VisibleClipBounds)
        'ppea.HasMorePages = False
    End Sub

    Private Sub PictureBox12_Click(sender As Object, e As EventArgs) Handles PictureBox12.Click

    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged

        TextBox1.Focus()
    End Sub

    Private Sub ListView1_MouseClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseClick
        Try
            For i = 0 To ListView1.Items.Count
                If ListView1.Items(i).BackColor = Color.Gray Or ListView1.Items(i).BackColor = Color.Red Or ListView1.Items(i).BackColor = Color.Orange Then
                    ListView1.Items(i).Selected = False

                End If

            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListView1_MouseMove(sender As Object, e As MouseEventArgs) Handles ListView1.MouseMove
        Try
            For i = 0 To ListView1.Items.Count
                If ListView1.Items(i).BackColor = Color.Gray Or ListView1.Items(i).BackColor = Color.Red Or ListView1.Items(i).BackColor = Color.Orange Then
                    ListView1.Items(i).Selected = False

                End If

            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComboBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox4.KeyPress

        If Not Char.IsDigit(e.KeyChar) Then e.Handled = True

        If e.KeyChar = Chr(8) Then e.Handled = False 'allow Backspace
    End Sub

    Private Sub POS_Screen_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try

            If conn.State = ConnectionState.Closed Then
                conn.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
                conn.Open()
            End If
            If autorestart = 1 Then
                Process.Start("C:\POS\retail+.exe")
                'Process.Start("C:\Users\Abdu\OneDrive\Desktop\POS Cloud Project\Source Code\POS Screen-Retail-Zatca -XE\POS Screen\bin\Debug\POS Screen.exe")
            Else


                Dim cmd_auto_logon As New OracleCommand
                cmd_auto_logon.Connection = conn
                cmd_auto_logon.CommandText = "delete from auto_logon"
                cmd_auto_logon.ExecuteNonQuery()

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub

    Private Sub POS_Screen_MenuStart(sender As Object, e As EventArgs) Handles Me.MenuStart

    End Sub

    Private Sub TextBox4_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox4.MouseClick

    End Sub
End Class
