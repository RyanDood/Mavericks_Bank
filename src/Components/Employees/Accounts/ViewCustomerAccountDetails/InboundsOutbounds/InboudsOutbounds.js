import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../../style.css';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';


function InboudsOutbounds() {

    var accountID = useSelector((state) => state.accountID);
    var navigate = useNavigate();

    var [accountReport,setAccountReport] = useState(
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
        getAccountReport();
    },[])

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function getAccountReport(){
        if(accountID === 0){
            navigate("/employeeMenu/accounts/viewCustomerAccount");
        }
        else{
            await axios.get('http://localhost:5224/api/Transactions/AccountFinancialPerformanceReport?accountID=' + accountID,httpHeader)
            .then(function (response) {
                console.log(response.data);
                setAccountReport(response.data);
            })
            .catch(function (error) {
                console.log(error);
            })
        }
    }

    return (
        <div className="smallBox40 margin4 widthBox">
                <span className="clickRegisterText7">Total Transactions: {accountReport.totalTransactions}</span>
                <span className="clickRegisterText7">Inbound Transactions: {accountReport.inboundTransactions}</span>
                <span className="clickRegisterText7">Outbound Transactions: {accountReport.outboundTransactions}</span>
                <span className="clickRegisterText7">Ratio: {accountReport.ratio}</span>
                <span className="clickRegisterText7">CreditWorthiness: {accountReport.creditWorthiness}</span>
            </div>
    );
}

export default InboudsOutbounds;