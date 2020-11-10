using Microsoft.EntityFrameworkCore;
using PatientsWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
