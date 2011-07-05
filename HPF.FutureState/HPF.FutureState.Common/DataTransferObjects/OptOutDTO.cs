using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.DataValidator;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class OptOutDTO:BaseDTO
    {
        public int? OptOutId { get; set; }

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

        private string _servicerName = null;
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 50, "Servicer Name", Ruleset = Constant.RULESET_LENGTH)]
        public string ServicerName 
        {
            get { return _servicerName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _servicerName = value.Trim();
                else
                    _servicerName = value;
            }
        }

        private string _servicerLoanNum = null;
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 30, "Servicer Loan Number", Ruleset = Constant.RULESET_LENGTH)]
        public string ServicerLoanNum
        {
            get { return _servicerLoanNum; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _servicerLoanNum = value.Trim();
                else
                    _servicerLoanNum = value;
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

        private string _propStateCd = null;
        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 15, "Property State Cd", Ruleset = Constant.RULESET_LENGTH)]
        public string PropStateCd
        {
            get { return _propStateCd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _propStateCd = value.ToUpper().Trim();
                else _propStateCd = value;
            }
        }

        [RequiredObjectValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH)]
        public DateTime? OptOutDt { get; set; }

        [StringRequiredValidator(Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD)]
        [NullableOrStringLengthValidator(true, 100, "Opt-Out Reason", Ruleset = Constant.RULESET_LENGTH)]
        public string OptOutReason { get; set; }


        //Postion of field in text file
        public const char SpitChar = '|';
        public const int TotalFields = 10;
        public const int FannieMaeLoanNumPos = 0;
        public const int ServicerNamePos = 1;
        public const int ServicerLoanNumPost = 2;
        public const int BorrowerFNamePos = 3;
        public const int BorrowerLNamePos = 4;
        public const int CoBorrowerFNamePos = 5;
        public const int CoBorrowerLNamePos = 6;
        public const int PropStateCdPos = 7;
        public const int OptOutDtPos = 8;
        public const int OptOutReasonPos = 9;
    }
}
