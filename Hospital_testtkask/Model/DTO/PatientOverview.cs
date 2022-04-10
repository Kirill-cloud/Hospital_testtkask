using Hospital_testtkask.Model.Entities;
using Hospital_testtkask.Model.Enums;

namespace Hospital_testtkask.Model.DTO
{
	public class PatientOverview
	{
		private Patient patient;
		public PatientOverview(Patient patient)
		{
			this.patient = patient;
		}

		public string Name => patient.Name;
		public string Surname => patient.Surname;
		public string Patronymic => patient.Patronymic;
		public string Address => patient.Address;
		public string Gender => GenderHelper.FromGender(patient.Gender);
		public string Domain => patient.Domain?.Name;
	}
}
