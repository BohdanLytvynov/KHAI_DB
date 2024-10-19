using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDbConnectionBuilder
    {
        IDbConnection Buid(string conString);
    }
}
