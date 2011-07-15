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

using System.Collections.Generic;

using System.Xml;
using System.Xml.XPath;
using HPF.Webservice.Agency;
using System.Collections.ObjectModel;

namespace HPF.FutureState.WebService.Test.Web
{
    public partial class SavePrePurchaseCase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDefaultPPCase();
            }
            else
            {
                if (!(String.IsNullOrEmpty(txtPassword.Text.Trim())))
                {
                    txtPassword.Attributes["value"] = txtPassword.Text;
                }
            }
        }

        private void LoadDefaultPPCase()
        {
            string filename = MapPath(ConfigurationManager.AppSettings["PrePurchaseCaseSetXML"]);
            XDocument xdoc = GetXmlDocument(filename);
            BindToForm(xdoc);
        }

        private XDocument GetXmlDocument(string filename)
        {
            XDocument xdoc = XDocument.Load(filename);
            return xdoc;
        }

        private void BindToForm(XDocument xdoc)
        {
            Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION] = AgencyHelper.ParsePPBudgetAssetDTO(xdoc);
            Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION] = AgencyHelper.ParsePPBudgetItemDTO(xdoc);
            Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION] = AgencyHelper.ParseProposedPPBudgetItemDTO(xdoc);
            Session[SessionVariables.PREPURCHASE_CASE] = AgencyHelper.ParsePrePurchaseCaseDTO(xdoc);
            Session[SessionVariables.APPLICANT] = AgencyHelper.ParseApplicant(xdoc);

            RefreshAllGrids();
            PrePurchaseCaseToForm((PrePurchaseCaseDTO)Session[SessionVariables.PREPURCHASE_CASE]);
            ApplicantToForm((ApplicantDTO)Session[SessionVariables.APPLICANT]);
        }
        private PrePurchaseCaseDTO FormToPrePurchaseCase()
        {
            PrePurchaseCaseDTO ppCase = new PrePurchaseCaseDTO();
            ppCase.AgencyId = Util.ConvertToInt(txtAgencyId.Text.Trim());
            ppCase.AgencyCaseNum =Util.ConvertToString(txtAgencyCaseNumber.Text.Trim());
            ppCase.AcctNum = Util.ConvertToString(txtAcctNum.Text.Trim());
            ppCase.MortgageProgramCd = Util.ConvertToString(txtMortgageProgramCd.Text.Trim());
            ppCase.BorrowerFName = Util.ConvertToString(txtBorrowerFName.Text.Trim());
            ppCase.BorrowerLName = Util.ConvertToString(txtBorrowerLName.Text.Trim());
            ppCase.CoBorrowerFName = Util.ConvertToString(txtCoBorrowerFName.Text.Trim());
            ppCase.CoBorrowerLName = Util.ConvertToString(txtCoBorrowerLName.Text.Trim());
            ppCase.PropAddr1 = Util.ConvertToString(txtPropAddr1.Text.Trim());
            ppCase.PropAddr2 = Util.ConvertToString(txtPropAddr2.Text.Trim());
            ppCase.PropCity = Util.ConvertToString(txtPropCity.Text.Trim());
            ppCase.PropStateCd = Util.ConvertToString(txtPropStateCd.Text.Trim());
            ppCase.PropZip = Util.ConvertToString(txtPropZip.Text.Trim());
            ppCase.MailAddr1 = Util.ConvertToString(txtMailAddr1.Text.Trim());
            ppCase.MailAddr2 = Util.ConvertToString(txtMailAddr2.Text.Trim());
            ppCase.MailCity = Util.ConvertToString(txtMailCity.Text.Trim());
            ppCase.MailStateCd = Util.ConvertToString(txtMailStateCd.Text.Trim());
            ppCase.MailZip = Util.ConvertToString(txtMailZip.Text.Trim());
            ppCase.BorrowerAuthorizationInd = Util.ConvertToString(txtBorrowerAuthorizationInd.Text.Trim());
            ppCase.MotherMaidenLName = Util.ConvertToString(txtMotherMaidenLName.Text.Trim());
            ppCase.PrimaryContactNo = Util.ConvertToString(txtPrimaryContactNo.Text.Trim());
            ppCase.SecondaryContactNo = Util.ConvertToString(txtSecondaryContactNo.Text.Trim());
            ppCase.BorrowerEmployerName = Util.ConvertToString(txtBorrowerEmployerName.Text.Trim());
            ppCase.BorrowerJobTitle = Util.ConvertToString(txtBorrowerJobTitle.Text.Trim());
            ppCase.BorrowerSelfEmployedInd = Util.ConvertToString(txtBorrowerSelfEmployedInd.Text.Trim());
            ppCase.BorrowerYearsEmployed = Util.ConvertToDouble(txtBorrowerYearsEmployed.Text.Trim());
            ppCase.CoBorrowerEmployerName = Util.ConvertToString(txtCoBorrowerEmployerName.Text.Trim());
            ppCase.CoBorrowerJobTitle = Util.ConvertToString(txtCoBorrowerJobTitle.Text.Trim());
            ppCase.CoBorrowerSelfEmployedInd = Util.ConvertToString(txtCoBorrowerSelfEmployedInd.Text.Trim());
            ppCase.CoBorrowerYearsEmployed = Util.ConvertToDouble(txtCoBorrowerYearsEmployed.Text.Trim());
            ppCase.CounselorIdRef = Util.ConvertToString(txtCounselorIdRef.Text.Trim());
            ppCase.CounselorFName = Util.ConvertToString(txtCounselorFName.Text.Trim());
            ppCase.CounselorLName = Util.ConvertToString(txtCounselorLName.Text.Trim());
            ppCase.CounselorEmail = Util.ConvertToString(txtCounselorEmail.Text.Trim());
            ppCase.CounselorPhone = Util.ConvertToString(txtCounselorPhone.Text.Trim());
            ppCase.CounselorExt = Util.ConvertToString(txtCounselorExt.Text.Trim());
            ppCase.CounselingDurationMins = Util.ConvertToInt(txtCounselingDurationMins.Text.Trim());
            
            return ppCase;
        }
        private void PrePurchaseCaseToForm(PrePurchaseCaseDTO ppCase)
        {
            if (ppCase != null)
            {
                //if (ppCase.PpcId.HasValue)
                //    txtPpcId.Text = ppCase.PpcId.Value.ToString();
                txtAgencyId.Text=Util.ConvertToString(ppCase.AgencyId);
                txtAgencyCaseNumber.Text = ppCase.AgencyCaseNum;
                txtAcctNum.Text = ppCase.AcctNum;
                txtMortgageProgramCd.Text = ppCase.MortgageProgramCd;
                txtBorrowerFName.Text = ppCase.BorrowerFName;
                txtBorrowerLName.Text = ppCase.BorrowerLName;
                txtCoBorrowerFName.Text = ppCase.CoBorrowerFName;
                txtCoBorrowerLName.Text = ppCase.CoBorrowerLName;
                txtPropAddr1.Text = ppCase.PropAddr1;
                txtPropAddr2.Text = ppCase.PropAddr2;
                txtPropCity.Text = ppCase.PropCity;
                txtPropStateCd.Text = ppCase.PropStateCd;
                txtPropZip.Text = ppCase.PropZip;
                txtMailAddr1.Text = ppCase.MailAddr1;
                txtMailAddr2.Text = ppCase.MailAddr2;
                txtMailCity.Text = ppCase.MailCity;
                txtMailStateCd.Text = ppCase.MailStateCd;
                txtMailZip.Text = ppCase.MailZip;
                txtBorrowerAuthorizationInd.Text = ppCase.BorrowerAuthorizationInd;
                txtMotherMaidenLName.Text = ppCase.MotherMaidenLName;
                txtPrimaryContactNo.Text = ppCase.PrimaryContactNo;
                txtSecondaryContactNo.Text = ppCase.SecondaryContactNo;
                txtBorrowerEmployerName.Text = ppCase.BorrowerEmployerName;
                txtBorrowerJobTitle.Text =  ppCase.BorrowerJobTitle;
                txtBorrowerSelfEmployedInd.Text = ppCase.BorrowerSelfEmployedInd;
                txtBorrowerYearsEmployed.Text =Util.ConvertToString(ppCase.BorrowerYearsEmployed);
                txtCoBorrowerEmployerName.Text = ppCase.CoBorrowerEmployerName;
                txtCoBorrowerJobTitle.Text = ppCase.CoBorrowerJobTitle;
                txtCoBorrowerSelfEmployedInd.Text = ppCase.CoBorrowerSelfEmployedInd;
                txtCounselorIdRef.Text = ppCase.CounselorIdRef;
                txtCounselorFName.Text = ppCase.CounselorFName;
                txtCounselorLName.Text = ppCase.CounselorLName;
                txtCounselorEmail.Text = ppCase.CounselorEmail;
                txtCounselorPhone.Text = ppCase.CounselorPhone;
                txtCounselorExt.Text = ppCase.CounselorExt;
                txtCounselingDurationMins.Text =Util.ConvertToString(ppCase.CounselingDurationMins);
            }
        }
        private ApplicantDTO FormToApplicant()
        {
            ApplicantDTO applicant = new ApplicantDTO();
            applicant.ApplicantId = Util.ConvertToInt(txtApplicantId.Text.Trim());
            applicant.RightPartyContactInd =Util.ConvertToString(txtRightPartyContactInd.Text.Trim());
            applicant.RpcMostRecentDt = Util.ConvertToDateTime(txtRpcMostrecentDt.Text.Trim());
            applicant.NoRpcReason = Util.ConvertToString(txtNoRpcReason.Text.Trim());
            applicant.CounselingAcceptedDt =Util.ConvertToDateTime(txtCounselingAcceptedDt.Text.Trim());
            applicant.CounselingScheduledDt = Util.ConvertToDateTime(txtCounselingScheduledDt.Text.Trim());
            applicant.CounselingCompletedDt = Util.ConvertToDateTime(txtCounselingCompletedDt.Text.Trim());
            applicant.CounselingRefusedDt = Util.ConvertToDateTime(txtCounselingRefusedDt.Text.Trim());
            applicant.FirstCounseledDt = Util.ConvertToDateTime(txtFirstCounseledDt.Text.Trim());
            applicant.SecondCounseledDt = Util.ConvertToDateTime(txtSecondCounseledDt.Text.Trim());
            applicant.EdModuleCompletedDt = Util.ConvertToDateTime(txtEdModuleCompletedDt.Text.Trim());
            applicant.InboundCallToNumDt = Util.ConvertToDateTime(txtInboundCallToNumDt.Text.Trim());
            applicant.InboundCallToNumReason = Util.ConvertToString(txtInboundCallToNumReason.Text.Trim());
            applicant.ActualCloseDt =Util.ConvertToDateTime(txtActualCloseDt.Text.Trim());
            applicant.ChgLstUserId = Util.ConvertToString(txtWorkingUserID.Text.Trim());
            return applicant;
        }
        private void ApplicantToForm(ApplicantDTO applicant)
        {
            if (applicant != null)
            {
                txtApplicantId.Text = Util.ConvertToString(applicant.ApplicantId);
                txtRightPartyContactInd.Text = applicant.RightPartyContactInd;
                txtRpcMostrecentDt.Text = Util.ConvertToString(applicant.RpcMostRecentDt);
                txtNoRpcReason.Text = applicant.NoRpcReason;
                txtCounselingAcceptedDt.Text = Util.ConvertToString(applicant.CounselingAcceptedDt);
                txtCounselingScheduledDt.Text = Util.ConvertToString(applicant.CounselingScheduledDt);
                txtCounselingCompletedDt.Text = Util.ConvertToString(applicant.CounselingCompletedDt);
                txtCounselingRefusedDt.Text = Util.ConvertToString(applicant.CounselingRefusedDt);
                txtFirstCounseledDt.Text = Util.ConvertToString(applicant.FirstCounseledDt);
                txtSecondCounseledDt.Text = Util.ConvertToString(applicant.SecondCounseledDt);
                txtEdModuleCompletedDt.Text = Util.ConvertToString(applicant.EdModuleCompletedDt);
                txtInboundCallToNumDt.Text = Util.ConvertToString(applicant.InboundCallToNumDt);
                txtInboundCallToNumReason.Text = applicant.InboundCallToNumReason;
                txtActualCloseDt.Text = Util.ConvertToString(applicant.ActualCloseDt);
                txtWorkingUserID.Text = applicant.ChgLstUserId.ToString();
            }
        }
        private void RefreshAllGrids()
        {
            grdvPPBudgetItemBinding();
            grdvProposedPPBudgetItemBinding();
            grdvPPBudgetAssetBinding();
        }
        #region PP Budget Item
        private void grdvPPBudgetItemBinding()
        {
            List<PPBudgetItemDTO_App> ppBudgetItems;
            if (Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION] != null)
            {
                ppBudgetItems = (List<PPBudgetItemDTO_App>)Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION];
                grdvPPBudgetItem.DataSource = ppBudgetItems;
                grdvPPBudgetItem.DataBind();
            }
            else
            {
                ppBudgetItems = new List<PPBudgetItemDTO_App>();
                ppBudgetItems.Add(new PPBudgetItemDTO_App());
                grdvPPBudgetItem.DataSource = ppBudgetItems;
                grdvPPBudgetItem.DataBind();

                int TotalColumns = grdvPPBudgetItem.Rows[0].Cells.Count;
                grdvPPBudgetItem.Rows[0].Cells.Clear();
                grdvPPBudgetItem.Rows[0].Cells.Add(new TableCell());
                grdvPPBudgetItem.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdvPPBudgetItem.Rows[0].Cells[0].Text = "No Records Found";
            }
        }
        private PPBudgetItemDTO_App RowToPPBudgetItemDTO(GridViewRow row)
        {
            TextBox txtPPBudgetItemAmt = (TextBox)row.FindControl("txtPPBudgetItemAmt");
            TextBox txtPPBudgetNote = (TextBox)row.FindControl("txtPPBudgetNote");
            TextBox txtBudgetSubcategoryId = (TextBox)row.FindControl("txtBudgetSubcategoryId");
            Label lblPPBudgetItemId = (Label)row.FindControl("lblPPBudgetItemId");
            
            PPBudgetItemDTO_App ppBudgetItem = new PPBudgetItemDTO_App();
            ppBudgetItem.PPBudgetItemAmt = Util.ConvertToDouble(txtPPBudgetItemAmt.Text.Trim());
            ppBudgetItem.PPBudgetNote = Util.ConvertToString(txtPPBudgetNote.Text.Trim());
            ppBudgetItem.BudgetSubcategoryId = Util.ConvertToInt(txtBudgetSubcategoryId.Text.Trim());

            ppBudgetItem.PPBudgetItemId = Util.ConvertToInt(lblPPBudgetItemId.Text.Trim());
            return ppBudgetItem;
        }

        protected void grdvPPBudgetItem_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            grdvPPBudgetItem.EditIndex = -1;
            RefreshAllGrids();

        }

        protected void grdvPPBudgetItemRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                PPBudgetItemDTO_App ppBudgetItem = RowToPPBudgetItemDTO(grdvPPBudgetItem.FooterRow);

                List<PPBudgetItemDTO_App> ppBudgetItems = new List<PPBudgetItemDTO_App>();
                if (Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION] != null)
                {
                    ppBudgetItems = (List<PPBudgetItemDTO_App>)Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION];
                    int? ppBudgetItemId = ppBudgetItems.Max(item => item.PPBudgetItemId);
                    ppBudgetItem.PPBudgetItemId = ppBudgetItemId + 1;
                }
                else
                {
                    ppBudgetItem.PPBudgetItemId = 1;
                }

                ppBudgetItems.Add(ppBudgetItem);
                Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION] = ppBudgetItems;
                
                RefreshAllGrids();
            }

        }

        protected void grdvPPBudgetItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int? ppBudgetItemId = Util.ConvertToInt(((Label)grdvPPBudgetItem.Rows[e.RowIndex].FindControl("lblPPBudgetItemId")).Text);
            if (Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION] != null)
            {
                List<PPBudgetItemDTO_App> ppBudgetItems = (List<PPBudgetItemDTO_App>)Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION];
                int index = ppBudgetItems.FindIndex(item => item.PPBudgetItemId == ppBudgetItemId);

                if (index < 0)
                {
                    //can not Delete item
                }
                else
                {
                    ppBudgetItems.RemoveAt(index);
                    if (ppBudgetItems.Count > 0)
                        Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION] = ppBudgetItems;
                    else
                        Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION] = null;
                }

                grdvPPBudgetItem.EditIndex = -1;
                RefreshAllGrids();
            }
            else
            {
                //can not Delete item
            }
        }

        protected void grdvPPBudgetItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvPPBudgetItem.EditIndex = e.NewEditIndex;
            RefreshAllGrids();

        }

        protected void grdvPPBudgetItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            PPBudgetItemDTO_App ppBudgetItem = RowToPPBudgetItemDTO(grdvPPBudgetItem.Rows[e.RowIndex]);
            if (Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION] != null)
            {
                List<PPBudgetItemDTO_App> ppBudgetItems = (List<PPBudgetItemDTO_App>)Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION];
                int index = ppBudgetItems.FindIndex(item => item.PPBudgetItemId == ppBudgetItem.PPBudgetItemId);

                if (index < 0)
                {
                    //can not update List
                }
                else
                {
                    ppBudgetItems[index] = ppBudgetItem;
                    Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION] = ppBudgetItems;
                }


                grdvPPBudgetItem.EditIndex = -1;
                RefreshAllGrids();
            }
            else
            {
                //can not update List
            }
        }
        #endregion
        #region PROPOSED PRE-PURCHASE BUDGET ITEMS
        private void grdvProposedPPBudgetItemBinding()
        {
            List<PPPBudgetItemDTO_App> proposedPPBudgetItems;
            if (Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION] != null)
            {
                proposedPPBudgetItems = (List<PPPBudgetItemDTO_App>)Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION];
                grdvProposedPPBudgetItem.DataSource = proposedPPBudgetItems;
                grdvProposedPPBudgetItem.DataBind();
            }
            else
            {
                proposedPPBudgetItems = new List<PPPBudgetItemDTO_App>();
                proposedPPBudgetItems.Add(new PPPBudgetItemDTO_App());
                grdvProposedPPBudgetItem.DataSource = proposedPPBudgetItems;
                grdvProposedPPBudgetItem.DataBind();

                int TotalColumns = grdvProposedPPBudgetItem.Rows[0].Cells.Count;
                grdvProposedPPBudgetItem.Rows[0].Cells.Clear();
                grdvProposedPPBudgetItem.Rows[0].Cells.Add(new TableCell());
                grdvProposedPPBudgetItem.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdvProposedPPBudgetItem.Rows[0].Cells[0].Text = "No Records Found";
            }
        }
        private PPPBudgetItemDTO_App RowToProposedPPBudgetItemDTO(GridViewRow row)
        {
            TextBox txtProposedBudgetItemAmt = (TextBox)row.FindControl("txtProposedBudgetItemAmt");
            TextBox txtProposedBudgetNote = (TextBox)row.FindControl("txtProposedBudgetNote");
            TextBox txtBudgetSubcategoryId = (TextBox)row.FindControl("txtBudgetSubcategoryId1");
            Label lblProposedBudgetItemId = (Label)row.FindControl("lblProposedBudgetItemId");

            PPPBudgetItemDTO_App proposedPPBudgetItem = new PPPBudgetItemDTO_App();
            proposedPPBudgetItem.ProposedBudgetItemAmt = Util.ConvertToDouble(txtProposedBudgetItemAmt.Text.Trim());
            proposedPPBudgetItem.ProposedBudgetNote = Util.ConvertToString(txtProposedBudgetNote.Text.Trim());
            proposedPPBudgetItem.BudgetSubcategoryId = Util.ConvertToInt(txtBudgetSubcategoryId.Text.Trim());

            proposedPPBudgetItem.PPPBudgetItemId = Util.ConvertToInt(lblProposedBudgetItemId.Text.Trim());
            return proposedPPBudgetItem;
        }

        protected void grdvProposedPPBudgetItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                PPPBudgetItemDTO_App proposedPPBudgetItem = RowToProposedPPBudgetItemDTO(grdvProposedPPBudgetItem.FooterRow);

                List<PPPBudgetItemDTO_App> proposedPPBudgetItems = new List<PPPBudgetItemDTO_App>();
                if (Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION] != null)
                {
                    proposedPPBudgetItems = (List<PPPBudgetItemDTO_App>)Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION];
                    int? proposedPPBudgetItemId = proposedPPBudgetItems.Max(item => item.PPPBudgetItemId);
                    proposedPPBudgetItem.PPPBudgetItemId = proposedPPBudgetItemId + 1;
                }
                else
                {
                    proposedPPBudgetItem.PPPBudgetItemId = 1;
                }

                proposedPPBudgetItems.Add(proposedPPBudgetItem);
                Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION] = proposedPPBudgetItems;
                //grdvBudgetItemBinding();
                RefreshAllGrids();
            }
        }

        protected void grdvProposedPPBudgetItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int? proposedPPBudgetItemId = Util.ConvertToInt(((Label)grdvProposedPPBudgetItem.Rows[e.RowIndex].FindControl("lblProposedBudgetItemId")).Text);
            if (Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION] != null)
            {
                List<PPPBudgetItemDTO_App> proposedPPBudgetItems = (List<PPPBudgetItemDTO_App>)Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION];
                int index = proposedPPBudgetItems.FindIndex(item => item.PPPBudgetItemId == proposedPPBudgetItemId);

                if (index < 0)
                {
                    //can not Delete item
                }
                else
                {
                    proposedPPBudgetItems.RemoveAt(index);
                    if (proposedPPBudgetItems.Count > 0)
                        Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION] = proposedPPBudgetItems;
                    else
                        Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION] = null;
                }

                grdvProposedPPBudgetItem.EditIndex = -1;
                RefreshAllGrids();
            }
            else
            {
                //can not Delete item
            }
        }

        protected void grdvProposedPPBudgetItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvProposedPPBudgetItem.EditIndex = e.NewEditIndex;
            RefreshAllGrids();

        }

        protected void grdvProposedPPBudgetItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            PPPBudgetItemDTO_App proposedPPBudgetItem = RowToProposedPPBudgetItemDTO(grdvProposedPPBudgetItem.Rows[e.RowIndex]);
            if (Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION] != null)
            {
                List<PPPBudgetItemDTO_App> proposedPPBudgetItems = (List<PPPBudgetItemDTO_App>)Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION];
                int index = proposedPPBudgetItems.FindIndex(item => item.PPPBudgetItemId == proposedPPBudgetItem.PPPBudgetItemId);

                if (index < 0)
                {
                    //can not update List
                }
                else
                {
                    proposedPPBudgetItems[index] = proposedPPBudgetItem;
                    Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION] = proposedPPBudgetItems;
                }


                grdvProposedPPBudgetItem.EditIndex = -1;
                RefreshAllGrids();
            }
            else
            {
                //can not update List
            }
        }

        protected void grdvProposedPPBudgetItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvProposedPPBudgetItem.EditIndex = -1;
            RefreshAllGrids();
        }
        #endregion
        #region Pre-Purchase Budget Asset
        private void grdvPPBudgetAssetBinding()
        {

            List<PPBudgetAssetDTO_App> ppBudgetAssets;
            if (Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION] != null)
            {
                ppBudgetAssets = (List<PPBudgetAssetDTO_App>)Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION];
                grdvPPBudgetAsset.DataSource = ppBudgetAssets;
                grdvPPBudgetAsset.DataBind();
            }
            else
            {
                ppBudgetAssets = new List<PPBudgetAssetDTO_App>();
                ppBudgetAssets.Add(new PPBudgetAssetDTO_App());
                grdvPPBudgetAsset.DataSource = ppBudgetAssets;
                grdvPPBudgetAsset.DataBind();

                int TotalColumns = grdvPPBudgetAsset.Rows[0].Cells.Count;
                grdvPPBudgetAsset.Rows[0].Cells.Clear();
                grdvPPBudgetAsset.Rows[0].Cells.Add(new TableCell());
                grdvPPBudgetAsset.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdvPPBudgetAsset.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        private PPBudgetAssetDTO_App RowToPPBudgetAssetDTO(GridViewRow row)
        {
            TextBox txtPPBudgetAssetName = (TextBox)row.FindControl("txtPPBudgetAssetName");
            TextBox txtPPBudgetAssetValue = (TextBox)row.FindControl("txtPPBudgetAssetValue");
            Label lblPPBudgetAssetID = (Label)row.FindControl("lblPPBudgetAssetID");

            PPBudgetAssetDTO_App ppBudgetAsset = new PPBudgetAssetDTO_App();
            ppBudgetAsset.PPBudgetAssetName = Util.ConvertToString(txtPPBudgetAssetName.Text.Trim());
            ppBudgetAsset.PPBudgetAssetValue = Util.ConvertToDouble(txtPPBudgetAssetValue.Text.Trim());
            ppBudgetAsset.PPBudgetAssetId = Util.ConvertToInt(lblPPBudgetAssetID.Text.Trim());
            
            return ppBudgetAsset;
        }
        protected void grdvPPBudgetAsset_RowCancelEditing(object sender, GridViewCancelEditEventArgs e)
        {
            grdvPPBudgetAsset.EditIndex = -1;
            RefreshAllGrids();

        }

        protected void grdvPPBudgetAssetRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                PPBudgetAssetDTO_App ppBudgetAsset = RowToPPBudgetAssetDTO(grdvPPBudgetAsset.FooterRow);

                List<PPBudgetAssetDTO_App> ppBudgetAssets = new List<PPBudgetAssetDTO_App>();
                if (Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION] != null)
                {
                    ppBudgetAssets = (List<PPBudgetAssetDTO_App>)Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION];
                    int? ppBudgetAssetId = ppBudgetAssets.Max(item => item.PPBudgetAssetId);
                    ppBudgetAsset.PPBudgetAssetId = ppBudgetAssetId + 1;
                }
                else
                {
                    ppBudgetAsset.PPBudgetAssetId = 1;
                }

                ppBudgetAssets.Add(ppBudgetAsset);
                Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION] = ppBudgetAssets;
                RefreshAllGrids();
            }
        }

        protected void grdvPPBudgetAsset_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int? ppBdgetAssetId = Util.ConvertToInt(((Label)grdvPPBudgetAsset.Rows[e.RowIndex].FindControl("lblPPBudgetAssetId")).Text);
            if (Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION] != null)
            {
                List<PPBudgetAssetDTO_App> ppBudgetAssets = (List<PPBudgetAssetDTO_App>)Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION];
                int index = ppBudgetAssets.FindIndex(item => item.PPBudgetAssetId == ppBdgetAssetId);

                if (index < 0)
                {
                    //can not Delete item
                }
                else
                {
                    ppBudgetAssets.RemoveAt(index);
                    if (ppBudgetAssets.Count > 0)
                        Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION] = ppBudgetAssets;
                    else
                        Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION] = null;
                }

                grdvPPBudgetAsset.EditIndex = -1;
                RefreshAllGrids();
            }
            else
            {
                //can not Delete item
            }
        }

        protected void grdvPPBudgetAsset_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvPPBudgetAsset.EditIndex = e.NewEditIndex;
            
            RefreshAllGrids();
        }

        protected void grdvPPBudgetAsset_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            PPBudgetAssetDTO_App ppBudgetAsset = RowToPPBudgetAssetDTO(grdvPPBudgetAsset.Rows[e.RowIndex]);
            if (Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION] != null)
            {
                List<PPBudgetAssetDTO_App> ppBudgetAssets = (List<PPBudgetAssetDTO_App>)Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION];
                int index = ppBudgetAssets.FindIndex(item => item.PPBudgetAssetId == ppBudgetAsset.PPBudgetAssetId);

                if (index < 0)
                {
                    //can not update List
                }
                else
                {
                    ppBudgetAssets[index] = ppBudgetAsset;
                    Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION] = ppBudgetAssets;
                }
                grdvPPBudgetAsset.EditIndex = -1;
                
                RefreshAllGrids();
            }
            else
            {
                //can not update List
            }
        }
        #endregion

        #region Upload XML Data File
        private static XDocument GetXmlDocument(XmlReader xmlreader)
        {
            XDocument xdoc = XDocument.Load(xmlreader);
            return xdoc;
        }
        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            grdvMessages.Visible = false;
            try
            {
                if (fileUpload.HasFile)
                {
                    XmlReader xmlReader = new XmlTextReader(fileUpload.FileContent);
                    XDocument xdoc = GetXmlDocument(xmlReader);
                    BindToForm(xdoc);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage em = new ExceptionMessage();
                List<ExceptionMessage> exList = new List<ExceptionMessage>();
                em.Message = "Invalid XML format:" + ex.Message;
                exList.Add(em);
                em = new ExceptionMessage();
                em.Message = "Default PPCase is loaded";
                exList.Add(em);
                grdvMessages.DataSource = exList;
                grdvMessages.DataBind();
                grdvMessages.Visible = true;

                LoadDefaultPPCase();

            }
        }
        #endregion

        #region Save Pre-Purchase Case
        protected void btnSave_Click(object sender, EventArgs e)
        {
            PrePurchaseCaseSaveRequest request = CreatePrePurchaseCaseSaveRequest();
            PrePurchaseCaseSaveResponse response;

            AuthenticationInfo ai = new AuthenticationInfo();
            ai.UserName = txtUsername.Text.Trim();
            ai.Password = txtPassword.Text.Trim();
            AgencyWebService proxy = new AgencyWebService();
            proxy.AuthenticationInfoValue = ai;

            response = proxy.SavePrePurchaseCase(request);
            if (response.Status != ResponseStatus.Success)
            {
                if (response.Status == ResponseStatus.Warning)
                {
                    if (response.PpcId.HasValue)
                        lblMessage.Text = "Congratulation - PpcId is " + response.PpcId;
                    else lblMessage.Text = "Congratulation - Just applicant is updated";
                }
                else
                    lblMessage.Text = "Error Message: ";
                grdvMessages.Visible = true;
                grdvMessages.DataSource = response.Messages;
                grdvMessages.DataBind();
            }
            else
            {
                grdvMessages.Visible = false;
                if (response.PpcId.HasValue)
                    lblMessage.Text = "Congratulation - PpcId is " + response.PpcId;
                else lblMessage.Text = "Congratulation - Just applicant is updated";
            }
        }
        private PrePurchaseCaseSaveRequest CreatePrePurchaseCaseSaveRequest()
        {
            PrePurchaseCaseSaveRequest request = new PrePurchaseCaseSaveRequest();
            PrePurchaseCaseSetDTO ppCaseSet = new PrePurchaseCaseSetDTO();
            
            ppCaseSet.PrePurchaseCase = FormToPrePurchaseCase();
            ppCaseSet.Applicant = FormToApplicant();
            if (Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION] != null)
            {
                var list = new List<PPBudgetAssetDTO>();
                var list_app = ((List<PPBudgetAssetDTO_App>)Session[SessionVariables.PP_BUDGET_ASSET_COLLECTION]);
                foreach (var item in list_app)
                    list.Add(item.ConvertToBase());
                ppCaseSet.PPBudgetAssets = list.ToArray();
            }
            else
                ppCaseSet.PPBudgetAssets = null;

            if (Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION] != null)
            {
                var list = new List<PPBudgetItemDTO>();
                var list_app = ((List<PPBudgetItemDTO_App>)Session[SessionVariables.PP_BUDGET_ITEM_COLLECTION]);
                foreach (var item in list_app)
                    list.Add(item.ConvertToBase());
                ppCaseSet.PPBudgetItems = list.ToArray();
            }
            else
                ppCaseSet.PPBudgetItems = null;

            if (Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION] != null)
            {
                var list = new List<PPPBudgetItemDTO>();
                var list_app = ((List<PPPBudgetItemDTO_App>)Session[SessionVariables.PROPOSED_PP_BUDGET_ITEM_COLLECTION]);
                foreach (var item in list_app)
                    list.Add(item.ConvertToBase());
                ppCaseSet.ProposedPPBudgetItems = list.ToArray();
            }
            else
                ppCaseSet.ProposedPPBudgetItems = null;

            request.PrePurchaseCaseSet = ppCaseSet;
            return request;
        }        
        #endregion

        

    }
}
