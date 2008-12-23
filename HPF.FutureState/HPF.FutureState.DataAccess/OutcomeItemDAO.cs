using System;
using System.Data;
using System.Data.SqlClient;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.DataTransferObjects.WebServices;

namespace HPF.FutureState.DataAccess
{
    public class OutcomeItemDAO : BaseDAO
    {
        private static readonly OutcomeItemDAO instance = new OutcomeItemDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static OutcomeItemDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected OutcomeItemDAO()
        {
            
        }

        /// <summary>
        /// Select all OutcomeItem from database by Fc_ID. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>OutcomeItemDTOCollection</returns>
        public OutcomeItemDTOCollection GetOutcomeItemCollection(int fcId)
        {            
            OutcomeItemDTOCollection results = HPFCacheManager.Instance.GetData<OutcomeItemDTOCollection>("outcomeItem");
            if (results == null)
            {
                var dbConnection = new SqlConnection(ConnectionString);
                var command = new SqlCommand("hpf_outcome_item_get", dbConnection);
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
                        results = new OutcomeItemDTOCollection();
                        while (reader.Read())
                        {
                            OutcomeItemDTO item = new OutcomeItemDTO();
                            item.OutcomeItemId = ConvertToInt(reader["outcome_item_id"]);
                            item.FcId = ConvertToInt(reader["fc_id"]);
                            item.OutcomeTypeId = ConvertToInt(reader["outcome_type_id"]);
                            item.OutcomeDt = ConvertToDateTime(reader["outcome_dt"]);
                            item.OutcomeDeletedDt = ConvertToDateTime(reader["outcome_deleted_dt"]);
                            item.NonprofitreferralKeyNum = ConvertToString(reader["nonprofitreferral_key_num"]);
                            item.ExtRefOtherName = ConvertToString(reader["ext_ref_other_name"]);
                            results.Add(item);
                        }
                        reader.Close();
                    }
                    dbConnection.Close();
                    HPFCacheManager.Instance.Add("outcomeItem", results);
                }
                catch (Exception Ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
                }
            }
            return results;
        }   
    }
}
