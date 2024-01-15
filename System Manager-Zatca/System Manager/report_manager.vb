'last update 01/11/2023 1:21PM
Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Printing 'for drawing QR and print
'Imports Oracle.ManagedDataAccess.EntityFramework

Imports System.IO

Imports System.IO.Ports

Imports System.Drawing.Graphics
Imports System.Drawing.Drawing2D
Imports System.Drawing.Image
Imports QRCoder 'for QR

Imports ZXing ' for generating barcode
'Imports ZXing.Presentation

Public Class report_manager
    Public u_id As String = ""
    Public pw As String = ""
    Dim ip As String
    Dim oracle_sid As String
    Dim ds As New PacketDataSet1
    Dim dt As New DataTable
    Dim cr As String
    Public store_code As String
    Public company_name As String
    Public store_name As String
    Public vat_number As String
    Public to_date As String
    Public from_date As String
    Public total As Double = 0
    Public CF As Double = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub report_manager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                store_code = SC
                pw = DB_password

            End If
            Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim conn As New OracleConnection(oradb)
            Dim cmd As New OracleCommand
            conn.Open()
            cmd.Connection = conn
            cmd.CommandText = "select de_itm from as_itm order by de_itm"
            Dim rd As OracleDataReader = cmd.ExecuteReader
            While rd.Read
                ComboBox2.Items.Add(rd.GetValue(0))

            End While

            rd.Close()


            Dim cmd1 As New OracleCommand
            cmd1.Connection = conn
            cmd1.CommandText = "select store_name,comp_name,vat_number from pos_info"
            Dim rd1 As OracleDataReader = cmd1.ExecuteReader
            While rd1.Read
                store_name = rd1.GetValue(0).ToString
                company_name = rd1.GetValue(1).ToString
                vat_number = rd1.GetValue(2).ToString
            End While
            rd1.Close()
            conn.Close()


            ''''''''''''''''''''''''

            Dim conn1 As New OracleConnection
            Dim cmd_CF As New OracleCommand



            cmd_CF.Connection = conn1
            cmd_CF.CommandText = "select * from pos_info"


            conn1.ConnectionString = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"


            conn1.Open()
            Dim rd_CF As OracleDataReader = cmd_CF.ExecuteReader
            If rd_CF.Read Then

                CF = rd_CF.GetValue(7)

            End If
            rd_CF.Close()
            conn1.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub ComboBox2_KeyUp(sender As Object, e As KeyEventArgs) Handles ComboBox2.KeyUp
        Try
            Dim index As Integer
            Dim actual As String
            Dim found As String

            ' Do nothing for some keys such as navigation keys.
            If ((e.KeyCode = Keys.Back) Or
                (e.KeyCode = Keys.Left) Or
                (e.KeyCode = Keys.Right) Or
                (e.KeyCode = Keys.Up) Or
                (e.KeyCode = Keys.Delete) Or
                (e.KeyCode = Keys.Down) Or
                (e.KeyCode = Keys.PageUp) Or
                (e.KeyCode = Keys.PageDown) Or
                (e.KeyCode = Keys.Home) Or
                (e.KeyCode = Keys.End)) Then

                Return
            End If

            ' Store the actual text that has been typed.
            actual = Me.ComboBox2.Text

            ' Find the first match for the typed value.
            index = Me.ComboBox2.FindString(actual)

            ' Get the text of the first match.
            If (index > -1) Then
                found = Me.ComboBox2.Items(index).ToString()

                ' Select this item from the list.
                Me.ComboBox2.SelectedIndex = index

                ' Select the portion of the text that was automatically
                ' added so that additional typing will replace it.
                Me.ComboBox2.SelectionStart = actual.Length
                Me.ComboBox2.SelectionLength = found.Length
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        Try
            Dim oradb As String = "Data Source=" + ip + ":1521/" + oracle_sid + ";User Id=" + u_id + ";password=" + pw + ";"
            Dim conn As New OracleConnection(oradb)
            Dim cmd As New OracleCommand
            Label5.Text = ""
            to_date = DateTimePicker2.Text
            from_date = DateTimePicker1.Text
            If ComboBox1.Text = "Stock summery" Then
                ''Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select i_b ""Item Number"",i_d  ""Item Description"",sum(quan) Quantity from stock  where to_date(r_d,'DD-MM-YYYY') >= to_date('" & DateTimePicker1.Text & "','DD-MM-YYYY') and to_date(r_d,'DD-MM-YYYY')  <= to_date('" & DateTimePicker2.Text & "','DD-MM-YYYY')  and i_b like '%" + txt1.Text + "%' and R_R != 'Expired' group by i_b,i_d"
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "STOCK")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Stock summery"

            ElseIf ComboBox1.Text = "Expired items" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select EX_D ""Expiration Date"", r_n  ""Receipt number"",r_r ""Receipt reference"",r_d ""Receipt Date"", i_b ""Item number"",i_d ""Item Description"",quan Quantity,i_p ""Unit price"",sum(quan) * i_p ""Total($)"" from stock 
                where to_date(EX_D,'DD-MM-YYYY') >= to_date('" & DateTimePicker1.Text & "','DD-MM-YYYY') and to_date(EX_D,'DD-MM-YYYY')  <= to_date('" & DateTimePicker2.Text & "','DD-MM-YYYY')
                and i_b like '%" & txt1.Text & "%' and R_R='Expired'
                group by  i_b,i_d,i_p,r_n,r_d,quan,i_p,r_r,EX_D
                order by i_b"

                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "STOCK")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Expired items"
                total = 0
                For i = 0 To DataGridView1.Rows.Count - 1


                    total = total + DataGridView1.Rows(i).Cells(8).Value
                Next
                Label5.Text = "Total Cost of Expired items is " & total
            ElseIf ComboBox1.Text = "Stock by receipts" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select R_N ""Delivery number"",R_D ""Delivery date"",I_B ""Item number"",I_D ""Item decription"",((i_P * " & CF & ") * Quan) ""Purchasing price($)"",Quan ""Quatity"",EX_D ""Expiration date"",(i_P * " & CF & ") ""Unit price""  from stock where to_date(r_d,'DD-MM-YYYY') >= to_date('" & DateTimePicker1.Text & "','DD-MM-YYYY') and to_date(r_d,'DD-MM-YYYY')  <= to_date('" & DateTimePicker2.Text & "','DD-MM-YYYY') and   r_n like '%" + txt1.Text + "%'  and R_R != 'Expired' order by r_n "
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "STOCK")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Stock by receipts"
                total = 0
                For i = 0 To DataGridView1.Rows.Count - 1


                    total = total + DataGridView1.Rows(i).Cells(4).Value
                Next
                Label5.Text = "Total of Purshasing Price is " & total
            ElseIf ComboBox1.Text = "Sales summery" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "SELECT
               H2.ID_ITM_POS ""Item Number"", b.de_itm ""Item Description"", H2.MO_PRN_PRC ""Item Price"", sum(h2.qu_itm_lm_rtn_sls) Quantity
               From tr_ltm_sls_rtn h2
               inner Join as_itm B
               On b.ID_itm=h2.id_itm And dc_dy_bsn >= '" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "' and  dc_dy_bsn <= '" & DateTimePicker2.Value.ToString("yyyy-MM-dd") & "'
               and h2.id_itm_pos like '%" & txt1.Text & "%'
               GROUP BY H2.ID_ITM_POS, b.de_itm, H2.MO_PRN_PRC, h2.qu_itm_lm_rtn_sls
               ORDER BY H2.ID_ITM_POS"
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "TR")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Sales summery"
            ElseIf ComboBox1.Text = "Sales by actual date" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select system_date  ""Actual date"",business_date  ""Business date"",receipt_reference ""Receipt reference"",qty Quantity,total ""Receipt amount"",tender_type ""Payment type"",ops_type ""Operation type"",VAT 
                from receipt_numbers where to_date(system_date,'DD-MM-YY') >= to_date('" & DateTimePicker1.Text & "','DD-MM-YY') and to_date(system_date,'DD-MM-YY') <= to_date('" & DateTimePicker2.Text & "','DD-MM-YY') order by system_date"
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "RECEIPT")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Sales by actual date"
                total = 0
                For i = 0 To DataGridView1.Rows.Count - 1


                    total = total + DataGridView1.Rows(i).Cells(4).Value
                Next
                Label5.Text = "Total of Sales is " & total
            ElseIf ComboBox1.Text = "Sales by receipts detail" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "SELECT     H2.AI_TRN ""Transaction number"",
