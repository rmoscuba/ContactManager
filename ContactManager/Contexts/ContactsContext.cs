namespace ContactManager.Contexts
{
    using ContactManager.Models;
    using Microsoft.EntityFrameworkCore;

    public class ContactsContext: DbContext
    {
        public ContactsContext(DbContextOptions options): base(options) {}

        public DbSet<User> Users { get; set; }

        public DbSet<Contact> Contacts { get; set; }

    }
}
