using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace HPF.FutureState.Web.BillingAdmin
{
    public partial class FixedHeaderGrid : System.Web.UI.UserControl
    {
        private List<string> _columsName;
        public List<String> ColumsName
        {
            get
            {
                return _columsName;
            }
            set
            {
                _columsName = value;
                CreateHeader();
            }
        }
        private List<int> _columsWidth;
        
        public List<int> ColumsWidth
        {
            get
            {
                return _columsWidth;
            }
            set
            {
                _columsWidth = value;
                CreateHeader();
            }
        }
        private string _headerCssClass;
        public string HeaderCssClass 
        { 
            get
            {
                return _headerCssClass;
            }
            set 
            {
                _headerCssClass = value;
                tbHeader.CssClass = value; 
            } 
        }
        private string _gridCssClass;

        public string GridCssClass
        {
            get { return _gridCssClass; }
            set 
            { 
                _gridCssClass = value;
                gridData.CssClass = value;
            }
        }
        public object DataSource
        {
            set
            {
                gridData.DataSource = value;
                gridData.RowDataBound += new GridViewRowEventHandler(GridDataRowDataBound);
                gridData.RowStyle.Wrap = true;
                gridData.DataBind();
            }
        }
        void GridDataRowDataBound(object sender, GridViewRowEventArgs e)
        {
            for(int i=0;i<ColumsWidth.Count;i++)
            {
                e.Row.Cells[i].Width = ColumsWidth[i];
                e.Row.Cells[i].Wrap = true;
            }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void CreateHeader()
        {
            if (ColumsName == null || ColumsWidth == null)
                return;
            tbHeader.Rows.Clear();
            var row = new TableRow();
            for (int i = 0; i < ColumsName.Count;i++ )
            {
                TableCell cell = new TableCell();
                cell.Text = ColumsName[i];
                cell.Width = ColumsWidth[i];
                cell.BorderWidth = 1;
                row.Cells.Add(cell);
            }
            tbHeader.Rows.Add(row);
        }
    }
}