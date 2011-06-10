using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HPF.FutureState.Common.DataTransferObjects.WebServices;
using HPF.FutureState.Common.DataTransferObjects;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace HPF.FutureState.Common.Utils
{
    public class DebugInfoCollector
    {
        public ForeclosureCaseSetDTO FCaseSetRequest { get; set; }
        public ForeclosureCaseSaveResponse Response { get; set; }
        public EventSaveRequest EventRequest { get; set; }
        public EventSaveResponse EventResponse { get; set; }
        public int? CurAgencyId { get; set; }
        public string FcId { get; set; }
        public void SaveForeclosureCaseWSDebugInfo()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(WSDebugInfoDTO));
                StringBuilder fileName = new StringBuilder();
                FcId = (!string.IsNullOrEmpty(FcId) ? FcId : "agency_" + CurAgencyId.ToString());
                string folder = EnsureFolderName("agency_" + CurAgencyId.ToString());
                fileName.AppendFormat("{0}{1}{2}{3}{4}{5}", folder, "debug_info_", FcId,"_",DateTime.Now.ToString("yyyyMMddhhmmss"), ".xml");
                WSDebugInfoDTO wsDebugInfo = new WSDebugInfoDTO() { FCaseSetRequest = FCaseSetRequest, Response = Response, FcId = FcId };
                TextWriter writer = new StreamWriter(fileName.ToString());
                serializer.Serialize(writer, wsDebugInfo);
                writer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveEventWSDebugInfo()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(WSDebugInfoDTO));
                StringBuilder fileName = new StringBuilder();
                FcId = (!string.IsNullOrEmpty(FcId) ? FcId : "agency_" + CurAgencyId.ToString());
                string folder = EnsureFolderName("agency_" + CurAgencyId.ToString());
                fileName.AppendFormat("{0}{1}{2}{3}{4}{5}", folder, "save_event_debug_info_", FcId, "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".xml");
                WSDebugInfoDTO wsDebugInfo = new WSDebugInfoDTO() { EventRequest = EventRequest, EventResponse = EventResponse, FcId = FcId };
                TextWriter writer = new StreamWriter(fileName.ToString());
                serializer.Serialize(writer, wsDebugInfo);
                writer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string EnsureFolderName(string folder)
        {
            StringBuilder root = new StringBuilder();
            string uploadDirectory = HPFConfigurationSettings.WS_DEBUG_OUTPUT_PATH;
            root.Append(uploadDirectory);
            foreach (string foldername in folder.Split(new Char[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
            {
                root.AppendFormat("{0}/", foldername);
                if (!Directory.Exists(root.ToString()))
                {
                    Directory.CreateDirectory(root.ToString());
                }
            }
            return root.ToString();
        }

    }
}
