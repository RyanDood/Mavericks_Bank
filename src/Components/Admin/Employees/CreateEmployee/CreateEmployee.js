import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link } from 'react-router-dom';

function CreateEmployee() {

    var [email, setEmail] = useState("");
    var [password, setPassword] = useState("");
    var [confirmPassword, setConfirmPassword] = useState("");
    var [name, setName] = useState("");

    var newEmployee = {
        "email": email,
        "password": password,
        "name": name
    }

    async function addEmployee(){
        if (email === "" || password === "" || confirmPassword === "" || name === "") {
            console.log("Please fill in all fields");
        } else {
            if (email.includes("@maverick.in") && email.length > 5 && email.length < 50) {
                if (password.length >= 8 && password.length <= 15) {
                    if (password === confirmPassword) {
                        if (name.length > 2 && name.length < 100) {
                            await axios.post('http://localhost:5224/api/Validation/RegisterBankEmployees', newEmployee)
                            .then(function (response) {
                                console.log(response.data);
                            })
                            .catch(function (error) {
                                console.log(error);
                            });
                        } else {
                            console.log("Name should be between 6 and 100 characters long");
                        }
                    } else {
                        console.log("Passwords do not match");
                    }
                } else {
                    console.log("Password length should be between 8-15 characters");
                }
            } else {
                console.log("Invalid email");
            }
        }
    }

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox40 widthBox">
            <ul className="smallBox22 nav">
            <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoWhite smallBox23" to="/adminMenu/allEmployees">All Employees</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoGreen smallBox23" to="/adminMenu/createEmployee">Create Employee</Link>
                        </li>
                </ul>
                <div className="smallBox54">
                    <div className="scrolling">
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterText">Email</span>
                            <input className="form-control enterDiv7" type="email" value={email} onChange={(eventargs) => setEmail(eventargs.target.value)}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterText">Password</span>
                            <input className="form-control enterDiv7" type="password" onChange={(eventargs) => setPassword(eventargs.target.value)}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterText">Confirm Password</span>
                            <input className="form-control enterDiv7" type="password" onChange={(eventargs) => setConfirmPassword(eventargs.target.value)}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterText">Name</span>
                            <input className="form-control enterDiv7" type="text" value={name} onChange={(eventargs) => setName(eventargs.target.value)}></input>
                        </div>
                    </div>
                    <div className="smallBox55">
                    <a onClick={addEmployee} className="btn btn-outline-success smallBox9">
                        <span>Create</span>
                    </a>
                </div>
                </div>
                
            </div>
        </div>
    );
}

export default CreateEmployee;