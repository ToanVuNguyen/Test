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
        private bool IsPrePurchaseCaseInserted;
        PrePurchaseCaseSetDTO PPCaseSetFromDB = new PrePurchaseCaseSetDTO();
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

        #region Implementation of IPrePurchaseCaseBL
        public PrePurchaseCaseDTO SavePrePurchaseCaseSet(PrePurchaseCaseSetDTO prePurchaseCaseSet)
        {
            int? ppCaseId;
            if (prePurchaseCaseSet == null || prePurchaseCaseSet.PrePurchaseCase == null)
                throw new DataValidationException(ErrorMessages.GetExceptionMessageCombined(ErrorMessages.ERR1165));
            var exceptionList = CheckRequireForPartial(prePurchaseCaseSet);

            _workingUserId = prePurchaseCaseSet.PrePurchaseCase.ChgLstUserId;

            var formatDataException = CheckInvalidFormatData(prePurchaseCaseSet);
            exceptionList.Add(formatDataException);
            exceptionList.Add(CheckValidCode(prePurchaseCaseSet));
            if (exceptionList.Count > 0)
                ThrowDataValidationException(exceptionList);
            PrePurchaseCaseDTO ppCase = prePurchaseCaseSet.PrePurchaseCase;
            LoadPrePurchaseCaseFromDB(ppCase.PPCaseId);
            if (ppCase.PPCaseId.HasValue)
                ppCaseId = ProcessInsertUpdateWithPrePurchaseCaseId(prePurchaseCaseSet);
            else
                ppCaseId = ProcessInsertUpdateWithoutPrePurchaseCaseId(prePurchaseCaseSet);
            prePurchaseCaseSet.PrePurchaseCase.PPCaseId = ppCaseId;
            return prePurchaseCaseSet.PrePurchaseCase;
        }
        #endregion

        #region Functions to save PrePurchaseCaseSet

        private int? ProcessInsertUpdateWithoutPrePurchaseCaseId(PrePurchaseCaseSetDTO prePurchaseCaseSet)
        {
            PrePurchaseCaseDTO ppCase = prePurchaseCaseSet.PrePurchaseCase;
            if (CheckExistingAgencyIdAndCaseNumber(ppCase.AgencyId, ppCase.AgencyCaseNum))
                ThrowDataValidationException(ErrorMessages.ERR1173);
            return ProcessInsertPrePurchaseCaseSet(prePurchaseCaseSet);
        }
        private int? ProcessInsertUpdateWithPrePurchaseCaseId(PrePurchaseCaseSetDTO prePurchaseCaseSet)
        {
            PrePurchaseCaseDTO ppCase = prePurchaseCaseSet.PrePurchaseCase;

            //check valid ppCase for Agency
            if (PPCaseSetFromDB.PrePurchaseCase.AgencyId != ppCase.AgencyId)
                ThrowDataValidationException(ErrorMessages.ERR1174);

            if (IsPrePurchaseCaseInserted)
                return ProcessInsertPrePurchaseCaseSet(prePurchaseCaseSet);
            else
                return ProcessUpdatePrePurchaseCaseSet(prePurchaseCaseSet);
        }

        private void LoadPrePurchaseCaseFromDB(int? ppCaseId)
        {
            IsPrePurchaseCaseInserted = true;
            if (!ppCaseId.HasValue) return;

            IsPrePurchaseCaseInserted = false;
            //check ppCaseId in db or not
            PPCaseSetFromDB.PrePurchaseCase = GetPrePurchaseCase(ppCaseId);

            if (PPCaseSetFromDB.PrePurchaseCase == null)
                ThrowDataValidationException(ErrorMessages.ERR1175);
            else
            {   
                //User this data to check data changed or not in Update function
                PPCaseSetFromDB.PPBudgetSet = PPBudgetBL.Instance.GetPPBudgetSet(ppCaseId);
                PPCaseSetFromDB.ProposedPPBudgetSet = PPBudgetBL.Instance.GetProposedPPBudgetSet(ppCaseId);
                PPCaseSetFromDB.PPBudgetAssets = PPBudgetBL.Instance.GetPPBudgetAssetSet(ppCaseId);
                PPCaseSetFromDB.PPBudgetItems = PPBudgetBL.Instance.GetPPBudgetItemSet(ppCaseId);
                PPCaseSetFromDB.ProposedPPBudgetItems = PPBudgetBL.Instance.GetProposedPPBudgetItemSet(ppCaseId);
            }
        }

        private int? ProcessUpdatePrePurchaseCaseSet(PrePurchaseCaseSetDTO prePurchaseCaseSet)
        {
            return UpdatePrePurchaseCaseSet(prePurchaseCaseSet);
        }
        private int? ProcessInsertPrePurchaseCaseSet(PrePurchaseCaseSetDTO prePurchaseCaseSet)
        {
            ///Can add actions to validate insert process here
            return InsertPrePurchaseCaseSet(prePurchaseCaseSet);
        }

        /// <summary>
        /// Check existed AgencyId and Case number
        /// </summary>
        bool CheckExistingAgencyIdAndCaseNumber(int? agencyId, string caseNumner)
        {
            return prePurchaseCaseSetDAO.CheckExistingAgencyIdAndCaseNumber(agencyId, caseNumner);
        }
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

        #region Function Insert Pre-Purcase Case Set
        /// <summary>
        /// Insert the PrePurchaseCaseSet
        /// </summary>
        private int? InsertPrePurchaseCaseSet(PrePurchaseCaseSetDTO prePurchaseCaseSet)
        {
            PPBudgetSetDTO ppBudgetSet = AssignPPBudgetSetHPFAuto(prePurchaseCaseSet.PPBudgetAssets, prePurchaseCaseSet.PPBudgetItems);
            prePurchaseCaseSet.PPBudgetSet = ppBudgetSet;
            
            PPBudgetItemDTOCollection ppBudgetItemCollection = prePurchaseCaseSet.PPBudgetItems;
            PPBudgetAssetDTOCollection ppBudgetAssetCollection = prePurchaseCaseSet.PPBudgetAssets;
            PrePurchaseCaseDTO prePurchaseCase = prePurchaseCaseSet.PrePurchaseCase;
            int? ppCaseId = 0;
            try
            {
                InitiateTransaction();
                //Insert Pre_Purchase_Case table
                //Return pp_case_id
                ppCaseId = InsertPrePurchaseCase(prePurchaseCaseSetDAO, prePurchaseCase);

                //Insert Pre-Purchase Budget Set table
                //Return Pre-Purchase Budget Set Id
                int? ppBudgetSetId = InsertPPBudgetSet(prePurchaseCaseSetDAO, ppBudgetSet, ppCaseId);
                //Insert Pre-Purchase Budget Item table 
                InsertPPBudgetItem(prePurchaseCaseSetDAO, ppBudgetItemCollection, ppBudgetSetId);
                //Insert Pre-Purchase Budget Asset table
                InsertPPBudgetAsset(prePurchaseCaseSetDAO, ppBudgetAssetCollection, ppBudgetSetId);

                if (prePurchaseCaseSet.ProposedPPBudgetItems != null && prePurchaseCaseSet.ProposedPPBudgetItems.Count > 0)
                {
                    PPPBudgetSetDTO proposedPPBudgetSet = AssignProposedPPBudgetSetHPFAuto(prePurchaseCaseSet.ProposedPPBudgetItems);
                    int? proposedPPBudgetSetId = InsertProposedPPBudgetSet(prePurchaseCaseSetDAO, proposedPPBudgetSet, ppCaseId);
                    InsertProposedPPBudgetItem(prePurchaseCaseSetDAO, prePurchaseCaseSet.ProposedPPBudgetItems, proposedPPBudgetSetId);
                }
                CompleteTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return ppCaseId;
        }
        #endregion

        #region Function Update Pre-Purchase Case Set
        /// <summary>
        /// Update the Pre-Purchase Case Set
        /// </summary>
        private int? UpdatePrePurchaseCaseSet(PrePurchaseCaseSetDTO prePurchaseCaseSet)
        {
            PPBudgetSetDTO ppBudgetSet = AssignPPBudgetSetHPFAuto(prePurchaseCaseSet.PPBudgetAssets, prePurchaseCaseSet.PPBudgetItems);
            prePurchaseCaseSet.PPBudgetSet = ppBudgetSet;
            
            PPBudgetItemDTOCollection ppBudgetItemCollection = prePurchaseCaseSet.PPBudgetItems;
            PPBudgetAssetDTOCollection ppBudgetAssetCollection = prePurchaseCaseSet.PPBudgetAssets;
            PrePurchaseCaseDTO prePurchaseCase = prePurchaseCaseSet.PrePurchaseCase;
            int? ppCaseId = 0;
            try
            {
                InitiateTransaction();
                //Update Pre-Purchase Case Table
                //Return Fc_id
                ppCaseId = UpdatePrePurchaseCase(prePurchaseCaseSetDAO, prePurchaseCase);

                //check changed from pre-purchase budgetItem and pre-purchase budget asset
                //if they are changed, insert new pre-purchase budget set, pre-purchase budget Item and pre-purchase budget asset
                InsertPPBudget(prePurchaseCaseSetDAO, ppBudgetSet, ppBudgetItemCollection, ppBudgetAssetCollection, ppCaseId);

                prePurchaseCaseSet.ProposedPPBudgetSet = AssignProposedPPBudgetSetHPFAuto(prePurchaseCaseSet.ProposedPPBudgetItems);
                InsertProposedPPBudget(prePurchaseCaseSetDAO, prePurchaseCaseSet.ProposedPPBudgetSet, prePurchaseCaseSet.ProposedPPBudgetItems, ppCaseId);
                
                CompleteTransaction();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            return ppCaseId;
        }

        /// <summary>
        /// Update prePurchaseCase
        /// </summary>
        private int? UpdatePrePurchaseCase(PrePurchaseCaseSetDAO prePurchaseCaseSetDAO, PrePurchaseCaseDTO prePurchaseCase)
        {
            prePurchaseCase.SetUpdateTrackingInformation(_workingUserId);
            return prePurchaseCaseSetDAO.UpdatePrePurchaseCase(prePurchaseCase);

        }
        #endregion

        #region Functions for Insert and Update tables
        /// <summary>
        /// Insert PPBudget
        /// </summary>
        private void InsertPPBudget(PrePurchaseCaseSetDAO prePurchaseCaseSetDAO, PPBudgetSetDTO ppBudgetSet, PPBudgetItemDTOCollection ppBudgetItemCollection, PPBudgetAssetDTOCollection ppBudgetAssetCollection, int? ppCaseId)
        {
            bool isInsertBudget = IsInsertPPBudgetSet(PPCaseSetFromDB.PPBudgetItems, ppBudgetItemCollection, PPCaseSetFromDB.PPBudgetAssets, ppBudgetAssetCollection);
            if (isInsertBudget)
            {
                //Insert Pre-Purchase Budget Set Table
                //Return PPBudgetSetId
                int? ppBudgetSetId = InsertPPBudgetSet(prePurchaseCaseSetDAO, ppBudgetSet, ppCaseId);
                //Insert Pre-Purchase Budget Item Table
                InsertPPBudgetItem(prePurchaseCaseSetDAO, ppBudgetItemCollection, ppBudgetSetId);
                //Insert Pre-Purchase Budget Asset Table
                InsertPPBudgetAsset(prePurchaseCaseSetDAO, ppBudgetAssetCollection, ppBudgetSetId);
            }
        }

        private void InsertProposedPPBudget(PrePurchaseCaseSetDAO prePurchaseCaseSetDAO, PPPBudgetSetDTO proposedPPBudgetSet, PPPBudgetItemDTOCollection proposedPPBudgetItemCollection, int? ppCaseId)
        {
            bool isInsertBudget = IsInsertPPBudgetSet(PPCaseSetFromDB.ProposedPPBudgetItems, proposedPPBudgetItemCollection);
            if (isInsertBudget && proposedPPBudgetItemCollection != null && proposedPPBudgetItemCollection.Count > 0)
            {
                //Insert Proposed Pre-Purchase Budget Set table
                //Return Proposed Pre-Purchase Budget Set Id
                int? proposedPPBudgetSetId = InsertProposedPPBudgetSet(prePurchaseCaseSetDAO, proposedPPBudgetSet, ppCaseId);
                //Insert Proposed Pre-Purchase Budget Item
                InsertProposedPPBudgetItem(prePurchaseCaseSetDAO, proposedPPBudgetItemCollection, proposedPPBudgetSetId);
            }
        }
        /// <summary>
        /// Insert Pre-Purchase Budget Asset
        /// </summary>
        private void InsertPPBudgetAsset(PrePurchaseCaseSetDAO prePurchaseCaseSetDAO, PPBudgetAssetDTOCollection ppBudgetAssetCollection, int? ppBudgetSetId)
        {
            if (ppBudgetAssetCollection != null)
            {
                foreach (PPBudgetAssetDTO item in ppBudgetAssetCollection)
                {
                    item.SetInsertTrackingInformation(_workingUserId);
                    prePurchaseCaseSetDAO.InsertPPBudgetAsset(item, ppBudgetSetId);
                }
            }
        }

        /// <summary>
        /// Insert Pre-Purchase Budget Item
        /// </summary>
        private void InsertPPBudgetItem(PrePurchaseCaseSetDAO prePurchaseCaseSetDAO, PPBudgetItemDTOCollection ppBudgetItemCollection, int? ppBudgetSetId)
        {
            if (ppBudgetItemCollection != null)
            {
                foreach (PPBudgetItemDTO item in ppBudgetItemCollection)
                {
                    item.SetInsertTrackingInformation(_workingUserId);
                    prePurchaseCaseSetDAO.InsertPPBudgetItem(item, ppBudgetSetId);
                }
            }
        }

        /// <summary>
        /// Insert Proposed Pre-Purchase Budget Item
        /// </summary>
        private void InsertProposedPPBudgetItem(PrePurchaseCaseSetDAO prePurchaseCaseSetDAO, PPPBudgetItemDTOCollection proposedPPBudgetItemCollection, int? proposedPPBudgetSetId)
        {
            if (proposedPPBudgetItemCollection != null && proposedPPBudgetSetId != null)
            {
                foreach (PPPBudgetItemDTO item in proposedPPBudgetItemCollection)
                {
                    item.SetInsertTrackingInformation(_workingUserId);
                    prePurchaseCaseSetDAO.InsertProposedPPBudgetItem(item, proposedPPBudgetSetId);
                }
            }
        }

        /// <summary>
        /// Insert Pre-Purchase Budget Set
        /// </summary>
        private int? InsertPPBudgetSet(PrePurchaseCaseSetDAO prePurchaseCaseSetDAO, PPBudgetSetDTO ppBudgetSet, int? ppCaseId)
        {
            int? ppBudgetSetId = null;
            if (ppBudgetSet != null)
            {
                ppBudgetSet.SetInsertTrackingInformation(_workingUserId);
                ppBudgetSetId = prePurchaseCaseSetDAO.InsertPPBudgetSet(ppBudgetSet, ppCaseId);
            }
            return ppBudgetSetId;
        }

        /// <summary>
        /// Insert Proposed Pre-Purchase Budget Set
        /// </summary>
        private int? InsertProposedPPBudgetSet(PrePurchaseCaseSetDAO prePurchaseCaseSetDAO, PPPBudgetSetDTO proposedPPBudgetSet, int? ppCaseId)
        {
            int? proposedPPBudgetSetId = null;
            if (proposedPPBudgetSet != null)
            {
                proposedPPBudgetSet.SetInsertTrackingInformation(_workingUserId);
                proposedPPBudgetSetId = prePurchaseCaseSetDAO.InsertProposedPPBudgetSet(proposedPPBudgetSet, ppCaseId);
            }
            return proposedPPBudgetSetId;
        }

        /// <summary>
        /// Insert PrePurchaseCase
        /// </summary>
        private int? InsertPrePurchaseCase(PrePurchaseCaseSetDAO prePurchaseCaseSetDAO, PrePurchaseCaseDTO prePurchaseCase)
        {
            int? ppCaseId = null;
            if (prePurchaseCase != null)
            {
                prePurchaseCase.SetInsertTrackingInformation(_workingUserId);
                ppCaseId = prePurchaseCaseSetDAO.InsertPrePurchaseCase(prePurchaseCase);
            }
            return ppCaseId;
        }
        #endregion

        #region Functions check for insert Pre-Purchase Budget
        /// <summary>
        /// Check Pre-Purchase Budget Item input with Pre-Purchase Budget Item from DB
        /// If difference Insert New Pre-Purchase Budget Items
        /// VS: Do nothing
        /// </summary>
        /// <param name>PPBudgetItemDTOCollection</param>
        /// <returns>true: if have difference</returns>
        private bool IsPPBudgetItemsDifference(PPBudgetItemDTOCollection ppBudgetCollectionDB, PPBudgetItemDTOCollection ppBudgetCollectionInput)
        {
            if ((ppBudgetCollectionInput != null && ppBudgetCollectionDB == null) || (ppBudgetCollectionInput == null && ppBudgetCollectionDB != null))
                return true;
            if (ppBudgetCollectionDB != null && ppBudgetCollectionInput != null && ppBudgetCollectionDB.Count != ppBudgetCollectionInput.Count)
                return true;
            if (ppBudgetCollectionDB != null && ppBudgetCollectionInput != null && ppBudgetCollectionDB.Count == ppBudgetCollectionInput.Count)
            {
                foreach (PPBudgetItemDTO budgetItem in ppBudgetCollectionInput)
                {
                    if (!ComparePPBudgetItem(budgetItem, ppBudgetCollectionDB))
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Check Proposed Pre-Purchase Budget Item input with Proposed Pre-Purchase Budget Item from DB
        /// If difference Insert New Proposed Pre-Purchase Budget Items
        /// VS: Do nothing
        /// </summary>
        /// <param name>PPPBudgetItemDTOCollection</param>
        /// <returns>true: if have difference</returns>
        private bool IsPPBudgetItemsDifference(PPPBudgetItemDTOCollection proposedPPBudgetCollectionDB, PPPBudgetItemDTOCollection proposedPPBudgetCollectionInput)
        {
            if ((proposedPPBudgetCollectionInput != null && proposedPPBudgetCollectionDB == null) || (proposedPPBudgetCollectionInput == null && proposedPPBudgetCollectionDB != null))
                return true;
            if (proposedPPBudgetCollectionDB != null && proposedPPBudgetCollectionInput != null && proposedPPBudgetCollectionDB.Count != proposedPPBudgetCollectionInput.Count)
                return true;
            if (proposedPPBudgetCollectionDB != null && proposedPPBudgetCollectionInput != null && proposedPPBudgetCollectionDB.Count == proposedPPBudgetCollectionInput.Count)
            {
                foreach (PPPBudgetItemDTO budgetItem in proposedPPBudgetCollectionInput)
                {
                    if (!ComparePPBudgetItem(budgetItem, proposedPPBudgetCollectionDB))
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Compare Pre-Purchase BudgetItem input with Pre-Purchase BudgetItems from DB        
        /// <returns>false: if have difference</returns>
        private bool ComparePPBudgetItem(PPBudgetItemDTO ppBudgetItemInput, PPBudgetItemDTOCollection ppBudgetCollectionDB)
        {
            foreach (PPBudgetItemDTO ppBudgetItemDB in ppBudgetCollectionDB)
            {
                if (ppBudgetItemInput.BudgetSubcategoryId == ppBudgetItemDB.BudgetSubcategoryId
                    && ppBudgetItemInput.PPBudgetItemAmt == ppBudgetItemDB.PPBudgetItemAmt
                    && CompareString(ppBudgetItemInput.PPBudgetNote, ppBudgetItemDB.PPBudgetNote))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Compare Proposed Pre-Purchase BudgetItem input with Proposed Pre-Purchase BudgetItems from DB        
        /// <returns>false: if have difference</returns>
        private bool ComparePPBudgetItem(PPPBudgetItemDTO proposedPPBudgetItemInput, PPPBudgetItemDTOCollection proposedPPBudgetCollectionDB)
        {
            foreach (PPPBudgetItemDTO proposedPPBudgetItemDB in proposedPPBudgetCollectionDB)
            {
                if (proposedPPBudgetItemInput.BudgetSubcategoryId == proposedPPBudgetItemDB.BudgetSubcategoryId
                    && proposedPPBudgetItemInput.ProposedBudgetItemAmt == proposedPPBudgetItemDB.ProposedBudgetItemAmt
                    && CompareString(proposedPPBudgetItemInput.ProposedBudgetNote, proposedPPBudgetItemDB.ProposedBudgetNote))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check Pre-purchase Budget Asset input with Pre-purchase Budget Asset from DB
        /// If difference Insert New Pre-purchase Budget Assets
        /// VS: Do nothing
        /// </summary>
        /// <param name>PPBudgetAssetDTOCollection</param>
        /// <returns>true: if have difference</returns>
        private bool IsPPBudgetAssetDifference(PPBudgetAssetDTOCollection ppBudgetAssetCollectionDB, PPBudgetAssetDTOCollection ppBudgetAssetCollectionInput)
        {
            if ((ppBudgetAssetCollectionInput != null && ppBudgetAssetCollectionDB == null) || (ppBudgetAssetCollectionInput == null && ppBudgetAssetCollectionDB != null))
                return true;
            if (ppBudgetAssetCollectionDB != null && ppBudgetAssetCollectionInput != null && ppBudgetAssetCollectionDB.Count != ppBudgetAssetCollectionInput.Count)
                return true;
            if (ppBudgetAssetCollectionDB != null && ppBudgetAssetCollectionInput != null && ppBudgetAssetCollectionDB.Count == ppBudgetAssetCollectionInput.Count)
            {
                foreach (PPBudgetAssetDTO ppBudgetAssetInput in ppBudgetAssetCollectionInput)
                {
                    if (!ComparePPBudgetAsset(ppBudgetAssetInput, ppBudgetAssetCollectionDB))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Compare pre-purchase budgetAsset item input with pre-purchase BudgetAsset from DB        
        /// <returns>false: if have difference</returns>
        private bool ComparePPBudgetAsset(PPBudgetAssetDTO ppBudgetAssetInput, PPBudgetAssetDTOCollection ppBudgetCollectionDB)
        {
            foreach (PPBudgetAssetDTO ppBudgetAssetDB in ppBudgetCollectionDB)
            {
                if (CompareString(ppBudgetAssetInput.PPBudgetAssetName, ppBudgetAssetDB.PPBudgetAssetName)
                    && ppBudgetAssetInput.PPBudgetAssetValue == ppBudgetAssetDB.PPBudgetAssetValue)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check for Insert new pre-purchased budget Set
        /// If pre-purchase budgetItem changed or pre-purchase budgetAsset changed insert new
        /// VS: do Nothing
        /// </summary>
        /// <param name>PPBudgetAssetDTOCollection,PPBudgetAssetDTOCollection</param>
        /// <returns>true: if have difference</returns>
        private bool IsInsertPPBudgetSet(PPBudgetItemDTOCollection ppBudgetCollectionDB, PPBudgetItemDTOCollection ppBudgetItemCollection, PPBudgetAssetDTOCollection ppBudgetAssetCollectionDB, PPBudgetAssetDTOCollection ppBudgetAssetCollection)
        {
            bool ppBudgetItem = IsPPBudgetItemsDifference(ppBudgetCollectionDB, ppBudgetItemCollection);
            bool ppBudgetAsset = IsPPBudgetAssetDifference(ppBudgetAssetCollectionDB, ppBudgetAssetCollection);
            return (ppBudgetItem || ppBudgetAsset);
        }
        /// <summary>
        /// Check for Insert new proposed pre-purchased budget Set
        /// If proposed pre-purchase budgetItem changed, insert new
        /// VS: do Nothing
        /// </summary>
        /// <param name>PPPBudgetAssetDTOCollection </param>
        /// <returns>true: if have difference</returns>
        private bool IsInsertPPBudgetSet(PPPBudgetItemDTOCollection proposedPPBudgetCollectionDB, PPPBudgetItemDTOCollection proposedPPBudgetItemCollection)
        {
            bool proposedPPBudgetItem = IsPPBudgetItemsDifference(proposedPPBudgetCollectionDB, proposedPPBudgetItemCollection);
            return proposedPPBudgetItem;
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

        #region Functions Set PrePurchaseCaseSet-Auto
        /// <summary>
        /// Add value HPF-Auto for PPBudget Set
        /// </summary>
        private PPBudgetSetDTO AssignPPBudgetSetHPFAuto(PPBudgetAssetDTOCollection ppBudgetAssetCollection, PPBudgetItemDTOCollection ppBudgetItemCollection)
        {
            PPBudgetSetDTO ppBudgetSet = new PPBudgetSetDTO();
            double? totalIncome = 0;
            double? totalExpenses = 0;
            double? totalAssest = 0;
                        
            if (ppBudgetAssetCollection != null)
                totalAssest = CalculateTotalAssets(ppBudgetAssetCollection, totalAssest);
            if (ppBudgetItemCollection != null)
                CalculateTotalExpenseAndIncome(ppBudgetItemCollection, ref totalIncome, ref totalExpenses);

            ppBudgetSet.TotalAssets = totalAssest;
            ppBudgetSet.TotalExpenses = totalExpenses;
            ppBudgetSet.TotalIncome = totalIncome;
            ppBudgetSet.PPBudgetSetDt = DateTime.Now;
            
            ppBudgetSet.SetInsertTrackingInformation(_workingUserId);
            return ppBudgetSet;
        }
        /// <summary>
        /// Add value HPF-Auto for PPBudget Set
        /// </summary>
        private PPPBudgetSetDTO AssignProposedPPBudgetSetHPFAuto(PPPBudgetItemDTOCollection proposedPPBudgetItemCollection)
        {
            PPPBudgetSetDTO proposedPPBudgetSet = new PPPBudgetSetDTO();
            double? totalIncome = 0;
            double? totalExpenses = 0;
            double? totalAssest = 0;

            if (proposedPPBudgetItemCollection != null)
                CalculateTotalExpenseAndIncome(proposedPPBudgetItemCollection, ref totalIncome, ref totalExpenses);

            proposedPPBudgetSet.TotalAssets = 0;
            proposedPPBudgetSet.TotalExpenses = totalExpenses;
            proposedPPBudgetSet.TotalIncome = totalIncome;
            proposedPPBudgetSet.PPPBudgetSetDt = DateTime.Now;

            proposedPPBudgetSet.SetInsertTrackingInformation(_workingUserId);
            return proposedPPBudgetSet;
        }
        /// <summary>
        /// Calculate Total Assets
        /// </summary>
        private double? CalculateTotalAssets(PPBudgetAssetDTOCollection ppBudgetAssetCollection, double? totalAssest)
        {
            //Calculate totalAssest
            if (ppBudgetAssetCollection == null || ppBudgetAssetCollection.Count < 1)
            {
                return 0;
            }
            totalAssest = 0;
            foreach (PPBudgetAssetDTO item in ppBudgetAssetCollection)
            {
                totalAssest += item.PPBudgetAssetValue;
            }
            return totalAssest;
        }

        /// <summary>
        /// Calculate Total Expense And Income of PPBudgetItemCollection
        /// </summary>
        private void CalculateTotalExpenseAndIncome(PPBudgetItemDTOCollection ppBudgetItemCollection, ref double? totalIncome, ref double? totalExpenses)
        {
            totalIncome = 0;
            totalExpenses = 0;
            foreach (var item in ppBudgetItemCollection)
            {
                string budgetCode = BuggetCategoryCode(item.BudgetSubcategoryId);
                if (budgetCode != null)
                {
                    if (budgetCode == Constant.INCOME)
                        totalIncome += item.PPBudgetItemAmt;
                    else if (budgetCode == Constant.EXPENSES)
                        totalExpenses += item.PPBudgetItemAmt;
                }
            }
        }
        /// <summary>
        /// Calculate Total Expense And Income of Proposed PPBudgetItemCollection
        /// </summary>
        private void CalculateTotalExpenseAndIncome(PPPBudgetItemDTOCollection proposedPPBudgetItemCollection, ref double? totalIncome, ref double? totalExpenses)
        {
            totalIncome = 0;
            totalExpenses = 0;
            foreach (var item in proposedPPBudgetItemCollection)
            {
                string budgetCode = BuggetCategoryCode(item.BudgetSubcategoryId);
                if (budgetCode != null)
                {
                    if (budgetCode == Constant.INCOME)
                        totalIncome += item.ProposedBudgetItemAmt;
                    else if (budgetCode == Constant.EXPENSES)
                        totalExpenses += item.ProposedBudgetItemAmt;
                }
            }
        }

        private string BuggetCategoryCode(int? subCategoryId)
        {
            BudgetDTOCollection budgetCollection = BudgetBL.Instance.GetBudget();
            if (budgetCollection == null || budgetCollection.Count < 1)
                return null;
            foreach (BudgetDTO item in budgetCollection)
            {
                if (subCategoryId == item.BudgetSubcategoryId)
                    return item.BudgetCategoryCode;
            }
            return null;
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
        #region Throw Detail Exception
        private void ThrowDataValidationException(string errorCode)
        {
            DataValidationException ex = new DataValidationException();
            ex.ExceptionMessages.AddExceptionMessage(errorCode, ErrorMessages.GetExceptionMessageCombined(errorCode));
            throw ex;
        }
        private static void ThrowDataValidationException(ExceptionMessageCollection exDetailCollection)
        {
            var ex = new DataValidationException(exDetailCollection);
            throw ex;
        }
        #endregion
        #region Manage Transaction
        private void RollbackTransaction()
        {
            prePurchaseCaseSetDAO.Rollback();
        }

        private void CompleteTransaction()
        {
            prePurchaseCaseSetDAO.Commit(); ;
        }

        private void InitiateTransaction()
        {
            prePurchaseCaseSetDAO.Begin();
        }
        private void CloseConnection()
        {
            prePurchaseCaseSetDAO.CloseConnection();
        }
        #endregion
        #endregion
        private PrePurchaseCaseDTO GetPrePurchaseCase(int? ppCaseId)
        {
            return prePurchaseCaseSetDAO.GetPrePurchaseCase(ppCaseId);
        }
    }
}
