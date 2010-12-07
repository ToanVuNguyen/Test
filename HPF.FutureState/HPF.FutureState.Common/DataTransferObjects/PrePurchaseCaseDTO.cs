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
        public int? PpcId { get; set; }

        [XmlIgnore]
        public int? ProgramId { get; set; }

        [XmlElement(IsNullable = true)]
        [RequiredObjectValidator(Tag = ErrorMessages.ERR1133, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public int? ApplicantId { get; set; }

        [XmlElement(IsNullable = true)]
        [RequiredObjectValidator(Tag = ErrorMessages.ERR1134, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        public int? AgencyId { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1139, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "AgencyCaseNum", Ruleset = Constant.RULESET_LENGTH)]
        public string AgencyCaseNum { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1152, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "AcctNum", Ruleset = Constant.RULESET_LENGTH)]
        public string AcctNum { get; set; }

        [NullableOrStringLengthValidator(true, 15, "MortgageProgramCd", Ruleset = Constant.RULESET_LENGTH)]
        public string MortgageProgramCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1153, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "BorrowerFName", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerFName { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1154, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "BorrowerLName", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerLName { get; set; }

        [NullableOrStringLengthValidator(true, 30, "CoBorrowerFName", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerFName { get; set; }

        [NullableOrStringLengthValidator(true, 30, "CoBorrowerFName", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerLName { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1155, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 50, "PropAddr1", Ruleset = Constant.RULESET_LENGTH)]
        public string PropAddr1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "PropAddr2", Ruleset = Constant.RULESET_LENGTH)]
        public string PropAddr2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1156, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "PropCity", Ruleset = Constant.RULESET_LENGTH)]
        public string PropCity { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1157, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "PropStateCd", Ruleset = Constant.RULESET_LENGTH)]
        public string PropStateCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1158, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 5, "PropZip", Ruleset = Constant.RULESET_LENGTH)]
        public string PropZip { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1159, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 50, "MailAddr1", Ruleset = Constant.RULESET_LENGTH)]
        public string MailAddr1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "MailAddr2", Ruleset = Constant.RULESET_LENGTH)]
        public string MailAddr2 { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1160, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "MailCity", Ruleset = Constant.RULESET_LENGTH)]
        public string MailCity { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1161, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 15, "MailStateCd", Ruleset = Constant.RULESET_LENGTH)]
        public string MailStateCd { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1162, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 5, "MailZip", Ruleset = Constant.RULESET_LENGTH)]
        public string MailZip { get; set; }

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

        [StringRequiredValidator(Tag=ErrorMessages.ERR1177,Ruleset=Constant.RULESET_MIN_REQUIRE_FIELD,MessageTemplate="Required!")]
        [NullableOrStringLengthValidator(true, 20, "PrimaryContactNo", Ruleset = Constant.RULESET_LENGTH)]
        public string PrimaryContactNo { get; set; }

        [NullableOrStringLengthValidator(true, 20, "SecondaryContactNo", Ruleset = Constant.RULESET_LENGTH)]
        public string SecondaryContactNo { get; set; }

        [NullableOrStringLengthValidator(true, 50, "BorrowerEmployerName", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerEmployerName { get; set; }

        [NullableOrStringLengthValidator(true, 50, "BorrowerJobTitle", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerJobTitle { get; set; }

        private string _borrowerSelfEmployedInd;
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

        [NullableOrInRangeNumberValidator(true, "0", "99", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "An invalid BorrowerYearsEmployed was provided.")]
        public double? BorrowerYearsEmployed { get; set; }

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

        [NullableOrInRangeNumberValidator(true, "0", "99", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "An invalid CoBorrowerYearsEmployed was provided.")]
        public double? CoBorrowerYearsEmployed { get; set; }

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

        [NullableOrInRangeNumberValidator(true, "0", "9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "An invalid CounselingDurationMins was provided.")]
        public int? CounselingDurationMins { get; set; }

        [StringRequiredValidator(Tag = ErrorMessages.ERR1176, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 30, "Change Last User ID", Ruleset = Constant.RULESET_LENGTH)]
        public string ChgLstUserId { get; set; }
    }
}
