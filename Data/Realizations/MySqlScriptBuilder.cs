using Data.Database;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Realizations
{
    public class MySqlScriptBuilder : ISqlScriptBuilder
    {
        public string BuildScript(Type entity, IEnumerable<Type> models, ScriptAction action)
        {
            switch (action)
            {
                case ScriptAction.Create:

                    break;
                case ScriptAction.Read:

                    break;
                case ScriptAction.Update:

                    break;
                case ScriptAction.Delete:

                    break;            
            }

            return null;
        }
    }
}
