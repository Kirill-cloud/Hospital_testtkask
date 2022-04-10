using Hospital_testtkask.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital_testtkask.Model.DbContexts.Configurations
{
	public class PatientToDomainConfiguration : IEntityTypeConfiguration<PatientToDomain>
	{
	    public void Configure(EntityTypeBuilder<PatientToDomain> builder)
	    {
		    builder.HasKey(patientToDomainConfiguration => new { patientToDomainConfiguration.PatientId, patientToDomainConfiguration.DomainId});
	    }
    }
}
