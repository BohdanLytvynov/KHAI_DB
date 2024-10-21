using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Accounts
{
    public class Account
    {
        public int AccountId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string Name { get; set; }

        public string Surename { get; set; }
       
        public string Email { get; set; }

        public int Gender { get; set; }

        public DateTime Birthday { get; set; }

        public Account()
        {
            
        }
    }
}
