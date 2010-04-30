using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data.SqlClient;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common;
using System.Data;
using HPF.FutureState.Common.Utils;

namespace HPF.FutureState.DataAccess
{
    public class CampaignSponsorProgramDAO: BaseDAO
    {
        private static readonly CampaignSponsorProgramDAO instance = new CampaignSponsorProgramDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CampaignSponsorProgramDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected CampaignSponsorProgramDAO()
        {
        }

        public CampaignSponsorProgramDTOCollection GetCampaignSponsorPrograms()
        {
            CampaignSponsorProgramDTOCollection results = HPFCacheManager.Instance.GetData<CampaignSponsorProgramDTOCollection>(Constant.HPF_CACHE_CAMPAIGN_SPONSOR_PROGRAM);            
            if (results == null)
            {
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_campaign_sponsor_program_get", dbConnection);
                
                try
                {                
                    command.CommandType = CommandType.StoredProcedure;
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new CampaignSponsorProgramDTOCollection();
                        while (reader.Read())
                        {
                            CampaignSponsorProgramDTO item = new CampaignSponsorProgramDTO();
                            item.CampaignId = ConvertToInt(reader["campaign_id"]).Value;
                            item.CampaignName = ConvertToString(reader["campaign_name"]);
                            item.CounseledProgramId = ConvertToInt(reader["counseled_program_id"]).Value;
                            item.EffDt = ConvertToDateTime(reader["eff_dt"]);
                            item.ExpDt = ConvertToDateTime(reader["exp_dt"]);
                            item.ProgramId = ConvertToInt(reader["program_id"]).Value;
                            item.SponsorId = ConvertToInt(reader["sponsor_id"]).Value;
                            
                            results.Add(item);
                        }
                    }
                    reader.Close();
                    HPFCacheManager.Instance.Add(Constant.HPF_CACHE_CAMPAIGN_SPONSOR_PROGRAM, results);
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
