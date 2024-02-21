import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link } from 'react-router-dom';

function ViewAccount(){

    var [account,setAccount] = useState(
        {
            "accountID": 0,
            "accountNumber": 0,
            "balance": 0,
            "accountType": "",
            "branches": {
              "ifscNumber": "",
              "branchName": "",
              "banks": {
                "bankName": ""
              }
            },
            "customers": {
              "name": "Ryan",
            }
          }
    );

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    var getAccount = async() => await axios.get('http://localhost:5224/api/Accounts/GetAccount?accountID=2',httpHeader)
                                .then(function (response) {
                                    console.log(response.data);
                                    setAccount(response.data);
                                })
                                .catch(function (error) {
                                    console.log(error);
                                })

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox21">
                    <div className="upMargin">
                        <Link to = "/menu/customerAccounts">
                            <div className="leftArrow change-my-color"></div>
                        </Link>
                        <div className="closeAccount">
                            <a href="" data-bs-toggle="modal" data-bs-target="#modal1">
                                <div className="delete change-my-color2"></div>
                            </a>
                            <span className="clickRegisterText">Close Account</span>
                        </div>
                        <button onClick = {getAccount} className = 'btn btn-success'>Click</button>
                    </div>
                    <span className="clickRegisterText">Account No: {account.accountNumber} - {account.accountType} Account</span>
                    <span className="clickRegisterText">Balance: {account.balance}</span>
                    <span className="clickRegisterText">IFSC: {account.branches.ifscNumber}, {account.branches.branchName} - {account.branches.banks.bankName}</span>
                    <hr></hr>
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            <a href="" className="nav-link textDecoGreen smallBox23">Recent Transactions</a>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <a href="" className="nav-link textDecoWhite smallBox23">Last Month</a>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <a href="" className="nav-link textDecoWhite smallBox23">From 2022 - 2023</a>
                        </li>
                    </ul>
                    <div className="whiteOutlineBox2">
                        <div className="whiteOutlineBoxMargin">
                            <div className="smallBox23">
                                <span className="clickRegisterText">Paid to Tharun Kumar</span>
                                <div className="transactiondetails">
                                    <span className="clickRegisterText">- 5000</span>
                                    <a href="viewTransaction.html">
                                        <div className="rightArrow change-my-color"></div>
                                    </a>
                                </div>
                            </div>
                        </div>
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

export default ViewAccount;