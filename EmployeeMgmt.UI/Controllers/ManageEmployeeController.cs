using EmployeeMgmt.Repository;
using EmployeeMgmt.UI.Models;
using EmployeeMgmt.UI.Utility;
using EmployeeMgmt.ViewModel;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EmployeeMgmt.UI.Controllers
{
    [Authorize]
    [SessionTimeout]
    public class ManageEmployeeController : Controller
    {

        private readonly IEmployeeRepository employeeRepository;
        public ManageEmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public ActionResult Index(EmployeeViewModel model, string FullName, string Gender, string Designation,
            DateTime? ImportedDate, DateTime? from_dob, DateTime? to_dob, decimal? from_salary, decimal? Salary,
            string Sorting_Order, int page = 1)
        {

            if (model == null)
            {
                model = new EmployeeViewModel();
            }

            if (model.FullName == null || model.Gender == null || model.Designation == null ||model.ImportedDate == null || model.from_dob == null || model.to_dob == null || model.from_salary == 0 || model.Salary == 0)
            {
                model.FullName = FullName;
                model.Gender = Gender;
                model.Designation = Designation;
                model.ImportedDate = ImportedDate;
                model.from_dob = from_dob;
                model.to_dob = to_dob;
                model.from_salary = from_salary.HasValue ? from_salary.Value : 0;
                model.Salary = Salary.HasValue ? Salary.Value : 0; ;
            }


            var data = GetFilteredEmployee(model);
            ViewBag.CurrentSortOrder = Sorting_Order; 
            ViewBag.FullName = String.IsNullOrEmpty(Sorting_Order) ? "FullName" : "";
            ViewBag.DateofBirth = Sorting_Order == "DateofBirth" ? "DateofBirth" : "Date";
            ViewBag.Gender = String.IsNullOrEmpty(Sorting_Order) ? "Gender" : "";
            ViewBag.Salary = String.IsNullOrEmpty(Sorting_Order) ? "Salary" : "";
            ViewBag.Designation = String.IsNullOrEmpty(Sorting_Order) ? "Designation" : "";
            ViewBag.ImportedDate = String.IsNullOrEmpty(Sorting_Order) ? "ImportedDate" : "";

            switch (Sorting_Order)
            {
                case "FullName":
                    data = data.OrderBy(e => e.FullName).ToList();
                    break;
                case "DateofBirth":
                    data = data.OrderBy(e => e.DateofBirth).ToList();
                    break;
                case "Gender":
                    data = data.OrderBy(e => e.Gender).ToList();
                    break;
                case "Salary":
                    data = data.OrderBy(e => e.Salary).ToList();
                    break;
                case "Designation":
                    data = data.OrderBy(e => e.Designation).ToList();
                    break;
                default:
                    data = data.OrderBy(e => e.ImportedDate).ToList();
                    break;
            }

            model.PaginatedList = new PaginatedListViewModel<EmployeeViewModel>
            {
                ItemPerPage = PageConfig.ItemPerPage,
                dataList = data,
                CurrentPage = page
            };
            var genderList = new List<SelectListItem>{
                                        new SelectListItem{ Text="Male", Value = "Male" },
                                        new SelectListItem{ Text="Female", Value = "Female" }
                                    };
            ViewData["genderList"] = genderList;
            return View(model);
        }





        [HttpGet]
        public ActionResult Create()
        {
            var genderList = new List<SelectListItem>{
                                        new SelectListItem{ Text="Male", Value = "Male" },
                                        new SelectListItem{ Text="Female", Value = "Female" }
                                    };
            ViewData["genderList"] = genderList;

            EmployeeViewModel model = new EmployeeViewModel();
            return View(model);

        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel model)
        {


            if (ModelState.IsValid)
            {
                if (model.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    string extension = Path.GetExtension(model.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    model.ImageUrl = "Content/img/" + fileName;
                    model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName));

                }
                int userId = Convert.ToInt32(Session["userId"].ToString());
                model.ImportedBy = userId;
                ResponseViewModel response = employeeRepository.AddEmployeeWithImage(model);
                if (response.status == "success")
                {
                    TempData["success_msg"] = response.message;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Model", response.message);
                    TempData["error_msg"] = response.message;
                }
            }
            var genderList = new List<SelectListItem>{
                                        new SelectListItem{ Text="Male", Value = "Male" },
                                        new SelectListItem{ Text="Female", Value = "Female" }
                                    };
            ViewData["genderList"] = genderList;
            return View(model);
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmployeeViewModel model = new EmployeeViewModel();
            model = employeeRepository.getEmployeeById(id);
            var genderList = new List<SelectListItem>{
                                        new SelectListItem{ Text="Male", Value = "Male" },
                                        new SelectListItem{ Text="Female", Value = "Female" }
                                    };
            ViewData["genderList"] = genderList;
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    string extension = Path.GetExtension(model.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    model.ImageUrl = "Content/img/" + fileName;
                    model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName));
                }
                int userId = Convert.ToInt32(Session["userId"].ToString());
                model.ModifiedBy = userId;
                ResponseViewModel response = employeeRepository.UpdateEmployeeInfo(model);
                if (response.status == "success")
                {
                    TempData["success_msg"] = response.message;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Model", response.message);
                    TempData["error_msg"] = response.message;
                }
            }
            var genderList = new List<SelectListItem>{
                                        new SelectListItem{ Text="Male", Value = "Male" },
                                        new SelectListItem{ Text="Female", Value = "Female" }
                                    };
            ViewData["genderList"] = genderList;
            return View(model);
        }


        #region filter_employee
        private List<EmployeeViewModel> GetFilteredEmployee(EmployeeViewModel model)
        {
            var data = employeeRepository.GetAllEmployee();

            if (!string.IsNullOrEmpty(model.FullName))
            {
                data = data.Where(x => x.FullName.ToLower().Contains(model.FullName.Trim().ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(model.Designation))
            {
                data = data.Where(x => x.Designation.ToLower().Contains(model.Designation.Trim().ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(model.Gender))
            {
                data = data.Where(x => x.Gender.ToLower() == model.Gender.Trim().ToLower()).ToList();
            }

            if (model.from_dob != null && model.to_dob != null)
            {
                data = data.Where(x => Convert.ToDateTime(x.DateofBirth).Date >= Convert.ToDateTime(model.from_dob).Date
                                    && Convert.ToDateTime(x.DateofBirth).Date <= Convert.ToDateTime(model.to_dob).Date
                                    ).ToList();
            }

            if (model.from_salary > 0 && model.Salary > 0)
            {
                data = data.Where(x => x.Salary >= model.from_salary && x.Salary <= model.Salary).ToList();
            }
            if (model.ImportedDate != null)
            {
                data = data.Where(x => Convert.ToDateTime(x.ImportedDate).Date == Convert.ToDateTime(model.ImportedDate).Date).ToList();
            }
            return data;
        }
        #endregion

        #region export_data

        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult ExportAll(int[] empIds,string exportTo)
        {
            var data = employeeRepository.GetAllEmployee();
            if (empIds!=null)
            {
                data = data.Where(e => empIds.Contains(e.EmployeeId)).ToList();
            }

            if (exportTo == "excel")
            {
                var excelPackage = Excel_CSV_Utility.ExportToExcel(data);
                Session["DownloadExcel_FileManager"] = excelPackage;
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else if (exportTo == "csv")
            {
                var csvdata = Excel_CSV_Utility.ExportToCSV(data);
                Session["DownloadCSV_FileManager"] = csvdata;
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else if (exportTo == "pdf")
            {
                var pdfData = Excel_CSV_Utility.ExportToPDF(data);
                Session["DownloadPDF_FileManager"] = pdfData;
                return Json("", JsonRequestBehavior.AllowGet);
            }
            return Json("");
        }

       
      

        public ActionResult Download()
        {

            if (Session["DownloadExcel_FileManager"] != null)
            {
                byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                Session["DownloadExcel_FileManager"] = null;
                return File(data, "application/octet-stream", "EmployeeInfo.xlsx");
            }
            else if (Session["DownloadCSV_FileManager"] != null)
            {
                byte[] data = Session["DownloadCSV_FileManager"] as byte[];
                Session["DownloadCSV_FileManager"] = null;
                return File(data, "application/octet-stream", "EmployeeInfo.csv");
            }
            else if (Session["DownloadPDF_FileManager"] != null)
            {
                byte[] data = Session["DownloadPDF_FileManager"] as byte[];
                Session["DownloadPDF_FileManager"] = null;
                return File(data, "application/octet-stream", "EmployeeInfo.pdf");
            }
            else
            {
                return new EmptyResult();
            }
        }
        #endregion
    }
}