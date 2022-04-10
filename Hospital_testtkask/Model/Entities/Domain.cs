using System.Collections.Generic;

namespace Hospital_testtkask.Model.Entities
{
	public class Domain : EntityBase
	{
		public int Number { get; set; }
		public virtual List<Patient> Patients { get; set; }
	}
}