H2.ID_ITM_POS ""Item number"" ,
decode(ty_trn, 1,'SALE',2,'RETURN',5,'EEXCH','UNKNOWN') ""Operation type"" ,
H2.QU_ITM_LM_RTN_SLS Quantity ,
H2.mo_extn_ln_itm_rtn ""Receipt amount"",
ID_OPR      Cashier, H1.DC_DY_BSN ""Business date"", H1.ts_trn_end ""Transaction time"" from  tr_trn H1 
inner Join    
(Select a.*  from tr_ltm_sls_rtn a  inner join  TR_LTM_RTL_TRN b  on a.ID_STR_RT = b.ID_STR_RT  And a.id_ws = b.id_ws  And a.DC_DY_BSN = b.DC_DY_BSN And a.dc_dy_bsn >='" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "' and a.dc_dy_bsn <='" & DateTimePicker2.Value.ToString("yyyy-MM-dd") & "' and a.ai_trn = b.ai_trn   and a.ai_ln_itm = b.ai_ln_itm  and a.FL_VD_LN_ITM = b.FL_VD_LN_ITM  and a.FL_VD_LN_ITM = 0) H2 on H1.dc_dy_bsn = H2.dc_dy_bsn and H2.ID_ITM_POS like '%" & txt1.Text & "%' and  H1.id_Str_rt= H2.id_str_rt and   H1.id_ws = H2.id_ws and   H1.ai_trn = H2.ai_trn left outer join  tr_ltm_dsc tld on  H2.dc_dy_bsn = tld.dc_dy_bsn and   H2.id_Str_rt = tld.id_str_rt  and   H2.id_ws = tld.id_ws and   H2.ai_trn = tld.ai_trn  and   H2.ai_ln_itm = tld.ai_ln_itm  left outer join  CO_MDFR_RTL_PRC cmr on H2.dc_dy_bsn = cmr.dc_dy_bsn and   H2.id_Str_rt = cmr.id_str_rt and  H2.id_ws = cmr.id_ws  and   H2.ai_trn = cmr.ai_trn  and   H2.ai_ln_itm = cmr.ai_ln_itm left outer join  TR_LTM_PRM Promo on H2.dc_dy_bsn = Promo.dc_dy_bsn and   H2.id_Str_rt = Promo.id_str_rt and H2.id_ws = Promo.id_ws and   H2.ai_trn = Promo.ai_trn  and   H2.ai_ln_itm = Promo.ai_ln_itm  where H2.fl_vd_ln_itm = 0 AND   H1.FL_TRG_TRN = 0  AND   H1.SC_TRN = 2  Order by  H2.dc_dy_bsn"
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "TR_TRN")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Sales by receipts detail"
                total = 0
                For i = 0 To DataGridView1.Rows.Count - 1


                    total = total + DataGridView1.Rows(i).Cells(4).Value
                Next
                Label5.Text = "Total of Sold Items is " & total
            ElseIf ComboBox1.Text = "Inventory" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select a.id_itm ""Item number"",a.de_itm ""Item description"",
