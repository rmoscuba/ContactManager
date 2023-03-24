using ContactManager.Contexts;
using ContactManager.Interfaces;
using ContactManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContactManager.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ContactsContext _contactsContext) : base(_contactsContext)
        {
        }
        public IEnumerable<User> GetAllUsers(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.FirstName)
            .ToList();

        public void CreateUser(User user) => Create(user);
    }
}
