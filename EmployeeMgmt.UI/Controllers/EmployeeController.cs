using EmployeeMgmt.Repository;
using EmployeeMgmt.UI.Models;
using EmployeeMgmt.UI.Utility;
using EmployeeMgmt.ViewModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeMgmt.UI.Controllers
{
    [Authorize]
    [SessionTimeout]
    public class EmployeeController : Controller
    {
        // GET: Employee
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UploadEmployeeInfo()
        {


            if (Request.Files.Count > 0)
            {
                var files = Request.Files[0];
                string extension = System.IO.Path.GetExtension(files.FileName).ToLower();

                EmployeeVMCustom employees = new EmployeeVMCustom();
                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path = string.Format("{0}/{1}", Server.MapPath("~/ExcelUploads"), files.FileName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(Server.MapPath("~/ExcelUploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    files.SaveAs(path);
                    if (extension == ".csv")
                    {
                        DataTable dt = Excel_CSV_Utility.ConvertCSVtoDataTable(path);
                        employees = Excel_CSV_Utility.ValidateData(dt);
                    }
                    else if (extension.Trim() == ".xls" || extension.Trim() == ".xlsx")
                    {
                        DataTable dt = Excel_CSV_Utility.readExcel(path);
                        employees = Excel_CSV_Utility.ValidateData(dt);
                    }
                    if (!(string.IsNullOrEmpty(employees.msg)))
                    {
                        ViewBag.Error = "Row " + employees.msg + " were skipped since they did not have data";
                    }
                    if (!(string.IsNullOrEmpty(employees.error_msg)))
                    {
                        ViewBag.ErrorVal = employees.error_msg;
                    }

                    return View(employees.EmployeeList);
                }
                else
                {
                    ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                }

            }

            return View();
        }


        [HttpPost]
        public ActionResult SaveEmployee(List<EmployeeViewModel> viewModels)
        {
            if (viewModels.Count > 0)
            {
                int userId = Convert.ToInt32(Session["userId"].ToString());
                ResponseViewModel response = employeeRepository.AddEmployee(viewModels, userId);


                if (response.status == "success")
                {
                    TempData["success_msg"] = response.message;
                }
                else
                {
                    ModelState.AddModelError("Model", response.message);
                    TempData["error_msg"] = response.message;
                }
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            return Json("error");
        }

        public ActionResult GenerateTemplate()
        {
            var template = Excel_CSV_Utility.EmptyExcelTemplate();
            Session["DownloadExcelTemplate_FileManager"] = template;
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadTemplate()
        {

            if (Session["DownloadExcelTemplate_FileManager"] != null)
            {
                byte[] data = Session["DownloadExcelTemplate_FileManager"] as byte[];
                Session["DownloadExcelTemplate_FileManager"] = null;
                return File(data, "application/octet-stream", "EmployeeExcelTemplate.xlsx");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}