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

    var allBeneficiaries = async() => await axios.get('http://localhost:5224/api/Beneficiaries/GetAllCustomerBeneficiaries?customerID=1').then(function (response) {
                                        console.log(response.data);
                                        setBeneficiaries(response.data);
                                    })
                                    .catch(function (error) {
                                        console.log(error);
                                    })

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox21">
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            <a href="allBeneficiary.html" className="nav-link textDecoGreen smallBox23">All Beneficiaries</a>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <a href="addBeneficiary.html" className="nav-link textDecoWhite smallBox23">Add Beneficiary</a>
                        </li>
                        <button onClick = {allBeneficiaries} className = 'btn btn-success'>Click</button>
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