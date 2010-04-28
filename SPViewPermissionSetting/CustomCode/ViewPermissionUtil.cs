using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using System.Xml;
using System.Xml.XPath;
using System.Security.Principal;

namespace Bewise.SharePoint.SPViewPermissionSetting
{
    public class ViewPermissionUtil
    {
        public static void ConvertFromString(ref Dictionary<int, Dictionary<Guid, bool>> roleProperties, ref Dictionary<int, Guid> defaultViews, string value, SPList currentList)
        {
            string[] groups = value.Split("|".ToCharArray());
            Dictionary<int, string> groupValues = new Dictionary<int, string>();

            foreach (string group in groups)
            {
                if (!string.IsNullOrEmpty(group))
                {
                    string[] values = group.Split("#".ToCharArray());
                    int groupId = int.Parse(values[0]);
                    groupValues.Add(groupId, group);
                }
            }

            foreach (SPGroup group in currentList.ParentWeb.Groups)
            {             
                roleProperties.Add(group.ID, new Dictionary<Guid, bool>());
                Guid defaultViewId = GetDefautView(groupValues, group.ID);

                SetDefaultVue(defaultViewId, currentList.DefaultView.ID, group.ID, currentList, ref defaultViews);

                foreach (SPView view in currentList.Views)
                {
                    if ((!view.Hidden) && (!view.PersonalView))
                        roleProperties[group.ID].Add(view.ID, IsViewAllowed(groupValues, group.ID, view.ID));
                    else
                    {
                        if (view.PersonalView)
                            roleProperties[group.ID].Add(view.ID, true);
                    }
                }
            }
        }

        public static void ConvertFromXmlString(ref Dictionary<int, Dictionary<Guid, bool>> roleProperties, ref Dictionary<int, Guid> defaultViews, ref Dictionary<int, bool> defaultActions, string value, SPList currentList)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(value);

