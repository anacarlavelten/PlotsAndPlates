using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlotsAndPlates.Backend.Models
{
    [Table("restaurantes")]
    public class Restaurante
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("endereco")]
        public string Endereco { get; set; } = string.Empty;

        [Column("descricao")]
        public string? Descricao { get; set; } // ? permite nulo

        [Column("foto_url")]
        public string? FotoUrl { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Propriedade de Navegação
        public List<Prato> Pratos { get; set; } = new();
    }
}