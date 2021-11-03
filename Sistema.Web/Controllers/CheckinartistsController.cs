using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Checkins;
using Sistema.Web.Models.Checkins;

namespace Sistema.Web.Controllers
{
    //[Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion,ExecutiveProducer,AsistProduccion,LineProducer,ChiefProducer,AsistGeneral")]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckinartistsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public CheckinartistsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Checkinartists/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CheckinartistViewModel>> Listar()
        {
            var Checkinartist = await _context.Checkinartists.ToListAsync();

            return Checkinartist.Select(r => new CheckinartistViewModel
            {
                id = r.id,
                artistid = r.artistid,
                checkinid = r.checkinid,
                iduseralta = r.iduseralta,
                fecalta = r.fecalta,
                iduserumod = r.iduserumod,
                fecumod = r.fecumod,
                activo = r.activo
            });

        }

        // GET: api/Checkinartists/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var checkinartist = await _context.Checkinartists.FindAsync(id);

            if (checkinartist == null)
            {
                return NotFound();
            }

            return Ok(new CheckinartistViewModel
            {
                id = checkinartist.id,
                checkinid = checkinartist.checkinid,
                artistid = checkinartist.artistid,
                iduseralta = checkinartist.iduseralta,
                fecalta = checkinartist.fecalta,
                iduserumod = checkinartist.iduserumod,
                fecumod = checkinartist.fecumod,
                activo = checkinartist.activo
            });
        }

        // PUT: api/Checkinartists/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] CheckinartistUpdateModel model)
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
            var checkinartist = await _context.Checkinartists.FirstOrDefaultAsync(c => c.id == model.id);

            if (checkinartist == null)
            {
                return NotFound();
            }

            checkinartist.checkinid = model.checkinid;
            checkinartist.artistid = model.artistid;
            checkinartist.iduserumod = model.iduserumod;
            checkinartist.fecumod = fechaHora;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(checkinartist);
        }

        // POST: api/Checkinartists/Crearcheckinset
        [HttpPost("[action]")]
        public async Task<IActionResult> Crearcheckinset([FromBody] CheckinartistMassiveCreateModel model)
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

            for (var i = 0; i < model.artistid.Length; i++)
            {
                Checkinartist checkinartist = new Checkinartist
                {
                    artistid = model.artistid[i],
                    checkinid = checkin.id,
                    iduseralta = model.iduseralta,
                    fecalta = fechaHora,
                    iduserumod = model.iduseralta,
                    fecumod = fechaHora,
                    activo = true
                };

                _context.Checkinartists.Add(checkinartist);
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


        // POST: api/Checkinartists/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CheckinartistCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Checkinartist checkinartist = new Checkinartist
            {
                checkinid = model.checkinid,
                artistid = model.artistid,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Checkinartists.Add(checkinartist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(checkinartist);
        }

        // DELETE: api/Checkinartists/Eliminarcheckinset/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminarcheckinset([FromRoute] int id)
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

            var baja = await _context.Checkinartists.Where(f => id == f.checkinid).ToListAsync();
            baja.ForEach(a => _context.Checkinartists.Remove(a));

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

        // DELETE: api/Checkinartists/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkinartist = await _context.Checkinartists.FindAsync(id);
            if (checkinartist == null)
            {
                return NotFound();
            }

            _context.Checkinartists.Remove(checkinartist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(checkinartist);
        }

        // PUT: api/Checkinartists/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var checkinartist = await _context.Checkinartists.FirstOrDefaultAsync(c => c.id == id);

            if (checkinartist == null)
            {
                return NotFound();
            }

            checkinartist.activo = false;

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

        // PUT: api/Checkinartists/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var checkinartist = await _context.Checkinartists.FirstOrDefaultAsync(c => c.id == id);

            if (checkinartist == null)
            {
                return NotFound();
            }

            checkinartist.activo = true;

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

        private bool CheckinartistExists(int id)
        {
            return _context.Checkinartists.Any(e => e.id == id);
        }
    }
}
