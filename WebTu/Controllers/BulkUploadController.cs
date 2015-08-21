using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebTu.BLL;
using WebTu.Filters;
using WebTu.Models;
using WebTu.ViewModels;

namespace WebTu.Controllers
{
    public class BulkUploadController : AsyncController
    {
        //
        // GET: /BulkUpload/
        [HeaderFooterFilter]
        [AdminFilter]
        public ActionResult Index()
        {
            return View(new FileUploadViewModel());
        }

        //[HttpPost]
        //[AdminFilter]
        //public ActionResult Upload(FileUploadViewModel model)
        //{
        //    List<Employee> employees = GetEmployees(model);
        //    EmployeeBusinessLayer ebl = new EmployeeBusinessLayer();
        //    ebl.UploadEmployees(employees);
        //    return RedirectToAction("Index", "Employee");
        //}

        [AdminFilter]
        [HandleError]
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            int t1 = Thread.CurrentThread.ManagedThreadId;
            List<Employee> employees = await Task.Factory.StartNew(() => GetEmployees(model));
            int t2 = Thread.CurrentThread.ManagedThreadId;

            EmployeeBusinessLayer ebl = new EmployeeBusinessLayer();
            ebl.UploadEmployees(employees);
            return RedirectToAction("Index", "Employee");
        }


        List<Employee> GetEmployees(FileUploadViewModel model)
        {
            var employees = new List<Employee>();
            StreamReader csvReader = new StreamReader(model.fileUpload.InputStream);
            csvReader.ReadLine();   //Assuming first line is header
            while(!csvReader.EndOfStream)
            {
                var line = csvReader.ReadLine();
                var values = line.Split(',');  //Values are comma separated
                Employee e = new Employee();
                e.FirstName = values[0];
                e.LastName = values[1];
                e.Salary = int.Parse(values[2]);
                employees.Add(e);
            }
            return employees;
        }

	}
}