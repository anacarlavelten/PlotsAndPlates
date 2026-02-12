using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlotsAndPlates.Backend.Data;
using PlotsAndPlates.Backend.Models;

namespace PlotsAndPlates.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AvaliacoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/avaliacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avaliacao>>> GetAvaliacoes()
        {
            return await _context.Avaliacoes
                .Include(a => a.Usuario) // Traz quem avaliou
                .Include(a => a.Prato)   // Traz qual prato foi avaliado
                .ToListAsync();
        }

        // POST: api/avaliacoes
        [HttpPost]
        public async Task<ActionResult<Avaliacao>> PostAvaliacao(Avaliacao avaliacao)
        {
            // Validações básicas
            if (!_context.Usuarios.Any(u => u.Id == avaliacao.UsuarioId))
                return BadRequest("Usuário inválido.");

            if (!_context.Pratos.Any(p => p.Id == avaliacao.PratoId))
                return BadRequest("Prato inválido.");

            _context.Avaliacoes.Add(avaliacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAvaliacoes", new { id = avaliacao.Id }, avaliacao);
        }
    }
}