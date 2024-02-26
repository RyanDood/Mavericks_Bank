import axios from 'axios';
import { useEffect, useState } from "react";
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { updateStatement } from '../../statementSlice';

function DashBoard() {

    var [accounts,setAccounts] = useState([]);
    var [accountID,setAccountID] = useState("");
    var [report,setReport] = useState([]);
    var [fromDate,setFromDate] = useState("");
    var [toDate,setToDate] = useState("");


    var statementData = {
        "accountID": accountID,
        "fromDate": fromDate,
        "toDate": toDate
    }

    var navigate = useNavigate(); 
    var dispatch = useDispatch();

    var customerID = sessionStorage.getItem('id');
    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        getAllCustomerAccounts();
        getCustomerReport();
    },[])

    async function navigateToAccountStatement(){
        if(accountID === "" || fromDate === "" || toDate === ""){
            console.log("Please fill in all the fields");
        }
        else{
            updateAccountStatement();
        }
    }

    function updateAccountStatement(){
        dispatch(
            updateStatement(statementData)
        );
        navigate("/menu/statement");
    }

    async function getAllCustomerAccounts(){
        await axios.get('http://localhost:5224/api/Accounts/GetAllCustomerApprovedAccounts?customerID=' + customerID,httpHeader)
        .then(function (response) {
            console.log(response.data);
            setAccounts(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function getCustomerReport(){
        await axios.get('http://localhost:5224/api/Transactions/CustomerRegulatoryReport?customerID=' + customerID,httpHeader)
        .then(function (response) {
            console.log(response.data);
            setReport(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox40">
                <div className="flexRow2">
                    <span className="clickRegisterText5">Hello Ryan,</span>
                </div>
                <hr className="hrS"></hr>
                <div className="flexRow2">
                    <div className="smallBox32">
                        <span className="clickRegisterText">Profile Strength</span>
                        <span className="clickRegisterText3">100%</span>
                    </div>
                    <div className="smallBox32">
                        <span className="clickRegisterText">Total Accounts</span>
                        <span className="clickRegisterText3">2</span>
                    </div>
                    <div className="smallBox32">
                        <span className="clickRegisterText">Active Loans</span>
                        <span className="clickRegisterText3">1</span>
                    </div>
                    <div className="smallBox32">
                        <span className="clickRegisterText">Beneficiaries</span>
                        <span className="clickRegisterText3">3</span>
                    </div>
                </div>
                <div className="flexRow2">
                    <div className="smallBox32">
                        <span className="clickRegisterText">Credit Status</span>
                        {report.creditWorthiness === "Yes" ? <span className="clickRegisterText3">Good</span> :
                        <span className="clickRegisterText3">Bad</span>}
                    </div>
                    <div className="smallBox32">
                        <span className="clickRegisterText">Inbounds</span>
                        <span className="clickRegisterText3">{report.inboundTransactions}</span>
                    </div>
                    <div className="smallBox32">
                        <span className="clickRegisterText">Outbounds</span>
                        <span className="clickRegisterText3">{report.outboundTransactions}</span>
                    </div>
                    <div className="smallBox32">
                        <span className="clickRegisterText">Ratio</span>
                        <span className="clickRegisterText3">{report.ratio}</span>
                    </div>
                </div>
                <hr className="hrS"></hr>
                <span className="clickRegisterText10">Generate Report</span>
                <div>
                    <div className="smallBox19">
                        <div className="margin1">
                            <span className="clickRegisterText">From</span>
                            <input className="form-control enterDiv2" type="date" onChange={(eventargs) => setFromDate(eventargs.target.value)}></input>
                        </div>
                        <div className="margin1">
                            <span className="clickRegisterText">To</span>
                            <input className="form-control enterDiv2" type="date" onChange={(eventargs) => setToDate(eventargs.target.value)}></input>
                        </div>
                    </div>
                    <div className="smallBox25">
                        <div>
                            <span className="clickRegisterText">Select Account</span>
                            <select className="form-control enterDiv2" value={accountID} onChange={(eventargs) => setAccountID(eventargs.target.value)}>
                                <option value="">Select</option>
                                {accounts.map(account =>
                                    <option key={account.accountID} value={account.accountID}>{account.accountType} - {account.accountNumber}</option>
                                )}
                            </select>
                        </div>
                        <span className="btn btn-outline-success buttonWidth pointer" onClick={navigateToAccountStatement}>
                            <span>Generate Report</span>
                        </span>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default DashBoard;