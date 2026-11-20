using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionPeliculas.Data;
using GestionPeliculas.Models;

namespace GestionPeliculas.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly AppDbContext _context;

        public PeliculasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var peliculas = await _context.Peliculas
                .Include(p => p.PeliculaActores)
                    .ThenInclude(pa => pa.Actor)
                .ToListAsync();

            return View(peliculas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pelicula);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null) return NotFound();

            return View(pelicula);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pelicula pelicula)
        {
            if (id != pelicula.PeliculaId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pelicula);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pelicula = await _context.Peliculas.FirstOrDefaultAsync(p => p.PeliculaId == id);
            if (pelicula == null) return NotFound();

            return View(pelicula);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Asignar actor a película (relación N:M)
        public async Task<IActionResult> AsignarActor(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null) return NotFound();

            var actores = await _context.Actores
                .Select(a => new
                {
                    a.ActorId,
                    NombreCompleto = a.Nombre + " " + a.Apellido
                })
                .ToListAsync();

            ViewBag.Pelicula = pelicula;
            ViewBag.Actores = new SelectList(actores, "ActorId", "NombreCompleto");

            return View(new PeliculaActor { PeliculaId = pelicula.PeliculaId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarActor(PeliculaActor relacion)
        {
            var existe = await _context.PeliculaActores
                .AnyAsync(pa => pa.PeliculaId == relacion.PeliculaId && pa.ActorId == relacion.ActorId);

            if (!existe)
            {
                _context.PeliculaActores.Add(relacion);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}