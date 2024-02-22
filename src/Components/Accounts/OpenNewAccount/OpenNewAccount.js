import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link } from 'react-router-dom';
import CustomerDetails from '../../Profile/CustomerDetails/CustomerDetails';

function OpenNewAccount(){

    var [accountType,setAccountType] = useState("")
    var [branchID, setBranchID] = useState("");
    var customerID = 8;
    var [branches,setBranches] = useState(
        [{
        "branchID": 0,
        "ifscNumber": "string",
        "branchName": "string",
        "bankID": 0,
        "banks": {
          "bankID": 0,
          "bankName": "string"
        }
      }]
    );
    
    var newAccount = {
        "accountType": accountType,
        "branchID": branchID,
        "customerID": customerID
    }

    useEffect(() => {
        getAllMavericksBranches();
    },[])

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function createAccount(){
        if(accountType === "" || branchID === ""){
            console.log("Please fill all fields");
        }
        else{
            await axios.post('http://localhost:5224/api/Accounts/AddAccount',newAccount,httpHeader).then(function (response) {
                console.log(response.data);
            })
            .catch(function (error) {
                console.log(error);
            })
        }
    }

    async function getAllMavericksBranches(){
        await axios.get('http://localhost:5224/api/Branches/GetAllSpecificBranches?bankID=2',httpHeader)
        .then(function (response) {
            console.log(response.data);
            setBranches(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox40">
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoWhite smallBox23" to="/menu/customerAccounts">All Accounts</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoGreen smallBox23" to="/menu/openAccount">Open New Account</Link>
                        </li>
                    </ul>
                    <div className="scrolling">
                        <CustomerDetails/>
                        <div className="smallBox25">
                            <div>
                                <span className="clickRegisterText">Account Type</span>
                                <select className="form-control enterDiv2" value = {accountType} onChange={(eventargs) => setAccountType(eventargs.target.value)}>
                                    <option value="">Select</option>
                                    <option value="Savings">Savings Account</option>
                                    <option value="Current">Current Account</option>
                                    <option value="Business">Business Account</option>
                                    <option value="Salary">Salary Account</option>
                                </select>
                            </div>
                            <div>
                                <span className="clickRegisterText">Select Branch</span>
                                <select className="form-control enterDiv2" value = {branchID} onChange={(eventargs) => setBranchID(eventargs.target.value)}>
                                    <option value="">Select</option>
                                    {branches.map(branch =>
                                        <option key={branch.branchID} value={branch.branchID}>{branch.branchName}</option>
                                    )}
                                </select>
                            </div>
                        </div>
                    </div>
                    <a className="btn btn-outline-success smallBox42" href="" data-bs-toggle="modal" data-bs-target="#modal1">
                        <span>Create Account</span>
                    </a>
                </div>
                <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Create Account</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to create account?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={createAccount}>Create</button>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
    );
}

export default OpenNewAccount;