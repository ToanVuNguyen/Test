using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using System.Diagnostics;
using Microsoft.SharePoint.Utilities;
using System.Xml;

namespace Bewise.SharePoint.SPViewPermissionSetting
{
    public class ViewPermissionPage : LayoutsPageBase
    {
        private string pageRender;
        private SPList currentList;
        private List<SPGroup> groups = new List<SPGroup>();
        private List<SPView> views = new List<SPView>();
        private Dictionary<int, Dictionary<Guid, bool>> roleProperties = null;
        private Dictionary<int, Guid> defaultViews = null;
        private Dictionary<int, bool> defaultActions = null;
        private List<int> hiddenFields = new List<int>();
        private StringBuilder computeFieldsScript = new StringBuilder();

        protected Button OK = new Button();
        protected Button Reset = new Button();
        protected Button Cancel = new Button();

        protected override void OnLoad(EventArgs e)
        {
            this.Title = "View Permission Settings";

            try
            {
                roleProperties = new Dictionary<int, Dictionary<Guid, bool>>();
                defaultViews = new Dictionary<int, Guid>();
                defaultActions = new Dictionary<int, bool>();

                if (this.CurrentList.ParentWeb.Properties.ContainsKey(String.Format("ViewPermissionXml{0}", this.CurrentList.ID.ToString())))
                {                    
                    string xml = this.CurrentList.ParentWeb.Properties[String.Format("ViewPermissionXml{0}", this.CurrentList.ID.ToString())];                    
                    //EventLog.WriteEntry("Bewise - SPViewPermissionSetting", "XML " + xml, EventLogEntryType.Information);
                    ViewPermissionUtil.ConvertFromXmlStringForPage(ref roleProperties, ref defaultViews, ref defaultActions, xml, this.CurrentList);                    
                }
                else if (this.CurrentList.ParentWeb.Properties.ContainsKey(String.Format("ViewPermission{0}", this.CurrentList.ID.ToString())))
                {
                    //EventLog.WriteEntry("Bewise - SPViewPermissionSetting", "text ", EventLogEntryType.Information);
                    ViewPermissionUtil.ConvertFromStringForPage(ref roleProperties, ref defaultViews, this.CurrentList.ParentWeb.Properties[String.Format("ViewPermission{0}", this.CurrentList.ID.ToString())], this.CurrentList);
                }
                else
                {
                    //EventLog.WriteEntry("Bewise - SPViewPermissionSetting", "DEFAULT LIST", EventLogEntryType.Information);
                    // Récupération des groups
                    foreach (SPGroup group in this.CurrentList.ParentWeb.Groups)
                        groups.Add(group);

                    // Récupération des vues
                    foreach (SPView view in this.CurrentList.Views)
                    {
                        if ((!view.Hidden) && (!view.PersonalView))
                            views.Add(view);
                    }

                    foreach (SPGroup group in groups)
                    {
                        if (!ViewPermissionUtil.CheckGroupPermission(this.CurrentList, group.ID)) continue;
                        roleProperties.Add(group.ID, new Dictionary<Guid, bool>());
                        defaultViews.Add(group.ID, this.CurrentList.DefaultView.ID);
                        foreach (SPView view in views)
                            roleProperties[group.ID].Add(view.ID, true);
                    }
                }

                pageRender = this.PrepareRenderPage();
                RegisterScript();
            }
            catch (Exception ex)
            {
                //si une erreur est levée, on propose une page par défaut avec un reset des vues (vidage du property bag du SPWeb)
                //log de l'erreur dans le journal du serveur
                EventLog.WriteEntry("Bewise - SPViewPermissionSetting", string.Format("Error when loading settings for SPViewPermissionSettings on list {0}. Error message:  {1}\n{2}", this.CurrentList.Title, ex.Message, ex.StackTrace), EventLogEntryType.Error);

                //affichage du bouton permettant le reset des paramètres des vues
                pageRender = this.PrepareRenderPageForExeception();
            }

            this.Cancel.PostBackUrl = string.Format("~/_layouts/listedit.aspx?List={0}", this.CurrentList.ID.ToString());
        }

        protected string RenderPage()
        {
            return pageRender;
        }

        protected SPList CurrentList
        {
            get
            {
                if (currentList == null)
                    currentList = SPContext.Current.Web.Lists[new Guid(Request.QueryString["List"])];

                return currentList;
            }
        }

