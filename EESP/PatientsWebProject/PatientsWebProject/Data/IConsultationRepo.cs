using Patients.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientsWebProject.Data
{
    public interface IConsultationRepo
    {
        public Task<IEnumerable<Consultation>> GetConsultationsAsync();
        public Task<Consultation> GetConsultByIdAsync(int id);
        public Task CreateConsultationAsync(Consultation consult);
        public Task<bool> SaveChangesAsync();
        public void DeleteConsultation(Consultation consult);
        public Task<IEnumerable<Consultation>> GetPatientConsultationsAsync(int patientId);
        public Task<int> GetConsultationsCountAsync();
        public void UpdateConsultation(Consultation consult);
    }
}
