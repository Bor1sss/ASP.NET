using System; 
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using UserPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.DTO.LoginRegDTO;
using MusicPortal.ViewModels;

namespace Controllers
{
    [Culture]
    public class LoginController : Controller
    {
        IUserService repo;

        public LoginController(IUserService r)
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
            return RedirectToAction("Index", "MusicModels");
        }

        public async Task<IActionResult> GoToReg()
        {
            return RedirectToAction("Index", "Register");
        }
        public async Task<IActionResult> GoToAdminPanel()
        {
            return RedirectToAction("Index", "AdminPanel");
        }
        public IActionResult BackToLog()
        {
            HttpContext.Session.SetString("Login", "");
            return RedirectToAction("Index", "Login");
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
        public async Task<IActionResult> Login(LoginDTO logon)
        {
            if (ModelState.IsValid)
            {
                List<UserDTO> b = (await repo.GetUsers()).ToList();
                if (b.Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return RedirectToAction("Index");
                }
                var users = b.Where(a => a.Name == logon.Login);
                if (users.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return RedirectToAction("Index");
                }
                var user = users.First();
                string? salt = user.Salt;


                byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);

               
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return RedirectToAction("Index");
                }
                if (user.Name != "Admin")
                {
                    ViewBag.Name = user.Name;
                    HttpContext.Session.SetString("Login", user.Name);
                    return RedirectToAction("Index", "MusicModels");
                }
                else
                {
                    ViewBag.Name = user.Name;
                    HttpContext.Session.SetString("Login", user.Name);
                    return RedirectToAction("Index", "AdminPanel");
                }
            }
            return RedirectToAction("Index");
        }



    }
}
