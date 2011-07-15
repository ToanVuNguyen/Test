using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.Utils.DataValidator;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class ServicerApplicantDTO:BaseDTO
    {
        [XmlElement(IsNullable = true)]
        public int? ServicerApplicantId { get; set; }

        [XmlElement(IsNullable = true)]
        public int? ServicerId { get; set; }
        
        [StringRequiredValidator(Tag = ErrorMessages.ERR1152, Ruleset = Constant.RULESET_MIN_REQUIRE_FIELD, MessageTemplate = "Required!")]
        [NullableOrStringLengthValidator(true, 30, "AcctNum", Ruleset = Constant.RULESET_LENGTH)]
        public string AcctNum { get; set; }

        [NullableOrStringLengthValidator(true, 30, "BorrowerFName", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerFName { get; set; }

        [NullableOrStringLengthValidator(true, 30, "BorrowerLName", Ruleset = Constant.RULESET_LENGTH)]
        public string BorrowerLName { get; set; }

        [NullableOrStringLengthValidator(true, 30, "CoBorrowerFName", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerFName { get; set; }

        [NullableOrStringLengthValidator(true, 30, "CoBorrowerLName", Ruleset = Constant.RULESET_LENGTH)]
        public string CoBorrowerLName { get; set; }

        [NullableOrStringLengthValidator(true, 50, "PropAddr1", Ruleset = Constant.RULESET_LENGTH)]
        public string PropAddr1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "PropAddr2", Ruleset = Constant.RULESET_LENGTH)]
        public string PropAddr2 { get; set; }

        [NullableOrStringLengthValidator(true, 30, "PropCity", Ruleset = Constant.RULESET_LENGTH)]
        public string PropCity { get; set; }

        private string _propStateCd;
        [NullableOrStringLengthValidator(true, 15, "PropStateCd", Ruleset = Constant.RULESET_LENGTH)]
        public string PropStateCd
        {
            get { return _propStateCd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _propStateCd = value.Trim().ToUpper();
                else _propStateCd = value;
            }
        }

        [NullableOrStringLengthValidator(true, 5, "PropZip", Ruleset = Constant.RULESET_LENGTH)]
        public string PropZip { get; set; }

        [NullableOrStringLengthValidator(true, 50, "MailAddr1", Ruleset = Constant.RULESET_LENGTH)]
        public string MailAddr1 { get; set; }

        [NullableOrStringLengthValidator(true, 50, "MailAddr2", Ruleset = Constant.RULESET_LENGTH)]
        public string MailAddr2 { get; set; }

        [NullableOrStringLengthValidator(true, 30, "MailCity", Ruleset = Constant.RULESET_LENGTH)]
        public string MailCity { get; set; }

        private string _mailStateCd;
        [NullableOrStringLengthValidator(true, 15, "MailStateCd", Ruleset = Constant.RULESET_LENGTH)]
        public string MailStateCd
        {
            get { return _mailStateCd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _mailStateCd = value.Trim().ToUpper();
                else _mailStateCd = value;
            }
        }

        [NullableOrStringLengthValidator(true, 5, "MailZip", Ruleset = Constant.RULESET_LENGTH)]
        public string MailZip { get; set; }

        [NullableOrStringLengthValidator(true, 20, "HomePhone", Ruleset = Constant.RULESET_LENGTH)]
        public string HomePhone { get; set; }

        [NullableOrStringLengthValidator(true, 20, "WorkPhone", Ruleset = Constant.RULESET_LENGTH)]
        public string WorkPhone { get; set; }

        [NullableOrStringLengthValidator(true, 50, "EmailAddr", Ruleset = Constant.RULESET_LENGTH)]
        public string EmailAddr { get; set; }

        private string _mortgageProgramCd;
        [NullableOrStringLengthValidator(true, 15, "MortgageProgramCd", Ruleset = Constant.RULESET_LENGTH)]
        public string MortgageProgramCd
        {
            get { return _mortgageProgramCd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _mortgageProgramCd = value.Trim().ToUpper();
                else _mortgageProgramCd = value;
            }
        }
        
        [NullableOrInRangeNumberValidator(true, "1-1-1753", "12-31-9999", Ruleset = Constant.RULESET_LENGTH, MessageTemplate = "ScheduledCloseDt must be between 1/1/1753 and 12/31/9999")]
        public DateTime? ScheduledCloseDt { get; set; }

        private string _acceptanceMethodCd;
        [NullableOrStringLengthValidator(true, 15, "AcceptanceMethodCd", Ruleset = Constant.RULESET_LENGTH)]
        public string AcceptanceMethodCd
        {
            get { return _acceptanceMethodCd; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _acceptanceMethodCd = value.Trim().ToUpper();
                else _acceptanceMethodCd = value;
            }
        }
                
        [NullableOrStringLengthValidator(true, 8000, "Comments", Ruleset = Constant.RULESET_LENGTH)]
        public string Comments { get; set; }

        [NullableOrStringLengthValidator(true, 80, "Servicer File Name", Ruleset = Constant.RULESET_LENGTH)]
        public string ServicerFileName { get; set; }

        //Postion of fields in text file
        public const char SpitChar = '|';
        public const int TotalFields = 20;
        public const int AcctNumPos = 0;
        public const int BorrowerFNamePos = 1;
        public const int BorrowerLNamePos = 2;
        public const int CoBorrowerFNamePos = 3;
        public const int CoBorrowerLNamePos = 4;
        public const int PropAddr1Pos = 5;
        public const int PropCityPos = 6;
        public const int PropStateCdPos = 7;
        public const int PropZipPos = 8;
        public const int MailAddr1Pos = 9;
        public const int MailCityPos = 10;
        public const int MailStateCdPos = 11;
        public const int MailZipPos = 12;
        public const int HomePhonePos = 13;
        public const int WorkPhonePos = 14;
        public const int EmailAddrPos = 15;
        public const int MortgageProgramCdPos = 16;
        public const int ScheduledCloseDtPos = 17;
        public const int AcceptanceMethodCdPos = 18;
        public const int CommentsPos = 19;
        

    }
}
