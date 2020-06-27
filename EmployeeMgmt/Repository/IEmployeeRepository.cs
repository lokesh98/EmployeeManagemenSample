using EmployeeMgmt.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgmt.Repository
{
  public  interface IEmployeeRepository
    {
        ResponseViewModel AddEmployee(List<EmployeeViewModel> employees,int userId);
        List<EmployeeViewModel> GetAllEmployee();

        ResponseViewModel AddEmployeeWithImage(EmployeeViewModel model);
        EmployeeViewModel getEmployeeById(int? employeeId);

        ResponseViewModel UpdateEmployeeInfo(EmployeeViewModel model);
    }
}
