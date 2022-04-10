using System;
using Hospital_testtkask.Model.DTO;

namespace Hospital_testtkask.Model.Entities
{
	public class Doctor : EntityBase
	{
		public Doctor(DoctorDetails doctor, Cabinet cabinet, Specialization specialization, Domain domain)
		{
			Name = doctor.Name;
			Surname = doctor.Surname;
			Patronymic = doctor.Patronymic;
			Cabinet = cabinet;
			Specialization = specialization;
			Domain = domain;
		}

		public Doctor()
		{
			
		}

		public string Name { get; set; }
		public string Surname { get; set; }
		public string Patronymic { get; set; }
		public Cabinet Cabinet{ get; set; }
		public Specialization Specialization { get; set; }
		public Domain? Domain { get; set; }
	}
}
