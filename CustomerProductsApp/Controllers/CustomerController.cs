using CustomerProductsApp.DTO;
using CustomerProductsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CustomerProductsApp.Controllers
{
    public class CustomerController : Controller
    {
        private IApplicationService _services;
        public List<String> _errors = new();

        public CustomerController(IApplicationService services)
        {
            _services = services;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<CustomerShowDTO> customers = await _services.CustomerService.GetAll();
            ViewData["Customers"] = customers;
            return View();
        }
        [Authorize]
        [HttpGet]
        public  IActionResult Insert()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] CustomerInsertDTO dto)
        {
            CustomerShowDTO? result = null;
            if (!ModelState.IsValid)
            {
                foreach(var val in ModelState.Values)
                {
                    foreach(var err in val.Errors)
                    {
                        _errors.Add(err.ErrorMessage);
                        
                    }
                }
                ViewData["Errors"] = _errors;
                return View(); 
            }
            try
            {
                result = await _services.CustomerService.AddNew(dto);
                if(result is null)
                {
                    _errors.Add("failure in adding new customer");
                    ViewData["Errors"] = _errors;
                    return View();
                }
                return RedirectToAction("Index", "Customer");
            }
            catch(Exception ex)
            {
                _errors.Add(ex.Message);
                ViewData["Errors"] = _errors;
                return View();
            }
                 
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            CustomerUpdateDTO? customerUpdateDTO = null;
            try
            {
                customerUpdateDTO = await _services.CustomerService.GetById(id);
                ViewData["Customer"] = customerUpdateDTO;            
                return View(customerUpdateDTO);
            }
            catch(Exception e)
            {
                _errors.Add(e.Message);
                ViewData["Errors"] = _errors;
                return RedirectToAction("Index", "Customer");
            }

        }
        [Authorize]
        [HttpPost("Customer/Update/{id}")]
        public async Task<IActionResult> Update( CustomerUpdateDTO dto)
        {
            
            if (!ModelState.IsValid)
            {
                foreach(var val in ModelState.Values)
                {
                    foreach(var err in val.Errors)
                    {
                        _errors.Add(err.ErrorMessage);
                    }
                }
                ViewData["Errors"] = _errors;
                return View();
            }
            try
            {
                var result = await _services.CustomerService.Update(dto);
                await Console.Out.WriteLineAsync($"Updating customer {dto.Firstname} {dto.Lastname} with id {dto.Id}"  );
                return RedirectToAction("Index", "Customer");
            }
            catch(Exception e)
            {
                _errors.Add(e.Message);
                ViewData["Errors"] = _errors;
                return View();
            }

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await  _services.CustomerService.Delete(id);
                return RedirectToAction("Index", "Customer");
            }
            catch (Exception e)
            {
                _errors.Add(e.Message);
                ViewData["Errors"] = _errors;
                return RedirectToAction("Index", "Customer");
            }
        }
    }
}
