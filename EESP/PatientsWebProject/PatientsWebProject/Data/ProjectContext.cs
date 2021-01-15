using Microsoft.EntityFrameworkCore;
using Patients.Shared.Models;

namespace PatientsWebProject.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> opt) : base (opt)
        {
            
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
    }
}
