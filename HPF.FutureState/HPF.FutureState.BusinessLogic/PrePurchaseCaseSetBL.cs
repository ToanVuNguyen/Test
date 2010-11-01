using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;

using HPF.FutureState.Common.BusinessLogicInterface;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.DataAccess;
using HPF.FutureState.Common.Utils.DataValidator;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.Utils.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Text.RegularExpressions;


namespace HPF.FutureState.BusinessLogic
{
    public class PrePurchaseCaseSetBL : BaseBusinessLogic
    {
        PrePurchaseCaseSetDAO prePurchaseCaseSetDAO = PrePurchaseCaseSetDAO.CreateInstance();
        private string _workingUserId;
        PrePurchaseCaseDTO prePurchaseFromDB = new PrePurchaseCaseDTO();
        public ExceptionMessageCollection WarningMessages { get; private set; }

        /// <summary>
        /// Singleton
        /// </summary>
        public static PrePurchaseCaseSetBL Instance
        {
            get
            {
                return new PrePurchaseCaseSetBL();
            }
        }
        protected PrePurchaseCaseSetBL()
        {
            WarningMessages = new ExceptionMessageCollection();
        }

        #region Implementation of saving Pre-PurchaseCase
        public PrePurchaseCaseDTO SavePrePurchaseCaseSet(PrePurchaseCaseSetDTO prePurchaseCaseSet)
        {
            int? ppCaseId;
            if (prePurchaseCaseSet == null || prePurchaseCaseSet.PrePurchaseCase == null)
                throw new DataValidationException(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR1165));
            var exceptionList = CheckRequireForPartial(prePurchaseCaseSet);

            _workingUserId = prePurchaseCaseSet.PrePurchaseCase.ChangeLastUserId;

            var formatDataException = CheckInvalidFormatData(prePurchaseCaseSet);
            exceptionList.Add(formatDataException);

            return prePurchaseCaseSet.PrePurchaseCase;
        }
        #endregion
        #region Functions check min request validate
        /// <summary>
        /// Min request validate the Pre-Purchase case set
        /// 1: Min request validate of Pre-Purchase Case
        /// 2: Min request validate of PPBudget Item Collection
        /// 3: Min request validate of Proposed PPBudgetItem Collection
        /// </summary>
        private ExceptionMessageCollection CheckRequireForPartial(PrePurchaseCaseSetDTO prePurchaseCaseSet)
        {
            return ValidationFieldByRuleSet(prePurchaseCaseSet, Constant.RULESET_MIN_REQUIRE_FIELD);
        }
        private ExceptionMessageCollection ValidationFieldByRuleSet(PrePurchaseCaseSetDTO prePurchaseCaseSet, string ruleSet)
        {
            PrePurchaseCaseDTO prePurchaseCase = prePurchaseCaseSet.PrePurchaseCase;
            PPBudgetItemDTOCollection ppBudgetItems = prePurchaseCaseSet.PPBudgetItems;
            PPPBudgetItemDTOCollection proposedPPBudgetItems = prePurchaseCaseSet.ProposedPPBudgetItems;

            var msgFcCaseSet = new ExceptionMessageCollection();
            msgFcCaseSet.Add(ValidateFieldsPrePurchaseCase(prePurchaseCase, ruleSet));
            msgFcCaseSet.Add(ValidateFieldsPPBudgetItem(ppBudgetItems, ruleSet));
            msgFcCaseSet.Add(ValidateFieldsProposedPPBudgetItem(proposedPPBudgetItems,ruleSet));
            return msgFcCaseSet;
        }

