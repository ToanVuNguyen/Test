using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

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
            var sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@pi_batch_job_id", batchJob.BatchJobId);
            sqlParam[1] = new SqlParameter("@pi_job_start_dt", batchJob.JobStartDate);
            sqlParam[2] = new SqlParameter("@pi_last_job_end_dt", batchJob.LastJobEndDate);
            
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
    }
}
