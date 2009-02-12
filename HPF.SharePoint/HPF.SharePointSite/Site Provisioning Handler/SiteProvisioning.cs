using System;
using System.Runtime.InteropServices;
using System.IO;

using Microsoft.SharePoint;
using HPF.SharePointSite.Common;

namespace HPF.SharePointSite
{
    public partial class SiteProvisioning
    {
        /// <summary>
        ///  Define your own feature activation action code here
        /// </summary>
        public void OnActivated(SPFeatureReceiverProperties properties)
        {            
            SPWeb web = properties.Feature.Parent as SPWeb;
            SPList reportList = web.Lists[DocumentLibraryName.ReportDocumentLibrary];
            string xml = "<Field ID=\"9610DC6F-5631-4fd8-A60A-CA9ABCC5AFE6\" Type=\"Computed\" ReadOnly=\"TRUE\" Name=\"ListItemSelection\" DisplayName=\"Select\" Sortable=\"FALSE\" Filterable=\"FALSE\" EnableLookup=\"FALSE\" SourceID=\"http://schemas.microsoft.com/sharepoint/v3\" StaticName=\"ListItemSelection\"><FieldRefs><FieldRef Name=\"ID\" /></FieldRefs><DisplayPattern><HTML><![CDATA[<input type=\"checkbox\" ]]></HTML><HTML><![CDATA[LItemId=\"]]></HTML><Column Name=\"ID\" HTMLEncode=\"TRUE\" /><HTML><![CDATA[\" onclick=\"DocumentSelectionOnClick(this,']]></HTML><Column Name=\"ID\" HTMLEncode=\"TRUE\" /><HTML><![CDATA[');\"> ]]></HTML></DisplayPattern></Field>";
            string name = reportList.Fields.AddFieldAsXml(xml, true, SPAddFieldOptions.Default);
        }
    }
}
