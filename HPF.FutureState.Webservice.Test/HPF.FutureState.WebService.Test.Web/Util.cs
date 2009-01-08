using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace HPF.FutureState.WebService.Test.Web
{
    public class Util
    {
        public static DateTime ConvertToDateTime(Object obj)
        {
            DateTime temp = DateTime.Now;
            DateTime.TryParse(obj.ToString(), out temp);
            return temp;
        }

        public static int ConvertToInt(Object obj)
        {
            int temp = 0;
            int.TryParse(obj.ToString(), out temp);
            return temp;
        }

        public static decimal ConvertToDecimal(Object obj)
        {
            decimal temp = 0;
            decimal.TryParse(obj.ToString(), out temp);
            return temp;
        }

        public static double ConvertToDouble(Object obj)
        {
            double temp = 0;
            double.TryParse(obj.ToString(), out temp);
            return temp;
        }

        public static byte ConvertToByte(Object obj)
        {
            byte temp;
            byte.TryParse(obj.ToString(), out temp);
            return temp;
        }        
    }
}
