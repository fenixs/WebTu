using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTu.BLL;
using WebTu.ViewModel.SPA;
using OldViewModel = WebTu.ViewModels;

using WebTu.Filters;
using WebTu.Models;

namespace WebTu.Areas.SPA.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /SPA/Main/
        [Authorize]
        public ActionResult Index()
        {
            MainViewModel v = new MainViewModel();
            v.UserName = User.Identity.Name;
            v.FooterData = new OldViewModel.FooterViewModel();
            v.FooterData.CompanyName = "CompanyName";
            v.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index",v);
        }

        [Authorize]
        public ActionResult EmployeeList()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            EmployeeBusinessLayer ebl = new EmployeeBusinessLayer();
            var employees = ebl.GetEmployees();            
            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();
            foreach (var item in employees)
            {
                EmployeeViewModel evm = new EmployeeViewModel();
                evm.EmployeeName = item.FirstName + "|" + item.LastName;
                evm.Salary = item.Salary.ToString("c");
                evm.SalaryColor = item.Salary > 15000 ? "yellow" : "green";
                empViewModels.Add(evm);
            }
            employeeListViewModel.Employees = empViewModels;
            return View("EmployeeList", employeeListViewModel);
        }


        public ActionResult GetAddNewLink()
        {
            if(Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }

        [AdminFilter]
        public ActionResult AddNew()
        {
            
            CreateEmployeeViewModel v = new CreateEmployeeViewModel();
            return PartialView("CreateEmployee", v);
        }

        [AdminFilter]
        public ActionResult SaveEmployee(Employee emp)
        {
            EmployeeBusinessLayer empbal = new EmployeeBusinessLayer();
            empbal.SaveEmployee(emp);
            EmployeeViewModel empViewModel = new EmployeeViewModel();
            empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
            empViewModel.Salary = emp.Salary.ToString("c");
            if(emp.Salary>15000)
            {
                empViewModel.SalaryColor = "yellow";
            }
            else
            {
                empViewModel.SalaryColor = "green";
            }
            return Json(empViewModel);
        }

	}
}