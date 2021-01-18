using Microsoft.EntityFrameworkCore;
using Patients.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientsWebProject.Data
{
    public class SqlConsultationRepo : IConsultationRepo
    {
        public ProjectContext _dbContext;
        public SqlConsultationRepo(ProjectContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateConsultationAsync(Consultation consult)
        {
            if(consult == null)
            {
                throw new ArgumentNullException(nameof(consult));
            }
            await _dbContext.Consultations.AddAsync(consult);
        }

        public void DeleteConsultation(Consultation consult)
        {
            if (consult == null)
            {
                throw new ArgumentNullException(nameof(consult));
            }

            _dbContext.Consultations.Remove(consult);
        }

        public async Task<IEnumerable<Consultation>> GetConsultationsAsync()
        {
            return await _dbContext.Consultations.ToListAsync();
        }

        public async Task<Consultation> GetConsultByIdAsync(int id)
        {
            return await _dbContext.Consultations.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Consultation>> GetPatientConsultationsAsync(int patientId)
        {
            var patientConsultations = await _dbContext.Consultations.Where(consult => 
                        consult.PatientId == patientId).ToListAsync();

            return patientConsultations;
        }

        // retrieves only the count of consultations in the database
        public async Task<int> GetConsultationsCountAsync()
        {
            var consultationsCount = await _dbContext.Consultations.CountAsync();

            return consultationsCount;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }

        public void UpdateConsultation(Consultation consult)
        {
            _dbContext.Update<Consultation>(consult);
        }
    }
}
