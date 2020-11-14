import { Route } from 'react-router-dom';
import './App.css';
import { Layout } from './Components/Layout';
import PatientFile from './Pages/PatientMedicalFile';
import Patients from './Pages/Patients';


function App() {
  return (
    <div className="App">
     <Layout>
          <Route exact path="/patients" component={Patients} />
          <Route exact path="/patient-file" component={PatientFile}/>
     </Layout>
    </div>
  );
}

export default App;
