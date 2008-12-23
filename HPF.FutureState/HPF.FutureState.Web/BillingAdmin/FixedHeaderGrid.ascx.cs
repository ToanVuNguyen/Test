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
        public List<String> columsName
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
        
        public List<int> columsWidth
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
                tb_header.CssClass = value; 
            } 
        }
        private string _gridCssClass;

        public string GridCssClass
        {
            get { return _gridCssClass; }
            set 
            { 
                _gridCssClass = value;
                grid_data.CssClass = value;
            }
        }
        public object DataSource
        {
            set
            {
                grid_data.DataSource = value;
                grid_data.RowDataBound += new GridViewRowEventHandler(GridDataRowDataBound);
                grid_data.RowStyle.Wrap = true;
                grid_data.DataBind();
            }
        }
        void GridDataRowDataBound(object sender, GridViewRowEventArgs e)
        {
            for(int i=0;i<columsWidth.Count;i++)
            {
                e.Row.Cells[i].Width = columsWidth[i];
                e.Row.Cells[i].Wrap = true;
            }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void CreateHeader()
        {
            if (columsName == null || columsWidth == null)
                return;
            tb_header.Rows.Clear();
            var row = new TableRow();
            for (int i = 0; i < columsName.Count;i++ )
            {
                TableCell cell = new TableCell();
                cell.Text = columsName[i];
                cell.Width = columsWidth[i];
                cell.BorderWidth = 1;
                row.Cells.Add(cell);
                
            }
            tb_header.Rows.Add(row);
        }
    }
}