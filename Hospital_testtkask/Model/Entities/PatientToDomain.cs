using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_testtkask.Model.Entities
{
	public class PatientToDomain
	{
		[ForeignKey(nameof(Patient.Id))]
		public int PatientId { get; set; }
		public int DomainId { get; set; }
	}
}
