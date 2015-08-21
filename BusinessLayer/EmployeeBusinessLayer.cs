using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebTu.DAL;
using WebTu.Models;

namespace WebTu.BLL
{
    public class EmployeeBusinessLayer
    {

        //public bool IsValidUser(UserDetails user)
        //{
        //    if(user.UserName == "Admin" && user.Password == "Admin")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public UserStatus GetUserValidity(UserDetails u)
        {
            if(u.UserName=="Admin" && u.Password=="Admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if(u.UserName == "Went" && u.Password =="Went")
            {
                return UserStatus.AuthenticatedUser;
            }
            return UserStatus.NonAuthenticatedUser;
        }


        public List<Employee> GetEmployees()
        {
            
            SalesERPDAL salesDal = new SalesERPDAL();
            return salesDal.Employees.ToList();
            
        }

        public Employee SaveEmployee(Employee e)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.Add(e);
            salesDal.SaveChanges();
            return e;
        }

        public void UploadEmployees(List<Employee> employees)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.AddRange(employees);
            salesDal.SaveChanges();
        }
    }
}