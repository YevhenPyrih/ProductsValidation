using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ProductsValidation.Models;
using ProductsValidation.Services;

namespace ProductsValidation.Controllers
{
    public class UsersController : Controller
    {
        private List<User> users;

        public UsersController(Data data)
        {
            users = data.Users;
        }
        
        public IActionResult Index(int id)
        {
            return View("Index", users);
        }

        public IActionResult View(int id)
        {
            User user = users.Find(user => user.Id == id);
            if (user != null)
            {
                return View(user);
            }

            return View("NotExists");
        }

        [HttpGet]
        public IActionResult Add([FromQuery] User user)
        {
            return View(user);
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult AddPost([FromQuery] User user)
        {
            if (ModelState.IsValid)
            {
                users.Add(user);
                return RedirectToAction(nameof(View), new {id = user.Id});
            }
            return View("BadQuery");
        }
    }
}
