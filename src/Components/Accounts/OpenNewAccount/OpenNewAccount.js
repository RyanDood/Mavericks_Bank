import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link } from 'react-router-dom';

function OpenNewAccount(){

    var [accountType,setAccountType] = useState("")
    var [branchID, setBranchID] = useState("");
    var customerID = sessionStorage.getItem('id');
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

    var [profile,setProfile] = useState(
        {
            "name": "",
            "dob": "",
            "age": "",
            "phoneNumber": "",
            "address": "",
            "aadharNumber": 0,
            "panNumber": "",
            "gender": "",
            "email": "",
    })

    var updateCustomer = {
        "customerID": customerID,
        "name": profile.name,
        "dob": profile.dob,
        "age": profile.age,
        "phoneNumber": profile.phoneNumber,
        "address": profile.address,
        "aadharNumber": profile.aadharNumber,
        "panNumber": profile.panNumber,
        "gender": profile.gender
    }

    useEffect(() => {
        getCustomerDetails();
        getAllMavericksBranches();
    },[])

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function getCustomerDetails(){
        await axios.get('http://localhost:5224/api/Customers/GetCustomer?customerID=' + customerID,httpHeader)
        .then(function (response) {
            console.log(response.data);
            convertDate(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    function calculateAge(eventargs){
        var age = Math.floor((new Date() - new Date(eventargs.target.value).getTime()) / 3.15576e+10);
        setProfile({...profile,dob:eventargs.target.value,age:age});
    }

    function convertDate(data){
        const date = new Date(data.dob);
        const year = date.getFullYear();
        const month = (date.getMonth() + 1).toString().padStart(2, '0'); // Adding 1 because months are zero-indexed
        const day = date.getDate().toString().padStart(2, '0');
        const formattedDate =  year + "-" + month + "-" + day;
        console.log(formattedDate);
        data.dob = formattedDate;
        data.phoneNumber = data.phoneNumber.toString();
        setProfile(data);
    }

    async function createAccount() {
        if (updateCustomer.name === "" || updateCustomer.phoneNumber === "" || updateCustomer.address === "" || accountType === "" || branchID === "") {
            console.log("Please fill all fields");
        } 
        else {
            if (updateCustomer.name.length > 2 && updateCustomer.name.length < 100) {
                if (updateCustomer.age >= 18) {
                    if (updateCustomer.phoneNumber.length === 10) {
                        if (updateCustomer.address.length > 5  && updateCustomer.address.length < 100) {
                            await axios.put('http://localhost:5224/api/Customers/UpdateCustomerDetails', updateCustomer, httpHeader)
                            .then(function (response) {
                                console.log(response.data);
                            })
                            .catch(function (error) {
                                console.log(error);
                            })
                            await axios.post('http://localhost:5224/api/Accounts/AddAccount',newAccount,httpHeader).then(function (response) {
                                console.log(response.data);
                            })
                            .catch(function (error) {
                                console.log(error);
                            })
                        }
                        else{
                            console.log("Address should be between 6 and 100 characters long");
                        }
                    }
                    else{
                        console.log("Phone number should be 10 digits");
                    }
                }
                else {
                    console.log("Age should be greater than or equal to 18");
                }
            } 
            else{
                console.log("Name should be between 3 and 100 characters long");
            }
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
            <div className="smallBox59">
                <ul className="smallBox22 nav">
                    <li className="nav-item highlight smallBox23">
                        <Link className="nav-link textDecoWhite smallBox23" to="/menu/customerAccounts">All Accounts</Link>
                    </li>
                    <li className="nav-item highlight smallBox23">
                        <Link className="nav-link textDecoGreen smallBox23" to="/menu/openAccount">Open New Account</Link>
                    </li>
                </ul>
                <div className="scrolling">
                    <div className='phoneMargin2'>
                        <div className="smallBox19"> 
                            <div className="margin1">
                                <span className="clickRegisterText">Name</span>
                                <input className="form-control enterDiv2" type="text" value={profile.name} onChange={(eventargs) => setProfile({...profile,name:eventargs.target.value})}></input>
                            </div>
                            <div className="margin1">
                                <span className="clickRegisterText">Email</span>
                                <input className="form-control enterDiv2" type="email" value={profile.email} readOnly></input>
                            </div>
                        </div>
                        <div className="smallBox19">
                            <div className="margin1">
                                <span className="clickRegisterText">Date of Birth</span>
                                <input className="form-control enterDiv2" type="date" value={profile.dob} onChange={calculateAge}></input>
                            </div>
                            <div className="margin1">
                                <span className="clickRegisterText">Phone Number</span>
                                <input className="form-control enterDiv2" type="number" value={profile.phoneNumber} onChange={(eventargs) => setProfile({...profile,phoneNumber:eventargs.target.value})}></input>
                            </div>
                        </div>
                        <div className="smallBox19">
                            <div className="margin1">
                                <span className="clickRegisterText">Aadhaar Number</span>
                                <input className="form-control enterDiv2" type="number" value={profile.aadharNumber} readOnly></input>
                            </div>
                            <div className="margin1">
                                <span className="clickRegisterText">PAN Number</span>
                                <input className="form-control enterDiv2" type="text" value={profile.panNumber} readOnly></input>
                            </div>
                        </div>
                        <div className="smallBox19">
                            <div className="margin1">
                                <span className="clickRegisterText">Gender</span>
                                <select className="form-control enterDiv2" value={profile.gender} onChange={(eventargs) => setProfile({...profile,gender:eventargs.target.value})}>
                                    <option value="Male">Male</option>
                                    <option value="Female">Female</option>
                                    <option value="Others">Others</option>
                                </select>
                            </div>
                            <div className="margin1">
                                <span className="clickRegisterText">Address</span>
                                <input className="form-control enterDiv2" type="text" value={profile.address} onChange={(eventargs) => setProfile({...profile,address:eventargs.target.value})}></input>
                            </div>
                        </div>
                    </div>
                    <div className="smallBox25">
                        <div className='phoneMargin'>
                            <span className="clickRegisterText">Account Type</span>
                            <select className="form-control enterDiv2" value={accountType} onChange={(eventargs) => setAccountType(eventargs.target.value)}>
                                <option value="">Select</option>
                                <option value="Savings">Savings Account</option>
                                <option value="Current">Current Account</option>
                                <option value="Business">Business Account</option>
                                <option value="Salary">Salary Account</option>
                            </select>
                        </div>
                        <div>
                            <span className="clickRegisterText">Select Branch</span>
                            <select className="form-control enterDiv2" value={branchID} onChange={(eventargs) => setBranchID(eventargs.target.value)}>
                                <option value="">Select</option>
                                {branches.map(branch =>
                                    <option key={branch.branchID} value={branch.branchID}>{branch.branchName}</option>
                                )}
                            </select>
                        </div>
                    </div>
                </div>
                <a className="btn btn-outline-success smallBox42 phoneMargin" href="" data-bs-toggle="modal" data-bs-target="#modal1">
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