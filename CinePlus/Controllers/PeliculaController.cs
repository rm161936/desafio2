using CinePlus.Models.ContextModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace CinePlus.Controllers
{
    public class PeliculaController : Controller
    {
        private readonly CinePlusContext _context;

        public PeliculaController(CinePlusContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page, string cadenaBusqueda)
        {
            ViewData["CurrentFilter"] = cadenaBusqueda;
            var pageNumber = page ?? 1;

            var peliculas = from s in _context.Peliculas
                           select s;

            if (!String.IsNullOrEmpty(cadenaBusqueda))
            {
                peliculas = peliculas.Where(p => p.Titulo.Contains(cadenaBusqueda)
                                       || p.Genero.Nombre.Contains(cadenaBusqueda));
            }

            

            return View(await peliculas.ToPagedListAsync(pageNumber, 5));
        }

        public ActionResult Create()
        {
            ViewBag.Generos = new SelectList(_context.Generos.OrderBy(e => e.Id), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Pelicula model)
        {
            ViewBag.Generos = new SelectList(_context.Generos.OrderBy(e => e.Id), "Id", "Nombre");
            try
            {
                if (model.Img != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        model.Img.CopyTo(memoryStream);
                        model.PosterImg = memoryStream.ToArray();
                    }
                }
                _context.Peliculas.Add(model);
                _context.SaveChanges();
            }
            catch(Exception) { }

            return View();
        }

        [HttpPost]
        public int AddRanking(int id, int rating)
        {
            int response;
            try
            {
                Puntuacion puntuacion = new()
                {
                    IdPelicula = id,
                    Calificacion = rating
                };
                _context.Puntuacions.Add(puntuacion);
                _context.SaveChanges();
                response = 1;
            }
            catch (Exception ex) { response = 0; }

            return response;
        }


    }
}
