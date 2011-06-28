using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class PostModInclusionDTO : BaseDTO
    {
        public int? PostModInclusionId { get; set; }

        private string _fannieMaeLoanNum = null;
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 10, "Fannie Mae Loan Number", Ruleset = Constant.RULESET_LENGTH)]
        public string FannieMaeLoanNum
        {
            get { return _fannieMaeLoanNum; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _fannieMaeLoanNum = value.Trim();
                else
                    _fannieMaeLoanNum = value;
            }
        }

        [RequiredObjectValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH)]
        public DateTime? ReferallDt { get; set; }

        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 50, "Servicer Name", Ruleset = Constant.RULESET_LENGTH)]
        public string ServicerName { get; set; }
        public int? ServicerId { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Agency Name", Ruleset = Constant.RULESET_LENGTH)]
        public string FannieMaeAgency { get; set; }

        private string _backLogInd = null;
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string BackLogInd
        {
            get { return _backLogInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _backLogInd = value.ToUpper().Trim();
                else _backLogInd = value;
            }
        }

        private string _trialModInd = null;
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string TrialModInd
        {
            get { return _trialModInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _trialModInd = value.ToUpper().Trim();
                else _trialModInd = value;
            }
        }

        [RequiredObjectValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH)]
        public DateTime? TrialStartDt { get; set; }
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH)]
        public DateTime? ModConversionDt { get; set; }

        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 30, "Servicer Loan Number", Ruleset = Constant.RULESET_LENGTH)]
        public string AcctNum { get; set; }

        private string _achInd = null;
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string AchInd {
            get { return _achInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _achInd = value.ToUpper().Trim();
                else _achInd = value;
            }
        }

        [RequiredObjectValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH)]
        public double? TrialModPmtAmt { get; set; }

        [RequiredObjectValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH)]
        public DateTime? NextPmtDueDt { get; set; }

        [RequiredObjectValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH)]
        public DateTime? LastPmtAppliedDt { get; set; }

        [RequiredObjectValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrInRangeNumberValidator(true, "-9999999999999.99", "9999999999999.99", Ruleset = Constant.RULESET_LENGTH)]
        public double? UnpaidPrincipalBalAmt { get; set; }

        [NullableOrStringLengthValidator(true, 80, "Default Reason", Ruleset = Constant.RULESET_LENGTH)]
        public string DefaultReason { get; set; }

        private string _spanishInd = null;
        [RequiredObjectValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [YesNoIndicatorValidator(true, Ruleset = Constant.RULESET_LENGTH)]
        public string SpanishInd {
            get { return _spanishInd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _spanishInd = value.ToUpper().Trim();
                else _spanishInd = value;
            }
        }

        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 30, "Borrower FName", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerFName { get; set; }
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 30, "Borrower LName", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerLName { get; set; }

        [NullableOrStringLengthValidator(true, 30, "Co-Borrower LName", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerFName { get; set; }
        [NullableOrStringLengthValidator(true, 30, "Co-Borrower FName", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerLName { get; set; }
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 50, "Property Address 1", Ruleset = Constant.RULESET_LENGTH)]
        public string PropAddr1 { get; set; }
        [NullableOrStringLengthValidator(true, 50, "Property Address 2", Ruleset = Constant.RULESET_LENGTH)]
        public string PropAddr2 { get; set; }
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 30, "Property City", Ruleset = Constant.RULESET_LENGTH)]
        public string PropCity { get; set; }

        private string _propStateCd = null;
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 15, "Property State", Ruleset = Constant.RULESET_LENGTH)]
        public string PropStateCd {
            get { return _propStateCd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _propStateCd = value.ToUpper().Trim();
                else _propStateCd = value;
            }
        }

        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        public string PropZip { get; set; }
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 50, "Mailing Address 1", Ruleset = Constant.RULESET_LENGTH)]
        public string ContactAddr1 { get; set; }
        [NullableOrStringLengthValidator(true, 50, "Mailing Address 2", Ruleset = Constant.RULESET_LENGTH)]
        public string ContactAddr2 { get; set; }
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 30, "Mailing City", Ruleset = Constant.RULESET_LENGTH)]
        public string ContactCity { get; set; }

        private string _contactStateCd = null;
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 15, "Mailing State", Ruleset = Constant.RULESET_LENGTH)]
        public string ContactStateCd {
            get { return _contactStateCd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _contactStateCd = value.ToUpper().Trim();
                else _contactStateCd = value;
            }
        }
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        public string ContactZip { get; set; }
        [NullableOrStringLengthValidator(true, 20, "Borrower Phone - Home", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerHomeContactNo { get; set; }
        [NullableOrStringLengthValidator(true, 20, "Borrower Phone - Office 1", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerOffice1ContactNo { get; set; }
        [NullableOrStringLengthValidator(true, 20, "Borrower Phone - Office 2", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerOffice2ContactNo { get; set; }
        [NullableOrStringLengthValidator(true, 20, "Borrower Phone - Other", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerOtherContactNo { get; set; }
        [NullableOrStringLengthValidator(true, 20, "Borrower Phone - Cell 1", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerCell1ContactNo { get; set; }
        [NullableOrStringLengthValidator(true, 20, "Borrower Phone - Cell 2", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerCell2ContactNo { get; set; }
        [NullableOrStringLengthValidator(true, 50, "Borrower Email", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerEmail { get; set; }
        [NullableOrStringLengthValidator(true, 20, "Co-Borrower Phone - Home", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerHomeContactNo { get; set; }
        [NullableOrStringLengthValidator(true, 20, "Co-Borrower Phone - Office 1", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerOffice1ContactNo { get; set; }
        [NullableOrStringLengthValidator(true, 20, "Co-Borrower Phone - Office 2", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerOffice2ContactNo { get; set; }
        [NullableOrStringLengthValidator(true, 20, "Co-Borrower Phone - Other", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerOtherContactNo { get; set; }
        [NullableOrStringLengthValidator(true, 20, "Co-Borrower Phone - Cell 1", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerCell1ContactNo { get; set; }
        [NullableOrStringLengthValidator(true, 20, "Co-Borrower Phone - Cell 2", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerCell2ContactNo { get; set; }
        [NullableOrStringLengthValidator(true, 50, "Co-Borrower Email", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerEmail { get; set; }
        public string ServicerFileName { get; set; }
        public int? AgencyId { get; set; }
        public string AgencyFileName { get; set; }
        public DateTime? AgencyFileDt { get; set; }


        //Postion of field in text file
        public const char SpitChar = '|';
        public const int TotalFields = 44;
        public const int FannieMaeLoanNumPos = 0;
        public const int ReferallDtPos = 1;
        public const int ServicerNamePos = 2;
        public const int FannieMaeAgencyPos = 3;
        public const int BackLogIndPos = 4;
        public const int TrialModIndPost = 5;
        public const int TrialStartDtPos = 6;
        public const int ModConversionDtPos = 7;
        public const int AcctNumPos = 8;
        public const int AchIndPos = 9;
        public const int TrialModPmtAmtPos = 10;
        public const int NextPmtDueDtPos = 11;
        public const int LastPmtAppliedDtPos = 12;
        public const int UnpaidPrincipalBalAmtPos = 13;
        public const int DefaultReasonPos = 14;
        public const int SpanishIndPos = 15;
        public const int BorrowerFNamePos = 16;
        public const int BorrowerLNamePos = 17;
        public const int CoBorrowerFNamePos = 18;
        public const int CoBorrowerLNamePos = 19;
        public const int PropAddr1Pos = 20;
        public const int PropAddr2Pos = 21;
        public const int PropCityPos = 22;
        public const int PropStateCdPos = 23;
        public const int PropZipPos = 24;
        public const int ContactAddr1Pos = 25;
        public const int ContactAddr2Pos = 26;
        public const int ContactCityPos = 27;
        public const int ContactStateCdPos = 28;
        public const int ContactZipPos = 29;
        public const int BorrowerHomeContactNoPos = 30;
        public const int BorrowerOffice1ContactNoPos = 31;
        public const int BorrowerOffice2ContactNoPos = 32;
        public const int BorrowerOtherContactNoPos = 33;
        public const int BorrowerCell1ContactNoPos = 34;
        public const int BorrowerCell2ContactNoPos = 35;
        public const int BorrowerEmailPos = 36;
        public const int CoBorrowerHomeContactNoPos = 37;
        public const int CoBorrowerOffice1ContactNoPos = 38;
        public const int CoBorrowerOffice2ContactNoPos = 39;
        public const int CoBorrowerOtherContactNoPos = 40;
        public const int CoBorrowerCell1ContactNoPos = 41;
        public const int CoBorrowerCell2ContactNoPos = 42;
        public const int CoBorrowerEmailPos = 43;
                
    }
}
