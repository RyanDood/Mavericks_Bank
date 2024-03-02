import axios from 'axios';
import { useEffect, useState } from "react";
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { updateStatement } from '../../statementSlice';

function DashBoard() {

    var [accounts,setAccounts] = useState([]);
    var [accountID,setAccountID] = useState("");
    var [report,setReport] = useState({});
    var [fromDate,setFromDate] = useState("");
    var [toDate,setToDate] = useState("");
    var [availedLoans,setAvailedLoans] = useState([]);
    var [beneficiaries,setBeneficiaries] = useState([]);
    var [customer,setCustomer] = useState({});

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
        getCustomer();
        getAllCustomerAccounts();
        allAvailedLoans();
        allBeneficiaries();
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

    async function allAvailedLoans(){
        await axios.get('http://localhost:5224/api/AppliedLoans/GetAllCustomerAvailedLoans?customerID=' + customerID,httpHeader).then(function (response) {
        console.log(response.data);
            setAvailedLoans(response.data);
        })
        .catch(function (error) {
            console.log(error);
         })
    }

    async function allBeneficiaries(){
        await axios.get('http://localhost:5224/api/Beneficiaries/GetAllCustomerBeneficiaries?customerID=' + customerID,httpHeader).then(function (response) {
            console.log(response.data);
            setBeneficiaries(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })  
    }

    async function getCustomer(){
        await axios.get('http://localhost:5224/api/Customers/GetCustomer?customerID=' + customerID,httpHeader).then(function (response) {
            console.log(response.data);
            setCustomer(response.data);
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
                    <span className="clickRegisterText5">Hello {customer.name},</span>
                </div>
                <hr className="hrS"></hr>
                <div className="flexRow6">
                    <div className="flexRow5">
                        <div className="smallBox32 width50">
                            <span className="clickRegisterText">Profile Strength</span>
                            <span className="clickRegisterText3">100%</span>
                        </div>
                        <div className="smallBox32 width50">
                            <span className="clickRegisterText">Total Accounts</span>
                            <span className="clickRegisterText3">{accounts.length}</span>
                        </div>
                    </div>
                    <div className="flexRow5">
                        <div className="smallBox32 width50">
                            <span className="clickRegisterText">Active Loans</span>
                            <span className="clickRegisterText3">{availedLoans.length}</span>
                        </div>
                        <div className="smallBox32 width50">
                            <span className="clickRegisterText">Beneficiaries</span>
                            <span className="clickRegisterText3">{beneficiaries.length}</span>
                        </div>
                    </div>
                </div>
                <div className="flexRow6">
                    <div className="flexRow5">
                        <div className="smallBox32 width50">
                            <span className="clickRegisterText">Credit Status</span>
                            {report.creditWorthiness === "Yes" ? <span className="clickRegisterText3">Good</span> :
                            <span className="clickRegisterText3">Bad</span>}
                        </div>
                        <div className="smallBox32 width50">
                            <span className="clickRegisterText">Inbounds</span>
                            <span className="clickRegisterText3">{report.inboundTransactions}</span>
                        </div>
                    </div>
                    <div className="flexRow5">
                        <div className="smallBox32 width50">
                            <span className="clickRegisterText">Outbounds</span>
                            <span className="clickRegisterText3">{report.outboundTransactions}</span>
                        </div>
                        <div className="smallBox32 width50">
                            <span className="clickRegisterText">Credit Score</span>
                            <span className="clickRegisterText3">{report.ratio}</span>
                        </div>
                    </div>
                </div>
                <hr className="hrS"></hr>
                <span className="clickRegisterText10">Generate Account Statement</span>
                <div className='phoneMargin2'>
                    <div className="smallBox19 phoneMargin2">
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
                        <div className='phoneMargin'>
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