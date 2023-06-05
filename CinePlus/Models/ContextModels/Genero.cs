using System;
using System.Collections.Generic;

namespace CinePlus.Models.ContextModels;

public partial class Genero
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Pelicula> Peliculas { get; set; } = new List<Pelicula>();
}
