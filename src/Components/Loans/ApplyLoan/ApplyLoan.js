import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function ApplyLoan(){
    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox28">
                    <div className="upMargin">
                        <Link to="/menu/allLoans">
                            <div className="leftArrow change-my-color"></div>
                        </Link >
                    </div>
                    <span className="clickRegisterText">Loan Amount: 14,74,330</span>
                    <span className="clickRegisterText">Interest: 6.4%</span>
                    <span className="clickRegisterText">Tenure: 3 yrs</span>
                    <span className="clickRegisterText">Business Loan - Acc No xxxxxxxx5487</span>
                    <div>
                        <span className="clickRegisterText">Your Amount</span>
                        <input className="form-control enterDiv3" type="number"></input>
                    </div>
                    <div>
                        <span className="clickRegisterText">Purpose</span>
                        <textarea className="form-control enterDiv4" type="text"></textarea>
                    </div>
                    <a className="btn btn-outline-success smallBox9" href="applyLoan.html" data-bs-toggle="modal" data-bs-target="#modal1">
                        <span>Apply</span>
                    </a>
                </div>
                <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Apply Loan</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to apply for this Loan?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal">Apply</button>
                        </div>
                    </div>
                    </div>
                </div>
        </div>
    );
}

export default ApplyLoan;