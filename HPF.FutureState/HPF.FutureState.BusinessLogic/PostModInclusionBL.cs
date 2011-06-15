using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using System.Globalization;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common;
using System.IO;

namespace HPF.FutureState.BusinessLogic
{
    public class PostModInclusionBL:BaseBusinessLogic
    {
        private static readonly PostModInclusionBL _instance = new PostModInclusionBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static PostModInclusionBL Instance
        {
            get
            {
                return _instance;
            }
        }
        protected PostModInclusionBL(){}


        /// <summary>
        /// Read post mod inclusion fields from text line
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public PostModInclusionDTO ReadPostModInclusion(string buffer)
        {
            PostModInclusionDTO result = null;
            string[] fields = buffer.Split(PostModInclusionDTO.SpitChar);
            if (fields.Length == PostModInclusionDTO.TotalFields)
            {
                PostModInclusionDTO draftResult = new PostModInclusionDTO();
                draftResult.FannieMaeLoanNum = fields[PostModInclusionDTO.FannieMaeLoanNumPos];
                draftResult.ReferallDt = ConvertToDateTime(fields[PostModInclusionDTO.ReferallDtPos]);
                draftResult.ServicerName = fields[PostModInclusionDTO.ServicerNamePos];
                draftResult.FannieMaeAgency = fields[PostModInclusionDTO.FannieMaeAgencyPos];
                draftResult.BackLogInd = fields[PostModInclusionDTO.BackLogIndPos];
                draftResult.TrialStartDt = ConvertToDateTime(fields[PostModInclusionDTO.TrialStartDtPos]);
                draftResult.ModConversionDt = ConvertToDateTime(fields[PostModInclusionDTO.ModConversionDtPos]);
                draftResult.AcctNum = fields[PostModInclusionDTO.AcctNumPos];
                draftResult.AchFlag = fields[PostModInclusionDTO.AchFlagPos];
                draftResult.TrialModPmtAmt = ConvertToDouble(fields[PostModInclusionDTO.TrialModPmtAmtPos]);
                draftResult.NextPmtDueDt = ConvertToDateTime(fields[PostModInclusionDTO.NextPmtDueDtPos]);
                draftResult.LastPmtAppliedDt = ConvertToDateTime(fields[PostModInclusionDTO.LastPmtAppliedDtPos]);
                draftResult.UnpaidPrincipalBalAmt = ConvertToDouble(fields[PostModInclusionDTO.UnpaidPrincipalBalAmtPos]);
                draftResult.DefaultReason = fields[PostModInclusionDTO.DefaultReasonPos];
                draftResult.SpanishInd = fields[PostModInclusionDTO.SpanishIndPos];
                draftResult.BorrowerFName = fields[PostModInclusionDTO.BorrowerFNamePos];
                draftResult.BorrowerLName = fields[PostModInclusionDTO.BorrowerLNamePos];
                draftResult.CoBorrowerFName = fields[PostModInclusionDTO.CoBorrowerFNamePos];
                draftResult.CoBorrowerLName = fields[PostModInclusionDTO.CoBorrowerLNamePos];
                draftResult.PropAddr1 = fields[PostModInclusionDTO.PropAddr1Pos];
                draftResult.PropAddr2 = fields[PostModInclusionDTO.PropAddr2Pos];
                draftResult.PropCity = fields[PostModInclusionDTO.PropCityPos];
                draftResult.PropStateCd = fields[PostModInclusionDTO.PropStateCdPos];
                draftResult.PropZip = fields[PostModInclusionDTO.PropZipPos];
                draftResult.ContactAddr1 = fields[PostModInclusionDTO.ContactAddr1Pos];
                draftResult.ContactAddr2 = fields[PostModInclusionDTO.ContactAddr2Pos];
                draftResult.ContactCity = fields[PostModInclusionDTO.ContactCityPos];
                draftResult.ContactStateCd = fields[PostModInclusionDTO.ContactStateCdPos];
                draftResult.ContactZip = fields[PostModInclusionDTO.ContactZipPos];
                draftResult.BorrowerHomeContactNo = fields[PostModInclusionDTO.BorrowerHomeContactNoPos];
                draftResult.BorrowerOffice1ContactNo = fields[PostModInclusionDTO.BorrowerOffice1ContactNoPos];
                draftResult.BorrowerOffice2ContactNo = fields[PostModInclusionDTO.BorrowerOffice2ContactNoPos];
                draftResult.BorrowerOtherContactNo = fields[PostModInclusionDTO.BorrowerOtherContactNoPos];
                draftResult.BorrowerCell1ContactNo = fields[PostModInclusionDTO.BorrowerCell1ContactNoPos];
                draftResult.BorrowerCell2ContactNo = fields[PostModInclusionDTO.BorrowerCell2ContactNoPos];
                draftResult.BorrowerEmail = fields[PostModInclusionDTO.BorrowerEmailPos];
                draftResult.CoBorrowerHomeContactNo = fields[PostModInclusionDTO.CoBorrowerHomeContactNoPos];
                draftResult.CoBorrowerOffice1ContactNo = fields[PostModInclusionDTO.CoBorrowerOffice1ContactNoPos];
                draftResult.CoBorrowerOffice2ContactNo = fields[PostModInclusionDTO.CoBorrowerOffice2ContactNoPos];
                draftResult.CoBorrowerOtherContactNo = fields[PostModInclusionDTO.CoBorrowerOtherContactNoPos];
                draftResult.CoBorrowerCell1ContactNo = fields[PostModInclusionDTO.CoBorrowerCell1ContactNoPos];
                draftResult.CoBorrowerCell2ContactNo = fields[PostModInclusionDTO.CoBorrowerCell2ContactNoPos];
                draftResult.CoBorrowerEmail = fields[PostModInclusionDTO.CoBorrowerEmailPos];

                //Validate fields
                var exceptionList = CheckRequiredFields(draftResult);
                exceptionList.Add(CheckInvalidFormatData(draftResult));
                if (exceptionList.Count > 0) return result;
                if (!CheckBusinessRule(draftResult)) return result;
                result =ApplyHPFValue(draftResult);
            }
            return result;
        }

