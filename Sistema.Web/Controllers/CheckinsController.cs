using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Checkins;
using Sistema.Web.Models.Checkins;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,ExecutiveProducer,AsistProduccion,LineProducer,ChiefProducer,AsistGeneral")]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckinsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public CheckinsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Checkins/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CheckinViewModel>> Listar()
        {
            var Checkin = await _context.Checkins.ToListAsync();

            return Checkin.Select(r => new CheckinViewModel
            {
                id = r.id,
                checkin = r.checkin,
                detail = r.detail,
                iduseralta = r.iduseralta,
                fecalta = r.fecalta,
                iduserumod = r.iduserumod,
                fecumod = r.fecumod,
                activo = r.activo
            });

        }

        // GET: api/Checkins/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var checkin = await _context.Checkins.FindAsync(id);

            if (checkin == null)
            {
                return NotFound();
            }

            return Ok(new CheckinViewModel
            {
                id = checkin.id,
                checkin = checkin.checkin,
                detail = checkin.detail,
                iduseralta = checkin.iduseralta,
                fecalta = checkin.fecalta,
                iduserumod = checkin.iduserumod,
                fecumod = checkin.fecumod,
                activo = checkin.activo
            });
        }

        // PUT: api/Checkins/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] CheckinUpdateModel model)
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
            var checkin = await _context.Checkins.FirstOrDefaultAsync(c => c.id == model.id);

            if (checkin == null)
            {
                return NotFound();
            }

            checkin.checkin = model.checkin;
            checkin.detail = model.detail;
            checkin.iduserumod = model.iduserumod;
            checkin.fecumod = fechaHora;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(checkin);
        }

        // POST: api/Checkins/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CheckinCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Checkin checkin = new Checkin
            {
                checkin = model.checkin,
                detail = model.detail,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Checkins.Add(checkin);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(checkin);
        }

        // DELETE: api/Checkins/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkin = await _context.Checkins.FindAsync(id);
            if (checkin == null)
            {
                return NotFound();
            }

            _context.Checkins.Remove(checkin);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(checkin);
        }

        // PUT: api/Checkins/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var checkin = await _context.Checkins.FirstOrDefaultAsync(c => c.id == id);

            if (checkin == null)
            {
                return NotFound();
            }

            checkin.activo = false;

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

        // PUT: api/Checkins/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var checkin = await _context.Checkins.FirstOrDefaultAsync(c => c.id == id);

            if (checkin == null)
            {
                return NotFound();
            }

            checkin.activo = true;

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

        private bool CheckinExists(int id)
        {
            return _context.Checkins.Any(e => e.id == id);
        }
    }
}
