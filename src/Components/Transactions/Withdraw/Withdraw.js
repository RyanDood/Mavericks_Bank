import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function Withdraw(){
    return(
<div className="smallBox17 col-md-9">
                <div className="smallBox29">
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoWhite" to="/menu/customerTransactions">History</Link>
                        </li>
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoWhite" to="/menu/transferMoney">Transfer Money</Link>
                        </li>
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoWhite" to="/menu/depositMoney">Deposit</Link>
                        </li>
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoGreen" to="/menu/withdrawMoney">Withdraw</Link>
                        </li>
                    </ul>
                    <div className="smallBox30"> 
                        <div>
                            <span className="clickRegisterText">Amount</span>
                            <input className="form-control enterDiv2" type="number"></input>
                        </div>
                        <div>
                            <span className="clickRegisterText">From (Account Number)</span>
                            <input className="form-control enterDiv2" type="number"></input>
                        </div>
                    </div>
                    <a className="btn btn-outline-success smallBox31" href="" data-bs-toggle="modal" data-bs-target="#modal1">
                        <span>Withdraw</span>
                    </a>
                </div>
                <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                        <div className="modal-content">
                            <div className="modal-header">
                            <h6 className="modal-title" id="modalEg1">Withdraw Money</h6>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div className="modal-body">
                            Are you sure you want to withdraw money?
                            </div>
                            <div className="modal-footer">
                            <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                            <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal">Withdraw</button>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
    )
}

export default Withdraw;