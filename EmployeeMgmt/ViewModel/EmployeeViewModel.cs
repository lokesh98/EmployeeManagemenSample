using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeMgmt.ViewModel
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            ImageUrl = "~/Content/img/dummy.jpg";
        }
        public int EmployeeId { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateofBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public string Designation { get; set; }
        public int ImportedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ImportedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Error_Column { get; set; }
        public string Error_Msg { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
        public virtual PaginatedListViewModel<EmployeeViewModel> PaginatedList { get; set; }


        // for filter

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? from_dob { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? to_dob { get; set; }
        public decimal from_salary { get; set; }
    }

    public class EmployeeVMCustom
    {
        public List<EmployeeViewModel> EmployeeList { get; set; }
        public string msg { get; set; }
        public string error_msg { get; set; }
    }

}
