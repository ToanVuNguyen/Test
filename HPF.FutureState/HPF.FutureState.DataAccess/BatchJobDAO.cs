using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

using HPF.FutureState.Common;
using HPF.FutureState.Common.DataTransferObjects;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class BatchJobDAO : BaseDAO
    {
        private static readonly BatchJobDAO instance = new BatchJobDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static BatchJobDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected BatchJobDAO()
        {
        }

        public BatchJobDTOCollection ReadBatchJobs()
        {
            BatchJobDTOCollection batchJobs = new BatchJobDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_batch_job_get", dbConnection);
            
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BatchJobDTO job = new BatchJobDTO();
                        job.JobDescription = ConvertToString(reader["job_description"]);
                        job.BatchJobId = ConvertToInt(reader["batch_job_id"]).Value;
                        job.JobFrequency = (JobFrequency)Enum.Parse(typeof(JobFrequency), reader["job_frequency"].ToString());
                        job.LastJobEndDate = ConvertToDateTime(reader["last_job_end_dt"]).Value;
                        job.JobName = ConvertToString(reader["job_name"]);
                        job.JobStartDate = ConvertToDateTime(reader["job_start_dt"]).Value;
                        job.OutputDestination = ConvertToString(reader["output_destination"]);
                        job.OutputFormat = ConvertToString(reader["output_format"]);
                        job.RequestorId = ConvertToInt(reader["requestor_id"]).Value;
                        job.RequestorType = (RequestorType)Enum.Parse(typeof(RequestorType), reader["requestor_type"].ToString());
                        batchJobs.Add(job);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return batchJobs;
        }

        public int[] SearchDailySummaryForeclosureCase(BatchJobDTO batchJob)
        {
            ArrayList fcIds = new ArrayList();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_foreclosure_case_daily_summary_report_search", dbConnection);
            var sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@pi_start_dt", batchJob.LastJobEndDate);
            sqlParam[1] = new SqlParameter("@pi_end_dt", batchJob.LastJobEndDate.AddDays((int)batchJob.JobFrequency));
            sqlParam[2] = new SqlParameter("@pi_servicer_id", batchJob.RequestorId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {                    
                    while (reader.Read())
                    {
                        fcIds.Add(ConvertToInt(reader["fc_id"]).Value);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }
            return (int[])fcIds.ToArray(typeof(int));
        }

        public string GenerateFannieMaeWeeklyXML(BatchJobDTO batchJob)
        {
            XmlDocument doc = new XmlDocument();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_Fannie_Mae_weekly_report_XML", dbConnection);
            var sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@pi_start_dt", batchJob.LastJobEndDate);
            sqlParam[1] = new SqlParameter("@pi_end_dt", batchJob.LastJobEndDate.AddDays((int)batchJob.JobFrequency));
            //sqlParam[2] = new SqlParameter("@pi_funding_source_id", batchJob.RequestorId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteXmlReader();                
                doc.Load(reader);                                   
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return doc.InnerXml;
        }

        public string GenerateForclosureCaseXML(int fcId)
        {
            XmlDocument doc = new XmlDocument();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_CounselingSummary_get_FC_detail_XML", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);            
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteXmlReader();
                doc.Load(reader);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return doc.InnerXml;
        }

        public string GenerateCaseLoanXML(int fcId)
        {
            XmlDocument doc = new XmlDocument();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_CounselingSummary_get_FC_CaseLoan_XML", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteXmlReader();
                doc.Load(reader);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return doc.InnerXml;
        }

        public string GenerateBudgetXML(int fcId)
        {
            XmlDocument doc = new XmlDocument();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_CounselingSummary_get_FC_Budget_XML", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteXmlReader();
                doc.Load(reader);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return doc.InnerXml;
        }

        public string GenerateBudgetAssetXML(int fcId)
        {
            XmlDocument doc = new XmlDocument();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_CounselingSummary_get_FC_Budget_asset_XML", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteXmlReader();
                doc.Load(reader);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return doc.InnerXml;
        }

        public string GenerateOutcomeXML(int fcId)
        {
            XmlDocument doc = new XmlDocument();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_CounselingSummary_get_FC_Outcome_XML", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_fc_id", fcId);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteXmlReader();
                doc.Load(reader);
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return doc.InnerXml;
        }

        public void InsertBatchJobLog(BatchJobLogDTO batchJobLog)
        {
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_batch_job_log_insert", dbConnection);
            var sqlParam = new SqlParameter[10];
            sqlParam[0] = new SqlParameter("@pi_batch_job_id", batchJobLog.BatchJobId);
            sqlParam[1] = new SqlParameter("@pi_job_result", batchJobLog.JobResult.ToString());
            sqlParam[2] = new SqlParameter("@pi_record_count", batchJobLog.RecordCount);
            sqlParam[3] = new SqlParameter("@pi_job_notes", batchJobLog.JobNotes);
            sqlParam[4] = new SqlParameter("@pi_create_dt", batchJobLog.CreateDate);
            sqlParam[5] = new SqlParameter("@pi_create_user_id", batchJobLog.CreateUserId);
            sqlParam[6] = new SqlParameter("@pi_create_app_name", batchJobLog.CreateAppName);
            sqlParam[7] = new SqlParameter("@pi_chg_lst_dt", batchJobLog.ChangeLastDate);
            sqlParam[8] = new SqlParameter("@pi_chg_user_id", batchJobLog.ChangeLastUserId);            
            sqlParam[9] = new SqlParameter("@pi_chg_app_name", batchJobLog.ChangeLastAppName);
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();
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

        /// <summary>
        /// Update start job date and last start date job after finish job
        /// </summary>
        /// <param name="batchJob"></param>
        public void UpdateBatchJobStartAndLastRunDates(BatchJobDTO batchJob)
        {
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_batch_job_update", dbConnection);
            var sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@pi_batch_job_id", batchJob.BatchJobId);
            sqlParam[1] = new SqlParameter("@pi_job_start_dt", batchJob.JobStartDate);
            sqlParam[2] = new SqlParameter("@pi_last_job_end_dt", batchJob.LastJobEndDate);
            sqlParam[3] = new SqlParameter("@pi_chg_lst_dt", batchJob.ChangeLastDate);
            sqlParam[4] = new SqlParameter("@pi_chg_user_id", batchJob.ChangeLastUserId);
            sqlParam[5] = new SqlParameter("@pi_chg_app_name", batchJob.ChangeLastAppName);
            
            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                command.ExecuteNonQuery();
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

        public CounselorDTOCollection GenerateCounsorList(BatchJobDTO batchJob)
        {
            CounselorDTOCollection results = new CounselorDTOCollection();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_counselor_generate_list_get", dbConnection);
            var sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@pi_startDt", batchJob.JobStartDate.AddDays(1));            

            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CounselorDTO counselor = new CounselorDTO();
                    counselor.AgencyName = ConvertToString(reader["agency_name"]);
                    counselor.CounselorEmail = ConvertToString(reader["counselor_email"]);
                    counselor.CounselorExt = ConvertToString(reader["counselor_ext"]);
                    counselor.counselorFirstName = ConvertToString(reader["counselor_fname"]);
                    counselor.CounselorLastName = ConvertToString(reader["counselor_lname"]);
                    counselor.CounselorPhone = ConvertToString(reader["counselor_phone"]);
                    results.Add(counselor);
                }
            }
            catch (Exception Ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(Ex);
            }
            finally
            {
                dbConnection.Close();
            }

            return results;
        }

        public void ImportMHAEscalationData(MHAEscalationDTOCollecion mhaEscaltions)
        {
            SqlConnection dbConnection;
            SqlTransaction trans;

            try
            {
                ServicerDTOCollection servicers = ServicerDAO.Instance.GetServicers();
                RefCodeItemDTOCollection refCodes = RefCodeItemDAO.Instance.GetRefCodeItems();
                RefCodeItemDTOCollection mhaEscalationCodes = refCodes.GetRefCodeItemsByRefCode(ReferenceCode.MHA_ESCALATION_CODE);
                RefCodeItemDTOCollection mhaFinalResolutionCodes = refCodes.GetRefCodeItemsByRefCode(ReferenceCode.MHA_FINAL_RESOLUTION_CODE);

                dbConnection = CreateConnection();
                dbConnection.Open();
                trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);

                var Deletecmd = CreateSPCommand("Delete from mha_escalation", dbConnection);
                Deletecmd.CommandType = CommandType.Text;
                Deletecmd.Transaction = trans;
                Deletecmd.ExecuteNonQuery();
                foreach (MHAEscalationDTO mhaEscaltion in mhaEscaltions)
                {
                    var InsertCmd = CreateSPCommand("hpf_mha_escalation_insert", dbConnection);
                    var sqlParam = new SqlParameter[28];
                    ServicerDTO servicer = servicers.GetServicerByName(mhaEscaltion.Servicer);
                    RefCodeItemDTO mhaEscalationCode = mhaEscalationCodes.GetRefCodeItemByCodeDescription(mhaEscaltion.Escalation);
                    RefCodeItemDTO mhaFinalResolutionCode = mhaFinalResolutionCodes.GetRefCodeItemByCodeDescription(mhaEscaltion.FinalResolution);

                    sqlParam[0] = new SqlParameter("@pi_created_dt", mhaEscaltion.CreatedDt);
                    sqlParam[1] = new SqlParameter("@pi_borrower_lname", mhaEscaltion.BorrowerLname);
                    sqlParam[2] = new SqlParameter("@pi_borrower_fname", mhaEscaltion.BorrowerFname);
                    sqlParam[3] = new SqlParameter("@pi_acct_num", mhaEscaltion.AcctNum);
                    sqlParam[4] = new SqlParameter("@pi_servicer", mhaEscaltion.Servicer);
                    sqlParam[5] = new SqlParameter("@pi_servicer_id", (servicer == null)?null:servicer.ServicerID);
                    sqlParam[6] = new SqlParameter("@pi_escalation", mhaEscaltion.Escalation);
                    sqlParam[7] = new SqlParameter("@pi_escalation_cd", (mhaEscalationCode == null)?null:mhaEscalationCode.CodeValue);
                    sqlParam[8] = new SqlParameter("@pi_escalation_team_notes", mhaEscaltion.EscalationTeamNotes);
                    sqlParam[9] = new SqlParameter("@pi_agency_case_num", mhaEscaltion.AgencyCaseNum);
                    sqlParam[10] = new SqlParameter("@pi_fc_id", mhaEscaltion.FcId);
                    sqlParam[11] = new SqlParameter("@pi_gse_lookup", mhaEscaltion.GseLookup);
                    sqlParam[12] = new SqlParameter("@pi_counselor_name", mhaEscaltion.CounselorName);
                    sqlParam[13] = new SqlParameter("@pi_counselor_email", mhaEscaltion.CounselorEmail);
                    sqlParam[14] = new SqlParameter("@pi_counselor_phone", mhaEscaltion.CounselorPhone);
                    sqlParam[15] = new SqlParameter("@pi_escalated_to_hpf", mhaEscaltion.EscalatedToHPF);
                    sqlParam[16] = new SqlParameter("@pi_current_owner_of_issue", mhaEscaltion.CurrentOwnerOfIssue);
                    sqlParam[17] = new SqlParameter("@pi_final_resolution", mhaEscaltion.FinalResolution);
                    sqlParam[18] = new SqlParameter("@pi_final_resolution_dt", mhaEscaltion.FinalResolutionDate);
                    sqlParam[19] = new SqlParameter("@pi_final_resolution_cd", (mhaFinalResolutionCode == null)?null: mhaFinalResolutionCode.CodeValue);
                    sqlParam[20] = new SqlParameter("@pi_final_resolution_notes", mhaEscaltion.FinalResolutionNotes);
                    sqlParam[21] = new SqlParameter("@pi_resolved_by", mhaEscaltion.ResolvedBy);
                    sqlParam[22] = new SqlParameter("@pi_escalated_to_fannie", mhaEscaltion.EscalatedToFannie);
                    sqlParam[23] = new SqlParameter("@pi_escalated_to_freddie", mhaEscaltion.EscalatedToFreddie);
                    sqlParam[24] = new SqlParameter("@pi_hpf_notes", mhaEscaltion.HpfNotes);
                    sqlParam[25] = new SqlParameter("@pi_gse_notes", mhaEscaltion.GseNotes);
                    sqlParam[26] = new SqlParameter("@pi_created_user_id", mhaEscaltion.CreateUserId);
                    sqlParam[27] = new SqlParameter("@pi_created_app_name", mhaEscaltion.CreateAppName);

                    InsertCmd.CommandType = CommandType.StoredProcedure;
                    InsertCmd.Parameters.AddRange(sqlParam);
                    InsertCmd.Transaction = trans;
                    InsertCmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            { 
            }
        }
    }
}
