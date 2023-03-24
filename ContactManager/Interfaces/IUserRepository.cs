using ContactManager.Models;
using System.Collections.Generic;

namespace ContactManager.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers(bool trackChanges);

        User GetUserByUserNameAndPassWord(string UserName, string PassWord, bool trackChanges);

        void CreateUser(User user);
    }
}