                    foreach (SPGroup group in currentList.ParentWeb.Groups)
                    {                        
                        roleProperties.Add(group.ID, new Dictionary<Guid, bool>());

                        //récupération du groupe dans le xml
                        XmlNode groupNode = doc.SelectSingleNode(string.Format("ViewPermissionSetting/Group[@ID=\"{0}\"]", group.ID));
                        if (groupNode != null)
                        {
                            //récupération de la vue par défaut
                            XmlNode viewNode = groupNode.SelectSingleNode(string.Format("View[@defaultView=\"{0}\"]", "True"));                            
                            if (viewNode != null)
                            {
                                Guid defaultViewId = new Guid(viewNode.Attributes["ID"].Value);
                                SetDefaultVue(defaultViewId, currentList.DefaultView.ID, group.ID, currentList, ref defaultViews);
                            }
                            
                            foreach (SPView view in currentList.Views)
                            {
                                if ((!view.Hidden) && (!view.PersonalView))
                                {
                                    //vérification que la vue soit autorisée
                                    XmlNode currentView = groupNode.SelectSingleNode(string.Format("View[@ID=\"{0}\"]", view.ID));
                                    if (currentView != null)
                                        roleProperties[group.ID].Add(view.ID, true);
                                    else
                                        roleProperties[group.ID].Add(view.ID, false);
                                }
                                else
                                {
                                    if(view.PersonalView)
                                        roleProperties[group.ID].Add(view.ID, true);
                                }
                            }
                            //récupération des actions par défaut
                            XmlNode defaultActionsNode = groupNode.SelectSingleNode("DefaultActions");
                            if (defaultActionsNode != null)
                            {
                                defaultActions.Add(group.ID, Convert.ToBoolean(defaultActionsNode.Attributes["display"].Value));
                            } 
                        }
                        else
                        {
                            defaultViews.Add(group.ID, currentList.DefaultView.ID);
                            foreach (SPView view in currentList.Views)
                            {
                                if ((!view.Hidden) && (!view.PersonalView))
                                    roleProperties[group.ID].Add(view.ID, true);
                            }
                        }                        
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Erreur lors du chargement des permissions : {0}", ex.Message));
                }
            }
        }

        private static void SetDefaultVue(Guid defaultUserView, Guid listDefaultVue, int groupId, SPList currentList, ref Dictionary<int, Guid> defaultViews)
        {
            try
            {
                if ((defaultUserView != Guid.Empty) && (currentList.Views[defaultUserView] != null))
                    defaultViews.Add(groupId, defaultUserView);
                else
                    defaultViews.Add(groupId, listDefaultVue);
            }
            catch (Exception)
            {
                defaultViews.Add(groupId, listDefaultVue);
            }
        }

        public static void ConvertFromStringForPage(ref Dictionary<int, Dictionary<Guid, bool>> roleProperties, ref Dictionary<int, Guid> defaultViews, string value, SPList currentList)
        {
            string[] groups = value.Split("|".ToCharArray());
            Dictionary<int, string> groupValues = new Dictionary<int, string>();

            foreach (string group in groups)
            {
                if (!string.IsNullOrEmpty(group))
                {
                    string[] values = group.Split("#".ToCharArray());
                    int groupId = int.Parse(values[0]);
                    groupValues.Add(groupId, group);
                }
            }

            foreach (SPGroup group in currentList.ParentWeb.Groups)
            {
                if (!CheckGroupPermission(currentList, group.ID)) continue;

                roleProperties.Add(group.ID, new Dictionary<Guid, bool>());
                Guid defaultViewId = GetDefautView(groupValues, group.ID);

                SetDefaultVue(defaultViewId, currentList.DefaultView.ID, group.ID, currentList, ref defaultViews);

                foreach (SPView view in currentList.Views)
                {
                    if ((!view.Hidden) && (!view.PersonalView))
                        roleProperties[group.ID].Add(view.ID, IsViewAllowed(groupValues, group.ID, view.ID));
                }

            }
        }

        public static bool CheckGroupPermission(SPList list, int groupId)
        {
            foreach (SPRoleAssignment roleAssign in list.RoleAssignments)
            {
                SPPrincipal principal = roleAssign.Member;
                if (principal.GetType() == typeof(SPGroup))
                {
                    SPGroup group = (SPGroup)principal;
                    if (group.ID == groupId)
                        return true;
                }
            }
            return false;
        }

        public static void ConvertFromXmlStringForPage(ref Dictionary<int, Dictionary<Guid, bool>> roleProperties, ref Dictionary<int, Guid> defaultViews, ref Dictionary<int, bool> defaultActions, string value, SPList currentList)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(value);
                    
                    foreach (SPGroup group in currentList.ParentWeb.Groups)
                    {
                        if (!CheckGroupPermission(currentList, group.ID)) continue;
                        roleProperties.Add(group.ID, new Dictionary<Guid, bool>());

                        //récupération du groupe dans le xml
                        XmlNode groupNode = doc.SelectSingleNode(string.Format("ViewPermissionSetting/Group[@ID=\"{0}\"]", group.ID));
                        if (groupNode != null)
                        {
                            //récupération de la vue par défaut
                            XmlNode viewNode = groupNode.SelectSingleNode(string.Format("View[@defaultView=\"{0}\"]", "True"));
                            if (viewNode != null)
                            {
                                Guid defaultViewId = new Guid(viewNode.Attributes["ID"].Value);
                                SetDefaultVue(defaultViewId, currentList.DefaultView.ID, group.ID, currentList, ref defaultViews);
                            }
                            else
                            {
                                defaultViews.Add(group.ID, currentList.DefaultView.ID);
                            }

                            foreach (SPView view in currentList.Views)
                            {
                                if ((!view.Hidden) && (!view.PersonalView))
                                {
                                    //vérification que la vue soit autorisée
                                    XmlNode currentView = groupNode.SelectSingleNode(string.Format("View[@ID=\"{0}\"]", view.ID));
                                    if (currentView != null)
                                        roleProperties[group.ID].Add(view.ID, true);
                                    else
                                        roleProperties[group.ID].Add(view.ID, false);
                                }
                            }
                            //récupération des actions par défaut
                            XmlNode defaultActionsNode = groupNode.SelectSingleNode("DefaultActions");
                            if (defaultActionsNode != null)
                            {
                                defaultActions.Add(group.ID, Convert.ToBoolean(defaultActionsNode.Attributes["display"].Value));
                            }
                        }
                        else
                        {
                            defaultViews.Add(group.ID, currentList.DefaultView.ID);
                            foreach (SPView view in currentList.Views)
                            {
                                if ((!view.Hidden) && (!view.PersonalView))
                                    roleProperties[group.ID].Add(view.ID, true);
                            }
                        }                        
                    }                   
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Erreur lors du chargement des permissions : {0}Trace{1}", ex.Message, ex.StackTrace));
                }
            }
        }

        private static Guid GetDefautView(Dictionary<int, string> groupValues, int groupId)
        {
            if (groupValues.ContainsKey(groupId))
            {
                string[] values = groupValues[groupId].Split("#".ToCharArray());
                return new Guid(values[1]);
            }
            else
                return Guid.Empty;
        }

        private static bool IsViewAllowed(Dictionary<int, string> groupValues, int groupId, Guid viewToTest)
        {
            if (groupValues.ContainsKey(groupId))
            {
                string[] values = groupValues[groupId].Split("#".ToCharArray());
                return values[2].Contains(viewToTest.ToString("D"));
            }
            else
                return true;
        }

        public static bool IsAnAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
