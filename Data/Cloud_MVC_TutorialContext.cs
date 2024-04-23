using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cloud_MVC_Tutorial.Models;

namespace Cloud_MVC_Tutorial.Data
{
    public class Cloud_MVC_TutorialContext : DbContext
    {
        public Cloud_MVC_TutorialContext (DbContextOptions<Cloud_MVC_TutorialContext> options)
            : base(options)
        {
        }

        public DbSet<Cloud_MVC_Tutorial.Models.Student> Student { get; set; } = default!;
        public DbSet<Cloud_MVC_Tutorial.Models.Course> Course { get; set; } = default!;
        public DbSet<Cloud_MVC_Tutorial.Models.Enrollment> Enrollment { get; set; } = default!;

        public DbSet<Cloud_MVC_Tutorial.Models.EnrollmentHistory> EnrollmentHistories { get; set; } = default!;
    }
}
