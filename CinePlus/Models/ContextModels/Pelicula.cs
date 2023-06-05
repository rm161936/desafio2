using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinePlus.Models.ContextModels;

public partial class Pelicula
{
    public int Id { get; set; }

    public byte[]? PosterImg { get; set; }

    public string? Titulo { get; set; }

    public string? Sinopsis { get; set; }

    public string? Director { get; set; }

    public int? IdGenero { get; set; }

    public virtual Genero? Genero { get; set; }

    public virtual Puntuacion? Puntuacion { get; set; }
    [NotMapped]
    public IFormFile? Img { get; set; }
}