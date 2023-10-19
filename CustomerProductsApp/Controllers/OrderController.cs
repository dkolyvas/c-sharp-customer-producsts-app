using CustomerProductsApp.Data;
using CustomerProductsApp.DTO;
using CustomerProductsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerProductsApp.Controllers
{
    public class OrderController : Controller
    {
        List<string> Errors = new();
        private IApplicationService _services;

        public OrderController(IApplicationService services)
        {
            _services = services;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(int customerid)
        {
            List<OrderShowDTO> orders = new();
            try
            {
                orders = await _services.OrderService.GetByCustomerId(customerid);
                ViewData["CustomerId"] = customerid;
                ViewData["Orders"] = orders;
                return View();
            }
            catch(Exception e)
            {
                Errors.Add(e.Message);
                ViewData["Errors"] = Errors;
                return View();
            }
        }
        [Authorize]
        [HttpGet("/Order/Customer/{customerid}/Insert")]
        public async Task<IActionResult> Insert(int customerid)
        {
            try
            {
                CustomerUpdateDTO? customer = await _services.CustomerService.GetById(customerid);
                List<ProductShowDTO> products = await _services.ProductService.GetAll();
                ViewData["Customer"] = customer.Firstname + " " + customer.Lastname;
                ViewData["Products"] = products;
                return View();
            }
            catch (Exception e)
            {
                Errors.Add(e.Message);
                ViewData["Errors"] = Errors;
                return View();

            }
        }
        [Authorize]
        [HttpPost("/Order/Customer/{customerid}/Insert")]
        public async Task<IActionResult> Insert(OrderInsertDTO dto)
        {
            if (!ModelState.IsValid)
            {
                foreach(var val in ModelState.Values)
                {
                    foreach(var err in val.Errors)
                    {
                        Errors.Add(err.ErrorMessage);
                    }
                }
                ViewData["Errors"] = Errors;
                return View();
            }
            try
            {
                await _services.OrderService.AddNew(dto);
                return RedirectToAction("Index", "Order",new {customerid = dto.CustomerId});
            }
            catch (Exception e)
            {
                Errors.Add(e.Message);
                ViewData["Errors"] = Errors;
                return View();

            }

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                OrderUpdateDTO order = await _services.OrderService.GetById(id);
                CustomerUpdateDTO? customer = await _services.CustomerService.GetById(order.CustomerId);
                List<ProductShowDTO> products = await _services.ProductService.GetAll();
                ViewData["Customer"] = customer.Firstname + " " + customer.Lastname;
                ViewData["Products"] = products;
                return View(order);
            }
            catch (Exception e)
            {
                Errors.Add(e.Message);
                ViewData["Errors"] = Errors;
                return View();

            }

        }
        [Authorize]
        [HttpPost ("/Order/Update/{id}")]
        public async Task<IActionResult> Update(OrderUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                foreach (var val in ModelState.Values)
                {
                    foreach (var err in val.Errors)
                    {
                        Errors.Add(err.ErrorMessage);
                    }
                }
                ViewData["Errors"] = Errors;
                return View();
            }
            try
            {
                await _services.OrderService.Update(dto);
                return RedirectToAction("Index", "Order", new { customerid = dto.CustomerId });
            }
            catch (Exception e)
            {
                Errors.Add(e.Message);
                ViewData["Errors"] = Errors;
                return View();

            }

        }
        [Authorize]
        [HttpGet("/Order/Delete/{id}/Customer/{customerid}")]
        public async Task<IActionResult> Delete(int id, int customerid)
        {
            try
            {
                await _services.OrderService.Delete(id);
                return RedirectToAction("Index", "Order", new { customerid = customerid });
            }
            catch(Exception e)
            {
                return RedirectToAction("Index", "Order", new { customerid = customerid });
            }
        }

    }
}
