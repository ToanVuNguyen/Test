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
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.BusinessLogic;


namespace HPF.FutureState.Web.AppNewInvoice
{
    public partial class AppNewInvoice : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FundingSourceDatabind();
                ProgramDatabind();
            }
        }
        private void FundingSourceDatabind()
        {
            FundingSourceDTOCollection fundingSourceCollection = LookupDataBL.Instance.GetFundingSource();
            dropFundingSource.DataValueField = "FundingSourceID";
            dropFundingSource.DataTextField = "FundingSourceName";
            dropFundingSource.DataSource = fundingSourceCollection;
            dropFundingSource.DataBind();
            dropFundingSource.Items.FindByText("ALL").Selected = true;
        }
        private void ProgramDatabind()
        {
            ProgramDTOCollection programCollection = LookupDataBL.Instance.GetProgram();
            dropProgram.DataValueField = "ProgramID";
            dropProgram.DataTextField = "ProgramName";
            dropProgram.DataSource = programCollection;
            dropProgram.DataBind();
            dropProgram.Items.FindByText("ALL").Selected = true;
        }
        protected void dropFundingSource_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (int.Parse(dropFundingSource.SelectedValue) == -1)
                return;
            ServicerDTOCollection servicers = null;
            try
            {
                servicers = LookupDataBL.Instance.GetServicerByFundingSourceId(int.Parse(dropFundingSource.SelectedValue));
            }
            catch (Exception ex)
            {
                ExceptionProcessor.HandleException(ex);
            }
            lst_FundingSourceGroup.DataSource = servicers;
            lst_FundingSourceGroup.DataBind();
            
        }
    }
}