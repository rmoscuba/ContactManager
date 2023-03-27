using ContactManager.Contexts;
using ContactManager.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private IJwtRepository _jwtRepository;
        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;

        public RepositoryManager(ContactsContext contactsContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _contactsContext = contactsContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
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

        public IJwtRepository Jwt
        {
            get
            {
                if (_jwtRepository == null)
                    _jwtRepository = new JwtRepository(_configuration, _httpContextAccessor);
                return _jwtRepository;
            }
        }

        public void Save() => _contactsContext.SaveChanges();
    }
}
