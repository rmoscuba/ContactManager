using System;

namespace ContactManager.Models
{
    using ContactManager.Validators;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Contact")]
    [Index(nameof(OwnerId), nameof(Email), IsUnique = true)]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(128)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(128)]
        public string LastName { get; set; }

        [StringLength(128)]
        [Required]
        public string Email { get; set; }

        [MinimunAge(18, "Contact must be 18 years or older")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(20)]
        [Required]
        public string Phone { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }

    }
}