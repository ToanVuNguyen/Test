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
using HPF.FutureState.DataAccess;
using HPF.FutureState.Common.Utils;


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

        public List<string> fannieMaeLoanNumExistedList;
        public ServicerDTOCollection servicerCollection;

        /// <summary>
        /// Import data into database from post mod inclusion file located in folder of servicer
        /// If there is error import file, it will log error and continue import the next file.
        /// It does not stop when found error.
        /// If import file successfuly, the file will be moved to Processed Folder
        /// </summary>
        /// <param name="batchJob"></param>
        /// <returns></returns>
        public int ImportPostModInclusionData(string servicerName, string hpfAccessFolder, string servicerAccessFolder, ref string listPostModInclusionErrorFile)
        {
            string[] postModFiles = Directory.GetFiles(servicerAccessFolder + @"InclusionFiles\");
            int recordCount = 0;
            //There are no post mod files in upload directory
            if (postModFiles.Length <= 0)
            {
                var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                var mail = new HPFSendMail
                {
                    To = hpfSupportEmail,
                    Subject = "Batch Manager Warning- Import post mod inclusion data",
                    Body = "Error import post mod inclusion file \n" +
                            "Messsage: There are no post mod inclusion files in upload directory of servicer "+servicerName
                };
                mail.Send();
                return 0;
            }
            foreach (string postModFile in postModFiles)
            {
                try
                {
                    recordCount += ImportPostModInclusionData(postModFile, hpfAccessFolder, servicerAccessFolder);
                }
                catch (Exception ex)
                {
                    listPostModInclusionErrorFile += postModFile + "\n--" + "Messsage: " + ex.Message + "\nTrace: " + ex.StackTrace+"\n";
                    throw ex;
                }
            }

            return recordCount;
        }

        /// <summary>
        /// Import data into database from post mod file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>The number of error record</returns>
        public int ImportPostModInclusionData(string filename, string hpfAccessFolder, string servicerAccessFolder)
        {
            int recordErrorCount = 0;
            int recordCount = 0;
            PostModInclusionDAO postModInclusionDAO = PostModInclusionDAO.CreateInstance();
            StringBuilder processedFileContent = new StringBuilder();
            StringBuilder errorFileContent = new StringBuilder();
            try
            {
                TextReader tr = new StreamReader(filename);
                string strLine = "";
                strLine = tr.ReadLine();
                postModInclusionDAO.Begin();
                while (strLine != null)
                {
                    //Bypass the empty line
                    if (strLine.Trim() == "") break;
                    PostModInclusionDTO postModInclusion = ReadPostModInclusion(strLine);
                    if (postModInclusion != null)
                    {
                        postModInclusion.ServicerFileName = Path.GetFileName(filename);
                        postModInclusion.SetInsertTrackingInformation("System");
                        postModInclusionDAO.InsertPostModInclusion(postModInclusion);
                        processedFileContent.AppendLine(strLine);
                        recordCount++;
                    }
                    else
                    {
                        errorFileContent.AppendLine(strLine);
                        recordErrorCount++;
                    }
                    strLine = tr.ReadLine();
                }
                tr.Close();
                //Move file to processed folder
                PostModInclusionBL.Instance.MoveProcessedFile(errorFileContent, processedFileContent, filename, hpfAccessFolder, servicerAccessFolder);
            }
            catch (Exception ex)
            {
                postModInclusionDAO.Cancel();
                throw ex;
            }
            finally
            {
                postModInclusionDAO.Commit();
                if (recordErrorCount > 0)
                {
                    //Send E-mail to support
                    var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                    var mail = new HPFSendMail
                    {
                        To = hpfSupportEmail,
                        Subject = "Batch Manager Warning- Import post mod inclusion",
                        Body = "Warning import post mod report file " + filename + "\n" +
                                "Messsage: There are " + recordErrorCount + " error records."
                    };
                    mail.Send();
                }
            }

            return recordCount;
        }

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
                draftResult.TrialModInd = fields[PostModInclusionDTO.TrialModIndPost];
                draftResult.TrialStartDt = ConvertToDateTime(fields[PostModInclusionDTO.TrialStartDtPos]);
                draftResult.ModConversionDt = ConvertToDateTime(fields[PostModInclusionDTO.ModConversionDtPos]);
                draftResult.AcctNum = fields[PostModInclusionDTO.AcctNumPos];
                draftResult.AchInd = fields[PostModInclusionDTO.AchIndPos];
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
                exceptionList.Add(CheckValidCode(draftResult));
                if (exceptionList.Count > 0) return result;
                //Validate Servicer Name and assign servicer id value
                ServicerDTO servicerDB = servicerCollection.GetServicerByLabel(draftResult.ServicerName);
                if (servicerDB == null)
                    return result;
                else
                    draftResult.ServicerId = servicerDB.ServicerID;
                //Check Business Rule
                if (!CheckBusinessRule(draftResult)) return result;

                //Check Duplicate fannieMaeLoanNum and add new fannieMaeLoanNum if it does not exist
                if (fannieMaeLoanNumExistedList.Contains(draftResult.FannieMaeLoanNum))
                    return result;
                else fannieMaeLoanNumExistedList.Add(draftResult.FannieMaeLoanNum);

                result =ApplyHPFValue(draftResult);
            }
            return result;
        }

        /// <summary>
        /// Copy processed Record to Processed folder
        /// Copy error Record to Error folder if any
        /// Move original file to Archive folder
        /// </summary>
        /// <param name="errorFileContent"></param>
        /// <param name="processedFileContent"></param>
        /// <param name="originalFileName"></param>
        /// <param name="outputDestination"></param>
        public void MoveProcessedFile(StringBuilder errorFileContent, StringBuilder processedFileContent, string originalFileName, string hpfAccessFolder, string servicerAccessFolder)
        {
            try
            {
                string orginalFileNameNoExt = Path.GetFileNameWithoutExtension(originalFileName);
                string orignalFileExt = Path.GetExtension(originalFileName);
                if (!string.IsNullOrEmpty(errorFileContent.ToString()))
                {
                    //Add "_ERROR" to File Name and store to HpfAccessFolder and ServiceAccessFolder
                    string errorFileName = CreateFileName(orginalFileNameNoExt + "_ERROR", orignalFileExt, hpfAccessFolder + @"\Errored\");
                    string errorFileNameServicer = CreateFileName(orginalFileNameNoExt + "_ERROR", orignalFileExt, servicerAccessFolder + @"\ErrorFiles\");
                    using (StreamWriter sw = new StreamWriter(errorFileName))
                    {
                        sw.Write(errorFileContent.ToString());
                    }
                    using (StreamWriter sw = new StreamWriter(errorFileNameServicer))
                    {
                        sw.Write(errorFileContent.ToString());
                    }
                }
                if (!string.IsNullOrEmpty(processedFileContent.ToString()))
                {
                    string processedFileName = CreateFileName(orginalFileNameNoExt, orignalFileExt, hpfAccessFolder + @"\Processed\");
                    using (StreamWriter sw = new StreamWriter(processedFileName))
                    {
                        sw.Write(processedFileContent.ToString());
                    }
                }
                string archiveFileName = CreateFileName(orginalFileNameNoExt, orignalFileExt, hpfAccessFolder + @"\Uploaded\");
                File.Move(originalFileName, archiveFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check if file name is existed, increase the number at the end of file name, return new file name with full path
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <param name="targetFolder"></param>
        /// <returns></returns>
        private string CreateFileName(string inputFileName,string extension, string targetFolder)
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("{0}{1}{2}", targetFolder, inputFileName, extension);
            int iCount = 0;
            while (File.Exists(result.ToString()))
            {
                iCount++;
                result = new StringBuilder();
                result.AppendFormat("{0}{1}_{2}{3}", targetFolder, inputFileName, iCount.ToString(), extension);
            }
            return result.ToString();
        }

        private PostModInclusionDTO ApplyHPFValue(PostModInclusionDTO postModInclusion)
        {
            postModInclusion.AgencyId = null;
            postModInclusion.AgencyFileName = null;
            postModInclusion.AgencyFileDt = null;
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
            if (postModInclusion.TrialStartDt > DateTime.Now || postModInclusion.TrialStartDt>postModInclusion.ReferallDt)
                return result;
            //In trial mod
            if (string.Compare(postModInclusion.TrialModInd,Constant.INDICATOR_YES)==0)
            {
                if (!string.IsNullOrEmpty(postModInclusion.AchInd) || postModInclusion.ModConversionDt.HasValue)
                    return result;
            }
            //In modification mod
            else
            {
                if (string.IsNullOrEmpty(postModInclusion.AchInd) || !postModInclusion.ModConversionDt.HasValue)
                    return result;
                if ((postModInclusion.ModConversionDt <= postModInclusion.TrialStartDt))
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
            ExceptionMessageCollection exceptionList = new ExceptionMessageCollection();
            exceptionList.Add(ValidateFieldsByRuleSet(postModInclusion, Constant.RULESET_LENGTH));
            //Validate fannie_mae_loan_num must be exactly 10 digits - all numeric with no leading zeros.
            double checkNum = 0;
            bool result = double.TryParse(postModInclusion.FannieMaeLoanNum, out checkNum);
            if (!result || checkNum.ToString().Length != 10)
                exceptionList.AddExceptionMessage("fannie_mae_loan_num must be exactly 10 digits with no leading zeros");
            return exceptionList;
        }
        private ExceptionMessageCollection ValidateFieldsByRuleSet(PostModInclusionDTO postModInclustion, string ruleSet)
        {
            var msgEventSet = new ExceptionMessageCollection { HPFValidator.ValidateToGetExceptionMessage(postModInclustion, ruleSet) };
            return msgEventSet;
        }

        private ExceptionMessageCollection CheckValidCode(PostModInclusionDTO postModInclusion)
        {
            ExceptionMessageCollection exceptionList = new ExceptionMessageCollection();
            exceptionList.Add(CheckValidZipCode(postModInclusion));
            exceptionList.Add(CheckValidCombinationStateCdAndZip(postModInclusion));
            return exceptionList;
        }
        /// <summary>
        /// Check valid combination state_code and zip code
        /// <input>PostModInclusionDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidCombinationStateCdAndZip(PostModInclusionDTO postModInclusion)
        {
            GeoCodeRefDTOCollection geoCodeRefCollection = GeoCodeRefDAO.Instance.GetGeoCodeRef();
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            bool contactValid = false;
            bool propertyValid = false;
            if (geoCodeRefCollection == null || geoCodeRefCollection.Count < 1)
                return null;
            foreach (GeoCodeRefDTO item in geoCodeRefCollection)
            {
                contactValid = CombinationContactValid(postModInclusion, item);
                if (contactValid == true)
                    break;
            }
            foreach (GeoCodeRefDTO item in geoCodeRefCollection)
            {
                propertyValid = CombinationPropertyValid(postModInclusion, item);
                if (propertyValid == true)
                    break;
            }
            if (contactValid == false)
                msgFcCaseSet.AddExceptionMessage("Contact is invalid");
            if (propertyValid == false)
                msgFcCaseSet.AddExceptionMessage("Property is invalid");
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid combination contact state_code and contact zip code
        /// <input>PostModInclusionDTO</input>
        /// <return>bool<return>
        /// </summary>
        private bool CombinationContactValid(PostModInclusionDTO postModInclusion, GeoCodeRefDTO item)
        {
            if (string.IsNullOrEmpty(postModInclusion.ContactZip) && string.IsNullOrEmpty(postModInclusion.ContactStateCd))
                return true;
            return (ConvertStringToUpper(postModInclusion.ContactZip) == ConvertStringToUpper(item.ZipCode) && ConvertStringToUpper(postModInclusion.ContactStateCd) == ConvertStringToUpper(item.StateAbbr));
        }

        /// <summary>
        /// Check valid combination property state_code and property zip code
        /// <input>PostModInclusionDTO</input>
        /// <return>bool<return>
        /// </summary>
        private bool CombinationPropertyValid(PostModInclusionDTO postModInclusion, GeoCodeRefDTO item)
        {
            if (string.IsNullOrEmpty(postModInclusion.PropZip) && string.IsNullOrEmpty(postModInclusion.PropStateCd))
                return true;
            return (ConvertStringToUpper(postModInclusion.PropZip) == ConvertStringToUpper(item.ZipCode) && ConvertStringToUpper(postModInclusion.PropStateCd) == ConvertStringToUpper(item.StateAbbr));
        }

        /// <summary>
        /// Check valid zipcode
        /// <input>PostModInclusionDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidZipCode(PostModInclusionDTO postModInclusion)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (!string.IsNullOrEmpty(postModInclusion.PropZip) && postModInclusion.PropZip.Length < 5)
                msgFcCaseSet.AddExceptionMessage("Property Zip is invalid");
            if (!string.IsNullOrEmpty(postModInclusion.ContactZip) && postModInclusion.ContactZip.Length < 5)
                msgFcCaseSet.AddExceptionMessage("Contact Zip is invalid");
            if (msgFcCaseSet.Count > 0) return msgFcCaseSet;
            else
            {
                postModInclusion.PropZip = postModInclusion.PropZip.Substring(0, 5);
                postModInclusion.ContactZip = postModInclusion.ContactZip.Substring(0, 5);
                if (!ConvertStringtoInt(postModInclusion.PropZip))
                    msgFcCaseSet.AddExceptionMessage("Property Zip is invalid");
                if (!ConvertStringtoInt(postModInclusion.ContactZip))
                    msgFcCaseSet.AddExceptionMessage("Contact Zip is invalid");
            }
            return msgFcCaseSet;
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

        private bool ConvertStringtoInt(string s)
        {
            if (s == null)
                return true;
            else
            {
                try
                {
                    int.Parse(s);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        private string ConvertStringToUpper(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;
            s = s.ToUpper().Trim();
            return s;
        }
        
    }
}
