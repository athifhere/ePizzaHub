using ePizzaHub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Interfaces
{
    public interface IAuthService
    {
        bool CreateUser(User user, string password, string role);
        User AuthenticateUser(string username, string password);
        bool SignOut();
    }
}
