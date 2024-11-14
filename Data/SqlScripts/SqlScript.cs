using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SqlScripts
{
    public struct SqlScript
    {
        public string CommandText { get; }

        public List<Tuple<string, object>> Params { get; }

        public SqlScript(string commandText, List<Tuple<string, object>> parametrs)
        {
            if (string.IsNullOrEmpty(commandText))
                throw new ArgumentNullException(nameof(CommandText));

            if (parametrs is null)
                Params = new();
            else
                Params = parametrs;

            CommandText = commandText;                        
        }

        public SqlScript(string commandText) : this(commandText, null)
        {
            
        }
    }
}
