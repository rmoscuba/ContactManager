using ContactManager.Contexts;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactManager.Controllers
{
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
            var contacts = _contactsContext
                .Contacts.Include(o => o.Owner)
                .Select(c => ContactDTO.Map(c));
            return contacts;
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public ContactDTO Get(Guid id)
        {
            var contact = _contactsContext.Contacts
                .Where(c => c.Id == id)
                .Include(b => b.Owner)
                .Select(b =>ContactDTO.Map(b))
                .FirstOrDefault();
            return contact;
        }

        // POST api/<ContactController>
        [HttpPost]
        public void Post([FromBody] Contact value)
        {
            _contactsContext.Contacts.Add(value);
            _contactsContext.SaveChanges();
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Contact value)
        {
            var contact = _contactsContext.Contacts.FirstOrDefault(s => s.Id == id);
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
            var contact = _contactsContext.Contacts.FirstOrDefault(s => s.Id == id);
            if (contact != null)
            {
                _contactsContext.Contacts.Remove(contact);
                _contactsContext.SaveChanges();
            }
        }
    }
}
