import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function ViewTransaction(){

    var [transaction,setTransaction] = useState(
        {
            "transactionID": 0,
            "amount": 0,
            "transactionDate": "2024-02-21T08:58:09.162Z",
            "description": "string",
            "transactionType": "string",
            "accounts": {
              "branches": {
                "banks": {
                  "bankName": "string"
                }
              },
              "customers": {
                "name": "string",
              }
            },
            "beneficiaries": {
              "name": "string",
              "branches": {
                "banks": {
                  "bankName": "string"
                }
              },
            }
          }
    );

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        getTransaction();
    },[])

    async function getTransaction(){
        await axios.get('http://localhost:5224/api/Transactions/GetTransaction?transactionID=1',httpHeader).then(function (response) {
        console.log(response.data);
            setTransaction(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox26">
                    <div className="upMargin2">
                        <Link to="/menu/customerTransactions">
                            <div className="leftArrow change-my-color"></div>
                        </Link>
                    </div>
                    <span className="clickRegisterText8">{transaction.amount}</span>
                    <span className="clickRegisterText7">{transaction.description}</span>
                    <span className="clickRegisterText7">{transaction.transactionType} To {transaction.beneficiaries.name}</span>
                    <span className="clickRegisterText7">{transaction.beneficiaries.branches.banks.bankName}</span>
                    <hr className="hrS"></hr>
                    <span className="clickRegisterText7">From</span>
                    <span className="clickRegisterText7">{transaction.accounts.customers.name}</span>
                    <span className="clickRegisterText7">{transaction.accounts.branches.banks.bankName}</span>
                    <hr className="hrS"></hr>
                    <span className="clickRegisterText7">Paid at {transaction.transactionDate}</span>
                    <span className="clickRegisterText7">Transaction ID: {transaction.transactionID}</span>
                </div>
            </div>
    );
}

export default ViewTransaction;