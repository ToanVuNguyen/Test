using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.Common.DataTransferObjects
{
    public class CallingDataDetailDTO: BaseDTO
    {
        public const int RECORD_LENGTH_LEN =  3;
        public const int STRUCTURE_CD_LEN =  4;
        public const int CALL_CD_LEN = 3;
        public const int INCOME_SWICHID_LEN = 6;
        public const int CONNECT_DATE_LEN = 5;
        public const int CONNECT_TIME_LEN = 7;
        public const int TIMING_IND_LEN = 5;
        public const int AMA_ANSWER_IND_LEN = 1;
        public const int ORIGINATING_NUM_LEN = 15;
        public const int ORIGINATING_NUM_TYPE_LEN = 1;
        public const int ORIGINATING_CCITT_LEN = 3;
        public const int DIALED_NUM_LEN = 15;
        public const int DIALED_NUM_TYPE_LEN = 1;
        public const int TERMINATING_NUM_LEN = 15;
        public const int TERMINATING_NUM_TYPE_LEN = 1;
        public const int ELAPSED_TIME_LEN = 8;
        public const int CALL_PROGRESS_STOPPED_LEN = 1;
        public const int TT_USF_LEN = 4;
        public const int STATION_GROUP_DESIGNATOR_LEN = 1;
        public const int AUTHORIZATION_CD_LEN = 15;
        public const int INCOMING_TRUNK_SUBGROUP_NUM_LEN = 5;
        public const int INCOMING_TRUNK_SUBGROUP_MEMBER_LEN = 4;
        public const int DATE_RATE_IND_LEN = 3;
        public const int ISDN_ACIFEATURES_LEN = 3;
        public const int STATION_ID_LEN = 10;
        public const int COUNT_OF_MESSAGE_ASSOCIATED_UUI_LEN = 5;
        public const int COUNT_OF_CALL_ASSOCIATED_TVC_UUI_LEN = 7;
        public const int ELAPSED_TIME_IN_QUEUE_LEN = 8;
        public const int SERVICE_FEATURE_IND_LEN = 3;
        public const int SERVICE_FEATURE_LEN = 3;
        public const int BILL_TO_IND_LEN = 1;
        public const int SERVICE_IND_CD_LEN = 3;
        public const int ANNOUNCEMENTS_BEFORE_ROUTING_LEN = 2;
        public const int ALTERNATE_BILLING_NUM_LEN = 10;
        public const int PRESENT_DATE_LEN = 5;
        public const int PRESENT_TIME_LEN = 7;
        public const int WIDE_AREA_TELEPHONE_SERVICE_IND_LEN = 1;
        public const int WATS_BAND_OR_TYPE_IND_LEN = 3;
        public const int SID_IND_LEN = 1;
        public const int TIME_DIGITS_OUTPULSED_LEN = 7;
        public const int CALL_DISPOSITION_CD_LEN = 3;
        public const int ACCOUNT_CD_LEN = 8;
        public const int INCOMING_ACCESS_IND_LEN = 1;
        public const int ENTERED_DIGITS_LEN = 30;
        public const int OUTGOING_SWITCH_ID_LEN = 6;
        public const int OUTGOING_ACCESS_IND_LEN = 1;
        public const int OUTGOING_TRUNK_SUBGROUP_NUM_LEN = 5;
        public const int OUTGOING_TRUNK_SUBGROUP_MEMBER_LEN = 4;
        public const int OUTPULSED_DIGITS_LEN = 24;
        public const int CHARGE_NUM_LEN = 10;
        public const int TOLL_FREE_NUM_LEN = 10;
        public const int VAB_RATE_IND_LEN = 1;
        public const int VAB_NEW_CHARGE_LEN = 5;
        public const int VAB_ELAPSED_TIME_LEN = 8;
        public const int ANNOUNCEMENTS_ELAPSED_TIME_LEN = 8;
        public const int CP_RATING_ANNOUNCEMENT_LEN = 5;
        public const int CP_RATING_DIGITS_LEN = 24;
        public const int CUSTOMER_FEATURES_AVAILABLE_LEN = 4;
        public const int FAR_END_NPA_LEN = 3;
        public const int OLI_II_DIGITS_LEN = 2;
        public const int OPERATOR_SERVICES_LEN = 1;
        public const int CPR_STATUS_IND_LEN = 1;
        public const int TT_USFI_CHILD_LEN = 5;
        public const int CSID_IND_LEN = 1;
        public const int WEEK_ROUTING_COUNT_LEN = 3;
        public const int GEOGRAPHIC_ROUTING_COUNT_LEN = 3;
        public const int ALLOCATOR_COUNT_LEN = 3;
        public const int DIALED_NUM_DECISION_COUNT_LEN = 3;
        public const int NEXT_AVAILABLE_AGENT_COUNT_LEN = 3;
        public const int VOICE_PROMPTER_LEN = 3;
        public const int FIRST_ANNC_NUMBER_LEN = 6;
        public const int FIRST_ANNC_LISTEN_TIME_LEN = 4;
        public const int FIRST_ANNC_TYPE_LEN = 1;
        public const int FIRST_ANNC_CATEGORY_LEN = 1;
        public const int SECOND_ANNC_NUMBER_LEN = 6;
        public const int SECOND_ANNC_LISTEN_TIME_LEN = 4;
        public const int SECOND_ANNC_TYPE_LEN = 1;
        public const int SECOND_ANNC_CATEGORY_LEN = 1;
        public const int THIRD_ANNC_NUMBER_LEN = 6;
        public const int THIRD_ANNC_LISTEN_TIME_LEN = 4;
        public const int THIRD_ANNC_TYPE_LEN = 1;
        public const int THIRD_ANNC_CATEGORY_LEN = 1;
        public const int FOURTH_ANNC_NUMBER_LEN = 6;
        public const int FOURTH_ANNC_LISTEN_TIME_LEN = 4;
        public const int FOURTH_ANNC_TYPE_LEN = 1;
        public const int FOURTH_ANNC_CATEGORY_LEN = 1;
        public const int FIVETH_ANNC_NUMBER_LEN = 6;
        public const int FIVETH_ANNC_LISTEN_TIME_LEN = 4;
        public const int FIVETH_ANNC_TYPE_LEN = 1;
        public const int FIVETH_ANNC_CATEGORY_LEN = 1;
        public const int OFL_ANNOUNCEMENT_COUNT_LEN = 3;
        public const int OFL_ANNC_LISTEN_TIME_LEN = 5;
        public const int DISCONNECT_DIRECTION_LEN = 1;
        public const int ADR_REDIRECTION_FEATURE_LEN = 7;
        public const int ADR_REDIRECTED_FROM_NUM_LEN = 15;
        public const int ADR_REDIRECTED_FROM_NUM_TYPE_LEN = 1;

        public int CallingHeaderId { get; set; }
        public int? RecordLength { get; set; }
        public string StructureCd { get; set; }
        public string CallCd { get; set; }
        public string IncomeSwichId { get; set; }
        public DateTime? ConnectDt { get; set; }
        public string TimingInd { get; set; }
        public string AMAAnswerInd { get; set; }
        public string OriginatingNum { get; set; }
        public string OriginatingNumType { get; set; }
        public string OriginatingCCITT { get; set; }
        public string DialedNum { get; set; }
        public string DialedNumType { get; set; }
        public string TerminatingNum { get; set; }
        public string TerminatingNumType { get; set; }
        public int? ElapsedTime { get; set; }
        public string CallProgressStopped { get; set; }
        public string TT_USF { get; set; }
        public string StationGroupDesignator { get; set; }
        public string AuthorizationCd { get; set; }
        public string IncomingTrunkSubgroupNum { get; set; }
        public string IncomingTrunkSubgroupMember { get; set; }
        public string DateRateInd { get; set; }
        public string ISDN_ACIFeatures { get; set; }
        public string StationId { get; set; }
        /// <summary>
        /// CountOfMessage_AssociatedUUI
        /// </summary>
        public string MessageCount { get; set; }
        /// <summary>
        /// CountOfCall_AssociatedTVCUUI
        /// </summary>
        public string CallCount { get; set; }
        public int? ElapsedTimeInQueue { get; set; }
        public string ServiceFeatureInd { get; set; }
        public string ServiceFeature { get; set; }
        public string BillToInd { get; set; }
        public string ServiceIndCd { get; set; }
        public string AnnouncementsBeforeRouting { get; set; }
        public string AlternateBillingNum { get; set; }
        public DateTime? PresentDt { get; set; }
        /// <summary>
        /// WideAreaTelephoneServiceInd
        /// </summary>
        public string WATSInd { get; set; }
        /// <summary>
        /// WATSBandOrTypeInd
        /// </summary>
        public string WATSBandInd { get; set; }
        public string SIDInd { get; set; }
        public string TimeDigitsOutpulsed { get; set; }
        public string CallDispositionCd { get; set; }
        public string AccountCd { get; set; }
        public string IncomingAccessInd { get; set; }
        public string EnteredDigits { get; set; }
        public string OutgoingSwitchId { get; set; }
        public string OutgoingAccessInd { get; set; }
        public string OutgoingTrunkSubgroupNum { get; set; }
        public string OutgoingTrunkSubgroupMember { get; set; }
        public string OutpulsedDigits { get; set; }
        public string ChargeNum { get; set; }
        public string TollFreeNum { get; set; }
        public string VABRateInd { get; set; }
        public string VABNewCharge { get; set; }
        public string VABElapsedTime { get; set; }//TimeSpan?
        public string AnnouncementsElapsedTime { get; set; }//TimeSpan?
        public string CPRatingAnnouncement { get; set; }
        public string CPRatingDigits { get; set; }
        public string CustomerFeaturesAvailable { get; set; }
        public string FarEndNPA { get; set; }
        public string OLI_IIDigits { get; set; }
        public string OperatorServices { get; set; }
        public string CPRStatusInd { get; set; }
        public string TT_Child { get; set; }
        public string CSIDIndication { get; set; }
        public string WeekRoutingCount { get; set; }
        public string GeographicRoutingCount { get; set; }
        public string AllocatorCount { get; set; }
        public string DialedNumDecisionCount { get; set; }
        public string NextAvailableAgentCount { get; set; }
        public string VoicePrompter { get; set; }
        public string FirstAnncNumber { get; set; }
        public string FirstAnncListenTime { get; set; }
        public string FirstAnncType { get; set; }
        public string FirstAnncCategory { get; set; }
        public string SecondAnncNumber { get; set; }
        public string SecondAnncListenTime { get; set; }
        public string SecondAnncType { get; set; }
        public string SecondAnncCategory { get; set; }
        public string ThirdAnncNumber { get; set; }
        public string ThirdAnncListenTime { get; set; }
        public string ThirdAnncType { get; set; }
        public string ThirdAnncCategory { get; set; }
        public string FourthAnncNumber { get; set; }
        public string FourthAnncListenTime { get; set; }
        public string FourthAnncType { get; set; }
        public string FourthAnncCategory { get; set; }
        public string FivethAnncNumber { get; set; }
        public string FivethAnncListenTime { get; set; }
        public string FivethAnncType { get; set; }
        public string FivethAnncCategory { get; set; }
        public string OflAnnouncementCount { get; set; }
        public string OflAnncListenTime { get; set; }
        public string DisconnectDirection { get; set; }
        public string ADRRedirectionFeature { get; set; }
        public string ADRRedirectedFromNum { get; set; }
        public string ADRRedirectedFromNumType { get; set; }
    }
}
