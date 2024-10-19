using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class Database
    {
        #region Properties

        private string m_conStr;

        IDbConnectionBuilder m_connBuilder;

        #endregion

        #region Ctor

        public Database(string conStr, 
            IDbConnectionBuilder conBuilder)
        {
            if (string.IsNullOrEmpty(conStr))
                throw new ArgumentNullException(nameof(conStr));

            if(conBuilder is null)
                throw new ArgumentNullException(nameof(conBuilder));

            m_conStr = conStr;

            m_connBuilder = conBuilder;
        }
        
        #endregion

        #region Functions

        public IDbConnection Open()
        {
           return m_connBuilder.Buid(m_conStr);          
        }

        #region Static

        //public static IDbCommand BuildCommand()
        //{ 
            
        //}

        #endregion

        #endregion
    }
}
