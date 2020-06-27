using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgmt.ViewModel
{
    public class ResponseViewModel
    {
        public string status { get; set; }
        public string message { get; set; }
    }


    public static class MsgBox
    {
        public static string save_msg = "Successfully Saved.";
        public static string update_msg = "Updated Successfully .";
        public static string upload_msg = "Uploaded Successfully .";
        public static string delete_msg = "Successfully Deleted.";
        public static string id_not_found = "Id not Found";
        public static string success_status = "success";
        public static string failed_status = "failed";
    }
}
