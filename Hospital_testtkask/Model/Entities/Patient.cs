using System.Collections.Generic;
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
		public List<Domain> Domains { get; set; }
	}
}
