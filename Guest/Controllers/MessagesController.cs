using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Guest.Models;
using Guest.Repository;
using Newtonsoft.Json;

namespace Guest.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IRepositoryMessage _messageRepository;
        private readonly IRepositoryUser _userRepository;

        public MessagesController(IRepositoryMessage messageRepository, IRepositoryUser userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
      
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            List<Messages> list = await _messageRepository.GetMessageList();
            
            //string response = JsonConvert.SerializeObject(list);
            
            return Json(list);
        }
        public IActionResult BackToLog()
        {
            HttpContext.Session.SetString("Login", "");
            return RedirectToAction("Index", "Register");
        }
        public IActionResult GetSessionValue(string key)
        {
            var sessionValue = HttpContext.Session.GetString(key);
            return Ok(sessionValue); // Возвращаем значение сессии
        }


        [HttpPost]
        public async Task<IActionResult> Create(string message)
        {
            if (message == null)
            {
                return BadRequest("Message cannot be null");
            }

            var userLogin = HttpContext.Session.GetString("Login");
            var user = await _userRepository.GetUserByLoginAsync(userLogin);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var newMessage = new Messages
            {
                Message = message,
                MessageDate = DateTime.Now,
                User = user
            };

            await _messageRepository.Create(newMessage);
            await _messageRepository.Save();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            List<Messages> list = await _messageRepository.GetMessageList();
             
            if (!(list?.Any(e => e.Id == id)).GetValueOrDefault())
            {
                return NotFound("Message not found");
            }

            await _messageRepository.Delete(id);
            return Ok();
        }
    }
}
