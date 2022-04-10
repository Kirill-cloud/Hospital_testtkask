using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital_testtkask.Model.DbContexts;
using Hospital_testtkask.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital_testtkask.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PatientsController : ControllerBase
	{
		private readonly BaseDbContext _dbContext;

		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<PatientsController> _logger;

		public PatientsController([FromServices] BaseDbContext dbContext, [FromServices] ILogger<PatientsController> logger)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		[HttpGet]
		[Route("{id}/details")]
		public Patient Get(int id)
		{
			var patient = _dbContext.Patients
				.AsNoTracking()
				.Include(p => p.Domains)
				.FirstOrDefault(p => p.Id == id);
			if (patient == null)
			{
				throw new ArgumentException($"User with id:{id} does not exist");
			}
			foreach (var item in patient?.Domains)
			{
				item.Patients = null;
			}
			return patient;
		}

		[HttpPost]
		[Route("create")]
		public async Task<ActionResult> Get([FromBody] Patient patient)
		{
			var requestedDomains = patient.Domains.Select(d => d.Id).ToList();
			var patientDomains = _dbContext.Domains.Where(d => requestedDomains.Contains(d.Id)).ToList();

			patient.Id = null;
			patient.Domains = patientDomains;
			_dbContext.Patients.Add(patient);
			await _dbContext.SaveChangesAsync();

			return Created(new Uri($"/Patients/{patient.Id}/details", UriKind.Relative), patient);
		}
	}
}
