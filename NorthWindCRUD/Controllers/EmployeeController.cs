using AutoMapper;
using NorthWindCRUD.App_Start;
using NorthWindCRUD.Dtos;
using NorthWindCRUD.Models;
using NorthWindCRUD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NorthWindCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        Context _context = new Context();
        IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        }));

        // GET: Employee
        public ActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees));
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeID == id);

            EditEmployeeViewModel viewModel = new EditEmployeeViewModel()
            {
                employeeDto = mapper.Map<EmployeeDto>(employee),
                reportCandidates = _context.Employees.Where(x => x.EmployeeID != id).ToDictionary(x => x.EmployeeID, x => x.CommonName)
            };
            return View(viewModel);
        }

        [ActionName("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Post(EmployeeDto employeeDto)
        {
            var employeeInDB = _context.Employees.First(x => x.EmployeeID == employeeDto.EmployeeID);
            if (employeeInDB == null)
            {

            }
            else
            {
                mapper.Map<EmployeeDto, Employee>(employeeDto, employeeInDB);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeID == id);
            return View(mapper.Map<EmployeeDto>(employee));
        }

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Post(int? id)
        {
            if (id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "id cannot be null");

            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeID == id);
            if (employee == null)
                return HttpNotFound($"There is no employee which id is {id}");
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeID == id);
            if (employee == null)
                return HttpNotFound($"There is no employee which id is {id}");
            return View(mapper.Map<EmployeeDto>(employee));
        }
    }
}