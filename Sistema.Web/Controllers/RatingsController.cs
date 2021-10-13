using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Artists;
using Sistema.Web.Models.Artists;

namespace Sistema.Web.Controllers
{
    //[Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,ExecutiveProducer,AsistProduccion,LineProducer,ChiefProducer,AsistGeneral")]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public RatingsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Ratings/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<RatingViewModel>> Listar()
        {
            var Rating = await _context.Ratings
                .OrderBy(o => o.artist).ThenByDescending(o => o.fecumod)
                .ToListAsync();

            return Rating.Select(r => new RatingViewModel
            {
                id = r.id,
                artistid = r.artistid,
                projectname = r.projectname,
                score = r.score,
                iduseralta = r.iduseralta,
                fecalta = r.fecalta,
                iduserumod = r.iduserumod,
                fecumod = r.fecumod,
                activo = r.activo
            });

        }

        // GET: api/Ratings/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(new RatingViewModel
            {
                id = rating.id,
                artistid = rating.artistid,
                projectname = rating.projectname,
                score = rating.score,
                iduseralta = rating.iduseralta,
                fecalta = rating.fecalta,
                iduserumod = rating.iduserumod,
                fecumod = rating.fecumod,
                activo = rating.activo
            });
        }

        // PUT: api/Ratings/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] RatingUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.id <= 0)
            {
                return BadRequest();
            }

            var fechaHora = DateTime.Now;
            var rating = await _context.Ratings.FirstOrDefaultAsync(c => c.id == model.id);

            if (rating == null)
            {
                return NotFound();
            }

            rating.projectname = model.projectname;
            rating.score = model.score;
            rating.iduserumod = model.iduserumod;
            rating.fecumod = fechaHora;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(rating);
        }

        // POST: api/Ratings/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] RatingCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Rating rating = new Rating
            {
                artistid = model.artistid,
                projectname = model.projectname,
                score = model.score,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Ratings.Add(rating);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(rating);
        }

        // DELETE: api/Ratings/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(rating);
        }

        // PUT: api/Ratings/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var rating = await _context.Ratings.FirstOrDefaultAsync(c => c.id == id);

            if (rating == null)
            {
                return NotFound();
            }

            rating.activo = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Ratings/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var rating = await _context.Ratings.FirstOrDefaultAsync(c => c.id == id);

            if (rating == null)
            {
                return NotFound();
            }

            rating.activo = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        private bool RatingExists(int id)
        {
            return _context.Ratings.Any(e => e.id == id);
        }
    }
}
