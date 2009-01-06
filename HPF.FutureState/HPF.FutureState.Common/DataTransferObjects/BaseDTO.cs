using System;
using System.Collections.Generic;
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

        [XmlIgnore]
        public string CreateUserId { get; set; }

        [XmlIgnore]
        public string CreateAppName { get; set; }

        [XmlIgnore]
        public DateTime ChangeLastDate { get; set; }

        [XmlIgnore]
        public string ChangeLastUserId { get; set; }

        [XmlIgnore]
        public string ChangeLastAppName { get; set; }

        /// <summary>
        /// Set Insert tracking information
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appName"></param>
        public void SetInsertTrackingInformation(string userId, string appName)
        {
            CreateUserId = userId;
            CreateDate = DateTime.Today;
            CreateAppName = appName;
            ChangeLastDate = DateTime.Today;
            ChangeLastUserId = userId;
            ChangeLastAppName = appName;
        }

        /// <summary>
        /// Set update tracking information
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appName"></param>
        public void SetUpdateTrackingInformation(string userId, string appName)
        {
            ChangeLastDate = DateTime.Today;
            ChangeLastUserId = userId;
            ChangeLastAppName = appName;
        }
    }
}
