using Hospital_testtkask.Model.Entities;

namespace Hospital_testtkask.Model.DTO
{
	public class DoctorOverview
	{
		private readonly Doctor _doctor;

		public DoctorOverview(Doctor doctor)
		{
			_doctor = doctor;
		}
		public string Name => _doctor.Name;
		public string Surname => _doctor.Surname;
		public string Patronymic => _doctor.Patronymic;
		public int? Cabinet => _doctor.Cabinet?.Number;
		public string? Specialization => _doctor.Specialization?.Name;
		public string? Domain => _doctor.Domain?.Name;
	}
}
