using Chat.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MusicPortal.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Controllers
{
    /*
    Ключевой сущностью в SignalR, через которую клиенты обмениваются сообщеними 
    с сервером и между собой, является хаб (hub). 
    Хаб представляет некоторый класс, который унаследован от абстрактного класса 
    Microsoft.AspNetCore.SignalR.Hub.
    */
    public class ChatHub : Hub
    {
        private readonly ChatContext _context;

        public ChatHub(ChatContext context)
        {
            _context = context;
        }

        // Отправка сообщений
        public async Task Send(string username, string message)
        {
            var user = _context.Users.FirstOrDefault(x => x.Name == username);

            if (user != null)
            {
                Message newMessage = new Message
                {
                    Text = message,
                    User = user,
                    dateTime = DateTime.Now
                };

                _context.Messages.Add(newMessage);

                // Получение последнего добавленного сообщения по времени добавления
                var lastMessage = _context.Messages.OrderByDescending(m => m.dateTime).FirstOrDefault();

                if (lastMessage != null)
                {
                    user.Message.Add(lastMessage);
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Обработка ошибок сохранения в базу данных
                    // Например, запись в лог или отправка уведомления об ошибке
                    Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
                    throw;
                }

                await Clients.All.SendAsync("AddMessage", username, message);
            }
        }

        // Подключение нового пользователя
        public async Task Connect(string userName)
        {
            var id = Context.ConnectionId;

            
            var user = _context.Users.FirstOrDefault(x => x.ConnectionId == id);

            if (user == null)
            {
                user = new User { ConnectionId = id, Name = userName, IsLoggedIn = true };
                
                _context.Users.Add(user);
                _context.SaveChanges();


                // Добавление в группу "LoggedInUsers"
                await Groups.AddToGroupAsync(id, "LoggedInUsers");

                
                var userList = _context.Users.ToList();

                var messageList = _context.Messages.ToList();
                

                await Clients.Caller.SendAsync("Connected", id, userName, userList, messageList);
            }

            // Отправка сообщения только тем, кто в группе "LoggedInUsers"
            await Clients.Group("LoggedInUsers").SendAsync("NewUserConnected", id, userName);
        }

        // OnDisconnectedAsync срабатывает при отключении клиента.
        // В качестве параметра передается сообщение об ошибке, которая описывает,
        // почему произошло отключение.
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            
            var item = _context.Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
 
            if (item != null)
            {
                //item.IsLoggedIn = false;

                _context.Users.Remove(item);
                _context.SaveChanges();

                var id = Context.ConnectionId;
                // Вызов метода UserDisconnected на всех клиентах
                await Clients.All.SendAsync("UserDisconnected", id, item.Name);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
