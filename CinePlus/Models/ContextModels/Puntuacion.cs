using System;
using System.Collections.Generic;

namespace CinePlus.Models.ContextModels;

public partial class Puntuacion
{
    public int Id { get; set; }

    public int? Calificacion { get; set; }

    public int? IdPelicula { get; set; }

    public virtual Pelicula Pelicula { get; set; } = null!;
}
