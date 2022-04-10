using System;
using Hospital_testtkask.Model.Entities;
using Hospital_testtkask.Model.Enums;

namespace Hospital_testtkask.Model.DTO
{
	public class PatientDetails
	{
		public PatientDetails(Patient patient)
		{
			if (patient == null)
				throw new ArgumentException(nameof(patient));

			Name = patient.Name;
			Surname = patient.Surname;
			Patronymic = patient.Patronymic;
			Address = patient.Address;
			Gender = patient.Gender;
			DomainId = patient.Domain?.Id;
		}

		public PatientDetails()
		{

		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Patronymic { get; set; }
		public string Address { get; set; }
		public Gender Gender { get; set; }
		public int? DomainId { get; set; }
	}
}
