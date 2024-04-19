using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Models;
using MusicPortal.Models.MusicModel;
using MusicPortal.Models.User;
using NuGet.Protocol.Plugins;
using Repository;

namespace MusicPortal.Controllers
{
    public class AdminPanelController : Controller
    {
    
        IRepositoryUser repoU;
        public AdminPanelController(IRepositoryUser u)
        {
           repoU = u;
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
                return View(repoU.GetAllUsers().Result);
            }
            else
            {
                HttpContext.Session.SetString("Login", "");
                return RedirectToAction("Index", "Login");
            }
     
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
          
                repoU.Delete(id);
                repoU.Save();

         
            return RedirectToAction(nameof(Index));
        }

     
    }
}
