using ePizzaHub.Entities;
using ePizzaHub.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Services
{
    public class AuthService : IAuthService
    {
        public User AuthenticateUser(string username, string password)
        {
            User user = null;
            return user;
        }

        public bool CreateUser(User user, string password, string role)
        {
            return true;
        }

        public void SignOut()
        {
            
        }
    }
}
