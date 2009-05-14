using System;
using System.Data;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using System.Data.SqlClient;

namespace HPF.FutureState.DataAccess
{
    public class ServicerDAO : BaseDAO
    {
        private static readonly ServicerDAO instance = new ServicerDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static ServicerDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected ServicerDAO()
        {
            
        }                       

        public ServicerDTOCollection GetServicers(int? servicerId)
        {
            ServicerDTOCollection results = HPFCacheManager.Instance.GetData<ServicerDTOCollection>(Constant.HPF_CACHE_SERVICER);
            if (results == null || servicerId.HasValue)
            {
                var dbConnection = CreateConnection();
                var command = CreateSPCommand("hpf_servicer_get", dbConnection);
                //<Parameter>         
                if (servicerId.HasValue)
                {
                    var sqlParam = new SqlParameter[1];
                    sqlParam[0] = new SqlParameter("@pi_servicer_id", servicerId);
                    command.Parameters.AddRange(sqlParam);
                }
                try
                {
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new ServicerDTOCollection();
                        while (reader.Read())
                        {
                            ServicerDTO servicer = new ServicerDTO();
                            servicer.ServicerID = ConvertToInt(reader["servicer_id"]);
                            servicer.ServicerName = ConvertToString(reader["servicer_name"]);
                            servicer.ContactFName = ConvertToString(reader["contact_fname"]);
                            servicer.ContactLName = ConvertToString(reader["contact_lname"]);
                            servicer.ContactEmail = ConvertToString(reader["contact_email"]);
                            servicer.Phone = ConvertToString(reader["phone"]);
                            servicer.Fax = ConvertToString(reader["fax"]);
                            servicer.ActiveInd = ConvertToString(reader["active_ind"]);
                            servicer.FundingAgreement = ConvertToString(reader["funding_agreement_ind"]);
                            servicer.SummaryDeliveryMethod = ConvertToString(reader["secure_delivery_method_cd"]);
                            servicer.CouselingSumFormatCd = ConvertToString(reader["couseling_sum_format_cd"]);                            
                            servicer.SPFolderName = ConvertToString(reader["sharepoint_foldername"]);
                            results.Add(servicer);
                        }
                        reader.Close();
                    }
                    HPFCacheManager.Instance.Add(Constant.HPF_CACHE_SERVICER, results);
                }
                catch (Exception ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(ex);
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return results;
        }
    }
}
