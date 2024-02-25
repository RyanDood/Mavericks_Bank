import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';
import Transaction from '../Transaction/Transaction';

function AllCustomerTransactions(){

    var customerID = 1;
    var [transactions,setTransactions] = useState([]);
    var [error,setError] = useState(false);

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        allTransactions();
    },[])

    async function allTransactions(){
        await axios.get('http://localhost:5224/api/Transactions/GetAllCustomerTransactions?customerID=' + customerID,httpHeader).then(function (response) {
        console.log(response.data);
            setTransactions(response.data);
        })
        .catch(function (error) {
            console.log(error);
            if(error.response.data === "No Transaction History Found for Customer ID " + customerID){
                setError(true);
            }
        })
    }

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox43">
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoGreen smallBox23" to="/menu/customerTransactions">History</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoWhite smallBox23" to="/menu/transferMoney">Transfer Money</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoWhite smallBox23" to="/menu/depositMoney">Deposit</Link>
                        </li>
                        <li className="nav-item highlight">
                            <Link className="nav-link textDecoWhite" to="/menu/withdrawMoney">Withdraw</Link>
                        </li>
                    </ul>
                    {error ? 
                        <div className="smallBox48">
                            <div className="errorImage2 change-my-color2"></div>
                            <div className="clickRegisterText">No Transaction History Found</div>
                        </div> : 
                        <div className="scrolling">
                            {transactions.map(transaction =>
                            <Transaction key={transaction.transactionID} transaction = {transaction}/> 
                            )
                            }
                        </div>
                    }
                </div>
        </div>
    );
}

export default AllCustomerTransactions;