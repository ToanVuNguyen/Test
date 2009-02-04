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
        public static DateTime? ConvertToDateTime(object obj)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                try
                {
                    return DateTime.Parse(obj.ToString());
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            else
                return null;
        }

        public static int? ConvertToInt(object obj)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                try
                {
                    return int.Parse(obj.ToString());
                }
                catch
                {
                    return int.MinValue;
                }
            }
            else
                return null;
        }

        public static decimal? ConvertToDecimal(object obj)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                try
                {
                    return Decimal.Parse(obj.ToString());
                }
                catch
                {
                    return Decimal.MinValue;
                }
            }
            else
                return null;
        }

        public static double? ConvertToDouble(object obj)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                try
                {
                    return double.Parse(obj.ToString());
                }
                catch
                {
                    return double.MinValue;
                }
            }
            else
                return null;
        }

        public static byte? ConvertToByte(object obj)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                try
                {
                    return byte.Parse(obj.ToString());
                }
                catch
                {
                    return byte.MinValue;
                }
            }
            else
                return null;
        }        
    }
}
