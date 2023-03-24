using ContactManager.Contexts;
using ContactManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private ContactsContext _contactsContext;
        private IContactRepository _contactRepository;
        private IUserRepository _userRepository;

        public RepositoryManager(ContactsContext contactsContext)
        {
            _contactsContext = contactsContext;
        }

        public IContactRepository Contact
        {
            get
            {
                if (_contactRepository == null)
                    _contactRepository = new ContactRepository(_contactsContext);
                return _contactRepository;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_contactsContext);
                return _userRepository;
            }
        }

        public void Save() => _contactsContext.SaveChanges();
    }
}
