using System;
using System.Collections.Generic;
using System.Text;

namespace HPF.SharePointAPI.BusinessEntity
{
    public class AgencyPayableInfo:BaseObject
    {
        private DateTime? _date;
        private int _year;
        private string _month;
        private string _agencyName;
        private string _payableNumber;
        private DateTime? _payableDate;

        public DateTime? Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public string Month
        {
            get { return _month; }
            set { _month = value; }
        }

        public string AgencyName
        {
            get { return _agencyName; }
            set { _agencyName = value; }
        }

        public string PayableNumber
        {
            get { return _payableNumber; }
            set { _payableNumber = value; }
        }

        public DateTime? PayableDate
        {
            get { return _payableDate; }
            set { _payableDate = value; }
        }

        public AgencyPayableInfo():base() { }
        public AgencyPayableInfo(string name, byte[] file,
            DateTime date, int year, string month, string agencyName,
            string payableNumber, DateTime? payableDate):base(name, file)
        {
            _date = date;
            _year = year;
            _month = month;
            _agencyName = agencyName;
            _payableNumber = payableNumber;
            _payableDate = payableDate;
        }
    }
}
