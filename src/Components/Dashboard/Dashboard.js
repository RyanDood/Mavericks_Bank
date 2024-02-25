import axios from 'axios';
import { useEffect, useState } from "react";
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';

function DashBoard() {

    var [accounts,setAccounts] = useState([]);
    var [accountID,setAccountID] = useState("");

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        getAllCustomerAccounts();
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

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox21">
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
                        <span className="clickRegisterText">Benefiaries</span>
                        <span className="clickRegisterText3">3</span>
                    </div>
                </div>
                <div className="flexRow2">
                    <div className="smallBox32">
                        <span className="clickRegisterText">Credit Status</span>
                        <span className="clickRegisterText3">Good</span>
                    </div>
                    <div className="smallBox32">
                        <span className="clickRegisterText">Inbounds</span>
                        <span className="clickRegisterText3">1</span>
                    </div>
                    <div className="smallBox32">
                        <span className="clickRegisterText">Outbounds</span>
                        <span className="clickRegisterText3">1</span>
                    </div>
                    <div className="smallBox32">
                        <span className="clickRegisterText">Ratio</span>
                        <span className="clickRegisterText3">1</span>
                    </div>
                </div>
                <hr className="hrS"></hr>
                <span className="clickRegisterText10">Generate Report</span>
                <div>
                    <div className="smallBox19">
                        <div className="margin1">
                            <span className="clickRegisterText">From</span>
                            <input className="form-control enterDiv2" type="date"></input>
                        </div>
                        <div className="margin1">
                            <span className="clickRegisterText">To</span>
                            <input className="form-control enterDiv2" type="date"></input>
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
                        <a className="btn btn-outline-success buttonWidth" href="">
                            <span>Generate Report</span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default DashBoard;