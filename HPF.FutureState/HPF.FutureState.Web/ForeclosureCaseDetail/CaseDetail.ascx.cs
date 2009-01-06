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
using HPF.FutureState.Common.DataTransferObjects;



namespace HPF.FutureState.Web.ForeclosureCaseDetail
{
    public partial class ForeclosureCaseDetail : System.Web.UI.UserControl
    {
        private ForeclosureCaseDTO _ForeclosureCase;
        public ForeclosureCaseDTO ForeclosureCase
        {

            get
            {
                return _ForeclosureCase;
            }
            set
            {
                _ForeclosureCase = value;
            }
        }
        private void BindData()
        {
            if (ForeclosureCase == null)
                return;
            //Property
            lblAddress1.Text = ForeclosureCase.PropAddr1;
            lblAddress2.Text=ForeclosureCase.PropAddr2;
            lblCity.Text=ForeclosureCase.PropCity;
            lblStateZip.Text=ForeclosureCase.PropStateCd+","+ForeclosureCase.PropZip;
            lblPrimaryResidence.Text = ForeclosureCase.PrimaryResidenceInd;
            lblOwnerOccupied.Text = ForeclosureCase.OwnerOccupiedInd;
            lblPropertyCode.Text = ForeclosureCase.PropertyCd;
            lblNumberOfOccupants.Text = ForeclosureCase.OccupantNum.ToString();
            lblPurchaseYear.Text = ForeclosureCase.HomePurchaseYear.ToString();
            lblPurchasePrice.Text = ForeclosureCase.HomePurchasePrice.ToString();
            lblCurrentMarketValue.Text = ForeclosureCase.HomeCurrentMarketValue.ToString();
            lblForSaleIndicator.Text = ForeclosureCase.ForSaleInd;
            lblRealtyCompany.Text = ForeclosureCase.RealtyCompany;
            lblHomeAskingPrice.Text = ForeclosureCase.HomeSalePrice.ToString();
            lblPrimaryResidence.Text = ForeclosureCase.PrimResEstMktValue.ToString();
            //Borrower
            lblFirstName.Text = ForeclosureCase.BorrowerFname;
            lblMidName.Text = ForeclosureCase.BorrowerMname;
            lblLastName.Text = ForeclosureCase.BorrowerLname;
            lblDOB.Text = ForeclosureCase.BorrowerDob.ToShortDateString();
            lblLast4SSN.Text = "XXX-XX-" + ForeclosureCase.BorrowerLast4Ssn;
            lblPrimaryContact.Text = ForeclosureCase.PrimaryContactNo;
            lblSecondaryContact.Text = ForeclosureCase.SecondContactNo;
            lblPrimaryEmail.Text = ForeclosureCase.Email1;
            lblSecondaryEmail.Text = ForeclosureCase.Email2;
            lblGender.Text = ForeclosureCase.GenderCd;
            lblMother.Text = ForeclosureCase.MotherMaidenLname;
            lblDisabled.Text = ForeclosureCase.BorrowerDisabledInd;
            lblRace.Text = ForeclosureCase.RaceCd;
            //lblEthnicity.Text = ForeclosureCase.b
            lblPreferedLanguage.Text = ForeclosureCase.BorrowerPreferredLangCd;
            lblEducationLevel.Text = ForeclosureCase.BorrowerEducLevelCompletedCd;
            lblMaritalStatus.Text = ForeclosureCase.BorrowerMaritalStatusCd;
            lblOccupation.Text = ForeclosureCase.BorrowerOccupationCd;
            lblMilitaryService.Text = ForeclosureCase.MilitaryServiceCd;
            //Co-Borrower
            lblCoFirstName.Text = ForeclosureCase.CoBorrowerFname;
            lblCoMidName.Text = ForeclosureCase.CoBorrowerMname;
            lblCoLastName.Text = ForeclosureCase.CoBorrowerLname;
            lblCoDOB.Text = ForeclosureCase.CoBorrowerDob.ToShortDateString();
            lblCoLast4SSN.Text = "XXX-XX-" + ForeclosureCase.CoBorrowerLast4Ssn;
            lblCoDisabled.Text = ForeclosureCase.CoBorrowerDisabledInd;
            lblCoOccupation.Text = ForeclosureCase.CoBorrowerOccupationCd;
            //Contact Address
            lblContactAdd1.Text = ForeclosureCase.ContactAddr1;
            lblContactAdd2.Text = ForeclosureCase.ContactAddr2;
            lblContactCity.Text = ForeclosureCase.ContactCity;
            lblContactStateZip.Text = ForeclosureCase.ContactStateCd +","+ ForeclosureCase.ContactZip;


            if (!IsPostBack)
            {
                BindDDLAgency();
            }



        }

        protected void PageLoad(object sender, EventArgs e)
        {

        }


        protected void BindDDLAgency()
        {
            AgencyDTOCollection agencyCollection = LookupDataBL.Instance.GetAgency();
            ddlAgency.DataValueField = "AgencyID";
            ddlAgency.DataTextField = "AgencyName";
            ddlAgency.DataSource = agencyCollection;
            ddlAgency.DataBind();
            ddlAgency.Items.FindByText("ALL").Selected = true;
        }

    }
}