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
        public IActionResult Get()
        {
            Guid OwnerId = HttpContext.User.GetOwnerId();
            var contacts = _repository.Contact.GetAllUserContacts(OwnerId, trackChanges: false);
            if (contacts is null) { return NotFound(); }
            else { return Ok(contacts); }
        }


        // GET api/<ContactController>/5
        [HttpGet("{ContactId}")]
        public IActionResult Get(Guid ContactId)
        {
            Guid OwnerId = HttpContext.User.GetOwnerId();
            var contact = _repository.Contact.GetUserContactDTOById(OwnerId, ContactId, trackChanges: false);
            if (contact is null) { return NotFound();  }
            else { return Ok(contact); }
        }

        

        // POST api/<ContactController>
        [HttpPost]
        public IActionResult Post([FromBody] Contact value)
        {
            Guid OwnerId = HttpContext.User.GetOwnerId();

            value.OwnerId = OwnerId;
            value.Id = Guid.NewGuid();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repository.Contact.CreateContact(value);
                _repository.Save();
            }
            catch (DbUpdateException e)
            {
                return BadRequest(e.InnerException.Message);
            }

            // 201 created successfully response, with a Location response header
            // containing the newly created contact's URL.
            return CreatedAtAction(nameof(Get), new { ContactId = value.Id }, ContactDTO.Map(value));
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
