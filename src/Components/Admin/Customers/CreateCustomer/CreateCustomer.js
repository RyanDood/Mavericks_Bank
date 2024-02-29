import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link } from 'react-router-dom';

function CreateCustomer() {

    var [email, setEmail] = useState("");
    var [password, setPassword] = useState("");
    var [confirmPassword, setConfirmPassword] = useState("");
    var [name, setName] = useState("");
    var [dob, setDob] = useState("");
    var [age, setAge] = useState(0);
    var [phoneNumber, setPhoneNumber] = useState("");
    var [address, setAddress] = useState("");
    var [aadharNumber, setAadharNumber] = useState("");
    var [panNumber, setPanNumber] = useState("");
    var [gender, setGender] = useState("Male");

    var newCustomer = {
        "email": email,
        "password": password,
        "name": name,
        "dob": dob,
        "age": age,
        "phoneNumber": phoneNumber,
        "address": address,
        "aadharNumber": aadharNumber,
        "panNumber": panNumber,
        "gender": gender
    };

    var calculateAge = (eventargs) => {
        setDob(eventargs.target.value);
        var age = Math.floor((new Date() - new Date(eventargs.target.value).getTime()) / 3.15576e+10);
        console.log(age);
        setAge(age);
    };

    var addCustomer = async () => await axios.post('http://localhost:5224/api/Validation/RegisterCustomers', newCustomer)
        .then(function (response) {
            console.log(response.data);
        })
        .catch(function (error) {
            console.log(error);
        });

    var registerCustomer = () => {
        if (email === "" || password === "" || confirmPassword === "" || name === "" || dob === "" || phoneNumber === "" || address === "" || aadharNumber === "" || panNumber === "") {
            console.log("Please fill in all fields");
        } else {
            if (email.includes("@") && email.includes(".") && email.length > 5 && email.length < 50) {
                if (password.length >= 8 && password.length <= 15) {
                    if (password === confirmPassword) {
                        if (name.length > 2 && name.length < 100) {
                            if (age >= 18) {
                                if (phoneNumber.length === 10) {
                                    if (address.length > 5 && address.length < 100) {
                                        if (aadharNumber.length === 12) {
                                            if (panNumber.length === 10) {
                                                addCustomer();
                                            } else {
                                                console.log("Pan number should be 10 characters");
                                            }
                                        } else {
                                            console.log("Aadhaar number should be 12 digits");
                                        }
                                    } else {
                                        console.log("Address should be between 6 and 100 characters long");
                                    }
                                } else {
                                    console.log("Phone number should be 10 digits");
                                }
                            } else {
                                console.log("Age should be 18 and above");
                            }
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
    };

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox40 widthBox">
            <ul className="smallBox22 nav">
            <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoWhite smallBox23" to="/adminMenu/allCustomers">All Customers</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoGreen  smallBox23" to="/adminMenu/createCustomer">Create Customer</Link>
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
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterText">Date of Birth</span>
                            <input className="form-control enterDiv7" type="date" value={dob} onChange={calculateAge}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterText">Phone Number</span>
                            <input className="form-control enterDiv7" type="number" value={phoneNumber} onChange={(eventargs) => setPhoneNumber(eventargs.target.value)}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterText">Address</span>
                            <input className="form-control enterDiv7" type="text" value={address} onChange={(eventargs) => setAddress(eventargs.target.value)}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterText">Aadhaar Number</span>
                            <input className="form-control enterDiv7" type="number" value={aadharNumber} onChange={(eventargs) => setAadharNumber(eventargs.target.value)}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterText">Pan Number</span>
                            <input className="form-control enterDiv7" type="text" value={panNumber} onChange={(eventargs) => setPanNumber(eventargs.target.value)}></input>
                        </div>
                        <div className="marginRegisterCustomer">
                            <span className="clickRegisterText">Gender</span>
                            <select className="form-control enterDiv7" onChange={(eventargs) => setGender(eventargs.target.value)}>
                                <option value="Male">Male</option>
                                <option value="Female">Female</option>
                                <option value="Others">Others</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div className="smallBox55">
                    <a onClick={registerCustomer} className="btn btn-outline-success smallBox9">
                        <span>Create</span>
                    </a>
                </div>
            </div>
        </div>
    );
}

export default CreateCustomer;