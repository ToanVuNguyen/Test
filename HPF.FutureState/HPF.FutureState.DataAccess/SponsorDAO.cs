using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data.SqlClient;
using System.Data;
using HPF.FutureState.Common.Utils;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common;

namespace HPF.FutureState.DataAccess
{
    public class SponsorDAO: BaseDAO
    {
        private static readonly SponsorDAO instance = new SponsorDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static SponsorDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected SponsorDAO()
        {
        }

        public SponsorDTOCollection GetSponsors()
        {
            SponsorDTOCollection results = HPFCacheManager.Instance.GetData<SponsorDTOCollection>(Constant.HPF_CACHE_SPONSOR);            
            if (results == null)
            {
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_sponsor_get", dbConnection);
                
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new SponsorDTOCollection();
                        while (reader.Read())
                        {
                            SponsorDTO item = new SponsorDTO();
                            item.SponsorId = ConvertToInt(reader["sponsor_id"]).Value;
                            item.SponsorName = ConvertToString(reader["sponsor_name"]);
                            item.SponsorComment = ConvertToString(reader["sponsor_comment"]);
                            item.EffDt = ConvertToDateTime(reader["eff_dt"]);
                            item.ExpDt = ConvertToDateTime(reader["exp_dt"]);

                            results.Add(item);
                        }
                    }
                    reader.Close();
                    HPFCacheManager.Instance.Add(Constant.HPF_CACHE_SPONSOR, results);
                }
                catch (Exception Ex)
                {
                    throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
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
