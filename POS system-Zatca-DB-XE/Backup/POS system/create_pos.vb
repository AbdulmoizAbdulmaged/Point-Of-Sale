Imports System.Data
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Public Class create_pos
    Dim conn As New OracleConnection
    Dim create_schema As New OracleCommand
    Dim grant_privilegies As New OracleCommand
    Dim grant1 As New OracleCommand
    Dim grant2 As New OracleCommand
    Dim grant3 As New OracleCommand
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









    Dim taskbar As Integer = 0




    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    

    End Sub

    Private Sub create_pos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            system_hose = dbinfo.MaskedTextBox3.Text
            system_id = dbinfo.MaskedTextBox1.Text
            system_password = dbinfo.MaskedTextBox2.Text


          


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
            conn_string = "Data Source=" + system_hose + ":1521/orcl;User Id=" + system_id + ";password=" + system_password + ";"

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
                taskbar = create + cmd1 + cmd2 + cmd3 + cmd4
                conn.Close()

            End If


            ''''''''''''''''''''''''''''''''''''''''''
            conn_string = "Data Source=" + system_hose + ":1521/orcl;User Id=" + store_code + ";password=" + password + ";"
            conn.ConnectionString = conn_string
            conn.Open()

            If conn.State = ConnectionState.Open Then
                t1.Connection = conn
                t1.CommandText = "CREATE TABLE ALL_ITEMS" & _
  "(" & _
    "PROMO_ID VARCHAR2(50 BYTE)," & _
    "BARCODE  VARCHAR2(200 BYTE)," & _
    "PROMO_DATE DATE" & _
  ")"

                Dim cmd5 As Integer = t1.ExecuteNonQuery()
                cmd5 = cmd5 * -1
                taskbar = taskbar + cmd5


                t2.Connection = conn
                t2.CommandText = "CREATE TABLE AS_ITM" & _
  "(" & _
    "ID_ITM          VARCHAR2(14 BYTE) NOT NULL ENABLE," & _
    "ID_ITM_PDT    VARCHAR2(14 BYTE)," & _
    "FL_ITM_DSC      CHAR(1 BYTE)," & _
    "FL_ITM_DSC_DMG  CHAR(1 BYTE)," & _
    "FL_ADT_ITM_PRC  CHAR(1 BYTE)," & _
    "FL_ITM_SZ_REQ   CHAR(1 BYTE) DEFAULT '0'," & _
    "ID_DPT_POS      VARCHAR2(14 BYTE)," & _
    "FL_AZN_FR_SLS   CHAR(1 BYTE)," & _
    "LU_EXM_TX       VARCHAR2(20 BYTE)," & _
    "LU_ITM_USG      VARCHAR2(20 BYTE)," & _
    "NM_ITM          VARCHAR2(120 BYTE)," & _
    "DE_ITM         VARCHAR2(255 BYTE)," & _
    "DE_ITM_SHRT     VARCHAR2(120 BYTE)," & _
    "TY_ITM          VARCHAR2(20 BYTE)," & _
    "LU_KT_ST       VARCHAR2(20 BYTE) DEFAULT '0'," & _
    "FL_ITM_SBST_IDN CHAR(1 BYTE) DEFAULT '0'," & _
    "LU_CLN_ORD      VARCHAR2(20 BYTE)," & _
    "ID_STRC_MR      NUMBER(*,0)," & _
    "ID_LN_PRC       NUMBER(*,0)," & _
    "NM_BRN          VARCHAR2(40 BYTE)," & _
    "LU_SN           VARCHAR2(20 BYTE)," & _
    "FY              VARCHAR2(4 BYTE)," & _
    "LU_HRC_MR_LV    VARCHAR2(4 BYTE)," & _
    "LU_SBSN         VARCHAR2(20 BYTE)," & _
    "ID_GP_TX        NUMBER(*,0) DEFAULT 0," & _
    "FL_ACTVN_RQ     CHAR(1 BYTE)," & _
    "FL_ITM_RGSTRY   CHAR(1 BYTE)," & _
    "ID_STRC_MR_CD0  VARCHAR2(10 BYTE)," & _
    "ID_STRC_MR_CD1  VARCHAR2(10 BYTE)," & _
    "ID_STRC_MR_CD2  VARCHAR2(10 BYTE)," & _
    "ID_STRC_MR_CD3  VARCHAR2(10 BYTE)," & _
    "ID_STRC_MR_CD4  VARCHAR2(10 BYTE)," & _
    "ID_STRC_MR_CD5  VARCHAR2(10 BYTE)," & _
    "ID_STRC_MR_CD6  VARCHAR2(10 BYTE)," & _
    "ID_STRC_MR_CD7  VARCHAR2(10 BYTE)," & _
    "ID_STRC_MR_CD8  VARCHAR2(10 BYTE)," & _
    "ID_STRC_MR_CD9  VARCHAR2(10 BYTE)," & _
    "ID_MRHRC_GP     VARCHAR2(14 BYTE) DEFAULT '0'," & _
    "ID_MF           NUMBER(*,0)," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9) DEFAULT SYSDATE," & _
    "PRIMARY KEY (ID_ITM) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
  ")"
                Dim cmd6 As Integer = t2.ExecuteNonQuery
                cmd6 = cmd6 * -1
                taskbar = taskbar + cmd6


                t3.Connection = conn
                t3.CommandText = "CREATE TABLE  AS_ITM_RTL_STR" & _
  "(" & _
    "ID_STR_RT       VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "ID_ITM           VARCHAR2(14 BYTE) NOT NULL ENABLE," & _
    "FL_STK_UPT_ON_HD CHAR(1 BYTE)," & _
    "DC_ITM_SLS DATE," & _
    "SC_ITM_SLS         VARCHAR2(2 BYTE)," & _
    "RP_PRC_CMPR_AT_SLS  NUMBER(8,2)," & _
    "DC_PRC_MF_REC_RT DATE," & _
    "RP_PRC_MF_REC_RT  NUMBER(8,2)," & _
    "ID_GP_TX         NUMBER(*,0) DEFAULT 0," & _
    "DC_PRC_SLS_EP_CRT DATE," & _
    "DC_PRC_SLS_EF_CRT  DATE," & _
    "FL_PRC_RT_PNT_ALW  CHAR(1 BYTE)," & _
    "TY_PRC_RT          VARCHAR2(2 BYTE)," & _
    "RP_SLS_CRT        NUMBER(8,2)," & _
    "DC_PRC_EF_PRN_RT  DATE," & _
    "QU_MKD_PR_PRC_PR   NUMBER(7,3)," & _
    "FL_MKD_ORGL_PRC_PR  CHAR(1 BYTE)," & _
    "RP_PR_SLS          NUMBER(8,2)," & _
    "SC_ITM             VARCHAR2(2 BYTE)," & _
    "IDN_SLS_AG_RST     NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "ID_TMPLT_LB        VARCHAR2(8 BYTE)," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9) DEFAULT SYSDATE," & _
    "PRIMARY KEY (ID_ITM, ID_STR_RT) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
