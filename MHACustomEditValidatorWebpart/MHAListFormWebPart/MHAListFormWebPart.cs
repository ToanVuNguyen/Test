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
using System.Collections;

namespace CustomEditWebpart
{
    [Guid("3741e8d7-ee61-456b-a262-c956f79395b1")]
    public class MHAListFormWebPart : System.Web.UI.WebControls.WebParts.WebPart
    {
        public MHAListFormWebPart()
        {
        }

        #region Properties
        private string _MHAEscalationYesNo_DateRule = "Escalated To Fannie Mae|Escalated To Freddie Mac-->Escalated To GSE Date;Escalated To MMI Mgmt-->Escalated To MMI Mgmt Date";
        [WebBrowsable(true),
        Personalizable(PersonalizationScope.User),
        WebDescription("yesnoField1a|yesnoField1b-->OutputDatetimeField1;yesnoField2a|yesnoField2b-->OutputDatetimeField2"),
        Category("MHA Escalation"),
        WebDisplayName("MHA Escalation Yes/No->Datetime")]
        public string MHAEscalationYesNoDateRule
        {
            get { return _MHAEscalationYesNo_DateRule; }
            set { _MHAEscalationYesNo_DateRule = value; }
        }

        private string _MHAEscalationChoice_DateRule = "Final Resolution Code-->Final Resolution Date";
        [WebBrowsable(true),
        Personalizable(PersonalizationScope.User),
        WebDescription("choiceField1|choiceField1x-->OutputDatetimeField1;.."),
        Category("MHA Escalation"),
        WebDisplayName("MHA Escalation Choice->Datetime")]
        public string MHAEscalationChoiceDateRule
        {
            get { return _MHAEscalationChoice_DateRule; }
            set { _MHAEscalationChoice_DateRule = value; }
        }

        private string _escalationSecurityGroup = "HPF Group - Escalation Mgmt";
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

        private string _readonlyFields = "Escalated To GSE Date";
        [WebBrowsable(true),
        Personalizable(PersonalizationScope.User),
        WebDescription("Including all readonly fields. Every field is seperated by ';'"),
        Category("MHA Escalation"),
        WebDisplayName("Readonly Fields")]
        public string ReadonlyFields
        {
            get { return _readonlyFields; }
            set { _readonlyFields = value; }
        }
        #endregion

        private ValidationRule[] ValivationYesNoDateRules
        {
            get
            {
                return ParseRuleSet(MHAEscalationYesNoDateRule);
            }
        }

        private ValidationRule[] ValivationChoiceDateRules
        {
            get
            {
                return ParseRuleSet(MHAEscalationChoiceDateRule);
            }
        }

