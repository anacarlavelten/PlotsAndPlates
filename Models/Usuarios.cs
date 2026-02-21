using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlotsAndPlates.Backend.Models
{
    [Table("usuarios")] // Mapeia para a tabela exata no Postgres
    public class Usuario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("senha_hash")]
        public string SenhaHash { get; set; } = string.Empty;

        [Column("tipo")]
        public TipoUsuario Tipo { get; set; } = TipoUsuario.visitante;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}