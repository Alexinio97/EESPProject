using PatientsWebProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientsWebProject.Data
{
    public interface IPatientRepo
    {
        public Task<IEnumerable<Patient>> GetPatientsAsync();
        public Task<Patient> GetPatientByIdAsync(int id);
        public Task CreatePatientAsync(Patient patient);
        public Task<bool> SaveChangesAsync();
        public void DeletePatient(Patient patient);
    }
}
