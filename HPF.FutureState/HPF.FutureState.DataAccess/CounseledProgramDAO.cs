using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data.SqlClient;
using HPF.FutureState.Common.Utils.Exceptions;
using HPF.FutureState.Common;
using HPF.FutureState.Common.Utils;
using System.Data;

namespace HPF.FutureState.DataAccess
{
    public class CounseledProgramDAO: BaseDAO
    {
        private static readonly CounseledProgramDAO instance = new CounseledProgramDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CounseledProgramDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected CounseledProgramDAO()
        {
        }

        public CounseledProgramDTOCollection GetCounceledPrograms()
        {
            CounseledProgramDTOCollection results = HPFCacheManager.Instance.GetData<CounseledProgramDTOCollection>(Constant.HPF_CACHE_COUNCELED_PROGRAM);            
            if (results == null)
            {
                var dbConnection = CreateConnection();
                var command = new SqlCommand("hpf_counseled_program_get", dbConnection);
                
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    dbConnection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        results = new CounseledProgramDTOCollection();
                        while (reader.Read())
                        {
                            CounseledProgramDTO item = new CounseledProgramDTO();
                            item.CounseledProgramId = ConvertToInt(reader["counseled_program_id"]).Value;
                            item.CounseledProgramName = ConvertToString(reader["counseled_program_name"]);
                            item.CounseledProgramComment = ConvertToString(reader["counseled_program_comment"]);
                            item.ProgramId = ConvertToInt(reader["program_id"]);
                            item.StartDt = ConvertToDateTime(reader["start_dt"]);
                            item.EndDt = ConvertToDateTime(reader["end_dt"]);

                            results.Add(item);
                        }
                    }
                    reader.Close();
                    HPFCacheManager.Instance.Add(Constant.HPF_CACHE_COUNCELED_PROGRAM, results);
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
