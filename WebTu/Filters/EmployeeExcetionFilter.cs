using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTu.Logger;

namespace WebTu.Filters
{
    public class EmployeeExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            FileLogger fl = new FileLogger();
            fl.LogException(filterContext.Exception);
            //base.OnException(filterContext);

            filterContext.ExceptionHandled = true;
            filterContext.Result = new ContentResult()
            {
                Content = "Sorry for the Error"
            };

        }
    }
}