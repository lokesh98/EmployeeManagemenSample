using EmployeeMgmt.Data;
using EmployeeMgmt.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgmt.Repository
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly EmployeeDBEntities _db;
        public UserGroupRepository(EmployeeDBEntities _db)
        {
            this._db = _db;
        }
        public UserGroupViewModel getUserGroupByID(int? usergroupId)
        {
            var usergroup = _db.UserGroups.Where(x => x.UserGroupId == usergroupId).FirstOrDefault();
            if (usergroupId != null)
            {
                return new UserGroupViewModel()
                {
                    UserGroupId = usergroup.UserGroupId,
                    UserGroupName = usergroup.UserGroupName
                };
            }
            return null;
        }
    }
}
