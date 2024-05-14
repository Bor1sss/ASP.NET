using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC_first;

namespace MovieRAZOR.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MVC_first.Context _context;

        public IndexModel(MVC_first.Context context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _context.Films.ToListAsync();
        }
    }
}
