using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class MainroleartistsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public MainroleartistsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Mainroleartists/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<MainroleartistViewModel>> Listar()
        {
            var Mainroleartist = await _context.Mainroleartists.ToListAsync();

            return Mainroleartist.Select(r => new MainroleartistViewModel
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

        // GET: api/Mainroleartists/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<MainroleartistSelectModel>> Select()
        {
            var mainroleartist = await _context.Mainroleartists
                .Include(s => s.skill)
                .Where(r => r.activo == true)
                .OrderByDescending(r => r.skill.skill)
                .ToListAsync();

            return mainroleartist.Select(r => new MainroleartistSelectModel
            {
                id = r.id,
                artistid = r.artistid,
                skillid = r.skillid
            });
        }

        // GET: api/Mainroleartists/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var mainroleartist = await _context.Mainroleartists.FindAsync(id);

            if (mainroleartist == null)
            {
                return NotFound();
            }

            return Ok(new MainroleartistViewModel
            {
                id = mainroleartist.id,
                skillid = mainroleartist.skillid,
                artistid = mainroleartist.artistid,
                iduseralta = mainroleartist.iduseralta,
                fecalta = mainroleartist.fecalta,
                iduserumod = mainroleartist.iduserumod,
                fecumod = mainroleartist.fecumod,
                activo = mainroleartist.activo
            });
        }

        // PUT: api/Mainroleartists/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] MainroleartistUpdateModel model)
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
            var mainroleartist = await _context.Mainroleartists.FirstOrDefaultAsync(c => c.id == model.id);

            if (mainroleartist == null)
            {
                return NotFound();
            }

            mainroleartist.skillid = model.skillid;
            mainroleartist.artistid = model.artistid;
            mainroleartist.iduserumod = model.iduserumod;
            mainroleartist.fecumod = fechaHora;

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

        // POST: api/Mainroleartists/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] MainroleartistCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Mainroleartist mainroleartist = new Mainroleartist
            {
                skillid = model.skillid,
                artistid = model.artistid,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Mainroleartists.Add(mainroleartist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(mainroleartist);
        }

        // DELETE: api/Mainroleartists/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mainroleartist = await _context.Mainroleartists.FindAsync(id);
            if (mainroleartist == null)
            {
                return NotFound();
            }

            _context.Mainroleartists.Remove(mainroleartist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(mainroleartist);
        }

        // PUT: api/Mainroleartists/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var mainroleartist = await _context.Mainroleartists.FirstOrDefaultAsync(c => c.id == id);

            if (mainroleartist == null)
            {
                return NotFound();
            }

            mainroleartist.activo = false;

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

        // PUT: api/Mainroleartists/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var mainroleartist = await _context.Mainroleartists.FirstOrDefaultAsync(c => c.id == id);

            if (mainroleartist == null)
            {
                return NotFound();
            }

            mainroleartist.activo = true;

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

        private bool MainroleartistExists(int id)
        {
            return _context.Mainroleartists.Any(e => e.id == id);
        }
    }
}
