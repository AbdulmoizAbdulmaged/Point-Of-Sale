﻿'last update 12/10/2023 4:04PM
Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types

Public Class create_pos
    Dim conn As New OracleConnection
    Dim create_schema As New OracleCommand
    Dim grant_privilegies As New OracleCommand
    Dim grant1 As New OracleCommand
    Dim grant2 As New OracleCommand
    Dim grant3 As New OracleCommand
    Dim grant4 As New OracleCommand
    Dim grant5 As New OracleCommand

    Dim t0 As New OracleCommand
    Dim t00 As New OracleCommand
    Dim t1 As New OracleCommand
    Dim t2 As New OracleCommand
    Dim t3 As New OracleCommand
    Dim t4 As New OracleCommand
    Dim t5 As New OracleCommand
    Dim t6 As New OracleCommand
    Dim t7 As New OracleCommand
    Dim t8 As New OracleCommand
    Dim t9 As New OracleCommand
    Dim t10 As New OracleCommand
    Dim t11 As New OracleCommand
    Dim t12 As New OracleCommand
    Dim t13 As New OracleCommand
    Dim t14 As New OracleCommand
    Dim t15 As New OracleCommand
    Dim t16 As New OracleCommand
    Dim t17 As New OracleCommand
    Dim t18 As New OracleCommand
    Dim t19 As New OracleCommand
    Dim t20 As New OracleCommand
    Dim t21 As New OracleCommand
    Dim t22 As New OracleCommand
    Dim t23 As New OracleCommand
    Dim t24 As New OracleCommand
    Dim t25 As New OracleCommand
    Dim t26 As New OracleCommand
    Dim t27 As New OracleCommand
    Dim t28 As New OracleCommand
    Dim t29 As New OracleCommand
    Dim t30 As New OracleCommand
    Dim t31 As New OracleCommand
    Dim t32 As New OracleCommand
    Dim t33 As New OracleCommand
    Dim t34 As New OracleCommand
    Dim t35 As New OracleCommand
    Dim insert1 As New OracleCommand
    Dim insert2 As New OracleCommand
    Dim insert3 As New OracleCommand
    Dim insert4 As New OracleCommand
    Dim insert_lang As New OracleCommand
    Dim insert_printer As New OracleCommand
    Dim insert_color As New OracleCommand
    Dim system_id As String
    Dim system_password As String
    Dim system_hose As String
    Public store_code As String
    Dim system_sid As String









    Dim taskbar As Integer = 0




    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    

    End Sub

    Private Sub create_pos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            system_hose = dbinfo.MaskedTextBox3.Text
            system_id = dbinfo.MaskedTextBox1.Text
            system_password = dbinfo.MaskedTextBox2.Text
            system_sid = dbinfo.MaskedTextBox5.Text





        Catch ex As Exception

        End Try
    End Sub

 

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try


            ProgressBar1.Increment(taskbar)

            If ProgressBar1.Value = 43 Then
                Timer1.Stop()
                Threading.Thread.Sleep(3000)
                MsgBox("POS Server has been created successfully.", MsgBoxStyle.Information)
                Me.Close()


            End If




          
        Catch ex As Exception

        End Try
    End Sub

    

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim DQ As String = Chr(34)
            Dim PW As Double
            PW = Convert.ToDouble(store_code) + 111
            Dim password As String = PW.ToString

            store_code = DQ & store_code & DQ
           
            password = DQ & password & DQ






            Timer1.Start()

            Dim conn_string As String
            '"Data Source=" + ip + ":1521/orcl;User Id=" + u_id + ";password=" + pw + ";"
            conn_string = "Data Source=" + system_hose + ":1521/" + system_sid + ";User Id=" + system_id + ";password=" + system_password + ";"

            conn.ConnectionString = conn_string

            conn.Open()

            If conn.State = ConnectionState.Open Then
                Dim sql As String = "create user " + store_code + " identified by " + password
                create_schema.CommandText = sql
                create_schema.Connection = conn
                Dim create As Integer = create_schema.ExecuteNonQuery()
                create = create * -1



                grant_privilegies.CommandText = "grant all privileges to " + store_code
                grant_privilegies.Connection = conn
                Dim cmd1 As Integer = grant_privilegies.ExecuteNonQuery()
                cmd1 = cmd1 * -1
                grant1.CommandText = "grant create session,select any table to " + store_code
                grant1.Connection = conn
                Dim cmd2 As Integer = grant1.ExecuteNonQuery()
                cmd2 = cmd2 * -1
                grant2.CommandText = "grant select any table,insert any table,delete any table to " + store_code
                grant2.Connection = conn
                Dim cmd3 As Integer = grant2.ExecuteNonQuery()
                cmd3 = cmd3 * -1
                grant3.CommandText = "GRANT UNLIMITED TABLESPACE TO " + store_code
                grant3.Connection = conn
                Dim cmd4 As Integer = grant3.ExecuteNonQuery()
                cmd4 = cmd4 * -1
                grant4.CommandText = "grant connect to " + store_code
                grant4.Connection = conn
                Dim cmd5 As Integer = grant4.ExecuteNonQuery()
                cmd5 = cmd5 * -1
                grant5.CommandText = "grant resource to " + store_code
                grant5.Connection = conn
                Dim cmd6 As Integer = grant5.ExecuteNonQuery()
                cmd6 = cmd6 * -1

                taskbar = create + cmd1 + cmd2 + cmd3 + cmd4 + cmd5 + cmd6
                conn.Close()

            End If


            ''''''''''''''''''''''''''''''''''''''''''
            conn_string = "Data Source=" + system_hose + ":1521/" + system_sid + ";User Id=" + store_code + ";password=" + password + ";"
            conn.ConnectionString = conn_string
            conn.Open()

            If conn.State = ConnectionState.Open Then
                '
                t0.Connection = conn
                t0.CommandText = "CREATE TABLE auto_logon" &
                 "(" &
                  "USER_ID VARCHAR2(100 BYTE)," &
                  "USER_POSITION NUMBER(38,0)" &
                   ")"

                t0.Connection = conn
                t0.ExecuteNonQuery()


                t00.Connection = conn
                t00.CommandText = "CREATE TABLE AUTO_LOGON_SCANNED" &
                 "(" &
                  "ITEM_NUMBER    VARCHAR2(100 BYTE)," &
                  "ITEM_DESC     VARCHAR2(500 BYTE)," &
                  "PRICE    VARCHAR2(100 BYTE)," &
                  "QTY        VARCHAR2(100 BYTE)," &
                  "DISCOUNT      VARCHAR2(100 BYTE)" &
                   ")"

                t00.Connection = conn
                t00.ExecuteNonQuery()



                t1.Connection = conn
                t1.CommandText = "CREATE TABLE ALL_ITEMS" &
      "(" &
    "PROMO_ID VARCHAR2(50 BYTE)," &
    "BARCODE  VARCHAR2(200 BYTE)," &
    "PROMO_DATE DATE" &
      ")"

                Dim cmd5 As Integer = t1.ExecuteNonQuery()
                cmd5 = cmd5 * -1
                taskbar = taskbar + cmd5


                t2.Connection = conn
                t2.CommandText = "CREATE TABLE AS_ITM" &
  "(" &
    "ID_ITM          VARCHAR2(14 BYTE) NOT NULL ENABLE," &
    "ID_ITM_PDT    VARCHAR2(14 BYTE)," &
    "FL_ITM_DSC      CHAR(1 BYTE)," &
    "FL_ITM_DSC_DMG  CHAR(1 BYTE)," &
    "FL_ADT_ITM_PRC  CHAR(1 BYTE)," &
    "FL_ITM_SZ_REQ   CHAR(1 BYTE) DEFAULT '0'," &
    "ID_DPT_POS      VARCHAR2(14 BYTE)," &
    "FL_AZN_FR_SLS   CHAR(1 BYTE)," &
    "LU_EXM_TX       VARCHAR2(20 BYTE)," &
    "LU_ITM_USG      VARCHAR2(20 BYTE)," &
    "NM_ITM          VARCHAR2(120 BYTE)," &
    "DE_ITM         VARCHAR2(255 BYTE)," &
    "DE_ITM_SHRT     VARCHAR2(120 BYTE)," &
    "TY_ITM          VARCHAR2(20 BYTE)," &
    "LU_KT_ST       VARCHAR2(20 BYTE) DEFAULT '0'," &
    "FL_ITM_SBST_IDN CHAR(1 BYTE) DEFAULT '0'," &
    "LU_CLN_ORD      VARCHAR2(20 BYTE)," &
    "ID_STRC_MR      NUMBER(*,0)," &
    "ID_LN_PRC       NUMBER(*,0)," &
    "NM_BRN          VARCHAR2(40 BYTE)," &
    "LU_SN           VARCHAR2(20 BYTE)," &
    "FY              VARCHAR2(4 BYTE)," &
    "LU_HRC_MR_LV    VARCHAR2(4 BYTE)," &
    "LU_SBSN         VARCHAR2(20 BYTE)," &
    "ID_GP_TX        NUMBER(*,0) DEFAULT 0," &
    "FL_ACTVN_RQ     CHAR(1 BYTE)," &
    "FL_ITM_RGSTRY   CHAR(1 BYTE)," &
    "ID_STRC_MR_CD0  VARCHAR2(10 BYTE)," &
    "ID_STRC_MR_CD1  VARCHAR2(10 BYTE)," &
    "ID_STRC_MR_CD2  VARCHAR2(10 BYTE)," &
    "ID_STRC_MR_CD3  VARCHAR2(10 BYTE)," &
    "ID_STRC_MR_CD4  VARCHAR2(10 BYTE)," &
    "ID_STRC_MR_CD5  VARCHAR2(10 BYTE)," &
    "ID_STRC_MR_CD6  VARCHAR2(10 BYTE)," &
    "ID_STRC_MR_CD7  VARCHAR2(10 BYTE)," &
    "ID_STRC_MR_CD8  VARCHAR2(10 BYTE)," &
    "ID_STRC_MR_CD9  VARCHAR2(10 BYTE)," &
    "ID_MRHRC_GP     VARCHAR2(14 BYTE) DEFAULT '0'," &
    "ID_MF           NUMBER(*,0)," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9) DEFAULT SYSDATE" &
  ")"
                Dim cmd6 As Integer = t2.ExecuteNonQuery
                cmd6 = cmd6 * -1
                taskbar = taskbar + cmd6


                t3.Connection = conn
                t3.CommandText = "CREATE TABLE  AS_ITM_RTL_STR" &
  "(" &
    "ID_STR_RT       VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "ID_ITM           VARCHAR2(14 BYTE) NOT NULL ENABLE," &
    "FL_STK_UPT_ON_HD CHAR(1 BYTE)," &
    "DC_ITM_SLS DATE," &
    "SC_ITM_SLS         VARCHAR2(2 BYTE)," &
    "RP_PRC_CMPR_AT_SLS  NUMBER(8,2)," &
    "DC_PRC_MF_REC_RT DATE," &
    "RP_PRC_MF_REC_RT  NUMBER(8,2)," &
    "ID_GP_TX         NUMBER(*,0) DEFAULT 0," &
    "DC_PRC_SLS_EP_CRT DATE," &
    "DC_PRC_SLS_EF_CRT  DATE," &
    "FL_PRC_RT_PNT_ALW  CHAR(1 BYTE)," &
    "TY_PRC_RT          VARCHAR2(2 BYTE)," &
    "RP_SLS_CRT        NUMBER(8,2)," &
    "DC_PRC_EF_PRN_RT  DATE," &
    "QU_MKD_PR_PRC_PR   NUMBER(7,3)," &
    "FL_MKD_ORGL_PRC_PR  CHAR(1 BYTE)," &
    "RP_PR_SLS          NUMBER(8,2)," &
    "SC_ITM             VARCHAR2(2 BYTE)," &
    "IDN_SLS_AG_RST     NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "ID_TMPLT_LB        VARCHAR2(8 BYTE)," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9) DEFAULT SYSDATE" &
