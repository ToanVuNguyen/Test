using System;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Serialization;

using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;
using System.ComponentModel;

namespace CustomEditWebpart
{
    [Guid("3741e8d7-ee61-456b-a262-c956f79395b1")]
    public class MHAListFormWebPart : System.Web.UI.WebControls.WebParts.WebPart
    {        
        public MHAListFormWebPart()
        {                                                                        
        }

        //private WebControl CreateControl(SPField field)
        //{
        //    if (field is SPFieldText)
        //    {
        //        //TextBox txt = new TextBox();
        //        //return txt;
        //    }
        //    else if (field is SPFieldMultiChoice)//|| field is SPFieldMultiChoiceValue)
        //    {
        //        SPFieldMultiChoice choice = field as SPFieldMultiChoice;
        //        DropDownList lst = new DropDownList();
        //        foreach (string val in choice.Choices)
        //            lst.Items.Add(val);

        //        return lst;
        //    }
        //    else if (field is SPFieldDateTime)
        //    {
        //        SPFieldDateTime date = field as SPFieldDateTime;
        //        DateTimeControl dCtrl = new DateTimeControl();
        //        dCtrl.TimeOnly = true;
        //        dCtrl.SelectedDate = DateTime.Now;
        //        dCtrl.DateOnly = true;
        //        Panel pnl = new Panel();
        //        pnl.Controls.Add(dCtrl);
        //        return pnl;
        //    }
        //    return null;
        //}            

        private string _MHAEscalationYesNo_DateInputField = "Escalated To Fannie Mae";
        [WebBrowsable(true),
        Personalizable(PersonalizationScope.User),
        WebDescription("MHA Escalation Input Field name for Yes/No and Datetime edit rule"),
        Category("MHA Escalation"),
        WebDisplayName("MHA Escalation [Input Field Name] Yes/No->Datetime")]
        public string MHAEscalationYesNoDateInputField
        {
            get { return _MHAEscalationYesNo_DateInputField; }
            set { _MHAEscalationYesNo_DateInputField = value; }
        }

        private string _MHAEscalationYesNo_DateOutputField = "Escalated To GSE Date";
        [WebBrowsable(true),
        Personalizable(PersonalizationScope.User),
        WebDescription("MHA Escalation Output field name for Yes/No and Datetime edit rule"),
        Category("MHA Escalation"),
        WebDisplayName("MHA Escalation [Output Field Name] for Yes/No->Datetime")]
        public string MHAEscalationYesNoDateOutputField
        {
            get { return _MHAEscalationYesNo_DateOutputField; }
            set { _MHAEscalationYesNo_DateOutputField = value; }
        }

        private string _MHAEscalationSelect_DateInputField = "Resolution Code";
        [WebBrowsable(true),
        Personalizable(PersonalizationScope.User),
        WebDescription("MHA Escalation Input Field Name Choice and Datetime edit rule"),
        Category("MHA Escalation"),
        WebDisplayName("MHA Escalation [Input Field Name] Choice->Datetime")]
        public string MHAEscalationSelectDateInputField
        {
            get { return _MHAEscalationSelect_DateInputField; }
            set { _MHAEscalationSelect_DateInputField = value; }
        }

        private string _MHAEscalationSelect_DateOutputField = "Final Resolution Date";
        [WebBrowsable(true),
        Personalizable(PersonalizationScope.User),
        WebDescription("MHA Escalation Output Field Name Choice and Datetime edit rule"),
        Category("MHA Escalation"),
        WebDisplayName("MHA Escalation [Output Field Name] Choice->Datetime")]
        public string MHAEscalationSelectDateOutputField
        {
            get { return _MHAEscalationSelect_DateOutputField; }
            set { _MHAEscalationSelect_DateOutputField = value; }
        }

        private string _escalationSecurityGroup = "Escalation Mngt";
        [WebBrowsable(true),
        Personalizable(PersonalizationScope.User),
        WebDescription("Escalation Manager Group"),
        Category("MHA Escalation"),
        WebDisplayName("Escalation Manager Group")]
        public string EscalationSecurityGroup
        {
            get { return _escalationSecurityGroup; }
            set { _escalationSecurityGroup = value; }
        }

        private string _escalationSecurityFields = "Escalated To Fannie Mae;Escalated To Freddie Mac";
        [WebBrowsable(true),
        Personalizable(PersonalizationScope.User),
        WebDescription("Escalation Manager Fields. Only users in group [Escalation Mngt] can edit them. User ';' to seperate fields"),
        Category("MHA Escalation"),
        WebDisplayName("Escalation Manager Fields")]
        public string EscalationSecurityFields
        {
            get { return _escalationSecurityFields; }
            set { _escalationSecurityFields = value; }
        }

