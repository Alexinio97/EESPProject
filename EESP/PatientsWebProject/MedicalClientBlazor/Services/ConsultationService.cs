using Patients.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MedicalClientBlazor.Services
{
    public class ConsultationService
    {
        private readonly string m_baseUri;
        private readonly HttpClient m_client;
        public ConsultationService(string baseUri)
        {
            m_baseUri = baseUri;
            m_client = new HttpClient();
        }

        public async Task<IEnumerable<Consultation>> GetConsultationsPartsAsync(int count)
        {
            try
            {
                var consultations = await m_client.GetFromJsonAsync<Consultation[]>($"{m_baseUri}consultations/consult-part/{count}");
                return consultations;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex}");
                return null;
            }
        }

        public async Task<Consultation> GetConsultationByIdAsync(int id)
        {
            try
            {
                var consultation = await m_client.GetFromJsonAsync<Consultation>($"{m_baseUri}consultations/{id}");
                return consultation;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex}");
                return null;
            }
        }

        public async Task<HttpResponseMessage> DeleteConsultationAsync(int id)
        {
            try
            {
                var response = await m_client.DeleteAsync($"{m_baseUri}consultations/{id}");
                Console.WriteLine(response.Content.ToString());
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error at deleting patient: {ex}");
                return null;
            }
        }

        public async Task<HttpResponseMessage> CreateConsultationAsync(Consultation consult)
        {
            try
            {
                var response = await m_client.PostAsJsonAsync($"{m_baseUri}consultations", consult);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<IEnumerable <Consultation>> GetPatientConsultationsAsync(int patientId)
        {
            try
            {
                var consultations = await m_client.GetFromJsonAsync<Consultation[]>($"{m_baseUri}consultations/patient/{patientId}");
                return consultations;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex}");
                return null;
            }
        }

        public async Task<int> GetConsultationsCountAsync()
        {
            try
            {
                var count = await m_client.GetFromJsonAsync<int>($"{m_baseUri}consultations/consultations-count");
                return count;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex}");
                return 0;
            }
        }

        public async Task<IEnumerable<Consultation>> GetTodayConsultationsAsync()
        {
            try
            {
                var consultations = await m_client.GetFromJsonAsync<Consultation[]>($"{m_baseUri}consultations/today");
                return consultations;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex}");
                return null;
            }
        }

        public async Task<HttpResponseMessage> UpdateConsultationAsync(Consultation consult)
        {
            try
            {
                var respone = await m_client.PutAsJsonAsync<Consultation>($"{m_baseUri}consultations", consult);
                return respone;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception caught at PUT: {ex}");
                return null;
            }
        }
    }
}
