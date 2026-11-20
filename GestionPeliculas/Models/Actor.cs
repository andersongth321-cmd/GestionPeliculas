using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPeliculas.Models
{
    [Table("Actores")]
    public class Actor
    {
        [Key]
        [Column("actor_id")]
        public int ActorId { get; set; }

        [Required]
        [StringLength(80)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(80)]
        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Column("fecha_nacimiento")]
        public DateOnly? FechaNacimiento { get; set; }

        [StringLength(80)]
        [Column("nacionalidad")]
        public string? Nacionalidad { get; set; }

        public ICollection<PeliculaActor> PeliculaActores { get; set; } = new List<PeliculaActor>();
    }
}