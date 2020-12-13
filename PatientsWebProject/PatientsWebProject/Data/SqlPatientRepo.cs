using Microsoft.EntityFrameworkCore;
using Patients.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientsWebProject.Data
{
    public class SqlPatientRepo : IPatientRepo
    {
        public ProjectContext _dbContext;
        public SqlPatientRepo(ProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreatePatientAsync(Patient patient)
        {
            if(patient == null)
            {
                throw new ArgumentNullException(nameof(patient));
            }

            await _dbContext.Patients.AddAsync(patient);
        }

        public void DeletePatient(Patient patient)
        {
            if(patient == null)
            {
                throw new ArgumentNullException(nameof(patient));
            }

            _dbContext.Patients.Remove(patient);
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            return await _dbContext.Patients.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}
