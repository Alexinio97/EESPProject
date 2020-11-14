import { differenceInYears, format, parse } from "date-fns";
import { Component } from "react";
import { Button, Col, Form, Row } from "react-bootstrap";
import patientService from "../Services/patient.service";


export default class PatientFile extends Component{
    constructor(props){
        super(props);
        this.state = {
            firstName: "",
            lastName: "",
            cnp: "",
            age: "",
            birthDate: "",
            gender: "",
        }
        this.handleChange = this.handleChange.bind(this);
        this.createPatient = this.createPatient.bind(this);
    }

    componentDidMount(){
        if(this.props.location.state !== undefined)
        {
                let patientReceived = this.props.location.state;
                this.setState({
                    firstName: patientReceived.firstName,
                    lastName: patientReceived.lastName,
                    cnp: patientReceived.cnp
                });
        }
    }

    calculateData(cnpValue){
        let cnp = cnpValue;
        if(cnp.length === 13)
        {
            let gender = cnp[0];
            
            let birthDateRaw = cnp.substring(1,7);
            let birthDateObj = { 
                year: birthDateRaw.substring(0,2),
                month: birthDateRaw.substring(2,4),
                day: birthDateRaw.substring(4,7),
            }
            
            let year = new Date(birthDateObj.year,birthDateObj.month,birthDateObj.day).getFullYear();
            console.log(year);
            let birthDay = parse(`${year}-${birthDateObj.month}-${birthDateObj.day}`,'yyyy-MM-dd',new Date());
            let age = differenceInYears(new Date(),birthDay);
            this.setState({
                gender: gender,
                birthDate: format(birthDay,"dd/MM/yyyy"),
                age: age
            });
        }
        
    }

    handleChange(event){
        const { name,value } = event.target;
        this.setState({[name]:value})
        if(name === "cnp"){
            this.calculateData(value);
        }
    }

    async createPatient(){
        
        let patient = {
            firstName: this.state.firstName,
            lastName: this.state.lastName,
            CNP: this.state.cnp
        }
        console.log(patient);
        await patientService.createPatient(patient)
            .then(response => {
                console.log("User created with succes: " + response.data);
                this.props.history.push("/patients");
            })
            .catch(error => console.log("Patient could not be created - error: " + error.message));
    }

    renderPatientFileForm(){
        return(
            <Form>
                <Form.Group>
                    <Row>
                        <Col>
                            <Form.Label sm={3}>Nume</Form.Label>
                            <Form.Control placeholder="Vasile" type="text" name="lastName" value={this.state.lastName} onChange={this.handleChange}/>
                        </Col>
                        <Col sm={3}>
                            <Form.Label>Prenume</Form.Label>
                            <Form.Control placeholder="Ion" type="text" name="firstName" value={this.state.firstName} onChange={this.handleChange}/>  
                        </Col>
                        <Col xl={6}>
                            <Form.Label>CNP</Form.Label>
                            <Form.Control placeholder="194..." type="text" name="cnp" value={this.state.cnp} onChange={this.handleChange}/>
                            <Form.Text muted>Prin completarea cnp-ului data de nastere, sexul si varsta vor fii compeltate automat.</Form.Text>  
                        </Col>
                    </Row>
                </Form.Group>
                <Form.Group as={Row} className="mt-4 ml-1">
                    <Col sm={4}>
                        <Row>
                            <Col sm={5} className="mt-2 text-left"> <Form.Label className="">Gen</Form.Label> </Col>
                            <Col> <Form.Control placeholder="Introduceti cnp..." plaintext="true" readOnly={true} value={this.state.gender}/> </Col>
                        </Row>
                        <Row>
                            <Col sm={5} className="mt-2 text-left"> <Form.Label>Varsta</Form.Label> </Col>
                            <Col> <Form.Control placeholder="Introduceti cnp..." plaintext="true" type="number" readOnly={true} value={this.state.age}/> </Col>
                        </Row>
                        <Row>
                            <Col sm={5} className="mt-2 text-left"> <Form.Label>Data nasterii</Form.Label> </Col>
                            <Col> <Form.Control placeholder="Introduceti cnp..." plaintext="true" type="text" readOnly={true} value={this.state.birthDate}/> </Col>
                        </Row>
                    </Col>
                </Form.Group>
                <Form.Group>
                    <Button onClick={this.createPatient}>Adauga pacient</Button>
                </Form.Group>
            </Form>
        )
    }


    render(){
        
        return(
            <div className="jumbotron mt-4" style={{opacity:"85%"}}>
                <h2 className="display-6 text-center">Fisa medicala pacient</h2>
                <hr className="mb-2"/>
                {this.renderPatientFileForm()}
            </div>
        )
    }
}