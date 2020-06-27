using EmployeeMgmt.Data;
using EmployeeMgmt.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgmt.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDBEntities _db;
        public EmployeeRepository(EmployeeDBEntities _db)
        {
            this._db = _db;
        }
        public ResponseViewModel AddEmployee(List<EmployeeViewModel> employees, int userId)
        {
            var emp = from e in employees
                      select new Employee
                      {
                          DateofBirth = e.DateofBirth,
                          Designation = e.Designation,
                          Gender = e.Gender,
                          Salary = e.Salary,
                          FullName = e.FullName,
                          ImportedDate = DateTime.Now,
                          ImportedBy = userId
                      };
            try
            {
                _db.Employees.AddRange(emp);
                _db.SaveChanges();

                return new ResponseViewModel()
                {
                    message =MsgBox.upload_msg,
                    status =MsgBox.success_status
                };
            }
            catch (Exception ex)
            {

                return new ResponseViewModel()
                {
                    message = ex.Message.ToString(),
                    status = "error"
                };
            }


        }

        public ResponseViewModel AddEmployeeWithImage(EmployeeViewModel model)
        {
            try
            {
                Employee employee = new Employee()
                {
                    FullName = model.FullName,
                    DateofBirth = model.DateofBirth,
                    Gender = model.Gender,
                    Salary = model.Salary,
                    Designation = model.Designation,
                    ImageUrl = model.ImageUrl,
                    ImportedDate = DateTime.Now,
                    ImportedBy = model.ImportedBy
                };
                _db.Employees.Add(employee);
                _db.SaveChangesAsync();
                return new ResponseViewModel() { message = MsgBox.save_msg, status = MsgBox.success_status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { message = ex.Message.ToString(), status = MsgBox.failed_status };
            }
        }

        public List<EmployeeViewModel> GetAllEmployee()
        {
            var employees = _db.Employees.AsNoTracking().Select(e => new EmployeeViewModel()
            {
                DateofBirth = e.DateofBirth,
                Designation = e.Designation,
                EmployeeId = e.EmployeeId,
                Gender = e.Gender,
                Salary = e.Salary.HasValue ? e.Salary.Value : 0,
                FullName = e.FullName,
                ImportedDate = e.ImportedDate
            }).ToList();

            return employees;
        }


        public EmployeeViewModel getEmployeeById(int? employeeId)
        {
            var employee = _db.Employees.Where(x => x.EmployeeId == employeeId).FirstOrDefault();
            var employeeView = new EmployeeViewModel()
            {
                DateofBirth = employee.DateofBirth,
                FullName = employee.FullName,
                ImageUrl = employee.ImageUrl,
                Salary = employee.Salary.HasValue ? employee.Salary.Value : 0,
                Designation = employee.Designation,
                EmployeeId = employee.EmployeeId,
                Gender = employee.Gender,

            };
            return employeeView;
        }

        public ResponseViewModel UpdateEmployeeInfo(EmployeeViewModel model)
        {
            var _employee = _db.Employees.Where(x => x.EmployeeId == model.EmployeeId).FirstOrDefault();
            if (_employee != null)
            {
                _employee.FullName = model.FullName;
                _employee.DateofBirth = model.DateofBirth;
                _employee.Salary = model.Salary;
                _employee.Gender = model.Gender;
                _employee.Designation = model.Designation;
                _employee.ModifiedDate = DateTime.Now;
                _employee.ModifiedBy = model.ModifiedBy;
                if (!string.IsNullOrEmpty(model.ImageUrl) && model.ImageUrl != "~/Content/img/dummy.jpg")
                {
                    _employee.ImageUrl = model.ImageUrl;
                }

            }
            try
            {
                _db.SaveChanges();
                return new ResponseViewModel() { message = MsgBox.update_msg, status = MsgBox.success_status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { message = ex.InnerException.Message.ToString(), status = MsgBox.failed_status };
            }
        }
    }
}
