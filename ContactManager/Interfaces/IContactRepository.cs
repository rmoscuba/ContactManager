using ContactManager.Models;
using System;
using System.Collections.Generic;

namespace ContactManager.Interfaces
{
    public interface IContactRepository
    {
        IEnumerable<ContactDTO> GetAllContacts(bool trackChanges);

        IEnumerable<ContactDTO> GetAllUserContacts(Guid OwnerId, bool trackChanges);

        ContactDTO GetUserContactDTOById(Guid OwnerId, Guid ContactId, bool trackChanges);

        Contact GetUserContactById(Guid OwnerId, Guid ContactId, bool trackChanges);

        void CreateContact(Contact contact);

        void UpdateContact(Contact contact);

        void DeleteContact(Contact contact);
    }

}
