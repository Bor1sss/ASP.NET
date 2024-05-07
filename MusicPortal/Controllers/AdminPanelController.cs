using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MusicPortal.BLL.Interfaces;
using MusicPortal.ViewModels;
using UserPortal.BLL.Interfaces;


namespace MusicPortal.Controllers
{
    [Culture]
    public class AdminPanelController : Controller
    {
        readonly ILangRead _langRead;
        IUserService repoU;
        public AdminPanelController(IUserService u, ILangRead langRead)
        {
           repoU = u;
            _langRead = langRead;
        }

        public async Task<IActionResult> Verify(int? id)
        {

            var verified = repoU.GetUser(id.Value);

            verified.Result.isVerified = true;
            repoU.Save();
            return RedirectToAction("Index", "AdminPanel");
        }
        public async Task<IActionResult> Index()
        {

            var login = HttpContext.Session.GetString("Login");

            if (login != "")
            {
                ViewBag.Name = login;
                return View(repoU.GetUsers().Result);
            }
            else
            {
                HttpContext.Session.SetString("Login", "");
                return RedirectToAction("Index", "Login");
            }
     
        }
        public ActionResult ChangeCulture(string lang)
        {
            string? returnUrl = HttpContext.Session.GetString("path") ?? "/Login/Index";

            // Список культур
            List<string> cultures = _langRead.languageList().Select(t => t.ShortName).ToList()!;
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10); // срок хранения куки - 10 дней
            Response.Cookies.Append("lang", lang, option); // создание куки
            return Redirect(returnUrl);
        }
        // GET: AdminPanel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = repoU.GetUser(id.Value);
               
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: AdminPanel/Create
        public IActionResult Create()
        {
            return View();
        }




        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = repoU.GetUser(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: AdminPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          
                repoU.DeleteUser(id);
                repoU.Save();

         
            return RedirectToAction(nameof(Index));
        }

     
    }
}
