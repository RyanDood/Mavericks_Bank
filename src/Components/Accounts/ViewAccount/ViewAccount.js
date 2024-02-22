import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import { Link, Outlet } from 'react-router-dom';

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

    useEffect(() => {
        getAccount();
    },[])

    async function getAccount(){
        await axios.get('http://localhost:5224/api/Accounts/GetAccount?accountID=2',httpHeader)
        .then(function (response) {
            console.log(response.data);
            setAccount(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox40">
                    <div className="upMargin3">
                        <Link to = "/menu/customerAccounts">
                            <div className="leftArrow change-my-color"></div>
                        </Link>
                        <div className="closeAccount">
                            <a href="" data-bs-toggle="modal" data-bs-target="#modal1">
                                <div className="delete change-my-color2"></div>
                            </a>
                            <span className="clickRegisterText">Close Account</span>
                        </div>
                    </div>
                    <span className="clickRegisterText7">Balance Remaining: {account.balance}</span>
                    <span className="clickRegisterText7">Account No: {account.accountNumber} - {account.accountType} Account</span>
                    <span className="clickRegisterText7">IFSC: {account.branches.ifscNumber}, {account.branches.branchName} - {account.branches.banks.bankName}</span>
                    <hr className='hrS' ></hr>
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoWhite smallBox23" to="/menu/viewAccount/recentTransaction">Recent Transactions</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoWhite smallBox23" to="/menu/viewAccount/lastMonthTransaction">Last Month</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoWhite smallBox23" to="/menu/viewAccount/filterTransaction" >From 2022 - 2023</Link>
                        </li>
                    </ul>
                    <Outlet/>
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