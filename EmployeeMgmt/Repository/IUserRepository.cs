using EmployeeMgmt.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgmt.Repository
{
    public interface IUserRepository
    {
        UserViewModel SignInUser(string email, string password);
    }
}
