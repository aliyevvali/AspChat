using AspChatTask.DAL;
using AspChatTask.Models;
using AspChatTask.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspChatTask.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Users = _context.Users.Where(x => x.UserName != User.Identity.Name).Include(x => x.Image),
                CurrentUser = _context.Users.SingleOrDefault(x => x.UserName == User.Identity.Name)
            };
            return View(homeVM);
        }   

        
    }
}
