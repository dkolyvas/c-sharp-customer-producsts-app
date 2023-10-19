using CustomerProductsApp.DTO;
using CustomerProductsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerProductsApp.Controllers
{
    public class ProductController : Controller
    {
        private IApplicationService _services;
        public List<String> Errors = new();

        public ProductController(IApplicationService services)
        {
            _services = services;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<ProductShowDTO> products = await _services.ProductService.GetAll();
                ViewData["Products"] = products;
                return View();
            }catch(Exception e)
            {
                Errors.Add(e.Message);
                ViewData["Errors"] = Errors;
                return View();
            }
        }
        [Authorize]
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] ProductInsertDTO dto)
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
                var result = await _services.ProductService.AddNew(dto);
                if (result is null)
                { 
                    Errors.Add("failure in adding new customer");
                    ViewData["Errors"] = Errors;
                    return View();
                }
                return RedirectToAction("Index", "Product");
            }
            catch(Exception e)
            {
                Errors.Add(e.Message);
                ViewData["Errors"] = Errors;
                return View();
            }

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _services.ProductService.Delete(id);
                return RedirectToAction("Index", "Product");
            }
            catch(Exception e)
            {
                Errors.Add(e.Message);
                ViewData["Errors"] = Errors;
                return RedirectToAction("Index", "Product");
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                ProductUpdateDTO product = await _services.ProductService.GetById(id);
                ViewData["Product"] = product;
                return View(product);
            }
            catch(Exception e)
            {
                Errors.Add(e.Message);
                ViewData["Errors"] = Errors;
                return RedirectToAction("Index", "Product");
            }
        }
        [Authorize]
        [HttpPost("Product/Update/{id}")]
        public async Task<IActionResult> Update( ProductUpdateDTO dto)
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
                var result = await _services.ProductService.Update(dto);
                return RedirectToAction("Index", "Product");
            }
            catch(Exception e)
            {
                Errors.Add(e.Message);
                ViewData["Errors"] = Errors;
                return View();
            }
        }
    }
}
