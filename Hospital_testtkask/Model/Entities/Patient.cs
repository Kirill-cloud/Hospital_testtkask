using System.Collections.Generic;
using Hospital_testtkask.Model.DTO;
using Hospital_testtkask.Model.Enums;

namespace Hospital_testtkask.Model.Entities
{
	public class Patient : EntityBase
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Patronymic { get; set; }
		public string Address { get; set; }
		public Gender Gender { get; set; }
		public Domain Domain { get; set; }

		public Patient(PatientDetails patientDetails, Domain domain)
		{
			Name = patientDetails.Name;
			Surname = patientDetails.Surname;
			Patronymic = patientDetails.Patronymic;
			Address = patientDetails.Address;
			Gender = patientDetails.Gender;
			Domain = domain;
		}

		public Patient()
		{
			
		}
	}
}
