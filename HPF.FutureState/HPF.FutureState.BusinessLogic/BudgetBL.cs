using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.DataAccess;


namespace HPF.FutureState.BusinessLogic
{
    public class BudgetBL:BaseBusinessLogic
    {
        private static readonly BudgetBL instance = new BudgetBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static BudgetBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected BudgetBL()
        {            
        }

        /// <summary>
        /// Get BudgetDetailDTO from 2 recordset retun by SP.
        /// </summary>
        /// <param name="budgetSetId">BudgetSet ID</param>
        /// <returns>BudgetDetailDTO contains 2 collections: BugetItem and BudgetAsset</returns>
        public BudgetDetailDTO GetBudgetDetail(int? budgetSetId)
        {
            BudgetDetailDTO result= BudgetDAO.Instance.GetBudgetDetail(budgetSetId);
            if (result.BudgetAssetCollection.Count > 0)
            {
                //Attach total Asset row in to AssetCollection
                double? sum = 0;
                foreach (var budgetAsset in result.BudgetAssetCollection)
                    sum += budgetAsset.AssetValue;
                BudgetAssetDTO totalRow = new BudgetAssetDTO { AssetValue = sum, AssetName = "Total Assets" };
                result.BudgetAssetCollection.Add(totalRow);
            }
            return result;
        }
        /// <summary>
        /// Get BudgetSet by ForeclosureCaseId
        /// </summary>
        /// <param name="caseId">ForeclosureCaseId</param>
        /// <returns>BudgetSEtDTOCollection</returns>
        public BudgetSetDTO GetBudgetSet(int? fcId)
        {
            return BudgetDAO.Instance.GetBudgetSet(fcId);
        }
        public BudgetSetDTOCollection GetBudgetSetList(int caseId)
        {
            return BudgetDAO.Instance.GetBudgetSetList(caseId);
        }

        public BudgetSetDTO GetProposedBudgetSet(int? fcId)
        {
            return BudgetDAO.Instance.GetProposedBudgetSet(fcId);
        }
        /// <summary>
        /// Group all the Budget Item by BudgetCategory
        /// </summary>
        /// <param name="budgetItemCollection">Collection contants all the BudgetItem</param>
        /// <returns>Collection contains all BudgetItemCollection </returns>
        public BudgetDetailDTOCollection GroupBudgetItem(BudgetItemDTOCollection budgetItemCollection)
        {
            BudgetDetailDTOCollection result = new BudgetDetailDTOCollection();
            foreach (var budgetItem in budgetItemCollection)
            {
                int index = result.IndexOf(budgetItem.BudgetCategory);
                //not exists
                if (index == -1)
                {
                    BudgetItemDTOCollection newItemCollection = new BudgetItemDTOCollection();
                    newItemCollection.BudgetCategory = budgetItem.BudgetCategory;
                    newItemCollection.Add(budgetItem);
                    result.Add(newItemCollection);
                }
                else //exists
                {
                    BudgetItemDTOCollection itemCollection = result[index];
                    budgetItem.BudgetCategory = "";
                    itemCollection.Add(budgetItem);
                }
            }
            //Add total row to BudgetItem
            foreach (var budgetGroup in result)
            { 
                double? sum =0;
                foreach (var budgetItem in budgetGroup)
                    sum += budgetItem.BudgetItemAmt;
                BudgetItemDTO totalRow = new BudgetItemDTO { BudgetItemAmt = sum, BudgetSubCategory =  "Total " + budgetGroup.BudgetCategory};
                budgetGroup.Add(totalRow);
            }
            return result;
        }

        public BudgetSubcategoryDTOCollection GetBudgetSubcategory()
        {
            return BudgetDAO.Instance.GetBudgetSubcategory();
        }

        public BudgetItemDTOCollection GetBudgetItemSet(int? fcId)
        {
            return BudgetDAO.Instance.GetBudgetItemSet(fcId);
        }
        
        public BudgetItemDTOCollection GetProposedBudgetItemSet(int? fcId)
        {
            return BudgetDAO.Instance.GetProposedBudgetItemSet(fcId);
        }

        public BudgetAssetDTOCollection GetBudgetAssetSet(int? fcId)
        {
            return BudgetDAO.Instance.GetBudgetAssetSet(fcId);
        }
        public BudgetDTOCollection GetBudget()
        {
            return BudgetDAO.Instance.GetBudget();
        }
    }
}
