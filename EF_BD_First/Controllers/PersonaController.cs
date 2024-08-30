using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EF_BD_First.Models;
using Microsoft.Extensions.Options;

namespace EF_BD_First.Controllers
{ 
    [Route("[controller]")]
    public class PersonaController : Controller
    {
        private readonly EfDbFirstContext _context;

        public PersonaController(EfDbFirstContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: Persona
        public async Task<IActionResult> Index()
        {
            try
            {
                var efDbFirstContext = _context.Personas.Include(p => p.Departamento);
                return View(await efDbFirstContext.ToListAsync());

            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpGet("Details/{id}")]
        // GET: Persona/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .Include(p => p.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        [HttpGet("Create")]
        // GET: Persona/Create
        public IActionResult Create()
        {
            var departamentos = _context.Departamentos.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Nombre
            }).ToList();

            // Populate ViewData with the list of departments
            ViewBag.Departamentos = departamentos;
            return View();
        }

        // POST: Persona/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Edad,DepartamentoId")] Persona persona)
        {

            if (ModelState.ContainsKey("Departamento"))
            {
                ModelState.Remove("Departamento");
            }

            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Id", persona.DepartamentoId);
            return View(persona);
        }

        [HttpGet("Edit/{id}")]
        // GET: Persona/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            var departamentos = await _context.Departamentos.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Nombre
            }).ToListAsync();

            // Populate ViewData with the list of departments
            ViewBag.Departamentos = departamentos;
            return View(persona);
        }

        [HttpPost("Edit/{id}")]
        // POST: Persona/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Edad,DepartamentoId")] Persona persona)
        {
            if (id != persona.Id)
            {
                return NotFound();
            }

            if (ModelState.ContainsKey("Departamento"))
            {
                ModelState.Remove("Departamento");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Id))
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
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Id", "Id", persona.DepartamentoId);
            return View(persona);
        }

        [HttpGet("Delete/{id}")]
        // GET: Persona/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .Include(p => p.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Persona/Delete/5
        // [HttpPost("Delete/{id}")]
        //[HttpPost]
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.Id == id);
        }
    }
}
