﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HPF.SharePointAPI {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class DocumentCenter : global::System.Configuration.ApplicationSettingsBase {
        
        private static DocumentCenter defaultInstance = ((DocumentCenter)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new DocumentCenter())));
        
        public static DocumentCenter Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Counseling Summaries")]
        public string CounselingSummary {
            get {
                return ((string)(this["CounselingSummary"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Invoices")]
        public string Invoice {
            get {
                return ((string)(this["Invoice"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Agency Payables")]
        public string AgencyPayable {
            get {
                return ((string)(this["AgencyPayable"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("DocumentCenter")]
        public string DocumentCenterWeb {
            get {
                return ((string)(this["DocumentCenterWeb"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Error Folder")]
        public string ErrorFolderName {
            get {
                return ((string)(this["ErrorFolderName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Upload report error")]
        public string ErrorSubject {
            get {
                return ((string)(this["ErrorSubject"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Error when upload report file {0} to {1} folder. The {1} folder does not exist. I" +
            "t was moved to {2}")]
        public string ErrorBodyDoesNotExistSPFolder {
            get {
                return ((string)(this["ErrorBodyDoesNotExistSPFolder"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Error when upload report file {0} to empty folder. It was moved to {1}")]
        public string ErrorBodyEmptySPFolderName {
            get {
                return ((string)(this["ErrorBodyEmptySPFolderName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("serverimage\\Administrator")]
        public string InvoiceLoginName {
            get {
                return ((string)(this["InvoiceLoginName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("serverimage\\Administrator")]
        public string AgencyPayableLoginName {
            get {
                return ((string)(this["AgencyPayableLoginName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("serverimage\\Administrator")]
        public string CounselingSummaryLoginName {
            get {
                return ((string)(this["CounselingSummaryLoginName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://hpf-01:6060")]
        public string SharePointSite {
            get {
                return ((string)(this["SharePointSite"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("serverimage\\Administrator")]
        public string FannieMaeLoginName {
            get {
                return ((string)(this["FannieMaeLoginName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("MHARawData")]
        public string FannieMaeWeeklyReport {
            get {
                return ((string)(this["FannieMaeWeeklyReport"]));
            }
        }
    }
}
