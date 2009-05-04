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


        public ServicerDTO GetServicer(int servicerId)
        {
            ServicerDTO servicer = null;
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_servicer_detail_get", dbConnection);
            //<Parameter>            
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_servicer_id", servicerId);
            //</Parameter>   
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    servicer = new ServicerDTO();
                    while (reader.Read())
                    {                     
                        servicer.ServicerID = ConvertToInt(reader["servicer_id"]);
                        servicer.ServicerName = ConvertToString(reader["servicer_name"]);
                        servicer.ContactFName = ConvertToString(reader["contact_fname"]);
                        servicer.ContactLName = ConvertToString(reader["contact_lname"]);
                        servicer.ContactEmail = ConvertToString(reader["contact_email"]);
                        servicer.Phone = ConvertToString(reader["phone"]);
                        servicer.Fax = ConvertToString(reader["fax"]);
                        servicer.ActiveInd = ConvertToString(reader["active_ind"]);
                        servicer.FundingAgreementInd = ConvertToString(reader["funding_agreement_ind"]);
                        servicer.SecureDeliveryMethodCd = ConvertToString(reader["secure_delivery_method_cd"]);
                        servicer.CouselingSumFormatCd = ConvertToString(reader["couseling_sum_format_cd"]);
                        //item.HudServicerNum = ConvertToString(reader["hud_servicer_num"]);
                        servicer.SPFolderName = ConvertToString(reader["sharepoint_foldername"]);                                                
                    }
                    reader.Close();
                }                
            }
            catch (Exception Ex)
            {
                dbConnection.Close();
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally 
            {
                dbConnection.Close();
            }
            return servicer;
        }
    }
}
