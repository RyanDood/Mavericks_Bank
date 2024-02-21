import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link } from 'react-router-dom';

function AddBeneficiary(){
    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox41">
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoWhite" to="/menu/customerBeneficiaries">All Beneficiaries</Link>
                        </li>
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoGreen" to="/menu/addBeneficiary">Add Beneficiary</Link>
                        </li>
                    </ul>
                    <div>
                        <span className="clickRegisterText">Beneficiary Name</span>
                        <input className="form-control enterDiv3" type="email"></input>
                    </div>
                    <div>
                        <span className="clickRegisterText">Account Number</span>
                        <input className="form-control enterDiv3" type="password"></input>
                    </div>
                    <div>
                        <span className="clickRegisterText">Bank Name</span>
                        <input className="form-control enterDiv3" type="password"></input>
                    </div>
                    <div>
                        <span className="clickRegisterText">Branch Name with IFSC</span>
                        <input className="form-control enterDiv3" type="password"></input>
                    </div>
                    <a className="btn btn-outline-success smallBox9" href="" data-bs-toggle="modal" data-bs-target="#modal1">
                        <span>Add</span>
                    </a>
                </div>
                <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Add Beneficiary</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to add this beneficiary?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal">Add</button>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
    );
}

export default AddBeneficiary;