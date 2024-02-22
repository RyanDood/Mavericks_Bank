import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function TransferMoney(){

    var [allBanks, setAllBanks] = useState([]);
    var [bankID, setBankID] = useState(1);
    var [allBranches, setAllBranches] = useState([]);
    var [branchID,setBranchID] = useState(1);
    var [accounts,setAccounts] = useState([]);
    var [accountID,setAccountID] = useState(1);
    var [beneficiaries,setBeneficiaries] = useState([]);
    var [beneficiaryID,setBeneficiaryID] = useState(1);
    var [addBeneficiary,setAddBeneficiary] = useState(false);

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        getAllBanks();
        getAllBranches(bankID);
        getAllCustomerAccounts();
        getAllCustomerBeneficiaries();
    },[])

    async function getAllCustomerAccounts(){
        await axios.get('http://localhost:5224/api/Accounts/GetAllCustomerApprovedAccounts?customerID=1',httpHeader)
        .then(function (response) {
            console.log(response.data);
            setAccounts(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function getAllCustomerBeneficiaries(){
        await axios.get('http://localhost:5224/api/Beneficiaries/GetAllCustomerBeneficiaries?customerID=1',httpHeader).then(function (response) {
            console.log(response.data);
            setBeneficiaries(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })  
    }

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
                <div className="smallBox43">
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoWhite" to="/menu/customerTransactions">History</Link>
                        </li>
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoGreen" to="/menu/transferMoney">Transfer Money</Link>
                        </li>
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoWhite" to="/menu/depositMoney">Deposit</Link>
                        </li>
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoWhite" to="/menu/withdrawMoney">Withdraw</Link>
                        </li>
                    </ul>
                    <div className="smallBox19"> 
                        <div>
                            <span className="clickRegisterText">Amount</span>
                            <input className="form-control enterDiv2" type="number"></input>
                        </div>
                        <div>
                            <span className="clickRegisterText">Description</span>
                            <input className="form-control enterDiv2" type="text"></input>
                        </div>
                    </div>
                    {addBeneficiary ? <div className="smallBox19">
                        <div>
                            <span className="clickRegisterText">Account Holder Name</span>
                            <input className="form-control enterDiv2" type="text"></input>
                        </div>
                        <div>
                            <span className="clickRegisterText">Holder Account Number</span>
                            <input className="form-control enterDiv2" type="number"></input>
                        </div>
                    </div> : <div className="smallBox19">
                        <div>
                            <span className="clickRegisterText">From</span>
                            <select className="form-control enterDiv2" value = {accountID} onChange={(eventargs) => setAccountID(eventargs.target.value)}>
                                {accounts.map(account => 
                                    <option key={account.accountID} value={account.accountID}>{account.accountType} - {account.accountNumber}</option>
                                )}
                            </select>
                        </div>
                        <div>
                            <span className="clickRegisterText">To</span>
                            <select className="form-control enterDiv2" value = {beneficiaryID} onChange={(eventargs) => setBeneficiaryID(eventargs.target.value)}>
                                {beneficiaries.map(beneficiary => 
                                    <option key={beneficiary.beneficiaryID} value={beneficiary.beneficiaryID}>{beneficiary.name} - {beneficiary.accountNumber}</option>
                                )}
                            </select>
                        </div>
                    </div>}
                    {addBeneficiary ? <div className="smallBox19">
                        <div>
                            <span className="clickRegisterText">Bank Name</span>
                            <select className="form-control enterDiv2" value = {bankID} onChange={changeBank}>
                                {allBanks.map(bank => 
                                    <option key={bank.bankID} value={bank.bankID}>{bank.bankName}</option>
                                )}
                            </select>
                        </div>
                        <div>
                            <span className="clickRegisterText">Branch Name with IFSC</span>
                            <select className="form-control enterDiv2" value = {branchID} onChange={(eventargs) => setBranchID(eventargs.target.value)}>
                                {allBranches.map(branch => 
                                    <option key={branch.branchID} value={branch.branchID}>{branch.branchName}</option>
                                )}
                            </select>
                        </div>
                    </div> : null}
                    <div className='smallBox46'>
                        <a className="btn btn-outline-success smallBox44" href="" data-bs-toggle="modal" data-bs-target="#modal1">
                            <span>Transfer</span>
                        </a>
                        {addBeneficiary ? 
                        <a className="btn btn-outline-success smallBox45" onClick={() => setAddBeneficiary(false)}>
                            <span>Cancel</span>
                        </a> : 
                        <a className="btn btn-outline-success smallBox45" onClick={() => setAddBeneficiary(true)}>
                            <span>Add Beneficiary</span>
                        </a>}
                    </div>
                </div>
                <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Transfer Money</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to transfer money?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal">Transfer</button>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
    );
}

export default TransferMoney;