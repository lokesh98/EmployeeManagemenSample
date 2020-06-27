using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMgmt.ViewModel
{
  public  class PaginatedListViewModel<T> where T : class
    {
        public IEnumerable<T> dataList { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(dataList.Count() / (double)ItemPerPage));
        }
        public IEnumerable<T> PaginatedData()
        {
            int start = (CurrentPage - 1) * ItemPerPage;
            return dataList.Skip(start).Take(ItemPerPage);
        }


    }

 

}
