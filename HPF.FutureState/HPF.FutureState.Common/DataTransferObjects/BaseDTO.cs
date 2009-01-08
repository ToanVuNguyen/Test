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
        public DateTime CreateDate { get; set; }

        [Microsoft.Practices.EnterpriseLibrary.Validation.Validators.NotNullValidator(MessageTemplate = "Create User ID is required", Ruleset = "RequirePartialValidate")]
        //[RequiredWhenInsertUpdateValidator("insert", true, false, MessageTemplate = "Create User ID is required", Ruleset = "RequirePartialValidate")]
        public string CreateUserId { get; set; }

        [XmlIgnore]
        public string CreateAppName { get; set; }

        [XmlIgnore]
        public DateTime ChangeLastDate { get; set; }

        //[RequiredWhenInsertUpdateValidator("update", false, true, MessageTemplate = "Last change User ID is required", Ruleset = "RequirePartialValidate")]
        [Microsoft.Practices.EnterpriseLibrary.Validation.Validators.NotNullValidator(Tag = "P-WS-SFC-00125", MessageTemplate = "Last change User ID is required", Ruleset = "RequirePartialValidate")]
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