        protected override void CreateChildControls()
        {
            try
            {                                
                Table tbl = new Table();
                base.CreateChildControls();
                
                this.Controls.Add(tbl);
                if (DesignMode)
                {
                    TableRow tr = new TableRow();
                    TableCell c = new TableCell();
                    c.ColumnSpan = 2;
                    c.Text ="MHA Escalation Edit Controller";
                    tr.Cells.Add(c);
                    tbl.Rows.Add(tr);
                    TableRow tr1 = new TableRow();                    
                    TableCell tc11 = new TableCell();
                    tc11.Text = "Field Input";
                    tr1.Cells.Add(tc11);
                    TableCell tc12 = new TableCell();
                    DropDownList drpIn = new DropDownList();
                    drpIn.Width = 150;
                    tc12.Controls.Add(drpIn);
                    tr1.Cells.Add(tc12);
                    tbl.Rows.Add(tr1);

                    TableRow tr2 = new TableRow();
                    TableCell tc21 = new TableCell();
                    tc21.Text = "Field Output";
                    tr2.Cells.Add(tc21);
                    TableCell tc22 = new TableCell();
                    TextBox txt = new TextBox();
                    tc22.Controls.Add(txt);
                    tr2.Cells.Add(tc22);
                    tbl.Rows.Add(tr2);
                    return;
                }
                                               
            }
            catch (Exception ex)
            {
                Label error = new Label();
                error.Text = "Error: " + ex.Message + "\\Track: " + ex.StackTrace;
                this.Controls.Add(error);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            EnsureChildControls();
            base.Render(writer);
            if (!DesignMode)
                RegisterClientScript();
        }

        public void RegisterClientScript()
        {
            string scriptBlock =
                                "function attachEventListener(el, sEvent, func, bCapture) {" +
                                "    var newFunc = function(e) {" +
                                "           e = (e) ? e : window.event;" +
                                "           if (!e.target) e.target = e.srcElement;" +
                                "           if (!e.stopPropagation) e.stopPropagation = function() { this.cancelBubble = true; };" +
                                "               b = func(e);" +
                                "           if (b === false && e.preventDefault) {" +
                                "               e.preventDefault();" +
                                "           }" +
                                "           return b;" +
                                "       };" +
                                "   if (el) {" +
                                "       if (el.addEventListener) {" +
                                "           el.addEventListener(sEvent, newFunc, bCapture);" +
                                "       } else if (el.attachEvent) {" +
                                "               el.attachEvent('on' + sEvent, newFunc);" +
                                "       } else {" +
                                "               alert('cannot addEventListener');" +
                                "       }" +
                                "   }" +
                                "}" +
                                "function EditFormController(checkBoxId, textBoxId) {" +
                                "    checkBox = document.getElementById(checkBoxId);" +
                                "    attachEventListener(checkBox, 'click', function(e) {" +
                                "        textBox = document.getElementById(textBoxId);" +
                                "        var curDate = new Date();" +
                                "        var stringDate = (curDate.getMonth() + 1) + '/' + curDate.getDate() + '/' + curDate.getFullYear();" +
                                "        if (checkBox.checked == true && textBox.value == '')" +
                                "            textBox.value = stringDate;" +
                                "        else if (checkBox.checked == false && textBox.value != '')" +
                                "            textBox.value = '';" +
                                "      });" +
                                "}" +
                                "function EditFormController2(dropDownId, textBoxId) {" +
                                "   listBox = document.getElementById(dropDownId);" +
                                "   attachEventListener(listBox, 'change', function(e) {" +
                                "    if (listBox.selectedIndex < 0) {                " +
                                "        return;" +
                                "    }" +
                                "    selValue = listBox.options[listBox.selectedIndex];" +
                                "    textBox = document.getElementById(textBoxId);" +
                                "    var curDate = new Date();" +
                                "    var stringDate = (curDate.getMonth() + 1) + '/' + curDate.getDate() + '/' + curDate.getFullYear();" +
                                "    if (selValue.value != '' && textBox.value =='')" +
                                "        textBox.value = stringDate;" +
                                "    else if (selValue.value == '' && textBox.value != '')" +
                                "        textBox.value = '';" +
                                "   });" +
                                "}" +
                                "function DisableControl(controlId){" +
                                "   ctrl = document.getElementById(controlId);" +
                                "   attachEventListener(ctrl, 'keypress', function(e) {" +                                
                                "       return false;" +
                                "   });" +
                                "   attachEventListener(ctrl, 'keydown', function(e) {" +
                                "       return false;" +
                                "   });" +
                                "   attachEventListener(ctrl, 'click', function(e) {" +                                
                                "       return false;" +
                                "   });" +                                
                                "}";
            //ListFieldIterator list = FormComponent1.Controls[0].FindControl("ListFieldIterator1") as ListFieldIterator;            
            Page.ClientScript.RegisterStartupScript(base.GetType(), "hpf_" + this.UniqueID.GetHashCode().ToString("X"), scriptBlock, true);

            bool enableEditField = CheckEscalationSecurity(SPContext.Current.Web.CurrentUser);
            string[] fields = EscalationSecurityFields.Split(';');
            if (!enableEditField)
            {                
                foreach (string field in fields)
                {
                    WebControl disableCtrl = null;
                    BaseFieldControl f = GetFieldControlByName(field);
                    if (f != null)
                        disableCtrl = GetControl<WebControl>(f);
                    if (disableCtrl != null)
                    {
                        string script = String.Format("window[\"DisableControl1\"] = new DisableControl('{0}');", disableCtrl.ClientID);
                        Page.ClientScript.RegisterStartupScript(GetType(), "even_disable_" + disableCtrl.UniqueID + DateTime.Now.ToBinary().ToString(), script, true);
                    }
                }
            }

            if (enableEditField || !IsDisableField(MHAEscalationYesNoDateInputField, fields))
            {
                WebControl editCtrl = null;
                WebControl checkBox = null;
                //foreach (MHAEscalationValidator val in MHAEscalationYesNo_DateValidator)
                {
                    BaseFieldControl f1 = GetFieldControlByName(MHAEscalationYesNoDateInputField);
                    BaseFieldControl f2 = GetFieldControlByName(MHAEscalationYesNoDateOutputField);
                    if (f1 != null)
                        checkBox = GetControl<WebControl>(f1);
                    if (f2 != null)
                        editCtrl = GetControl<WebControl>(f2);

                    if (editCtrl != null && checkBox != null)
                    {
                        string script = String.Format("window[\"EditFormController\"] = new EditFormController('{0}','{1}');",
                            checkBox.ClientID, editCtrl.ClientID);

                        Page.ClientScript.RegisterStartupScript(GetType(), "even_" + editCtrl.UniqueID + DateTime.Now.ToBinary().ToString(), script, true);
                    }
                }
            }
            if (enableEditField || !IsDisableField(MHAEscalationSelectDateInputField, fields))
            {
                WebControl editCtrl2 = null;
                WebControl listBox = null;
                //foreach (MHAEscalationValidator val in MHAEscalationSelect_DateValidator)
                {
                    BaseFieldControl f1 = GetFieldControlByName(MHAEscalationSelectDateInputField);
                    BaseFieldControl f2 = GetFieldControlByName(MHAEscalationSelectDateOutputField);
                    if (f1 != null)
                        listBox = GetControl<WebControl>(f1);
                    if (f2 != null)
                        editCtrl2 = GetControl<WebControl>(f2);

                    if (editCtrl2 != null && listBox != null)
                    {
                        string script = String.Format("window[\"EditFormController2\"] = new EditFormController2('{0}','{1}');",
                            listBox.ClientID, editCtrl2.ClientID);

                        Page.ClientScript.RegisterStartupScript(GetType(), "even_" + editCtrl2.UniqueID + DateTime.Now.ToBinary().ToString(), script, true);
                    }
                }
            }
        }

        private bool IsDisableField(string field, string[] fields)
        {
            foreach (string fieldName in fields)
                if (fieldName.Equals(field))
                    return true;

            return false;
        }

        private bool CheckEscalationSecurity(SPUser user)
        {
            SPGroup escalationMngt = null;
            try
            {
                escalationMngt = SPContext.Current.Web.Groups[EscalationSecurityGroup];
                if (escalationMngt.Users.GetByID(user.ID) != null)
                    return true;
            }
            catch
            {
                if (escalationMngt == null)
                    return true;
            }

            return false;
        }
        public static T GetControl<T>(Control ctrl) where T : Control
        {
            if (ctrl is T)
                return (T)ctrl;
            foreach (Control subCtrl in ctrl.Controls)
            {
                Control result = GetControl<T>(subCtrl);
                if (result != null)
                    return (T)result;
            }
            return null;
        }

        private BaseFieldControl GetFieldControlByName(String fieldNameToSearch)
        {
            //ListFieldIterator list = FormComponent1.Controls[0].FindControl("ListFieldIterator1") as ListFieldIterator;
            //String iteratorId = list.ClientID;
            foreach (IValidator validator in Page.Validators)
            {
                if (validator is BaseFieldControl)
                {
                    BaseFieldControl baseField = (BaseFieldControl)validator;
                    if (baseField.Field == null) continue;

                    String fieldName = baseField.Field.Title;
                    if (fieldName == fieldNameToSearch)
                    {
                        return baseField;
                    }
                }
            }
            return null;
        }
    }  
}