(select sum(quan)from stock s where a.id_itm=s.i_b) Received ,
(select sum(qu_itm_lm_rtn_sls) from tr_ltm_sls_rtn t where a.id_itm=t.id_itm_pos) Sold,
(select sum(quan) from stock s where a.id_itm=s.i_b)-(select sum(qu_itm_lm_rtn_sls) from tr_ltm_sls_rtn t where a.id_itm=t.id_itm_pos) Remaining,
(select s_p from original_prices o where a.id_itm=o.barcode) ""Market price"",
((select sum(quan) from stock s where a.id_itm=s.i_b)-(select sum(qu_itm_lm_rtn_sls) from tr_ltm_sls_rtn t where a.id_itm=t.id_itm_pos)) * (select s_p from original_prices o where a.id_itm=o.barcode) ""REMAINING($)""
From as_itm a where a.id_itm like '%" & txt1.Text & "%'
Order By a.id_itm"
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "INVI")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Inventory"
                total = 0
                For i = 0 To DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(i).Cells(2).Value Is Nothing OrElse IsDBNull(DataGridView1.Rows(i).Cells(2).Value) Then
                        DataGridView1.Rows(i).Cells(2).Value = 0
                    End If
                    If DataGridView1.Rows(i).Cells(3).Value Is Nothing OrElse IsDBNull(DataGridView1.Rows(i).Cells(3).Value) Then
                        DataGridView1.Rows(i).Cells(3).Value = 0
                    End If
                    If DataGridView1.Rows(i).Cells(4).Value Is Nothing OrElse IsDBNull(DataGridView1.Rows(i).Cells(4).Value) Then
                        DataGridView1.Rows(i).Cells(4).Value = DataGridView1.Rows(i).Cells(2).Value - DataGridView1.Rows(i).Cells(3).Value
                    End If
                    If DataGridView1.Rows(i).Cells(6).Value Is Nothing OrElse IsDBNull(DataGridView1.Rows(i).Cells(6).Value) Then
                        DataGridView1.Rows(i).Cells(6).Value = DataGridView1.Rows(i).Cells(4).Value * DataGridView1.Rows(i).Cells(5).Value
                    End If


                    'DataGridView1.Rows(i).Cells(4).Value = DataGridView1.Rows(i).Cells(2).Value - DataGridView1.Rows(i).Cells(3).Value
                    'DataGridView1.Rows(i).Cells(6).Value = DataGridView1.Rows(i).Cells(4).Value * DataGridView1.Rows(i).Cells(5).Value

                    total = total + DataGridView1.Rows(i).Cells(6).Value
                Next
                Label5.Text = "Total of The Remaining Stock is " & total

            ElseIf ComboBox1.Text = "Sales by business date" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select business_date  ""Business date"",system_date  ""Actual date"",receipt_reference ""Receipt reference"",qty Quantity,total ""Receipt amount"",tender_type ""Payment type"",ops_type ""Operation type"",VAT 
                from receipt_numbers where to_date(business_date,'DD-MM-YY') >= to_date('" & DateTimePicker1.Text & "','DD-MM-YY') and to_date(business_date,'DD-MM-YY') <= to_date('" & DateTimePicker2.Text & "','DD-MM-YY') order by business_date"
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "SBB")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Sales by business date"
                total = 0
                For i = 0 To DataGridView1.Rows.Count - 1


                    total = total + DataGridView1.Rows(i).Cells(4).Value
                Next
                Label5.Text = "Total of Sales is " & total
            ElseIf ComboBox1.Text = "Merchandise" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select r_n  ""Receipt number"",r_r ""Receipt reference"",r_d ""Receipt date"", i_b ""Item number"",i_d ""Item description"",quan Quantity,i_p ""Unit price"",sum(quan) * i_p ""Total($)"" from stock 
                where i_b like '%" & txt1.Text & "%' and to_date(r_d,'DD-MM-YYYY') >= to_date('" & DateTimePicker1.Text & "','DD-MM-YYYY') and to_date(r_d,'DD-MM-YYYY')  <= to_date('" & DateTimePicker2.Text & "','DD-MM-YYYY') 
                group by  i_b,i_d,i_p,r_n,r_d,quan,i_p,r_r 
                order by r_n"
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "STOCK")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Merchandise"
                total = 0
                For i = 0 To DataGridView1.Rows.Count - 1


                    total = total + DataGridView1.Rows(i).Cells(7).Value
                Next
                Label5.Text = "Total Cost of the Merchandise is " & total
            ElseIf ComboBox1.Text = "Expiration report" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select EX_D ""Expiration Date"", r_n  ""Receipt number"",r_r ""Receipt reference"",r_d ""Receipt Date"", i_b ""Item number"",i_d ""Item Description"",quan Quantity,i_p ""Unit price"",sum(quan) * i_p ""Total($)"" from stock 
                where R_R != 'Expired' and to_date(EX_D,'DD-MM-YYYY') >= to_date('" & DateTimePicker1.Text & "','DD-MM-YYYY') and to_date(EX_D,'DD-MM-YYYY')  <= to_date('" & DateTimePicker2.Text & "','DD-MM-YYYY')
                and i_b like '%" & txt1.Text & "%'
                group by  i_b,i_d,i_p,r_n,r_d,quan,i_p,r_r,EX_D
                order by i_b"

                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "STOCK")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Expiration report"
                total = 0
                For i = 0 To DataGridView1.Rows.Count - 1


                    total = total + DataGridView1.Rows(i).Cells(8).Value
                Next
                Label5.Text = "Total Cost of Expired items is " & total
            ElseIf ComboBox1.Text = "Search item" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select a.id_itm ""Item number"",a.de_itm ""Item description"",
