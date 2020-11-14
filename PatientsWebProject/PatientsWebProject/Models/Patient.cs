using System.ComponentModel.DataAnnotations;

namespace PatientsWebProject.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, StringLength(13, MinimumLength = 13, ErrorMessage = "CNP-ul trebuie sa aiba exact 13 caractere!")]
        public string CNP { get; set; }
    }
}