")"
                Dim cmd7 As Integer = t3.ExecuteNonQuery
                cmd7 = cmd7 * -1
                taskbar = taskbar + cmd7


                t4.Connection = conn
                t4.CommandText = "CREATE TABLE AS_ITM_STK" &
  "(" &
    "ID_ITM             VARCHAR2(14 BYTE) NOT NULL ENABLE," &
    "LU_UOM_SLS          VARCHAR2(20 BYTE) NOT NULL ENABLE," &
    "ED_CLR            VARCHAR2(20 BYTE)," &
    "ED_SZ              VARCHAR2(10 BYTE)," &
    "LU_STYL            VARCHAR2(4 BYTE)," &
    "ID_STR_RT           VARCHAR2(5 BYTE) DEFAULT '0'," &
    "QL_HT_PCKG_CNS      NUMBER(9,2) DEFAULT 0," &
    "ID_SPR             VARCHAR2(20 BYTE) DEFAULT '0'," &
    "NM_NMB_SRZ_ITM_MDL  VARCHAR2(40 BYTE)," &
    "QL_UOM_WD_PCKG_CNS  NUMBER(9,2) DEFAULT 0," &
    "TY_WST_BLK_SLS      VARCHAR2(20 BYTE)," &
    "CY_MDL_SRZ_ITM      VARCHAR2(4 BYTE)," &
    "QU_CPC_HLD           NUMBER(9,2) DEFAULT 0," &
    "LU_UOM              VARCHAR2(2 BYTE)," &
    "QW_ITM_PCK         NUMBER(9,2) DEFAULT 0," &
    "TY_UN_DPLY         VARCHAR2(20 BYTE)," &
    "QU_CB_PCK_ITM       NUMBER(9,2) DEFAULT 0," &
    "QL_PCKG_CNS         NUMBER(9,2) DEFAULT 0," &
    "DE_CLR_MF_SRZ_ITM  VARCHAR2(255 BYTE)," &
    "TY_ITM_STK          VARCHAR2(20 BYTE)," &
    "PE_WST_BLK_SLS     NUMBER(5,2) DEFAULT 0," &
    "LU_UOM_PCKG_CNS_DMN  VARCHAR2(20 BYTE)," &
    "LU_SYS_PRMRY_MS    VARCHAR2(20 BYTE)," &
    "DE_SZ_MF_SRZ_ITM   VARCHAR2(255 BYTE)," &
    "QU_UN_PCK_ITM       NUMBER(9,2) DEFAULT 0," &
    "DC_UN_DPLY_ST_UP DATE," &
    "LU_WRTY_MF_SRZ_ITM  VARCHAR2(20 BYTE)," &
    "QW_WT_PCKG_CNS     NUMBER(9,2) DEFAULT 0," &
    "DC_UN_DPLY_TK_DWN  DATE," &
    "DE_FBRC             VARCHAR2(255 BYTE)," &
    "DP_UN_DPLY        VARCHAR2(20 BYTE)," &
    "LU_CNT_SLS_WT_UN   VARCHAR2(20 BYTE)," &
    "LU_WRTY_STR_SRZ    VARCHAR2(20 BYTE)," &
    "LU_UOM_WT_PCKG_CNS VARCHAR2(20 BYTE)," &
    "TY_PKP_CT_STK_ITM VARCHAR2(20 BYTE)," &
    "LU_UOM_SZ_PCKG_CNS VARCHAR2(20 BYTE)," &
    "DE_SLH             VARCHAR2(255 BYTE)," &
    "FL_VLD_SRZ_ITM    CHAR(1 BYTE)," &
    "FA_PRC_UN_STK_ITM  NUMBER(9,2) DEFAULT 0," &
    "FL_DSD_AZN        CHAR(1 BYTE)," &
    "DI_PRD_SH_LF       NUMBER(3,0) DEFAULT 0," &
    "DI_LF_SH           NUMBER(3,0) DEFAULT 0," &
    "ID_BRKR            NUMBER(*,0) DEFAULT 0," &
    "DC_AVLB_FR_SLS DATE," &
    "TY_ITM_STPL_PRSH   VARCHAR2(20 BYTE)," &
    "TY_ENV_STK_ITM     VARCHAR2(20 BYTE)," &
    "NM_LCN_ASL          VARCHAR2(255 BYTE)," &
    "TY_SCTY_RQ         VARCHAR2(20 BYTE)," &
    "NM_LCN_SH          VARCHAR2(255 BYTE)," &
    "NM_LCN_SID         VARCHAR2(255 BYTE)," &
    "TY_MTR_HZ_STK_ITM  VARCHAR2(20 BYTE)," &
    "QU_FCG             NUMBER(9,2) DEFAULT 0," &
    "CP_UN_SL_LS_RCV_BS NUMBER(7,3) DEFAULT 0," &
    "CP_CST_NT_LS_RCV   NUMBER(7,3) DEFAULT 0," &
    "CP_UN_SL_LND        NUMBER(7,3) DEFAULT 0," &
    "DC_CST_EST_LS_RCV DATE," &
    "FL_SHRK_SH_ITM CHAR(1 BYTE)," &
    "FL_SWL_SH_ITM  CHAR(1 BYTE)," &
    "FL_RQ_UN_PRC   CHAR(1 BYTE)," &
    "FL_FE_RSTK     CHAR(1 BYTE)," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9) DEFAULT SYSDATE" &
 " )"
                Dim cmd8 As Integer = t4.ExecuteNonQuery
                cmd8 = cmd8 * -1
                taskbar = taskbar + cmd8

                t5.Connection = conn
                t5.CommandText = "CREATE TABLE CO_BRK_SPR_ITM_BS" &
 "(" &
    "ID_SPR           VARCHAR2(20 BYTE) NOT NULL ENABLE," &
    "ID_ITM_SPR        VARCHAR2(20 BYTE) NOT NULL ENABLE," &
    "TY_UN_CST         VARCHAR2(3 BYTE) NOT NULL ENABLE," &
    "QU_PNT_UND_BRK   NUMBER(9,2) NOT NULL ENABLE," &
    "CP_PNT_BRK_BS_CST NUMBER(13,4)," &
    "TS_CRT_RCRD  TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9)" &
  ")"
                Dim cmd9 As Integer = t5.ExecuteNonQuery
                cmd9 = cmd9 * -1
                taskbar = taskbar + cmd9


                t6.Connection = conn
                t6.CommandText = "CREATE TABLE CO_EV" &
  "(" &
    "ID_EV        NUMBER(*,0) NOT NULL ENABLE," &
    "ID_STR_RT   VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "NM_EV       VARCHAR2(160 BYTE)," &
    "DE_EV        VARCHAR2(640 BYTE)," &
    "TY_EV       VARCHAR2(20 BYTE)," &
    "SC_EV        VARCHAR2(20 BYTE)," &
    "CC_EV        VARCHAR2(20 BYTE)," &
    "NM_EV_OWNR   VARCHAR2(40 BYTE)," &
    "DC_DY_BSN_SS VARCHAR2(10 BYTE)," &
    "DC_DY_BSN_SE VARCHAR2(10 BYTE)," &
    "DC_DY_BSN_AS VARCHAR2(10 BYTE)," &
    "DC_DY_BSN_AE VARCHAR2(10 BYTE)," &
    "TS_EV_PL_EF TIMESTAMP (9)," &
    "TS_EV_PL_EP TIMESTAMP (9)," &
    "TS_EV_ACT_EF TIMESTAMP (9)," &
    "TS_EV_ACT_EP TIMESTAMP (9)," &
    "ID_EV_EXT VARCHAR2(20 BYTE)," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9)" &
  ")"
                Dim cmd10 As Integer = t6.ExecuteNonQuery
                cmd10 = cmd10 * -1

                taskbar = taskbar + cmd10


                t7.Connection = conn
                t7.CommandText = "CREATE TABLE CO_EV_MNT" &
  "(" &
    "ID_EV     NUMBER(*,0) NOT NULL ENABLE," &
    "ID_STR_RT VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "NM_EV_MNT VARCHAR2(40 BYTE)," &
    "DE_EV_MNT VARCHAR2(255 BYTE)," &
    "TS_EV_MNT_EF TIMESTAMP (9)," &
    "TS_EV_MNT_EP TIMESTAMP (9)," &
    "TY_EV_MNT     VARCHAR2(20 BYTE)," &
    "SC_EV_MNT    VARCHAR2(20 BYTE)," &
    "RC_EV_MNT     VARCHAR2(20 BYTE)," &
    "TY_EV_MNT_ORG VARCHAR2(20 BYTE)," &
    "ID_EM         VARCHAR2(10 BYTE)," &
    "ID_CMP        NUMBER(*,0)," &
    "TS_EV_MNT_CRT TIMESTAMP (9)," &
    "TS_EV_MNT_APLY TIMESTAMP (9)," &
    "ID_JOB_ST    VARCHAR2(12 BYTE)," &
    "ID_JOB_END   VARCHAR2(12 BYTE)," &
    "SC_EV_MNT_EF VARCHAR2(20 BYTE)," &
    "SC_EV_MNT_EP VARCHAR2(20 BYTE)," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9)" &
  ")"

                Dim cmd11 As Integer = t7.ExecuteNonQuery
                cmd11 = cmd11 * -1
                taskbar = taskbar + cmd11

                t8.Connection = conn
                t8.CommandText = "CREATE TABLE CO_MDFR_RTL_PRC" &
  "(" &
    "ID_STR_RT          VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "ID_WS               VARCHAR2(3 BYTE) NOT NULL ENABLE," &
    "DC_DY_BSN           VARCHAR2(10 BYTE) NOT NULL ENABLE," &
    "AI_TRN             NUMBER(*,0) NOT NULL ENABLE," &
    "AI_LN_ITM          NUMBER(*,0) NOT NULL ENABLE," &
    "AI_MDFR_RT_PRC     NUMBER(*,0) NOT NULL ENABLE," &
    "RC_MDFR_RT_PRC      VARCHAR2(20 BYTE)," &
    "PE_MDFR_RT_PRC      NUMBER(5,2) DEFAULT 0," &
    "MO_MDFR_RT_PRC      NUMBER(16,5) DEFAULT 0," &
    "DP_LDG_STK_MDFR     VARCHAR2(20 BYTE)," &
    "ID_RU_MDFR          NUMBER(*,0) DEFAULT 1," &
    "CD_MTH_PRDV         NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "CD_BAS_PRDV         NUMBER(*,0) DEFAULT 0," &
    "CD_TY_PRDV         NUMBER(*,0) DEFAULT 0," &
    "FL_PCD_DL_ADVN_APLY CHAR(1 BYTE) DEFAULT '0' NOT NULL ENABLE," &
    "FL_DSC_ADVN_PRC    CHAR(1 BYTE) DEFAULT '0' NOT NULL ENABLE," &
    "ID_DSC_REF         VARCHAR2(255 BYTE)," &
    "CO_ID_DSC_REF       VARCHAR2(2 BYTE) DEFAULT 'UN' NOT NULL ENABLE," &
    "ID_EM_AZN_OVRD     VARCHAR2(10 BYTE)," &
    "CD_MTH_ENR_OVRD     NUMBER(*,0) DEFAULT -1 NOT NULL ENABLE," &
    "ID_DSC_EM           VARCHAR2(10 BYTE)," &
    "FL_DSC_DMG          CHAR(1 BYTE) DEFAULT '0' NOT NULL ENABLE," &
    "ID_PRM             NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "ID_PRM_CMP          NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "ID_PRM_CMP_DTL     NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9)" &
  ")"
                Dim cmd12 As Integer = t8.ExecuteNonQuery
                cmd12 = cmd12 * -1
                taskbar = taskbar + cmd12

                t9.Connection = conn
                t9.CommandText = "CREATE TABLE DAILY_OPS" &
  "(" &
    "USER_ID     VARCHAR2(50 BYTE)," &
    "FLOAT_AMOUNT  VARCHAR2(20 BYTE)," &
    "START_DATE   VARCHAR2(50 BYTE)," &
    "END_DATE      VARCHAR2(50 BYTE)," &
    "STATUS_FLAG   VARCHAR2(20 BYTE)," &
    "BUSINESS_DATE VARCHAR2(50 BYTE)" &
  ")"
                Dim cmd13 As Integer = t9.ExecuteNonQuery
                cmd13 = cmd13 * -1
                taskbar = taskbar + cmd13

                t10.Connection = conn
                t10.CommandText = "CREATE TABLE ID_IDN_PS" &
  "(" &
    "ID_STR_RT          VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "ID_ITM_POS         VARCHAR2(14 BYTE) NOT NULL ENABLE," &
    "ID_ITM            VARCHAR2(14 BYTE) NOT NULL ENABLE," &
    "DE_ITM_POS         VARCHAR2(255 BYTE)," &
    "RP_SLS_POS_CRT     NUMBER(8,2) DEFAULT 0," &
    "FL_PNT_FQ_SHPR_EL  CHAR(1 BYTE)," &
    "ID_MF             NUMBER(*,0) DEFAULT 0," &
    "ID_ITM_MF_UPC      VARCHAR2(14 BYTE)," &
    "ID_AGNT_RTN       NUMBER(*,0) DEFAULT 0," &
    "FL_DSC_AF_DSC_ALW  CHAR(1 BYTE)," &
    "LU_VT_PS_CPN       NUMBER(2,2) DEFAULT 0," &
    "DT_END_PS_CPN_OFR  VARCHAR2(4 BYTE)," &
    "QU_UN_BLK_MNM     NUMBER(5,2) DEFAULT 1," &
    "QU_UN_BLK_MXM     NUMBER(5,2) DEFAULT -1," &
    "FL_DSC_MRK_BSK_ALW CHAR(1 BYTE)," &
    "FL_DSC_CT_ACNT_ALW  CHAR(1 BYTE)," &
    "FL_DSC_EM_ALW      CHAR(1 BYTE) DEFAULT '1'," &
    "FL_CPN_ALW_MULTY   CHAR(1 BYTE) DEFAULT '0'," &
    "FL_FD_STP_ALW      CHAR(1 BYTE)," &
    "FL_CPN_ELNTC      CHAR(1 BYTE) DEFAULT '0', " &
    "FL_CPN_RST        CHAR(1 BYTE) DEFAULT '0'," &
    "FL_ENTR_PRC_RQ    CHAR(1 BYTE), " &
    "FL_QR_ENR_WT      CHAR(1 BYTE), " &
    "FL_KY_PRH_QTY    CHAR(1 BYTE)," &
    "FL_RTN_PRH        CHAR(1 BYTE)," &
    "FL_ITM_GWY         CHAR(1 BYTE)," &
    "FL_ITM_WIC        CHAR(1 BYTE), " &
    "FL_PRC_VS_VR      CHAR(1 BYTE), " &
    "FL_KY_PRH_RPT     CHAR(1 BYTE), " &
    "FL_SPO_ITM         CHAR(1 BYTE) DEFAULT '0'," &
    "QU_PNT_FQ_SHPR    NUMBER(9,2) DEFAULT 0, " &
    "LU_GP_TND_RST     VARCHAR2(20 BYTE)," &
    "FC_FMY_MF        VARCHAR2(3 BYTE)," &
    "FL_MDFR_RT_PRC   CHAR(1 BYTE)," &
    "ID_PRM           NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "ID_PRM_CMP        NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "ID_PRM_CMP_DTL    NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9) DEFAULT SYSDATE" &
  ")"
                Dim cmd14 As Integer = t10.ExecuteNonQuery
                cmd14 = cmd14 * -1
                taskbar = taskbar + cmd14

                t11.Connection = conn
                t11.CommandText = "CREATE TABLE INVENTORY_TEMP" &
  "(" &
    "STORE          VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "CASHIER         VARCHAR2(10 BYTE)," &
    "SALE_DT         VARCHAR2(20 BYTE)," &
    "BARCODE        VARCHAR2(14 BYTE)," &
    "SALE_TYPE      VARCHAR2(7 BYTE)," &
    "QTY            NUMBER(9,2)," &
    "RETN_FLAG        CHAR(1 BYTE)," &
    "AMOUNT          NUMBER(13,2)," &
    "NET_AMOUNT      NUMBER(13,2)," &
    "MANUAL_DISCOUNT NUMBER(14,3)," &
    "TOTAL_DISCOUNT  NUMBER," &
    "MDFR_PROMO_CODE NUMBER(*,0)," &
    "MDFR_COMP_CODE  NUMBER(*,0)," &
    "MDFR_DISCOUNT   NUMBER," &
    "LTM_PROMO_CODE  NUMBER(*,0)," &
    "LTM_PROMO_COMP  NUMBER(*,0)," &
    "LTM_DISCOUNT    NUMBER," &
    "MO_PRN_PRC      NUMBER(8,2)," &
    "AI_LN_ITM       NUMBER(*,0) NOT NULL ENABLE" &
 " )"
                Dim cmd15 As Integer = t11.ExecuteNonQuery
                cmd15 = cmd15 * -1
                taskbar = taskbar + cmd15

                t12.Connection = conn
                t12.CommandText = "CREATE TABLE LOG_IN" &
  "(" &
    "USER_NAME VARCHAR2(50 BYTE)," &
    "USER_ID  VARCHAR2(30 BYTE)," &
    "LOG_TIME DATE" &
  ")"
                Dim cmd16 As Integer = t12.ExecuteNonQuery
                cmd16 = cmd16 * -1
                taskbar = taskbar + cmd16
                t13.Connection = conn
                t13.CommandText = "CREATE TABLE MA_ITM_PRN_PRC_ITM" &
  "(" &
    "ID_EV       NUMBER(*,0) NOT NULL ENABLE," &
    "ID_STR_RT   VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "ID_ITM      VARCHAR2(14 BYTE) NOT NULL ENABLE," &
    "ID_TMPLT_LB VARCHAR2(8 BYTE)," &
    "MO_OVRD_PRC NUMBER(7,2)," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9)" &
  ")"
                Dim cmd17 As Integer = t13.ExecuteNonQuery
                cmd17 = cmd17 * -1

                t14.Connection = conn
                t14.CommandText = "CREATE TABLE MA_PRC_ITM" &
  "(" &
    "ID_EV        NUMBER(*,0) NOT NULL ENABLE," &
    "ID_STR_RT    VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "TY_PRC_MNT   VARCHAR2(20 BYTE)," &
    "UN_PRI_EV    NUMBER(*,0)," &
    "UN_DG_LS_PRC CHAR(1 BYTE)," &
    "ID_TMPLT_LB  VARCHAR2(8 BYTE)," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9)" &
 " )"
                Dim cmd18 As Integer = t14.ExecuteNonQuery
                cmd18 = cmd18 * -1

                t15.Connection = conn
                t15.CommandText = "CREATE TABLE OPS" &
  "(" &
    "USER_NAME VARCHAR2(50 BYTE)," &
    "USER_ID   VARCHAR2(30 BYTE)," &
    "OPS_TYPE  NUMBER," &
    "ENTRY_TIME DATE" &
  ")"
                Dim cmd19 As Integer = t15.ExecuteNonQuery
                cmd19 = cmd19 * -1

                t16.Connection = conn
                t16.CommandText = "CREATE TABLE ORIGINAL_PRICES" &
  "(" &
    "BARCODE VARCHAR2(50 BYTE)," &
    "I_D    VARCHAR2(500)," &
    "S_P     VARCHAR2(50 BYTE)" &
 " )"
                Dim cmd20 As Integer = t16.ExecuteNonQuery
                cmd20 = cmd20 * -1

                t17.Connection = conn
                t17.CommandText = "CREATE TABLE PA_EM" &
  "(" &
    "ID_EM           VARCHAR2(10 BYTE) NOT NULL ENABLE," &
    "ID_PRTY       NUMBER(*,0) DEFAULT 0," &
    "ID_LOGIN         VARCHAR2(10 BYTE)," &
    "ID_ALT          VARCHAR2(10 BYTE)," &
    "PW_ACS_EM       VARCHAR2(40 BYTE)," &
    "NM_EM           VARCHAR2(150 BYTE)," &
    "LN_EM           VARCHAR2(50 BYTE)," &
    "FN_EM           VARCHAR2(50 BYTE)," &
    "MD_EM          VARCHAR2(50 BYTE), " &
    "ROLE_EM        VARCHAR2(50 BYTE), " &
    "UN_NMB_SCL_SCTY CHAR(9 BYTE)," &
    "SC_EM           VARCHAR2(20 BYTE)," &
    "ID_GP_WRK       NUMBER(*,0) NOT NULL ENABLE," &
    "LCL             VARCHAR2(10 BYTE)," &
    "NUMB_DYS_VLD    NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "DC_EXP_TMP  DATE DEFAULT NULL," &
    "TYPE_EMP     NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "ID_STR_RT    VARCHAR2(5 BYTE)," &
    "FL_PW_NW_REQ CHAR(1 BYTE) DEFAULT '0' NOT NULL ENABLE," &
    "TS_CRT_PW  TIMESTAMP (9)," &
    "NUMB_FLD_PW NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE" &
  ")"

                Dim cmd21 As Integer = t17.ExecuteNonQuery
                cmd21 = cmd21 * -1

                t18.Connection = conn
                t18.CommandText = "CREATE TABLE POLE_DISPLAY" &
  "(" &
    "COM   VARCHAR2(20 BYTE)," &
    "STATUS VARCHAR2(20 BYTE)" &
  ")"
                Dim cmd22 As Integer = t18.ExecuteNonQuery
                cmd22 = cmd22 * -1

                t19.Connection = conn
                t19.CommandText = "CREATE TABLE POS_INFO
   (	COMP_NAME VARCHAR2(1200 BYTE), 
	STORE_NAME VARCHAR2(1200 BYTE), 
	ADDRESS VARCHAR2(1200 BYTE), 
	TELEPHONE VARCHAR2(500 BYTE), 
	POLICY VARCHAR2(2000 BYTE), 
	VAT_LIMIT VARCHAR2(20 BYTE), 
	VAT VARCHAR2(20 BYTE), 
	CF NUMBER,
    VAT_NUMBER VARCHAR2(100 BYTE)
   ) "

                Dim cmd23 As Integer = t19.ExecuteNonQuery
                cmd23 = cmd23 * -1

                t20.Connection = conn
                t20.CommandText = "CREATE TABLE PRICES_HISTORY" &
  "(" &
    "BARCODE        VARCHAR2(30 BYTE)," &
    "DESCRIPTION    VARCHAR2(500)," &
    "SELL_PRICE     NUMBER," &
    "ENTRY_TIME     VARCHAR2(50 BYTE)," &
    "CREATED_BY     VARCHAR2(50 BYTE)," &
    "OPERATION_TYPE VARCHAR2(20 BYTE)" &
  ")"
                Dim cmd24 As Integer = t20.ExecuteNonQuery
                cmd24 = cmd24 * -1

                t21.Connection = conn
                t21.CommandText = "CREATE TABLE R_I" &
  "(" &
    "R_ID NUMBER," &
    "R_T DATE DEFAULT SYSDATE," &
    "UNIQUE (R_ID) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" &
 " )"
                Dim cmd25 As Integer = t21.ExecuteNonQuery
                cmd25 = cmd25 * -1

                t22.Connection = conn
                t22.CommandText = "CREATE TABLE RECEIPT_NUMBERS" &
  "(" &
    "SYSTEM_DATE      VARCHAR2(100 BYTE)," &
    "RECEIPT_NUMBER    NUMBER," &
    "RECEIPT_REFERENCE VARCHAR2(100 BYTE)," &
    "QTY              VARCHAR2(20 BYTE)," &
    "TOTAL            VARCHAR2(20 BYTE), " &
     "VAT           VARCHAR2(20 BYTE), " &
    "EMP               VARCHAR2(20 BYTE)," &
    "TENDER_TYPE        VARCHAR2(20 BYTE), " &
    "OPS_TYPE         VARCHAR2(20 BYTE), " &
    "OPS_TIME        VARCHAR2(200 BYTE)," &
    "BUSINESS_DATE    VARCHAR2(200 BYTE)," &
    "CRDT_INFO         VARCHAR2(1000 BYTE)" &
  ")"
                Dim cmd26 As Integer = t22.ExecuteNonQuery
                cmd26 = cmd26 * -1

                t23.Connection = conn
                t23.CommandText = "CREATE TABLE STOCK
  (	R_N VARCHAR2(50 BYTE) , 
	R_R VARCHAR2(50 BYTE) , 
	R_D VARCHAR2(50 BYTE) , 
	I_B VARCHAR2(50 BYTE) , 
	I_D VARCHAR2(250 BYTE) , 
	I_P VARCHAR2(50 BYTE) , 
	QUAN VARCHAR2(50 BYTE) , 
    ex_d VARCHAR2(50 BYTE) , 
	TIME_STAMP DATE
   )"

                Dim cmd27 As Integer = t23.ExecuteNonQuery
                cmd27 = cmd27 * -1

                t24.Connection = conn
                t24.CommandText = "CREATE TABLE STOCK_HISTORY
   (	RN VARCHAR2(250 BYTE), 
	RR VARCHAR2(250 BYTE), 
	IB VARCHAR2(250 BYTE), 
	I_D VARCHAR2(250 BYTE), 
	IP VARCHAR2(250 BYTE), 
	QUAN VARCHAR2(250 BYTE), 
	ENTRY_TIME VARCHAR2(250 BYTE), 
	CREATED_BY VARCHAR2(250 BYTE), 
	OPS_TYPE VARCHAR2(250 BYTE)
   )"
                Dim cmd28 As Integer = t24.ExecuteNonQuery
                cmd28 = cmd28 * -1


                t25.Connection = conn
                t25.CommandText = "CREATE TABLE TR_CHN_PRN_PRC" &
  "(" &
    "ID_EV            NUMBER(*,0) NOT NULL ENABLE," &
    "ID_STR_RT         VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "MO_CHN_PRN_UN_PRC VARCHAR2(20 BYTE)," &
    "TY_CHN_PRN_UN_PRC VARCHAR2(20 BYTE)," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9)" &
  ")"
                Dim cmd29 As Integer = t25.ExecuteNonQuery
                cmd29 = cmd29 * -1

                t26.Connection = conn
                t26.CommandText = "CREATE TABLE TR_LTM_DSC" &
  "(" &
    "ID_STR_RT       VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "ID_WS         VARCHAR2(3 BYTE) NOT NULL ENABLE," &
    "DC_DY_BSN      VARCHAR2(10 BYTE) NOT NULL ENABLE," &
    "AI_TRN         NUMBER(*,0) NOT NULL ENABLE," &
    "AI_LN_ITM       NUMBER(*,0) NOT NULL ENABLE," &
    "TY_DSC          VARCHAR2(20 BYTE) NOT NULL ENABLE," &
    "RC_DSC          VARCHAR2(20 BYTE) NOT NULL ENABLE," &
    "MO_DSC          NUMBER(14,3) DEFAULT 0," &
    "PE_DSC          NUMBER(5,2) DEFAULT 0," &
    "FL_DSC_ENA      CHAR(1 BYTE) NOT NULL ENABLE," &
    "LU_BAS_ASGN     VARCHAR2(2 BYTE) NOT NULL ENABLE," &
    "FL_DL_ADVN_APLY CHAR(1 BYTE) DEFAULT '0' NOT NULL ENABLE," &
    "ID_RU_DSC      NUMBER(*,0)," &
    "ID_DSC_REF      VARCHAR2(255 BYTE)," &
    "CO_ID_DSC_REF  VARCHAR2(2 BYTE) DEFAULT 'UN' NOT NULL ENABLE," &
    "ID_DSC_EM      VARCHAR2(10 BYTE)," &
    "ID_PRM         NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "ID_PRM_CMP      NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "ID_PRM_CMP_DTL  NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9)" &
 " )"
                Dim cmd30 As Integer = t26.ExecuteNonQuery
                cmd30 = cmd30 * -1

                t27.Connection = conn
                t27.CommandText = "CREATE TABLE TR_LTM_PRM" &
  "(" &
    "ID_STR_RT      VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "ID_WS         VARCHAR2(3 BYTE) NOT NULL ENABLE," &
    "DC_DY_BSN      VARCHAR2(10 BYTE) NOT NULL ENABLE," &
    "AI_TRN         NUMBER(*,0) NOT NULL ENABLE," &
    "AI_LN_ITM      NUMBER(*,0) NOT NULL ENABLE," &
    "AI_LTM_PRM     NUMBER(*,0) NOT NULL ENABLE," &
    "ID_PRM         NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "ID_PRM_CMP     NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "ID_PRM_CMP_DTL NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," &
    "MO_MDFR_RT_PRC NUMBER(16,5) DEFAULT 0 NOT NULL ENABLE," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9)" &
 " )"

                Dim cmd31 As Integer = t27.ExecuteNonQuery
                cmd31 = cmd31 * -1

                t28.Connection = conn
                t28.CommandText = "CREATE TABLE TR_LTM_RTL_TRN" &
  "(" &
    "ID_STR_RT VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "ID_WS     VARCHAR2(3 BYTE) NOT NULL ENABLE," &
    "DC_DY_BSN VARCHAR2(10 BYTE) NOT NULL ENABLE," &
    "AI_TRN    NUMBER(*,0) NOT NULL ENABLE," &
    "AI_LN_ITM NUMBER(*,0) NOT NULL ENABLE," &
    "TY_LN_ITM VARCHAR2(20 BYTE)," &
    "TS_LN_ITM_BGN TIMESTAMP (9)," &
    "TS_LN_ITM_END TIMESTAMP (9)," &
    "FL_VD_LN_ITM CHAR(1 BYTE) DEFAULT '0'," &
    "AI_LN_ITM_VD NUMBER(*,0) DEFAULT -1," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9)" &
 " )"
                Dim cmd32 As Integer = t28.ExecuteNonQuery
                cmd32 = cmd32 * -1

                t29.Connection = conn
                t29.CommandText = "CREATE TABLE TR_LTM_SLS_RTN" &
  "(" &
    "ID_STR_RT             VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "ID_WS                VARCHAR2(3 BYTE) NOT NULL ENABLE," &
    "DC_DY_BSN            VARCHAR2(10 BYTE) NOT NULL ENABLE," &
    "AI_TRN                NUMBER(*,0) NOT NULL ENABLE," &
    "AI_LN_ITM            NUMBER(*,0) NOT NULL ENABLE," &
    "ID_REGISTRY           VARCHAR2(14 BYTE)," &
    "ID_GP_TX              NUMBER(*,0) DEFAULT 0," &
    "ID_DPT_POS            VARCHAR2(14 BYTE)," &
    "ID_ITM_POS            VARCHAR2(14 BYTE)," &
    "ID_ITM                VARCHAR2(14 BYTE)," &
    "QU_ITM_LM_RTN_SLS    NUMBER(9,2) DEFAULT 0," &
    "MO_EXTN_LN_ITM_RTN    NUMBER(13,2) DEFAULT 0," &
    "MO_EXTN_DSC_LN_ITM   NUMBER(13,2) DEFAULT 0," &
    "MO_VAT_LN_ITM_RTN     NUMBER(13,2) DEFAULT 0," &
    "MO_FE_RSTK            NUMBER(13,2)," &
    "FL_RTN_MR            CHAR(1 BYTE) DEFAULT '0'," &
    "RC_RTN_MR            VARCHAR2(2 BYTE)," &
    "FL_RFD_SV            CHAR(1 BYTE) DEFAULT '0'," &
    "RC_RFD_SV            VARCHAR2(2 BYTE)," &
    "LU_MTH_ID_ENR        VARCHAR2(4 BYTE)," &
    "LU_ENTR_RT_PRC        VARCHAR2(4 BYTE)," &
    "LU_PRC_RT_DRVN        VARCHAR2(4 BYTE)," &
    "QU_ITM_LN_RTN         NUMBER(9,2) DEFAULT 0," &
    "ID_TRN_ORG            VARCHAR2(20 BYTE)," &
    "DC_DY_BSN_ORG        VARCHAR2(10 BYTE)," &
    "AI_LN_ITM_ORG        NUMBER(*,0) DEFAULT -1," &
    "ID_STR_RT_ORG         VARCHAR2(5 BYTE)," &
    "ID_NMB_SRZ            VARCHAR2(40 BYTE)," &
    "LU_KT_ST             VARCHAR2(20 BYTE) DEFAULT '0'," &
    "LU_KT_HDR_RFN_ID       NUMBER(*,0)," &
    "ID_CLN                VARCHAR2(14 BYTE)," &
    "FL_SND               CHAR(1 BYTE) DEFAULT '0'," &
    "CNT_SND_LAB           NUMBER(*,0) DEFAULT 0," &
    "ID_SHP_MTH            NUMBER(*,0)," &
    "SPL_INSTRC            VARCHAR2(120 BYTE)," &
    "ADS_SHP               VARCHAR2(80 BYTE)," &
    "FL_RCV_GF             CHAR(1 BYTE) DEFAULT '0'," &
    "OR_ID_REF             NUMBER(*,0) DEFAULT 0," &
    "FL_VD_LN_ITM          CHAR(1 BYTE) DEFAULT '0'," &
    "FL_MDFR_RTL_PRC      CHAR(1 BYTE) DEFAULT '0'," &
    "ED_SZ                VARCHAR2(10 BYTE)," &
    "FL_ITM_PRC_ADJ        CHAR(1 BYTE) DEFAULT '0'," &
    "LU_PRC_ADJ_RFN_ID     NUMBER(*,0)," &
    "FL_RLTD_ITM_RTN       CHAR(1 BYTE)," &
    "AI_LN_ITM_RLTD        NUMBER(*,0)," &
    "FL_RLTD_ITM_RM        CHAR(1 BYTE) DEFAULT '1' NOT NULL ENABLE," &
    "FL_RTRVD_TRN          CHAR(1 BYTE) DEFAULT '0'," &
    "MO_TAX_INC_LN_ITM_RTN NUMBER(13,2) DEFAULT 0," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9)," &
    "FL_SLS_ASSC_MDF CHAR(1 BYTE) DEFAULT '0'," &
    "MO_PRN_PRC      NUMBER(8,2) DEFAULT 0" &
 " )"

                Dim cmd33 As Integer = t29.ExecuteNonQuery
                cmd33 = cmd33 * -1

                t30.Connection = conn
                t30.CommandText = "CREATE TABLE TR_LTM_SLS_RTN_RECEIPT" &
  "(" &
    "ID_STR_RT             VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "ID_WS                VARCHAR2(3 BYTE) NOT NULL ENABLE," &
    "DC_DY_BSN            VARCHAR2(10 BYTE) NOT NULL ENABLE," &
    "AI_TRN                NUMBER(*,0) NOT NULL ENABLE," &
    "AI_LN_ITM            NUMBER(*,0) NOT NULL ENABLE," &
    "ID_REGISTRY           VARCHAR2(14 BYTE)," &
    "ID_GP_TX              NUMBER(*,0) DEFAULT 0," &
    "ID_DPT_POS            VARCHAR2(14 BYTE)," &
    "ID_ITM_POS            VARCHAR2(14 BYTE)," &
    "ID_ITM                VARCHAR2(14 BYTE)," &
    "QU_ITM_LM_RTN_SLS    NUMBER(9,2) DEFAULT 0," &
    "MO_EXTN_LN_ITM_RTN    NUMBER(13,2) DEFAULT 0," &
    "MO_EXTN_DSC_LN_ITM   NUMBER(13,2) DEFAULT 0," &
    "MO_VAT_LN_ITM_RTN     NUMBER(13,2) DEFAULT 0," &
    "MO_FE_RSTK            NUMBER(13,2)," &
    "FL_RTN_MR            CHAR(1 BYTE) DEFAULT '0'," &
    "RC_RTN_MR            VARCHAR2(2 BYTE)," &
    "FL_RFD_SV            CHAR(1 BYTE) DEFAULT '0'," &
    "RC_RFD_SV            VARCHAR2(2 BYTE)," &
    "LU_MTH_ID_ENR        VARCHAR2(4 BYTE)," &
    "LU_ENTR_RT_PRC        VARCHAR2(4 BYTE)," &
    "LU_PRC_RT_DRVN        VARCHAR2(4 BYTE)," &
    "QU_ITM_LN_RTN         NUMBER(9,2) DEFAULT 0," &
    "ID_TRN_ORG            VARCHAR2(20 BYTE)," &
    "DC_DY_BSN_ORG        VARCHAR2(10 BYTE)," &
    "AI_LN_ITM_ORG        NUMBER(*,0) DEFAULT -1," &
    "ID_STR_RT_ORG         VARCHAR2(5 BYTE)," &
    "ID_NMB_SRZ            VARCHAR2(40 BYTE)," &
    "LU_KT_ST             VARCHAR2(20 BYTE) DEFAULT '0'," &
    "LU_KT_HDR_RFN_ID       NUMBER(*,0)," &
    "ID_CLN                VARCHAR2(14 BYTE)," &
    "FL_SND               CHAR(1 BYTE) DEFAULT '0'," &
    "CNT_SND_LAB           NUMBER(*,0) DEFAULT 0," &
    "ID_SHP_MTH            NUMBER(*,0)," &
    "SPL_INSTRC            VARCHAR2(120 BYTE)," &
    "ADS_SHP               VARCHAR2(80 BYTE)," &
    "FL_RCV_GF             CHAR(1 BYTE) DEFAULT '0'," &
    "OR_ID_REF             NUMBER(*,0) DEFAULT 0," &
    "FL_VD_LN_ITM          CHAR(1 BYTE) DEFAULT '0'," &
    "FL_MDFR_RTL_PRC      CHAR(1 BYTE) DEFAULT '0'," &
    "ED_SZ                VARCHAR2(10 BYTE)," &
    "FL_ITM_PRC_ADJ        CHAR(1 BYTE) DEFAULT '0'," &
    "LU_PRC_ADJ_RFN_ID     NUMBER(*,0)," &
    "FL_RLTD_ITM_RTN       CHAR(1 BYTE)," &
    "AI_LN_ITM_RLTD        NUMBER(*,0)," &
    "FL_RLTD_ITM_RM        CHAR(1 BYTE) DEFAULT '1' NOT NULL ENABLE," &
    "FL_RTRVD_TRN          CHAR(1 BYTE) DEFAULT '0'," &
    "MO_TAX_INC_LN_ITM_RTN NUMBER(13,2) DEFAULT 0," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9)," &
    "FL_SLS_ASSC_MDF CHAR(1 BYTE) DEFAULT '0'," &
    "MO_PRN_PRC      NUMBER(8,2) DEFAULT 0" &
 " )"
                Dim cmd34 As Integer = t30.ExecuteNonQuery
                cmd34 = cmd34 * -1



                t31.Connection = conn
                t31.CommandText = "CREATE TABLE TR_TRN" &
  "(" &
    "ID_STR_RT VARCHAR2(5 BYTE) NOT NULL ENABLE," &
    "ID_WS     VARCHAR2(3 BYTE) NOT NULL ENABLE," &
    "DC_DY_BSN  VARCHAR2(10 BYTE) NOT NULL ENABLE," &
    "AI_TRN    NUMBER(*,0) NOT NULL ENABLE," &
    "ID_OPR    VARCHAR2(10 BYTE)," &
    "TY_TRN     VARCHAR2(20 BYTE)," &
    "TS_TM_SRT  TIMESTAMP (9)," &
    "TS_TRN_BGN TIMESTAMP (9)," &
    "TS_TRN_END  TIMESTAMP (9)," &
    "FL_TRG_TRN    CHAR(1 BYTE) DEFAULT '0'," &
    "FL_KY_OFL    CHAR(1 BYTE) DEFAULT '0'," &
    "SC_TRN        NUMBER(*,0) DEFAULT 2," &
    "ID_EM        VARCHAR2(10 BYTE)," &
    "INF_CT        VARCHAR2(14 BYTE)," &
    "TY_INF_CT     NUMBER(*,0) DEFAULT 0," &
    "ID_RPSTY_TND   VARCHAR2(10 BYTE)," &
    "ID_CNY_ICD    NUMBER(*,0)," &
    "ID_TLOG_BTCH NUMBER(*,0) DEFAULT -1," &
    "ID_BTCH_ARCH  VARCHAR2(14 BYTE) DEFAULT '-1'," &
    "ID_RTLOG_BTCH VARCHAR2(14 BYTE) DEFAULT '-1'," &
    "SC_PST_PRCS   NUMBER(*,0) DEFAULT 0," &
    "FL_TRE_TRN    CHAR(1 BYTE) DEFAULT '0'," &
    "TS_CRT_RCRD TIMESTAMP (9)," &
    "TS_MDF_RCRD TIMESTAMP (9)," &
    "FL_SLS_ASSC_MDF CHAR(1 BYTE) DEFAULT '0'" &
  ")"
                Dim cmd35 As Integer = t31.ExecuteNonQuery
                cmd35 = cmd35 * -1

                t32.Connection = conn
                t32.CommandText = "CREATE TABLE USR_PASSWORD" &
  "(" &
    "EMP_ID   VARCHAR2(20 BYTE)," &
    "PASSWORD VARCHAR2(200 BYTE)" &
  ")"
                Dim cmd36 As Integer = t32.ExecuteNonQuery
                cmd36 = cmd36 * -1

                t33.Connection = conn
                t33.CommandText = "CREATE TABLE VALIDITY" &
  "(" &
    "BARCODE       VARCHAR2(50 BYTE)," &
    "VALIDITY_DATE VARCHAR2(50 BYTE)" &
 " )"
                Dim cmd37 As Integer = t33.ExecuteNonQuery
                cmd37 = cmd37 * -1

                t34.Connection = conn
                t34.CommandText = "CREATE TABLE  SERIAL" &
  "(" &
    "SN VARCHAR2(500 BYTE)," &
    "WS number," &
    "WS_STATUS VARCHAR2(500 BYTE)" &
  ")"
                Dim cmd38 As Integer = t34.ExecuteNonQuery
                cmd38 = cmd38 * -1

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''
                insert1.Connection = conn
                insert1.CommandText = "insert into daily_ops values('admin','1000','',TO_CHAR(SYSDATE, 'dd-mm-yyyy HH:MM'),'2',TO_CHAR(SYSDATE, 'dd-mm-yyyy'))"

                Dim cmd39 As Integer = insert1.ExecuteNonQuery
                'cmd39 = cmd39 * -1

                insert2.Connection = conn
                insert2.CommandText = "Insert into pole_display values('COM',0)"

                Dim cmd40 As Integer = insert2.ExecuteNonQuery
                'cmd40 = cmd40 * -1

                insert3.Connection = conn
                insert3.CommandText = "insert into pos_info values('...','...','...','...','...','1','0',1,'...')"

                Dim cmd41 As Integer = insert3.ExecuteNonQuery
                'cmd41 = cmd41 * -1

                insert4.Connection = conn
                insert4.CommandText = "INSERT INTO PA_EM (ID_EM, ID_PRTY, ID_LOGIN, ID_ALT, PW_ACS_EM, NM_EM, LN_EM, " &
