using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;
using System.IO;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common.Utils;

namespace HPF.FutureState.BusinessLogic
{
    public class ServicerApplicantBL:BaseBusinessLogic
    {
        private static readonly ServicerApplicantBL _instance = new ServicerApplicantBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static ServicerApplicantBL Instance
        {
            get
            {
                return _instance;
            }
        }
        protected ServicerApplicantBL() { }
        public ServicerDTOCollection servicerCollection;
        public string hpfAccessFolderPattern = @"C:\HPF_Batch_Processed\PrePurchase\{0}\";
        public string servicerAccessFolderPattern = @"C:\HPF_FTP_Secure\{0}\PrePurchase\";
        public string agencyAccessFolderPattern = @"C:\HPF_FTP_Secure\{0}\PrePurchase\{1}\";
        private string hpfAccessFolder;
        private string servicerAccessFolder;
        private string hpfAgencyFileFolder;
        private string servicerAgencyFileFolder;

        public void SetPrivateParameter(string servicerName)
        {
            servicerAccessFolder = string.Format(servicerAccessFolderPattern, servicerName);
            hpfAccessFolder = string.Format(hpfAccessFolderPattern, servicerName);
            servicerAgencyFileFolder = string.Format(agencyAccessFolderPattern, "CCCS_SF",servicerName);
            hpfAgencyFileFolder = string.Format(hpfAccessFolderPattern, "CCCS_SF");
        }

