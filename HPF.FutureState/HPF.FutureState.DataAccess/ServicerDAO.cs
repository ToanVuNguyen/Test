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


        public ServicerDTOCollection GetServicersByFcId(int? fcId)
        {
            ServicerDTOCollection servicers = null;
            var dbConnection = CreateConnection();
            var command = CreateCommand("hpf_servicer_get_from_FcId", dbConnection);
            //<Parameter>            
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
            //</Parameter>   
            command.Parameters.AddRange(sqlParam);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    servicers = new ServicerDTOCollection();
                    while (reader.Read())
                    {
                        var item = new ServicerDTO();
                        item.ServicerID = ConvertToInt(reader["servicer_id"]);
                        item.ServicerName = ConvertToString(reader["servicer_name"]);
                        item.ContactFName = ConvertToString(reader["contact_fname"]);
                        item.ContactLName= ConvertToString(reader["contact_lname"]);
                        item.ContactEmail = ConvertToString(reader["contact_email"]);
                        item.Phone = ConvertToString(reader["phone"]);
                        item.Fax = ConvertToString(reader["fax"]);
                        item.ActiveInd = ConvertToString(reader["active_ind"]);
                        item.FundingAgreementInd = ConvertToString(reader["funding_agreement_ind"]);
                        item.SecureDeliveryMethodCd = ConvertToString(reader["secure_delivery_method_cd"]);
                        item.CouselingSumFormatCd = ConvertToString(reader["couseling_sum_format_cd"]);
                        //item.HudServicerNum = ConvertToString(reader["hud_servicer_num"]);
                        item.SPFolderName = ConvertToString(reader["sharepoint_foldername"]);                        
                        servicers.Add(item);
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
            return servicers;
        }
    }
}
