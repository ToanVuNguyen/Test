using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.IO;

namespace HPF.FutureState.Web.Security
{
    public class HPFMediaFileHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string filePath = context.Server.MapPath(context.Request.ServerVariables["SCRIPT_NAME"].ToString());
            FileInfo file = new System.IO.FileInfo(filePath);
            if (file.Exists)
            {
                //return the file
                context.Response.Clear();
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                context.Response.AddHeader("Content-Length", file.Length.ToString());
                context.Response.ContentType = "application/octet-stream";
                context.Response.WriteFile(file.FullName);
                context.ApplicationInstance.CompleteRequest();
                context.Response.End();
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
