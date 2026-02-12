using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlotsAndPlates.Backend.Models
{
    [Table("pratos")]
    public class Prato
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("restaurante_id")]
        public int RestauranteId { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("tipo")] // tipo do prato
        public string Tipo { get; set; } = string.Empty;

        [Column("descricao")]
        public string? Descricao { get; set; }

        [Column("foto_url")]
        public string? FotoUrl { get; set; }

        [Column("preco")]
        public decimal Preco { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // navegação para voltar ao Restaurante
        [ForeignKey("RestauranteId")]
        public Restaurante? Restaurante { get; set; }

        // um prato pode ter várias avaliações
        public List<Avaliacao> Avaliacoes { get; set; } = new();
    }
}