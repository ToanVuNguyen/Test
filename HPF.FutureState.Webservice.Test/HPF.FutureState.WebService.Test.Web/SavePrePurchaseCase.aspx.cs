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

            RefreshAllGrids();
            PrePurchaseCaseToForm((PrePurchaseCaseDTO)Session[SessionVariables.PREPURCHASE_CASE]);
        }
        private PrePurchaseCaseDTO FormToPrePurchaseCase()
        {
            PrePurchaseCaseDTO ppCase = new PrePurchaseCaseDTO();

            ppCase.AgencyCaseNum = txtAgencyCaseNumber.Text.Trim();
            ppCase.AgencyId =Util.ConvertToInt(txtAgencyId.Text.Trim());
            ppCase.AssignedToGroupDt = Util.ConvertToDateTime(txtAssignedToGroupDt.Text.Trim());
            ppCase.BorrowerAuthorizationInd = txtBorrowerAuthorizationInd.Text.Trim();
            ppCase.BorrowerEmployerName = txtBorrowerEmployerName.Text.Trim();
            ppCase.BorrowerJobTitle = txtBorrowerJobTitle.Text.Trim();
            ppCase.BorrowerSelfEmployedInd = txtBorrowerSelfEmployedInd.Text.Trim();
            ppCase.BorrowerYearsEmployed = Util.ConvertToDouble(txtBorrowerYearsEmployed.Text.Trim());
            ppCase.CoBorrowerEmployerName = txtCoBorrowerEmployerName.Text.Trim();
            ppCase.CoBorrowerJobTitle = txtCoBorrowerJobTitle.Text.Trim();
            ppCase.CoBorrowerSelfEmployedInd = txtCoBorrowerSelfEmployedInd.Text.Trim();
            ppCase.CoBorrowerYearsEmployed = Util.ConvertToDouble(txtCoBorrowerYearsEmployed.Text.Trim());
            ppCase.CounselingAcceptedDt = Util.ConvertToDateTime(txtCounselingAcceptedDt.Text.Trim());
            ppCase.CounselingCompletedDt = Util.ConvertToDateTime(txtCounselingCompletedDt.Text.Trim());
            ppCase.CounselingDurationMins = Util.ConvertToInt(txtCounselingDurationMins.Text.Trim());
            ppCase.CounselingRefusedDt = Util.ConvertToDateTime(txtCounselingRefusedDt.Text.Trim());
            ppCase.CounselingScheduledDt = Util.ConvertToDateTime(txtCounselingScheduledDt.Text.Trim());
            ppCase.CounselorEmail = txtCounselorEmail.Text.Trim();
            ppCase.CounselorExt = txtCounselorExt.Text.Trim();
            ppCase.CounselorFName = txtCounselorFName.Text.Trim();
            ppCase.CounselorIdRef = txtCounselorIdRef.Text.Trim();
            ppCase.CounselorLName = txtCounselorLName.Text.Trim();
            ppCase.CounselorPhone = txtCounselorPhone.Text.Trim();
            ppCase.FirstCounseledDt = Util.ConvertToDateTime(txtFirstCounseledDt.Text.Trim());
            ppCase.GroupCd = txtGroupCd.Text.Trim();
            ppCase.InboundCallToNumDt = Util.ConvertToDateTime(txtInboundCallToNumDt.Text.Trim());
            ppCase.InboundCallToNumReason = txtInboundCallToNumReason.Text.Trim();
            ppCase.MotherMaidenLName = txtMotherMaidenLName.Text.Trim();
            ppCase.NewMailAddr1 = txtNewMailAddr1.Text.Trim();
            ppCase.NewMailAddr2 = txtNewMailAddr2.Text.Trim();
            ppCase.NewMailCity = txtNewMailCity.Text.Trim();
            ppCase.NewMailStateCd = txtNewMailStateCd.Text.Trim();
            ppCase.NewMailZip = txtNewMailZip.Text.Trim();
            ppCase.NoRpcReason = txtNoRpcReason.Text.Trim();
            ppCase.PPBorrowerId =Util.ConvertToInt(txtPPBorrowerId.Text.Trim());
            ppCase.PPCaseId = Util.ConvertToInt(txtPPCaseId.Text.Trim());
            ppCase.PrimaryContactNo = txtPrimaryContactNo.Text.Trim();
            ppCase.ProgramId = Util.ConvertToInt(txtProgramId.Text.Trim());
            ppCase.RightPartyContactInd = txtRightPartyContactInd.Text.Trim();
            ppCase.RpcMostRecentDt = Util.ConvertToDateTime(txtRpcMostRecentDt.Text.Trim());
            ppCase.SecondaryContactNo = txtSecondaryContactNo.Text.Trim();
            ppCase.SecondCounseledDt = Util.ConvertToDateTime(txtSecondCounseledDt.Text.Trim());
            ppCase.SentToSurveyorDt = Util.ConvertToDateTime(txtSentToSurveyorDt.Text.Trim());
            ppCase.SurveyorId = Util.ConvertToInt(txtSurveyorId.Text.Trim());

            return ppCase;
        }
        private void PrePurchaseCaseToForm(PrePurchaseCaseDTO ppCase)
        {
            if (ppCase != null)
            {
                if (ppCase.PPCaseId.HasValue)
                    txtPPCaseId.Text = ppCase.PPCaseId.Value.ToString();
                txtAgencyCaseNumber.Text = ppCase.AgencyCaseNum;
                txtAgencyId.Text= ppCase.AgencyId.Value.ToString();
                txtAssignedToGroupDt.Text = Util.ConvertToString(ppCase.AssignedToGroupDt);
                txtBorrowerAuthorizationInd.Text = ppCase.BorrowerAuthorizationInd;
                txtBorrowerEmployerName.Text = ppCase.BorrowerEmployerName;
                txtBorrowerJobTitle.Text =  ppCase.BorrowerJobTitle;
                txtBorrowerSelfEmployedInd.Text = ppCase.BorrowerSelfEmployedInd;
                txtBorrowerYearsEmployed.Text = ppCase.BorrowerYearsEmployed.Value.ToString();
                txtCoBorrowerEmployerName.Text = ppCase.CoBorrowerEmployerName;
                txtCoBorrowerJobTitle.Text = ppCase.CoBorrowerJobTitle;
                txtCoBorrowerSelfEmployedInd.Text = ppCase.CoBorrowerSelfEmployedInd;
                txtCoBorrowerYearsEmployed.Text = ppCase.CoBorrowerYearsEmployed.ToString();
                txtCounselingAcceptedDt.Text =Util.ConvertToString(ppCase.CounselingAcceptedDt);
                txtCounselingCompletedDt.Text =Util.ConvertToString(ppCase.CounselingCompletedDt);
                txtCounselingDurationMins.Text = ppCase.CounselingDurationMins.Value.ToString();
                txtCounselingRefusedDt.Text =Util.ConvertToString(ppCase.CounselingRefusedDt);
                txtCounselingScheduledDt.Text =Util.ConvertToString(ppCase.CounselingScheduledDt);
                txtCounselorEmail.Text = ppCase.CounselorEmail;
                txtCounselorExt.Text = ppCase.CounselorExt;
                txtCounselorFName.Text = ppCase.CounselorFName;
                txtCounselorIdRef.Text = ppCase.CounselorIdRef;
                txtCounselorLName.Text = ppCase.CounselorLName;
                txtCounselorPhone.Text = ppCase.CounselorPhone;
                txtFirstCounseledDt.Text =Util.ConvertToString(ppCase.FirstCounseledDt);
                txtGroupCd.Text = ppCase.GroupCd;
                txtInboundCallToNumDt.Text =Util.ConvertToString(ppCase.InboundCallToNumDt);
                txtInboundCallToNumReason.Text = ppCase.InboundCallToNumReason;
                txtMotherMaidenLName.Text = ppCase.MotherMaidenLName;
                txtNewMailAddr1.Text = ppCase.NewMailAddr1;
                txtNewMailAddr2.Text = ppCase.NewMailAddr2;
                txtNewMailCity.Text = ppCase.NewMailCity;
                txtNewMailStateCd.Text = ppCase.NewMailStateCd;
                txtNewMailZip.Text = ppCase.NewMailZip;
                txtNoRpcReason.Text = ppCase.NoRpcReason;
                txtPPBorrowerId.Text = ppCase.PPBorrowerId.Value.ToString();
                txtPrimaryContactNo.Text = ppCase.PrimaryContactNo;
                txtProgramId.Text =  ppCase.ProgramId.Value.ToString();
                txtRightPartyContactInd.Text = ppCase.RightPartyContactInd;
                txtRpcMostRecentDt.Text =Util.ConvertToString(ppCase.RpcMostRecentDt);
                txtSecondaryContactNo.Text = ppCase.SecondaryContactNo;
                txtSecondCounseledDt.Text =Util.ConvertToString(ppCase.SecondCounseledDt);
                txtSentToSurveyorDt.Text=Util.ConvertToString(ppCase.SentToSurveyorDt);
                txtSurveyorId.Text =ppCase.SurveyorId.Value.ToString();
                txtWorkingUserID.Text = ppCase.ChgLstUserId.ToString();
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
            ppBudgetItem.PPBudgetNote = txtPPBudgetNote.Text.Trim();
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
            proposedPPBudgetItem.ProposedBudgetNote = txtProposedBudgetNote.Text.Trim();
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
            ppBudgetAsset.PPBudgetAssetName = txtPPBudgetAssetName.Text.Trim();
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
                    lblMessage.Text = "Congratulation - PPId is " + response.PPCaseId;
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
                lblMessage.Text = "Congratulation - PPId is " + response.PPCaseId;
            }
        }
        private PrePurchaseCaseSaveRequest CreatePrePurchaseCaseSaveRequest()
        {
            PrePurchaseCaseSaveRequest request = new PrePurchaseCaseSaveRequest();
            PrePurchaseCaseSetDTO ppCaseSet = new PrePurchaseCaseSetDTO();
            
            ppCaseSet.PrePurchaseCase = FormToPrePurchaseCase();
            ppCaseSet.PrePurchaseCase.ChgLstUserId = txtWorkingUserID.Text.Trim();
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
