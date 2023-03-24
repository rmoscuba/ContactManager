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
        public IEnumerable<User> GetAllUsers(bool trackChanges)
        {
            return FindAll(trackChanges)
                    .OrderBy(c => c.FirstName)
                    .ToList();
        }
        public User GetUserByUserNameAndPassWord(string UserName, string PassWord, bool trackChanges)
        {

            return FindByCondition(u => u.UserName == UserName && u.PassWord == PassWord, trackChanges)
                    .FirstOrDefault();
        }

        public void CreateUser(User user) => Create(user);
    }
}
