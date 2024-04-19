using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Guest.Models;
using Guest.Repository;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Core.Types;

namespace Guest.Controllers
{
    public class MessagesController : Controller
    {

        IRepositoryMessage repo;
      
        IRepositoryUser repoU;

        public MessagesController(IRepositoryMessage r, IRepositoryUser repoU)
        {
            repo = r;
            this.repoU = repoU;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
         
            var model = await repo.GetMessageList();

            return View(new CombinedMessages
            {
                Messages = model
            });
        }

        

       public IActionResult BackToLog()
        {
            HttpContext.Session.SetString("Login", "");
            return RedirectToAction("Index", "Register");
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string message)
        {
            if (message == null)
            {
                ModelState.AddModelError("", "Сообщение должно быть не null");
            }
            Messages mes = new Messages();
            mes.Message= message;   
            mes.MessageDate= DateTime.Now;
            var login = HttpContext.Session.GetString("Login");
          
            mes.User= await repoU.GetUserByLoginAsync(login);
            
            if (ModelState.IsValid)
            {
           
                await repo.Create(mes);
                await repo.Save();
               
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             await repo.Delete(id);
          
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MessagesExists(int id)
        {
            List<Messages> list = await repo.GetMessageList();
            return (list?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
