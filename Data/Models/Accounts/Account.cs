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
       
        public string Name { get; set; }

        public string Surename { get; set; }
       
        public string Email { get; set; }

        public bool Gender { get; set; }

        public DateTime Birthday { get; set; }

        public Role Role { get; set; }

        public Account() : this(-1, "", "", "", "", "", true, new DateTime(), new Role())
        {
            
        }

        public Account(int accountId, 
            string login, 
            string password, 
            string name, 
            string surename, 
            string email, 
            bool gender, 
            DateTime birthday, 
            Role role)            
        {
            AccountId = accountId;
            Login = login;
            Password = password;
            Name = name;
            Surename = surename;
            Email = email;
            Gender = gender;
            Birthday = birthday;
            Role = role;
        }
    }
}
