using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.DataAccess
{
    public class NonProfitReferralDAO: BaseDAO
    {
        private static readonly NonProfitReferralDAO instance = new NonProfitReferralDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static NonProfitReferralDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected NonProfitReferralDAO()
        {
        }

        public NonProfitReferralDTOCollection GetNonProfitReferrals()
        {
            NonProfitReferralDTOCollection result = HPFCacheManager.Instance.GetData<NonProfitReferralDTOCollection>(Constant.HPF_CACHE_NONPROFITREFERRALS);
            if (result == null)
            {
                result = new NonProfitReferralDTOCollection();
                var dbConnection = CreateConnection();
                var command = CreateCommand("hpf_nonprofitreferral_get", dbConnection);
                command.CommandType = CommandType.StoredProcedure;
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {                    
                    while (reader.Read())
                    {
                        NonProfitReferralDTO item = new NonProfitReferralDTO();
                        item.Id = ConvertToString(reader["id"]);
                        item.ReferralOrgState = ConvertToString(reader["referral_org_state"]);
                        item.ReferralOrgName = ConvertToString(reader["referral_org_name"]);
                        item.ReferralOrgZip = ConvertToString(reader["referral_org_zip"]);
                        item.ReferralContactEmail = ConvertToString(reader["referral_contact_email"]);

                        result.Add(item);
                    }
                }
                HPFCacheManager.Instance.Add(Constant.HPF_CACHE_NONPROFITREFERRALS, result);
            }

            return result;
        }
    }
}
