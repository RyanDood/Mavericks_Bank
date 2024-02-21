import axios from 'axios';
import { useState } from 'react';
import { Link, useNavigate } from "react-router-dom";
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';

function AllCustomerAccounts(){

    var [accounts,setAccounts] = useState(
        [{
            "accountID": 0,
            "accountNumber": "",
            "accountType": "",
            "balance": "",
        }]
    );

    var allAccounts = async() => await axios.get('http://localhost:5224/api/Accounts/GetAllCustomerApprovedAccounts?customerID=1')
                                .then(function (response) {
                                    console.log(response.data);
                                    setAccounts(response.data);
                                })
                                .catch(function (error) {
                                    console.log(error);
                                })

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox21">
                <ul className="smallBox22 nav">
                    <li className="nav-item highlight smallBox23">
                        <a href="allAccount.html" className="nav-link textDecoGreen smallBox23">All Accounts</a>
                    </li>
                    <li className="nav-item highlight smallBox23">
                        <a href="addAccount.html" className="nav-link textDecoWhite smallBox23">Open New Account</a>
                    </li>
                    <button onClick = {allAccounts} className = 'btn btn-success'>Click</button>
                </ul>
                <div className="scrolling">
                    {accounts.map(account => 
                        <div key = {account.accountID} className="whiteOutlineBox1">
                            <div className="whiteOutlineBoxMargin">
                                <div className="smallBox23">
                                    <span className="clickRegisterText">Account No: {account.accountNumber}</span>
                                    <a href="" data-bs-toggle="modal" data-bs-target="#modal1">
                                        <div className="delete change-my-color2"></div>
                                    </a>
                                </div>
                                <span className="clickRegisterText">Account type: {account.accountType}</span>
                                <div className="smallBox23">
                                    <span className="clickRegisterText">Balance: {account.balance}</span>
                                    <a href="viewAccount.html">
                                        <div className="rightArrow change-my-color"></div>
                                    </a>
                                </div>
                            </div>
                        </div>
                    )}
                </div>
            </div>
            <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Delete Account</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to delete this Account?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal">Delete</button>
                        </div>
                    </div>
                </div>
            </div>
    </div>
    );
}

export default AllCustomerAccounts;