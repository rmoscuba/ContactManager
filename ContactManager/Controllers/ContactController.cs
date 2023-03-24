using ContactManager.Contexts;
using ContactManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ContactManager.Extensions;
using ContactManager.Interfaces;

namespace ContactManager.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private IRepositoryManager _repository;

        public ContactController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        // GET: api/<ContactController>
        [HttpGet]
        public IEnumerable<ContactDTO> Get()
        {
            Guid OwnerId = HttpContext.User.GetOwnerId();
            var contacts = _repository.Contact.GetAllUserContacts(OwnerId, trackChanges: false);
            return contacts;
        }


        // GET api/<ContactController>/5
        [HttpGet("{ContactId}")]
        public ContactDTO Get(Guid ContactId)
        {
            Guid OwnerId = HttpContext.User.GetOwnerId();
            var contact = _repository.Contact.GetUserContactDTOById(OwnerId, ContactId, trackChanges: false);
            return contact;
        }

        

        // POST api/<ContactController>
        [HttpPost]
        public void Post([FromBody] Contact value)
        {
            Guid OwnerId = HttpContext.User.GetOwnerId();
            value.OwnerId = OwnerId;

            _repository.Contact.CreateContact(value);
            _repository.Save();
        }


        // PUT api/<ContactController>/5
        [HttpPut("{ContactId}")]
        public void Put(Guid ContactId, [FromBody] Contact value)
        {
            Guid OwnerId = HttpContext.User.GetOwnerId();

            // Ensure Ids are not tampered with
            value.Id = ContactId;
            value.OwnerId = OwnerId;

            // Ensure contact for this User exists
            var contact = _repository.Contact.GetUserContactDTOById(OwnerId, ContactId, trackChanges: false);
            if (contact != null)
            {
                _repository.Contact.UpdateContact(value);
                _repository.Save();
            }
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{ContactId}")]
        public void Delete(Guid ContactId)
        {
            Guid OwnerId = HttpContext.User.GetOwnerId();

            var contact = _repository.Contact.GetUserContactById(OwnerId, ContactId, trackChanges: false);
            if (contact != null)
            {
                _repository.Contact.DeleteContact(contact);
                _repository.Save();
            }
        }
    }
}
