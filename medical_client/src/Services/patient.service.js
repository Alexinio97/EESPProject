import  http from './http.service'

export default new class PatientService{

    
    getAllPatients(){
        return http.get('/api/patients');
    }

    getPatientById(id){
        return http.get(`/api/patients/${id}`);
    }

    createPatient(patient){
        return http.post('/api/patients',patient);
    }

    deletePatient(id){
        return http.delete(`/api/patients/${id}`);
    }
}

