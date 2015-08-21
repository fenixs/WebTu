using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTu.BLL;
using WebTu.Filters;
using WebTu.Models;
using WebTu.ViewModels;

namespace WebTu.Controllers
{
    public class EmployeeController : Controller
    {
        

       

        
        [HeaderFooterFilter]
        [Authorize]
        [Route("Employee/List")]
        public ActionResult Index()
        {
            //Employee em = new Employee()
            //{
            //    FirstName = "FN",
            //    LastName = "LN",
            //    Salary = 20000
            //};

            //EmployeeViewModel vmEmp = new EmployeeViewModel();
            //vmEmp.EmployeeName = em.FirstName + " " + em.LastName;
            //vmEmp.Salary = em.Salary.ToString("c");
            //vmEmp.SalaryColor = em.Salary > 15000 ? "yellow" : "green";

            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel() { Employees = new List<EmployeeViewModel>() };
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            var employees = empBal.GetEmployees();
            foreach (var emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel()
                {
                     EmployeeName = emp.FirstName + " " + emp.LastName,
                     Salary = emp.Salary.ToString(),
                     SalaryColor = emp.Salary>15000?"yellow":"green",
                };
                employeeListViewModel.Employees.Add(empViewModel);
            }

            //employeeListViewModel.UserName = User.Identity.Name;

            //employeeListViewModel.FooterData = new FooterViewModel()
            //{
            //     CompanyName = "CompanyName",
            //     Year = DateTime.Now.Year.ToString()
            //};

            
            return View(employeeListViewModel);
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult AddNew()
        {

            var ceViewModel = new CreateEmployeeViewModel();
            //ceViewModel.FooterData = new FooterViewModel()
            //{
            //     CompanyName = "CompanyName",
            //     Year = DateTime.Now.Year.ToString()
            //};
            //ceViewModel.UserName = User.Identity.Name;
            

            return View("CreateEmployee",ceViewModel);
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
        [HeaderFooterFilter]
        public ActionResult SaveEmployee(Employee e)
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
                empBal.SaveEmployee(e);

                return RedirectToAction("Index");
            }
            else
            {
                CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                vm.FirstName = e.FirstName;
                vm.LastName = e.LastName;
                
                vm.Salary = e.Salary.ToString();

                //vm.FooterData = new FooterViewModel()
                //{
                //    CompanyName = "CompanyName",
                //     Year = DateTime.Now.Year.ToString()
                //};

                //vm.UserName = User.Identity.Name;
                
                return View("CreateEmployee",vm);
            }
            //return new EmptyResult();
        }

        public ActionResult CancelSave()
        {
            return RedirectToAction("Index");
        }
	}

    public class MyEmployeeModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            Employee e = new Employee();
            e.FirstName = controllerContext.RequestContext.HttpContext.Request.Form["FirstName"];
            e.LastName = controllerContext.RequestContext.HttpContext.Request.Form["LastName"];
            e.Salary = int.Parse(controllerContext.RequestContext.HttpContext.Request.Form["Salary"]);

            return e;
            //return base.CreateModel(controllerContext, bindingContext, modelType);
        }
    }
}