﻿using System;
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
    public class ArtistsController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ArtistsController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Artists/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArtistViewModel>> Listar()
        {
            var Artist = await _context.Artists.ToListAsync();

            return Artist.Select(r => new ArtistViewModel
            {
                id = r.id,
                fullname = r.fullname,
                projectsworked = r.projectsworked,
                cost = r.cost,
                costingdate = r.costingdate,
                costinguserid = r.costinguserid,
                email = r.email,
                phone = r.phone,
                mobile = r.mobile,
                imgcliente = r.imgcliente,
                proveedorid = r.proveedorid,
                iduseralta = r.iduseralta,
                fecalta = r.fecalta,
                iduserumod = r.iduserumod,
                fecumod = r.fecumod,
                activo = r.activo
            });

        }

        // GET: api/Artists/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArtistSelectModel>> Select()
        {
            var artist = await _context.Artists
                .Where(r => r.activo == true)
                .OrderBy(r => r.fullname)
                .ToListAsync();

            return artist.Select(r => new ArtistSelectModel
            {
                id = r.id,
                fullname = r.fullname,
            });
        }

        // GET: api/Artists/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var artist = await _context.Artists.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(new ArtistViewModel
            {
                id = artist.id,
                fullname = artist.fullname,
                projectsworked = artist.projectsworked,
                cost = artist.cost,
                costingdate = artist.costingdate,
                costinguserid = artist.costinguserid,
                email = artist.email,
                phone = artist.phone,
                mobile = artist.mobile,
                imgcliente = artist.imgcliente,
                proveedorid = artist.proveedorid,
                iduseralta = artist.iduseralta,
                fecalta = artist.fecalta,
                iduserumod = artist.iduserumod,
                fecumod = artist.fecumod,
                activo = artist.activo
            });
        }

        // PUT: api/Artists/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ArtistUpdateModel model)
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
            var artist = await _context.Artists.FirstOrDefaultAsync(c => c.id == model.id);

            if (artist == null)
            {
                return NotFound();
            }

            artist.fullname = model.fullname;
            artist.projectsworked = model.projectsworked;
            artist.cost = model.cost;
            artist.costingdate = model.costingdate;
            artist.costinguserid = model.costinguserid;
            artist.email = model.email;
            artist.phone = model.phone;
            artist.mobile = model.mobile;
            artist.imgcliente = model.imgcliente;
            artist.proveedorid = model.proveedorid;
            artist.iduserumod = model.iduserumod;
            artist.fecumod = fechaHora;

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

        // POST: api/Artists/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ArtistCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fechaHora = DateTime.Now;
            Artist artist = new Artist
            {
                fullname = model.fullname,
                projectsworked = model.projectsworked,
                cost = model.cost,
                costingdate = model.costingdate,
                costinguserid = model.costinguserid,
                email = model.email,
                phone = model.phone,
                mobile = model.mobile,
                imgcliente = model.imgcliente,
                proveedorid = model.proveedorid,
                iduseralta = model.iduseralta,
                fecalta = fechaHora,
                iduserumod = model.iduseralta,
                fecumod = fechaHora,
                activo = true
            };

            _context.Artists.Add(artist);
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

        // DELETE: api/Artists/Eliminar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(artist);
        }

        // PUT: api/Artists/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var artist = await _context.Artists.FirstOrDefaultAsync(c => c.id == id);

            if (artist == null)
            {
                return NotFound();
            }

            artist.activo = false;

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

        // PUT: api/Artists/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var artist = await _context.Artists.FirstOrDefaultAsync(c => c.id == id);

            if (artist == null)
            {
                return NotFound();
            }

            artist.activo = true;

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

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.id == id);
        }
    }
}