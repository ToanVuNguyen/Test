using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects;
using System.Globalization;

namespace HPF.FutureState.BusinessLogic
{
    public class CallingDataBL: BaseBusinessLogic
    {
        private static readonly CallingDataBL instance = new CallingDataBL();
        /// <summary>
        /// Singleton
        /// </summary>
        public static CallingDataBL Instance
        {
            get
            {
                return instance;
            }
        }

        protected CallingDataBL()
        {            
        }

        public CallingDataHeaderDTO ReadHeaderData(string buffer)
        {
            int startIndex = 0;
            if(string.IsNullOrEmpty(buffer)|| buffer.Length < 132) return null;
            CallingDataHeaderDTO result = new CallingDataHeaderDTO();
            
            result.FileLength = (float)((double)ReadLong(buffer,ref startIndex, CallingDataHeaderDTO.FILE_LENGTH_LEN).Value / 1024f)/1024f;            
            result.SubscriberId = ReadString(buffer, ref startIndex, CallingDataHeaderDTO.SUBSCRIBER_ID_LEN);
            result.SubaccountName = ReadString(buffer, ref startIndex, CallingDataHeaderDTO.SUBACCOUNT_NAME_LEN);
            result.LoginId = ReadString(buffer, ref startIndex, CallingDataHeaderDTO.LONGIN_ID_LEN);
            result.NumAssignedServiceType = ReadString(buffer, ref startIndex, CallingDataHeaderDTO.NUM_ASSIGNED_SERVICE_TYPE_LEN);
            result.ServiceType = ReadString(buffer, ref startIndex, CallingDataHeaderDTO.SERVICER_TYPE_LEN);
            result.Reserved = ReadString(buffer, ref startIndex, CallingDataHeaderDTO.RESERVED_LEN);
            result.FileCreationDt = ReadDateTime(buffer, ref startIndex, CallingDataHeaderDTO.FILE_CREATION_DATE_LEN).Value;            
            result.StartDt = ReadDateTime(buffer, ref startIndex, CallingDataHeaderDTO.DATE_LEN, CallingDataHeaderDTO.TIME_LEN).Value;
            result.EndDt = ReadDateTime(buffer, ref startIndex, CallingDataHeaderDTO.DATE_LEN, CallingDataHeaderDTO.TIME_LEN).Value;
            result.CallRecordCount = ReadInt(buffer, ref startIndex, CallingDataHeaderDTO.CALL_RECORD_COUNT_LEN).Value;
            result.CustomerProvidedHeader = ReadString(buffer, ref startIndex, CallingDataHeaderDTO.CUSTOMER_PROVIDED_HEADER_LEN);
            result.DownloadType = ReadString(buffer, ref startIndex, CallingDataHeaderDTO.DOWNLOAD_TYPE_LEN);
            result.ReportForm = ReadString(buffer, ref startIndex, CallingDataHeaderDTO.REPORT_FORM_LEN);

            return result;
        }

