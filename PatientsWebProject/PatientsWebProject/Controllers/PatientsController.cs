using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientsWebProject.Data;
using PatientsWebProject.Models;

namespace PatientsWebProject.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientsController : Controller
    {
        private readonly IPatientRepo _patientRepo;
        public PatientsController(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }

        // GET: api/patients
        [HttpGet]
        public async Task <ActionResult <IEnumerable<Patient>>> GetPatientsAsync()
        {
            var patients = await _patientRepo.GetPatientsAsync();

            return Ok(patients);
        }

        // GET: api/patients/{id}
        [HttpGet("{id}",Name = "GetPatientByIdAsync")]
        public async Task<ActionResult <Patient>> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepo.GetPatientByIdAsync(id);

            if(patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        // POST: api/patients
        [HttpPost]
        public async Task<ActionResult> CreatePatientAsync(Patient patient)
        {
            Console.WriteLine(patient);
            if(patient == null)
            {
                return BadRequest();
            }

            await _patientRepo.CreatePatientAsync(patient);
            await _patientRepo.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetPatientByIdAsync), new { Id = patient.Id }, patient);
        }

        // GET: PatientsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }


        // GET: api/patients/{id}
        [HttpDelete("{id}")]
        public async Task <ActionResult> Delete(int id)
        {
            var patientFromFrepo = await _patientRepo.GetPatientByIdAsync(id);
            if(patientFromFrepo == null)
            {
                return BadRequest();
            }

            _patientRepo.DeletePatient(patientFromFrepo);
            await _patientRepo.SaveChangesAsync();
            return NoContent();
        }
    }
}
