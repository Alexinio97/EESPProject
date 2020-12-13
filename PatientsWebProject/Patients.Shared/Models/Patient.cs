

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Patients.Shared.Models
{
    public enum Gender
    {
        male = 1,
        female = 2
    }

    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Prenumele trebuie inserat!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Numele trebuie inserat!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "CNP-ul trebuie completat!"), 
            StringLength(13, MinimumLength = 13, ErrorMessage = "CNP-ul trebuie sa aiba exact 13 caractere!")]
        public string CNP { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        [MaxLength(256,ErrorMessage ="Observatiile nu pot depasii 256 de caractere!")]
        public string Observations { get; set; }
        //  contact Data
        public string State { get; set; }
        public string City { get; set; }
        [EmailAddress(ErrorMessage = "Adresa de mail nu este valida!")]
        public string EmailAdress { get; set; }
        [StringLength(10,MinimumLength =10,ErrorMessage ="Telefonul trebuie sa contina exact 10 caractere.")]
        public string  Phone { get; set; }

        // consultations
        public ICollection<Consultation> Consultations { get; set; }

    }
}
