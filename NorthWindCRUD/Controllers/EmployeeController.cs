using AutoMapper;
using NorthWindCRUD.App_Start;
using NorthWindCRUD.Dtos;
using NorthWindCRUD.Models;
using NorthWindCRUD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

            List<ReportCandidate> reportCandidates = new List<ReportCandidate>() { new ReportCandidate { ReportToId = 0, ReportToName = "-" } };
            _context.Employees.Where(x => x.EmployeeID != id).ToList().ForEach(
                x => reportCandidates.Add(new ReportCandidate { ReportToId = x.EmployeeID, ReportToName = x.CommonName }));

            EmployeeViewModel viewModel = new EmployeeViewModel()
            {
                employeeDto = mapper.Map<EmployeeDto>(employee),
                reportCandidates = reportCandidates
            };
            return View("Form", viewModel);
        }

        [ActionName("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit_Post(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                Update(employeeDto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            List<ReportCandidate> reportCandidates = new List<ReportCandidate>() { new ReportCandidate { ReportToId = 0, ReportToName = "-" } };
            _context.Employees.Where(x => x.EmployeeID != employeeDto.EmployeeID).ToList().ForEach(
                x => reportCandidates.Add(new ReportCandidate { ReportToId = x.EmployeeID, ReportToName = x.CommonName }));

            EmployeeViewModel viewModel = new EmployeeViewModel()
            {
                employeeDto = employeeDto,
                reportCandidates = reportCandidates
            };
            return View("Form", viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<ReportCandidate> reportCandidates = new List<ReportCandidate>() { new ReportCandidate { ReportToId = 0, ReportToName = "-" } };
            _context.Employees.ToList().ForEach(
                x => reportCandidates.Add(new ReportCandidate { ReportToId = x.EmployeeID, ReportToName = x.CommonName }));

            EmployeeViewModel viewModel = new EmployeeViewModel()
            {
                employeeDto = mapper.Map<EmployeeDto>(new Employee()),
                reportCandidates = reportCandidates
            };
            return View("Form", viewModel);
        }

        [ActionName("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create_Post(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                Update(employeeDto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            List<ReportCandidate> reportCandidates = new List<ReportCandidate>() { new ReportCandidate { ReportToId = 0, ReportToName = "-" } };
            _context.Employees.ToList().ForEach(
                x => reportCandidates.Add(new ReportCandidate { ReportToId = x.EmployeeID, ReportToName = x.CommonName }));

            EmployeeViewModel viewModel = new EmployeeViewModel()
            {
                employeeDto = employeeDto,
                reportCandidates = reportCandidates
            };
            return View("Form", viewModel);
        }

        private void Update(EmployeeDto employeeDto)
        {
            var employeeInDB = _context.Employees.FirstOrDefault(x => x.EmployeeID == employeeDto.EmployeeID);
            if (employeeInDB == null)
            {
                _context.Employees.Add(mapper.Map<Employee>(employeeDto));
            }
            else
            {
                mapper.Map<EmployeeDto, Employee>(employeeDto, employeeInDB);
            }
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
            if (!id.HasValue)
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