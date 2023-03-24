using ContactManager.Models;
using System.Collections.Generic;

namespace ContactManager.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers(bool trackChanges);
        void CreateUser(User user);
    }
}
