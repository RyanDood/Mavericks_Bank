import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function AllCustomerTransactions(){
    var [transactions,setTransactions] = useState(
        [{
            "transactionID": 0,
            "amount": 0,
            "transactionType": "",
        }]
    );

    const navigate = useNavigate();

    var removeSession = () => {
        sessionStorage.removeItem("email");
        sessionStorage.removeItem("token");
        navigate("/");
    }

    var allTransactions = async() => await axios.get('http://localhost:5224/api/Transactions/GetAllCustomerTransactions?customerID=1').then(function (response) {
                                        console.log(response.data);
                                        setTransactions(response.data);
                                    })
                                    .catch(function (error) {
                                        console.log(error);
                                    })

    return (
        <div className="container">
        <div className="row">
            <div className="smallBox15 col-md-3">
                <div className="smallBox34">
                    <div className="flexRow4">
                        <div className="logoImage change-my-color3"></div>
                        <span className="logo">mavericks</span>
                    </div>
                    <ul className="smallBox16 nav">
                        <div className="flexRow3">
                            <div className="dashboard change-my-color3"></div>
                            <li className="nav-item highlight">
                                <a className="nav-link textDecoWhite" href="dashboard.html">DashBoard</a>
                            </li>
                        </div>
                        <hr className="navBarLine"></hr>
                        <div className="flexRow3">
                            <div className="account change-my-color3"></div>
                            <li className="nav-item highlight">
                                <Link className="nav-link textDecoWhite" to="/customerAccounts">Accounts</Link>
                            </li>
                        </div>
                        <hr className="navBarLine"></hr>
                        <div className="flexRow3">
                            <div className="transaction change-my-color"></div>
                            <li className="nav-item highlight">
                                <Link className="nav-link textDecoGreen" to="/customerTransactions">Transactions</Link>
                                <button onClick = {allTransactions} className = 'btn btn-success'>Click</button>
                            </li>
                        </div>
                        <hr className="navBarLine"></hr>
                        <div className="flexRow3">
                            <div className="loan change-my-color3"></div>
                            <li className="nav-item highlight">
                                <Link  className="nav-link textDecoWhite" to="/allLoans">Loans</Link >
                            </li>
                        </div>
                        <hr className="navBarLine"></hr>
                        <div className="flexRow3">
                            <div className="beneficiary change-my-color3"></div>
                            <li className="nav-item highlight">
                                <Link className="nav-link textDecoWhite" to="/customerBeneficiaries">Beneficiaries</Link>
                            </li>
                        </div>
                        <hr className="navBarLine"></hr>
                        <div className="flexRow3">
                            <div className="profile change-my-color3"></div>
                            <li className="nav-item highlight">
                                <a className="nav-link textDecoWhite" href="profile.html">Profile</a>
                            </li>
                        </div>
                        <hr className="navBarLine"></hr>
                        <div className="flexRow3">
                            <div className="signout change-my-color3"></div>
                            <li className="nav-item highlight">
                                <a className="nav-link textDecoWhite" href="index.html" data-bs-toggle="modal" data-bs-target="#modal2">Signout</a>
                            </li>
                        </div>
                    </ul>
                </div>
            </div>
            <div className="smallBox17 col-md-9">
                <div className="smallBox20">
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            <a href="allTransaction.html" className="nav-link textDecoGreen smallBox23">History</a>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <a href="transferMoney.html" className="nav-link textDecoWhite smallBox23">Transfer Money</a>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <a href="depositMoney.html" className="nav-link textDecoWhite smallBox23">Deposit</a>
                        </li>
                    </ul>
                    <div className="scrolling">
                        {transactions.map(transaction => 
                        <div  key = {transaction.transactionID} className="whiteOutlineBox2">
                            <div className="whiteOutlineBoxMargin">
                                <div className="smallBox23">
                                    <span className="clickRegisterText">{transaction.transactionType}</span>
                                    <div className="transactiondetails">
                                        <span className="clickRegisterText">Rs.{transaction.amount}</span>
                                        <a href="viewTransaction.html">
                                            <div className="rightArrow change-my-color"></div>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>)
                        }
                    </div>
                </div>
            </div>
        </div>
        <div className="modal fade" id="modal2" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Sign Out</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="No"></button>
                    </div>
                    <div className="modal-body">
                        Are you sure you want to Sign Out?
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <a className="btn btn-outline-success" onClick={removeSession} data-bs-dismiss="modal">Sign Out</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    );
}

export default AllCustomerTransactions;