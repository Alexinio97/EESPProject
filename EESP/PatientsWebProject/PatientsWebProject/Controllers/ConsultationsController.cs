using Microsoft.AspNetCore.Mvc;
using Patients.Shared.Models;
using PatientsWebProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientsWebProject.Controllers
{
    [ApiController]
    [Route("api/consultations")]
    public class ConsultationsController : Controller
    {
        private readonly IConsultationRepo _consultRepo;
        public ConsultationsController(IConsultationRepo consultRepo)
        {
            _consultRepo = consultRepo;
        }
        
        // TODO: new endpoint that retrieves the number of all consulations
        // TODO: modified endpoint that retrieves a certain amount of consultations
        [HttpGet("consult-part/{count}")]
        public async Task<ActionResult <ICollection<Consultation>>> GetAllConsultations(int count)
        {
            if(count == 0)
            {
                return BadRequest();
            }
            List<Consultation> consultations = (List<Consultation>)await _consultRepo.GetConsultationsAsync();
            try
            {
                List<Consultation> splittedConsults = new List<Consultation>();
                var startIndex = (count * 6) - 6;
                for(int i = startIndex; i < consultations.Count; i++)
                {
                    if(splittedConsults.Count == 6)
                    {
                        break;
                    }    
                    splittedConsults.Add(consultations[i]);
                }
                return Ok(splittedConsults);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("consultations-count")]
        public async Task<ActionResult <int>> GetConsultationsCount()
        {
            int count = await _consultRepo.GetConsultationsCountAsync();

            return Ok(count);
        }


        [HttpGet("{id}",Name = "GetConsultationByIdAsync")]
        public async Task<ActionResult <Consultation>> GetConsultationByIdAsync(int id)
        {
            var consult = await _consultRepo.GetConsultByIdAsync(id);

            if (consult == null)
            {
                return NotFound();
            }
            return Ok(consult);
        }

        [HttpGet("patient/{patientId}",Name = "GetPatientConsultationsAsync")]
        public async Task<ActionResult <IEnumerable<Consultation>>> GetPatientConsultationsAsync(int patientId)
        {
            var consultations = await _consultRepo.GetPatientConsultationsAsync(patientId);

            if(consultations == null)
            {
                return NoContent();
            }
            return Ok(consultations);
        }

        [HttpGet("today")]
        public async Task<ActionResult<List <Consultation>>> GetTodayConsultations()
        {
            var consultations = await _consultRepo.GetConsultationsAsync();
            consultations = consultations.Where(c => c.Date.Date == DateTime.Today.Date);
            if (!consultations.Any())
            {
                return NoContent();
            }
            return Ok(consultations);
        }

        // POST: api/consultations
        [HttpPost]
        public async Task<ActionResult> CreateConsultAsync(Consultation consult)
        {
            if (consult == null)
            {
                return BadRequest();
            }
            if (await _consultRepo.GetConsultByIdAsync(consult.Id) != null)
            {
                return Conflict();
            }

            await _consultRepo.CreateConsultationAsync(consult);
            try { 
                await _consultRepo.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Save changes to database exception: " + ex.ToString());
            }

            return CreatedAtRoute(nameof(GetConsultationByIdAsync), new { consult.Id }, consult);
        }

        // DELETE: api/consultations/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveConsultAsync(int id)
        {
            var consultFound = await _consultRepo.GetConsultByIdAsync(id);
            if(consultFound == null)
            {
                return NotFound();
            }

            _consultRepo.DeleteConsultation(consultFound);
            await _consultRepo.SaveChangesAsync();
            return NoContent();
        }

        //PUT: api/consultations
        [HttpPut]
        public async Task<ActionResult> UpdateConsultationAsync(Consultation consult)
        {
            if(consult == null)
            {
                return BadRequest();
            }

            _consultRepo.UpdateConsultation(consult);
            await _consultRepo.SaveChangesAsync();
            return NoContent();
        }
    }
}
