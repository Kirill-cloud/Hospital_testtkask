using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital_testtkask.Model.DbContexts;
using Hospital_testtkask.Model.DTO;
using Hospital_testtkask.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital_testtkask.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DoctorsController : ControllerBase
	{
		private readonly ILogger<DoctorsController> log;
		private readonly BaseDbContext _dbContext;

		public DoctorsController([FromServices] ILogger<DoctorsController> logger, BaseDbContext dbContext)
		{
			log = logger;
			_dbContext = dbContext;
		}


		[HttpGet]
		[Route("{id}/details")]
		public DoctorDetails Get(int id)
		{
			var doctor = _dbContext.Doctors
				.Include(d => d.Domain)
				.Include(d => d.Cabinet)
				.Include(d => d.Specialization)
				.FirstOrDefault(p => p.Id == id);
			if (doctor == null)
			{
				throw new ArgumentException($"Doctor with id:{id} does not exist");
			}
			return new DoctorDetails(doctor);
		}

		[HttpGet]
		[Route("{id}/overview")]
		public DoctorOverview GetOverview(int id)
		{
			var doctor = _dbContext.Doctors
				.Include(d => d.Domain)
				.Include(d => d.Cabinet)
				.Include(d => d.Specialization)
				.FirstOrDefault(p => p.Id == id);
			if (doctor == null)
			{
				throw new ArgumentException($"Doctor with id:{id} does not exist");
			}
			return new DoctorOverview(doctor);
		}

		[HttpGet]
		[Route("all")]
		public List<DoctorOverview> GetDoctors(int page = 0, int countOnPage = 0, string orderBy = null)
		{
			var doctors =
				_dbContext.Doctors
					.Include(p => p.Domain)
					.Include(p => p.Specialization)
					.Include(p => p.Cabinet);

			IOrderedQueryable<Doctor> orderedDoctors;

			switch (orderBy)
			{
				case null:
					orderedDoctors = doctors.OrderBy(p => p.Id);
					break;
				case "domain":
					orderedDoctors = doctors.OrderBy(p => p.Domain.Name);
					break;
				case "surname":
					orderedDoctors = doctors.OrderBy(p => p.Surname);
					break;
				case "cabinet":
					orderedDoctors = doctors.OrderBy(p => p.Cabinet.Number);
					break;
				case "specialization":
					orderedDoctors = doctors.OrderBy(p => p.Specialization.Name);
					break;
				default: throw new ArgumentException($"Unknown sorting field: {orderBy}");
			}

			var doctorsPage = orderedDoctors
				.Skip(countOnPage * page)
				.Take(countOnPage != 0 ? countOnPage : _dbContext.Doctors.Count());

			var doctorsOverview = new List<DoctorOverview>();

			foreach (var doctor in doctorsPage) // думаю было бы лучше тоже через linq, но не смог придумать как
			{
				doctorsOverview.Add(new DoctorOverview(doctor));
			}

			// if (doctors.Count() == 0) не уверен как тут лучше поступить 
			//	 return NoContent(); 

			return doctorsOverview;
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public async Task<ActionResult> DeletePatient(int id)
		{
			var doctor = _dbContext.Doctors
				.FirstOrDefault(p => p.Id == id);
			if (doctor == null)
			{
				throw new ArgumentException($"Doctor with id:{id} does not exist");
			}

			_dbContext.Doctors.Remove(doctor);
			await _dbContext.SaveChangesAsync();

			return Ok();
		}

		[HttpPost]
		[Route("create")]
		public async Task<ActionResult> CreateNew([FromBody] DoctorDetails doctor)
		{

			var newDoctor = BuildNewDoctor(doctor);
			_dbContext.Doctors.Add(newDoctor);
			await _dbContext.SaveChangesAsync();

			return Created(new Uri($"/Doctors/{newDoctor.Id}/details", UriKind.Relative), doctor);
		}

		[HttpPatch]
		[Route("edit")]
		public async Task<ActionResult> Edit([FromBody] DoctorDetails doctor)
		{
			var editedDoctor = BuildNewDoctor(doctor);
			editedDoctor.Id = doctor.Id;

			_dbContext.Doctors.Update(editedDoctor);
			await _dbContext.SaveChangesAsync();

			return Ok();
		}

		private Doctor BuildNewDoctor(DoctorDetails doctor)
		{
			var domain = _dbContext.Domains.FirstOrDefault(d => d.Id == doctor.DomainId);
			if (doctor.DomainId != null && domain == null)
				throw new ArgumentException($"Domain with id:{doctor.DomainId} does not exist");

			var specialization = _dbContext.Specializations.FirstOrDefault(d => d.Id == doctor.SpecializationId);
			if (specialization == null)
				throw new ArgumentException($"Specialization with id:{doctor.SpecializationId} does not exist");

			var cabinet = _dbContext.Cabinets.FirstOrDefault(d => d.Id == doctor.CabinetId);
			if (cabinet == null)
				throw new ArgumentException($"Cabinet with id:{doctor.SpecializationId} does not exist");

			var newDoctor = new Doctor(doctor, cabinet, specialization, domain);
			return newDoctor;
		}
	}
}
