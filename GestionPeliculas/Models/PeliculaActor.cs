using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionPeliculas.Models
{
    [Table("Pelicula_Actor")]
    public class PeliculaActor
    {
        [Column("pelicula_id")]
        public int PeliculaId { get; set; }
        public Pelicula? Pelicula { get; set; }

        [Column("actor_id")]
        public int ActorId { get; set; }
        public Actor? Actor { get; set; }

        [StringLength(120)]
        [Column("personaje")]
        public string? Personaje { get; set; }
    }
}
