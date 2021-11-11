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
    public class SchedulesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public SchedulesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Schedules/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ScheduleViewModel>> Listar()
        {
            var fechaHora = DateTime.Now;

            var Schedule = await _context.Schedules
                .Where(f => f.startdate >= fechaHora || ( f.enddate > fechaHora && f.startdate < fechaHora ))
                .OrderBy(o => o.artist).ThenByDescending(o => o.startdate)
                .ToListAsync();

            return Schedule.Select(r => new ScheduleViewModel
            {
                id = r.id,
                artistid = r.artistid,
                startdate = r.startdate,
                enddate = r.enddate,
                comment = r.comment,
                limboid = r.limboid,
                proyectoid = r.proyectoid,
                iduseralta = r.iduseralta,
                fecalta = r.fecalta,
                iduserumod = r.iduserumod,
                fecumod = r.fecumod,
                activo = r.activo
            });

        }

        // GET: api/Schedules/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var schedule = await _context.Schedules.FindAsync(id);

            if (schedule == null)
            {
                return NotFound();
            }

            return Ok(new ScheduleViewModel
            {
                id = schedule.id,
                artistid = schedule.artistid,
                startdate = schedule.startdate,
                enddate = schedule.enddate,
                comment = schedule.comment,
                limboid = schedule.limboid,
                proyectoid = schedule.proyectoid,
                iduseralta = schedule.iduseralta,
                fecalta = schedule.fecalta,
                iduserumod = schedule.iduserumod,
                fecumod = schedule.fecumod,
                activo = schedule.activo
            });
        }

        // PUT: api/Schedules/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ScheduleUpdateModel model)
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
            var schedule = await _context.Schedules.FirstOrDefaultAsync(c => c.id == model.id);

            if (schedule == null)
            {
                return NotFound();
            }

            schedule.startdate = model.startdate;
            schedule.enddate = model.enddate;
            schedule.comment = model.comment;
            schedule.limboid = model.limboid;
            schedule.proyectoid = model.proyectoid;
            schedule.iduserumod = model.iduserumod;
            schedule.fecumod = fechaHora;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(schedule);
        }

        // POST: api/Schedules/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ScheduleCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Schedule schedule = new Schedule
            {
                artistid = model.artistid,
                startdate = model.startdate,
                enddate = model.enddate,
                comment = model.comment,
                limboid = model.limboid,
                proyectoid = model.proyectoid,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Schedules.Add(schedule);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(schedule);
        }

        // DELETE: api/Schedules/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedule);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(schedule);
        }

        // PUT: api/Schedules/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var schedule = await _context.Schedules.FirstOrDefaultAsync(c => c.id == id);

            if (schedule == null)
            {
                return NotFound();
            }

            schedule.activo = false;

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

        // PUT: api/Schedules/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var schedule = await _context.Schedules.FirstOrDefaultAsync(c => c.id == id);

            if (schedule == null)
            {
                return NotFound();
            }

            schedule.activo = true;

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

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.id == id);
        }
    }
}
