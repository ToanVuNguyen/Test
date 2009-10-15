using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class ServicerDTOCollection : BaseDTOCollection<ServicerDTO>
    {
        /// <summary>
        /// Get servicer by their delivery method.
        /// </summary>
        /// <param name="deliveryMethod"></param>
        /// <returns></returns>
        public ServicerDTOCollection ExtractServicerByDeliveryMethod(string deliveryMethod)
        {
            var returnValue = new ServicerDTOCollection();
            var result = this.Where(c => c.SummaryDeliveryMethod == deliveryMethod);
            foreach (var servicer in result)
            {
                returnValue.Add(servicer);
            }
            return returnValue;
        }

        public ServicerDTO GetServicerById(int? servicerId)
        {
            return this.SingleOrDefault(i => i.ServicerID == servicerId);
        }

        public ServicerDTO GetServicerByName(string servicerName)
        {
            if (string.IsNullOrEmpty(servicerName))
                return null;
            string sName = servicerName.ToUpper();
            return this.SingleOrDefault(i => i.ServicerName.ToUpper().Equals(sName));
        }
    }
}
