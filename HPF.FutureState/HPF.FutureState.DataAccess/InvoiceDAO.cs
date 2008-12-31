using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using HPF.FutureState.Common.DataTransferObjects;

namespace HPF.FutureState.DataAccess
{
    public class InvoiceDAO: BaseDAO
    {
        # region Private variables
        private SqlConnection dbConnection;
        /// <summary>
        /// Share transaction for InvoiceCase and Invoice
        /// </summary>
        private SqlTransaction trans;
        #endregion

        #region Share functions
        public static InvoiceDAO CreateInstance()
        {
            return new InvoiceDAO();
        }

        /// <summary>
        /// Begin working
        /// </summary>
        public void Begin()
        {
            dbConnection = CreateConnection();
            dbConnection.Open();
            trans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Commit work.
        /// </summary>
        public void Commit()
        {
            trans.Commit();
            dbConnection.Close();
        }
        /// <summary>
        /// Cancel work
        /// </summary>
        public void Cancel()
        {
            trans.Rollback();
            dbConnection.Close();
        }
        #endregion

        #region Insert
        public bool InsertInvoiceCase(InvoiceCaseDTO invoiceCase)
        {
            throw new NotImplementedException();
        }
        public bool InserInvoice(InvoiceDTO invoice)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Searching Create Draft
        public InvoiceDTOCollection SearchInvoice(InvoiceSearchCriterialDTO searchCriterial)
        {
            throw new NotImplementedException();
        }

        public InvoiceDraftDTOCollection CreateInvoiceDraft(InvoiceSearchCriterialDTO searchCriterial)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
