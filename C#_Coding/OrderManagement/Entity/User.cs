using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Entity
{
    internal class User
    {
        private int user_id;
        private string username;
        private string password;
        private string role;

        public int UserId { get { return user_id; } set { user_id = value; } }
        public string Username { get { return username; } set { username = value; } }   
        public string Password {get { return password; } set { password = value; } }
        public string Role { get { return role; } set { role = value; } }
    }
}