        /// <summary>
        /// Min request validate the Pre-Purchase case set
        /// 1: Min request validate of Pre-Purchase Case
        /// <return>Collection Message Error</return>
        /// </summary>
        private ExceptionMessageCollection ValidateFieldsPrePurchaseCase(PrePurchaseCaseDTO prePurchaseCase, string ruleSet)
        {
            var msgPPCaseSet = new ExceptionMessageCollection { HPFValidator.ValidateToGetExceptionMessage(prePurchaseCase, ruleSet) };
            return msgPPCaseSet;
        }
        /// <summary>
        /// Min request validate the Pre-Purchase case set
        /// 2:  Min request validate of Pre-Purchase Budget Item Collection
        /// <return>Collection Message Error</return>
        /// </summary>
        private ExceptionMessageCollection ValidateFieldsPPBudgetItem(PPBudgetItemDTOCollection ppBudgetItemDTOCollection, string ruleSet)
        {
            var msgPPCaseSet = new ExceptionMessageCollection();
            if (ppBudgetItemDTOCollection != null || ppBudgetItemDTOCollection.Count > 0)
            {
                foreach (var item in ppBudgetItemDTOCollection)
                {
                    var ex = HPFValidator.ValidateToGetExceptionMessage(item, ruleSet);
                    if (ex.Count > 0)
                        return ex;
                }
            }
            return msgPPCaseSet;
        }
        /// <summary>
        /// Min request validate the Pre-Purchase case set
        /// 3:  Min request validate of Proposed Pre-Purchase Budget Item Collection
        /// <return>Collection Message Error</return>
        /// </summary>
        private ExceptionMessageCollection ValidateFieldsProposedPPBudgetItem(PPPBudgetItemDTOCollection propoesedPPBudgetItemDTOCollection, string ruleSet)
        {
            var msgPPCaseSet = new ExceptionMessageCollection();
            if (propoesedPPBudgetItemDTOCollection != null || propoesedPPBudgetItemDTOCollection.Count > 0)
            {
                foreach (var item in propoesedPPBudgetItemDTOCollection)
                {
                    var ex = HPFValidator.ValidateToGetExceptionMessage(item, ruleSet);
                    if (ex.Count > 0)
                        return ex;
                }
            }
            return msgPPCaseSet;
        }
        #endregion
        #region Check Invalid Format Data
        private ExceptionMessageCollection CheckInvalidFormatData(PrePurchaseCaseSetDTO ppCaseSet)
        {
            ExceptionMessageCollection msgPPCaseSet = new ExceptionMessageCollection();
            
            ExceptionMessageCollection ex = ValidationFieldByRuleSet(ppCaseSet, Constant.RULESET_LENGTH);
            ExceptionMessageCollection msgBudgetAsset = ValidateFieldsPPBudgetAsset(ppCaseSet.PPBudgetAssets, Constant.RULESET_LENGTH);
            msgPPCaseSet.Add(ex);
            msgPPCaseSet.Add(msgBudgetAsset);
            
            if (!CheckSpecialCharacrer(ppCaseSet.PrePurchaseCase.MotherMaidenLName))
                msgPPCaseSet.AddExceptionMessage(ErrorMessages.ERR1166, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR1166));
            if (!CheckSpecialCharacrer(ppCaseSet.PrePurchaseCase.BorrowerEmployerName))
                msgPPCaseSet.AddExceptionMessage(ErrorMessages.ERR1167, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR1167));
            if (!CheckSpecialCharacrer(ppCaseSet.PrePurchaseCase.CoBorrowerEmployerName))
                msgPPCaseSet.AddExceptionMessage(ErrorMessages.ERR1168, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR1168));
            if (!CheckSpecialCharacrer(ppCaseSet.PrePurchaseCase.CounselorFName))
                msgPPCaseSet.AddExceptionMessage(ErrorMessages.ERR1169, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR1169));
            if (!CheckSpecialCharacrer(ppCaseSet.PrePurchaseCase.CounselorLName))
                msgPPCaseSet.AddExceptionMessage(ErrorMessages.ERR1170, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR1170));
            return msgPPCaseSet;
        }
        /// <summary>
        /// Check Special characrer in Name
        /// </summary>
        private bool CheckSpecialCharacrer(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;
            Regex regex = new Regex(@"[!@#$%^*(){}|:;?><567890]");
            if (regex.IsMatch(s))
                return false;
            return true;
        }
        /// <summary>
        /// Check MaxLength for pre-purchase BudgetAsset
        /// </summary>
        private ExceptionMessageCollection ValidateFieldsPPBudgetAsset(PPBudgetAssetDTOCollection ppBudgetAssetDTOCollection, string ruleSet)
        {
            var msgPPCaseSet = new ExceptionMessageCollection();
            for (int i = 0; i < ppBudgetAssetDTOCollection.Count; i++)
            {
                var item = ppBudgetAssetDTOCollection[i];
                var ex = HPFValidator.ValidateToGetExceptionMessage(item, ruleSet);
                if (ex.Count != 0)
                {
                    for (int j = 0; j < ex.Count; j++)
                    {
                        var exItem = ex[j];
                        msgPPCaseSet.AddExceptionMessage(exItem.ErrorCode, ErrorMessages.GetExceptionMessageCombined(exItem.ErrorCode) + " working on budget asset index " + (i + 1));
                    }
                }

            }
            return msgPPCaseSet;
        }
        #endregion
        #region Functions check valid code
        /// <summary>
        /// Check valid code
        /// <input>PrePurchaseCaseSetDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidCode(PrePurchaseCaseSetDTO prePurchaseCaseSet)
        {
            PrePurchaseCaseDTO prePurchaseCase = prePurchaseCaseSet.PrePurchaseCase;
            PPBudgetItemDTOCollection ppBudgetItems = prePurchaseCaseSet.PPBudgetItems;
            PPPBudgetItemDTOCollection proposedPPBudgetItems = prePurchaseCaseSet.ProposedPPBudgetItems;

            ExceptionMessageCollection msgPPCaseSet = new ExceptionMessageCollection();

            ExceptionMessageCollection msgStateCdAndZip = CheckValidCombinationStateCdAndZip(prePurchaseCase);
            if (msgStateCdAndZip != null && msgStateCdAndZip.Count != 0)
                msgPPCaseSet.Add(msgStateCdAndZip);

            ExceptionMessageCollection msgZip = CheckValidZipCode(prePurchaseCase);
            if (msgZip != null && msgZip.Count != 0)
                msgPPCaseSet.Add(msgZip);

            ExceptionMessageCollection msgAgencyId = CheckValidAgencyId(prePurchaseCase);
            if (msgAgencyId != null && msgAgencyId.Count != 0)
                msgPPCaseSet.Add(msgAgencyId);

            ExceptionMessageCollection msgProgramId = CheckValidProgramId(prePurchaseCase);
            if (msgProgramId != null && msgProgramId.Count != 0)
                msgPPCaseSet.Add(msgProgramId);

            ExceptionMessageCollection msgBudgetSubId = CheckValidPPBudgetSubcategoryId(ppBudgetItems);
            if (msgBudgetSubId != null && msgBudgetSubId.Count != 0)
                msgPPCaseSet.Add(msgBudgetSubId);

            ExceptionMessageCollection msgProposedBudgetSubId = CheckValidProposedPPBudgetSubcategoryId(proposedPPBudgetItems);
            if (msgProposedBudgetSubId != null && msgProposedBudgetSubId.Count != 0)
                msgPPCaseSet.Add(msgProposedBudgetSubId);

            //ExceptionMessageCollection msgCallId = CheckValidCallId(foreclosureCase);
            //if (msgCallId != null && msgCallId.Count != 0)
            //    msgPPCaseSet.Add(msgCallId);
            return msgPPCaseSet;
        }
        /// <summary>
        /// Check valid combination state_code and zip code
        /// <input>PrePurchaseCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidCombinationStateCdAndZip(PrePurchaseCaseDTO prePurchaseCase)
        {
            GeoCodeRefDTOCollection geoCodeRefCollection = GeoCodeRefDAO.Instance.GetGeoCodeRef();
            ExceptionMessageCollection msgPPCaseSet = new ExceptionMessageCollection();
            bool newMailValid = false;
            if (geoCodeRefCollection == null || geoCodeRefCollection.Count < 1)
                return null;
            foreach (GeoCodeRefDTO item in geoCodeRefCollection)
            {
                newMailValid = CombinationNewEmailValid(prePurchaseCase, item);
                if (newMailValid == true)
                    break;
            }
            if (newMailValid == false)
                msgPPCaseSet.AddExceptionMessage(ErrorMessages.ERR1171, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR1171));
            return msgPPCaseSet;
        }
        /// <summary>
        /// Check valid combination new email state_code and new email zip code
        /// <input>PrePurchaseCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private bool CombinationNewEmailValid(PrePurchaseCaseDTO prePurchaseCase, GeoCodeRefDTO item)
        {
            if (string.IsNullOrEmpty(prePurchaseCase.NewMailZip) && string.IsNullOrEmpty(prePurchaseCase.NewMailStateCd))
                return true;
            return (ConvertStringToUpper(prePurchaseCase.NewMailZip) == ConvertStringToUpper(item.ZipCode) && ConvertStringToUpper(prePurchaseCase.NewMailStateCd) == ConvertStringToUpper(item.StateAbbr));
        }
        /// <summary>
        /// Check valid zipcode
        /// <input>PrePurchaseCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidZipCode(PrePurchaseCaseDTO prePurchaseCase)
        {
            ExceptionMessageCollection msgPPCaseSet = new ExceptionMessageCollection();
            if ((!string.IsNullOrEmpty(prePurchaseCase.NewMailZip) && prePurchaseCase.NewMailZip.Length != 5) || !ConvertStringtoInt(prePurchaseCase.NewMailZip))
                msgPPCaseSet.AddExceptionMessage(ErrorMessages.ERR1172, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0257));
            return msgPPCaseSet;
        }
        /// <summary>
        /// Check valid AgencyId
        /// <input>PrePurchaseCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidAgencyId(PrePurchaseCaseDTO prePurchaseCase)
        {
            ExceptionMessageCollection msgPPCaseSet = new ExceptionMessageCollection();
            AgencyDTOCollection agencyCollection = AgencyDAO.Instance.GetAgency();
            int? agencyID = prePurchaseCase.AgencyId;
            if (agencyID == null)
                return null;
            else if ((agencyCollection == null || agencyCollection.Count < 1) && agencyID != null)
            {
                msgPPCaseSet.AddExceptionMessage(ErrorMessages.ERR0250, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0250));
                return msgPPCaseSet;
            }
            foreach (AgencyDTO item in agencyCollection)
            {
                if (item.AgencyID == agencyID.ToString())
                    return null;
            }
            msgPPCaseSet.AddExceptionMessage(ErrorMessages.ERR0250, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0250));
            return msgPPCaseSet;
        }
        /// <summary>
        /// Check valid programID
        /// <input>PrePurchaseCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidProgramId(PrePurchaseCaseDTO prePurchaseCase)
        {
            ProgramDTOCollection programCollection = ForeclosureCaseSetDAO.CreateInstance().GetProgram();
            ExceptionMessageCollection msgPPCaseSet = new ExceptionMessageCollection();
            int? programId = prePurchaseCase.ProgramId;
            if (programId == null)
                return null;
            else if (programCollection == null || programCollection.Count < 1)
            {
                msgPPCaseSet.AddExceptionMessage(ErrorMessages.ERR0261, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0261));
                return msgPPCaseSet;
            }
            foreach (ProgramDTO item in programCollection)
            {
                if (item.ProgramID == programId.ToString())
                    return null;
            }
            msgPPCaseSet.AddExceptionMessage(ErrorMessages.ERR0261, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0261));
            return msgPPCaseSet;
        }
        /// <summary>
        /// Check valid pre-purchase budget subcategory id
        /// <input>PPBudgetItemDTOCollection</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidPPBudgetSubcategoryId(PPBudgetItemDTOCollection ppBudgetItem)
        {
            ExceptionMessageCollection msgPPCaseSet = new ExceptionMessageCollection();
            if (ppBudgetItem == null || ppBudgetItem.Count < 1)
                return null;
            for (int i = 0; i < ppBudgetItem.Count; i++)
            {
                PPBudgetItemDTO item = ppBudgetItem[i];
                bool isValid = CheckBudgetSubcategory(item);
                if (!isValid)
                    msgPPCaseSet.AddExceptionMessage(ErrorMessages.ERR0262, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0262));
            }
            return msgPPCaseSet;
        }
        private bool CheckBudgetSubcategory(PPBudgetItemDTO ppBudgetItem)
        {
            BudgetSubcategoryDTOCollection budgetSubcategoryCollection = BudgetBL.Instance.GetBudgetSubcategory();
            int? budgetSubId = ppBudgetItem.BudgetSubcategoryId;
            if (budgetSubcategoryCollection == null || budgetSubcategoryCollection.Count < 1 || budgetSubId == null)
                return true;
            foreach (BudgetSubcategoryDTO item in budgetSubcategoryCollection)
            {
                if (item.BudgetSubcategoryID == budgetSubId)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check valid proposed pre-purchase budget subcategory id
        /// <input>ForeclosureCaseDTO</input>
        /// <return>bool<return>
        /// </summary>
        private ExceptionMessageCollection CheckValidProposedPPBudgetSubcategoryId(PPPBudgetItemDTOCollection propossedPPBudgetItem)
        {
            ExceptionMessageCollection msgPPCaseSet = new ExceptionMessageCollection();
            if (propossedPPBudgetItem == null || propossedPPBudgetItem.Count < 1)
                return null;
            for (int i = 0; i < propossedPPBudgetItem.Count; i++)
            {
                PPPBudgetItemDTO item = propossedPPBudgetItem[i];
                bool isValid = CheckBudgetSubcategory(item);
                if (!isValid)
                    msgPPCaseSet.AddExceptionMessage(ErrorMessages.ERR0262, ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR0262));
            }
            return msgPPCaseSet;
        }
        private bool CheckBudgetSubcategory(PPPBudgetItemDTO ppBudgetItem)
        {
            BudgetSubcategoryDTOCollection budgetSubcategoryCollection = BudgetBL.Instance.GetBudgetSubcategory();
            int? budgetSubId = ppBudgetItem.BudgetSubcategoryId;
            if (budgetSubcategoryCollection == null || budgetSubcategoryCollection.Count < 1 || budgetSubId == null)
                return true;
            foreach (BudgetSubcategoryDTO item in budgetSubcategoryCollection)
            {
                if (item.BudgetSubcategoryID == budgetSubId)
                    return true;
            }
            return false;
        }
        #endregion

        #region Utility
        private string ConvertStringToUpper(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;
            s = s.ToUpper().Trim();
            return s;
        }

        private string ConvertStringEmptyToNull(string s)
        {
            if (s == null)
                return null;
            s = s.Trim();
            if (string.IsNullOrEmpty(s))
                return null;
            return s.Trim(); ;
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

        private bool CompareString(string str1, string str2)
        {
            var temp1 = ConvertStringEmptyToNull(str1);
            var temp2 = ConvertStringEmptyToNull(str2);
            if (temp1 == null && temp2 == null)
                return true;
            if ((temp1 == null && temp2 != null) || (temp1 != null && temp2 == null))
                return false;
            if (temp1.ToUpper() == temp2.ToUpper())
                return true;
            return false;
        }
        #endregion
    }
}
