using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Usuarios;
using Sistema.Web.Models.Usuarios;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,JefeAdministracion,AsistAdministracion")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public RolesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Roles/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolViewModel>> Listar()
        {
            var rol = await _context.Roles.ToListAsync();

            return rol.Select(r => new RolViewModel
            {
                id = r.id,
                nombre = r.nombre,
                descripcion = r.descripcion,
                activo = r.activo
            });

        }

        // GET: api/Roles/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolSelectModel>> Select()
        {
            var rol = await _context.Roles.Where(r => r.activo == true).ToListAsync();

            return rol.Select(r => new RolSelectModel
            {
                id = r.id,
                nombre = r.nombre
            });
        }

        // PUT: api/Roles/Desactivar/4
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var rol = await _context.Roles.FirstOrDefaultAsync(c => c.id == id);

            if (rol == null)
            {
                return NotFound();
            }

            rol.activo = false;

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

        // PUT: api/Roles/Activar/4
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var rol = await _context.Roles.FirstOrDefaultAsync(c => c.id == id);

            if (rol == null)
            {
                return NotFound();
            }

            rol.activo = true;

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

        private bool RolExists(int id)
        {
            return _context.Roles.Any(e => e.id == id);
        }
    }
}
