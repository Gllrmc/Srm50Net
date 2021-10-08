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
    public class SkillsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public SkillsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Skills/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SkillViewModel>> Listar()
        {
            var Skill = await _context.Skills.ToListAsync();

            return Skill.Select(r => new SkillViewModel
            {
                id = r.id,
                skill = r.skill,
                ismainrole = r.ismainrole,
                iduseralta = r.iduseralta,
                fecalta = r.fecalta,
                iduserumod = r.iduserumod,
                fecumod = r.fecumod,
                activo = r.activo
            });

        }

        // GET: api/Skills/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SkillSelectModel>> Select()
        {
            var skill = await _context.Skills.Where(r => r.activo == true).OrderBy(r => r.skill).ToListAsync();

            return skill.Select(r => new SkillSelectModel
            {
                id = r.id,
                skill = r.skill,
                ismainrole = r.ismainrole
            });
        }

        // GET: api/Skills/Selectmainrole
        [HttpGet("[action]")]
        public async Task<IEnumerable<SkillSelectModel>> Selectmainrole()
        {
            var skill = await _context.Skills
                .Where(r => r.activo == true && r.ismainrole == true )
                .OrderBy(r => r.skill)
                .ToListAsync();

            return skill.Select(r => new SkillSelectModel
            {
                id = r.id,
                skill = r.skill,
            });
        }

        // GET: api/Skills/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            return Ok(new SkillViewModel
            {
                id = skill.id,
                skill = skill.skill,
                ismainrole = skill.ismainrole,
                iduseralta = skill.iduseralta,
                fecalta = skill.fecalta,
                iduserumod = skill.iduserumod,
                fecumod = skill.fecumod,
                activo = skill.activo
            });
        }

        // PUT: api/Skills/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] SkillUpdateModel model)
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
            var skill = await _context.Skills.FirstOrDefaultAsync(c => c.id == model.id);

            if (skill == null)
            {
                return NotFound();
            }

            skill.skill = model.skill;
            skill.ismainrole = model.ismainrole;
            skill.iduserumod = model.iduserumod;
            skill.fecumod = fechaHora;

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

        // POST: api/Skills/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] SkillCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Skill skill = new Skill
            {
                skill = model.skill,
                ismainrole = model.ismainrole,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Skills.Add(skill);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(skill);
        }

        // DELETE: api/Skills/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(skill);
        }

        // PUT: api/Skills/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var skill = await _context.Skills.FirstOrDefaultAsync(c => c.id == id);

            if (skill == null)
            {
                return NotFound();
            }

            skill.activo = false;

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

        // PUT: api/Skills/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var skill = await _context.Skills.FirstOrDefaultAsync(c => c.id == id);

            if (skill == null)
            {
                return NotFound();
            }

            skill.activo = true;

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

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.id == id);
        }
    }
}