        private string PrepareRenderPage()
        {
            StringBuilder result = new StringBuilder();

            // Table générale
            result.Append("<table style=\"width:100%\" cellpadding=\"0\" cellspacing=\"0\">");

            foreach (int groupId in roleProperties.Keys)
            {
                result.Append("<tr><td colspan=\"2\" class=\"ms-sectionline\" style=\"height:1px;\" ></td></tr>");
                result.Append(string.Format("<tr><td valign=\"top\" class=\"ms-sectionheader\" style=\"width:200px;padding-right:15px;\">{0}</td>", this.CurrentList.ParentWeb.Groups.GetByID(groupId).Name));
                result.Append(string.Format("<td class=\"ms-authoringcontrols\">{0}</td></tr><tr><td></td><td class=\"ms-authoringcontrols\" style=\"height:10px;\"></td></tr>", RenderOptions(groupId)));
            }

            // Fermeture de la table générale
            result.Append("</table>");

            // Copyright
            result.Append(RenderCopyright());
            result.Append("<script language=\"javascript\" type=\"text/javascript\">");
            result.Append("ComputeFields();");
            result.Append("</script>");

            //affichage du bouton OK
            this.OK.Visible = true;
            this.Reset.Visible = true;

            return result.ToString();
        }

        private string PrepareRenderPageForExeception()
        {
            StringBuilder result = new StringBuilder();

            // Table générale
            result.Append("<table style=\"width:100%;\" cellpadding=\"0\" cellspacing=\"0\">");
            result.Append("<tr><td colspan=\"2\" class=\"ms-sectionline\" style=\"height:1px;\" ></td></tr>");
            result.Append("<tr><td valign=\"top\" class=\"ms-sectionheader\" style=\"width:200px;padding-right:15px;\">SPViewPermissionSetting - Error</td>");
            result.Append("<td class=\"ms-authoringcontrols\">The feature SPViewPermissionSetting throw an error.<br/>You can reset all view settings by clicking on the Reset button. This will erase all your view definitions for all groups.<br/><br/><br/>");
            //result.Append("<input class=\"ms-ButtonHeightWidth\" id=\"ctl00_PlaceHolderMain_Reset\" onclick=\"javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(\"ctl00$PlaceHolderMain$Reset\", \"\", false, \"\", \"listedit.aspx?List=57a4d5f9-ba42-4043-a63a-56056e3735cf\", false, false))\" type=\"submit\" value=\"Reset\"/>");
            result.Append("</td></tr><tr><td></td><td class=\"ms-authoringcontrols\" style=\"height:10px;\"></td></tr>");
            // Fermeture de la table générale
            result.Append("</table>");

            // Copyright
            result.Append(RenderCopyright());

            this.OK.Visible = false;
            this.Reset.Visible = true;

            return result.ToString();
        }

        private string RenderOptions(int groupId)
        {
            StringBuilder result = new StringBuilder();

            // Default View
            result.Append("<table style=\"width: 100%\">");
            result.Append("<tr><td style=\"width: 100px;\"  class=\"ms-authoringcontrols\">");
            result.Append("Default view : ");
            result.Append("</td><td class=\"ms-authoringcontrols\">");
            result.Append(RenderDefaultView(groupId));
            result.Append("</td></tr>");
            result.Append("</table>");

            // Available views
            result.Append("<table style=\"width: 100%\">");
            result.Append("<tr><td style=\"width: 100px;vertical-align:top;\" class=\"ms-authoringcontrols\">");
            result.Append("Available views : ");
            result.Append("</td><td class=\"ms-authoringcontrols\">");
            result.Append(RenderAvailableViews(groupId));
            result.Append("</td></tr>");
            result.Append("</table>");

            //available default options
            result.Append("<table style=\"width: 100%\">");
            result.Append("<tr><td style=\"width: 100px;vertical-align:top;\" class=\"ms-authoringcontrols\">");
            result.Append("</td><td class=\"ms-authoringcontrols\">");
            result.Append(RenderAvailableActionsOptions(groupId));
            result.Append("</td></tr>");
            result.Append("</table>");

            return result.ToString();
        }

