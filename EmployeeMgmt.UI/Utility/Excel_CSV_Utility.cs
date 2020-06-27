using EmployeeMgmt.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EmployeeMgmt.UI.Utility
{
    public static class Excel_CSV_Utility
    {
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Full Name"),
                                 new DataColumn("Date of Birth", typeof(string)),
                                 new DataColumn("Gender", typeof(string)),
                                 new DataColumn("Salary", typeof(string)),
                                 new DataColumn("Designation",typeof(string)) });

            //Read the contents of CSV file.  
            string csvData = File.ReadAllText(strFilePath);
            //Execute a loop over the rows.  
            foreach (string row in csvData.Split('\n'))
            {
                if (row.Contains("Full Name"))
                {
                    continue;
                }
                dt.Rows.Add();
                int i = 0;

                //Execute a loop over the columns.  
                foreach (string cell in row.Split(','))
                {
                    dt.Rows[dt.Rows.Count - 1][i] = cell;
                    i++;
                }
            }

            return dt;
        }



        public static DataTable readExcel(string FilePath)
        {
            try
            {
                DataTable dt = new DataTable();
                FileInfo existingFile = new FileInfo(FilePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.End.Row;
                    int ColCount = worksheet.Dimension.Columns;

                    List<string> columnNames = new List<string>();

                    //loop all columns in the sheet and add them to the datatable

                    //loop all columns in the sheet and add them to the datatable
                    foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                    {
                        string columnName = cell.Text.Trim();
                        if (!string.IsNullOrEmpty(columnName))
                        {
                            columnNames.Add(columnName);
                            dt.Columns.Add(columnName);
                        }
                    }



                    int i = 1;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Full Name"] = (worksheet.Cells[row, columnNames.IndexOf("Full Name") + i].Value ?? string.Empty).ToString();
                        dr["Date of Birth"] = (worksheet.Cells[row, columnNames.IndexOf("Date of Birth") + i].Value ?? string.Empty).ToString();
                        dr["Gender"] = (worksheet.Cells[row, columnNames.IndexOf("Gender") + i].Value ?? string.Empty).ToString();
                        dr["Salary"] = (worksheet.Cells[row, columnNames.IndexOf("Salary") + i].Value ?? string.Empty).ToString();
                        dr["Designation"] = (worksheet.Cells[row, columnNames.IndexOf("Designation") + i].Value ?? string.Empty).ToString();
                        dt.Rows.Add(dr);

                    }

                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static EmployeeVMCustom ValidateData(DataTable dataTable)
        {
            EmployeeVMCustom employees = new EmployeeVMCustom();
            employees.EmployeeList = new List<EmployeeViewModel>();

            int i = 1;
            foreach (DataRow dr in dataTable.Rows)
            {
                if (!IsEmpty(dr))
                {
                    string full_name = dr["Full Name"].ToString();
                    string dob = dr["Date of Birth"].ToString();
                    string gender = dr["Gender"].ToString();
                    string salary = dr["Salary"].ToString();
                    string designation = dr["Designation"].ToString();
                    string error_col = "", error_msg = "";

                    DateTime? tmpDate = null;
                    decimal tmpSalary;
                    bool isSalary = decimal.TryParse(salary, out tmpSalary);

                    if (string.IsNullOrEmpty(full_name))
                    {
                        error_col = "Full Name; ";
                        error_msg = "Full Name is Required; ";
                    }
                    if (string.IsNullOrEmpty(dob))
                    {
                        error_col += "Date of Birth; ";
                        error_msg += "Date of Birth is Required; ";
                    }
                    if (!IsDate(dob))
                    {
                        error_col += "Date of Birth; ";
                        error_msg += "Date of Birth must contain date value only; ";

                    }


                    if (string.IsNullOrEmpty(gender))
                    {
                        error_col += "Gender; ";
                        error_msg += "Gender is Required; ";
                    }
                    if (string.IsNullOrEmpty(salary))
                    {
                        error_col += "Salary; ";
                        error_msg += "Salary is Required; ";
                    }
                    if (!isSalary)
                    {
                        error_col += "Salary; ";
                        error_msg += "Salary must contain numeric value only; ";

                    }
                    if (string.IsNullOrEmpty(designation))
                    {
                        error_col += "Designation";
                        error_msg += "Designation is Required";
                    }

                    if (IsDate(dob)) // string.IsNullOrEmpty if on .NET pre 4.0
                    {
                        tmpDate = DateTime.Parse(dob);
                    }
                    if (isSalary)
                    {
                        tmpSalary = decimal.Parse(salary);
                    }
                    if (!string.IsNullOrEmpty(error_col))
                    {
                        employees.error_msg = "error";
                    }

                    employees.EmployeeList.Add(new EmployeeViewModel()
                    {
                        DateofBirth = tmpDate,
                        FullName = full_name,
                        Gender = gender,
                        Salary = tmpSalary,
                        Designation = designation,
                        Error_Column = error_col,
                        Error_Msg = error_msg
                    });
                }

                else
                {
                    employees.msg += i.ToString() + ";";
                }
                i++;

            }
            return employees;
        }

        public static bool IsEmpty(this DataRow row)
        {
            return row == null || row.ItemArray.All(i => i.IsNullEquivalent());
        }

        public static bool IsNullEquivalent(this object value)
        {
            return value == null
                   || value is DBNull
                   || string.IsNullOrWhiteSpace(value.ToString());
        }


        public static bool IsDate(string date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DataTable GetEmployeeDataTable(IEnumerable<EmployeeViewModel> data)
        {

            DataTable dtEmp = new DataTable("EmployeeDetails");
            dtEmp.Columns.AddRange(new DataColumn[5] { new DataColumn("FullName"),
                                            new DataColumn("DateOfBirth"),
                                            new DataColumn("Gender"),
                                            new DataColumn("Salary"),
                                              new DataColumn("Designation")
            });
            foreach (var item in data)
            {
                dtEmp.Rows.Add(item.FullName, item.DateofBirth, item.Gender, item.Salary, item.Designation);
            }

            return dtEmp;
        }


        public static byte[] ExportToExcel(IEnumerable<EmployeeViewModel> data)
        {

            DataTable dt = GetEmployeeDataTable(data);

            try
            {
                var memoryStream = new MemoryStream();
                using (var excelPackage = new ExcelPackage(memoryStream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                    worksheet.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.None);
                    worksheet.Cells["A1:AN1"].Style.Font.Bold = true;
                    worksheet.DefaultRowHeight = 18;


                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.DefaultColWidth = 20;
                    worksheet.Column(2).AutoFit();

                    //Session["DownloadExcel_FileManager"] = excelPackage.GetAsByteArray();
                    return excelPackage.GetAsByteArray();
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static byte[] ExportToCSV(List<EmployeeViewModel> data)
        {
            List<object> employee = (from d in data
                                     select new[] { d.FullName.ToString(),d.DateofBirth.ToString(),
                                                           d.Gender,d.Salary.ToString(),d.Designation

                                }).ToList<object>();

            //Insert the Column Names.
            employee.Insert(0, new string[5] { "Full Name", "Date Of Birth", "Gender", "Salary", "Designation" });

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < employee.Count; i++)
            {
                string[] customer = (string[])employee[i];
                for (int j = 0; j < customer.Length; j++)
                {
                    //Append data with separator.
                    sb.Append(customer[j] + ',');
                }

                //Append new line character.
                sb.Append("\r\n");

            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public static byte[] ExportToPDF(List<EmployeeViewModel> data)
        {
            DataTable dtProduct = GetEmployeeDataTable(data);
            int pdfRowIndex = 1;
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                //Initialize the PDF document object.
                using (Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
                    Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

                    float[] columnDefinitionSize = { 5F, 2F, 2F, 2F, 5F };
                    PdfPTable table;
                    PdfPCell cell;

                    table = new PdfPTable(columnDefinitionSize)
                    {
                        WidthPercentage = 100
                    };

                    cell = new PdfPCell
                    {
                        BackgroundColor = new BaseColor(0xC0, 0xC0, 0xC0)
                    };

                    table.AddCell(new Phrase("FullName", font1));
                    table.AddCell(new Phrase("DateOfBirth", font1));
                    table.AddCell(new Phrase("Gender", font1));
                    table.AddCell(new Phrase("Salary", font1));
                    table.AddCell(new Phrase("Designation", font1));
                    table.HeaderRows = 1;

                    foreach (DataRow dataRow in dtProduct.Rows)
                    {
                        table.AddCell(new Phrase(dataRow["FullName"].ToString(), font2));
                        table.AddCell(new Phrase(dataRow["DateOfBirth"].ToString(), font2));
                        table.AddCell(new Phrase(dataRow["Gender"].ToString(), font2));
                        table.AddCell(new Phrase(dataRow["Salary"].ToString(), font2));
                        table.AddCell(new Phrase(dataRow["Designation"].ToString(), font2));

                        pdfRowIndex++;
                    }

                    pdfDoc.Add(table);
                    pdfDoc.Close();
                    pdfDoc.CloseDocument();
                    pdfDoc.Dispose();
                    writer.Close();
                    writer.Dispose();

                    //float fileSize = 0;
                    //fileSize = sourceFile.Length;

                    //Download the PDF file.

                }
                return stream.ToArray();
            }
        }

        public static byte[] EmptyExcelTemplate()
        {
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet Sheet = excelPackage.Workbook.Worksheets.Add("MarksheetExcel");
            Sheet.Cells["A1"].Value = "Full Name";
            Sheet.Cells["B1"].Value = "Date of Birth";
            Sheet.Cells["C1"].Value = "Gender";
            Sheet.Cells["D1"].Value = "Salary";
            Sheet.Cells["E1"].Value = "Designation";

            Sheet.Cells["A:AZ"].AutoFitColumns();
            return excelPackage.GetAsByteArray();
        }
    }


}