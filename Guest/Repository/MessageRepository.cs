using Guest.Models;
using Microsoft.EntityFrameworkCore;

namespace Guest.Repository
{
    public class MessageRepository : IRepositoryMessage
    {
        private readonly UserContext _context;
        public MessageRepository(UserContext context)
        {
            _context = context;
        }



        public async Task Create(Messages item)
        {
            await _context.Messages.AddAsync(item);
            _context.SaveChanges();
        }
     
        public async Task Delete(int id)
        {
            Messages? st = await _context.Messages.FindAsync(id);
            if (st != null)
                _context.Messages.Remove(st);
            _context.SaveChanges();
        }

        public async  Task<Messages> GetMessage(int id)
        {
            return await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);
        }


        public async Task<List<Messages>> GetMessageList()
        {
            var a = await _context.Messages.ToListAsync();
            var b = await _context.Users.ToListAsync();
            return a;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(Messages item)
        {
            _context.Update(item);
        }

    
    }

    public class UserRepository : IRepositoryUser
    {

        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<Users> GetUserByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == login);
        }
        public async Task Create(Users item)
        {
            await _context.Users.AddAsync(item);
            _context.SaveChanges();
        }
        public Task<List<Users>> GetAllUsers()
        {
            return _context.Users.ToListAsync();
        }
    }



    }