        private string RenderDefaultView(int groupId)
        {
            StringBuilder result = new StringBuilder();
            List<Guid> availableViews = new List<Guid>();
            Guid defaultView = defaultViews[groupId];

            // Récupération de la liste des vues authorisées pour ce groupe
            foreach (Guid viewId in roleProperties[groupId].Keys)
            {
                if (roleProperties[groupId][viewId])
                    availableViews.Add(viewId);
            }


            result.Append(String.Format("<select id=\"Option{0}\" runat=\"server\" style=\"width: 150px;\" onchange=\"javascript:ComputeHidden('{0}');\">", groupId));
            foreach (Guid viewId in availableViews)
            {
                if (this.CurrentList.Views[viewId] != null)
                {
                    string viewName = this.CurrentList.Views[viewId].Title;
                    result.Append(String.Format("<option {0}value=\"{1}\">{2}</option>", viewId.Equals(defaultView) ? "selected=\"selected\" " : "", viewId, viewName));
                }
            }

            result.Append("</select>");
            result.Append("</td></tr>");

            return result.ToString();
        }

        private string RenderAvailableViews(int groupId)
        {
            StringBuilder result = new StringBuilder();

            result.Append(string.Format("<div id=\"Div{0}\">", groupId));
            // Récupération de la liste des vues
            foreach (SPView view in this.CurrentList.Views)
            {
                if ((!view.Hidden) && (!view.PersonalView))
                {
                    if ((roleProperties[groupId].ContainsKey(view.ID)) && (roleProperties[groupId][view.ID] == true))
                        result.Append(string.Format("<input id=\"Chk{0}{1}\" title=\"{2}\" type=\"checkbox\" value=\"{1}\" onclick=\"javascript:OptionChange('{0}','chk{0}{1}');\" checked=\"checked\"/> {2}<br/>", groupId, view.ID, view.Title));
                    else
                        result.Append(string.Format("<input id=\"Chk{0}{1}\" title=\"{2}\" type=\"checkbox\" value=\"{1}\" onclick=\"javascript:OptionChange('{0}','chk{0}{1}');\" /> {2}<br/>", groupId, view.ID, view.Title));
                }
            }

            result.Append("</div>");
            result.Append("</td></tr>");
            UpdateGlobalScript(groupId);

            return result.ToString();
        }
       
