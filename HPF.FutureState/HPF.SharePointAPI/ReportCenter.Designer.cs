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
    internal sealed partial class ReportCenter : global::System.Configuration.ApplicationSettingsBase {
        
        private static ReportCenter defaultInstance = ((ReportCenter)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new ReportCenter())));
        
        public static ReportCenter Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Auto Generated")]
        public string AutoGeneratedList {
            get {
                return ((string)(this["AutoGeneratedList"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("fpdemo\\Administrator")]
        public string AutoGeneratedLoginName {
            get {
                return ((string)(this["AutoGeneratedLoginName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ReportCenter")]
        public string ReportCenterWeb {
            get {
                return ((string)(this["ReportCenterWeb"]));
            }
        }
    }
}
