import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function Profile(){
    return (
        <div className="smallBox17 col-sm-9">
            <div className="smallBox18">
                <div>
                    <span className="textDecoGreen">Personal Details</span> 
                </div>
                <div className="smallBox19"> 
                    <div className="margin1">
                        <span className="clickRegisterText">Name</span>
                        <input className="form-control enterDiv2" type="text"></input>
                    </div>
                    <div className="margin1">
                        <span className="clickRegisterText">Email</span>
                        <input className="form-control enterDiv2" type="email"></input>
                    </div>
                </div>
                <div className="smallBox19">
                    <div className="margin1">
                        <span className="clickRegisterText">Date of Birth</span>
                        <input className="form-control enterDiv2" type="date"></input>
                    </div>
                    <div className="margin1">
                        <span className="clickRegisterText">Phone Number</span>
                        <input className="form-control enterDiv2" type="number"></input>
                    </div>
                </div>
                <div className="smallBox19">
                    <div className="margin1">
                        <span className="clickRegisterText">Aadhaar Number</span>
                        <input className="form-control enterDiv2" type="number"></input>
                    </div>
                    <div className="margin1">
                        <span className="clickRegisterText">PAN Number</span>
                        <input className="form-control enterDiv2" type="text"></input>
                    </div>
                </div>
                <div className="smallBox19">
                    <div className="margin1">
                        <span className="clickRegisterText">Gender</span>
                        <input className="form-control enterDiv2" type="text"></input>
                    </div>
                    <div className="margin1">
                        <span className="clickRegisterText">Address</span>
                        <input className="form-control enterDiv2" type="text"></input>
                    </div>
                </div>
                <a className="btn btn-outline-success smallBox9 margin1" href="" data-bs-toggle="modal" data-bs-target="#modal1">
                    <span>Update</span>
                </a>
            </div>
            <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                    <h6 className="modal-title" id="modalEg1">Update Details</h6>
                    <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body">
                    Are you sure you want to update your details?
                    </div>
                    <div className="modal-footer">
                    <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                    <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal">Update</button>
                    </div>
                </div>
                </div>
            </div>
        </div>
    );
}

export default Profile;