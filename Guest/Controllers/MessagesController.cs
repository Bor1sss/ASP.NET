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

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await repo.GetMessageList() == null)
            {
                return NotFound();
            }
            var message = await repo.GetMessage((int)id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Message,MessageDate")] Messages messages)
        {
            if (id != messages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repo.Update(messages);
                    await repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MessagesExists(messages.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(messages);
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
