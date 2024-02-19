import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function AllCustomerBeneficiaries(){

    var [beneficiaries,setBeneficiaries] = useState(
        [{
            "beneficiaryID": 0,
            "accountNumber": "",
            "name": "",
            "branches": {
                "ifscNumber": "",
                "banks": {
                    "bankName": ""
                }
            }
        }]
    );

    const navigate = useNavigate();
    
    var removeSession = () => {
        sessionStorage.removeItem("email");
        sessionStorage.removeItem("token");
        navigate("/");
    }

    var allBeneficiaries = async() => await axios.get('http://localhost:5224/api/Beneficiaries/GetAllCustomerBeneficiaries?customerID=1').then(function (response) {
                                        console.log(response.data);
                                        setBeneficiaries(response.data);
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
                            <div className="transaction change-my-color3"></div>
                            <li className="nav-item highlight">
                                <Link className="nav-link textDecoWhite" to="/customerTransactions">Transactions</Link>
                            </li>
                        </div>
                        <hr className="navBarLine"></hr>
                        <div className="flexRow3">
                            <div className="loan change-my-color3"></div>
                            <li className="nav-item highlight">
                                <Link className="nav-link textDecoWhite" to="/allLoans">Loans</Link>
                            </li>
                        </div>
                        <hr className="navBarLine"></hr>
                        <div className="flexRow3">
                            <div className="beneficiary change-my-color"></div>
                            <li className="nav-item highlight">
                                <Link className="nav-link textDecoGreen" to="/customerBeneficiaries">Beneficiaries</Link>
                                <button onClick = {allBeneficiaries} className = 'btn btn-success'>Click</button>
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
                                <a className="nav-link textDecoWhite" href="" data-bs-toggle="modal" data-bs-target="#modal2">Signout</a>
                            </li>
                        </div>
                    </ul>
                </div>
            </div>
            <div className="smallBox17 col-md-9">
                <div className="smallBox21">
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            <a href="allBeneficiary.html" className="nav-link textDecoGreen smallBox23">All Beneficiaries</a>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <a href="addBeneficiary.html" className="nav-link textDecoWhite smallBox23">Add Beneficiary</a>
                        </li>
                    </ul>
                    <div className="scrolling">
                        {beneficiaries.map(beneficiary =>
                        <div key = {beneficiary.beneficiaryID} className="whiteOutlineBox1">
                            <div className="whiteOutlineBoxMargin">
                                <div className="smallBox23">
                                    <span className="clickRegisterText">{beneficiary.name}</span>
                                    <a href="" data-bs-toggle="modal" data-bs-target="#modal1">
                                        <div className="delete change-my-color2"></div>
                                    </a>
                                </div>
                                <span className="clickRegisterText">{beneficiary.branches.banks.bankName} - Acc No {beneficiary.accountNumber}</span>
                                <span className="clickRegisterText">IFSC: {beneficiary.branches.ifscNumber}</span>
                            </div>
                        </div>
                        )}
                    </div>
                </div>
            </div>
        </div>
        <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                    <h6 className="modal-title" id="modalEg1">Delete Beneficiary</h6>
                    <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body">
                    Are you sure you want to delete this beneficiary?
                    </div>
                    <div className="modal-footer">
                    <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                    <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal">Delete</button>
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
                    <a className="btn btn-outline-success" onClick={removeSession}  data-bs-dismiss="modal">Sign Out</a>
                </div>
            </div>
            </div>
        </div>
        <div className="toast align-items-center text-white border-0 greenBackground topcorner" role="alert" aria-live="assertive" aria-atomic="true">
            <div className="d-flex">
            <div className="toast-body">
                Beneficiary Deleted Successfully
            </div>
            <button type="button" className="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
        </div>
    </div>
    )
}

export default AllCustomerBeneficiaries;