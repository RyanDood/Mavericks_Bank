import axios from 'axios';
import { useState } from 'react';
import '../../style.css';
import { Link, useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';

function Landing(){

    var [email,setEmail]= useState("");
    var [password,setPassword]= useState("");
    var navigate = useNavigate();

    var loginValidation = {
        "email": email,
        "password": password,
        "userType": "",
        "token": ""
    }

    var login = async() => await axios.post('http://localhost:5224/api/Validation/Login',loginValidation).then(function (response) {
                                console.log(response.data);
                                sessionStorage.setItem("email",response.data.email);
                                sessionStorage.setItem("token",response.data.token);
                                if(response.data.userType === "Customer"){
                                    getCustomerID();
                                }
                                else if(response.data.userType === "Employee"){
                                    getEmployeeID();
                                }
                            })
                            .catch(function (error) {
                                console.log(error);
                            })

    var loginUser = () => {
        if(email === "" || password === ""){
            console.log("Please fill in all fields");
        }
        else{
            if(email.includes("@") && email.includes(".") && email.length > 5){
                if(password.length >= 8 && password.length <= 15){
                    login();
                }
                else{
                    console.log("Password length should be between 8-15 characters");
                }
            }
            else{
                console.log("Invalid Email");
            }
        }
    }

    async function getCustomerID(){
        const email = sessionStorage.getItem('email');
        const token = sessionStorage.getItem('token');
        const httpHeader = { 
            headers: {'Authorization': 'Bearer ' + token}
        };
        await axios.get('http://localhost:5224/api/Customers/GetCustomerByEmail?email=' + email,httpHeader)
        .then(function (response) {
            console.log(response.data);
            sessionStorage.setItem("id",response.data.customerID);
            navigate("/menu/dashboard");
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function getEmployeeID(){
        const email = sessionStorage.getItem('email');
        const token = sessionStorage.getItem('token');
        const httpHeader = { 
            headers: {'Authorization': 'Bearer ' + token}
        };
        await axios.get('http://localhost:5224/api/BankEmployees/GetEmployeeByEmail?email=' + email,httpHeader)
        .then(function (response) {
            console.log(response.data);
            sessionStorage.setItem("id",response.data.employeeID);
            navigate("/employeeMenu/accounts");
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    return(
        <div className="container">
            <div className="row">
                <div className="smallBox1 col-sm-5">
                    <div className="topcorner2 flexRow">
                        <div className="logoImage change-my-color3"></div>
                        <span className="logo">mavericks</span>
                    </div>
                    <div className="smallBox2">
                        <span className="welcomeText">Welcome!</span>
                        <span className="clickRegisterText">New?, Click below to create account</span>
                        <Link className="btn btn-outline-success smallBox3" to="/registerCustomer">
                            <span>Start your journey</span>
                        </Link>
                    </div>
                </div>
                <div className="smallBox4 col-sm-7">
                    <div className="smallBox7">
                        <div className="smallBox5">
                            <span className="welcomeText2">Welcome!</span>
                            <span className="welcomeText3">Sign In</span>
                            <div>
                                <span className="clickRegisterText">Email</span>
                                <input className="form-control enterDiv" type="email" value={email} onChange={(eventargs) => setEmail(eventargs.target.value)}></input>
                            </div>
                            <div>
                                <span className="clickRegisterText">Password</span>
                                <input className="form-control enterDiv" type="password" value={password} onChange={(eventargs) => setPassword(eventargs.target.value)}></input>
                            </div>
                        </div>
                        <div className="smallBox6">
                            <Link className="clickRegisterText" to="/forgotPassword">Forgot Password?</Link>
                        </div>
                        <div className="smallBox8">
                            <a className="btn btn-outline-success smallBox9 noDisplay" href="register.html">
                                <span>Register</span>
                            </a>
                            <div className="pad">
                                <a onClick = {loginUser} className="btn btn-outline-success smallBox9">
                                    <span>Login</span>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Landing;