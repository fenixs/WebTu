using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTu.ViewModels
{
    public class EmployeeViewModel
    {
        public string EmployeeName { get; set; }
        public string Salary { get; set; }
        public string SalaryColor { get; set; }
        
    }

    public class EmployeeListViewModel : BaseViewModel
    {
        public List<EmployeeViewModel> Employees { get; set; }
        //public string UserName { get; set; }

        //public FooterViewModel FooterData { get; set; }
    }

    public class CreateEmployeeViewModel : BaseViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salary { get; set; }
    }
}