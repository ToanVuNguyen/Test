using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;


namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class ForeClosureCaseSearchCriteriaDTO
    {
        string _loanNumber = null;       
        public string AgencyCaseNumber 
        { 
            get; 
            set;
            //{ if value.Equals(string.Empty)
            //}
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LoanNumber 
        {
            get { return _loanNumber; }
            set
            {
                _loanNumber = value;
                
            }
        }

        [RegexValidator("^\\d{5}$", Ruleset = "PropertyZipValidationRule")]
        public string PropertyZip { get; set; }

        [RegexValidator("^\\d{4}$", Ruleset = "SSNValidationRule")]
        public string Last4_SSN { get; set; }

    }
}