"FN_EM, MD_EM, UN_NMB_SCL_SCTY, SC_EM, ID_GP_WRK, LCL, NUMB_DYS_VLD, TYPE_EMP, FL_PW_NW_REQ, TS_CRT_PW, NUMB_FLD_PW) " &
"VALUES('admin', '1', 'system', 'admin', 'd033e22ae348aeb5660fc2140aec35850c4da997', 'administrator','system', '', 'administrator', '736352826', '1', '8', 'en_US', '0', '0', '1', " &
"TO_TIMESTAMP('24-SEP-13 09.08.56.741000000 AM', 'DD-MON-RR HH.MI.SS.FF AM'), '0')"
                Dim cmd42 As Integer = insert4.ExecuteNonQuery
                'cmd42 = cmd42 * -1



                t35.Connection = conn
                t35.CommandText = "CREATE TABLE RECEIPT_NUMBERS_WS" &
  "(" &
    "SYSTEM_DATE      VARCHAR2(100 BYTE)," &
    "RECEIPT_NUMBER    NUMBER," &
    "RECEIPT_REFERENCE VARCHAR2(100 BYTE)," &
    "QTY              VARCHAR2(20 BYTE)," &
    "TOTAL            VARCHAR2(20 BYTE), " &
     "VAT           VARCHAR2(20 BYTE), " &
    "EMP               VARCHAR2(20 BYTE)," &
    "TENDER_TYPE        VARCHAR2(20 BYTE), " &
    "OPS_TYPE         VARCHAR2(20 BYTE), " &
    "OPS_TIME        VARCHAR2(200 BYTE)," &
    "BUSINESS_DATE    VARCHAR2(200 BYTE)," &
    "CRDT_INFO         VARCHAR2(1000 BYTE)," &
    "WS         VARCHAR2(500 BYTE)" &
  ")"

                Dim cmd43 As Integer = t35.ExecuteNonQuery
                cmd43 = cmd43 * -1


                Dim t36 As New OracleCommand
                t36.Connection = conn
                t36.CommandText = "create table buy_x_get_y_discount" &
                "(" &
                "ITM     VARCHAR2(50) ," &
                "X       NUMBER," &
                "Y       NUMBER," &
                "C       NUMBER," &
                "PROMOTION_ID varchar2(10)," &
                "START_DATE varchar2(50)," &
                "END_DATE varchar2(50)" &
                 ")"

                Dim cmd44 As Integer = t36.ExecuteNonQuery
                cmd44 = cmd44 * -1


                Dim t37 As New OracleCommand
                t37.Connection = conn
                t37.CommandText = "CREATE TABLE PROMO_PER 
   (	BARCODE VARCHAR2(20), 
	PER VARCHAR2(10), 
	PROMOTION_ID VARCHAR2(10), 
	START_DATE VARCHAR2(50), 
	END_DATE VARCHAR2(50), 
	CREATION_DATE VARCHAR2(50), 
	OLD_PRICE NUMBER, 
	NEW_PRICE NUMBER
   )"

                Dim cmd45 As Integer = t37.ExecuteNonQuery
                cmd45 = cmd45 * -1

                Dim t38 As New OracleCommand
                t38.Connection = conn
                t38.CommandText = "CREATE TABLE promo_oneprice
   (	BARCODE VARCHAR2(20), 
	PER VARCHAR2(10), 
	PROMOTION_ID VARCHAR2(10), 
	START_DATE VARCHAR2(50), 
	END_DATE VARCHAR2(50), 
	CREATION_DATE VARCHAR2(50), 
	OLD_PRICE NUMBER, 
	NEW_PRICE NUMBER
   )"

                Dim cmd46 As Integer = t38.ExecuteNonQuery
                cmd46 = cmd46 * -1

                Dim t39 As New OracleCommand
                t39.Connection = conn
                t39.CommandText = "create table PROMOTIONS_ID" &
                "(" &
                "PROMOTION_ID varchar2(10)," &
                "PROMOTION_NAME varchar2(50)," &
                "CREATED_BY varchar2(50)," &
                "CREATION_DATE varchar2(50)" &
                ")"

                Dim cmd47 As Integer = t39.ExecuteNonQuery
                cmd47 = cmd47 * -1

                Dim t40 As New OracleCommand
                t40.Connection = conn
                t40.CommandText = "create table DSF" &
                "(" &
                 "Store_Code varchar2(100)," &
                "Itm_number varchar2(20)," &
                "Itm_name varchar2(200)," &
                "Itm_price varchar2(20)," &
                "Business_Date varchar2(100)," &
                "Scan_time Varchar2(100)," &
                "Ops_type varchar2(100)," &
                "Payment varchar2(100)," &
                "User_Id varchar2(100)" &
                ")"

                Dim cmd48 As Integer = t40.ExecuteNonQuery
                cmd48 = cmd48 * -1

                Dim t41 As New OracleCommand
                t41.Connection = conn
                t41.CommandText = "CREATE TABLE T_T" &
  "(" &
    "T_T_FLAG  VARCHAR2(20 BYTE)," &
    "TIME_STAMP VARCHAR2(50 BYTE)" &
 ")"

                Dim cmd49 As Integer = t41.ExecuteNonQuery
                cmd49 = cmd49 * -1

                Dim t42 As New OracleCommand
                t42.Connection = conn
                t42.CommandText = "CREATE TABLE LOCATIONS_MANAGER" &
  "(" &
    "LOCATION_NUMBER VARCHAR2(20 BYTE)," &
    "LOCATION_NAME   VARCHAR2(100 BYTE)," &
    "LOCATION_ADDRESS VARCHAR2(200 BYTE)," &
    "LOCATION_TEL     VARCHAR2(50 BYTE)" &
 ")"


                Dim cmd50 As Integer = t42.ExecuteNonQuery
                cmd50 = cmd50 * -1


                Dim t43 As New OracleCommand
                t43.Connection = conn
                t43.CommandText = "CREATE TABLE TRANSFER_MANAGER
   (	TRANSFER_NUMBER NUMBER, 
	TRANSFER_FROM VARCHAR2(250), 
	TRANSFER_TO VARCHAR2(250), 
	TRANSFER_DATE VARCHAR2(50), 
	CONTACT_PERSON VARCHAR2(50), 
	TRANSFER_STATUS VARCHAR2(100)
   )"


                Dim cmd51 As Integer = t43.ExecuteNonQuery
                cmd51 = cmd51 * -1

                Dim t44 As New OracleCommand
                t44.Connection = conn
                t44.CommandText = "CREATE TABLE SCANNED_ITEMS" &
  "(" &
    "TRANSFER_NUMBER VARCHAR2(20 BYTE)," &
     "BOX_ID          VARCHAR2(20 BYTE)," &
   "ITM_BARCODE     VARCHAR2(20 BYTE)," &
  "ITM_NAME        VARCHAR2(100 BYTE)," &
   "QTY             VARCHAR2(20 BYTE)," &
     "R_I            VARCHAR2(50 BYTE) " &
    ")"


                Dim cmd52 As Integer = t44.ExecuteNonQuery
                cmd52 = cmd52 * -1


                insert1.Connection = conn
                insert1.CommandText = "insert into T_T values('1984',sysdate)"
                insert1.ExecuteNonQuery()

                Dim t45 As New OracleCommand
                t45.Connection = conn
                t45.CommandText = "CREATE TABLE PRINTER_STATUS" &
  "(" &
    "STATUS VARCHAR2(10 BYTE)" &
       ")"


                Dim cmd53 As Integer = t45.ExecuteNonQuery
                cmd53 = cmd53 * -1

                Dim t46 As New OracleCommand
                t46.Connection = conn
                t46.CommandText = "CREATE TABLE BACKGROUND" &
  "(" &
    "COLOR VARCHAR2(20 BYTE)" &
       ")"


                Dim cmd54 As Integer = t46.ExecuteNonQuery
                cmd54 = cmd54 * -1


                Dim t47 As New OracleCommand
                t47.Connection = conn
                t47.CommandText = "CREATE TABLE RECEIPT_LANGUEGE" &
  "(" &
    "LANG VARCHAR2(20 BYTE)" &
       ")"

                Dim cmd55 As Integer = t47.ExecuteNonQuery
                cmd55 = cmd55 * -1


                Dim t48 As New OracleCommand
                t48.Connection = conn
                t48.CommandText = "CREATE TABLE BXGD" &
  "(" &
    "BARCODE      VARCHAR2(20 BYTE)," &
    "X           VARCHAR2(20 BYTE)," &
    "Y           VARCHAR2(20 BYTE)," &
    "TEMP          VARCHAR2(20 BYTE)," &
    "PROMO_ID      VARCHAR2(50 BYTE)," &
    "START_DATE    VARCHAR2(50 BYTE)," &
    "END_DATE      VARCHAR2(50 BYTE)," &
    "CREATION_DATE VARCHAR2(50 BYTE)" &
       ")"



                Dim cmd56 As Integer = t48.ExecuteNonQuery
                cmd56 = cmd56 * -1

                Dim t49 As New OracleCommand
                t49.Connection = conn
                t49.CommandText = "CREATE TABLE PriceBasedonWeight" &
  "(" &
    "ITM_ID VARCHAR2(50)," &
    "ITM_NAME VARCHAR2(200)," &
    "UNIT VARCHAR2(100)," &
    "PRICE_UNIT VARCHAR2(100)" &
       ")"



                Dim cmd57 As Integer = t49.ExecuteNonQuery
                cmd57 = cmd57 * -1


                Dim t50 As New OracleCommand
                t50.Connection = conn
                t50.CommandText = "create table ops_fees
