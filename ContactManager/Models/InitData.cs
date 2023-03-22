using System.Linq;
using ContactManager.Contexts;
using ContactManager.Models;

namespace ContactManager.Models
{

    public static class InitData
    {
        public static void Seed(this ContactsContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                dbContext.Users.Add(new User
                {
                    FirstName = "José",
                    LastName = "Pérez",
                    UserName = "pepe",
                    PassWord = "1234",
                });

                dbContext.Users.Add(new User
                {
                    FirstName = "Francisco",
                    LastName = "García",
                    UserName = "paco",
                    PassWord = "4321",
                });

                dbContext.SaveChanges();
            }
        }
    }
}
