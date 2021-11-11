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
    public class ContactsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ContactsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Contacts/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ContactViewModel>> Listar()
        {
            var Contact = await _context.Contacts
                .OrderBy(o => o.artist).ThenByDescending(o => o.fecumod)
                .ToListAsync();

            return Contact.Select(r => new ContactViewModel
            {
                id = r.id,
                artistid = r.artistid,
                contact = r.contact,
                iduseralta = r.iduseralta,
                fecalta = r.fecalta,
                iduserumod = r.iduserumod,
                fecumod = r.fecumod,
                activo = r.activo
            });

        }

        // GET: api/Contacts/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(new ContactViewModel
            {
                id = contact.id,
                artistid = contact.artistid,
                contact = contact.contact,
                iduseralta = contact.iduseralta,
                fecalta = contact.fecalta,
                iduserumod = contact.iduserumod,
                fecumod = contact.fecumod,
                activo = contact.activo
            });
        }

        // PUT: api/Contacts/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ContactUpdateModel model)
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
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.id == model.id);

            if (contact == null)
            {
                return NotFound();
            }

            contact.contact = model.contact;
            contact.iduserumod = model.iduserumod;
            contact.fecumod = fechaHora;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok(contact);
        }

        // POST: api/Contacts/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ContactCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Contact contact = new Contact
            {
                artistid = model.artistid,
                contact = model.contact,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Contacts.Add(contact);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(contact);
        }

        // DELETE: api/Contacts/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(contact);
        }

        // PUT: api/Contacts/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.id == id);

            if (contact == null)
            {
                return NotFound();
            }

            contact.activo = false;

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

        // PUT: api/Contacts/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.id == id);

            if (contact == null)
            {
                return NotFound();
            }

            contact.activo = true;

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

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.id == id);
        }
    }
}
