<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintSummaryUC.ascx.cs" Inherits="HPF.FutureState.Web.PrintSummary.PrintSummaryUC" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<rsweb:ReportViewer ID="ReportViewerPrintSummary" runat="server" Width="100%" 
    Font-Names="Verdana" Font-Size="8pt" Height="400px" ProcessingMode="Remote">
    <ServerReport ReportServerUrl="http://HPF-01:8080/reportservers" />
    
</rsweb:ReportViewer>
