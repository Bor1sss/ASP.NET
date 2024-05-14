using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieRAZOR.IRepository;
using MVC_first;

namespace MovieRAZOR.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Movie> _repository;

        public IndexModel(IRepository<Movie> repository)
        {
            _repository = repository;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _repository.GetList();
        }
    }
}
