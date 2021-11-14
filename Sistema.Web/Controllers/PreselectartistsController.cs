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
    public class PreselectartistsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public PreselectartistsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Preselectartists/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<PreselectartistViewModel>> Listar()
        {
            var Preselectartist = await _context.Preselectartists.ToListAsync();

            return Preselectartist.Select(r => new PreselectartistViewModel
            {
                id = r.id,
                artistid = r.artistid,
                preselectid = r.preselectid,
                iduseralta = r.iduseralta,
                fecalta = r.fecalta,
                iduserumod = r.iduserumod,
                fecumod = r.fecumod,
                activo = r.activo
            });

        }

        // GET: api/Preselectartists/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var preselectartist = await _context.Preselectartists.FindAsync(id);

            if (preselectartist == null)
            {
                return NotFound();
            }

            return Ok(new PreselectartistViewModel
            {
                id = preselectartist.id,
                preselectid = preselectartist.preselectid,
                artistid = preselectartist.artistid,
                iduseralta = preselectartist.iduseralta,
                fecalta = preselectartist.fecalta,
                iduserumod = preselectartist.iduserumod,
                fecumod = preselectartist.fecumod,
                activo = preselectartist.activo
            });
        }

        // PUT: api/Preselectartists/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] PreselectartistUpdateModel model)
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
            var preselectartist = await _context.Preselectartists.FirstOrDefaultAsync(c => c.id == model.id);

            if (preselectartist == null)
            {
                return NotFound();
            }

            preselectartist.preselectid = model.preselectid;
            preselectartist.artistid = model.artistid;
            preselectartist.iduserumod = model.iduserumod;
            preselectartist.fecumod = fechaHora;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(preselectartist);
        }

        // PUT: api/Preselectartists/Actualizarpreselectset
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizarpreselectset([FromBody] PreselectartistUpdateModel model)
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
            var preselectartist = await _context.Preselectartists.FirstOrDefaultAsync(c => c.id == model.id);

            if (preselectartist == null)
            {
                return NotFound();
            }

            preselectartist.preselectid = model.preselectid;
            preselectartist.artistid = model.artistid;
            preselectartist.iduserumod = model.iduserumod;
            preselectartist.fecumod = fechaHora;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(preselectartist);
        }

        // POST: api/Preselectartists/Crearpreselectset
        [HttpPost("[action]")]
        public async Task<IActionResult> Crearpreselectset([FromBody] PreselectartistMassiveCreateModel model)
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

            for (var i = 0; i < model.artistid.Length; i++)
            {
                Preselectartist preselectartist = new Preselectartist
                {
                    artistid = model.artistid[i],
                    preselectid = preselect.id,
                    iduseralta = model.iduseralta,
                    fecalta = fechaHora,
                    iduserumod = model.iduseralta,
                    fecumod = fechaHora,
                    activo = true
                };

                _context.Preselectartists.Add(preselectartist);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }


        // POST: api/Preselectartists/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] PreselectartistCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Preselectartist preselectartist = new Preselectartist
            {
                preselectid = model.preselectid,
                artistid = model.artistid,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Preselectartists.Add(preselectartist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(preselectartist);
        }

        // DELETE: api/Preselectartists/Eliminarpreselectset/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminarpreselectset([FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var preselect = await _context.Preselects.FindAsync(id);
            if (preselect == null)
            {
                return NotFound();
            }
            _context.Preselects.Remove(preselect);

            var baja = await _context.Preselectartists.Where(f => id == f.preselectid).ToListAsync();
            baja.ForEach(a => _context.Preselectartists.Remove(a));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Preselectartists/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var preselectartist = await _context.Preselectartists.FindAsync(id);
            if (preselectartist == null)
            {
                return NotFound();
            }

            _context.Preselectartists.Remove(preselectartist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(preselectartist);
        }

        // PUT: api/Preselectartists/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var preselectartist = await _context.Preselectartists.FirstOrDefaultAsync(c => c.id == id);

            if (preselectartist == null)
            {
                return NotFound();
            }

            preselectartist.activo = false;

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

        // PUT: api/Preselectartists/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var preselectartist = await _context.Preselectartists.FirstOrDefaultAsync(c => c.id == id);

            if (preselectartist == null)
            {
                return NotFound();
            }

            preselectartist.activo = true;

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

        private bool PreselectartistExists(int id)
        {
            return _context.Preselectartists.Any(e => e.id == id);
        }
    }
}
