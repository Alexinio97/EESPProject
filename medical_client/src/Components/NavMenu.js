import React, { Component } from 'react';
import { Form, Button, Navbar, FormControl, Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';


export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
      <header>
        <Navbar bg="dark" variant="dark" expand="lg">
            <Navbar.Brand href="#home">Bine ati venit</Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="mr-auto">
                    <Nav.Link href="/patients">Pacienti</Nav.Link>
                    <Nav.Link href="/">Consultatii</Nav.Link>
                </Nav>
                <Form inline>
                <FormControl type="text" placeholder="Search" className="mr-sm-2" />
                <Button variant="outline-success">Search</Button>
                </Form>
            </Navbar.Collapse>
        </Navbar>
      </header>
    );
  }
}