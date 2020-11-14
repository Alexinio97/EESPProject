import { Component } from "react";
import  patientService  from "../Services/patient.service";
import { Table, Spinner, Modal, Button } from "react-bootstrap";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash, faFileMedical} from "@fortawesome/free-solid-svg-icons";


export default class Patients extends Component {
    constructor(props){
        super(props);
        this.state = { patients: [] , 
            loading: true, 
            deleteModal: false,
            patientToDelete: null,
        }
    }

    componentDidMount(){
        this.populatePatientsList();
        console.log(this.state.patients.length);
    }

    async populatePatientsList(){
        await patientService.getAllPatients().then(response => {
            this.setState({patients: response.data, loading: false})
        }).catch(error => console.log("Error caught while downloading patients: " + error.message));
    }

    renderPatientsTable(){
        return(
            <Table>
                <thead>
                    <tr>
                    <th>Actiuni</th>
                    <th>Id</th>
                    <th>Prenume</th>
                    <th>Nume</th>
                    <th>CNP</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.patients.map( patient => 
                        <tr>
                            <td>
                                <button className="btn" title="Fisa medicala" onClick={() => this.props.history.push('/patient-file', patient)}>
                                    <FontAwesomeIcon icon={faFileMedical} color="blue" size="lg"/>
                                </button> 
                                <button className="btn" title="Sterge" onClick={() => this.setState({deleteModal: true,patientToDelete: patient})}>
                                    <FontAwesomeIcon icon={faTrash} color="red" size="lg"/>
                                </button>
                            </td>
                            <td>{patient.id}</td>
                            <td>{patient.firstName}</td>
                            <td>{patient.lastName}</td>
                            <td>{patient.cnp}</td>
                        </tr>
                        )}
                </tbody>
            </Table>
        )
    }

    renderDeleteDialog(patient){
        return(
            <Modal 
                show={this.state.deleteModal}
                onHide={() => this.setState({deleteModal: false})}
                backdrop="static"
                centered
            >
                <Modal.Header closeButton>
                    <Modal.Title>Stergere pacient</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <p>Sigur doriti sa stergeti pacientul {patient.firstName} {patient.lastName}?</p>
                </Modal.Body>

                <Modal.Footer>
                    <Button variant="secondary" onClick={() => this.setState({deleteModal:false})}>Nu</Button>
                    <Button variant="primary" onClick={() => this.deletePatient(patient.id)}>Da</Button>
                </Modal.Footer>
            </Modal>
        )
    }

    deletePatient(id)
    {
        patientService.deletePatient(id).then( response => 
            {
                console.log("Patient deleted, response: " + response.data);
                this.setState({deleteModal: false});
                this.populatePatientsList();
            }).catch(error => console.log("Error deleting patient " + error));
    }


    render()
    {
        let deleteDialogContent = this.state.deleteModal ? this.renderDeleteDialog(this.state.patientToDelete) : "";
        let content = this.state.loading ?
            <Spinner animation="border" variant="primary" style={{position:"absolute",top:"50%",left:"50%"}}>
            </Spinner> : this.renderPatientsTable();
        return(
            <div className="jumbotron mt-5 text-left" style={{opacity:"85%"}}>
                <div className="row">
                    <div className="col">
                        <h1 className="display-5">Lista pacienti</h1>
                    </div>
                    <div className="col-2 mt-2">
                        <Button onClick={() => this.props.history.push('/patient-file')}>Adauga pacient</Button>
                    </div>
                </div>
                
                <hr className="mb-5"/>
                {deleteDialogContent}
                {content}
            </div>
        )
    }
}