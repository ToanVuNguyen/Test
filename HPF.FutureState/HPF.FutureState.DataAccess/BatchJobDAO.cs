﻿using System;
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
using HPF.FutureState.Common.Utils;
using System.Collections.ObjectModel;

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
            SqlConnection dbConnection=null;
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
                Deletecmd.Dispose();
                foreach (MHAEscalationDTO mhaEscaltion in mhaEscaltions)
                {
                    var InsertCmd = CreateSPCommand("hpf_mha_escalation_insert", dbConnection);
                    var sqlParam = new SqlParameter[47];
                    ServicerDTO servicer = servicers.GetServicerByName(mhaEscaltion.Servicer);
                    RefCodeItemDTO mhaEscalationCode = mhaEscalationCodes.GetRefCodeItemByCodeDescription(mhaEscaltion.Escalation);
                    RefCodeItemDTO mhaFinalResolutionCode = mhaFinalResolutionCodes.GetRefCodeItemByCodeDescription(mhaEscaltion.FinalResolution);

                    sqlParam[0] = new SqlParameter("@pi_item_created_dt", mhaEscaltion.ItemCreatedDt);
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
                    sqlParam[15] = new SqlParameter("@pi_escalated_to_mmi_mgmt", mhaEscaltion.EscalatedToMMIMgmt);
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
                    sqlParam[26] = new SqlParameter("@pi_address", mhaEscaltion.Address);
                    sqlParam[27] = new SqlParameter("@pi_city", mhaEscaltion.City);
                    sqlParam[28] = new SqlParameter("@pi_state", mhaEscaltion.State);
                    sqlParam[29] = new SqlParameter("@pi_zip", mhaEscaltion.Zip);
                    sqlParam[30] = new SqlParameter("@pi_created_user_id", mhaEscaltion.CreateUserId);
                    sqlParam[31] = new SqlParameter("@pi_created_app_name", mhaEscaltion.CreateAppName);

                    sqlParam[32] = new SqlParameter("@pi_best_time_to_reach", mhaEscaltion.BestTime);
                    sqlParam[33] = new SqlParameter("@pi_best_number_to_call", mhaEscaltion.BestNumber);
                    sqlParam[34] = new SqlParameter("@pi_borrower_email", mhaEscaltion.BorrowerEmail);
                    sqlParam[35] = new SqlParameter("@pi_handle_time_hrs", mhaEscaltion.HandleTimeHrs);
                    sqlParam[36] = new SqlParameter("@pi_handle_time_mins", mhaEscaltion.HandleTimeMins);
                    sqlParam[37] = new SqlParameter("@pi_escalated_to_gse_dt", mhaEscaltion.EscalatedToGseDt);
                    sqlParam[38] = new SqlParameter("@pi_escalated_to_mmi_mgmt_dt", mhaEscaltion.EscalatedToMMIMgmtDt);
                    sqlParam[39] = new SqlParameter("@pi_gse_notes_completed_dt", mhaEscaltion.GSENotesCompletedDt);
                    
                    sqlParam[40] = new SqlParameter("@pi_item_created_user", mhaEscaltion.ItemCreatedUser);
                    sqlParam[41] = new SqlParameter("@pi_item_modified_dt", mhaEscaltion.ItemModifiedDt);
                    sqlParam[42] = new SqlParameter("@pi_item_modified_user", mhaEscaltion.ItemModifiedUser);

                    sqlParam[43] = new SqlParameter("@pi_commitment_ind", mhaEscaltion.CommitmentInd);
                    sqlParam[44] = new SqlParameter("@pi_followup_dt", mhaEscaltion.FollowupDt);
                    sqlParam[45] = new SqlParameter("@pi_commitment_closed_dt", mhaEscaltion.CommitmentClosedDt);
                    sqlParam[46] = new SqlParameter("@pi_escalated_to_hsc_ind", mhaEscaltion.EscalatedToHscInd);                    

                    InsertCmd.CommandType = CommandType.StoredProcedure;
                    InsertCmd.Parameters.AddRange(sqlParam);
                    InsertCmd.Transaction = trans;
                    InsertCmd.ExecuteNonQuery();
                    InsertCmd.Dispose();
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }
            finally
            {
                if (dbConnection != null) dbConnection.Close();
            }
        }

        public void ImportMHAHelp(MHAHelpDTOCollection mhaHelps)
        {
            SqlConnection dbConnection=null;
            SqlTransaction trans;

            try
            {
                ServicerDTOCollection servicers = ServicerDAO.Instance.GetServicers();                

                dbConnection = CreateConnection();
                dbConnection.Open();
                trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);

                foreach (MHAHelpDTO mhaHelp in mhaHelps)
                {
                    var InsertCmd = CreateSPCommand("hpf_mha_help_insert", dbConnection);
                    var sqlParam = new SqlParameter[36];
                    ServicerDTO servicer = servicers.GetServicerByName(mhaHelp.Servicer);

                    sqlParam[0] = new SqlParameter("@pi_borrower_fname", mhaHelp.BorrowerFName);
                    sqlParam[1] = new SqlParameter("@pi_borrower_lname", mhaHelp.BorrowerLName);
                    sqlParam[2] = new SqlParameter("@pi_servicer", mhaHelp.Servicer);
                    sqlParam[3] = new SqlParameter("@pi_servicer_id", (servicer == null) ? null : servicer.ServicerID);
                    sqlParam[4] = new SqlParameter("@pi_acct_num", mhaHelp.AcctNum);
                    sqlParam[5] = new SqlParameter("@pi_counselor_name", mhaHelp.CounselorName);
                    sqlParam[6] = new SqlParameter("@pi_counselor_email", mhaHelp.CounselorEmail);
                    sqlParam[7] = new SqlParameter("@pi_call_src", mhaHelp.CallSource);
                    sqlParam[8] = new SqlParameter("@pi_voicemail_dt", mhaHelp.VoicemailDt);
                    sqlParam[9] = new SqlParameter("@pi_mha_help_reason", mhaHelp.MHAHelpReason);
                    sqlParam[10] = new SqlParameter("@pi_comments", mhaHelp.Comments);
                    sqlParam[11] = new SqlParameter("@pi_privacy_consent", mhaHelp.PrivacyConsent);
                    sqlParam[12] = new SqlParameter("@pi_borrower_in_trial_mod", mhaHelp.BorrowerInTrialMod);
                    sqlParam[13] = new SqlParameter("@pi_trial_mod_before_sept1", mhaHelp.TrialModStartedBeforeStept1);
                    sqlParam[14] = new SqlParameter("@pi_current_on_payments", mhaHelp.CurrentOnPayments);
                    sqlParam[15] = new SqlParameter("@pi_wage_earner", mhaHelp.WageEarner);
                    sqlParam[16] = new SqlParameter("@pi_two_paystubs_sent", mhaHelp.IfWageEarnerWereTwoPayStubsSentIn);
                    sqlParam[17] = new SqlParameter("@pi_all_docs_submitted", mhaHelp.AllDocumentsSubmitted);
                    sqlParam[18] = new SqlParameter("@pi_list_of_docs_submitted", mhaHelp.DocumentsSubmitted);
                    sqlParam[19] = new SqlParameter("@pi_borrower_phone", mhaHelp.BorrowerPhone);
                    sqlParam[20] = new SqlParameter("@pi_best_time_to_reach", mhaHelp.BestTimeToReach);
                    sqlParam[21] = new SqlParameter("@pi_borrower_email", mhaHelp.BorrowerEmail);
                    sqlParam[22] = new SqlParameter("@pi_address", mhaHelp.Address);
                    sqlParam[23] = new SqlParameter("@pi_city", mhaHelp.City);
                    sqlParam[24] = new SqlParameter("@pi_state", mhaHelp.State);
                    sqlParam[25] = new SqlParameter("@pi_zip", mhaHelp.Zip);
                    sqlParam[26] = new SqlParameter("@pi_mha_help_resolution", mhaHelp.MHAHelpResolution);
                    sqlParam[27] = new SqlParameter("@pi_item_created_dt", mhaHelp.ItemCreatedDt);
                    sqlParam[28] = new SqlParameter("@pi_item_created_user", mhaHelp.ItemCreatedUser);
                    sqlParam[29] = new SqlParameter("@pi_item_modified_dt", mhaHelp.ItemModifiedDt);
                    sqlParam[30] = new SqlParameter("@pi_item_modified_user", mhaHelp.ItemModifiedUser);
                    sqlParam[31] = new SqlParameter("@pi_item_id", mhaHelp.ItemId);

                    sqlParam[32] = new SqlParameter("@pi_created_user_id", mhaHelp.CreateUserId);
                    sqlParam[33] = new SqlParameter("@pi_created_app_name", mhaHelp.CreateAppName);

                    sqlParam[34] = new SqlParameter("@pi_handle_time_hrs", mhaHelp.HandleTimeHrs);
                    sqlParam[35] = new SqlParameter("@pi_handle_time_mins", mhaHelp.HandleTimeMins);

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
                if (dbConnection != null) dbConnection.Close();
            }
        }

        public void ImportCounselingSummaryAuditLog(CounselingSummaryAuditLogDTOCollection auditLogs)
        {
            var dbConnection = CreateConnection();            
            
            try
            {
                dbConnection.Open();
                var command = CreateSPCommand("hpf_counseling_summary_audit_log_insert", dbConnection);
                var sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@pi_archive_name", null);
                sqlParam[1] = new SqlParameter("@pi_counseling_summary_name", null);
                sqlParam[2] = new SqlParameter("@pi_occurred_dt", null);
                sqlParam[3] = new SqlParameter("@pi_servicer", null);
                sqlParam[4] = new SqlParameter("@pi_loan_number", null);
                sqlParam[5] = new SqlParameter("@pi_completed_dt", null);
                sqlParam[6] = new SqlParameter("@pi_item_created_dt", null);
                sqlParam[7] = new SqlParameter("@pi_user_id", null);
                sqlParam[8] = new SqlParameter("@pi_create_user_id", null);
                sqlParam[9] = new SqlParameter("@pi_create_app_name", null);
                command.Parameters.AddRange(sqlParam);

                foreach (CounselingSummaryAuditLogDTO audit in auditLogs)
                {
                    sqlParam[0].Value = audit.ArchiveName;
                    sqlParam[1].Value = audit.CounselingSummaryFile;
                    sqlParam[2].Value = audit.OccurredDate;
                    sqlParam[3].Value = audit.Servicer;
                    sqlParam[4].Value = audit.LoanNumber;
                    sqlParam[5].Value = audit.CompletedDate;
                    sqlParam[6].Value = audit.ItemCreatedDate;
                    sqlParam[7].Value = audit.UserId;
                    sqlParam[8].Value = audit.CreateUserId;
                    sqlParam[9].Value = audit.CreateAppName;
                    command.ExecuteNonQuery();
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
        }

        public Collection<ExcelDataRow> GetCompletedCounselingDetailReportData(string filename, DateTime fromDate, DateTime toDate)
        {            
            Collection<ExcelDataRow> results = new Collection<ExcelDataRow>();
            var dbConnection = CreateConnection();
            var command = CreateSPCommand("hpf_rpt_CompletedCounselingDetail", dbConnection);
            var sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@pi_agency_id", -1);
            sqlParam[1] = new SqlParameter("@pi_program_id", -1);
            sqlParam[2] = new SqlParameter("@pi_from_dt", fromDate);
            sqlParam[3] = new SqlParameter("@pi_to_dt", toDate);

            command.Parameters.AddRange(sqlParam);
            try
            {
                dbConnection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {                    
                    ExcelDataRow item = new ExcelDataRow();
                    item.Columns = new Collection<string>();
                    item.Columns.Add(ConvertToString(reader["fc_id"]));
                    item.Columns.Add(ConvertToString(reader["agency_name"]));
                    item.Columns.Add(ConvertToString(reader["completed_dt"]));
                    item.Columns.Add(ConvertToString(reader["agency_case_num"]));
                    item.Columns.Add(ConvertToString(reader["agency_client_num"]));                    
                    item.Columns.Add(ConvertToString(reader["counselor_fname"]));
                    item.Columns.Add(ConvertToString(reader["counselor_lname"]));
                    item.Columns.Add(ConvertToString(reader["program_name"])); 
                    item.Columns.Add(ConvertToString(reader["intake_dt"]));
                    item.Columns.Add(ConvertToString(reader["call_id"]));  
                    item.Columns.Add(ConvertToString(reader["case_source_cd"]));
                    item.Columns.Add(ConvertToString(reader["counseling_duration_cd"]));
                    item.Columns.Add(ConvertToString(reader["dflt_reason_1st_cd"]));
                    item.Columns.Add(ConvertToString(reader["dflt_reason_2nd_cd"]));
                    item.Columns.Add(ConvertToString(reader["borrower_fname"]));
                    item.Columns.Add(ConvertToString(reader["borrower_lname"]));
                    item.Columns.Add(ConvertToString(reader["mother_maiden_lname"]));
                    item.Columns.Add(ConvertToString(reader["borrower_last4_ssn"]));
                    item.Columns.Add(ConvertToString(reader["borrower_dob"]));
                    item.Columns.Add(ConvertToString(reader["gender_cd"]));
                    item.Columns.Add(ConvertToString(reader["race_cd"]));
                    item.Columns.Add(ConvertToString(reader["hispanic_ind"]));
                    item.Columns.Add(ConvertToString(reader["borrower_marital_status_cd"]));
                    item.Columns.Add(ConvertToString(reader["borrower_educ_level_completed_cd"]));                    
                    item.Columns.Add(ConvertToString(reader["military_service_cd"]));                                                                                                    
                    item.Columns.Add(ConvertToString(reader["borrower_disabled_ind"]));
                    item.Columns.Add(ConvertToString(reader["primary_contact_no"]));
                    item.Columns.Add(ConvertToString(reader["second_contact_no"]));
                    item.Columns.Add(ConvertToString(reader["email_1"]));                    
                    item.Columns.Add(ConvertToString(reader["co_borrower_fname"]));
                    item.Columns.Add(ConvertToString(reader["co_borrower_lname"]));
                    item.Columns.Add(ConvertToString(reader["co_borrower_last4_ssn"]));
                    item.Columns.Add(ConvertToString(reader["co_borrower_dob"]));
                    item.Columns.Add(ConvertToString(reader["household_cd"])); 
                    item.Columns.Add(ConvertToString(reader["occupant_num"]));
                    item.Columns.Add(ConvertToString(reader["owner_occupied_ind"]));
                    item.Columns.Add(ConvertToString(reader["income_earners_cd"]));                    
                    item.Columns.Add(ConvertToString(reader["household_gross_annual_income_amt"]));
                    item.Columns.Add(ConvertToString(reader["intake_credit_score"]));
                    item.Columns.Add(ConvertToString(reader["prop_addr1"]));
                    item.Columns.Add(ConvertToString(reader["prop_addr2"]));
                    item.Columns.Add(ConvertToString(reader["prop_city"]));
                    item.Columns.Add(ConvertToString(reader["prop_state_cd"]));
                    item.Columns.Add(ConvertToString(reader["prop_zip"]));
                    item.Columns.Add(ConvertToString(reader["servicer_consent_ind"]));
                    item.Columns.Add(ConvertToString(reader["funding_consent_ind"]));
                    item.Columns.Add(ConvertToString(reader["summary_sent_other_cd"]));
                    item.Columns.Add(ConvertToString(reader["summary_sent_other_dt"]));
                    item.Columns.Add(ConvertToString(reader["summary_sent_dt"]));
                    item.Columns.Add(ConvertToString(reader["servicer_name"]));
                    item.Columns.Add(ConvertToString(reader["acct_num"]));
                    item.Columns.Add(ConvertToString(reader["mortgage_type_cd"]));
                    item.Columns.Add(ConvertToString(reader["arm_reset_ind"]));
                    item.Columns.Add(ConvertToString(reader["interest_rate"]));
                    item.Columns.Add(ConvertToString(reader["term_length_cd"]));
                    item.Columns.Add(ConvertToString(reader["current_loan_balance_amt"]));
                    item.Columns.Add(ConvertToString(reader["loan_delinq_status_cd"]));
                    item.Columns.Add(ConvertToString(reader["fc_notice_received_ind"]));
                    item.Columns.Add(ConvertToString(reader["hud_termination_reason_cd"]));
                    item.Columns.Add(ConvertToString(reader["hud_termination_dt"]));
                    item.Columns.Add(ConvertToString(reader["hud_outcome_cd"]));
                    item.Columns.Add(ConvertToString(reader["outcome_1"]));
                    item.Columns.Add(ConvertToString(reader["outcome_2"]));
                    item.Columns.Add(ConvertToString(reader["outcome_3"]));                                        
                    results.Add(item);

                    //if (results.Count >= ExcelFileWriter.PAGE_ROW_COUNT)
                    //{
                    //    count += results.Count;
                    //    //Console.WriteLine("Writing data into sheet " + sheetName + sheetCount+ "...");
                    //    ExcelFileWriter.PutToExcel(filename, sheetName + (sheetCount++), dataHeaders, results);
                    //    //Console.WriteLine("Compled Writing data into sheet. Continue reading data...");
                    //    results.Clear();                        
                    //}
                }

                //if (results.Count >=0)
                //{
                //    count += results.Count;
                //    //Console.WriteLine("Writing data into sheet " + sheetName + sheetCount + "...");
                //    ExcelFileWriter.PutToExcel(filename, sheetName + (sheetCount++), dataHeaders, results);
                //    //Console.WriteLine("Compled Writing data into sheet");
                //    results.Clear();
                //}                
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
    }
}
