using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patients.Shared.Models
{
    public class Consultation
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public string ConsultationReason { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
    }
}
