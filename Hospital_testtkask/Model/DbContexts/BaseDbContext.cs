using Hospital_testtkask.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital_testtkask.Model.DbContexts
{
	public class BaseDbContext : DbContext
	{
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Domain> Domains { get; set; }
		public DbSet<Cabinet> Cabinets { get; set; }
		public DbSet<Specialization> Specializations { get; set; }
		public BaseDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}
	}
}
