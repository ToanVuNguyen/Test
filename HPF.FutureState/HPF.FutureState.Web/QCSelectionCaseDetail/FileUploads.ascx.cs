using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Web.Security;
using System.Text;

namespace HPF.FutureState.Web.QCSelectionCaseDetail
{
    public partial class FileUploads : System.Web.UI.UserControl
    {
        private CaseEvalSearchResultDTO caseEval;
        private const string saveDir = "C:/QCTest/";
        private const string filePath = "/agencies/{0}/QC_Audit/{1}/{2}/";
        private const int maxUploadFiles = 5;
        private int totalFile;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int caseId = int.Parse(Request.QueryString["CaseID"].ToString());
                caseEval = CaseEvaluationBL.Instance.SearchCaseEvalByFcId(caseId);
                RenderFileUpload(caseId);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void RenderFileUpload(int fcId)
        {
            CaseEvalFileDTOCollection files = CaseEvaluationBL.Instance.GetCaseEvalFileByEvalHeaderIdAll(caseEval.CaseEvalHeaderId);
            totalFile = files.Count;
            int i = 1;
            foreach (CaseEvalFileDTO file in files)
            {
                RenderRow(file.FileName,file.FilePath);
                i++;
            }
            if (totalFile == maxUploadFiles) btnUpload.Enabled = false;
        }
        private void RenderRow(string fileName,string filePath)
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            Label lblFileOrder = new Label();
            lblFileOrder.Attributes.Add("style", "sidelinks");
            lblFileOrder.Text = "File";
            tc.Controls.Add(lblFileOrder);
            tr.Controls.Add(tc);
            tc = new TableCell();
            HyperLink hl = new HyperLink();
            hl.Attributes.Add("style", "Text");
            hl.NavigateUrl ="file:///"+ saveDir + fileName;
            hl.Text = fileName;
            hl.Target = "_blank";
            tc.Controls.Add(hl);
            tr.Controls.Add(tc);
            placeHolder.Controls.Add(tr);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                try
                {
                    CaseEvalFileDTO evalFile = new CaseEvalFileDTO();
                    StringBuilder fileUploadPath = new StringBuilder();
                    fileUpload.SaveAs(saveDir + fileUpload.FileName);
                    fileUploadPath.AppendFormat(filePath, caseEval.AgencyName, caseEval.EvaluationYearMonth, caseEval.FcId.ToString());
                    evalFile.CaseEvalHeaderId = caseEval.CaseEvalHeaderId;
                    evalFile.FileName = fileUpload.FileName;
                    evalFile.FilePath = fileUploadPath.ToString();
                    CaseEvaluationBL.Instance.InsertCaseEvalFile(evalFile,caseEval, HPFWebSecurity.CurrentIdentity.LoginName);
                    RenderRow(evalFile.FileName, evalFile.FilePath);
                }
                catch (Exception ex)
                {
                    lblErrorMessage.Text = ex.Message;
                    ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
                }
            }
            else
                lblErrorMessage.Text = "Choose file to upload!!!";
        }
    }
}