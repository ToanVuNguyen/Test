using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using System.Reflection;
using System.Web;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;

namespace Bewise.SharePoint.SPViewPermissionSetting
{
    public class ViewPermissionSelectorMenu : ViewSelectorMenu
    {
        private Dictionary<int, Dictionary<Guid, bool>> roleProperties = null;
        private Dictionary<int, Guid> defaultViews = null;
        private Dictionary<int, bool> defaultActions = null;
        private bool featureEnabled = false;

        protected override void OnLoad(EventArgs e)
        {            
            if (this.Visible && ! ViewPermissionUtil.IsAnAdministrator())
            {
                using (SPSite site = SPContext.Current.Site)
                {
                    SPFeature listDisplaySettingFeature = site.Features[new Guid("88E9E47A-BA92-47ab-A253-8AA472CCC76B")];

                    if ((listDisplaySettingFeature != null) && (listDisplaySettingFeature.Definition.Status == Microsoft.SharePoint.Administration.SPObjectStatus.Online))
                    {
                        featureEnabled = true;
                        roleProperties = new Dictionary<int, Dictionary<Guid, bool>>();
                        defaultViews = new Dictionary<int, Guid>();
                        defaultActions = new Dictionary<int, bool>();

                        if (SPContext.Current.List.ParentWeb.Properties.ContainsKey(String.Format("ViewPermissionXml{0}", SPContext.Current.List.ID.ToString())) || SPContext.Current.List.ParentWeb.Properties.ContainsKey(String.Format("ViewPermission{0}", SPContext.Current.List.ID.ToString())))
                        {
                            using (SPWeb web = SPContext.Current.List.ParentWeb)
                            {
                                //si nouvelle version ou ancienne version
                                if (SPContext.Current.List.ParentWeb.Properties.ContainsKey(String.Format("ViewPermissionXml{0}", SPContext.Current.List.ID.ToString())))
                                    ViewPermissionUtil.ConvertFromXmlString(ref roleProperties, ref defaultViews, ref defaultActions, web.Properties[String.Format("ViewPermissionXml{0}", SPContext.Current.List.ID.ToString())], SPContext.Current.List);
                                else
                                    ViewPermissionUtil.ConvertFromString(ref roleProperties, ref defaultViews, web.Properties[String.Format("ViewPermission{0}", SPContext.Current.List.ID.ToString())], SPContext.Current.List);
                                                                                               
                                if (!Page.IsPostBack)
                                {
                                    string queryStr = "redirect=true";
                                    SPView spView = null;
                                    if (!string.IsNullOrEmpty(Context.Request.QueryString["Paged"]))
                                        queryStr += "&Paged=" + Context.Request.QueryString["Paged"];
                                    if (!string.IsNullOrEmpty(Context.Request.QueryString["View"]))
                                    {
                                        spView = SPContext.Current.List.Views[new Guid(Context.Request.QueryString["View"])];
                                    }
                                    if (spView == null)
                                        spView = GoToDefaultView(defaultViews);
                                    if (!string.IsNullOrEmpty(Context.Request.QueryString["PageFirstRow"]))
                                        queryStr += "&PageFirstRow=" + Context.Request.QueryString["PageFirstRow"];
                                    if (!string.IsNullOrEmpty(Context.Request.QueryString["p_ID"]))
                                        queryStr += "&p_ID=" + Context.Request.QueryString["p_ID"];
                                    if (!string.IsNullOrEmpty(Context.Request.QueryString["FolderCTID"]))
                                        queryStr += "&FolderCTID=" + Context.Request.QueryString["FolderCTID"];
                                    
                                    bool? res = UserCanSeeView(base.RenderContext.ViewContext.View.ID, roleProperties);
                                    if ( ((res.HasValue) && (!res.Value)) ||!ComeFromView())
                                    {
                                        SPUtility.Redirect(spView.ServerRelativeUrl, SPRedirectFlags.Default, HttpContext.Current, queryStr);
                                    }                                    
                                }
                                //System.Diagnostics.EventLog.WriteEntry("Bewise - SPViewPermissionSetting", "URL " + Context.Request.Url, System.Diagnostics.EventLogEntryType.Information);
                               
                            }
                        }
                    }
                }
            }
            else
                base.OnLoad(e);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter output)
        {
            if (this.Visible)
            {
                if (featureEnabled)
                {
                    foreach (Control item in base.MenuTemplateControl.Controls)
                    {
                        try
                        {
                            if ((item is MenuItemTemplate) && (SPContext.Current.List.Views[((MenuItemTemplate)item).Text]) != null)
                            {
                                bool? res = UserCanSeeView(SPContext.Current.List.Views[((MenuItemTemplate)item).Text].ID, roleProperties);
                                if (res.HasValue)
                                    item.Visible = res.Value;

                                string targetUrl = (item as MenuItemTemplate).ClientOnClickNavigateUrl;
                                if (!targetUrl.Contains("?"))
                                    targetUrl += "?redirect=true";
                                else
                                    targetUrl += "&redirect=true";

                                (item as MenuItemTemplate).ClientOnClickNavigateUrl = targetUrl;
                            }

                        }
                        catch (Exception)
                        {
                            if ((item is MenuItemTemplate) && (item.ID == "CreateView" || item.ID == "ModifyView"))
                            {
                                bool? res = UserCanCreateOrModifyView(defaultActions);
                                if (res.HasValue)
                                    item.Visible = res.Value;
                            }
                        }
                    }
                }

                base.Render(output);
            }
            else
                base.Render(output);

        }

        private bool? UserCanCreateOrModifyView(Dictionary<int, bool> defaultActions)
        {
            using (SPWeb webSite = SPContext.Current.Web)
            {
                SPUser user = webSite.CurrentUser;
                SPGroupCollection userGroups = user.Groups;

                if (userGroups.Count > 0)
                {
                    foreach (SPGroup group in userGroups)
                    {
                        if (defaultActions.ContainsKey(group.ID))
                        {
                            return defaultActions[group.ID];
                        }
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        private static bool? UserCanSeeView(Guid viewId, Dictionary<int, Dictionary<Guid, bool>> roleProperties)
        {
            using (SPWeb webSite = SPContext.Current.Web)
            {
                SPUser user = webSite.CurrentUser;
                SPGroupCollection userGroups = user.Groups;

                if (userGroups.Count > 0)
                {
                    foreach (SPGroup group in userGroups)
                    {
                        if (roleProperties.ContainsKey(group.ID))
                        {
                            if (roleProperties[group.ID].ContainsKey(viewId))
                                return roleProperties[group.ID][viewId];
                        }
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        private SPView GoToDefaultView(Dictionary<int, Guid> defaultViews)
        {
            using (SPWeb webSite = SPContext.Current.Web)
            {
                SPUser user = webSite.CurrentUser;
                SPGroupCollection userGroups = user.Groups;

                if (userGroups.Count > 0)
                {
                    foreach (SPGroup group in userGroups)
                    {
                        if (defaultViews.ContainsKey(group.ID))
                        {
                            if (SPContext.Current.List.Views[defaultViews[group.ID]] != null)
                                return SPContext.Current.List.Views[defaultViews[group.ID]];
                        }
                    }

                    return SPContext.Current.List.DefaultView;
                }
                else
                    return SPContext.Current.List.DefaultView;
            }
        }

        private bool ComeFromView()
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["redirect"]))
                return true;
            else
                return false;
        }
    }
}
