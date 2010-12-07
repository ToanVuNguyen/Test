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
    public class PPBudgetBL:BaseBusinessLogic
    {
        private static PPBudgetBL _instance = new PPBudgetBL();
        public static PPBudgetBL Instance
        {
            get { return _instance; }
        }
        protected PPBudgetBL() { }

        /// <summary>
        /// Get PPBudgetDetailDTO from 2 recordset retun by SP.
        /// </summary>
        /// <param name="ppBudgetSetId">PPBudgetSet ID</param>
        /// <returns>PPBudgetDetailDTO contains 2 collections: PPBugetItem and PPBudgetAsset</returns>
        public PPBudgetDetailDTO GetPPBudgetDetail(int? ppBudgetSetId)
        {
            PPBudgetDetailDTO result = PPBudgetDAO.Instance.GetPPBudgetDetail(ppBudgetSetId);
            if (result.PPBudgetAssetCollection.Count > 0)
            {
                //Attach total Asset row in to AssetCollection
                double? sum = 0;
                foreach (var ppBudgetAsset in result.PPBudgetAssetCollection)
                    sum += ppBudgetAsset.PPBudgetAssetValue;
                PPBudgetAssetDTO totalRow = new PPBudgetAssetDTO { PPBudgetAssetValue = sum, PPBudgetAssetName = "Total Assets" };
                result.PPBudgetAssetCollection.Add(totalRow);
            }
            return result;
        }
        /// <summary>
        /// Get PPBudgetSet by PrePurchaseCaseId
        /// </summary>
        /// <param name="ppcId">PrePurchaseCaseId</param>
        /// <returns>PPBudgetSetDTOCollection</returns>
        public PPBudgetSetDTO GetPPBudgetSet(int? ppcId)
        {
            return PPBudgetDAO.Instance.GetPPBudgetSet(ppcId);
        }
        public PPPBudgetSetDTO GetProposedPPBudgetSet(int? ppcId)
        {
            return PPBudgetDAO.Instance.GetProposedPPBudgetSet(ppcId);
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
        public PPBudgetItemDTOCollection GetPPBudgetItemSet(int? ppcId)
        {
            return PPBudgetDAO.Instance.GetPPBudgetItemSet(ppcId);
        }

        public PPPBudgetItemDTOCollection GetProposedPPBudgetItemSet(int? ppcId)
        {
            return PPBudgetDAO.Instance.GetProposedPPBudgetItemSet(ppcId);
        }

        public PPBudgetAssetDTOCollection GetPPBudgetAssetSet(int? ppcId)
        {
            return PPBudgetDAO.Instance.GetPPBudgetAssetSet(ppcId);
        }
    }
}