(select sum(quan)from stock s where a.id_itm=s.i_b) Received ,
(select sum(qu_itm_lm_rtn_sls) from tr_ltm_sls_rtn t where a.id_itm=t.id_itm_pos) Sold,
(select sum(quan) from stock s where a.id_itm=s.i_b)-(select sum(qu_itm_lm_rtn_sls) from tr_ltm_sls_rtn t where a.id_itm=t.id_itm_pos) Remaining,
(select s_p from original_prices o where a.id_itm=o.barcode) ""Market price"",
((select sum(quan) from stock s where a.id_itm=s.i_b)-(select sum(qu_itm_lm_rtn_sls) from tr_ltm_sls_rtn t where a.id_itm=t.id_itm_pos)) * (select s_p from original_prices o where a.id_itm=o.barcode) ""REMAINING($)""
From as_itm a where de_itm like '%" + ComboBox2.Text + "%'
Order By a.id_itm"
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "AS_ITM")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Search item"
                total = 0
                For i = 0 To DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(i).Cells(2).Value Is Nothing OrElse IsDBNull(DataGridView1.Rows(i).Cells(2).Value) Then
                        DataGridView1.Rows(i).Cells(2).Value = 0
                    End If
                    If DataGridView1.Rows(i).Cells(3).Value Is Nothing OrElse IsDBNull(DataGridView1.Rows(i).Cells(3).Value) Then
                        DataGridView1.Rows(i).Cells(3).Value = 0
                    End If
                    If DataGridView1.Rows(i).Cells(4).Value Is Nothing OrElse IsDBNull(DataGridView1.Rows(i).Cells(4).Value) Then
                        DataGridView1.Rows(i).Cells(4).Value = DataGridView1.Rows(i).Cells(2).Value - DataGridView1.Rows(i).Cells(3).Value
                    End If
                    If DataGridView1.Rows(i).Cells(6).Value Is Nothing OrElse IsDBNull(DataGridView1.Rows(i).Cells(6).Value) Then
                        DataGridView1.Rows(i).Cells(6).Value = DataGridView1.Rows(i).Cells(4).Value * DataGridView1.Rows(i).Cells(5).Value
                    End If


                    'DataGridView1.Rows(i).Cells(4).Value = DataGridView1.Rows(i).Cells(2).Value - DataGridView1.Rows(i).Cells(3).Value
                    'DataGridView1.Rows(i).Cells(6).Value = DataGridView1.Rows(i).Cells(4).Value * DataGridView1.Rows(i).Cells(5).Value

                    total = total + DataGridView1.Rows(i).Cells(6).Value
                Next
                Label5.Text = "Total of The Remaining Stock is " & total


            ElseIf ComboBox1.Text = "Operation fees" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select Bill_number ""Bill number"",Fees_type ""Fees type"", Fees_Desc ""Fees description"",Bill_Ref ""Bill reference"",Bill_date ""Bill date"",Bill_Amount ""Bill amount""
                                  from ops_fees
                                  where to_date(Bill_date,'DD-MM-YYYY') >= to_date('" & DateTimePicker1.Text & "','DD-MM-YYYY') and to_date(Bill_date,'DD-MM-YYYY')  <= to_date('" & DateTimePicker2.Text & "','DD-MM-YYYY') and Bill_number like '%" & txt1.Text & "%' order by bill_number"

                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "STOCK")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Operation fees"
                total = 0
                For i = 0 To DataGridView1.Rows.Count - 1
                    total = total + DataGridView1.Rows(i).Cells(5).Value
                Next
                Label5.Text = "Total Value of Bills is " & total

            ElseIf ComboBox1.Text = "Rent" Or ComboBox1.Text = "Electric" Or ComboBox1.Text = "Salaries" Or ComboBox1.Text = "TAX" Or ComboBox1.Text = "Gov. Fees" Or ComboBox1.Text = "Petrol" Or ComboBox1.Text = "Maintenance" Or ComboBox1.Text = "Shrink" Or ComboBox1.Text = "Transport" Or ComboBox1.Text = "Packing stuff" Or ComboBox1.Text = "Tools" Or ComboBox1.Text = "Devices" Or ComboBox1.Text = "Super market stuff" Or ComboBox1.Text = "Others fees" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select Bill_number ""Bill number"",Fees_type ""Fees type"", Fees_Desc ""Fees description"",Bill_Ref ""Bill reference"",Bill_date ""Bill date"",Bill_Amount ""Bill amount""
                                  from ops_fees
                                  where fees_type='" & ComboBox1.Text & "' and to_date(Bill_date,'DD-MM-YYYY') >= to_date('" & DateTimePicker1.Text & "','DD-MM-YYYY') and to_date(Bill_date,'DD-MM-YYYY')  <= to_date('" & DateTimePicker2.Text & "','DD-MM-YYYY')  order by bill_number"

                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "STOCK")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Rent"
                total = 0
                For i = 0 To DataGridView1.Rows.Count - 1
                    total = total + DataGridView1.Rows(i).Cells(5).Value
                Next
                Label5.Text = "Total Value of Bills is " & total

            ElseIf ComboBox1.Text = "Find receipt - stock" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select r_n  ""Receipt number"",r_r ""Receipt reference"",r_d ""Receipt date"", i_b ""Item number"",i_d ""Item description"",quan Quantity,i_p ""Unit price"",time_stamp ""Time stamp"" from stock where r_n ='" & txt1.Text & "'"
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "STOCK")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Find receipt - stock"
            ElseIf ComboBox1.Text = "Find receipt - fees" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select Bill_number ""Bill number"",Fees_type ""Fees type"", Fees_Desc ""Fees description"",Bill_Ref ""Bill reference"",Bill_date ""Bill date"",Bill_Amount ""Bill amount""
                                  from ops_fees
                                  where Bill_number='" & txt1.Text & "' "
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "STOCK")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Find receipt - fees"

            ElseIf ComboBox1.Text = "Find receipt - sales" Then
                'Fill datagridview with report data

                cmd.Connection = conn
                cmd.CommandText = "select system_date ""Actual date"",business_date ""POS date"",receipt_number ""Receipt number"",receipt_reference ""Receipt reference"",qty Quantity,total ""Total($)"",tender_type ""Payment type"",ops_type ""Operation type"",VAT VAT
                                  From receipt_numbers Where receipt_reference = '" & txt1.Text.Trim & "'"
                Dim oraada As New OracleDataAdapter(cmd)
                Dim ds As New DataSet
                oraada.Fill(ds, "STOCK")
                DataGridView1.DataSource = ds.Tables(0)
                cr = "Find receipt - sales"
                If DataGridView1.Rows.Count > 0 Then
                    If DataGridView1.Rows(0).Cells(3).Value = txt1.Text Then
                        PictureBox8.Visible = True
                    End If

                Else
                    PictureBox8.Visible = False
                End If

                If ComboBox1.Text <> "Find receipt - sales" Then
                    PictureBox8.Visible = False

                End If

            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click
        Try
            If cr = "Stock summery" Then
                Dim ds As New DataSet2
                Dim dt As New DataTable
                dt = ds.Tables("PacketDataTable2")
                For i = 0 To DataGridView1.Rows.Count - 1
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value)
                Next

                With store_summery.ReportViewer1.LocalReport
                    '.ReportPath = "C:\Users\User\source\repos\System Manager\System Manager\stock.rdlc"
                    .DataSources.Clear()
                    .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("myDataSet2", dt))
                End With
                store_summery.Show()
                store_summery.ReportViewer1.Refresh()



            ElseIf cr = "Stock by receipts" Then
                Dim ds As New DataSet3
                Dim dt As New DataTable
                dt = ds.Tables("PacketDataTable3")
                For i = 0 To DataGridView1.Rows.Count - 1
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value)
                Next
                With stock_by_receipt.ReportViewer1.LocalReport
                    '.ReportPath = "C:\Users\User\source\repos\System Manager\System Manager\stock.rdlc"
                    .DataSources.Clear()
                    .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("MyDataSet3", dt))
                End With
                stock_by_receipt.Show()
                stock_by_receipt.ReportViewer1.Refresh()

            ElseIf cr = "Expired items" Then
                Dim ds As New DataSet9
                Dim dt As New DataTable
                dt = ds.Tables("PacketDataTable9")
                For i = 0 To DataGridView1.Rows.Count - 1
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(8).Value)
                Next
                With expired_items.ReportViewer1.LocalReport
                    '.ReportPath = "C:\Users\User\source\repos\System Manager\System Manager\stock.rdlc"
                    .DataSources.Clear()
                    .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("MyDataSet9", dt))
                End With
                expired_items.Show()
                expired_items.ReportViewer1.Refresh()

            ElseIf cr = "Sales summery" Then
                Dim ds As New DataSet4
                Dim dt As New DataTable
                dt = ds.Tables("PacketDataTable4")
                For i = 0 To DataGridView1.Rows.Count - 1
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value)
                Next

                With sales_summery.ReportViewer1.LocalReport
                    '.ReportPath = "C:\Users\User\source\repos\System Manager\System Manager\stock.rdlc"
                    .DataSources.Clear()
                    .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("MyDataSet4", dt))
                End With
                sales_summery.Show()
                sales_summery.ReportViewer1.Refresh()


            ElseIf cr = "Sales by actual date" Then
                Dim ds As New DataSet5
                Dim dt As New DataTable
                dt = ds.Tables("PacketDataTable5")
                For i = 0 To DataGridView1.Rows.Count - 1
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value)
                Next

                With sales_by_receipt.ReportViewer1.LocalReport
                    '.ReportPath = "C:\Users\User\source\repos\System Manager\System Manager\stock.rdlc"
                    .DataSources.Clear()
                    .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("MyDataSet5", dt))
                End With
                sales_by_receipt.Show()
                sales_by_receipt.ReportViewer1.Refresh()


            ElseIf cr = "Sales by receipts detail" Then
                Dim ds As New DataSet6
                Dim dt As New DataTable
                dt = ds.Tables("PacketDataTable6")
                For i = 0 To DataGridView1.Rows.Count - 1
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value)
                Next

                With sales_by_detail.ReportViewer1.LocalReport
                    '.ReportPath = "C:\Users\User\source\repos\System Manager\System Manager\stock.rdlc"
                    .DataSources.Clear()
                    .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("MyDataSet6", dt))
                End With
                sales_by_detail.Show()
                sales_by_detail.ReportViewer1.Refresh()
            ElseIf cr = "Inventory" Then
                Dim ds As New DataSet7
                Dim dt As New DataTable
                dt = ds.Tables("PacketDataTable7")
                For i = 0 To DataGridView1.Rows.Count - 1
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value)
                Next

                With inventory.ReportViewer1.LocalReport
                    '.ReportPath = "C:\Users\User\source\repos\System Manager\System Manager\stock.rdlc"
                    .DataSources.Clear()
                    .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("MyDataSet7", dt))
                End With
                inventory.Show()
                inventory.ReportViewer1.Refresh()

            ElseIf cr = "Sales by business date" Then
                Dim ds As New DataSet5
                Dim dt As New DataTable
                dt = ds.Tables("PacketDataTable5")
                For i = 0 To DataGridView1.Rows.Count - 1
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value)
                Next

                With sales_by_receipt.ReportViewer1.LocalReport
                    '.ReportPath = "C:\Users\User\source\repos\System Manager\System Manager\stock.rdlc"
                    .DataSources.Clear()
                    .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("MyDataSet5", dt))
                End With
                sales_by_receipt.Show()
                sales_by_receipt.ReportViewer1.Refresh()
            ElseIf cr = "Merchandise" Then
                Dim ds As New DataSet8
                Dim dt As New DataTable
                dt = ds.Tables("PacketDataTable8")
                For i = 0 To DataGridView1.Rows.Count - 1
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value)
                Next

                With Merchandise.ReportViewer1.LocalReport
                    '.ReportPath = "C:\Users\User\source\repos\System Manager\System Manager\stock.rdlc"
                    .DataSources.Clear()
                    .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("MyDataSet8", dt))
                End With
                Merchandise.Show()
                Merchandise.ReportViewer1.Refresh()

            ElseIf cr = "Expiration report" Then
                expired_items.Show()
                expired_items.ReportViewer1.Refresh()


            ElseIf cr = "Search item" Then
                Dim ds As New DataSet7
                Dim dt As New DataTable
                dt = ds.Tables("PacketDataTable7")
                For i = 0 To DataGridView1.Rows.Count - 1
                    dt.Rows.Add(DataGridView1.Rows(i).Cells(0).Value, DataGridView1.Rows(i).Cells(1).Value, DataGridView1.Rows(i).Cells(2).Value, DataGridView1.Rows(i).Cells(3).Value, DataGridView1.Rows(i).Cells(4).Value, DataGridView1.Rows(i).Cells(5).Value, DataGridView1.Rows(i).Cells(6).Value, DataGridView1.Rows(i).Cells(7).Value, DataGridView1.Rows(i).Cells(8).Value)
                Next

                With inventory.ReportViewer1.LocalReport
                    '.ReportPath = "C:\Users\User\source\repos\System Manager\System Manager\stock.rdlc"
                    .DataSources.Clear()
                    .DataSources.Add(New Microsoft.Reporting.WinForms.ReportDataSource("MyDataSet7", dt))
                End With
                inventory.Show()
                inventory.ReportViewer1.Refresh()
            End If


        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txt1_TextChanged(sender As Object, e As EventArgs) Handles txt1.TextChanged
        PictureBox8.Visible = False
    End Sub

    Private Sub txt1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim f8 As New Font("Calibri", 11, FontStyle.Regular)
        Dim f9 As New Font("Calibri", 9, FontStyle.Regular)

        Dim leftmargin As Integer = PrintDocument1.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PrintDocument1.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PrintDocument1.DefaultPageSettings.PaperSize.Width

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
        Dim FilePath As String = "C:\POS\Receipts\" + txt1.Text.Trim + ".txt"
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
            Dim value3 As String = getTLV(3, DataGridView1.Rows(0).Cells(0).Value)
            Dim value4 As String = getTLV(4, DataGridView1.Rows(0).Cells(5).Value)
            Dim value5 As String = getTLV(5, DataGridView1.Rows(0).Cells(8).Value)
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
            Dim barcodeImage As Image

            barcodeImage = New Bitmap(writer.Write(txt1.Text.Trim))
            e.Graphics.DrawImage(barcodeImage, CInt((e.PageBounds.Width - 190) / 2), hight + 134)
        Catch ex As Exception

        End Try
    End Sub
    Private Function getTLV(tag As Integer, value As String) As String
        Return Chr(tag) & Chr(value.Length) & value
    End Function

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        Try

            AddHandler PrintDocument1.PrintPage, AddressOf Me.PrintImage
            PrintDocument1.Print()
            MMenu.WindowState = FormWindowState.Minimized
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PrintImage(ByVal sender As Object, ByVal ppea As PrintPageEventArgs)
        ' ppea.Graphics.DrawImage(Image.FromFile("D:\QR\QR.jpg"), ppea.Graphics.VisibleClipBounds)
        'ppea.HasMorePages = False
    End Sub
End Class