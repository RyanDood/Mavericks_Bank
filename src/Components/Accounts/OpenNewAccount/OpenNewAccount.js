import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link } from 'react-router-dom';
import CustomerDetails from '../../Profile/CustomerDetails/CustomerDetails';

function OpenNewAccount(){
    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox40">
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoWhite smallBox23" to="/menu/customerAccounts">All Accounts</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoGreen smallBox23" to="/menu/openAccount">Open New Account</Link>
                        </li>
                    </ul>
                    <CustomerDetails/>
                    <div className="smallBox25">
                        <div>
                            <span className="clickRegisterText">Account Type</span>
                            <input className="form-control enterDiv2" type="text"></input>
                        </div>
                        <a className="btn btn-outline-success smallBox24" href="" data-bs-toggle="modal" data-bs-target="#modal1">
                            <span>Create Account</span>
                        </a>
                    </div>
                </div>
                <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Create Account</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to create account?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal">Create</button>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
    );
}

export default OpenNewAccount;