using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Patients.Shared.DTOs;
using Patients.Shared.Models;
using PatientsWebProject.Data;

namespace PatientsWebProject.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientsController : Controller
    {
        private readonly IPatientRepo _patientRepo;
        private readonly IMapper _mapper;
        public PatientsController(IPatientRepo patientRepo,IMapper mapper)
        {
            _patientRepo = patientRepo;
            _mapper = mapper;
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
            if(patient == null)
            {
                return BadRequest();
            }
            if(await _patientRepo.GetPatientByIdAsync(patient.Id) != null)
            {
                return Conflict();
            }

            await _patientRepo.CreatePatientAsync(patient);
            await _patientRepo.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetPatientByIdAsync), new { patient.Id }, patient);
        }

        // PUT: api/patients
        [HttpPut]
        public async Task<ActionResult> UpdatePatient(Patient patient)
        {
            if(patient == null)
            {
                return BadRequest();
            }

            _patientRepo.UpdatePatient(patient);
            await _patientRepo.SaveChangesAsync();
            return NoContent();
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


        // Retrieves patients only with ID and name
        [HttpGet("consult-patients")]
        public async Task <ActionResult <IEnumerable<PatientDTO>>> GetPatientsIdAndNameAsync()
        {
            var commands = await _patientRepo.GetPatientsAsync();

            return Ok(_mapper.Map<IEnumerable<PatientDTO>>(commands));
        }

        // Retrieves one patient only with ID and name
        [HttpGet("consult-patients/{id}")]
        public async Task<ActionResult<PatientDTO>> GetPatientsIdAndNameByIdAsync(int id)
        {
            var patients = (List<Patient>) await  _patientRepo.GetPatientsAsync();
            var patientFound = patients.Find(e => e.Id == id);
            return Ok(_mapper.Map<PatientDTO>(patientFound));
        }

        
    }
}
