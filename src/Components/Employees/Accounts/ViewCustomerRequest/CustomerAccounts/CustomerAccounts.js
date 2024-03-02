import { useEffect, useState } from 'react';
import axios from 'axios';
import '../../../../style.css';

function CustomerAccounts(props) {

    var [accounts,setAccounts] = useState(
        [
            {
              "accountID": 0,
              "accountNumber": 0,
              "balance": 0,
              "accountType": "string",
              "status": "string",
              "branchID": 0,
              "branches": {
                "branchID": 0,
                "ifscNumber": "string",
                "branchName": "string",
                "bankID": 0,
                "banks": {
                  "bankID": 0,
                  "bankName": "string"
                }
              },
              "customerID": 0,
              "customers": {
                "customerID": 0,
                "name": "string",
                "dob": "2024-02-28T16:06:12.549Z",
                "age": 0,
                "phoneNumber": 0,
                "address": "string",
                "aadharNumber": 0,
                "panNumber": "string",
                "gender": "string",
                "email": "string",
                "validation": {
                  "email": "string",
                  "password": "string",
                  "userType": "string",
                  "key": "string"
                }
              }
            }
          ]
    );

    useEffect(() => {
        getCustomerAccounts();
    }, []);

    const token = sessionStorage.getItem('token');
    const httpHeader = {
        headers: { 'Authorization': 'Bearer ' + token }
    };

    async function getCustomerAccounts() {
        await axios.get('http://localhost:5224/api/Accounts/GetAllCustomerAccounts?customerID=' + props.account.customerID, httpHeader)
            .then(function (response) {
                console.log(response.data);
                setAccounts(response.data);
            })
            .catch(function (error) {
                console.log(error);
            })
    }

    return (
        <div className="scrolling phoneBox">
            {accounts.map(account => 
                <div key={account.accountID} className="whiteOutlineBox6">
                    <div className="whiteOutlineBoxMargin">
                        <span className="clickRegisterText">Account No: {account.accountNumber}</span>
                        <span className="clickRegisterText">Account type: {account.accountType}</span>
                        <span className="clickRegisterText">Branch Name: {account.branches.branchName}</span>
                        <span className="clickRegisterText13">Status: {account.status}</span>
                    </div>
                </div>
            )}
        </div>
    );
}

export default CustomerAccounts;