import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link } from 'react-router-dom';

function AddBeneficiary(){

    var [allBanks, setAllBanks] = useState([]);
    var [bankID, setBankID] = useState(1);
    var [allBranches, setAllBranches] = useState([]);
    var [branchID,setBranchID] = useState(1);

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        getAllBanks();
        getAllBranches(bankID);
    },[])

    async function getAllBanks(){
        await axios.get('http://localhost:5224/api/Banks/GetAllBanks',httpHeader)
        .then(function (response) {
            console.log(response.data);
            setAllBanks(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    function changeBank(eventargs){
        setBankID(eventargs.target.value);
        getAllBranches(eventargs.target.value);
    }

    async function getAllBranches(changedBankID){
        await axios.get('http://localhost:5224/api/Branches/GetAllSpecificBranches?bankID=' + changedBankID,httpHeader)
        .then(function (response) {
            console.log(response.data);
            setAllBranches(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

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
                        <select className="form-control enterDiv3" value = {bankID} onChange={changeBank}>
                            {allBanks.map(bank => 
                                <option key={bank.bankID} value={bank.bankID}>{bank.bankName}</option>
                            )}
                        </select>
                    </div>
                    <div>
                        <span className="clickRegisterText">Branch Name with IFSC</span>
                        <select className="form-control enterDiv3" value = {branchID} onChange={(eventargs) => setBranchID(eventargs.target.value)}>
                            {allBranches.map(branch => 
                                <option key={branch.branchID} value={branch.branchID}>{branch.branchName}</option>
                            )}
                        </select>
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