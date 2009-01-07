using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HPF.FutureState.Common.DataTransferObjects
{
    [Serializable]
    public class BaseDTO
    {
        [XmlIgnore]
        public DateTime CreateDate { get; set; }
        
        public string CreateUserId { get; set; }

        [XmlIgnore]
        public string CreateAppName { get; set; }

        [XmlIgnore]
        public DateTime ChangeLastDate { get; set; }
        
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
            CreateDate = DateTime.Today;
            CreateAppName = GetApplicationName();
            ChangeLastDate = DateTime.Today;
            ChangeLastUserId = userId;
            ChangeLastAppName = GetApplicationName();
        }

        /// <summary>
        /// Set update tracking information
        /// </summary>
        /// <param name="userId"></param>        
        public void SetUpdateTrackingInformation(string userId)
        {
            ChangeLastDate = DateTime.Today;
            ChangeLastUserId = userId;
            ChangeLastAppName = GetApplicationName();
        }

        private static string GetApplicationName()
        {
            return ConfigurationManager.AppSettings["HPFApplicationName"];
        }
    }
}
