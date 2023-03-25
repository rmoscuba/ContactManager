using ContactManager.Interfaces;
using System;

namespace TestContactsWebAPI
{
    class RepositoryManagerFake : IRepositoryManager
    {
        public IContactRepository Contact => new ContactRepositoryFake();

        public IUserRepository User => throw new NotImplementedException();

        public IJwtRepository Jwt => throw new NotImplementedException();

        public void Save() {}
    }
}
