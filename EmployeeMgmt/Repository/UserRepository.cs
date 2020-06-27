using EmployeeMgmt.Data;
using EmployeeMgmt.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgmt.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EmployeeDBEntities _db;
        public UserRepository(EmployeeDBEntities _db)
        {
            this._db = _db;
        }

        public UserViewModel SignInUser(string email, string password)
        {
            var user = _db.Users.AsNoTracking().Where(x => x.email == email && x.Password == password).FirstOrDefault();
            if (user != null)
            {
                return new UserViewModel()
                {
                    UserId = user.UserId,
                    Username = user.UserName,
                    Email = user.email,
                    UserGroupId = user.UserGroupId,
                    UserGroup = new UserGroupViewModel() { UserGroupName = user.UserGroup.UserGroupName }

                };
            }
            return null;
        }
    }
}