        private string RenderAvailableActionsOptions(int groupId)
        {
            StringBuilder result = new StringBuilder();
            //construction du div d'affichage
            result.Append(string.Format("<div id=\"DivOptions{0}\">", groupId));

            foreach (SPRoleAssignment roleAssign in CurrentList.RoleAssignments)
            {
                SPPrincipal principal = roleAssign.Member;
                try
                {
                    if (principal.GetType() == typeof(SPGroup))
                    {
                        SPGroup group = (SPGroup)principal;
                        if (group.ID == groupId)
                        {
                            //c'est le groupe courant, on cherche les droit pour afficher la checkbox (cochée ou pas)
                            foreach (SPRoleDefinition roleDef in roleAssign.RoleDefinitionBindings)
                            {
                                if (((long)(roleDef.BasePermissions & SPBasePermissions.ManageLists)) != 0 || ((long)(roleDef.BasePermissions & SPBasePermissions.EditListItems)) != 0)
                                {
                                    //test si on doit cocher la checkbox ou pas.
                                    if (defaultActions != null && defaultActions.ContainsKey(groupId))
                                    {
                                        if (defaultActions[groupId] == true)
                                            result.Append(string.Format("<input id=\"ChkAction{0}\" title=\"{1}\" type=\"checkbox\" value=\"{1}\" onclick=\"javascript:OptionActionChange('{0}');\" checked=\"checked\"/> {1}<br/>", groupId, "Allow create or edit Views"));
                                        else
                                            result.Append(string.Format("<input id=\"ChkAction{0}\" title=\"{1}\" type=\"checkbox\" value=\"{1}\" onclick=\"javascript:OptionActionChange('{0}');\" /> {1}<br/>", groupId, "Allow create or edit Views"));
                                    }
                                    else
                                        result.Append(string.Format("<input id=\"ChkAction{0}\" title=\"{1}\" type=\"checkbox\" value=\"{1}\" onclick=\"javascript:OptionActionChange('{0}');\" checked=\"checked\" /> {1}<br/>", groupId, "Allow create or edit Views"));
                                }
                                else
                                {
                                    //on affiche une check box grisée
                                    result.Append(string.Format("<input id=\"ChkAction{0}\" title=\"{1}\" type=\"checkbox\" value=\"{1}\" disabled=\"true\" /> {1}<br/>", groupId, "Allow create or edit Views"));
                                }
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Impossible de charger les permissions de création/modification des vues : {0}", ex.Message));
                }
            }

            result.Append("</div>");
            result.Append("</td></tr>");

            return result.ToString();

        }

        private void UpdateGlobalScript(int groupId)
        {
            if (!hiddenFields.Contains(groupId))
            {
                hiddenFields.Add(groupId);
                computeFieldsScript.Append(String.Format("ComputeHidden(\"{0}\");", groupId));
                computeFieldsScript.Append(string.Format("OptionActionChange(\"{0}\");", groupId));
            }

            this.ClientScript.RegisterHiddenField(String.Format("Hidden{0}", groupId), "");
            this.ClientScript.RegisterHiddenField(String.Format("HiddenAction{0}", groupId), "");
        }

        protected void SaveCustomPermission(object sender, EventArgs e)
        {
            //nouvelle sauvegarde des paramètres de vues
            XmlDocument doc = new XmlDocument();

            doc.AppendChild(doc.CreateElement("ViewPermissionSetting"));
            XmlElement rootElement = doc.DocumentElement;

            string valueGlobalXml = string.Empty;

            foreach (int groupId in hiddenFields)
            {
                string value = HttpContext.Current.Request.Params[string.Format("Hidden{0}", groupId)];
                string valueActions = HttpContext.Current.Request.Params[string.Format("HiddenAction{0}", groupId)];
                //EventLog.WriteEntry("Bewise - SPViewPermissionSetting", "SAVE- group id: " + groupId + "-value:" + value + "--valueaction:" + valueActions, EventLogEntryType.Information);
                if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(valueActions))
                {
                    //création du groupe
                    XmlElement group = doc.CreateElement("Group");
                    group.SetAttribute("ID", groupId.ToString());

                    string[] infos = value.Split('#');
                    if (infos.Length > 1)
                    {
                        //vue par défaut
                        string defaultview = infos[1];

                        //récupération des vues pour le groupe
                        string[] views = infos[2].Split(';');
                        for (int i = 0; i < views.Length - 1; i++)
                        {
                            //création de la vue
                            XmlElement view = doc.CreateElement("View");
                            view.SetAttribute("ID", views[i]);
                            if (views[i] == defaultview)
                                view.SetAttribute("defaultView", "True");
                            else
                                view.SetAttribute("defaultView", "False");
                            group.AppendChild(view);
                        }                        
                    }

                    //récupération des actions par défaut du groupe
                    XmlElement action = doc.CreateElement("DefaultActions");
                    action.SetAttribute("display", (valueActions == "true" ? "true" : "false"));
                    group.AppendChild(action);

                    //rattachement du groupe dans l'arbre xml
                    rootElement.AppendChild(group);
                }
            }

            if (!this.CurrentList.ParentWeb.Properties.ContainsKey(String.Format("ViewPermissionXml{0}", this.CurrentList.ID.ToString())))
                this.CurrentList.ParentWeb.Properties.Add(String.Format("ViewPermissionXml{0}", this.CurrentList.ID.ToString()), doc.InnerXml);
            else
                this.CurrentList.ParentWeb.Properties[String.Format("ViewPermissionXml{0}", this.CurrentList.ID.ToString())] = doc.InnerXml;

            //EventLog.WriteEntry("Bewise - SPViewPermissionSetting", "SAVE " + doc.InnerXml, EventLogEntryType.Information);
            //suppression des clés de l'ancienne version
            if (this.CurrentList.ParentWeb.Properties.ContainsKey(String.Format("ViewPermission{0}", this.CurrentList.ID.ToString())))
                this.CurrentList.ParentWeb.Properties.Remove(String.Format("ViewPermission{0}", this.CurrentList.ID.ToString()));

            this.CurrentList.ParentWeb.Properties.Update();

            Server.Transfer(string.Format("~/_layouts/listedit.aspx?List={0}", this.CurrentList.ID.ToString()), true);
        }

        /// <summary>
        /// Reset du propertybag du SPWeb.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ResetCustomPermission(object sender, EventArgs e)
        {
            Web.AllowUnsafeUpdates = true;
            //suppression des clés associées a la liste (ancienne et nouvelle version)            
            if (this.CurrentList.ParentWeb.Properties.ContainsKey(String.Format("ViewPermission{0}", this.CurrentList.ID.ToString())))
            {
                //this.CurrentList.ParentWeb.Properties.Remove(String.Format("ViewPermissionXml{0}", this.CurrentList.ID.ToString()));
                this.CurrentList.ParentWeb.Properties[String.Format("ViewPermission{0}", this.CurrentList.ID.ToString())] = null;
                this.CurrentList.ParentWeb.Properties.Update();
                //EventLog.WriteEntry("Bewise - SPViewPermissionSetting", "reset...1" + this.CurrentList.ID.ToString(), EventLogEntryType.Information);
            }
            if (this.CurrentList.ParentWeb.Properties.ContainsKey(String.Format("ViewPermissionXml{0}", this.CurrentList.ID.ToString())))
            {
                //this.CurrentList.ParentWeb.Properties.Remove(String.Format("ViewPermissionXml{0}", this.CurrentList.ID.ToString()));
                this.CurrentList.ParentWeb.Properties[String.Format("ViewPermissionXml{0}", this.CurrentList.ID.ToString())] = null;
                this.CurrentList.ParentWeb.Properties.Update();
                //EventLog.WriteEntry("Bewise - SPViewPermissionSetting", "reset...2" + this.CurrentList.ID.ToString(), EventLogEntryType.Information);
            }
            
            Web.AllowUnsafeUpdates = false;            
            //redirection vers la page courante
            SPUtility.Redirect(string.Format("~/_layouts/ViewPermissionSetting.aspx?List={0}", this.CurrentList.ID.ToString()), SPRedirectFlags.Default, HttpContext.Current);
        }

        private string RenderCopyright()
        {
            StringBuilder result = new StringBuilder();

            result.Append("<div style=\"float:left;margin-top:10px;\">");
            result.Append("Powered by Laurent Cotton (Aka Suchii)<br />Bewise</div>");

            return result.ToString();
        }

        private void RegisterScript()
        {
            computeFieldsScript.Insert(0, "function ComputeFields(){");
            computeFieldsScript.Append("}");
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ComputeFields", computeFieldsScript.ToString(), true);

            string OptionChangeScript = @"function OptionChange(id,sender)
                                    {
                                        if(CountChecked(id) == 0)
                                            document.getElementById(sender).checked = true;
                                        else
                                            OptionBind(id);
                                    }";
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OptionChange", OptionChangeScript, true);

            string CountCheckedScript = @"function CountChecked(id) 
                                        {
                                            var result = 0;
                                            var panel = document.getElementById('Div' + id);
                                            
                                            for(index=0;index < panel.childNodes.length;index++) 
                                            {
                                                var ctrl = panel.childNodes.item(index);
                                                if((ctrl.name != null) && (ctrl.checked))
                                                    result++;
                                            }
                                            
                                            return result;
                                        }";
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CountChecked", CountCheckedScript, true);

            string OptionBindScript = @"function OptionBind(id)
                                        {
                                            var selectCtrl = document.getElementById('Option' + id);
                                            var panel = document.getElementById('Div' + id);
                                            var i = 0;
                                            var lenght = CountChecked(id);
                                            
                                            selectCtrl.options.length = lenght;                                           

                                            for(index=0;index < panel.childNodes.length;index++) 
                                            {
                                                var ctrl = panel.childNodes.item(index);
                                                if((ctrl.name != null) && (ctrl.checked))
                                                {
                                                    selectCtrl.options[i].value = ctrl.value;
                                                    selectCtrl.options[i].text = ctrl.title;
                                                    i++;
                                                }
                                            }
                                            
                                            selectCtrl.selectedIndex = 0;
                                            ComputeHidden(id);
                                        }";
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OptionBind", OptionBindScript, true);

            string OptionActionScript = @"function OptionActionChange(id)
                                          {
//alert('action');
                                                var selectCtrl = document.getElementById('ChkAction' + id);
                                                var hidden = document.getElementById('HiddenAction' + id);

                                                if(selectCtrl.checked)
                                                {
                                                    hidden.value = 'true';                                                   
                                                }
                                                else
                                                {
                                                    hidden.value = 'false'; 
                                                }
                                          }";

            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OptionActionChange", OptionActionScript, true);


            string ComputeHiddenScript = @"function ComputeHidden(id)
                                        {
//alert('view' + id);
                                            var selectCtrl = document.getElementById('Option' + id);
                                            var panel = document.getElementById('Div' + id);
                                            var hidden = document.getElementById('Hidden' + id);
                                            var groupId = id;
                                            var defaultView = selectCtrl.options[selectCtrl.selectedIndex].value
                                            var views = '';
                                            
                                            for(index=0;index < panel.childNodes.length;index++) 
                                            {
                                                var ctrl = panel.childNodes.item(index);
                                                if((ctrl.name != null) && (ctrl.checked))
                                                {
                                                    views = views + ctrl.value + ';'
                                                }
                                            }

                                            hidden.value = groupId + '#' + defaultView + '#' + views;
                                        }";
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ComputeHidden", ComputeHiddenScript, true);
        }
    }
}
