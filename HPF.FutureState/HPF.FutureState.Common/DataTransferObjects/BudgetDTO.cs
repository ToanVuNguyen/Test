using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BudgetDTO : BaseDTO
    {
        [XmlElement(IsNullable = true)]
        public int? BudgetSubcategoryId { get; set; }

        public string BudgetCategoryCode { get; set; }
    }
}
