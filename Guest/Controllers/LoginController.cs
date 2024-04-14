using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Guest.Models;
using System.Security.Cryptography;
using System.Text;
using Guest.Models.LoginRegModel;
using Guest.Repository;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Guest.Controllers
{
    public class LoginController : Controller
    {
        IRepository repo;

        public LoginController(IRepository r)
        {
            repo = r;
        }

        // GET: Register
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> ContinueByGuest()
        {
            HttpContext.Session.SetString("Login", "Guest");
            return RedirectToAction("Index", "Messages");
        }

        public async Task<IActionResult> GoToReg()
        {
            return RedirectToAction("Index", "Register");
        }
        public IActionResult Create()
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Models.LoginRegModel.LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                List<Users> b = await repo.GetAllUsers();
                if (b.Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                var users = b.Where(a => a.Name == logon.Login);
                if (users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }
                var user = users.First();
                string? salt = user.Salt;

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(logon);
                }

                HttpContext.Session.SetString("Login", user.Name);
                return RedirectToAction("Index", "Messages");
            }
            return RedirectToAction("Index");
        }



    }
}
