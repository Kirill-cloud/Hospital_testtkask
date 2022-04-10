using Hospital_testtkask.Model.Entities;

namespace Hospital_testtkask.Model.DTO
{
	public class DoctorDetails
	{
		public DoctorDetails(Doctor doctor)
		{
			Name = doctor.Name;
			Surname= doctor.Surname;
			Patronymic= doctor.Patronymic;
			CabinetId = doctor.Cabinet?.Id;
			SpecializationId = doctor.Specialization?.Id;
			DomainId = doctor.Domain?.Id;
		}

		public DoctorDetails()
		{

		}


		public string Name { get; set; }
		public string Surname { get; set; }
		public string Patronymic { get; set; }
		public int? CabinetId { get; set; }
		public int? SpecializationId { get; set; }
		public int? DomainId { get; set; }
		public int? Id { get; set; }
	}
}
