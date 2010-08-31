<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUploads.ascx.cs" Inherits="HPF.FutureState.Web.QCSelectionCaseDetail.FileUploads" %>
<div class="Text">
    <asp:Label ID="lblErrorMessage" runat="server" CssClass="ErrorMessage"></asp:Label><br />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="ErrorMessage"
             runat="server" ErrorMessage="Only zip file is allowed!"
             ValidationExpression ="^.+(.xls|.XLS|.xlsx|.XLSX|.doc|.docx|.DOC|.DOCX|.pdf|.PDF)$"
             ControlToValidate="fileUpload"> 
        </asp:RegularExpressionValidator>
</div>
<table width="100%" border="1" style="border-collapse:collapse">
    <tr>
        <td class="sidelinks" align="left" style="width:15%">Auditor Name: </td>
        <td class="sidelinks" align="center"></td>
    </tr>
    <!-- List file upload -->
    <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
    <tr>
        <td align="left">Upload File</td>
        <td align="left"><asp:FileUpload ID="fileUpload" runat="server" BackColor="#EBEBE4" 
                            CssClass="Text" Height="18px" onkeydown="return false;" 
                            onkeypress="return false;" Width="400px" />
                            <asp:Button ID='btnUpload' runat='server' Text='Upload' Width='120px' 
                        CssClass='MyButton' onclick="btnUpload_Click"/>
                            </td>
    </tr>
</table>