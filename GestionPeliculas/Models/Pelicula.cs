using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPeliculas.Models
{
    [Table("Peliculas")]
    public class Pelicula
    {
        [Key]
        [Column("pelicula_id")]
        public int PeliculaId { get; set; }

        [Required]
        [StringLength(150)]
        [Column("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(80)]
        [Column("genero")]
        public string? Genero { get; set; }

        [Column("año")]
        public int? Anio { get; set; }

        [StringLength(120)]
        [Column("director")]
        public string? Director { get; set; }

        public ICollection<PeliculaActor> PeliculaActores { get; set; } = new List<PeliculaActor>();
    }
}