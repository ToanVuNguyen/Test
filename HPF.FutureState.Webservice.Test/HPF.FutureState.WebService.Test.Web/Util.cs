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

        public static string ConvertToString(object obj)
        {            
            if (obj == null)
                return null;
            if (string.IsNullOrEmpty(obj.ToString())) return null;
            return obj.ToString();
        }
        public static string ConvertToString(XElement obj)
        {
            if (obj == null)
                return null;
            if (string.IsNullOrEmpty(obj.Value)) return null;
            return obj.Value;
        }

        public static DateTime? ConvertToDateTime(XElement obj)
        {
            if (obj == null)
                return null;
            if (string.IsNullOrEmpty(obj.Value)) return null;
            DateTime dt;
            if (!DateTime.TryParse(obj.Value, out dt))
                return null;
            return dt;
        }

        public static int? ConvertToInt(XElement obj)
        {
            if (obj == null)
                return null;
            if (string.IsNullOrEmpty(obj.Value)) return null;
            int i;
            if (!int.TryParse(obj.Value, out i))
                return null;
            return i;
        }

        public static double? ConvertToDouble(XElement obj)
        {
            if (obj == null)
                return null;
            if (string.IsNullOrEmpty(obj.Value)) return null;
            double d;
            if (!double.TryParse(obj.Value, out d))
                return null;
            return d;
        }

        public static decimal? ConvertToDecimal(XElement obj)
        {
            if (obj == null)
                return null;
            if (string.IsNullOrEmpty(obj.Value)) return null;
            decimal dec;
            if (!decimal.TryParse(obj.Value, out dec))
                return null;
            return dec;
        }

        public static byte? ConvertToByte(XElement obj)
        {
            if (obj == null)
                return null;
            if (string.IsNullOrEmpty(obj.Value)) return null;
            byte b;
            if (!byte.TryParse(obj.Value, out b))
                return null;
            return b;
        }
    }
}
