using Patients.Shared.DTOs;
using Patients.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalClientBlazor.Services
{
    public class PatientService
    {
        private readonly string m_baseUri;
        private readonly HttpClient m_client;
        public PatientService(string baseUri)
        {
            m_baseUri = baseUri;
            m_client = new HttpClient();
        }
        public async Task <IEnumerable <Patient>> GetPatientsAsync()
        {
            try
            {
                var patients = await m_client.GetFromJsonAsync<Patient[]>($"{m_baseUri}patients");
                return patients;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex}");
                return null;
            }
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            try
            {
                var patient = await m_client.GetFromJsonAsync<Patient>($"{m_baseUri}patients/{id}");
                return patient;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex}");
                return null;
            }
        }

        public async Task<HttpResponseMessage> DeletePatientAsync(int id)
        {
            try
            {
                var response = await m_client.DeleteAsync($"{m_baseUri}patients/{id}");
                Console.WriteLine(response.Content.ToString());
                return response;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error at deleting patient: {ex}");
                return null;
            }
        }

        public async Task<HttpResponseMessage> CreatePatient(Patient patient)
        {
            try
            {
                var response = await m_client.PostAsJsonAsync($"{m_baseUri}patients",patient);
                return response;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<IEnumerable<PatientDTO>> GetPatientsShortNamesAsync()
        {
            try
            {
                var patients = await m_client.GetFromJsonAsync<PatientDTO[]>($"{m_baseUri}patients/consult-patients");
                return patients;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<PatientDTO> GetPatientsShortNamesByIdAsync(int id)
        {
            try
            {
                var patient = await m_client.GetFromJsonAsync<PatientDTO>($"{m_baseUri}patients/consult-patients/{id}");
                return patient;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<HttpResponseMessage> UpdatePatientAsync(Patient newPatient)
        {
            try
            {
                var response = await m_client.PutAsJsonAsync<Patient>($"{m_baseUri}patients", newPatient);
                return response;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
