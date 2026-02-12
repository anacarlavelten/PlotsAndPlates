using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlotsAndPlates.Backend.Data;
using PlotsAndPlates.Backend.Models;

namespace PlotsAndPlates.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PratosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PratosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/pratos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prato>>> GetPratos()
        {
            return await _context.Pratos.ToListAsync();
        }

        // POST: api/pratos
        [HttpPost]
        public async Task<ActionResult<Prato>> PostPrato(Prato prato)
        {
            // Verifica se o restaurante existe antes de criar o prato
            var restauranteExists = await _context.Restaurantes.AnyAsync(r => r.Id == prato.RestauranteId);
            if (!restauranteExists)
            {
                return BadRequest("Restaurante não encontrado.");
            }

            _context.Pratos.Add(prato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPratos", new { id = prato.Id }, prato);
        }
    }
}