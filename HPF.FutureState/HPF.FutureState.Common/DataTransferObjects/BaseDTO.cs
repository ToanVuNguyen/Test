using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using HPF.FutureState.Common.Utils.DataValidator;
namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BaseDTO
    {
        [XmlIgnore]
        public DateTime? CreateDate { get; set; }

        [XmlIgnore]
        public string CreateUserId { get; set; }

        [XmlIgnore]
        public string CreateAppName { get; set; }

        [XmlIgnore]
        public DateTime? ChangeLastDate { get; set; }

        [XmlIgnore]
        public string ChangeLastUserId { get; set; }

        [XmlIgnore]
        public string ChangeLastAppName { get; set; }

        /// <summary>
        /// Set Insert tracking information
        /// </summary>
        /// <param name="userId"></param>  
        
        public void SetInsertTrackingInformation(string userId)
        {
            CreateUserId = userId;
            CreateDate = DateTime.Now;
            CreateAppName = HPFConfigurationSettings.HPF_APPLICATION_NAME;
            ChangeLastDate = DateTime.Now;
            ChangeLastUserId = userId;
            ChangeLastAppName = HPFConfigurationSettings.HPF_APPLICATION_NAME;
        }

        /// <summary>
        /// Set update tracking information
        /// </summary>
        /// <param name="userId"></param> 
        
        public void SetUpdateTrackingInformation(string userId)
        {
            ChangeLastDate = DateTime.Now;
            ChangeLastUserId = userId;
            ChangeLastAppName = HPFConfigurationSettings.HPF_APPLICATION_NAME;
        }

        private static string GetApplicationName()
        {
            return HPFConfigurationSettings.HPF_APPLICATION_NAME;
        }        
    }
}
