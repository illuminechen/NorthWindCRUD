using NorthWindCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWindCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            Context context = new Context();
            var employees = context.Employees.ToList();
            return View(employees);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Context context = new Context();
                var employee = context.Employees.FirstOrDefault(x => x.EmployeeID == id);
                return View(employee);
            }
            return Redirect("Employee");
        }

        [ActionName("Delete")]
        [HttpPost]
        public ActionResult Delete_Post(int? id)
        {
            Context context = new Context();
            var employee = context.Employees.FirstOrDefault(x => x.EmployeeID == id);
            context.Employees.Remove(employee);
            //context.SaveChanges();
            return Redirect("/Employee");
        }
    }
}