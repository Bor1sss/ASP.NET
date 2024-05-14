using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieRAZOR.IRepository;
using MVC_first;
using NuGet.Protocol.Core.Types;

namespace MovieRAZOR.Repository
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly Context _context;

        public MovieRepository(Context context)
        {
            _context = context;
        }

        public async Task Attach(Movie item)
        {
            _context.Attach(item).State = EntityState.Modified;
        }

        public async Task Create(Movie item)
        {
            await _context.Films.AddAsync(item);
        }

        public async Task Delete(int id)
        {
            var movie = await GetById(id);
            if (movie != null)
            {
                _context.Films.Remove(movie);
            }
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Films.AnyAsync(m => m.Id == id);
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Films.FindAsync(id);
        }

        public async Task<Movie> GetByName(string name)
        {
            return await _context.Films.FirstOrDefaultAsync(m => m.Title == name);
        }

        public async Task<List<Movie>> GetList()
        {
           return await _context.Films.ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public Task Update(Movie item)
        {
            _context.Films.Update(item);
            return Task.CompletedTask;

        }
    }
}
