using Patients.Shared.Models;

namespace MedicalClientBlazor.Helpers
{
    public class PatientHelper
    {
        public Patient ClonePatient(string patient)
        {
            var copyPatient = System.Text.Json.JsonSerializer.Deserialize<Patient>(patient);
            return copyPatient;
        }
    }
}
