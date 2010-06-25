using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using System.Data;
using System.Data.SqlClient;
using HPF.FutureState.Common.Utils.Exceptions;

namespace HPF.FutureState.DataAccess
{
    public class CallingDataDAO: BaseDAO
    {        
        protected CallingDataDAO()
        { 
        }

        public SqlConnection dbConnection;

        public SqlTransaction trans;

        public static CallingDataDAO CreateInstance()
        {
            return new CallingDataDAO();
        }

        /// <summary>
        /// Begin working
        /// </summary>
        public void Begin()
        {
            dbConnection = CreateConnection();
            dbConnection.Open();
            trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Commit work.
        /// </summary>
        public void Commit()
        {
            try
            {
                trans.Commit();
                dbConnection.Close();
            }
            catch (Exception)
            {
                
            }
        }
        /// <summary>
        /// Cancel work
        /// </summary>
        public void Cancel()
        {
            try
            {
                trans.Rollback();
                dbConnection.Close();
            }
            catch (Exception)
            {
                
            }
        }

        public int InsertCallingHeader(CallingDataHeaderDTO header)
        {             
            var command = CreateSPCommand("hpf_calling_data_header_insert", dbConnection);
            var sqlParam = new SqlParameter[17];
            try
            {

                sqlParam[0] = new SqlParameter("@pi_file_length", header.FileLength);
                sqlParam[1] = new SqlParameter("@pi_subscriber_ID", header.SubscriberId);
                sqlParam[2] = new SqlParameter("@pi_subaccount_name", header.SubaccountName);
                sqlParam[3] = new SqlParameter("@pi_login_id", header.LoginId);
                sqlParam[4] = new SqlParameter("@pi_num_assigned_service_type", header.NumAssignedServiceType);
                sqlParam[5] = new SqlParameter("@pi_service_type", header.ServiceType);
                sqlParam[6] = new SqlParameter("@pi_reserved", header.Reserved);
                sqlParam[7] = new SqlParameter("@pi_file_creation_dt", header.FileCreationDt);
                sqlParam[8] = new SqlParameter("@pi_start_dt", header.StartDt);
                sqlParam[9] = new SqlParameter("@pi_end_dt", header.EndDt);
                sqlParam[10] = new SqlParameter("@pi_call_record_count", header.CallRecordCount);
                sqlParam[11] = new SqlParameter("@pi_customer_provided_header", header.CustomerProvidedHeader);
                sqlParam[12] = new SqlParameter("@pi_download_type", header.DownloadType);
                sqlParam[13] = new SqlParameter("@pi_report_form", header.ReportForm);
                sqlParam[14] = new SqlParameter("@pi_create_user_id", header.CreateUserId);
                sqlParam[15] = new SqlParameter("@pi_create_app_name", header.CreateAppName);
                sqlParam[16] = new SqlParameter("@po_calling_data_header_id", SqlDbType.Int) { Direction = ParameterDirection.Output };

                command.Parameters.AddRange(sqlParam);
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
                return ConvertToInt(sqlParam[16].Value).Value;
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }            
            
        }

        public void InsertCallingDetail(CallingDataDetailDTO detail)
        {            
            var command = CreateSPCommand("hpf_calling_data_detail_insert", dbConnection);
            var sqlParam = new SqlParameter[97];
            try
            {

                sqlParam[0] = new SqlParameter("@pi_calling_data_header_id", detail.CallingHeaderId);
                sqlParam[1] = new SqlParameter("@pi_record_length", detail.RecordLength);
                sqlParam[2] = new SqlParameter("@pi_structure_code", detail.StructureCd);
                sqlParam[3] = new SqlParameter("@pi_call_code", detail.CallCd);
                sqlParam[4] = new SqlParameter("@pi_incoming_switch_ID", detail.IncomeSwichId);
                sqlParam[5] = new SqlParameter("@pi_connect_dt", detail.ConnectDt);
                sqlParam[6] = new SqlParameter("@pi_timing_indicator", detail.TimingInd);
                sqlParam[7] = new SqlParameter("@pi_ama_answer_ind", detail.AMAAnswerInd);
                sqlParam[8] = new SqlParameter("@pi_originating_number", detail.OriginatingNum);
                sqlParam[9] = new SqlParameter("@pi_originating_number_type", detail.OriginatingNumType);
                sqlParam[10] = new SqlParameter("@pi_originating_CCITT", detail.OriginatingCCITT);
                sqlParam[11] = new SqlParameter("@pi_dialed_number", detail.DialedNum);
                sqlParam[12] = new SqlParameter("@pi_dialed_number_type", detail.DialedNumType);
                sqlParam[13] = new SqlParameter("@pi_terminating_number", detail.TerminatingNum);
                sqlParam[14] = new SqlParameter("@pi_terminating_number_type", detail.TerminatingNumType);
                sqlParam[15] = new SqlParameter("@pi_elapsed_time", detail.ElapsedTime);
                sqlParam[16] = new SqlParameter("@pi_call_progress_stopped", detail.CallProgressStopped);
                sqlParam[17] = new SqlParameter("@pi_TT_USF", detail.TT_USF);
                sqlParam[18] = new SqlParameter("@pi_station_group_designator", detail.StationGroupDesignator);
                sqlParam[19] = new SqlParameter("@pi_authorization_code", detail.AuthorizationCd);
                sqlParam[20] = new SqlParameter("@pi_incoming_trunk_subgroup_number", detail.IncomingTrunkSubgroupNum);
                sqlParam[21] = new SqlParameter("@pi_incoming_trunk_subgroup_member", detail.IncomingTrunkSubgroupMember);
                sqlParam[22] = new SqlParameter("@pi_date_rate_ind", detail.DateRateInd);
                sqlParam[23] = new SqlParameter("@pi_ISDN_ACI_features", detail.ISDN_ACIFeatures);
                sqlParam[24] = new SqlParameter("@pi_station_ID", detail.StationId);
                sqlParam[25] = new SqlParameter("@pi_message_count", detail.MessageCount);
                sqlParam[26] = new SqlParameter("@pi_call_count", detail.CallCount);
                sqlParam[27] = new SqlParameter("@pi_elapsed_time_in_queue", detail.ElapsedTimeInQueue);
                sqlParam[28] = new SqlParameter("@pi_service_feature_ind", detail.ServiceFeatureInd);
                sqlParam[29] = new SqlParameter("@pi_service_feature", detail.ServiceFeature);
                sqlParam[30] = new SqlParameter("@pi_bill_to_ind", detail.BillToInd);
                sqlParam[31] = new SqlParameter("@pi_service_ind_code", detail.ServiceIndCd);
                sqlParam[32] = new SqlParameter("@pi_announcements_before_routing", detail.AnnouncementsBeforeRouting);
                sqlParam[33] = new SqlParameter("@pi_alternate_billing_number", detail.AlternateBillingNum);
                sqlParam[34] = new SqlParameter("@pi_present_dt", detail.PresentDt);
                sqlParam[35] = new SqlParameter("@pi_WATS_ind", detail.WATSInd);
                sqlParam[36] = new SqlParameter("@pi_WATS_band_ind", detail.WATSBandInd);
                sqlParam[37] = new SqlParameter("@pi_SID_ind", detail.SIDInd);
                sqlParam[38] = new SqlParameter("@pi_time_digits_outpulsed", detail.TimeDigitsOutpulsed);
                sqlParam[39] = new SqlParameter("@pi_call_disposition_code", detail.CallDispositionCd);
                sqlParam[40] = new SqlParameter("@pi_account_code", detail.AccountCd);
                sqlParam[41] = new SqlParameter("@pi_incoming_access_ind", detail.IncomingAccessInd);
                sqlParam[42] = new SqlParameter("@pi_entered_digits", detail.EnteredDigits);
                sqlParam[43] = new SqlParameter("@pi_outgoing_switch_ID", detail.OutgoingSwitchId);
                sqlParam[44] = new SqlParameter("@pi_outgoing_access_ind", detail.OutgoingAccessInd);
                sqlParam[45] = new SqlParameter("@pi_outgoing_trunk_subgroup_number", detail.OutgoingTrunkSubgroupNum);
                sqlParam[46] = new SqlParameter("@pi_outgoing_trunk_subgroup_member", detail.OutgoingTrunkSubgroupMember);
                sqlParam[47] = new SqlParameter("@pi_outpulsed_digits", detail.OutpulsedDigits);
                sqlParam[48] = new SqlParameter("@pi_charge_number", detail.ChargeNum);
                sqlParam[49] = new SqlParameter("@pi_toll_free_number", detail.TollFreeNum);
                sqlParam[50] = new SqlParameter("@pi_VAB_rate_ind", detail.VABRateInd);
                sqlParam[51] = new SqlParameter("@pi_VAB_new_charge", detail.VABNewCharge);
                sqlParam[52] = new SqlParameter("@pi_VAB_elapsed_time", detail.VABElapsedTime);
                sqlParam[53] = new SqlParameter("@pi_announcements_elapsed_time", detail.AnnouncementsElapsedTime);
                sqlParam[54] = new SqlParameter("@pi_CPRating_announcement", detail.CPRatingAnnouncement);
                sqlParam[55] = new SqlParameter("@pi_CPRating_digits", detail.CPRatingDigits);
                sqlParam[56] = new SqlParameter("@pi_customer_features_available", detail.CustomerFeaturesAvailable);
                sqlParam[57] = new SqlParameter("@pi_far_end_NPA", detail.FarEndNPA);
                sqlParam[58] = new SqlParameter("@pi_OLI_II_digits", detail.OLI_IIDigits);
                sqlParam[59] = new SqlParameter("@pi_operator_services", detail.OperatorServices);
                sqlParam[60] = new SqlParameter("@pi_CPR_status_ind", detail.CPRStatusInd);
                sqlParam[61] = new SqlParameter("@pi_TTChild", detail.TT_Child);
                sqlParam[62] = new SqlParameter("@pi_CSID_Indication", detail.CSIDIndication);
                sqlParam[63] = new SqlParameter("@pi_week_routing_count", detail.WeekRoutingCount);
                sqlParam[64] = new SqlParameter("@pi_geographic_routing_count", detail.GeographicRoutingCount);
                sqlParam[65] = new SqlParameter("@pi_allocator_count", detail.AllocatorCount);
                sqlParam[66] = new SqlParameter("@pi_dialed_number_decision_count", detail.DialedNumDecisionCount);
                sqlParam[67] = new SqlParameter("@pi_next_available_agent_count", detail.NextAvailableAgentCount);
                sqlParam[68] = new SqlParameter("@pi_voice_prompter", detail.VoicePrompter);
                sqlParam[69] = new SqlParameter("@pi_1st_annc_number", detail.FirstAnncNumber);
                sqlParam[70] = new SqlParameter("@pi_1st_annc_listen_time", detail.FirstAnncListenTime);
                sqlParam[71] = new SqlParameter("@pi_1st_annc_type", detail.FirstAnncType);
                sqlParam[72] = new SqlParameter("@pi_1st_annc_category", detail.FirstAnncCategory);
                sqlParam[73] = new SqlParameter("@pi_2nd_annc_number", detail.SecondAnncNumber);
                sqlParam[74] = new SqlParameter("@pi_2nd_annc_listen_time", detail.SecondAnncListenTime);
                sqlParam[75] = new SqlParameter("@pi_2nd_annc_type", detail.SecondAnncType);
                sqlParam[76] = new SqlParameter("@pi_2nd_annc_category", detail.SecondAnncCategory);
                sqlParam[77] = new SqlParameter("@pi_3rd_annc_number", detail.ThirdAnncNumber);
                sqlParam[78] = new SqlParameter("@pi_3rd_annc_listen_time", detail.ThirdAnncListenTime);
                sqlParam[79] = new SqlParameter("@pi_3rd_annc_type", detail.ThirdAnncType);
                sqlParam[80] = new SqlParameter("@pi_3rd_annc_category", detail.ThirdAnncCategory);
                sqlParam[81] = new SqlParameter("@pi_4th_annc_number", detail.FourthAnncNumber);
                sqlParam[82] = new SqlParameter("@pi_4th_annc_listen_time", detail.FourthAnncListenTime);
                sqlParam[83] = new SqlParameter("@pi_4th_annc_type", detail.FourthAnncType);
                sqlParam[84] = new SqlParameter("@pi_4th_annc_category", detail.FourthAnncCategory);
                sqlParam[85] = new SqlParameter("@pi_5th_annc_number", detail.FivethAnncNumber);
                sqlParam[86] = new SqlParameter("@pi_5th_annc_listen_time", detail.FivethAnncListenTime);
                sqlParam[87] = new SqlParameter("@pi_5th_annc_type", detail.FivethAnncType);
                sqlParam[88] = new SqlParameter("@pi_5th_annc_category", detail.FivethAnncCategory);
                sqlParam[89] = new SqlParameter("@pi_ofl_announcement_count", detail.OflAnnouncementCount);
                sqlParam[90] = new SqlParameter("@pi_ofl_annc_listen_time", detail.OflAnncListenTime);
                sqlParam[91] = new SqlParameter("@pi_disconnect_direction", detail.DisconnectDirection);
                sqlParam[92] = new SqlParameter("@pi_ADR_redirection_feature", detail.ADRRedirectionFeature);
                sqlParam[93] = new SqlParameter("@pi_ADR_redirected_from_number", detail.ADRRedirectedFromNum);
                sqlParam[94] = new SqlParameter("@pi_ADR_redirected_from_number_type", detail.ADRRedirectedFromNumType);                
                sqlParam[95] = new SqlParameter("@pi_create_user_id", detail.CreateUserId);
                sqlParam[96] = new SqlParameter("@pi_create_app_name", detail.CreateAppName);                

                command.Parameters.AddRange(sqlParam);
                command.Transaction = trans;
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw ExceptionProcessor.Wrap<DataAccessException>(ex);
            }            
        }
    }
}