        private ValidationRule[] ParseRuleSet(string ruleName)
        {
            ArrayList output = new ArrayList();
            try
            {
                string[] rules = ruleName.Split(';');

                foreach (string rule in rules)
                {
                    if (string.IsNullOrEmpty(rule)) continue;
                    int index = rule.IndexOf("-->");
                    if (index < 0) continue;

                    ValidationRule valRule = new ValidationRule();
                    valRule.InputFields = rule.Substring(0, index).Split('|');
                    valRule.Outputfield = rule.Substring(index + 3, rule.Length - index - 3);

                    output.Add(valRule);
                }
            }
            catch { }

            return (ValidationRule[])output.ToArray(typeof(ValidationRule));
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
                    #region Design Mode
                    TableRow tr = new TableRow();
                    TableCell c = new TableCell();
                    c.ColumnSpan = 2;
                    c.Text = "MHA Escalation Edit Controller";
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
                    #endregion
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

        private void RegisterYesNoToDatetimeScript(string EscalationYesNoDateInputField, string MHAEscalationYesNoDateOutputField)
        {
            WebControl editCtrl = null;
            WebControl checkBox = null;
            {
                BaseFieldControl f1 = GetFieldControlByName(EscalationYesNoDateInputField);
                BaseFieldControl f2 = GetFieldControlByName(MHAEscalationYesNoDateOutputField);
                if (f1 == null || f2 == null) return;

                checkBox = GetControl<WebControl>(f1);
                editCtrl = GetControl<WebControl>(f2);

                if (editCtrl != null && checkBox != null)
                {
                    string script = String.Format("window[\"EditFormYesNoController_{0}\"] = new EditFormYesNoController('{1}','{2}');",
                        EscalationYesNoDateInputField, checkBox.ClientID, editCtrl.ClientID);

                    Page.ClientScript.RegisterStartupScript(GetType(), "event_" + checkBox.UniqueID, script, true);
                }
            }
        }

        private void RegisterSelectionToDatetimeScript(string MHAEscalationSelectDateInputField, string MHAEscalationSelectDateOutputField)
        {
            WebControl editCtrl2 = null;
            //WebControl radioCtrl = null;                

            BaseFieldControl f1 = GetFieldControlByName(MHAEscalationSelectDateInputField);
            BaseFieldControl f2 = GetFieldControlByName(MHAEscalationSelectDateOutputField);
            //if (f1 != null)
            //    radioCtrl = GetControl<WebControl>(f1);
            if (f2 != null)
                editCtrl2 = GetControl<WebControl>(f2);

            if (editCtrl2 != null && f1 != null)
            {
                for (int index = 0; index < f1.Controls.Count; index++)
                {
                    if (f1.Controls[index] is WebControl)
                    {
                        string script = String.Format("window[\"EditFormSelectionController_{0}\"] = new EditFormSelectionController('{1}','{2}');",
                            MHAEscalationSelectDateInputField, f1.Controls[index].ClientID, editCtrl2.ClientID);

                        Page.ClientScript.RegisterStartupScript(GetType(), "event_" + f1.Controls[index].UniqueID, script, true);
                    }
                }
            }
        }

        public void RegisterClientScript()
        {
            #region javascript block
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
                                "function findLableForControlText(idVal) {" +
                               "    labels = document.getElementsByTagName('label');" +
                               "    for( var i = 0; i < labels.length; i++ ) {"+
                               "        if (labels[i].htmlFor == idVal)"+
                               "            return labels[i].innerText;"+
                               "    }"+
                               "    return '';"+
                               "}"+       
                                "function getCheckedRadioId(radioObj) {" +
                                "	if(!radioObj)" +
                                "		return '';" +
                                "	var radioLength = radioObj.length;" +
                                "	if(radioLength == undefined)" +
                                "		if(radioObj.checked)" +
                                "			return radioObj.id;" +
                                "		else" +
                                "			return '';" +
                                "	for(var i = 0; i < radioLength; i++) {" +
                                "		if(radioObj[i].checked) {" +
                                "			return radioObj[i].id;" +
                                "		}" +
                                "	}" +
                                "	return '';" +
                                "}" +
                                "function EditFormYesNoController(checkBoxId, textBoxId) {" +
                                "    checkBox = document.getElementById(checkBoxId);" +
                                "    attachEventListener(checkBox, 'click', function(e) {" +
                                "        textBox = document.getElementById(textBoxId);" +
                                "        var curDate = new Date();" +
                                "        var stringDate = (curDate.getMonth() + 1) + '/' + curDate.getDate() + '/' + curDate.getFullYear();" +
                                "        if (e.target.checked == true && textBox.value == '')" +
                                "            textBox.value = stringDate;" +
                                "      });" +
                                "}" +
                                "function EditFormSelectionController(radioId, textBoxId) {" +
                                "   var radio = document.getElementById(radioId);" +
                                "   attachEventListener(radio, 'click', function(e) {" +
                                "       id = getCheckedRadioId(radio);"+
                                "       selText = findLableForControlText(id);" + 
                                "       textBox = document.getElementById(textBoxId);" +
                                "       var curDate = new Date();" +
                                "       var stringDate = (curDate.getMonth() + 1) + '/' + curDate.getDate() + '/' + curDate.getFullYear();" +
                                "       if(selText == 'Pending FLUP')" +
                                "           textBox.value = '';" +
                                "       else if (textBox.value =='')" +
                                "           textBox.value = stringDate;" +
                                "   });" +
                                "}" +
                                "function EditFormController3(dropDownId, textBoxId) {" +
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
                                "   ctrl.readonly = true;" +
                                "   attachEventListener(ctrl, 'keypress', function(e1) {" +
                                "       return false;" +
                                "   });" +
                                "   attachEventListener(ctrl, 'keydown', function(e2) {" +
                                "       return false;" +
                                "   });" +
                                "   attachEventListener(ctrl, 'click', function(e3) {" +
                                "       return false;" +
                                "   });" +
                              //"   ctrl.disabled = true;" +
                                "}" +
                                "function clickDatePicker(a, b, c){" +
                                "   var date;" +
                                "	var obja=document.getElementById(a);" +
                                "	var aid;" +
                                "	if (event !=null)" +
                                "		event.cancelBubble=true;" +
                                "	if(a==null && this.Picker !=null)" +
                                "	{" +
                                "		this.Picker.style.display=\"none\";" +
                                "		this.Picker=null;" +
                                "	}" +
                                "	else if (obja !=null)" +
                                "	{" +
                                "		var aelm=document.getElementById(a);" +
                                "		if(aelm !=null && (aelm.isDisabled || obja.readonly==true))" +
                                "			return;" +
                                "		date=getDate(obja, c);" +
                                "		aid=obja.id;" +
                                "		var objDatePickerImage=document.getElementById(aid+g_strDatePickerImageID);" +
                                "		clickDatePickerHelper(aid, aid+g_strDatePickerFrameID, objDatePickerImage, date, b, OnSelectDate, OnPickerFinish);" +
                                "		document.body.onclick=OnPickerFinish;" +
                                "	}" +
                                "}";
            #endregion
            Page.ClientScript.RegisterStartupScript(base.GetType(), "hpf_" + this.UniqueID.GetHashCode().ToString("X"), scriptBlock, true);

            bool enableEditField = CheckEscalationSecurity();
            string[] secureFields = EscalationSecurityFields.Split(';');
            ArrayList readonlyrFields = new ArrayList(ReadonlyFields.Split(';'));

            #region Disable readonly fields
            if (!enableEditField)
                readonlyrFields.AddRange(secureFields);

            foreach (string field in readonlyrFields)
            {
                WebControl disableCtrl = null;
                BaseFieldControl f = GetFieldControlByName(field);
                if (f != null)
                    disableCtrl = GetControl<WebControl>(f);
                if (disableCtrl != null)
                {
                    string script = String.Format("window[\"DisableControl_{0}\"] = new DisableControl('{1}');", field, disableCtrl.ClientID);
                    Page.ClientScript.RegisterStartupScript(GetType(), "even_disable_" + disableCtrl.UniqueID + DateTime.Now.ToBinary().ToString(), script, true);
                }
            }
            #endregion

            foreach (ValidationRule rule in ValivationYesNoDateRules)
            {
                foreach (string InputField in rule.InputFields)
                {
                    if (enableEditField || !IsDisableField(InputField, readonlyrFields))
                        RegisterYesNoToDatetimeScript(InputField, rule.Outputfield);
                }
            }

            foreach (ValidationRule rule in ValivationChoiceDateRules)
            {
                foreach (string InputField in rule.InputFields)
                {
                    if (enableEditField || !IsDisableField(InputField, readonlyrFields))
                        RegisterSelectionToDatetimeScript(InputField, rule.Outputfield);
                }
            }

        }

        private bool IsDisableField(string field, ArrayList fields)
        {
            foreach (string fieldName in fields)
                if (fieldName.ToUpper().Equals(field.ToUpper()))
                    return true;

            return false;
        }

        private bool CheckEscalationSecurity()
        {
            bool result = false;
            SPGroup escalationMngt = null;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                try
                {
                    escalationMngt = SPContext.Current.Web.Groups[EscalationSecurityGroup];

                    result = escalationMngt.ContainsCurrentUser;
                    //if (escalationMngt.Users.GetByID(user.ID) != null)
                    //    result = true;
                }
                catch (Exception ex)
                {
                    if (escalationMngt == null)
                        result = true;
                }
            });
            return result;
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
            try
            {
                foreach (IValidator validator in Page.Validators)
                {
                    if (validator is BaseFieldControl)
                    {
                        BaseFieldControl baseField = (BaseFieldControl)validator;
                        if (baseField.Field == null) continue;

                        String fieldName = baseField.Field.Title;
                        if (fieldName.ToUpper() == fieldNameToSearch.ToUpper())
                        {
                            return baseField;
                        }
                    }
                }
            }
            catch
            {
            }
            return null;
        }
    }

    [Serializable]
    public class ValidationRule
    {
        public string[] InputFields { get; set; }
        public string Outputfield { get; set; }
    }
}

