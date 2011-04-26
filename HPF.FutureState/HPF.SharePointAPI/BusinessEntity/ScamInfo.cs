using System;
using System.Collections.Generic;
using System.Text;

namespace HPF.SharePointAPI.BusinessEntity
{
    public class ScamInfo : BaseObject
    {
        public DateTime? ItemCreatedDate { get; set; }
        public string ItemCreatedUser { get; set; }
        public DateTime? ItemModifiedDate { get; set; }
        public string ItemModifiedUser { get; set; }
        public int? ItemId { get; set; }

        public string VoiceMailOnlyInd { get; set; }
        public string LoanModificationScamConsent { get; set; }
        public string HotlineSource { get; set; }
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
        public double?  TotalAmountPaid { get; set; }
        public string PaymentType { get; set; }
        public string ContractServicesPerfomed { get; set; }
        public string MainContact { get; set; }
        public string ScamOrgName { get; set; }
        public string ScamOrgAddress { get; set; }
        public string ScamOrgCity { get; set; }
        public string ScamOrgState { get; set; }
        public string ScamOrgZip { get; set; }
        public string ScamOrgPhone { get; set; }
        public string ScamOrgFax { get; set; }
        public string ScamOrgURL { get; set; }
        public string ScamOrgEmail { get; set; }
        public DateTime? FindOutAboutDate { get; set; }
        public DateTime? LastContactDate { get; set; }
        public string Summary { get; set; }
        public string ScamOrgStatus { get; set; }
        public string CurrentLoanStatus { get; set; }
        public string PriorLoanStatus { get; set; }
        public string AgenciesContacted { get; set; }
        public string OptionsOfferedByLender { get; set; }
        public string MultiScammerCount { get; set; }
        public string MultiScammerContactInfo { get; set; }
        public string ScamOrgAddtlContact { get; set; }
        public string ServicerContactSinceScamInd { get; set; }
        public string GovernmentAffiliatedInd { get; set; }
        public string ServicerAffiliatedInd { get; set; }
        public string BorrowerReferredOthersInd { get; set; }
        public string WillingToShareStoryInd { get; set; }
        public string WillingToSendInfoInd { get; set; }
        public string ReferredToCounselingInd { get; set; }
        public string HpfMediaCandidateInd { get; set; }
        public string HpfSuccessStoryInd { get; set; }
        public string DeclinedCounselingInd { get; set; }
        public string CounselorName { get; set; }
        public string CounselorEmail { get; set; }
        public string Agency { get; set; }
        public string AgencyCaseNum { get; set; }
        public string LoanNumber { get; set; }
        public string Servicer { get; set; }
    }
}
