using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class ContactDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }

        public Guid Owner { get; set; }

        internal static ContactDTO Map(Contact entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            ContactDTO dto = new ContactDTO();
            try
            {
                dto.Id = entity.Id;
                dto.FirstName = entity.FirstName;
                dto.LastName = entity.LastName;
                dto.Email = entity.Email;
                dto.DateOfBirth = entity.DateOfBirth;
                dto.Phone = entity.Phone;
                dto.Owner = entity.Owner.Id;
            }
            catch (Exception e)
            {
                string errMsg = String.Format("Error: {0} while mapping a Contact entity to DTO. Id: {1}.", e.Message, entity.Id);
                Console.WriteLine(errMsg);
            }

            return dto;
        }
    }
}
