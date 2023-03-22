using System;

namespace ContactManager.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(128)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(128)]
        [Required]
        public string LastName { get; set; }

        [StringLength(60)]
        [Required]
        public string UserName { get; set; }

        [StringLength(256)]
        [Required]
        public string PassWord { get; set; }

    }
}