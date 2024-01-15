Imports Microsoft.Reporting.WinForms
Public Class sales_by_receipt
    Private Sub sales_by_receipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Dim paramters As IList(Of ReportParameter) = New List(Of ReportParameter)
            paramters.Add(New ReportParameter("storecode", report_manager.store_code))
            paramters.Add(New ReportParameter("storename", report_manager.store_name))
            paramters.Add(New ReportParameter("companyname", report_manager.company_name))
            paramters.Add(New ReportParameter("todate", report_manager.to_date))
            paramters.Add(New ReportParameter("fromdate", report_manager.from_date))
            paramters.Add(New ReportParameter("total", report_manager.Label5.Text))
            ReportViewer1.LocalReport.SetParameters(paramters)
            Me.ReportViewer1.RefreshReport()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class