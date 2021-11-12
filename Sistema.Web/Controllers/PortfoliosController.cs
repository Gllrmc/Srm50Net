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
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,ExecutiveProducer,AsistProduccion,LineProducer,ChiefProducer,AsistGeneral")]
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public PortfoliosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Portfolios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<PortfolioViewModel>> Listar()
        {
            var Portfolio = await _context.Portfolios
                .OrderBy(o => o.artist).ThenByDescending(o => o.fecumod)
                .ToListAsync();

            return Portfolio.Select(r => new PortfolioViewModel
            {
                id = r.id,
                artistid = r.artistid,
                url = r.url,
                iduseralta = r.iduseralta,
                fecalta = r.fecalta,
                iduserumod = r.iduserumod,
                fecumod = r.fecumod,
                activo = r.activo
            });

        }

        // GET: api/Portfolios/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var portfolio = await _context.Portfolios.FindAsync(id);

            if (portfolio == null)
            {
                return NotFound();
            }

            return Ok(new PortfolioViewModel
            {
                id = portfolio.id,
                artistid = portfolio.artistid,
                url = portfolio.url,
                iduseralta = portfolio.iduseralta,
                fecalta = portfolio.fecalta,
                iduserumod = portfolio.iduserumod,
                fecumod = portfolio.fecumod,
                activo = portfolio.activo
            });
        }

        // PUT: api/Portfolios/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] PortfolioUpdateModel model)
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
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(c => c.id == model.id);

            if (portfolio == null)
            {
                return NotFound();
            }

            portfolio.url = model.url;
            portfolio.iduserumod = model.iduserumod;
            portfolio.fecumod = fechaHora;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(portfolio);
        }

        // POST: api/Portfolios/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] PortfolioCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Portfolio portfolio = new Portfolio
            {
                artistid = model.artistid,
                url = model.url,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Portfolios.Add(portfolio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(portfolio);
        }

        // DELETE: api/Portfolios/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio == null)
            {
                return NotFound();
            }

            _context.Portfolios.Remove(portfolio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(portfolio);
        }

        // PUT: api/Portfolios/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(c => c.id == id);

            if (portfolio == null)
            {
                return NotFound();
            }

            portfolio.activo = false;

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

        // PUT: api/Portfolios/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(c => c.id == id);

            if (portfolio == null)
            {
                return NotFound();
            }

            portfolio.activo = true;

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

        private bool PortfolioExists(int id)
        {
            return _context.Portfolios.Any(e => e.id == id);
        }
    }
}
