using ContactManager.Interfaces;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestContactsWebAPI
{
    class ContactRepositoryFake : IContactRepository
    {
        private readonly List<Contact> _contacts;

        public ContactRepositoryFake()
        {
            _contacts = new List<Contact>()
            {
                new Contact() { 
                        Id = new Guid("34D6D18A-3CFF-485F-EA7F-08DB2B278368"),
                        FirstName = "Pedro",
                        LastName = "Martínez",
                        Email = "pedro@avangenio.com",
                        DateOfBirth = DateTime.Parse("1988-11-06"),
                        Phone ="555555551",
                        OwnerId = new Guid("D17592DC-EF09-4962-F366-08DB2B10FCA3")
                    },
                new Contact() {
                        Id = new Guid("3AC4CCE1-48D2-41D3-E7C1-08DB2B9CBBE8"),
                        FirstName = "Alfredo",
                        LastName = "Rodríguez",
                        Email = "alfredo@avangenio.com",
                        DateOfBirth = DateTime.Parse("1990-12-06"),
                        Phone ="555555552",
                        OwnerId = new Guid("D17592DC-EF09-4962-F366-08DB2B10FCA3")
                    },
                new Contact() {
                        Id = new Guid("EED907EF-AA49-4EE5-380D-08DB2BE0175D"),
                        FirstName = "Yordanis",
                        LastName = "Pérez",
                        Email = "yordanis@avangenio.com",
                        DateOfBirth = DateTime.Parse("1985-09-08"),
                        Phone ="555555553",
                        OwnerId = new Guid("838EC765-936F-463C-F367-08DB2B10FCA3")
                    }
            };
        }

        public void CreateContact(Contact contact)
        {
            contact.Id = Guid.NewGuid();
            _contacts.Add(contact);
        }

        public void DeleteContact(Contact contact)
        {
            var _contact = _contacts.First(c => c.Id == contact.Id);
            _contacts.Remove(_contact);
        }

        public IEnumerable<ContactDTO> GetAllContacts(bool trackChanges)
        {
            return _contacts.Select(c => ContactDTO.Map(c));
        }

        public IEnumerable<ContactDTO> GetAllUserContacts(Guid OwnerId, bool trackChanges)
        {
            return _contacts
                .FindAll(c => c.OwnerId == OwnerId)
                .Select(c => ContactDTO.Map(c))
                .ToList();
        }

        public Contact GetUserContactById(Guid OwnerId, Guid ContactId, bool trackChanges)
        {
            var _contact = _contacts
                .First(c => c.Id == ContactId && c.OwnerId == OwnerId);
            return _contact;
        }

        public ContactDTO GetUserContactDTOById(Guid OwnerId, Guid ContactId, bool trackChanges)
        {
            var _contact = _contacts
                .FindAll(c => c.Id == ContactId && c.OwnerId == OwnerId)
                .Select(c => ContactDTO.Map(c))
                .FirstOrDefault();
            return _contact;
        }

        public void UpdateContact(Contact contact)
        {
            var _contact = _contacts.First(c => c.Id == contact.Id);
            _contact = contact;
        }
    }
}
