using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EF_BD_First.Models;

namespace EF_BD_First.Views.Departamento
{
    public class IndexModel : PageModel
    {
        private readonly EF_BD_First.Models.EfDbFirstContext _context;

        public IndexModel(EF_BD_First.Models.EfDbFirstContext context)
        {
            _context = context;
        }

        public IList<Models.Departamento> Departamento { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Departamento = await _context.Departamentos.ToListAsync();
        }
    }
}
