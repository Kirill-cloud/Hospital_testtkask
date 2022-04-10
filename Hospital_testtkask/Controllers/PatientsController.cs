using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital_testtkask.Model.DbContexts;
using Hospital_testtkask.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Hospital_testtkask.Model.DTO;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Hospital_testtkask.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PatientsController : ControllerBase
	{
		private readonly BaseDbContext _dbContext;
		private readonly ILogger<PatientsController> _logger;

		public PatientsController([FromServices] BaseDbContext dbContext, [FromServices] ILogger<PatientsController> logger)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		[HttpGet]
		[Route("{id}/details")]
		public PatientDetails Get(int id)
		{
			var patient = _dbContext.Patients
				.AsNoTracking()
				.Include(p => p.Domain)
				.FirstOrDefault(p => p.Id == id);
			if (patient == null)
			{
				throw new ArgumentException($"Patient with id:{id} does not exist");
			}
			return new PatientDetails(patient);
		}

		[HttpGet]
		[Route("{id}/overview")]
		public PatientOverview GetOverview(int id)
		{
			var patient = _dbContext.Patients
				.AsNoTracking()
				.Include(p => p.Domain)
				.FirstOrDefault(p => p.Id == id);
			if (patient == null)
			{
				throw new ArgumentException($"Patient with id:{id} does not exist");
			}
			return new PatientOverview(patient);
		}

		[HttpGet]
		[Route("all")]
		public List<PatientOverview> GetPatients(int page = 0, int countOnPage = 0)
		{
			var patients =
				_dbContext.Patients
					.Include(p => p.Domain)
					.OrderBy(p => p.Name)
					.Skip(countOnPage * page)
					.Take(countOnPage != 0 ? countOnPage : _dbContext.Patients.Count());

			var patientsOverview = new List<PatientOverview>();

			foreach (var patient in patients) // думаю было бы лучше тоже через linq, но не смог придумать как
			{
				patientsOverview.Add(new PatientOverview(patient));
			}

			// if (patients.Count() == 0) не уверен как тут лучше поступить 
			//	 return NoContent(); 

			return patientsOverview;
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public async Task<ActionResult> DeletePatient(int id)
		{
			var patient = _dbContext.Patients
				.AsNoTracking()
				.Include(p => p.Domain)
				.FirstOrDefault(p => p.Id == id);
			if (patient == null)
			{
				throw new ArgumentException($"User with id:{id} does not exist");
			}

			_dbContext.Patients.Remove(patient);
			await _dbContext.SaveChangesAsync();

			return Ok();
		}

		[HttpPost]
		[Route("create")]
		public async Task<ActionResult> CreateNew([FromBody] PatientDetails patient)
		{

			var patientDomain = _dbContext.Domains.FirstOrDefault(d => d.Id == patient.DomainId);

			var newPatient = new Patient(patient, patientDomain);
			_dbContext.Patients.Add(newPatient);
			await _dbContext.SaveChangesAsync();

			return Created(new Uri($"/Patients/{newPatient.Id}/details", UriKind.Relative), patient);
		}

		[HttpPatch]
		[Route("edit")]
		public async Task<ActionResult> Edit([FromBody] PatientDetails patient)
		{

			var patientDomain = _dbContext.Domains.FirstOrDefault(d => d.Id == patient.DomainId);
			var patientToEdit = _dbContext.Patients.AsNoTracking().FirstOrDefault(p => p.Id == patient.Id);

			if (patientToEdit == null)
				throw new ArgumentException(nameof(patient.Id));

			patientToEdit = new Patient(patient, patientDomain);
			patientToEdit.Id = patient.Id;

			_dbContext.Patients.Update(patientToEdit);
			await _dbContext.SaveChangesAsync();

			return Ok();
		}
	}
}
