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
    [Authorize(Roles = "Administrador,Owner,Collaborator,Reader")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public NotesController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Notes/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<NoteViewModel>> Listar()
        {
            var Note = await _context.Notes
                .OrderBy(o => o.artist).ThenByDescending(o => o.fecumod)
                .ToListAsync();

            return Note.Select(r => new NoteViewModel
            {
                id = r.id,
                artistid = r.artistid,
                note = r.note,
                iduseralta = r.iduseralta,
                fecalta = r.fecalta,
                iduserumod = r.iduserumod,
                fecumod = r.fecumod,
                activo = r.activo
            });

        }

        // GET: api/Notes/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(new NoteViewModel
            {
                id = note.id,
                artistid = note.artistid,
                note = note.note,
                iduseralta = note.iduseralta,
                fecalta = note.fecalta,
                iduserumod = note.iduserumod,
                fecumod = note.fecumod,
                activo = note.activo
            });
        }

        // PUT: api/Notes/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] NoteUpdateModel model)
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
            var note = await _context.Notes.FirstOrDefaultAsync(c => c.id == model.id);

            if (note == null)
            {
                return NotFound();
            }

            note.note = model.note;
            note.iduserumod = model.iduserumod;
            note.fecumod = fechaHora;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(note);
        }

        // POST: api/Notes/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] NoteCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Note note = new Note
            {
                artistid = model.artistid,
                note = model.note,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Notes.Add(note);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(note);
        }

        // DELETE: api/Notes/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(note);
        }

        // PUT: api/Notes/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var note = await _context.Notes.FirstOrDefaultAsync(c => c.id == id);

            if (note == null)
            {
                return NotFound();
            }

            note.activo = false;

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

        // PUT: api/Notes/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var note = await _context.Notes.FirstOrDefaultAsync(c => c.id == id);

            if (note == null)
            {
                return NotFound();
            }

            note.activo = true;

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

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.id == id);
        }
    }
}