        /// <summary>
        /// Import servicer applicant and applicant from referral files from servicer (Chase)
        /// </summary>
        /// <param name="batchJob"></param>
        /// <returns></returns>
        public int ImportServicerApplicantData()
        {
            string[] servicerApplicantFiles = Directory.GetFiles(servicerAccessFolder + @"ReferralFiles\");
            int recordCount = 0;
            if (servicerApplicantFiles.Length > 0)
            {

                foreach (string servicerApplicantFile in servicerApplicantFiles)
                {
                    try
                    {
                        recordCount += ImportServicerApplicantData(servicerApplicantFile);
                    }
                    catch (Exception ex)
                    {
                        ExceptionProcessor.HandleException(ex);
                        //Send E-mail to support
                        var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                        var mail = new HPFSendMail
                        {
                            To = hpfSupportEmail,
                            Subject = "Batch Manager Error- Import servicer applicant data",
                            Body = "Error import servicer file " + servicerApplicantFile + "\n--" +
                                    "Messsage: " + ex.Message + "\nTrace: " + ex.StackTrace
                        };
                        mail.Send();
                    }
                }
            }
            return recordCount;
        }

        /// <summary>
        /// Import data into database from a referral file 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>The number of success record</returns>
        private int ImportServicerApplicantData(string filename)
        {
            int recordErrorCount = 0;
            int recordCount = 0;
            ServicerApplicantDAO servicerApplicantDAO = ServicerApplicantDAO.CreateInstance();
            StringBuilder processedFileContent = new StringBuilder();
            StringBuilder errorFileContent = new StringBuilder();
            StringBuilder agencyFileContent = new StringBuilder();
            try
            {
                TextReader tr = new StreamReader(filename);
                string strLine = "";
                string agencyFileLine = "";
                strLine = tr.ReadLine();
                servicerApplicantDAO.Begin();
                //Get Servicer Id
                int? servicerId = 0;
                string servicerFileName = Path.GetFileName(filename);
                string[] array = servicerFileName.Split('_');
                if (array.Length > 2)
                {
                    ServicerDTO servicerDB = servicerCollection.GetServicerByLabel(array[0]);
                    if (servicerDB != null)
                        servicerId = servicerDB.ServicerID;

                    //Read file line by line
                    while (strLine != null)
                    {
                        //Bypass the empty line
                        if (strLine.Trim() == "") continue;
                        ServicerApplicantDTO servicerApplicant = ReadServicerApplicant(strLine, ref agencyFileLine);
                        if (servicerApplicant != null && servicerDB!=null)
                        {
                            servicerApplicant.ServicerId = servicerId;
                            servicerApplicant.ServicerFileName = servicerFileName;
                            servicerApplicant.SetInsertTrackingInformation("System");
                            servicerApplicantDAO.InsertServicerApplicant(servicerApplicant);

                            processedFileContent.AppendLine(strLine);
                            recordCount++;
                            //Set applicant values and insert to database
                            ApplicantDTO applicant = new ApplicantDTO();
                            applicant.ServicerApplicantId = servicerApplicant.ServicerApplicantId;
                            applicant.AssignedToGroupDt = DateTime.Now;
                            if (recordCount % 3 == 0)
                                applicant.GroupCd = Constant.APPLICANT_GROUP_B_CODE;
                            else
                            {
                                applicant.GroupCd = Constant.APPLICANT_GROUP_A_CODE;
                                applicant.SentToAgencyId = 15881;
                                applicant.SentToAgencyDt = DateTime.Now;
                            }
                            applicant.SetInsertTrackingInformation("System");
                            servicerApplicantDAO.InsertApplicant(applicant);
                            if (applicant.GroupCd == Constant.APPLICANT_GROUP_A_CODE)
                            {
                                agencyFileLine = string.Format(agencyFileLine, applicant.ApplicantId.Value, servicerId.Value);
                                agencyFileContent.AppendLine(agencyFileLine);
                            }
                        }
                        else
                        {
                            errorFileContent.AppendLine(strLine);
                            recordErrorCount++;
                        }
                        strLine = tr.ReadLine();
                    }
                    tr.Close();
                    //Create agencyFileName
                    string agencyFileNameNoExt = string.Format("{0}_{1}_{2}_{3}", "CCCS-SF", array[0], array[1], DateTime.Now.ToString("ffff"));
                    string agencyFileName = CreateFileName(agencyFileNameNoExt, Path.GetExtension(filename), hpfAgencyFileFolder + @"\Created\");
                    //Move file to processed folder
                    MoveProcessedFile(errorFileContent, processedFileContent, agencyFileContent, filename, agencyFileName);
                }
            }
            catch (Exception ex)
            {
                servicerApplicantDAO.Cancel();
                throw ex;
            }
            finally
            {
                servicerApplicantDAO.Commit();
                if (recordErrorCount > 0)
                {
                    //Send E-mail to support
                    var hpfSupportEmail = HPFConfigurationSettings.HPF_SUPPORT_EMAIL;
                    var mail = new HPFSendMail
                    {
                        To = hpfSupportEmail,
                        Subject = "Batch Manager Warning- Import servicer applicant data",
                        Body = "Warning import Chase file " + filename + "\n" +
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
        private ServicerApplicantDTO ReadServicerApplicant(string buffer, ref string sentAgencyLine)
        {
            ServicerApplicantDTO result = null;
            string[] fields = buffer.Split(ServicerApplicantDTO.SpitChar);
            if (fields.Length == ServicerApplicantDTO.TotalFields)
            {
                ServicerApplicantDTO draftResult = new ServicerApplicantDTO();

                draftResult.AcctNum = fields[ServicerApplicantDTO.AcctNumPos];
                draftResult.BorrowerLName = fields[ServicerApplicantDTO.BorrowerLNamePos];
                draftResult.BorrowerFName = fields[ServicerApplicantDTO.BorrowerFNamePos];
                draftResult.CoBorrowerLName = fields[ServicerApplicantDTO.CoBorrowerLNamePos];
                draftResult.CoBorrowerFName = fields[ServicerApplicantDTO.CoBorrowerFNamePos];
                draftResult.PropAddr1 = fields[ServicerApplicantDTO.PropAddr1Pos];
                draftResult.PropCity = fields[ServicerApplicantDTO.PropCityPos];
                draftResult.PropStateCd = fields[ServicerApplicantDTO.PropStateCdPos];
                draftResult.PropZip = fields[ServicerApplicantDTO.PropZipPos];
                draftResult.MailAddr1 = fields[ServicerApplicantDTO.MailAddr1Pos];
                draftResult.MailCity = fields[ServicerApplicantDTO.MailCityPos];
                draftResult.MailStateCd = fields[ServicerApplicantDTO.MailStateCdPos];
                draftResult.MailZip = fields[ServicerApplicantDTO.MailZipPos];
                draftResult.HomePhone = fields[ServicerApplicantDTO.HomePhonePos];
                draftResult.WorkPhone = fields[ServicerApplicantDTO.WorkPhonePos];
                draftResult.EmailAddr = fields[ServicerApplicantDTO.EmailAddrPos];
                draftResult.MortgageProgramCd = fields[ServicerApplicantDTO.MortgageProgramCdPos];
                draftResult.ScheduledCloseDt =ConvertToDateTime(fields[ServicerApplicantDTO.ScheduledCloseDtPos]);
                draftResult.AcceptanceMethodCd = fields[ServicerApplicantDTO.AcceptanceMethodCdPos];
                draftResult.Comments = fields[ServicerApplicantDTO.CommentsPos];
                
                //Validate fields
                var exceptionList = CheckRequiredFields(draftResult);
                exceptionList.Add(CheckInvalidFormatData(draftResult));
                exceptionList.Add(CheckValidCode(draftResult));
                if (exceptionList.Count > 0) return result;
                                
                result = draftResult;
                
                //Add extend fields to agency referall record
                //Reserver place to put servicer id and applicant id to this line
                sentAgencyLine = buffer.Replace(fields[ServicerApplicantDTO.AcctNumPos], fields[ServicerApplicantDTO.AcctNumPos] + "|{1}");
                sentAgencyLine = "{0}|" + sentAgencyLine;
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
        private void MoveProcessedFile(StringBuilder errorFileContent, StringBuilder processedFileContent, StringBuilder agencyFileContent, string originalFileName, string agencyFileName)
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
                    File.Copy(errorFileName, errorFileNameServicer);
                }
                if (!string.IsNullOrEmpty(processedFileContent.ToString()))
                {
                    string processedFileName = CreateFileName(orginalFileNameNoExt, orignalFileExt, hpfAccessFolder + @"\Processed\");
                    using (StreamWriter sw = new StreamWriter(processedFileName))
                    {
                        sw.Write(processedFileContent.ToString());
                    }
                }
                if (!string.IsNullOrEmpty(agencyFileContent.ToString()))
                {

                    string agencyFileNameCopy = CreateFileName(Path.GetFileNameWithoutExtension(agencyFileName), orignalFileExt, servicerAgencyFileFolder + @"\ReferralFiles\");
                    using (StreamWriter sw = new StreamWriter(agencyFileName))
                    {
                        sw.Write(agencyFileContent.ToString());
                    }
                    File.Copy(agencyFileName, agencyFileNameCopy);
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
        private string CreateFileName(string inputFileName, string extension, string targetFolder)
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

        /// <summary>
        /// Check all fields are required by servicer applicant
        /// </summary>
        /// <param name="postModInclusion"></param>
        /// <returns></returns>
        private ExceptionMessageCollection CheckRequiredFields(ServicerApplicantDTO servicerApplicant)
        {
            return ValidateFieldsByRuleSet(servicerApplicant, Constant.RULESET_MIN_REQUIRE_FIELD);
        }
        /// <summary>
        /// Check data input format
        /// </summary>
        /// <param name="postModInclusion"></param>
        /// <returns></returns>
        private ExceptionMessageCollection CheckInvalidFormatData(ServicerApplicantDTO servicerApplicant)
        {
            ExceptionMessageCollection exceptionList = new ExceptionMessageCollection();
            exceptionList.Add(ValidateFieldsByRuleSet(servicerApplicant, Constant.RULESET_LENGTH));
            return exceptionList;
        }

        private ExceptionMessageCollection CheckValidCode(ServicerApplicantDTO servicerApplicant)
        {
            ExceptionMessageCollection exceptionList = new ExceptionMessageCollection();
            ReferenceCodeValidatorBL referenceCode = new ReferenceCodeValidatorBL();
            exceptionList.Add(CheckValidZipCode(servicerApplicant));
            exceptionList.Add(CheckValidCombinationStateCdAndZip(servicerApplicant));
            if (!referenceCode.Validate(ReferenceCode.MORTGAGE_PROGRAM_CODE, servicerApplicant.MortgageProgramCd))
                exceptionList.AddExceptionMessage("Product Type is invalid");
            if (!referenceCode.Validate(ReferenceCode.ACCEPTANCE_METHOD_CODE,servicerApplicant.AcceptanceMethodCd))
                exceptionList.AddExceptionMessage("Acceptance Method is invalid");
            return exceptionList;
        }
        /// <summary>
        /// Check valid combination state_code and zip code
        /// <input>PostModInclusionDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidCombinationStateCdAndZip(ServicerApplicantDTO servicerApplicant)
        {
            GeoCodeRefDTOCollection geoCodeRefCollection = GeoCodeRefDAO.Instance.GetGeoCodeRef();
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            bool mailValid = false;
            bool propertyValid = false;
            if (geoCodeRefCollection == null || geoCodeRefCollection.Count < 1)
                return null;
            foreach (GeoCodeRefDTO item in geoCodeRefCollection)
            {
                mailValid = CombinationMailValid(servicerApplicant, item);
                if (mailValid == true)
                    break;
            }
            foreach (GeoCodeRefDTO item in geoCodeRefCollection)
            {
                propertyValid = CombinationPropertyValid(servicerApplicant, item);
                if (propertyValid == true)
                    break;
            }
            if (mailValid == false)
                msgFcCaseSet.AddExceptionMessage("Mail is invalid");
            if (propertyValid == false)
                msgFcCaseSet.AddExceptionMessage("Property is invalid");
            return msgFcCaseSet;
        }

        /// <summary>
        /// Check valid combination mail state_code and mail zip code
        /// <input>ServicerApplicantDTO</input>
        /// <return>bool<return>
        /// </summary>
        private bool CombinationMailValid(ServicerApplicantDTO servicerApplicant, GeoCodeRefDTO item)
        {
            if (string.IsNullOrEmpty(servicerApplicant.MailZip) && string.IsNullOrEmpty(servicerApplicant.MailStateCd))
                return true;
            return (ConvertStringToUpper(servicerApplicant.MailZip) == ConvertStringToUpper(item.ZipCode) && ConvertStringToUpper(servicerApplicant.MailStateCd) == ConvertStringToUpper(item.StateAbbr));
        }

        /// <summary>
        /// Check valid combination property state_code and property zip code
        /// <input>ServicerApplicantDTO</input>
        /// <return>bool<return>
        /// </summary>
        private bool CombinationPropertyValid(ServicerApplicantDTO servicerApplicant, GeoCodeRefDTO item)
        {
            if (string.IsNullOrEmpty(servicerApplicant.PropZip) && string.IsNullOrEmpty(servicerApplicant.PropStateCd))
                return true;
            return (ConvertStringToUpper(servicerApplicant.PropZip) == ConvertStringToUpper(item.ZipCode) && ConvertStringToUpper(servicerApplicant.PropStateCd) == ConvertStringToUpper(item.StateAbbr));
        }

        /// <summary>
        /// Check valid zipcode
        /// <input>ServicerApplicantDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidZipCode(ServicerApplicantDTO servicerApplicant)
        {
            ExceptionMessageCollection msgFcCaseSet = new ExceptionMessageCollection();
            if (!string.IsNullOrEmpty(servicerApplicant.PropZip) && servicerApplicant.PropZip.Length < 5)
                msgFcCaseSet.AddExceptionMessage("Property Zip is invalid");
            if (!string.IsNullOrEmpty(servicerApplicant.MailZip) && servicerApplicant.MailZip.Length < 5)
                msgFcCaseSet.AddExceptionMessage("Mail Zip is invalid");
            if (msgFcCaseSet.Count > 0) return msgFcCaseSet;
            else
            {
                servicerApplicant.PropZip = servicerApplicant.PropZip.Substring(0, 5);
                servicerApplicant.MailZip = servicerApplicant.MailZip.Substring(0, 5);
                if (!ConvertStringtoInt(servicerApplicant.PropZip))
                    msgFcCaseSet.AddExceptionMessage("Property Zip is invalid");
                if (!ConvertStringtoInt(servicerApplicant.MailZip))
                    msgFcCaseSet.AddExceptionMessage("Mail Zip is invalid");
            }
            return msgFcCaseSet;
        }
        private ExceptionMessageCollection ValidateFieldsByRuleSet(ServicerApplicantDTO servicerApplicant, string ruleSet)
        {
            var msgEventSet = new ExceptionMessageCollection { HPFValidator.ValidateToGetExceptionMessage(servicerApplicant, ruleSet) };
            return msgEventSet;
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
        private DateTime? ConvertToDateTime(object obj)
        {
            DateTime returnValue = DateTime.Now;
            if (obj == null || !DateTime.TryParse(obj.ToString(), out returnValue))
                return null;
            return returnValue;
        }
    }
}
