using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgmt.ViewModel
{
   public class UserViewModel
    {
        public int UserId { get; set; }

        public string Username { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool RememberMe { get; set; }
        public string ReturnURL { get; set; }
        public int UserGroupId { get; set; }

        public UserGroupViewModel UserGroup { get; set; }
    }
}
