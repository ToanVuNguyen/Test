using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.BusinessLogicInterface;

namespace HPF.FutureState.BusinessLogic
{
    public class ReferenceCodeValidatorBL : IReferenceCodeValidatorBL
    {
        public bool Validate(ReferenceCode refCode, string value)
        {
            RefCodeItemDTOCollection refCodeItemCollection = RefCodeItem.Instance.GetRefCodeItem();
            string refCodeName = ConvertRefCodeToString(refCode);
            if (refCodeItemCollection == null || refCodeItemCollection.Count < 1)
                return true;
            RefCodeItemDTOCollection refCodeItemCollectionByCode = refCodeItemCollection.GetRefCodeItemByRefCode(refCodeName);
            if (refCodeItemCollectionByCode == null || refCodeItemCollectionByCode.Count < 1)
                return true;
            foreach (RefCodeItemDTO items in refCodeItemCollectionByCode)
            {
                if (value == null || value == string.Empty || (value == items.Code && refCodeName.ToUpper() == items.RefCodeSetName.ToUpper() ))
                    return true;
            }  
            return false;
        }

        private string ConvertRefCodeToString(ReferenceCode refCode)
        {
            string[] refCodeName = new string[42] 
            { "Activity code",
              "Agency code",
              "Agency Payable - Status code",
              "Billing delivery method code",
              "Budget category code",
              "Call Center code",
              "Case follow up outcome code",
              "Case followup source code",
              "Case resource code",
              "Case status code",
              "Counseling durarion code",
              "Counseling summary format code",
              "Credit burreau code",
              "Default reason code",
              "Education code",
              "Export format code",              
              "Gender code",
              "Household code",
              "HUD outcome code",
              "HUD termination reason code",
              "Income earners code",
              "Invoice - Status code",
              "Invoice code",
              "Language code",
              "Loan 1st 2nd",
              "Loan delinquency status code",
              "Marital status code",
              "Military service code",
              "Mortgage type code",
              "Never bill reason code",
              "Never pay reason code",
              "Occupation code",
              "Payment code",
              "Program - Service code",
              "Property code",
              "Race code",
              "Secure delivery method code",
              "State",
              "Summary sent other code",
              "System activity code",
              "Term length code",
              "Final Disposition Code"
            };

            return refCodeName[(int)refCode];
        }
    }
}