")"
                Dim cmd7 As Integer = t3.ExecuteNonQuery
                cmd7 = cmd7 * -1
                taskbar = taskbar + cmd7


                t4.Connection = conn
                t4.CommandText = "CREATE TABLE AS_ITM_STK" & _
  "(" & _
    "ID_ITM             VARCHAR2(14 BYTE) NOT NULL ENABLE," & _
    "LU_UOM_SLS          VARCHAR2(20 BYTE) NOT NULL ENABLE," & _
    "ED_CLR            VARCHAR2(20 BYTE)," & _
    "ED_SZ              VARCHAR2(10 BYTE)," & _
    "LU_STYL            VARCHAR2(4 BYTE)," & _
    "ID_STR_RT           VARCHAR2(5 BYTE) DEFAULT '0'," & _
    "QL_HT_PCKG_CNS      NUMBER(9,2) DEFAULT 0," & _
    "ID_SPR             VARCHAR2(20 BYTE) DEFAULT '0'," & _
    "NM_NMB_SRZ_ITM_MDL  VARCHAR2(40 BYTE)," & _
    "QL_UOM_WD_PCKG_CNS  NUMBER(9,2) DEFAULT 0," & _
    "TY_WST_BLK_SLS      VARCHAR2(20 BYTE)," & _
    "CY_MDL_SRZ_ITM      VARCHAR2(4 BYTE)," & _
    "QU_CPC_HLD           NUMBER(9,2) DEFAULT 0," & _
    "LU_UOM              VARCHAR2(2 BYTE)," & _
    "QW_ITM_PCK         NUMBER(9,2) DEFAULT 0," & _
    "TY_UN_DPLY         VARCHAR2(20 BYTE)," & _
    "QU_CB_PCK_ITM       NUMBER(9,2) DEFAULT 0," & _
    "QL_PCKG_CNS         NUMBER(9,2) DEFAULT 0," & _
    "DE_CLR_MF_SRZ_ITM  VARCHAR2(255 BYTE)," & _
    "TY_ITM_STK          VARCHAR2(20 BYTE)," & _
    "PE_WST_BLK_SLS     NUMBER(5,2) DEFAULT 0," & _
    "LU_UOM_PCKG_CNS_DMN  VARCHAR2(20 BYTE)," & _
    "LU_SYS_PRMRY_MS    VARCHAR2(20 BYTE)," & _
    "DE_SZ_MF_SRZ_ITM   VARCHAR2(255 BYTE)," & _
    "QU_UN_PCK_ITM       NUMBER(9,2) DEFAULT 0," & _
    "DC_UN_DPLY_ST_UP DATE," & _
    "LU_WRTY_MF_SRZ_ITM  VARCHAR2(20 BYTE)," & _
    "QW_WT_PCKG_CNS     NUMBER(9,2) DEFAULT 0," & _
    "DC_UN_DPLY_TK_DWN  DATE," & _
    "DE_FBRC             VARCHAR2(255 BYTE)," & _
    "DP_UN_DPLY        VARCHAR2(20 BYTE)," & _
    "LU_CNT_SLS_WT_UN   VARCHAR2(20 BYTE)," & _
    "LU_WRTY_STR_SRZ    VARCHAR2(20 BYTE)," & _
    "LU_UOM_WT_PCKG_CNS VARCHAR2(20 BYTE)," & _
    "TY_PKP_CT_STK_ITM VARCHAR2(20 BYTE)," & _
    "LU_UOM_SZ_PCKG_CNS VARCHAR2(20 BYTE)," & _
    "DE_SLH             VARCHAR2(255 BYTE)," & _
    "FL_VLD_SRZ_ITM    CHAR(1 BYTE)," & _
    "FA_PRC_UN_STK_ITM  NUMBER(9,2) DEFAULT 0," & _
    "FL_DSD_AZN        CHAR(1 BYTE)," & _
    "DI_PRD_SH_LF       NUMBER(3,0) DEFAULT 0," & _
    "DI_LF_SH           NUMBER(3,0) DEFAULT 0," & _
    "ID_BRKR            NUMBER(*,0) DEFAULT 0," & _
    "DC_AVLB_FR_SLS DATE," & _
    "TY_ITM_STPL_PRSH   VARCHAR2(20 BYTE)," & _
    "TY_ENV_STK_ITM     VARCHAR2(20 BYTE)," & _
    "NM_LCN_ASL          VARCHAR2(255 BYTE)," & _
    "TY_SCTY_RQ         VARCHAR2(20 BYTE)," & _
    "NM_LCN_SH          VARCHAR2(255 BYTE)," & _
    "NM_LCN_SID         VARCHAR2(255 BYTE)," & _
    "TY_MTR_HZ_STK_ITM  VARCHAR2(20 BYTE)," & _
    "QU_FCG             NUMBER(9,2) DEFAULT 0," & _
    "CP_UN_SL_LS_RCV_BS NUMBER(7,3) DEFAULT 0," & _
    "CP_CST_NT_LS_RCV   NUMBER(7,3) DEFAULT 0," & _
    "CP_UN_SL_LND        NUMBER(7,3) DEFAULT 0," & _
    "DC_CST_EST_LS_RCV DATE," & _
    "FL_SHRK_SH_ITM CHAR(1 BYTE)," & _
    "FL_SWL_SH_ITM  CHAR(1 BYTE)," & _
    "FL_RQ_UN_PRC   CHAR(1 BYTE)," & _
    "FL_FE_RSTK     CHAR(1 BYTE)," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9) DEFAULT SYSDATE," & _
    "PRIMARY KEY (ID_ITM) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
 " )"
                Dim cmd8 As Integer = t4.ExecuteNonQuery
                cmd8 = cmd8 * -1
                taskbar = taskbar + cmd8

                t5.Connection = conn
                t5.CommandText = "CREATE TABLE CO_BRK_SPR_ITM_BS" & _
 "(" & _
    "ID_SPR           VARCHAR2(20 BYTE) NOT NULL ENABLE," & _
    "ID_ITM_SPR        VARCHAR2(20 BYTE) NOT NULL ENABLE," & _
    "TY_UN_CST         VARCHAR2(3 BYTE) NOT NULL ENABLE," & _
    "QU_PNT_UND_BRK   NUMBER(9,2) NOT NULL ENABLE," & _
    "CP_PNT_BRK_BS_CST NUMBER(13,4)," & _
    "TS_CRT_RCRD  TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9)," & _
   " PRIMARY KEY (ID_SPR, ID_ITM_SPR, TY_UN_CST, QU_PNT_UND_BRK) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
  ")"
                Dim cmd9 As Integer = t5.ExecuteNonQuery
                cmd9 = cmd9 * -1
                taskbar = taskbar + cmd9


                t6.Connection = conn
                t6.CommandText = "CREATE TABLE CO_EV" & _
  "(" & _
    "ID_EV        NUMBER(*,0) NOT NULL ENABLE," & _
    "ID_STR_RT   VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "NM_EV       VARCHAR2(160 BYTE)," & _
    "DE_EV        VARCHAR2(640 BYTE)," & _
    "TY_EV       VARCHAR2(20 BYTE)," & _
    "SC_EV        VARCHAR2(20 BYTE)," & _
    "CC_EV        VARCHAR2(20 BYTE)," & _
    "NM_EV_OWNR   VARCHAR2(40 BYTE)," & _
    "DC_DY_BSN_SS VARCHAR2(10 BYTE)," & _
    "DC_DY_BSN_SE VARCHAR2(10 BYTE)," & _
    "DC_DY_BSN_AS VARCHAR2(10 BYTE)," & _
    "DC_DY_BSN_AE VARCHAR2(10 BYTE)," & _
    "TS_EV_PL_EF TIMESTAMP (9)," & _
    "TS_EV_PL_EP TIMESTAMP (9)," & _
    "TS_EV_ACT_EF TIMESTAMP (9)," & _
    "TS_EV_ACT_EP TIMESTAMP (9)," & _
    "ID_EV_EXT VARCHAR2(20 BYTE)," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9)," & _
    "PRIMARY KEY (ID_EV, ID_STR_RT) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
  ")"
                Dim cmd10 As Integer = t6.ExecuteNonQuery
                cmd10 = cmd10 * -1

                taskbar = taskbar + cmd10


                t7.Connection = conn
                t7.CommandText = "CREATE TABLE CO_EV_MNT" & _
  "(" & _
    "ID_EV     NUMBER(*,0) NOT NULL ENABLE," & _
    "ID_STR_RT VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "NM_EV_MNT VARCHAR2(40 BYTE)," & _
    "DE_EV_MNT VARCHAR2(255 BYTE)," & _
    "TS_EV_MNT_EF TIMESTAMP (9)," & _
    "TS_EV_MNT_EP TIMESTAMP (9)," & _
    "TY_EV_MNT     VARCHAR2(20 BYTE)," & _
    "SC_EV_MNT    VARCHAR2(20 BYTE)," & _
    "RC_EV_MNT     VARCHAR2(20 BYTE)," & _
    "TY_EV_MNT_ORG VARCHAR2(20 BYTE)," & _
    "ID_EM         VARCHAR2(10 BYTE)," & _
    "ID_CMP        NUMBER(*,0)," & _
    "TS_EV_MNT_CRT TIMESTAMP (9)," & _
    "TS_EV_MNT_APLY TIMESTAMP (9)," & _
    "ID_JOB_ST    VARCHAR2(12 BYTE)," & _
    "ID_JOB_END   VARCHAR2(12 BYTE)," & _
    "SC_EV_MNT_EF VARCHAR2(20 BYTE)," & _
    "SC_EV_MNT_EP VARCHAR2(20 BYTE)," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9)," & _
    "PRIMARY KEY (ID_EV, ID_STR_RT) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
  ")"

                Dim cmd11 As Integer = t7.ExecuteNonQuery
                cmd11 = cmd11 * -1
                taskbar = taskbar + cmd11

                t8.Connection = conn
                t8.CommandText = "CREATE TABLE CO_MDFR_RTL_PRC" & _
  "(" & _
    "ID_STR_RT          VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "ID_WS               VARCHAR2(3 BYTE) NOT NULL ENABLE," & _
    "DC_DY_BSN           VARCHAR2(10 BYTE) NOT NULL ENABLE," & _
    "AI_TRN             NUMBER(*,0) NOT NULL ENABLE," & _
    "AI_LN_ITM          NUMBER(*,0) NOT NULL ENABLE," & _
    "AI_MDFR_RT_PRC     NUMBER(*,0) NOT NULL ENABLE," & _
    "RC_MDFR_RT_PRC      VARCHAR2(20 BYTE)," & _
    "PE_MDFR_RT_PRC      NUMBER(5,2) DEFAULT 0," & _
    "MO_MDFR_RT_PRC      NUMBER(16,5) DEFAULT 0," & _
    "DP_LDG_STK_MDFR     VARCHAR2(20 BYTE)," & _
    "ID_RU_MDFR          NUMBER(*,0) DEFAULT 1," & _
    "CD_MTH_PRDV         NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "CD_BAS_PRDV         NUMBER(*,0) DEFAULT 0," & _
    "CD_TY_PRDV         NUMBER(*,0) DEFAULT 0," & _
    "FL_PCD_DL_ADVN_APLY CHAR(1 BYTE) DEFAULT '0' NOT NULL ENABLE," & _
    "FL_DSC_ADVN_PRC    CHAR(1 BYTE) DEFAULT '0' NOT NULL ENABLE," & _
    "ID_DSC_REF         VARCHAR2(255 BYTE)," & _
    "CO_ID_DSC_REF       VARCHAR2(2 BYTE) DEFAULT 'UN' NOT NULL ENABLE," & _
    "ID_EM_AZN_OVRD     VARCHAR2(10 BYTE)," & _
    "CD_MTH_ENR_OVRD     NUMBER(*,0) DEFAULT -1 NOT NULL ENABLE," & _
    "ID_DSC_EM           VARCHAR2(10 BYTE)," & _
    "FL_DSC_DMG          CHAR(1 BYTE) DEFAULT '0' NOT NULL ENABLE," & _
    "ID_PRM             NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "ID_PRM_CMP          NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "ID_PRM_CMP_DTL     NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9)," & _
    "PRIMARY KEY (ID_STR_RT, ID_WS, DC_DY_BSN, AI_TRN, AI_LN_ITM, AI_MDFR_RT_PRC) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
  ")"
                Dim cmd12 As Integer = t8.ExecuteNonQuery
                cmd12 = cmd12 * -1
                taskbar = taskbar + cmd12

                t9.Connection = conn
                t9.CommandText = "CREATE TABLE DAILY_OPS" & _
  "(" & _
    "USER_ID     VARCHAR2(50 BYTE)," & _
    "FLOAT_AMOUNT  VARCHAR2(20 BYTE)," & _
    "START_DATE   VARCHAR2(50 BYTE)," & _
    "END_DATE      VARCHAR2(50 BYTE)," & _
    "STATUS_FLAG   VARCHAR2(20 BYTE)," & _
    "BUSINESS_DATE VARCHAR2(50 BYTE)" & _
  ")"
                Dim cmd13 As Integer = t9.ExecuteNonQuery
                cmd13 = cmd13 * -1
                taskbar = taskbar + cmd13

                t10.Connection = conn
                t10.CommandText = "CREATE TABLE ID_IDN_PS" & _
  "(" & _
    "ID_STR_RT          VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "ID_ITM_POS         VARCHAR2(14 BYTE) NOT NULL ENABLE," & _
    "ID_ITM            VARCHAR2(14 BYTE) NOT NULL ENABLE," & _
    "DE_ITM_POS         VARCHAR2(255 BYTE)," & _
    "RP_SLS_POS_CRT     NUMBER(8,2) DEFAULT 0," & _
    "FL_PNT_FQ_SHPR_EL  CHAR(1 BYTE)," & _
    "ID_MF             NUMBER(*,0) DEFAULT 0," & _
    "ID_ITM_MF_UPC      VARCHAR2(14 BYTE)," & _
    "ID_AGNT_RTN       NUMBER(*,0) DEFAULT 0," & _
    "FL_DSC_AF_DSC_ALW  CHAR(1 BYTE)," & _
    "LU_VT_PS_CPN       NUMBER(2,2) DEFAULT 0," & _
    "DT_END_PS_CPN_OFR  VARCHAR2(4 BYTE)," & _
    "QU_UN_BLK_MNM     NUMBER(5,2) DEFAULT 1," & _
    "QU_UN_BLK_MXM     NUMBER(5,2) DEFAULT -1," & _
    "FL_DSC_MRK_BSK_ALW CHAR(1 BYTE)," & _
    "FL_DSC_CT_ACNT_ALW  CHAR(1 BYTE)," & _
    "FL_DSC_EM_ALW      CHAR(1 BYTE) DEFAULT '1'," & _
    "FL_CPN_ALW_MULTY   CHAR(1 BYTE) DEFAULT '0'," & _
    "FL_FD_STP_ALW      CHAR(1 BYTE)," & _
    "FL_CPN_ELNTC      CHAR(1 BYTE) DEFAULT '0', " & _
    "FL_CPN_RST        CHAR(1 BYTE) DEFAULT '0'," & _
    "FL_ENTR_PRC_RQ    CHAR(1 BYTE), " & _
    "FL_QR_ENR_WT      CHAR(1 BYTE), " & _
    "FL_KY_PRH_QTY    CHAR(1 BYTE)," & _
    "FL_RTN_PRH        CHAR(1 BYTE)," & _
    "FL_ITM_GWY         CHAR(1 BYTE)," & _
    "FL_ITM_WIC        CHAR(1 BYTE), " & _
    "FL_PRC_VS_VR      CHAR(1 BYTE), " & _
    "FL_KY_PRH_RPT     CHAR(1 BYTE), " & _
    "FL_SPO_ITM         CHAR(1 BYTE) DEFAULT '0'," & _
    "QU_PNT_FQ_SHPR    NUMBER(9,2) DEFAULT 0, " & _
    "LU_GP_TND_RST     VARCHAR2(20 BYTE)," & _
    "FC_FMY_MF        VARCHAR2(3 BYTE)," & _
    "FL_MDFR_RT_PRC   CHAR(1 BYTE)," & _
    "ID_PRM           NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "ID_PRM_CMP        NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "ID_PRM_CMP_DTL    NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9) DEFAULT SYSDATE," & _
    "PRIMARY KEY (ID_STR_RT, ID_ITM_POS, ID_ITM) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
  ")"
                Dim cmd14 As Integer = t10.ExecuteNonQuery
                cmd14 = cmd14 * -1
                taskbar = taskbar + cmd14

                t11.Connection = conn
                t11.CommandText = "CREATE TABLE INVENTORY_TEMP" & _
  "(" & _
    "STORE          VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "CASHIER         VARCHAR2(10 BYTE)," & _
    "SALE_DT         VARCHAR2(20 BYTE)," & _
    "BARCODE        VARCHAR2(14 BYTE)," & _
    "SALE_TYPE      VARCHAR2(7 BYTE)," & _
    "QTY            NUMBER(9,2)," & _
    "RETN_FLAG        CHAR(1 BYTE)," & _
    "AMOUNT          NUMBER(13,2)," & _
    "NET_AMOUNT      NUMBER(13,2)," & _
    "MANUAL_DISCOUNT NUMBER(14,3)," & _
    "TOTAL_DISCOUNT  NUMBER," & _
    "MDFR_PROMO_CODE NUMBER(*,0)," & _
    "MDFR_COMP_CODE  NUMBER(*,0)," & _
    "MDFR_DISCOUNT   NUMBER," & _
    "LTM_PROMO_CODE  NUMBER(*,0)," & _
    "LTM_PROMO_COMP  NUMBER(*,0)," & _
    "LTM_DISCOUNT    NUMBER," & _
    "MO_PRN_PRC      NUMBER(8,2)," & _
    "AI_LN_ITM       NUMBER(*,0) NOT NULL ENABLE" & _
 " )"
                Dim cmd15 As Integer = t11.ExecuteNonQuery
                cmd15 = cmd15 * -1
                taskbar = taskbar + cmd15

                t12.Connection = conn
                t12.CommandText = "CREATE TABLE LOG_IN" & _
  "(" & _
    "USER_NAME VARCHAR2(50 BYTE)," & _
    "USER_ID  VARCHAR2(30 BYTE)," & _
    "LOG_TIME DATE" & _
  ")"
                Dim cmd16 As Integer = t12.ExecuteNonQuery
                cmd16 = cmd16 * -1
                taskbar = taskbar + cmd16
                t13.Connection = conn
                t13.CommandText = "CREATE TABLE MA_ITM_PRN_PRC_ITM" & _
  "(" & _
    "ID_EV       NUMBER(*,0) NOT NULL ENABLE," & _
    "ID_STR_RT   VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "ID_ITM      VARCHAR2(14 BYTE) NOT NULL ENABLE," & _
    "ID_TMPLT_LB VARCHAR2(8 BYTE)," & _
    "MO_OVRD_PRC NUMBER(7,2)," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9)," & _
    "PRIMARY KEY (ID_EV, ID_STR_RT, ID_ITM) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
  ")"
                Dim cmd17 As Integer = t13.ExecuteNonQuery
                cmd17 = cmd17 * -1

                t14.Connection = conn
                t14.CommandText = "CREATE TABLE MA_PRC_ITM" & _
  "(" & _
    "ID_EV        NUMBER(*,0) NOT NULL ENABLE," & _
    "ID_STR_RT    VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "TY_PRC_MNT   VARCHAR2(20 BYTE)," & _
    "UN_PRI_EV    NUMBER(*,0)," & _
    "UN_DG_LS_PRC CHAR(1 BYTE)," & _
    "ID_TMPLT_LB  VARCHAR2(8 BYTE)," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9)," & _
    "PRIMARY KEY (ID_EV, ID_STR_RT) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
 " )"
                Dim cmd18 As Integer = t14.ExecuteNonQuery
                cmd18 = cmd18 * -1

                t15.Connection = conn
                t15.CommandText = "CREATE TABLE OPS" & _
  "(" & _
    "USER_NAME VARCHAR2(50 BYTE)," & _
    "USER_ID   VARCHAR2(30 BYTE)," & _
    "OPS_TYPE  NUMBER," & _
    "ENTRY_TIME DATE" & _
  ")"
                Dim cmd19 As Integer = t15.ExecuteNonQuery
                cmd19 = cmd19 * -1

                t16.Connection = conn
                t16.CommandText = "CREATE TABLE ORIGINAL_PRICES" & _
  "(" & _
    "BARCODE VARCHAR2(50 BYTE)," & _
    "I_D    VARCHAR2(500)," & _
    "S_P     VARCHAR2(50 BYTE)" & _
 " )"
                Dim cmd20 As Integer = t16.ExecuteNonQuery
                cmd20 = cmd20 * -1

                t17.Connection = conn
                t17.CommandText = "CREATE TABLE PA_EM" & _
  "(" & _
    "ID_EM           VARCHAR2(10 BYTE) NOT NULL ENABLE," & _
    "ID_PRTY       NUMBER(*,0) DEFAULT 0," & _
    "ID_LOGIN         VARCHAR2(10 BYTE)," & _
    "ID_ALT          VARCHAR2(10 BYTE)," & _
    "PW_ACS_EM       VARCHAR2(40 BYTE)," & _
    "NM_EM           VARCHAR2(150 BYTE)," & _
    "LN_EM           VARCHAR2(50 BYTE)," & _
    "FN_EM           VARCHAR2(50 BYTE)," & _
    "MD_EM          VARCHAR2(50 BYTE), " & _
    "ROLE_EM        VARCHAR2(50 BYTE), " & _
    "UN_NMB_SCL_SCTY CHAR(9 BYTE)," & _
    "SC_EM           VARCHAR2(20 BYTE)," & _
    "ID_GP_WRK       NUMBER(*,0) NOT NULL ENABLE," & _
    "LCL             VARCHAR2(10 BYTE)," & _
    "NUMB_DYS_VLD    NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "DC_EXP_TMP  DATE DEFAULT NULL," & _
    "TYPE_EMP     NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "ID_STR_RT    VARCHAR2(5 BYTE)," & _
    "FL_PW_NW_REQ CHAR(1 BYTE) DEFAULT '0' NOT NULL ENABLE," & _
    "TS_CRT_PW  TIMESTAMP (9)," & _
    "NUMB_FLD_PW NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "PRIMARY KEY (ID_EM) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
  ")"

                Dim cmd21 As Integer = t17.ExecuteNonQuery
                cmd21 = cmd21 * -1

                t18.Connection = conn
                t18.CommandText = "CREATE TABLE POLE_DISPLAY" & _
  "(" & _
    "COM   VARCHAR2(20 BYTE)," & _
    "STATUS VARCHAR2(20 BYTE)" & _
  ")"
                Dim cmd22 As Integer = t18.ExecuteNonQuery
                cmd22 = cmd22 * -1

                t19.Connection = conn
                t19.CommandText = "CREATE TABLE POS_INFO" & _
  "(" & _
    "COMP_NAME VARCHAR2(1200 BYTE)," & _
    "STORE_NAME VARCHAR2(1200 BYTE)," & _
    "ADDRESS   VARCHAR2(1200 BYTE)," & _
    "TELEPHONE  VARCHAR2(500 BYTE)," & _
    "POLICY     VARCHAR2(2000 BYTE)," & _
    "VAT_LIMIT  VARCHAR2(20 BYTE)," & _
    "VAT        VARCHAR2(20 BYTE)" & _
  ")"

                Dim cmd23 As Integer = t19.ExecuteNonQuery
                cmd23 = cmd23 * -1

                t20.Connection = conn
                t20.CommandText = "CREATE TABLE PRICES_HISTORY" & _
  "(" & _
    "BARCODE        VARCHAR2(30 BYTE)," & _
    "DESCRIPTION    VARCHAR2(500)," & _
    "SELL_PRICE     NUMBER," & _
    "ENTRY_TIME     VARCHAR2(50 BYTE)," & _
    "CREATED_BY     VARCHAR2(50 BYTE)," & _
    "OPERATION_TYPE VARCHAR2(20 BYTE)" & _
  ")"
                Dim cmd24 As Integer = t20.ExecuteNonQuery
                cmd24 = cmd24 * -1

                t21.Connection = conn
                t21.CommandText = "CREATE TABLE R_I" & _
  "(" & _
    "R_ID NUMBER," & _
    "R_T DATE DEFAULT SYSDATE," & _
    "UNIQUE (R_ID) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
 " )"
                Dim cmd25 As Integer = t21.ExecuteNonQuery
                cmd25 = cmd25 * -1

                t22.Connection = conn
                t22.CommandText = "CREATE TABLE RECEIPT_NUMBERS" & _
  "(" & _
    "SYSTEM_DATE      VARCHAR2(100 BYTE)," & _
    "RECEIPT_NUMBER    NUMBER," & _
    "RECEIPT_REFERENCE VARCHAR2(100 BYTE)," & _
    "QTY              VARCHAR2(20 BYTE)," & _
    "TOTAL            VARCHAR2(20 BYTE), " & _
     "VAT           VARCHAR2(20 BYTE), " & _
    "EMP               VARCHAR2(20 BYTE)," & _
    "TENDER_TYPE        VARCHAR2(20 BYTE), " & _
    "OPS_TYPE         VARCHAR2(20 BYTE), " & _
    "OPS_TIME        VARCHAR2(200 BYTE)," & _
    "BUSINESS_DATE    VARCHAR2(200 BYTE)," & _
    "CRDT_INFO         VARCHAR2(1000 BYTE)" & _
  ")"
                Dim cmd26 As Integer = t22.ExecuteNonQuery
                cmd26 = cmd26 * -1

                t23.Connection = conn
                t23.CommandText = "CREATE TABLE STOCK" & _
  "(" & _
    "R_N  VARCHAR2(50 BYTE) NOT NULL ENABLE," & _
    "R_R  VARCHAR2(50 BYTE) NOT NULL ENABLE," & _
    "R_D  VARCHAR2(50 BYTE) NOT NULL ENABLE," & _
    "I_B  VARCHAR2(50 BYTE) NOT NULL ENABLE," & _
    "I_D  VARCHAR2(50 BYTE) NOT NULL ENABLE," & _
    "S_P  VARCHAR2(50 BYTE) NOT NULL ENABLE," & _
    "I_P  VARCHAR2(50 BYTE) NOT NULL ENABLE," & _
    "QUAN VARCHAR2(50 BYTE) NOT NULL ENABLE," & _
    "TIME_STAMP DATE" & _
  ")"

                Dim cmd27 As Integer = t23.ExecuteNonQuery
                cmd27 = cmd27 * -1

                t24.Connection = conn
                t24.CommandText = "CREATE TABLE STOCK_HISTORY" & _
  "(" & _
    "RN        VARCHAR2(250 BYTE)," & _
    "RR        VARCHAR2(250 BYTE)," & _
    "IB         VARCHAR2(250 BYTE)," & _
    "I_D        VARCHAR2(250 BYTE)," & _
    "SP         VARCHAR2(250 BYTE)," & _
    "IP         VARCHAR2(250 BYTE)," & _
    "QUAN       VARCHAR2(250 BYTE)," & _
    "ENTRY_TIME VARCHAR2(250 BYTE)," & _
    "CREATED_BY VARCHAR2(250 BYTE)," & _
    "OPS_TYPE   VARCHAR2(250 BYTE)" & _
 " )"
                Dim cmd28 As Integer = t24.ExecuteNonQuery
                cmd28 = cmd28 * -1


                t25.Connection = conn
                t25.CommandText = "CREATE TABLE TR_CHN_PRN_PRC" & _
  "(" & _
    "ID_EV            NUMBER(*,0) NOT NULL ENABLE," & _
    "ID_STR_RT         VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "MO_CHN_PRN_UN_PRC VARCHAR2(20 BYTE)," & _
    "TY_CHN_PRN_UN_PRC VARCHAR2(20 BYTE)," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9)," & _
    "PRIMARY KEY (ID_EV, ID_STR_RT) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
  ")"
                Dim cmd29 As Integer = t25.ExecuteNonQuery
                cmd29 = cmd29 * -1

                t26.Connection = conn
                t26.CommandText = "CREATE TABLE TR_LTM_DSC" & _
  "(" & _
    "ID_STR_RT       VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "ID_WS         VARCHAR2(3 BYTE) NOT NULL ENABLE," & _
    "DC_DY_BSN      VARCHAR2(10 BYTE) NOT NULL ENABLE," & _
    "AI_TRN         NUMBER(*,0) NOT NULL ENABLE," & _
    "AI_LN_ITM       NUMBER(*,0) NOT NULL ENABLE," & _
    "TY_DSC          VARCHAR2(20 BYTE) NOT NULL ENABLE," & _
    "RC_DSC          VARCHAR2(20 BYTE) NOT NULL ENABLE," & _
    "MO_DSC          NUMBER(14,3) DEFAULT 0," & _
    "PE_DSC          NUMBER(5,2) DEFAULT 0," & _
    "FL_DSC_ENA      CHAR(1 BYTE) NOT NULL ENABLE," & _
    "LU_BAS_ASGN     VARCHAR2(2 BYTE) NOT NULL ENABLE," & _
    "FL_DL_ADVN_APLY CHAR(1 BYTE) DEFAULT '0' NOT NULL ENABLE," & _
    "ID_RU_DSC      NUMBER(*,0)," & _
    "ID_DSC_REF      VARCHAR2(255 BYTE)," & _
    "CO_ID_DSC_REF  VARCHAR2(2 BYTE) DEFAULT 'UN' NOT NULL ENABLE," & _
    "ID_DSC_EM      VARCHAR2(10 BYTE)," & _
    "ID_PRM         NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "ID_PRM_CMP      NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "ID_PRM_CMP_DTL  NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9)," & _
    "PRIMARY KEY (ID_STR_RT, ID_WS, DC_DY_BSN, AI_TRN, AI_LN_ITM) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
 " )"
                Dim cmd30 As Integer = t26.ExecuteNonQuery
                cmd30 = cmd30 * -1

                t27.Connection = conn
                t27.CommandText = "CREATE TABLE TR_LTM_PRM" & _
  "(" & _
    "ID_STR_RT      VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "ID_WS         VARCHAR2(3 BYTE) NOT NULL ENABLE," & _
    "DC_DY_BSN      VARCHAR2(10 BYTE) NOT NULL ENABLE," & _
    "AI_TRN         NUMBER(*,0) NOT NULL ENABLE," & _
    "AI_LN_ITM      NUMBER(*,0) NOT NULL ENABLE," & _
    "AI_LTM_PRM     NUMBER(*,0) NOT NULL ENABLE," & _
    "ID_PRM         NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "ID_PRM_CMP     NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "ID_PRM_CMP_DTL NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE," & _
    "MO_MDFR_RT_PRC NUMBER(16,5) DEFAULT 0 NOT NULL ENABLE," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9)," & _
    "PRIMARY KEY (ID_STR_RT, ID_WS, DC_DY_BSN, AI_TRN, AI_LN_ITM) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
 " )"

                Dim cmd31 As Integer = t27.ExecuteNonQuery
                cmd31 = cmd31 * -1

                t28.Connection = conn
                t28.CommandText = "CREATE TABLE TR_LTM_RTL_TRN" & _
  "(" & _
    "ID_STR_RT VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "ID_WS     VARCHAR2(3 BYTE) NOT NULL ENABLE," & _
    "DC_DY_BSN VARCHAR2(10 BYTE) NOT NULL ENABLE," & _
    "AI_TRN    NUMBER(*,0) NOT NULL ENABLE," & _
    "AI_LN_ITM NUMBER(*,0) NOT NULL ENABLE," & _
    "TY_LN_ITM VARCHAR2(20 BYTE)," & _
    "TS_LN_ITM_BGN TIMESTAMP (9)," & _
    "TS_LN_ITM_END TIMESTAMP (9)," & _
    "FL_VD_LN_ITM CHAR(1 BYTE) DEFAULT '0'," & _
    "AI_LN_ITM_VD NUMBER(*,0) DEFAULT -1," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9)," & _
    "PRIMARY KEY (ID_STR_RT, ID_WS, DC_DY_BSN, AI_TRN, AI_LN_ITM) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
 " )"
                Dim cmd32 As Integer = t28.ExecuteNonQuery
                cmd32 = cmd32 * -1

                t29.Connection = conn
                t29.CommandText = "CREATE TABLE TR_LTM_SLS_RTN" & _
  "(" & _
    "ID_STR_RT             VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "ID_WS                VARCHAR2(3 BYTE) NOT NULL ENABLE," & _
    "DC_DY_BSN            VARCHAR2(10 BYTE) NOT NULL ENABLE," & _
    "AI_TRN                NUMBER(*,0) NOT NULL ENABLE," & _
    "AI_LN_ITM            NUMBER(*,0) NOT NULL ENABLE," & _
    "ID_REGISTRY           VARCHAR2(14 BYTE)," & _
    "ID_GP_TX              NUMBER(*,0) DEFAULT 0," & _
    "ID_DPT_POS            VARCHAR2(14 BYTE)," & _
    "ID_ITM_POS            VARCHAR2(14 BYTE)," & _
    "ID_ITM                VARCHAR2(14 BYTE)," & _
    "QU_ITM_LM_RTN_SLS    NUMBER(9,2) DEFAULT 0," & _
    "MO_EXTN_LN_ITM_RTN    NUMBER(13,2) DEFAULT 0," & _
    "MO_EXTN_DSC_LN_ITM   NUMBER(13,2) DEFAULT 0," & _
    "MO_VAT_LN_ITM_RTN     NUMBER(13,2) DEFAULT 0," & _
    "MO_FE_RSTK            NUMBER(13,2)," & _
    "FL_RTN_MR            CHAR(1 BYTE) DEFAULT '0'," & _
    "RC_RTN_MR            VARCHAR2(2 BYTE)," & _
    "FL_RFD_SV            CHAR(1 BYTE) DEFAULT '0'," & _
    "RC_RFD_SV            VARCHAR2(2 BYTE)," & _
    "LU_MTH_ID_ENR        VARCHAR2(4 BYTE)," & _
    "LU_ENTR_RT_PRC        VARCHAR2(4 BYTE)," & _
    "LU_PRC_RT_DRVN        VARCHAR2(4 BYTE)," & _
    "QU_ITM_LN_RTN         NUMBER(9,2) DEFAULT 0," & _
    "ID_TRN_ORG            VARCHAR2(20 BYTE)," & _
    "DC_DY_BSN_ORG        VARCHAR2(10 BYTE)," & _
    "AI_LN_ITM_ORG        NUMBER(*,0) DEFAULT -1," & _
    "ID_STR_RT_ORG         VARCHAR2(5 BYTE)," & _
    "ID_NMB_SRZ            VARCHAR2(40 BYTE)," & _
    "LU_KT_ST             VARCHAR2(20 BYTE) DEFAULT '0'," & _
    "LU_KT_HDR_RFN_ID       NUMBER(*,0)," & _
    "ID_CLN                VARCHAR2(14 BYTE)," & _
    "FL_SND               CHAR(1 BYTE) DEFAULT '0'," & _
    "CNT_SND_LAB           NUMBER(*,0) DEFAULT 0," & _
    "ID_SHP_MTH            NUMBER(*,0)," & _
    "SPL_INSTRC            VARCHAR2(120 BYTE)," & _
    "ADS_SHP               VARCHAR2(80 BYTE)," & _
    "FL_RCV_GF             CHAR(1 BYTE) DEFAULT '0'," & _
    "OR_ID_REF             NUMBER(*,0) DEFAULT 0," & _
    "FL_VD_LN_ITM          CHAR(1 BYTE) DEFAULT '0'," & _
    "FL_MDFR_RTL_PRC      CHAR(1 BYTE) DEFAULT '0'," & _
    "ED_SZ                VARCHAR2(10 BYTE)," & _
    "FL_ITM_PRC_ADJ        CHAR(1 BYTE) DEFAULT '0'," & _
    "LU_PRC_ADJ_RFN_ID     NUMBER(*,0)," & _
    "FL_RLTD_ITM_RTN       CHAR(1 BYTE)," & _
    "AI_LN_ITM_RLTD        NUMBER(*,0)," & _
    "FL_RLTD_ITM_RM        CHAR(1 BYTE) DEFAULT '1' NOT NULL ENABLE," & _
    "FL_RTRVD_TRN          CHAR(1 BYTE) DEFAULT '0'," & _
    "MO_TAX_INC_LN_ITM_RTN NUMBER(13,2) DEFAULT 0," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9)," & _
    "FL_SLS_ASSC_MDF CHAR(1 BYTE) DEFAULT '0'," & _
    "MO_PRN_PRC      NUMBER(8,2) DEFAULT 0," & _
    "PRIMARY KEY (ID_STR_RT, ID_WS, DC_DY_BSN, AI_TRN, AI_LN_ITM) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
 " )"

                Dim cmd33 As Integer = t29.ExecuteNonQuery
                cmd33 = cmd33 * -1

                t30.Connection = conn
                t30.CommandText = "CREATE TABLE TR_LTM_SLS_RTN_RECEIPT" & _
  "(" & _
    "ID_STR_RT             VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "ID_WS                VARCHAR2(3 BYTE) NOT NULL ENABLE," & _
    "DC_DY_BSN            VARCHAR2(10 BYTE) NOT NULL ENABLE," & _
    "AI_TRN                NUMBER(*,0) NOT NULL ENABLE," & _
    "AI_LN_ITM            NUMBER(*,0) NOT NULL ENABLE," & _
    "ID_REGISTRY           VARCHAR2(14 BYTE)," & _
    "ID_GP_TX              NUMBER(*,0) DEFAULT 0," & _
    "ID_DPT_POS            VARCHAR2(14 BYTE)," & _
    "ID_ITM_POS            VARCHAR2(14 BYTE)," & _
    "ID_ITM                VARCHAR2(14 BYTE)," & _
    "QU_ITM_LM_RTN_SLS    NUMBER(9,2) DEFAULT 0," & _
    "MO_EXTN_LN_ITM_RTN    NUMBER(13,2) DEFAULT 0," & _
    "MO_EXTN_DSC_LN_ITM   NUMBER(13,2) DEFAULT 0," & _
    "MO_VAT_LN_ITM_RTN     NUMBER(13,2) DEFAULT 0," & _
    "MO_FE_RSTK            NUMBER(13,2)," & _
    "FL_RTN_MR            CHAR(1 BYTE) DEFAULT '0'," & _
    "RC_RTN_MR            VARCHAR2(2 BYTE)," & _
    "FL_RFD_SV            CHAR(1 BYTE) DEFAULT '0'," & _
    "RC_RFD_SV            VARCHAR2(2 BYTE)," & _
    "LU_MTH_ID_ENR        VARCHAR2(4 BYTE)," & _
    "LU_ENTR_RT_PRC        VARCHAR2(4 BYTE)," & _
    "LU_PRC_RT_DRVN        VARCHAR2(4 BYTE)," & _
    "QU_ITM_LN_RTN         NUMBER(9,2) DEFAULT 0," & _
    "ID_TRN_ORG            VARCHAR2(20 BYTE)," & _
    "DC_DY_BSN_ORG        VARCHAR2(10 BYTE)," & _
    "AI_LN_ITM_ORG        NUMBER(*,0) DEFAULT -1," & _
    "ID_STR_RT_ORG         VARCHAR2(5 BYTE)," & _
    "ID_NMB_SRZ            VARCHAR2(40 BYTE)," & _
    "LU_KT_ST             VARCHAR2(20 BYTE) DEFAULT '0'," & _
    "LU_KT_HDR_RFN_ID       NUMBER(*,0)," & _
    "ID_CLN                VARCHAR2(14 BYTE)," & _
    "FL_SND               CHAR(1 BYTE) DEFAULT '0'," & _
    "CNT_SND_LAB           NUMBER(*,0) DEFAULT 0," & _
    "ID_SHP_MTH            NUMBER(*,0)," & _
    "SPL_INSTRC            VARCHAR2(120 BYTE)," & _
    "ADS_SHP               VARCHAR2(80 BYTE)," & _
    "FL_RCV_GF             CHAR(1 BYTE) DEFAULT '0'," & _
    "OR_ID_REF             NUMBER(*,0) DEFAULT 0," & _
    "FL_VD_LN_ITM          CHAR(1 BYTE) DEFAULT '0'," & _
    "FL_MDFR_RTL_PRC      CHAR(1 BYTE) DEFAULT '0'," & _
    "ED_SZ                VARCHAR2(10 BYTE)," & _
    "FL_ITM_PRC_ADJ        CHAR(1 BYTE) DEFAULT '0'," & _
    "LU_PRC_ADJ_RFN_ID     NUMBER(*,0)," & _
    "FL_RLTD_ITM_RTN       CHAR(1 BYTE)," & _
    "AI_LN_ITM_RLTD        NUMBER(*,0)," & _
    "FL_RLTD_ITM_RM        CHAR(1 BYTE) DEFAULT '1' NOT NULL ENABLE," & _
    "FL_RTRVD_TRN          CHAR(1 BYTE) DEFAULT '0'," & _
    "MO_TAX_INC_LN_ITM_RTN NUMBER(13,2) DEFAULT 0," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9)," & _
    "FL_SLS_ASSC_MDF CHAR(1 BYTE) DEFAULT '0'," & _
    "MO_PRN_PRC      NUMBER(8,2) DEFAULT 0," & _
    "PRIMARY KEY (ID_STR_RT, ID_WS, DC_DY_BSN, AI_TRN, AI_LN_ITM) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
 " )"
                Dim cmd34 As Integer = t30.ExecuteNonQuery
                cmd34 = cmd34 * -1



                t31.Connection = conn
                t31.CommandText = "CREATE TABLE TR_TRN" & _
  "(" & _
    "ID_STR_RT VARCHAR2(5 BYTE) NOT NULL ENABLE," & _
    "ID_WS     VARCHAR2(3 BYTE) NOT NULL ENABLE," & _
    "DC_DY_BSN  VARCHAR2(10 BYTE) NOT NULL ENABLE," & _
    "AI_TRN    NUMBER(*,0) NOT NULL ENABLE," & _
    "ID_OPR    VARCHAR2(10 BYTE)," & _
    "TY_TRN     VARCHAR2(20 BYTE)," & _
    "TS_TM_SRT  TIMESTAMP (9)," & _
    "TS_TRN_BGN TIMESTAMP (9)," & _
    "TS_TRN_END  TIMESTAMP (9)," & _
    "FL_TRG_TRN    CHAR(1 BYTE) DEFAULT '0'," & _
    "FL_KY_OFL    CHAR(1 BYTE) DEFAULT '0'," & _
    "SC_TRN        NUMBER(*,0) DEFAULT 2," & _
    "ID_EM        VARCHAR2(10 BYTE)," & _
    "INF_CT        VARCHAR2(14 BYTE)," & _
    "TY_INF_CT     NUMBER(*,0) DEFAULT 0," & _
    "ID_RPSTY_TND   VARCHAR2(10 BYTE)," & _
    "ID_CNY_ICD    NUMBER(*,0)," & _
    "ID_TLOG_BTCH NUMBER(*,0) DEFAULT -1," & _
    "ID_BTCH_ARCH  VARCHAR2(14 BYTE) DEFAULT '-1'," & _
    "ID_RTLOG_BTCH VARCHAR2(14 BYTE) DEFAULT '-1'," & _
    "SC_PST_PRCS   NUMBER(*,0) DEFAULT 0," & _
    "FL_TRE_TRN    CHAR(1 BYTE) DEFAULT '0'," & _
    "TS_CRT_RCRD TIMESTAMP (9)," & _
    "TS_MDF_RCRD TIMESTAMP (9)," & _
    "FL_SLS_ASSC_MDF CHAR(1 BYTE) DEFAULT '0'," & _
    "PRIMARY KEY (ID_STR_RT, ID_WS, DC_DY_BSN, AI_TRN) USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645 PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT) TABLESPACE USERS ENABLE" & _
  ")"
                Dim cmd35 As Integer = t31.ExecuteNonQuery
                cmd35 = cmd35 * -1

                t32.Connection = conn
                t32.CommandText = "CREATE TABLE USR_PASSWORD" & _
  "(" & _
    "EMP_ID   VARCHAR2(20 BYTE)," & _
    "PASSWORD VARCHAR2(200 BYTE)" & _
  ")"
                Dim cmd36 As Integer = t32.ExecuteNonQuery
                cmd36 = cmd36 * -1

                t33.Connection = conn
                t33.CommandText = "CREATE TABLE VALIDITY" & _
  "(" & _
    "BARCODE       VARCHAR2(50 BYTE)," & _
    "VALIDITY_DATE VARCHAR2(50 BYTE)" & _
 " )"
                Dim cmd37 As Integer = t33.ExecuteNonQuery
                cmd37 = cmd37 * -1

                t34.Connection = conn
                t34.CommandText = "CREATE TABLE  SERIAL" & _
  "(" & _
    "SN VARCHAR2(500 BYTE)," & _
    "WS number," & _
    "WS_STATUS VARCHAR2(500 BYTE)" & _
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
                insert3.CommandText = "insert into pos_info values('...','...','...','...','...','1','0')"

                Dim cmd41 As Integer = insert3.ExecuteNonQuery
                'cmd41 = cmd41 * -1

                insert4.Connection = conn
                insert4.CommandText = "INSERT INTO PA_EM (ID_EM, ID_PRTY, ID_LOGIN, ID_ALT, PW_ACS_EM, NM_EM, LN_EM, " & _
