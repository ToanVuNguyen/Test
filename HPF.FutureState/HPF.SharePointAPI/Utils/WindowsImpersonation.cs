using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Globalization;

namespace HPF.SharePointAPI.Utils
{    
    public sealed class WindowsImpersonation
    {       
        private WindowsImpersonationContext _windowsContext;

        private WindowsImpersonation()
        {
        }

        public static WindowsImpersonation ImpersonateAs(string domainAccount, string password)
        {
            WindowsImpersonation imp = new WindowsImpersonation();

            if (imp.Impersonate(domainAccount, password))
                return imp;

            return null;
        }

        public void Undo()
        {
            if (this._windowsContext != null)
            {
                this._windowsContext.Undo();
                this._windowsContext = null;
            }
        }

        private bool Impersonate(string domainAccount, string password)
        {
            IntPtr token = IntPtr.Zero;

            try
            {
                string[] accountparts = domainAccount.Split('\\');
                string domain = accountparts[0];
                string username = accountparts[1];

                if (NativeMethods.LogonUser(username, domain, password,
                      NativeMethods.LOGON32_LOGON_NETWORK,
                               NativeMethods.LOGON32_PROVIDER_DEFAULT,
                      ref token) != 0)
                {
                    WindowsIdentity wi = new WindowsIdentity(token);
                    this._windowsContext = wi.Impersonate();
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(),
                     String.Format(CultureInfo.InvariantCulture,
                     "Impersonation: Error logging on user \"{0}\"",
                     domainAccount));
                }
            }
            catch (Exception ex)
            {   
                throw;
            }
            finally
            {
                if (token != IntPtr.Zero)
                    NativeMethods.CloseHandle(token);
            }

            return (this._windowsContext != null);
        }
    }

    internal sealed class NativeMethods
    {
        private NativeMethods() { }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        public const int LOGON32_PROVIDER_DEFAULT = 0;
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_LOGON_NETWORK = 3;


        [DllImport("advapi32.dll")]
        public static extern int LogonUser(String lpszUserName,
         String lpszDomain,
         String lpszPassword,
         int dwLogonType,
         int dwLogonProvider,
         ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
         int impersonationLevel,
         ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();
    }
}
