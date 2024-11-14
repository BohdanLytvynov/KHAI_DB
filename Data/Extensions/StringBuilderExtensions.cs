using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder Append(this StringBuilder sb, string value)
        { 
            sb.Append(value);
            return sb;
        }
    }
}
