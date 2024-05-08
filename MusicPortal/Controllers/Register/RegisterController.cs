using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UserPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.ViewModels;


namespace Controllers
{
    [Culture]
    public class RegisterController : Controller
    {
        IUserService repo;

        public RegisterController(IUserService r)
        {
            repo = r;
        }

        // GET: Register
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        }

        public async Task<IActionResult> ContinueByGuest()
        {
            HttpContext.Session.SetString("Login", "Guest");
            return RedirectToAction("Index", "Messages");
        }

        public async Task<IActionResult> GoToLogon()
        {
            return RedirectToAction("Index", "Login");
        }

        // GET: Register/Create
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
        public async Task<IActionResult> Register(RegisterDTO reg)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO();
                user.Name = reg.Login;

                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                user.Password = hash.ToString();
                user.Salt = salt;
                repo.CreateUser(user);
                if (user.Name != "Admin")
                {
                    HttpContext.Session.SetString("Login", user.Name);
                    return RedirectToAction("Index", "MusicModels");
                }
                else
                {
                    HttpContext.Session.SetString("Login", user.Name);
                    return RedirectToAction("Index", "AdminPanel");
                }
            }
            return RedirectToAction("Index");
        }


     
    }
}
