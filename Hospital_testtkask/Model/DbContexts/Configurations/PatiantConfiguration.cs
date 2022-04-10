using Hospital_testtkask.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital_testtkask.Model.DbContexts.Configurations
{
	public class PatientConfiguration : IEntityTypeConfiguration<Patient>
	{
		public void Configure(EntityTypeBuilder<Patient> builder)
		{
			builder
				.HasMany(p => p.Domains)
				.WithMany(d => d.Patients)
				.UsingEntity(j => j.ToTable(nameof(PatientToDomain)));

		}
	}
}
