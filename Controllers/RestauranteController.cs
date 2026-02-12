using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlotsAndPlates.Backend.Data;
using PlotsAndPlates.Backend.Models;

namespace PlotsAndPlates.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RestaurantesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/restaurantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurante>>> GetRestaurantes()
        {
            return await _context.Restaurantes.ToListAsync();
        }

        // GET: api/restaurantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurante>> GetRestaurante(int id)
        {
            // Busca o restaurante e JÁ CARREGA os pratos dele (Include)
            var restaurante = await _context.Restaurantes
                .Include(r => r.Pratos) 
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurante == null)
            {
                return NotFound();
            }

            return restaurante;
        }

        // POST: api/restaurantes
        [HttpPost]
        public async Task<ActionResult<Restaurante>> PostRestaurante(Restaurante restaurante)
        {
            _context.Restaurantes.Add(restaurante);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurante", new { id = restaurante.Id }, restaurante);
        }
    }
}