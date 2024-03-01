import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link } from 'react-router-dom';

function ForgotPassword(){

    var [email,setEmail]= useState("");
    var [password,setPassword]= useState("");
    var [confirmPassword,setConfirmPassword]= useState("");

    var newPassword = {
        "email": email,
        "password": password,
        "userType": "",
        "token": ""
    }

    var changePassword = async() => await axios.post('http://localhost:5224/api/Validation/ForgotPassword',newPassword).then(function (response) {
                                        console.log(response.data);
                                    })
                                    .catch(function (error) {
                                        console.log(error);
                                    })

    var changeExistingPassword = () => {
        if(email === "" || password === "" || confirmPassword === "") {
            console.log("Please fill in all fields");
        }
        else{
            if(email.includes("@") && email.includes(".com") && email.length > 5){
                if(password.length >= 8 && password.length <= 15){
                    if(password === confirmPassword){
                        changePassword();
                    }
                    else{
                        console.log("Passwords do not match");
                    }
                }
                else{
                    console.log("Password length should be between 8-15 characters");
                }
            }
            else{
                console.log("Invalid email");
            }
        }
    }

    return (
        <div className="container">
            <div className="orginalRow">
                <div className="smallBox1 col-sm-5">
                    <div className="topcorner2 flexRow">
                        <div className="logoImage change-my-color3"></div>
                        <span className="logo">mavericks</span>
                    </div>
                    <div  className="smallBox13">
                        <Link to="/">
                            <div className="back change-my-color"></div>
                        </Link>
                    </div>
                    <div className="smallBox11">
                        <span className="welcomeText">Secured!</span>
                        <div className="flexRow">
                            <span className="clickRegisterText">Banking made</span>
                            <span className="clickRegisterText2">Perfect</span>
                        </div>
                    </div>
                </div>
                <div className="smallBox4 col-sm-7">
                    <div className="smallBox7">
                        <div className="smallBox35">
                            <div className="smallBox14">
                                <div  className="smallBox13">
                                    <Link to="/">
                                        <div className="back change-my-color"></div>
                                    </Link>
                                </div>
                                <span className="welcomeText2">Secured!</span>
                                <div className="flexRow">
                                    <span className="clickRegisterText">Banking made</span>
                                    <span className="clickRegisterText2">Perfect</span>
                                </div>
                            </div>
                            <span className="welcomeText3 marginRegisterCustomer">Change Password</span>
                            <div className="scrolling">
                                <div className="marginRegisterCustomer">
                                    <span className="clickRegisterText">Email</span>
                                    <input className="form-control enterDiv" type="email" value={email} onChange={(eventargs) => setEmail(eventargs.target.value)}></input>
                                </div>
                                <div className="marginRegisterCustomer">
                                    <span className="clickRegisterText">Password</span>
                                    <input className="form-control enterDiv" type="password" onChange={(eventargs) => setPassword(eventargs.target.value)}></input>
                                </div>
                                <div className="marginRegisterCustomer">
                                    <span className="clickRegisterText">Confirm Password</span>
                                    <input className="form-control enterDiv" type="password" onChange={(eventargs) => setConfirmPassword(eventargs.target.value)}></input>
                                </div>
                            </div>
                        </div>
                        <div className="smallBox36">
                            <a onClick = {changeExistingPassword} className="btn btn-outline-success smallBox9">
                                <span>Change</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default ForgotPassword;