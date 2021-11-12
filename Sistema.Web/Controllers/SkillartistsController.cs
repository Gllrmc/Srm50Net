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
    public class SkillartistsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public SkillartistsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Skillartists/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SkillartistViewModel>> Listar()
        {
            var Skillartist = await _context.Skillartists.ToListAsync();

            return Skillartist.Select(r => new SkillartistViewModel
            {
                id = r.id,
                artistid = r.artistid,
                skillid = r.skillid,
                iduseralta = r.iduseralta,
                fecalta = r.fecalta,
                iduserumod = r.iduserumod,
                fecumod = r.fecumod,
                activo = r.activo
            });

        }

        // GET: api/Skillartists/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SkillartistSelectModel>> Select()
        {
            var skillartist = await _context.Skillartists
                .Include(s => s.skill)
                .Where(r => r.activo == true)
                .OrderByDescending(r => r.skill.skill)
                .ToListAsync();

            return skillartist.Select(r => new SkillartistSelectModel
            {
                id = r.id,
                artistid = r.artistid,
                skillid = r.skillid
            });
        }

        // GET: api/Skillartists/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var skillartist = await _context.Skillartists.FindAsync(id);

            if (skillartist == null)
            {
                return NotFound();
            }

            return Ok(new SkillartistViewModel
            {
                id = skillartist.id,
                skillid = skillartist.skillid,
                artistid = skillartist.artistid,
                iduseralta = skillartist.iduseralta,
                fecalta = skillartist.fecalta,
                iduserumod = skillartist.iduserumod,
                fecumod = skillartist.fecumod,
                activo = skillartist.activo
            });
        }

        // PUT: api/Skillartists/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] SkillartistUpdateModel model)
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
            var skillartist = await _context.Skillartists.FirstOrDefaultAsync(c => c.id == model.id);

            if (skillartist == null)
            {
                return NotFound();
            }

            skillartist.skillid = model.skillid;
            skillartist.artistid = model.artistid;
            skillartist.iduserumod = model.iduserumod;
            skillartist.fecumod = fechaHora;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(skillartist);
        }

        // POST: api/Skillartists/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] SkillartistCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Skillartist skillartist = new Skillartist
            {
                skillid = model.skillid,
                artistid = model.artistid,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Skillartists.Add(skillartist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(skillartist);
        }

        // DELETE: api/Skillartists/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var skillartist = await _context.Skillartists.FindAsync(id);
            if (skillartist == null)
            {
                return NotFound();
            }

            _context.Skillartists.Remove(skillartist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(skillartist);
        }

        // PUT: api/Skillartists/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var skillartist = await _context.Skillartists.FirstOrDefaultAsync(c => c.id == id);

            if (skillartist == null)
            {
                return NotFound();
            }

            skillartist.activo = false;

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

        // PUT: api/Skillartists/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var skillartist = await _context.Skillartists.FirstOrDefaultAsync(c => c.id == id);

            if (skillartist == null)
            {
                return NotFound();
            }

            skillartist.activo = true;

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

        private bool SkillartistExists(int id)
        {
            return _context.Skillartists.Any(e => e.id == id);
        }
    }
}
