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
using HPF.FutureState.Common;
using System.Text.RegularExpressions;
using System.IO;

namespace HPF.FutureState.Web.QCSelectionCaseDetail
{
    public partial class FileUploads : System.Web.UI.UserControl
    {
        private CaseEvalSearchResultDTO caseEval;
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
                RenderRow(i.ToString(), file.FileName,file.FilePath,file.CreateUserId);
                i++;
            }
            if (totalFile >= maxUploadFiles) btnUpload.Enabled = false;
            btnUploadFinished.Visible = (string.Compare(caseEval.EvalStatus, CaseEvaluationBL.EvaluationStatus.AGENCY_UPLOAD_REQUIRED) == 0 ? true : false);
        }
        private void RenderRow(string order,string fileName,string filePath,string createUser)
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            Label lblFileOrder = new Label();
            lblFileOrder.Attributes.Add("style", "sidelinks");
            lblFileOrder.Text = "File "+order;
            tc.Controls.Add(lblFileOrder);
            tr.Controls.Add(tc);
            tc = new TableCell();
            HyperLink hl = new HyperLink();
            hl.Attributes.Add("style", "Text");
            StringBuilder navigateUrl = new StringBuilder();
            navigateUrl.AppendFormat("{0}://{1}{2}/{3}{4}{5}",Request.Url.Scheme, Request.ServerVariables["HTTP_HOST"],Request.ApplicationPath, HPFConfigurationSettings.HPF_QC_FILE_UPLOAD_PATH, filePath, fileName);
            hl.NavigateUrl = navigateUrl.ToString();
            hl.Text = fileName;
            hl.Target = "_blank";
            tc.Controls.Add(hl);
            Label lblCreateUser = new Label();
            lblCreateUser.Attributes.Add("style", "Text");
            lblCreateUser.Text = " - uploaded by " + createUser;
            tc.Controls.Add(lblCreateUser);
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
                    //Check file extension
                    StringBuilder expresstion = new StringBuilder();
                    StringBuilder errorMessage = new StringBuilder();
                    expresstion.Append("^.+(");
                    errorMessage.AppendFormat("Only {0} are allowed", HPFConfigurationSettings.HPF_QC_FILE_UPLOAD_EXTENSTION);
                    string[] extList = HPFConfigurationSettings.HPF_QC_FILE_UPLOAD_EXTENSTION.Split(',');
                    foreach (string ext in extList)
                    {
                        expresstion.AppendFormat(".{0}|", ext);
                    }
                    expresstion.Remove(expresstion.Length - 1, 1);
                    expresstion.Append(")$");
                    Regex rxValidate = new Regex(expresstion.ToString(),RegexOptions.IgnoreCase);
                    if (!rxValidate.IsMatch(fileUpload.FileName))
                        throw new Exception(errorMessage.ToString());
                    //End check file extension
                    fileUploadPath.AppendFormat("{0}/{1}-{2}/{3}", caseEval.AgencyName, caseEval.EvaluationYearMonth.Substring(4, 2), caseEval.EvaluationYearMonth.Substring(0, 4), caseEval.FcId.ToString());
                    string folder = EnsureFolderName(fileUploadPath.ToString());
                    string fullPath = Server.MapPath(HPFConfigurationSettings.HPF_QC_FILE_UPLOAD_PATH) + folder + fileUpload.FileName;
                    if (!File.Exists(fullPath))
                    {
                        evalFile.CaseEvalHeaderId = caseEval.CaseEvalHeaderId;
                        evalFile.FileName = fileUpload.FileName;
                        evalFile.FilePath = folder;
                        evalFile.SetInsertTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
                        CaseEvaluationBL.Instance.InsertCaseEvalFile(evalFile, caseEval, HPFWebSecurity.CurrentIdentity.LoginName);
                        if (totalFile >= maxUploadFiles)
                            if ((string.Compare(HPFWebSecurity.CurrentIdentity.UserType, Constant.USER_TYPE_AGENCY) == 0)
                                && (string.Compare(caseEval.EvalStatus, CaseEvaluationBL.EvaluationStatus.AGENCY_UPLOAD_REQUIRED) == 0))
                                UploadFinished();
                            else
                                btnUpload.Enabled = false;
                        else totalFile++;
                        RenderRow(totalFile.ToString(), evalFile.FileName, evalFile.FilePath, HPFWebSecurity.CurrentIdentity.LoginName);
                    }
                    fileUpload.SaveAs(fullPath);
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

        protected void btnUploadFinished_Click(object sender, EventArgs e)
        {
            try
            {
                if (totalFile > 0)
                    UploadFinished();
                else
                    lblErrorMessage.Text = "Error: At least one file uploaded before finishing upload !";
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex, HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        private void UploadFinished()
        {
            CaseEvalHeaderDTO caseEvalHeader = new CaseEvalHeaderDTO();
            caseEvalHeader.CaseEvalHeaderId = caseEval.CaseEvalHeaderId;
            caseEvalHeader.EvalStatus =CaseEvaluationBL.EvaluationStatus.HPF_INPUT_REQUIRED;
            caseEvalHeader.SetUpdateTrackingInformation(HPFWebSecurity.CurrentIdentity.LoginName);
            CaseEvaluationBL.Instance.UpdateCaseEvalHeader(caseEvalHeader);
            Response.Redirect(Request.Url.ToString());
        }
        private string EnsureFolderName(string folder)
        {
            StringBuilder root = new StringBuilder();
            string uploadDirectory = Server.MapPath(HPFConfigurationSettings.HPF_QC_FILE_UPLOAD_PATH);
            root.Append(uploadDirectory);
            foreach (string foldername in folder.Split(new Char[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
            {
                root.AppendFormat("{0}/", foldername);
                if (!Directory.Exists(root.ToString()))
                {
                    Directory.CreateDirectory(root.ToString());
                }
            }
            //return relative directory
            return root.ToString().Remove(0,uploadDirectory.Length);
        }
    }
}