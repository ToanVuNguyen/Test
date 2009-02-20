using System;
using System.Collections.Generic;
using System.Text;
using HPF.SharePointAPI.Enum;

namespace HPF.SharePointAPI.BusinessEntity
{
    public class ConselingSummaryInfo:BaseObject
    {
        private int _loanNumber;
        private string _servicer;
        private DateTime _completedDate;
        private DateTime _foreclosureSaleDate;
        private string _delinquency;
        private ReviewStatus _reviewStatus;

        public int LoanNumber
        {
            get { return _loanNumber; }
            set { _loanNumber = value; }
        }

        public string Servicer
        {
            get { return _servicer; }
            set { _servicer = value; }
        }

        public DateTime CompletedDate
        {
            get { return _completedDate; }
            set { _completedDate = value; }
        }

        public DateTime ForeclosureSaleDate
        {
            get { return _foreclosureSaleDate; }
            set { _foreclosureSaleDate = value; }
        }

        public string Delinquency
        {
            get { return _delinquency; }
            set { _delinquency = value; }
        }

        public ReviewStatus ReviewStatus
        {
            get { return _reviewStatus; }
            set { _reviewStatus = value; }
        }

        public ConselingSummaryInfo():base()
        {
            _reviewStatus = ReviewStatus.PendingReview;
        }
        public ConselingSummaryInfo(string name, byte[] file,
             int loanNumber, string servicer, DateTime completedDate, 
             DateTime foreclosureSaleDate, Delinquency delinquency, ReviewStatus reviewStatus):
            base(name, file)
        {
            _loanNumber = loanNumber;
            _servicer = servicer;
            _completedDate = completedDate;
            _foreclosureSaleDate = foreclosureSaleDate;
            _delinquency = delinquency;
            _reviewStatus = reviewStatus;
        }
    }
}
