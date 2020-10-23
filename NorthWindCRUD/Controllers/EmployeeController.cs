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
            if (!id.HasValue)
                return Redirect("/Employee");
            Context context = new Context();
            var employee = context.Employees.FirstOrDefault(x => x.EmployeeID == id);
            return View(employee);
        }

        [ActionName("Delete")]
        [HttpPost]
        public ActionResult Delete_Post(int? id)
        {
            if (id.HasValue)
                return Content($"{id} cannot be null");

            Context context = new Context();
            var employee = context.Employees.FirstOrDefault(x => x.EmployeeID == id);
            if (employee == null)
                return Content($"There is no employee which id is {id}");
            context.Employees.Remove(employee);
            //context.SaveChanges();
            return Redirect("/Employee");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return Redirect("/Employee");
            Context context = new Context();
            var employee = context.Employees.FirstOrDefault(x => x.EmployeeID == id);
            return View(employee);
        }
    }
}