using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using entity_framework_tutorial_web_api.Data;
using entity_framework_tutorial_web_api.Models;

namespace entity_framework_tutorial_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityFrameworkContactController : ControllerBase
    {
        private readonly DataContext _context;

        public EntityFrameworkContactController(DataContext context)
        {
            _context = context;
        }

        // GET: api/EntityFrameworkContact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContactDbSet()
        {
            if (_context.ContactDbSet == null)
            {
                return NotFound();
            }
          
            // Ignore soft-deleted things
            //return await _context.ContactDbSet.Where(c => !c.IsDeleted).ToListAsync();

            // Return all things
            return await _context.ContactDbSet.ToListAsync();
        }

        // GET: api/EntityFrameworkContact/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            if (_context.ContactDbSet == null)
            {
                return NotFound();
            }
            var contact = await _context.ContactDbSet.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/EntityFrameworkContact/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (_context.ContactDbSet == null)
            {
                return NotFound();
            }
            var dbContact = await _context.ContactDbSet.FindAsync(id);

            if (dbContact == null)
            {
                return NotFound();
            }

            dbContact.FirstName = contact.FirstName;
            dbContact.LastName = contact.LastName;
            dbContact.Nickname = contact.Nickname;
            dbContact.Place = contact.Place;

            await _context.SaveChangesAsync();

            return NoContent();

            // Default, changes the creation date field if not specified
            //if (id != contact.ID)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(contact).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ContactExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        // POST: api/EntityFrameworkContact
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            if (_context.ContactDbSet == null)
            {
                return Problem("Entity set 'DataContext.ContactDbSet'  is null.");
            }
            _context.ContactDbSet.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.ID }, contact);
        }

        // DELETE: api/EntityFrameworkContact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            if (_context.ContactDbSet == null)
            {
                return NotFound();
            }
            var contact = await _context.ContactDbSet.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            // Hard Delete
            //_context.ContactDbSet.Remove(contact);
            // Soft Delete
            contact.IsDeleted = true;


            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return (_context.ContactDbSet?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
