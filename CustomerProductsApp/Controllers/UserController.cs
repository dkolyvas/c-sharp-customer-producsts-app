using AutoMapper;
using CustomerProductsApp.Data;
using CustomerProductsApp.DTO;
using CustomerProductsApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CustomerProductsApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public List<String> Errors = new();
        public UserController(IApplicationService applicationService, IMapper mapper, IConfiguration configuration)
        {
            _applicationService = applicationService;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal principal = HttpContext.User;
            if (principal.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login( UserLoginDTO credentials)
        {
            User? user  =null;
            
            if (credentials is not null) 
            {
                user = await _applicationService.UserService.Login(credentials);
            }
            if(user is null)
            {
                string error = "Wrong username/password";
                ViewData["Error"] = error;
                return View();
            }
            List<Claim> claims = new();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, credentials.Email));
            
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new()
            {
                AllowRefresh = true,
                IsPersistent = true
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity),
                properties);
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost] public async Task<IActionResult> Register([FromForm] UserRegisterDTO dto)
        {
            if(!ModelState.IsValid)
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
                UserShowDTO user = await _applicationService.UserService.RegisterUser(dto);
             }
            catch(Exception e)
            {
                Errors.Add(e.Message);
                ViewData["Errors"] = Errors;
                return View();
            }
            return RedirectToAction("Index", "Home");

        }
        [Authorize]
        public IActionResult Update()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDTO dto)
        {
            dto.Email = HttpContext.User.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.NameIdentifier)?.Value;
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
                await _applicationService.UserService.UpdateUser(dto);
            }
            catch (Exception e)
            {
                Errors.Add(e.Message);
                ViewData["Errors"] = Errors;
                return View();
            }
            return RedirectToAction("Index", "Home");

        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "User");
        }
    }

    
}
