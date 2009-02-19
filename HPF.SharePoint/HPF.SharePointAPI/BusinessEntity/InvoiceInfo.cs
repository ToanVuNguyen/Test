using System;
using System.Collections.Generic;
using System.Text;

namespace HPF.SharePointAPI.BusinessEntity
{
    public class InvoiceInfo:BaseObject
    {
        private DateTime _date;
        private int _year;
        private string _month;
        private string _fundingSource;
        private string _invoiceNumber;

        public DateTime Date
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

        public string FundingSource
        {
            get { return _fundingSource; }
            set { _fundingSource = value; }
        }

        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set { _invoiceNumber = value; }
        }

        public InvoiceInfo():base() { }
        public InvoiceInfo(string name, byte[] file,
            DateTime date, int year, string month, string fundingSource,
            string invoiceNumber):base(name, file)
        {
            _date = date;
            _year = year;
            _month = month;
            _fundingSource = fundingSource;
            _invoiceNumber = invoiceNumber;
        }
    }
}
