using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPF.FutureState.DataAccess
{
    public class MenuSecurityDAO : BaseDAO
    {
        private static readonly MenuSecurityDAO instance = new MenuSecurityDAO();
        /// <summary>
        /// Singleton
        /// </summary>
        public static MenuSecurityDAO Instance
        {
            get
            {
                return instance;
            }
        }

        protected MenuSecurityDAO()
        {
            
        }
    }
}
