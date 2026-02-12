using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlotsAndPlates.Backend.Models
{
    [Table("avaliacoes")]
    public class Avaliacao
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Column("prato_id")]
        public int PratoId { get; set; }

        [Column("nota")]
        public int Nota { get; set; } // de 1 a 5

        [Column("comentario")]
        public string? Comentario { get; set; }

        [Column("data_visita")]
        public DateTime DataVisita { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navegação
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }

        [ForeignKey("PratoId")]
        public Prato? Prato { get; set; }
    }
}