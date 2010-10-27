using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class PrePurchaseCaseDTO:BaseDTO
    {
        [XmlElement (IsNullable=true)]
        public int? PPCaseId { get; set; }
        [XmlElement(IsNullable = true)]
        [RequiredObjectValidator(Tag = ErrorMessages.ERR1133, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public int? PPBorrowerId { get; set; }
        public int? ProgramId { get; set; }

        private string _groupCd;
        [NullableOrStringLengthValidator(true, 15, "Group Code", Ruleset = Constant.RULESET_LENGTH)]
        public string GroupCd
        {
            get { return _groupCd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _groupCd = value.Trim().ToUpper();
                else _groupCd = value;
            }
        }
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "AssignedToGroupDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime AssignedToGroupDt { get; set; }
        [XmlElement(IsNullable = true)]
        [RequiredObjectValidator(Tag = ErrorMessages.ERR1134, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public int? AgencyId { get; set; }
        public int? SurveyorId { get; set; }
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "SentToSurveyorDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime SentToSurveyorDt { get; set; }

        private string _rightPartyContactInd;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1135)]
        public string RightPartyContactInd
        {
            get {return _rightPartyContactInd;}
            set
            {
                if (!string.IsNullOrEmpty(value)) _rightPartyContactInd = value.Trim().ToUpper();
                else _rightPartyContactInd = value;
            }
        }
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "RpcMostRecentDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime RpcMostRecentDt{get;set;}
        [NullableOrStringLengthValidator(true, 300, "No Rpc Reason", Ruleset = Constant.RULESET_LENGTH)]
        public string NoRpcReason { get; set; }
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "CounselingAcceptedDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime CounselingAcceptedDt { get; set; }
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "CounselingScheduledDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime CounselingScheduledDt { get; set; }
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "CounselingCompletedDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime CounselingCompletedDt { get; set; }
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "CounselingRefusedDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime CounselingRefusedDt { get; set; }
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "FirstCounseledDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime FirstCounseledDt { get; set; }
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "SecondCounseledDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime SecondCounseledDt { get; set; }
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "InboundCallToNumDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime InboundCallToNumDt { get; set; }
        [NullableOrStringLengthValidator(true, 300, "InboundCallToNumReason", Ruleset = Constant.RULESET_LENGTH)]
        public string InboundCallToNumReason { get; set; }
        [StringRequiredValidator(Tag = ErrorMessages.ERR1139, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "AgencyCaseNum", Ruleset = Constant.RULESET_LENGTH)]
        public string AgencyCaseNum { get; set; }
        [NullableOrStringLengthValidator(true, 50, "NewMailAddr1", Ruleset = Constant.RULESET_LENGTH)]
        public string NewMailAddr1 { get; set; }
        [NullableOrStringLengthValidator(true, 50, "NewMailAddr2", Ruleset = Constant.RULESET_LENGTH)]
        public string NewMailAddr2 { get; set; }
        [NullableOrStringLengthValidator(true, 30, "NewMailCity", Ruleset = Constant.RULESET_LENGTH)]
        public string NewMailCity { get; set; }
        private string _newMailStateCd;
        [NullableOrStringLengthValidator(true, 15, "NewMailStateCd", Ruleset = Constant.RULESET_LENGTH)]
        public string NewMailStateCd
        {
            get { return _newMailStateCd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _newMailStateCd = value.Trim().ToUpper();
                else _newMailStateCd = value;
            }
        }
        [NullableOrStringLengthValidator(true, 5, "NewMailZip", Ruleset = Constant.RULESET_LENGTH)]
        public string NewMailZip { get; set; }
        private string _borrowerAuthorizationInd;
        [StringRequiredValidator(Tag = ErrorMessages.ERR1140, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1136)]
        public string BorrowerAuthorizationInd
        {
            get { return _borrowerAuthorizationInd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _borrowerAuthorizationInd = value.Trim().ToUpper();
                else _borrowerAuthorizationInd = value;
            }
        }
        [NullableOrStringLengthValidator(true, 30, "MotherMaidenLName", Ruleset = Constant.RULESET_LENGTH)]
        public string MotherMaidenLName { get; set; }
        [NullableOrStringLengthValidator(true, 20, "PrimaryContactNo", Ruleset = Constant.RULESET_LENGTH)]
        public string PrimaryContactNo { get; set; }
        [NullableOrStringLengthValidator(true, 20, "SecondaryContactNo", Ruleset = Constant.RULESET_LENGTH)]
        public string SecondaryContactNo { get; set; }
        [StringRequiredValidator(Tag = ErrorMessages.ERR1141, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 50, "BorrowerEmployerName", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerEmployerName { get; set; }
        [NullableOrStringLengthValidator(true, 50, "BorrowerJobTitle", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerJobTitle { get; set; }

        private string _borrowerSelfEmployedInd;
        [StringRequiredValidator(Tag = ErrorMessages.ERR1142, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1137)]
        public string BorrowerSelfEmployedInd
        {
            get { return _borrowerSelfEmployedInd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _borrowerSelfEmployedInd = value.Trim().ToUpper();
                else _borrowerSelfEmployedInd = value;
            }
        }
        [RequiredObjectValidator(Tag = ErrorMessages.ERR1143, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrInRangeNumberValidator(true, "0", "99", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "An invalid BorrowerYearsEmployed was provided.")]
        public double BorrowerYearsEmployed { get; set; }
        [NullableOrStringLengthValidator(true, 50, "CoBorrowerEmployerName", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerEmployerName { get; set; }
        [NullableOrStringLengthValidator(true, 50, "CoBorrowerJobTitle", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerJobTitle { get; set; }

        private string _coBorrowerSelfEmployedInd;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH, Tag = ErrorMessages.ERR1138)]
        public string CoBorrowerSelfEmployedInd
        {
            get { return _coBorrowerSelfEmployedInd; }
            set
            {
                if (!string.IsNullOrEmpty(value)) _coBorrowerSelfEmployedInd = value.Trim().ToUpper();
                else _coBorrowerSelfEmployedInd = value;
            }
        }
        public double CoBorrowerYearsEmployed { get; set; }
        [StringRequiredValidator(Tag = ErrorMessages.ERR1144, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "CounselorIdRef", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorIdRef { get; set; }
        [StringRequiredValidator(Tag = ErrorMessages.ERR1145, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "CounselorFName", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorFName { get; set; }
        [StringRequiredValidator(Tag = ErrorMessages.ERR1146, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "CounselorLName", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorLName { get; set; }
        [StringRequiredValidator(Tag = ErrorMessages.ERR1147, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 50, "CounselorEmail", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorEmail { get; set; }
        [StringRequiredValidator(Tag = ErrorMessages.ERR1148, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 20, "CounselorPhone", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorPhone { get; set; }
        [NullableOrStringLengthValidator(true, 20, "CounselorExt", Ruleset = Constant.RULESET_LENGTH)]
        public string CounselorExt { get; set; }
        [RequiredObjectValidator (Tag=ErrorMessages.ERR1149,Ruleset=Constant.RULESET_MIN_REQUIRE_FIELD,MessageTemplate="Required!")]
        [NullableOrInRangeNumberValidator(true, "0", "9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "An invalid CounselingDurationMins was provided.")]
        public int CounselingDurationMins { get; set; }

    }
}
