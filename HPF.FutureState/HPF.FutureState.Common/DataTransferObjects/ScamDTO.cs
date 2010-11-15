using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class ScamDTO:BaseDTO
    {
        public DateTime? ItemCreatedDt { get; set; }
        public string ItemCreatedUser { get; set; }
        public DateTime? ItemModifiedDt { get; set; }
        public string ItemModifiedUser { get; set; }
        public int? ItemId { get; set; }

        public string LoanModificationScamConsent { get; set; }
        public string InformationSharingConsent { get; set; }
        public string MortgageModificationOffer { get; set; }
        public string ListOfWereYous { get; set; }
        public string BorrowerFName { get; set; }
        public string BorrowerLName { get; set; }
        public string BorrowerPhone { get; set; }
        public string BorrowerSecondPhone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string BorrowerAgeRange { get; set; }
        public string BorrowerEmail { get; set; }
        public string BorrowerRace { get; set; }
        public string ListOfServicesOffered { get; set; }
        public string GuraranteedLoanModification { get; set; }
        public string FeePaid { get; set; }
        public string TotalAmountPaid { get; set; }
        public string ContractServicesPerfomed { get; set; }
        public string MainContact { get; set; }
        public string ScamOrgName { get; set; }
        public string ScamOrgAddress { get; set; }
        public string ScamOrgCity { get; set; }
        public string ScamOrgState { get; set; }
        public string ScamOrgZip { get; set; }
        public string ScamOrgPhone { get; set; }
        public string ScamOrgURL { get; set; }
        public string ScamOrgEmail { get; set; }
        public DateTime? FindOutAboutDate { get; set; }
        public DateTime? LastContactDate { get; set; }
        public string Summary { get; set; }
        public string CounselorName { get; set; }
        public string CounselorEmail { get; set; }
        public string Agency { get; set; }
        public string AgencyCaseNum { get; set; }
        public string LoanNumber { get; set; }
        public string Servicer { get; set; }
    }
}
