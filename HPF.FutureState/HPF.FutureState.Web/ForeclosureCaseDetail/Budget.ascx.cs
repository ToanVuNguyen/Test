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
using HPF.FutureState.BusinessLogic;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using System.Drawing;
using HPF.FutureState.Web.Security;



namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class Budget : System.Web.UI.UserControl
    {
        int caseId;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                caseId = int.Parse(Request.QueryString["CaseID"].ToString());
                BudgetSetDTOCollection budgetSets = BudgetBL.Instance.GetBudgetSet(caseId);
                if (budgetSets != null)
                {
                    grvBudgetSet.DataSource = budgetSets;
                    grvBudgetSet.DataBind();
                    if (budgetSets.Count > 0)
                    {
                        grvBudgetSet.SelectedIndex = 0;
                        GetBudgetDetail(budgetSets[0].BudgetSetId);
                    }
                }
                else
                {
                    budgetSets = new BudgetSetDTOCollection();
                    budgetSets.Add(new BudgetSetDTO());
                    grvBudgetSet.DataSource = budgetSets;
                    grvBudgetSet.DataBind();
                    grvBudgetSet.Rows[0].Cells.Clear();
                    grvBudgetSet.Rows[0].Cells.Add(new TableCell { Text = "No data found.", ColumnSpan=6 });
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }
        /// <summary>
        /// Get total amount of money and Expense
        /// </summary>
        /// <param name="budgetItems"></param>
        /// <returns></returns>
        private double? GetTotalAmount(BudgetDetailDTOCollection budgetItems)
        {
            double? result = 0;
            foreach (var itemCollection in budgetItems)
            {
                foreach (var item in itemCollection)
                {
                    if (item.BudgetSubCategory.ToLower().Contains("total"))
                        result += item.BudgetItemAmt;
                }
            }
            return result;
        }
        /// <summary>
        /// Get budgetDetail and bind on gridview
        /// </summary>
        /// <param name="budgetSetId"></param>
        private void GetBudgetDetail(int? budgetSetId)
        {
            try
            {
                BudgetDetailDTO budgetDetail = BudgetBL.Instance.GetBudgetDetail(budgetSetId);
                BudgetDetailDTOCollection budgetExpenseItem = BudgetBL.Instance.GroupBudgetItem(budgetDetail.BudgetItemCollection);
                BudgetDetailDTOCollection budgetIncomeItem = new BudgetDetailDTOCollection();
                foreach (var i in budgetExpenseItem)
                {
                    if (i.BudgetCategory.ToLower() == "income")
                    {
                        budgetExpenseItem.Remove(i);
                        budgetIncomeItem.Add(i);
                        break;
                    }
                }

                


                if (budgetDetail.BudgetAssetCollection.Count == 0)
                {
                    var dummyData = new BudgetAssetDTOCollection();
                    dummyData.Add(new BudgetAssetDTO());
                    grvAsset.DataSource = dummyData;
                    grvAsset.DataBind();
                    grvAsset.Rows[0].Cells.Clear();
                    grvAsset.Rows[0].Cells.Add(new TableCell { Text = "No data found!", ColumnSpan = 2 });
                }
                else
                {
                    grvAsset.RowCreated += new GridViewRowEventHandler(grvAsset_RowCreated);
                    grvAsset.DataSource = budgetDetail.BudgetAssetCollection;
                    grvAsset.DataBind();
                }
                lstIncomes.DataSource = budgetIncomeItem;
                lstIncomes.DataBind();
                lstExpense.DataSource = budgetExpenseItem;
                lstExpense.DataBind();
                
                double? totalIncome = GetTotalAmount(budgetIncomeItem);
                double? totalExpense = GetTotalAmount(budgetExpenseItem);
                //DataBind for Total gird
                string curCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();

                System.Globalization.NumberFormatInfo currencyFormat = new System.Globalization.CultureInfo(curCulture).NumberFormat;

                currencyFormat.CurrencyNegativePattern = 1;
                lblExpenseTotal.Text = totalExpense.Value.ToString("C",currencyFormat);
                lblIncomeTotal.Text = totalIncome.Value.ToString("C",currencyFormat);
                lblSurplusTotal.Text =(totalIncome.Value - totalExpense.Value).ToString("C",currencyFormat);
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                ExceptionProcessor.HandleException(ex,HPFWebSecurity.CurrentIdentity.LoginName);
            }
        }

        void grvAsset_RowCreated(object sender, GridViewRowEventArgs e)
        {
            BudgetAssetDTO budgetAsset = e.Row.DataItem as BudgetAssetDTO;
            if (budgetAsset == null)
                return;
            if (budgetAsset.AssetName.ToLower().Contains("total"))
            {
                #region Set Style
                e.Row.Cells[0].CssClass = "TextBoldCell";
                e.Row.Cells[1].CssClass = "TextBoldCell";
                #endregion
            }
        }
        protected void lstIncomes_ItemCreated(object sender, DataListItemEventArgs e)
        {
            GridView grvIncome = e.Item.FindControl("grvIncome") as GridView;
            if (grvIncome != null)
            {
                BudgetItemDTOCollection currentItem = e.Item.DataItem as BudgetItemDTOCollection;
                if(currentItem!=null)
                {
                    grvIncome.RowCreated += new GridViewRowEventHandler(grvIncome_RowCreated);
                    grvIncome.DataSource = currentItem;
                    grvIncome.DataBind();
                }
            }
        }

        void grvIncome_RowCreated(object sender, GridViewRowEventArgs e)
        {
            BudgetItemDTO budgetItem = e.Row.DataItem as BudgetItemDTO;
            if (budgetItem == null)
                return;
            if (budgetItem.BudgetSubCategory.ToLower().Contains("total"))
            {
                #region Set Style
                e.Row.Cells[0].CssClass = "CellBorderRight";
                e.Row.Cells[3].CssClass = "CellBorderLeft";
                e.Row.Cells[1].CssClass = "TextBoldCell";
                e.Row.Cells[2].CssClass = "TextBoldCell";
                e.Row.Cells[0].BackColor = Color.Transparent;
                e.Row.Cells[3].BackColor = Color.Transparent;
                #endregion 
            }
        }
        /// <summary>
        /// LoadBudgetDetail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvBudgetSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedId =(int) grvBudgetSet.SelectedValue;
            GetBudgetDetail(selectedId);
        }

        protected void lstExpense_ItemCreated(object sender, DataListItemEventArgs e)
        {
            GridView grvExpense = e.Item.FindControl("grvExpense") as GridView;
            if (grvExpense != null)
            {
                BudgetItemDTOCollection currentItem = e.Item.DataItem as BudgetItemDTOCollection;
                if (currentItem != null)
                {
                    grvExpense.RowCreated += new GridViewRowEventHandler(grvIncome_RowCreated);
                    grvExpense.DataSource = currentItem;
                    grvExpense.DataBind();
                }
            }
        }

        protected void grvBudgetSet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lblSurplus = e.Row.FindControl("lblSurplus") as Label;
            BudgetSetDTO bud = e.Row.DataItem as BudgetSetDTO;
            if(lblSurplus!=null)
                if (bud != null)
                { 
                    string curCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
                    System.Globalization.NumberFormatInfo currencyFormat = new System.Globalization.CultureInfo(curCulture).NumberFormat;
                    currencyFormat.CurrencyNegativePattern = 1;
                    lblSurplus.Text   =  bud.TotalSurplus.Value.ToString("C",currencyFormat);
                }
        }
    }
}