(
bill_number varchar2(50),
fees_type varchar2(200),
fees_desc varchar2(500),
bill_ref varchar2(100),
bill_date varchar2(50),
bill_amount varchar2(50),
time_stamp date,
user_name varchar2(200)
)"

                Dim cmd58 As Integer = t50.ExecuteNonQuery
                cmd58 = cmd58 * -1


                Dim t51 As New OracleCommand
                t51.Connection = conn
                t51.CommandText = "create table ops_fees_serial
(
ops number,
ops_t date default sysdate,
unique(ops)
)"

                Dim cmd59 As Integer = t51.ExecuteNonQuery
                cmd59 = cmd59 * -1

                Dim t52 As New OracleCommand
                t52.Connection = conn
                t52.CommandText = "create table transfer_serial
(
t_s number,
t_t date default sysdate,
unique(t_s)
)"

                Dim cmd60 As Integer = t52.ExecuteNonQuery
                cmd60 = cmd60 * -1

                insert_lang.Connection = conn
                insert_lang.CommandText = "insert into RECEIPT_LANGUEGE values('1')"
                insert_lang.ExecuteNonQuery()

                insert_printer.Connection = conn
                insert_printer.CommandText = "insert into PRINTER_STATUS values('2')"
                insert_printer.ExecuteNonQuery()

                insert_color.Connection = conn
                insert_color.CommandText = "insert into BACKGROUND values('Cyan')"
                insert_color.ExecuteNonQuery()

                taskbar = taskbar + cmd17 + cmd18 + cmd19 + cmd20 + cmd21 + cmd22 + cmd23 + cmd24 + cmd25 + cmd26 + cmd27 + cmd28 + cmd29 + cmd30 + cmd31 + cmd32 + cmd33 + cmd34 + cmd35 + cmd36 + cmd37 + cmd38 + cmd39 + cmd40 + cmd41 + cmd42 + cmd43 + cmd44 + cmd45 + cmd46 + cmd47 + cmd48 + cmd49 + cmd50 + cmd51 + cmd52 + cmd53 + cmd54 + cmd55 + cmd56 + cmd57 + cmd58 + cmd59 + cmd60








                conn.Close()

            End If



            ' MessageBox.Show(taskbar.ToString)



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            dbinfo.Show()


            Me.Close()



        End Try
    End Sub

    
    Private Sub create_pos_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'Threading.Thread.Sleep(5000)
            Button1_Click(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class