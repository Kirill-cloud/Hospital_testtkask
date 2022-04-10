using System;

namespace Hospital_testtkask.Model.Entities
{
	public class Doctor : EntityBase
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Patronymic { get; set; }
		public int CabinetId { get; set; }
		public Cabinet Cabinet{ get; set; }
		public int SpecializationId { get; set; }
		public Specialization Specialization { get; set; }
		public int DomainId { get; set; }
		public Domain Domain { get; set; }
	}
}
