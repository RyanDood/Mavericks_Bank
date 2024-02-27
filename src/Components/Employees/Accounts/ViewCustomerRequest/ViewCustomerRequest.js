import axios from 'axios';
import { useEffect, useState } from "react";
import { useSelector } from 'react-redux';
import { Link, useNavigate } from 'react-router-dom';

function ViewCustomerRequest(){

    var accountID = useSelector((state) => state.accountID);
    var navigate = useNavigate();

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

    useEffect(() => {
        getAccount();
    },[])

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function getAccount(){
        if(accountID === 0){
            navigate("/employeeMenu/accounts/openRequests");
        }
        else{
            await axios.get('http://localhost:5224/api/Accounts/GetAccount?accountID=' + accountID,httpHeader)
            .then(function (response) {
                console.log(response.data);
                setAccount(response.data);
            })
            .catch(function (error) {
                console.log(error);
            })
        }
    }

    async function appproveCloseAccount(){
        await axios.put('http://localhost:5224/api/Accounts/UpdateAccountStatus?accountID=' + accountID + '&status=Close%20Account%20Request%20Approved',account,httpHeader)
        .then(function (response) {
            console.log(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function approveOpenAccount(){
        await axios.put('http://localhost:5224/api/Accounts/UpdateAccountStatus?accountID=' + accountID + '&status=Open%20Account%20Request%20Approved',account,httpHeader)
        .then(function (response) {
            console.log(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function disApproveOpenAccount(){
        await axios.put('http://localhost:5224/api/Accounts/UpdateAccountStatus?accountID=' + accountID + '&status=Open%20Account%20Request%20Disapproved',account,httpHeader)
        .then(function (response) {
            console.log(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    return(
        <div className="smallBox17 col-md-9">
            <div className="smallBox40">
                <div className="upMargin3">
                    {account.status === "Open Account Request Pending" ? 
                    <Link to="/employeeMenu/accounts/openRequests">
                        <div className="leftArrow change-my-color"></div>
                    </Link> :
                    <Link to="/employeeMenu/accounts/closeRequests">
                        <div className="leftArrow change-my-color"></div>
                    </Link>
                    }
                    
                    {account.status === "Open Account Request Pending" ? 
                    <div className='flexRow'>
                        <div className="smallBox53">
                            <span className="btn btn-outline-danger pointer" data-bs-toggle="modal" data-bs-target="#modal5">Disapprove</span>
                        </div>
                        <div className="smallBox53">
                            <span className="btn btn-outline-success pointer" data-bs-toggle="modal" data-bs-target="#modal4">Approve</span>
                        </div>
                    </div> :
                    <div className='flexRow'>
                        <div className="smallBox53">
                            <span className="btn btn-outline-danger pointer" data-bs-toggle="modal" data-bs-target="#modal3">Disapprove</span>
                        </div>
                        <div className="smallBox53">
                            <span className="btn btn-outline-success pointer" data-bs-toggle="modal" data-bs-target="#modal1">Approve</span>
                        </div>
                    </div>}
                </div>
                {account.status === "Open Account Request Pending" ?
                <span className="clickRegisterText12">Open Account</span>:
                <span className="clickRegisterText12">Close Account</span>}
                
                <span className="clickRegisterText11">Account Details</span>
                <span className="clickRegisterText7">Balance Remaining: {account.balance}</span>
                <span className="clickRegisterText7">Account No: {account.accountNumber} - {account.accountType} Account</span>
                <span className="clickRegisterText7">IFSC: {account.branches.ifscNumber}, {account.branches.branchName} - {account.branches.banks.bankName}</span>
                <hr className='hrS'></hr>
                <span className="clickRegisterText11">Customer Details</span>
                <div className='smallBox40 scrolling'>
                    <span className="clickRegisterText7">Name: {account.customers.name}</span>
                    <span className="clickRegisterText7">Email: {account.customers.email}</span>
                    <span className="clickRegisterText7">Date of Birth: {account.customers.dob}</span>
                    <span className="clickRegisterText7">Phone Number: {account.customers.phoneNumber}</span>
                    <span className="clickRegisterText7">Aadhaar Number: {account.customers.aadharNumber}</span>
                    <span className="clickRegisterText7">PAN Number: {account.customers.panNumber}</span>
                    <span className="clickRegisterText7">Address: {account.customers.address}</span>
                    <span className="clickRegisterText7">Gender: {account.customers.gender}</span>
                </div>
            </div>
            <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Close Account</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to close this Account?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={appproveCloseAccount}>Close</button>
                        </div>
                    </div>
                    </div>
                </div>
                <div className="modal fade" id="modal3" tabIndex="-1" aria-labelledby="modalEg2" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg2">Disapprove Close Request</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to DisApprove this Close Account Request?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={approveOpenAccount}>Disapprove</button>
                        </div>
                    </div>
                    </div>
                </div>
                <div className="modal fade" id="modal4" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Open Account</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to open this Account?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={approveOpenAccount}>Open</button>
                        </div>
                    </div>
                    </div>
                </div>
                <div className="modal fade" id="modal5" tabIndex="-1" aria-labelledby="modalEg2" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg2">Disapprove Open Request</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to DisApprove this Open Account Request?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={disApproveOpenAccount}>Disapprove</button>
                        </div>
                    </div>
                    </div>
                </div>
        </div>
    );
}

export default ViewCustomerRequest;