        /// <summary>
        /// Delete original file, copy processed Record to Processed folder
        /// Copy error Record to Error folder if any
        /// </summary>
        /// <param name="errorFileContent"></param>
        /// <param name="processedFileContent"></param>
        /// <param name="originalFileName"></param>
        /// <param name="outputDestination"></param>
        public void MoveProcessedFile(StringBuilder errorFileContent, StringBuilder processedFileContent, string originalFileName, string outputDestination)
        {
            try
            {
                if (!string.IsNullOrEmpty(errorFileContent.ToString()))
                {
                    //Add "_ERROR" to File Name
                    StringBuilder errorFileName = new StringBuilder();
                    errorFileName.AppendFormat(@"{0}\Error\{1}_ERROR{2}", outputDestination, Path.GetFileNameWithoutExtension(originalFileName),
                                                Path.GetExtension(originalFileName));

                    using (StreamWriter sw = new StreamWriter(errorFileName.ToString()))
                    {
                        sw.Write(errorFileContent.ToString());
                    }
                }
                if (!string.IsNullOrEmpty(processedFileContent.ToString()))
                {
                    StringBuilder processedFileName = new StringBuilder();
                    processedFileName.AppendFormat(@"{0}\Processed\{1}", outputDestination, Path.GetFileName(originalFileName));
                    using (StreamWriter sw = new StreamWriter(processedFileName.ToString()))
                    {
                        sw.Write(processedFileContent.ToString());
                    }
                }
                File.Delete(originalFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private PostModInclusionDTO ApplyHPFValue(PostModInclusionDTO postModInclusion)
        {
            postModInclusion.ServicerId = 0;
            postModInclusion.ServicerFileName = "";
            postModInclusion.AgencyId = 0;
            postModInclusion.AgencyFileName = "";
            postModInclusion.AgencyFileDt = DateTime.Now;
            return postModInclusion;
        }
        /// <summary>
        /// Check all fields are required by post mod inclusion
        /// </summary>
        /// <param name="postModInclusion"></param>
        /// <returns></returns>
        private ExceptionMessageCollection CheckRequiredFields(PostModInclusionDTO postModInclusion)
        {
            return ValidateFieldsByRuleSet(postModInclusion, Constant.RULESET_MIN_REQUIRE_FIELD);
        }
        /// <summary>
        /// Validate business rule
        /// </summary>
        /// <param name="postModInclusion"></param>
        /// <returns>Return true if valid</returns>
        private bool CheckBusinessRule(PostModInclusionDTO postModInclusion)
        {
            bool result = false;
            if (postModInclusion.TrialStartDt > DateTime.Now)
                return result;
            //In trial mod
            if (postModInclusion.BackLogInd == Constant.INDICATOR_YES)
            {
                if (postModInclusion.ModConversionDt.HasValue || !string.IsNullOrEmpty(postModInclusion.AchFlag))
                    return result;
            }
            //In modification mod
            else
            {
                if (!postModInclusion.ModConversionDt.HasValue || (postModInclusion.ModConversionDt <= postModInclusion.TrialStartDt)
                    || string.IsNullOrEmpty(postModInclusion.AchFlag))
                    return result;
            }
            if (postModInclusion.NextPmtDueDt < postModInclusion.ReferallDt)
                return result;
            //Check to make sure this record has at least one contact phone number
            if (!string.IsNullOrEmpty(postModInclusion.BorrowerHomeContactNo)
                ||!string.IsNullOrEmpty(postModInclusion.BorrowerOffice1ContactNo)
                ||!string.IsNullOrEmpty(postModInclusion.BorrowerOffice2ContactNo)
                ||!string.IsNullOrEmpty(postModInclusion.BorrowerOtherContactNo)
                ||!string.IsNullOrEmpty(postModInclusion.BorrowerCell1ContactNo)
                ||!string.IsNullOrEmpty(postModInclusion.BorrowerCell2ContactNo))
                result = true;
            return result;
        }
        /// <summary>
        /// Check data input format
        /// </summary>
        /// <param name="postModInclusion"></param>
        /// <returns></returns>
        private ExceptionMessageCollection CheckInvalidFormatData(PostModInclusionDTO postModInclusion)
        {
            return ValidateFieldsByRuleSet(postModInclusion, Constant.RULESET_LENGTH);
        }
        private ExceptionMessageCollection ValidateFieldsByRuleSet(PostModInclusionDTO postModInclustion, string ruleSet)
        {
            var msgEventSet = new ExceptionMessageCollection { HPFValidator.ValidateToGetExceptionMessage(postModInclustion, ruleSet) };
            return msgEventSet;
        }

        /// <summary>
        /// Convert an object to Int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int? ConvertToInt(object obj)
        {
            int returnValue = 0;

            if (obj == null || !int.TryParse(obj.ToString(), out returnValue))
                return null;
            return returnValue;
        }

        /// <summary>
        /// Convert an object to datetime
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private DateTime? ConvertToDateTime(object obj)
        {
            DateTime returnValue = DateTime.Now;
            if (obj == null || !DateTime.TryParse(obj.ToString(), out returnValue))
                return null;
            return returnValue;
        }

        /// <summary>
        /// Convert an object to double
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private double? ConvertToDouble(object obj)
        {
            double returnValue = 0;
            if (obj == null || !double.TryParse(obj.ToString(), out returnValue))
                return null;
            return returnValue;
        }
    }
}
