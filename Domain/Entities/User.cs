using System.ComponentModel.DataAnnotations;

namespace AdministratioSchool.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FamilyName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Password { get; set; }
    }
}
