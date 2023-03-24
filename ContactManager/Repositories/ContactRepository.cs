using ContactManager.Contexts;
using ContactManager.Interfaces;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactManager.Repositories
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(ContactsContext _contactsContext) : base(_contactsContext)
        {
        }

        public IEnumerable<ContactDTO> GetAllContacts(bool trackChanges)
        {
            return FindAll(trackChanges)
                    .OrderBy(c => c.FirstName)
                    .Select(c => ContactDTO.Map(c))
                    .ToList();
        }

        public IEnumerable<ContactDTO> GetAllUserContacts(Guid OwnerId, bool trackChanges)
        {
            return FindByCondition(o => o.OwnerId == OwnerId, trackChanges)
                    .OrderBy(c => c.FirstName)
                    .Select(c => ContactDTO.Map(c))
                    .ToList();
        }

        public ContactDTO GetUserContactDTOById(Guid OwnerId, Guid ContactId, bool trackChanges)
        {
            return FindByCondition(o => o.OwnerId == OwnerId && o.Id == ContactId, trackChanges)
                    .OrderBy(c => c.FirstName)
                    .Select(c => ContactDTO.Map(c))
                    .FirstOrDefault();
        }

        public Contact GetUserContactById(Guid OwnerId, Guid ContactId, bool trackChanges)
        {
            return FindByCondition(o => o.OwnerId == OwnerId && o.Id == ContactId, trackChanges)
                    .OrderBy(c => c.FirstName)
                    .FirstOrDefault();
        }

        public void CreateContact(Contact Contact) => Create(Contact);

        public void UpdateContact(Contact Contact) => Update(Contact);

        public void DeleteContact(Contact Contact) => Delete(Contact);
    }
}
