using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital_testtkask.Model.Entities
{
	public class EntityBase
	{
		[Key]
		public int? Id { get; set; }
	}
}
