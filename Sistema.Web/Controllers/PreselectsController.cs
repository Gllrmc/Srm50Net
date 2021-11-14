using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Preselects;
using Sistema.Web.Models.Preselects;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,Owner,Collaborator,Reader")]
    [Route("api/[controller]")]
    [ApiController]
    public class PreselectsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public PreselectsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Preselecs/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<PreselectViewModel>> Listar()
        {
            var Checkin = await _context.Preselects.ToListAsync();

            return Checkin.Select(r => new PreselectViewModel
            {
                id = r.id,
                code = r.code,
                preselect = r.preselect,
                iduseralta = r.iduseralta,
                fecalta = r.fecalta,
                iduserumod = r.iduserumod,
                fecumod = r.fecumod,
                activo = r.activo
            });

        }

        // GET: api/Preselecs/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var preselec = await _context.Preselects.FindAsync(id);

            if (preselec == null)
            {
                return NotFound();
            }

            return Ok(new PreselectViewModel
            {
                id = preselec.id,
                code = preselec.code,
                preselect = preselec.preselect,
                iduseralta = preselec.iduseralta,
                fecalta = preselec.fecalta,
                iduserumod = preselec.iduserumod,
                fecumod = preselec.fecumod,
                activo = preselec.activo
            });
        }

        // PUT: api/Preselect/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] PreselectUpdateModel model)
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
            var preselec = await _context.Preselects.FirstOrDefaultAsync(c => c.id == model.id);

            if (preselec == null)
            {
                return NotFound();
            }

            preselec.code = model.code;
            preselec.preselect = model.preselect;
            preselec.iduserumod = model.iduserumod;
            preselec.fecumod = fechaHora;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(preselec);
        }

        // POST: api/Preselect/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] PreselectCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Preselect preselect = new Preselect
            {
                code = model.code,
                preselect = model.preselect,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Preselects.Add(preselect);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(preselect);
        }

        // DELETE: api/Preselect/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var preselec = await _context.Preselects.FindAsync(id);
            if (preselec == null)
            {
                return NotFound();
            }

            _context.Preselects.Remove(preselec);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(preselec);
        }

        // PUT: api/Preselect/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var preselec = await _context.Preselects.FirstOrDefaultAsync(c => c.id == id);

            if (preselec == null)
            {
                return NotFound();
            }

            preselec.activo = false;

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

        // PUT: api/Preselect/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var preselec = await _context.Preselects.FirstOrDefaultAsync(c => c.id == id);

            if (preselec == null)
            {
                return NotFound();
            }

            preselec.activo = true;

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
            return _context.Preselects.Any(e => e.id == id);
        }
    }
}
