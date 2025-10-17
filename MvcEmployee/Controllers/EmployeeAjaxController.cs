using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcEmployee.Data;
using MvcEmployee.Models;
using Newtonsoft.Json;
namespace MvcEmployee.Controllers
{
    public class EmployeeAjaxController : Controller
    {
        private readonly MvcEmployeeContext _context;

        public EmployeeAjaxController(MvcEmployeeContext context)
        {
            _context = context;
        }
        public IActionResult Index() {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'MvcEmployeeContext.Employee'  is null.");
            }

            var employees = from m in _context.Employee
                        select m;
             return View(employees);
        
        }
        //ajax 
        [HttpGet]
        public JsonResult GetDetailsById(int id) {
            var Employee = _context.Employee.Where(d => d.Id.Equals(id)).FirstOrDefault();
            JsonResponseViewModel model = new JsonResponseViewModel();
            if (Employee != null) {
                model.ResponseCode = 0;
                model.ResponseMessage = JsonConvert.SerializeObject(Employee);
            } else {
                model.ResponseCode = 1;
                model.ResponseMessage = "No record available";
            }
            return Json(model);
        }
        [HttpPost]
        public async Task<IActionResult> InsertEmployee(IFormCollection formcollection) {
            Employee employee = new Employee();
            employee.Name = formcollection["Name"];
            employee.DateofBirth= DateTime.Parse(formcollection["DateofBirth"]);
            employee.Position = formcollection["Position"];
            employee.Salary= Convert.ToDecimal(formcollection["Salary"]);
            JsonResponseViewModel model = new JsonResponseViewModel();
            //MAKE DB CALL and handle the response
            if (employee != null) {
                if (ModelState.IsValid)
                {
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                model.ResponseCode = 0;
                model.ResponseMessage = JsonConvert.SerializeObject(employee);
            } else {
                model.ResponseCode = 1;
                model.ResponseMessage = "No record available";
            }
            return Json(model);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(IFormCollection formcollection) {
            Employee employee = new Employee();
            employee.Id = Int32.Parse(formcollection["id"]);
            employee.Name = formcollection["Name"];
            employee.DateofBirth= DateTime.Parse(formcollection["DateofBirth"]);
            employee.Position = formcollection["Position"];
            employee.Salary= Convert.ToDecimal(formcollection["Salary"]);
            JsonResponseViewModel model = new JsonResponseViewModel();
            //MAKE DB CALL and handle the response
            if (employee != null) {
                if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
                model.ResponseCode = 0;
                model.ResponseMessage = JsonConvert.SerializeObject(employee);
            } else {
                model.ResponseCode = 1;
                model.ResponseMessage = "No record available";
            }
            return Json(model);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(IFormCollection formcollection) {
            Employee employee = new Employee();
            employee.Id = Int32.Parse(formcollection["id"]);
            JsonResponseViewModel model = new JsonResponseViewModel();
            //MAKE DB CALL and handle the response
            /* if (employee != null) {
                _context.Remove(employee);
                model.ResponseCode = 0;
                model.ResponseMessage = JsonConvert.SerializeObject(employee);
            } else {
                model.ResponseCode = 1;
                model.ResponseMessage = "No record available";
            } */
            if (employee != null) {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Remove(employee);
                        model.ResponseCode = 0;
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeExists(employee.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                    model.ResponseCode = 0;
                    model.ResponseMessage = JsonConvert.SerializeObject(employee);
                } else {
                    model.ResponseCode = 1;
                    model.ResponseMessage = "No record available";
                }
            return Json(model);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}