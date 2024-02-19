import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function AllLoans(){

    var [loans,setloans] = useState(
        [{
            "loanID": 0,
            "loanAmount": "",
            "loanType": "",
            "interest": "",
            "tenure": "",
        }]
    );

    const navigate = useNavigate();

    var removeSession = () => {
        sessionStorage.removeItem("email");
        sessionStorage.removeItem("token");
        navigate("/");
    }

    var allLoans = async() => await axios.get('http://localhost:5224/api/Loans/GetAllLoans')
                                .then(function (response) {
                                    setloans(response.data);
                                })
                                .catch(function (error) {
                                    console.log(error);
                                })
                     
    return(
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
                                <div className="transaction change-my-color3"></div>
                                <li className="nav-item highlight">
                                    <Link className="nav-link textDecoWhite" to="/customerTransactions">Transactions</Link>
                                </li>
                            </div>
                            <hr className="navBarLine"></hr>
                            <div className="flexRow3">
                                <div className="loan change-my-color"></div>
                                <li className="nav-item highlight">
                                    <Link className="nav-link textDecoGreen" to="/allLoans">Loans</Link>
                                    <button onClick = {allLoans} className = 'btn btn-success'>Click</button>
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
                <div className="smallBox17 col-md-9 scrolling">
                    <div className="smallBox26">
                        <ul className="smallBox22 nav">
                            <li className="nav-item highlight smallBox23">
                                <a href="allLoan.html" className="nav-link textDecoGreen smallBox23">All Loans</a>
                            </li>
                            <li className="nav-item highlight smallBox23">
                                <a href="availedLoan.html" className="nav-link textDecoWhite smallBox23">Availed Loans</a>
                            </li>
                        </ul>
                        <div className="scrolling">
                            {loans.map(loan => 
                                <div key = {loan.loanID} className="whiteOutlineBox3">
                                    <div className="whiteOutlineBoxMargin">
                                        <span className="clickRegisterText">Loan Amount: {loan.loanAmount}</span>
                                        <span className="clickRegisterText">Interest: {loan.interest}</span>
                                        <span className="clickRegisterText">Tenure: {loan.tenure}</span>
                                        <div className="smallBox23">
                                            <span className="clickRegisterText">Type: {loan.loanType}</span>
                                            <a className="btn btn-outline-success smallBox9" href="applyLoan.html">
                                                <span>Apply</span>
                                            </a>
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

export default AllLoans;