"FN_EM, MD_EM, UN_NMB_SCL_SCTY, SC_EM, ID_GP_WRK, LCL, NUMB_DYS_VLD, TYPE_EMP, FL_PW_NW_REQ, TS_CRT_PW, NUMB_FLD_PW) " & _
"VALUES('admin', '1', 'system', 'admin', 'd033e22ae348aeb5660fc2140aec35850c4da997', 'administrator','system', '', 'administrator', '736352826', '1', '8', 'en_US', '0', '0', '1', " & _
"TO_TIMESTAMP('24-SEP-13 09.08.56.741000000 AM', 'DD-MON-RR HH.MI.SS.FF AM'), '0')"
                Dim cmd42 As Integer = insert4.ExecuteNonQuery
                'cmd42 = cmd42 * -1



                t35.Connection = conn
                t35.CommandText = "CREATE TABLE RECEIPT_NUMBERS_WS" & _
  "(" & _
    "SYSTEM_DATE      VARCHAR2(100 BYTE)," & _
    "RECEIPT_NUMBER    NUMBER," & _
    "RECEIPT_REFERENCE VARCHAR2(100 BYTE)," & _
    "QTY              VARCHAR2(20 BYTE)," & _
    "TOTAL            VARCHAR2(20 BYTE), " & _
     "VAT           VARCHAR2(20 BYTE), " & _
    "EMP               VARCHAR2(20 BYTE)," & _
    "TENDER_TYPE        VARCHAR2(20 BYTE), " & _
    "OPS_TYPE         VARCHAR2(20 BYTE), " & _
    "OPS_TIME        VARCHAR2(200 BYTE)," & _
    "BUSINESS_DATE    VARCHAR2(200 BYTE)," & _
    "CRDT_INFO         VARCHAR2(1000 BYTE)," & _
    "WS         VARCHAR2(500 BYTE)" & _
  ")"

                Dim cmd43 As Integer = t35.ExecuteNonQuery
                cmd43 = cmd43 * -1


                Dim t36 As New OracleCommand
                t36.Connection = conn
                t36.CommandText = "create table BxGy" & _
                "(" & _
                "Barcode varchar2(20)," & _
                "X varchar2(20)," & _
                "Y varchar2(20)," & _
                "PROMOTION_ID varchar2(10)," & _
                "START_DATE varchar2(50)," & _
                "END_DATE varchar2(50)," & _
                "CREATION_DATE varchar2(50)" & _
                ")"

                Dim cmd44 As Integer = t36.ExecuteNonQuery
                cmd44 = cmd44 * -1


                Dim t37 As New OracleCommand
                t37.Connection = conn
                t37.CommandText = "create table PROMO_PER" & _
                "(" & _
                "Barcode varchar2(20)," & _
                "PER varchar2(10)," & _
                "PROMOTION_ID varchar2(10)," & _
                "START_DATE varchar2(50)," & _
                "END_DATE varchar2(50)," & _
                "CREATION_DATE varchar2(50)" & _
                ")"

                Dim cmd45 As Integer = t37.ExecuteNonQuery
                cmd45 = cmd45 * -1

                Dim t38 As New OracleCommand
                t38.Connection = conn
                t38.CommandText = "create table PROMO_ONEPRICE" & _
                "(" & _
                "Barcode varchar2(20)," & _
                "PRICE varchar2(10)," & _
                "PROMOTION_ID varchar2(10)," & _
                "START_DATE varchar2(50)," & _
                "END_DATE varchar2(50)," & _
                "CREATION_DATE varchar2(50)" & _
                ")"

                Dim cmd46 As Integer = t38.ExecuteNonQuery
                cmd46 = cmd46 * -1

                Dim t39 As New OracleCommand
                t39.Connection = conn
                t39.CommandText = "create table PROMOTIONS_ID" & _
                "(" & _
                "PROMOTION_ID varchar2(10)," & _
                "PROMOTION_NAME varchar2(50)," & _
                "CREATED_BY varchar2(50)," & _
                "CREATION_DATE varchar2(50)" & _
                ")"

                Dim cmd47 As Integer = t39.ExecuteNonQuery
                cmd47 = cmd47 * -1

                Dim t40 As New OracleCommand
                t40.Connection = conn
                t40.CommandText = "create table DSF" & _
                "(" & _
                 "Store_Code varchar2(100)," & _
                "Itm_number varchar2(20)," & _
                "Itm_name varchar2(200)," & _
                "Itm_price varchar2(20)," & _
                "Business_Date varchar2(100)," & _
                "Scan_time Varchar2(100)," & _
                "Ops_type varchar2(100)," & _
                "Payment varchar2(100)," & _
                "User_Id varchar2(100)" & _
                ")"

                Dim cmd48 As Integer = t40.ExecuteNonQuery
                cmd48 = cmd48 * -1

                Dim t41 As New OracleCommand
                t41.Connection = conn
                t41.CommandText = "CREATE TABLE T_T" & _
  "(" & _
    "T_T_FLAG  VARCHAR2(20 BYTE)," & _
    "TIME_STAMP VARCHAR2(50 BYTE)" & _
 ")"

                Dim cmd49 As Integer = t41.ExecuteNonQuery
                cmd49 = cmd49 * -1

                Dim t42 As New OracleCommand
                t42.Connection = conn
                t42.CommandText = "CREATE TABLE LOCATIONS_MANAGER" & _
  "(" & _
    "LOCATION_NUMBER VARCHAR2(20 BYTE)," & _
    "LOCATION_NAME   VARCHAR2(100 BYTE)," & _
    "LOCATION_ADDRESS VARCHAR2(200 BYTE)," & _
    "LOCATION_TEL     VARCHAR2(50 BYTE)" & _
 ")"


                Dim cmd50 As Integer = t42.ExecuteNonQuery
                cmd50 = cmd50 * -1


                Dim t43 As New OracleCommand
                t43.Connection = conn
                t43.CommandText = "CREATE TABLE TRANSFER_MANAGER" & _
  "(" & _
    "FROM_NUMBER     VARCHAR2(20 BYTE)," & _
     "FROM_NAME       VARCHAR2(100 BYTE)," & _
    "TO_NUMBER      VARCHAR2(20 BYTE), " & _
    "TO_NAME        VARCHAR2(100 BYTE)," & _
   "TRANSFER_NUMBER   VARCHAR2(20 BYTE)," & _
    "TRANSFER_DATE   VARCHAR2(50 BYTE)," & _
    "STOCK_CATEGORY  VARCHAR2(100 BYTE), " & _
    "STOCK_SEASON    VARCHAR2(100 BYTE)," & _
       "TRANSER_REASON  VARCHAR2(100 BYTE)," & _
          "STATUS          VARCHAR2(50 BYTE)" & _
 ")"


                Dim cmd51 As Integer = t43.ExecuteNonQuery
                cmd51 = cmd51 * -1

                Dim t44 As New OracleCommand
                t44.Connection = conn
                t44.CommandText = "CREATE TABLE SCANNED_ITEMS" & _
  "(" & _
    "TRANSFER_NUMBER VARCHAR2(20 BYTE)," & _
     "BOX_ID          VARCHAR2(20 BYTE)," & _
   "ITM_BARCODE     VARCHAR2(20 BYTE)," & _
  "ITM_NAME        VARCHAR2(100 BYTE)," & _
   "QTY             VARCHAR2(20 BYTE)," & _
     "R_I            VARCHAR2(50 BYTE) " & _
    ")"


                Dim cmd52 As Integer = t44.ExecuteNonQuery
                cmd52 = cmd52 * -1


                insert1.Connection = conn
                insert1.CommandText = "insert into T_T values('1984',sysdate)"
                insert1.ExecuteNonQuery()

                Dim t45 As New OracleCommand
                t45.Connection = conn
                t45.CommandText = "CREATE TABLE PRINTER_STATUS" & _
  "(" & _
    "STATUS VARCHAR2(10 BYTE)" & _
       ")"


                Dim cmd53 As Integer = t45.ExecuteNonQuery
                cmd53 = cmd53 * -1

                Dim t46 As New OracleCommand
                t46.Connection = conn
                t46.CommandText = "CREATE TABLE BACKGROUND" & _
  "(" & _
    "COLOR VARCHAR2(20 BYTE)" & _
       ")"


                Dim cmd54 As Integer = t46.ExecuteNonQuery
                cmd54 = cmd54 * -1


                Dim t47 As New OracleCommand
                t47.Connection = conn
                t47.CommandText = "CREATE TABLE RECEIPT_LANGUEGE" & _
  "(" & _
    "LANG VARCHAR2(20 BYTE)" & _
       ")"

                Dim cmd55 As Integer = t47.ExecuteNonQuery
                cmd55 = cmd55 * -1


                Dim t48 As New OracleCommand
                t48.Connection = conn
                t48.CommandText = "CREATE TABLE BXGD" & _
  "(" & _
    "BARCODE      VARCHAR2(20 BYTE)," & _
    "X           VARCHAR2(20 BYTE)," & _
    "Y           VARCHAR2(20 BYTE)," & _
    "TEMP          VARCHAR2(20 BYTE)," & _
    "PROMO_ID      VARCHAR2(50 BYTE)," & _
    "START_DATE    VARCHAR2(50 BYTE)," & _
    "END_DATE      VARCHAR2(50 BYTE)," & _
    "CREATION_DATE VARCHAR2(50 BYTE)" & _
       ")"



                Dim cmd56 As Integer = t48.ExecuteNonQuery
                cmd56 = cmd56 * -1

                Dim t49 As New OracleCommand
                t49.Connection = conn
                t49.CommandText = "CREATE TABLE PriceBasedonWeight" & _
  "(" & _
    "ITM_ID VARCHAR2(50)," & _
    "ITM_NAME VARCHAR2(200)," & _
    "UNIT VARCHAR2(100)," & _
    "PRICE_UNIT VARCHAR2(100)" & _
       ")"



                Dim cmd57 As Integer = t49.ExecuteNonQuery
                cmd57 = cmd57 * -1

                insert_lang.Connection = conn
                insert_lang.CommandText = "insert into RECEIPT_LANGUEGE values('1')"
                insert_lang.ExecuteNonQuery()

                insert_printer.Connection = conn
                insert_printer.CommandText = "insert into PRINTER_STATUS values('2')"
                insert_printer.ExecuteNonQuery()

                insert_color.Connection = conn
                insert_color.CommandText = "insert into BACKGROUND values('Cyan')"
                insert_color.ExecuteNonQuery()

                taskbar = taskbar + cmd17 + cmd18 + cmd19 + cmd20 + cmd21 + cmd22 + cmd23 + cmd24 + cmd25 + cmd26 + cmd27 + cmd28 + cmd29 + cmd30 + cmd31 + cmd32 + cmd33 + cmd34 + cmd35 + cmd36 + cmd37 + cmd38 + cmd39 + cmd40 + cmd41 + cmd42 + cmd43 + cmd44 + cmd45 + cmd46 + cmd47 + cmd48 + cmd49 + cmd50 + cmd51 + cmd52 + cmd53 + cmd54 + cmd55 + cmd56 + cmd57








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