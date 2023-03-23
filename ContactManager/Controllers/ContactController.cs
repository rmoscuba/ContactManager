using ContactManager.Contexts;
using ContactManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace ContactManager.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private ContactsContext _contactsContext;

        public ContactController(ContactsContext ContactsContext)
        {
            _contactsContext = ContactsContext;
        }

        // GET: api/<ContactController>
        [HttpGet]
        public IEnumerable<ContactDTO> Get()
        {
            Guid OwnerId = GetOwnerId();

            var contacts = _contactsContext
                .Contacts.Include(o => o.Owner)
                .Where(c => c.OwnerId == OwnerId)
                .Select(c => ContactDTO.Map(c));
            return contacts;
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public ContactDTO Get(Guid id)
        {
            Guid OwnerId = GetOwnerId();

            var contact = _contactsContext.Contacts
                .Where(c => c.Id == id && c.OwnerId == OwnerId)
                .Include(b => b.Owner)
                .Select(b =>ContactDTO.Map(b))
                .FirstOrDefault();
            return contact;
        }

        // POST api/<ContactController>
        [HttpPost]
        public void Post([FromBody] Contact value)
        {
            Guid OwnerId = GetOwnerId();
            value.OwnerId = OwnerId;

            _contactsContext.Contacts.Add(value);
            _contactsContext.SaveChanges();
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Contact value)
        {
            Guid OwnerId = GetOwnerId();

            var contact = _contactsContext.Contacts.FirstOrDefault(c => c.Id == id && c.OwnerId == OwnerId);
            if (contact != null)
            {
                _contactsContext.Entry<Contact>(contact).CurrentValues.SetValues(value);
                _contactsContext.SaveChanges();
            }
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            Guid OwnerId = GetOwnerId();

            var contact = _contactsContext.Contacts.FirstOrDefault(c => c.Id == id && c.OwnerId == OwnerId);
            if (contact != null)
            {
                _contactsContext.Contacts.Remove(contact);
                _contactsContext.SaveChanges();
            }
        }

        private Guid GetOwnerId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            Guid OwnerId = Guid.Empty;
            if (identity != null)
            {
                OwnerId = Guid.Parse(identity.FindFirst("UserId").Value);
            }

            return OwnerId;
        }
    }
}
