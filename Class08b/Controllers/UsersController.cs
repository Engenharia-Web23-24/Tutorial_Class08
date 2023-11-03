﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Class08b.Data;
using Class08b.Models;

namespace Class08b.Controllers
{
    public class UsersController : Controller
    {
        private readonly Class08bContext _context;

        public UsersController(Class08bContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                User u = _context.User.SingleOrDefault(u => u.Username == username && u.Password == password);
                if (u == null)
                    ModelState.AddModelError("username", "username or password are wrong");
                else
                {
                    // the user is authenticated
                    // the session variable "user" is created to recover the user identify at each request
                    HttpContext.Session.SetString("user", username);

                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete(".Class08b.Session");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Preferences()
        {
            ViewBag.mode = HttpContext.Request.Cookies["viewMode"] ?? "light";

            return View();
        }
        [HttpPost]
        public IActionResult Preferences(string mode)
        {

            HttpContext.Response.Cookies.Append("viewMode", mode, new CookieOptions { Expires = DateTime.Now.AddYears(1) });
            return RedirectToAction("Index", "Home");
        }
    }
}
