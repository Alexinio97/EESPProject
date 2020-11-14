using Microsoft.EntityFrameworkCore;
using PatientsWebProject.Models;

namespace PatientsWebProject.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> opt) : base (opt)
        {
            
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
