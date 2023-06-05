using CinePlus.Models.ContextModels;
using CinePlus.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinePlus.Controllers
{
    public class PuntuacionController : Controller
    {
        private readonly CinePlusContext _context;
        public PuntuacionController(CinePlusContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {


            List<PuntuacionViewModel> data = _context.Puntuacions
                .GroupBy(p => p.IdPelicula)
                .Select(g => new PuntuacionViewModel()
                {
                    Id = (int)g.Key,
                    NombrePelicula = g.First().Pelicula.Titulo,
                    Promedio = Math.Round((decimal)g.Average(p => p.Calificacion), 2)
                })
                .OrderByDescending(p => p.Promedio)
                .Take(5)
                .ToList();

            return View(data);
        }
    }
}
