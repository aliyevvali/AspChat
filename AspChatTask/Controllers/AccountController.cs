using AspChatTask.DAL;
using AspChatTask.Models;
using AspChatTask.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspChatTask.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _ev;
        private readonly AppDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> manager, IWebHostEnvironment environment,AppDbContext context,SignInManager<AppUser> sign)
        {
            _userManager = manager;
            _ev = environment;
            _context = context;
            _signInManager = sign;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (login == null) return View();


            AppUser appUser = _context.Users.SingleOrDefault(x =>x.UserName == login.UserName);
            var result = await _signInManager.PasswordSignInAsync(appUser ,login.Password, true, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "login veya parolda sehvlik var!!");
                return View(login);
            }
            return RedirectToAction("Index", "Home");

        }
        
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            AppUser user = new AppUser()
            {
                FullName = register.FullName,
                UserName = register.UserName
            };
            await _userManager.CreateAsync(user,register.Password);
            string fileName = register.UserName + register.Image.FileName;
            using (FileStream stream = new FileStream(Path.Combine(_ev.WebRootPath,"img",fileName),FileMode.Create ))
            {
                register.Image.CopyTo(stream);
            }
            UserImage ui = new UserImage()
            {
                AppUser = user,
                Url = fileName
            };
            await _context.UserImages.AddAsync(ui);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
