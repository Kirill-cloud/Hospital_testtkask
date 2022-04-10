using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_testtkask.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DoctorsController : ControllerBase
	{
		private readonly ILogger<DoctorsController> log;

		public DoctorsController([FromServices] ILogger<DoctorsController> logger)
		{
			log = logger;
		}

		[HttpPost]
		[Route("doctors/get2")]
		public string Get()
		{
			return "DoctorsController";
		}
	}
}