        public CallingDataDetailDTO ReadDetailData(string buffer)
        {
            int startIndex = 0;
            CallingDataDetailDTO result = new CallingDataDetailDTO();

            result.RecordLength = ReadInt(buffer, ref startIndex, CallingDataDetailDTO.RECORD_LENGTH_LEN);
            result.StructureCd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.STRUCTURE_CD_LEN);
            result.CallCd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.CALL_CD_LEN);
            result.IncomeSwichId = ReadString(buffer, ref startIndex, CallingDataDetailDTO.INCOME_SWICHID_LEN);
            result.ConnectDt = ReadDateTimeFromNum(buffer, ref startIndex, CallingDataDetailDTO.CONNECT_DATE_LEN, CallingDataDetailDTO.CONNECT_TIME_LEN);
            result.TimingInd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.TIMING_IND_LEN);
            result.AMAAnswerInd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.AMA_ANSWER_IND_LEN);
            result.OriginatingNum = ReadString(buffer, ref startIndex, CallingDataDetailDTO.ORIGINATING_NUM_LEN);
            result.OriginatingNumType = ReadString(buffer, ref startIndex, CallingDataDetailDTO.ORIGINATING_NUM_TYPE_LEN);
            result.OriginatingCCITT = ReadString(buffer, ref startIndex, CallingDataDetailDTO.ORIGINATING_CCITT_LEN);
            result.DialedNum = ReadString(buffer, ref startIndex, CallingDataDetailDTO.DIALED_NUM_LEN);
            result.DialedNumType = ReadString(buffer, ref startIndex, CallingDataDetailDTO.DIALED_NUM_TYPE_LEN);
            result.TerminatingNum = ReadString(buffer, ref startIndex, CallingDataDetailDTO.TERMINATING_NUM_LEN);
            result.TerminatingNumType = ReadString(buffer, ref startIndex, CallingDataDetailDTO.TERMINATING_NUM_TYPE_LEN);
            result.ElapsedTime = ReadInt(buffer, ref startIndex, CallingDataDetailDTO.ELAPSED_TIME_LEN);
            result.CallProgressStopped = ReadString(buffer, ref startIndex, CallingDataDetailDTO.CALL_PROGRESS_STOPPED_LEN);
            result.TT_USF = ReadString(buffer, ref startIndex, CallingDataDetailDTO.TT_USF_LEN);
            result.StationGroupDesignator = ReadString(buffer, ref startIndex, CallingDataDetailDTO.STATION_GROUP_DESIGNATOR_LEN);
            result.AuthorizationCd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.AUTHORIZATION_CD_LEN);
            result.IncomingTrunkSubgroupNum = ReadString(buffer, ref startIndex, CallingDataDetailDTO.INCOMING_TRUNK_SUBGROUP_NUM_LEN);
            result.IncomingTrunkSubgroupMember = ReadString(buffer, ref startIndex, CallingDataDetailDTO.INCOMING_TRUNK_SUBGROUP_MEMBER_LEN);
            result.DateRateInd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.DATE_RATE_IND_LEN);
            result.ISDN_ACIFeatures = ReadString(buffer, ref startIndex, CallingDataDetailDTO.ISDN_ACIFEATURES_LEN);
            result.StationId = ReadString(buffer, ref startIndex, CallingDataDetailDTO.STATION_ID_LEN);
            result.MessageCount = ReadString(buffer, ref startIndex, CallingDataDetailDTO.COUNT_OF_MESSAGE_ASSOCIATED_UUI_LEN);
            result.CallCount = ReadString(buffer, ref startIndex, CallingDataDetailDTO.COUNT_OF_CALL_ASSOCIATED_TVC_UUI_LEN);
            result.ElapsedTimeInQueue = ReadInt(buffer, ref startIndex, CallingDataDetailDTO.ELAPSED_TIME_IN_QUEUE_LEN);
            result.ServiceFeatureInd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.SERVICE_FEATURE_IND_LEN);
            result.ServiceFeature = ReadString(buffer, ref startIndex, CallingDataDetailDTO.SERVICE_FEATURE_LEN);
            result.BillToInd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.BILL_TO_IND_LEN);
            result.ServiceIndCd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.SERVICE_IND_CD_LEN);
            result.AnnouncementsBeforeRouting = ReadString(buffer, ref startIndex, CallingDataDetailDTO.ANNOUNCEMENTS_BEFORE_ROUTING_LEN);
            result.AlternateBillingNum = ReadString(buffer, ref startIndex, CallingDataDetailDTO.ALTERNATE_BILLING_NUM_LEN);
            result.PresentDt = ReadDateTimeFromNum(buffer, ref startIndex, CallingDataDetailDTO.PRESENT_DATE_LEN, CallingDataDetailDTO.PRESENT_TIME_LEN);
            result.WATSInd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.WIDE_AREA_TELEPHONE_SERVICE_IND_LEN);
            result.WATSBandInd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.WATS_BAND_OR_TYPE_IND_LEN);
            result.SIDInd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.SID_IND_LEN);
            result.TimeDigitsOutpulsed = ReadString(buffer, ref startIndex, CallingDataDetailDTO.TIME_DIGITS_OUTPULSED_LEN);
            result.CallDispositionCd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.CALL_DISPOSITION_CD_LEN);
            result.AccountCd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.ACCOUNT_CD_LEN);
            result.IncomingAccessInd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.INCOMING_ACCESS_IND_LEN);
            result.EnteredDigits = ReadString(buffer, ref startIndex, CallingDataDetailDTO.ENTERED_DIGITS_LEN);
            result.OutgoingSwitchId = ReadString(buffer, ref startIndex, CallingDataDetailDTO.OUTGOING_SWITCH_ID_LEN);
            result.OutgoingAccessInd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.OUTGOING_ACCESS_IND_LEN);
            result.OutgoingTrunkSubgroupNum = ReadString(buffer, ref startIndex, CallingDataDetailDTO.OUTGOING_TRUNK_SUBGROUP_NUM_LEN);
            result.OutgoingTrunkSubgroupMember = ReadString(buffer, ref startIndex, CallingDataDetailDTO.OUTGOING_TRUNK_SUBGROUP_MEMBER_LEN);
            result.OutpulsedDigits = ReadString(buffer, ref startIndex, CallingDataDetailDTO.OUTPULSED_DIGITS_LEN);
            result.ChargeNum = ReadString(buffer, ref startIndex, CallingDataDetailDTO.CHARGE_NUM_LEN);
            result.TollFreeNum = ReadString(buffer, ref startIndex, CallingDataDetailDTO.TOLL_FREE_NUM_LEN);
            result.VABRateInd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.VAB_RATE_IND_LEN);
            result.VABNewCharge = ReadString(buffer, ref startIndex, CallingDataDetailDTO.VAB_NEW_CHARGE_LEN);
            result.VABElapsedTime = ReadString(buffer, ref startIndex, CallingDataDetailDTO.VAB_ELAPSED_TIME_LEN);
            result.AnnouncementsElapsedTime = ReadString(buffer, ref startIndex, CallingDataDetailDTO.ANNOUNCEMENTS_ELAPSED_TIME_LEN);
            result.CPRatingAnnouncement = ReadString(buffer, ref startIndex, CallingDataDetailDTO.CP_RATING_ANNOUNCEMENT_LEN);
            result.CPRatingDigits = ReadString(buffer, ref startIndex, CallingDataDetailDTO.CP_RATING_DIGITS_LEN);
            result.CustomerFeaturesAvailable = ReadString(buffer, ref startIndex, CallingDataDetailDTO.CUSTOMER_FEATURES_AVAILABLE_LEN);
            result.FarEndNPA = ReadString(buffer, ref startIndex, CallingDataDetailDTO.FAR_END_NPA_LEN);
            result.OLI_IIDigits = ReadString(buffer, ref startIndex, CallingDataDetailDTO.OLI_II_DIGITS_LEN);
            result.OperatorServices = ReadString(buffer, ref startIndex, CallingDataDetailDTO.OPERATOR_SERVICES_LEN);
            result.CPRStatusInd = ReadString(buffer, ref startIndex, CallingDataDetailDTO.CPR_STATUS_IND_LEN);
            result.TT_Child = ReadString(buffer, ref startIndex, CallingDataDetailDTO.TT_USFI_CHILD_LEN);
            result.CSIDIndication = ReadString(buffer, ref startIndex, CallingDataDetailDTO.CSID_IND_LEN);
            result.WeekRoutingCount = ReadString(buffer, ref startIndex, CallingDataDetailDTO.WEEK_ROUTING_COUNT_LEN);
            result.GeographicRoutingCount = ReadString(buffer, ref startIndex, CallingDataDetailDTO.GEOGRAPHIC_ROUTING_COUNT_LEN);
            result.AllocatorCount = ReadString(buffer, ref startIndex, CallingDataDetailDTO.ALLOCATOR_COUNT_LEN);
            result.DialedNumDecisionCount = ReadString(buffer, ref startIndex, CallingDataDetailDTO.DIALED_NUM_DECISION_COUNT_LEN);
            result.NextAvailableAgentCount = ReadString(buffer, ref startIndex, CallingDataDetailDTO.NEXT_AVAILABLE_AGENT_COUNT_LEN);
            result.VoicePrompter = ReadString(buffer, ref startIndex, CallingDataDetailDTO.VOICE_PROMPTER_LEN);
            result.FirstAnncNumber = ReadString(buffer, ref startIndex, CallingDataDetailDTO.FIRST_ANNC_NUMBER_LEN);
            result.FirstAnncListenTime = ReadString(buffer, ref startIndex, CallingDataDetailDTO.FIRST_ANNC_LISTEN_TIME_LEN);
            result.FirstAnncType = ReadString(buffer, ref startIndex, CallingDataDetailDTO.FIRST_ANNC_TYPE_LEN);
            result.FirstAnncCategory = ReadString(buffer, ref startIndex, CallingDataDetailDTO.FIRST_ANNC_CATEGORY_LEN);
            result.SecondAnncNumber = ReadString(buffer, ref startIndex, CallingDataDetailDTO.SECOND_ANNC_NUMBER_LEN);
            result.SecondAnncListenTime = ReadString(buffer, ref startIndex, CallingDataDetailDTO.SECOND_ANNC_LISTEN_TIME_LEN);
            result.SecondAnncType = ReadString(buffer, ref startIndex, CallingDataDetailDTO.SECOND_ANNC_TYPE_LEN);
            result.SecondAnncCategory = ReadString(buffer, ref startIndex, CallingDataDetailDTO.SECOND_ANNC_CATEGORY_LEN);
            result.ThirdAnncNumber = ReadString(buffer, ref startIndex, CallingDataDetailDTO.THIRD_ANNC_NUMBER_LEN);
            result.ThirdAnncListenTime = ReadString(buffer, ref startIndex, CallingDataDetailDTO.THIRD_ANNC_LISTEN_TIME_LEN);
            result.ThirdAnncType = ReadString(buffer, ref startIndex, CallingDataDetailDTO.THIRD_ANNC_TYPE_LEN);
            result.ThirdAnncCategory = ReadString(buffer, ref startIndex, CallingDataDetailDTO.THIRD_ANNC_CATEGORY_LEN);
            result.FourthAnncNumber = ReadString(buffer, ref startIndex, CallingDataDetailDTO.FOURTH_ANNC_NUMBER_LEN);
            result.FourthAnncListenTime = ReadString(buffer, ref startIndex, CallingDataDetailDTO.FOURTH_ANNC_LISTEN_TIME_LEN);
            result.FourthAnncType = ReadString(buffer, ref startIndex, CallingDataDetailDTO.FOURTH_ANNC_TYPE_LEN);
            result.FourthAnncCategory = ReadString(buffer, ref startIndex, CallingDataDetailDTO.FOURTH_ANNC_CATEGORY_LEN);
            result.FivethAnncNumber = ReadString(buffer, ref startIndex, CallingDataDetailDTO.FIVETH_ANNC_NUMBER_LEN);
            result.FivethAnncListenTime = ReadString(buffer, ref startIndex, CallingDataDetailDTO.FIVETH_ANNC_LISTEN_TIME_LEN);
            result.FivethAnncType = ReadString(buffer, ref startIndex, CallingDataDetailDTO.FIVETH_ANNC_TYPE_LEN);
            result.FivethAnncCategory = ReadString(buffer, ref startIndex, CallingDataDetailDTO.FIVETH_ANNC_CATEGORY_LEN);
            result.OflAnnouncementCount = ReadString(buffer, ref startIndex, CallingDataDetailDTO.OFL_ANNOUNCEMENT_COUNT_LEN);
            result.OflAnncListenTime = ReadString(buffer, ref startIndex, CallingDataDetailDTO.OFL_ANNC_LISTEN_TIME_LEN);
            result.DisconnectDirection = ReadString(buffer, ref startIndex, CallingDataDetailDTO.DISCONNECT_DIRECTION_LEN);
            result.ADRRedirectionFeature = ReadString(buffer, ref startIndex, CallingDataDetailDTO.ADR_REDIRECTION_FEATURE_LEN);
            result.ADRRedirectedFromNum = ReadString(buffer, ref startIndex, CallingDataDetailDTO.ADR_REDIRECTED_FROM_NUM_LEN);
            result.ADRRedirectedFromNumType = ReadString(buffer, ref startIndex, CallingDataDetailDTO.ADR_REDIRECTED_FROM_NUM_TYPE_LEN);

            return result;
        }

        private string ReadString(string buffer, ref int startIndex, int len)
        {
            if (startIndex + len > buffer.Length)
            {
                startIndex = buffer.Length;//move to end to stop;
                return null;
            }
            string s = buffer.Substring(startIndex, len);
            if(buffer.Substring(startIndex, 1) == "-")
            {
                startIndex ++;
                return null;
            }
            string value = buffer.Substring(startIndex, len);
            startIndex += len;
            return value.Trim();
        }

        private DateTime? ReadDateTime(string buffer, ref int startIndex, int len)
        {
            string value = ReadString(buffer, ref startIndex, len);
            if (value == null) return null;

            DateTime date = DateTime.Parse(value.Substring(0, CallingDataHeaderDTO.DATE_LEN).Replace(":", "/"));
            date = date.Add(GetTimeSpan(value.Substring(CallingDataHeaderDTO.DATE_LEN + 1, CallingDataHeaderDTO.TIME_LEN)));

            return date;
        }

        private DateTime? ReadDateTime(string buffer, ref int startIndex, int dateLen, int timeLen)
        {
            string value = ReadString(buffer, ref startIndex, dateLen);
            if (value == null)
            {
                value = ReadString(buffer, ref startIndex, timeLen); //skip the time
                return null;
            }
            DateTime date = DateTime.Parse(value.Replace(":", "/"));
            value = ReadString(buffer, ref startIndex, timeLen);
            date = date.Add(GetTimeSpan(value));

            return date;
        }

        private DateTime? ReadDateTimeFromNum(string buffer, ref int startIndex, int dateLen, int timeLen)
        {
            string value = ReadString(buffer, ref startIndex, dateLen);
            if (value == null)
            {
                value = ReadString(buffer, ref startIndex, timeLen);//read and skip time
                return null;
            }
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime date = DateTime.ParseExact("1"+ value, "yyMMdd", provider);
            date = date.Add(GetTimeSpanInDetail(ReadString(buffer, ref startIndex, timeLen)));            

            return date;
        }

        private int? ReadInt(string buffer, ref int startIndex, int len)
        {
            string value = ReadString(buffer, ref startIndex, len);
            if (value == null) return null;
            return int.Parse(value);
        }

        private long? ReadLong(string buffer, ref int startIndex, int len)
        {
            string value = ReadString(buffer, ref startIndex, len);
            if (value == null) return null;
            return long.Parse(value);
        }

        private TimeSpan GetTimeSpan(string buffer)
        {
            return new TimeSpan(int.Parse(buffer.Substring(0, 2)), int.Parse(buffer.Substring(3, 2)), 0);
        }

        private TimeSpan GetTimeSpanInDetail(string buffer)
        {
            return new TimeSpan(0, int.Parse(buffer.Substring(0, 2)), int.Parse(buffer.Substring(2, 2)), int.Parse(buffer.Substring(4, 2)), int.Parse(buffer.Substring(6, 1)));            
        }
    }
}
