import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';
import Transaction from '../../../../Transactions/Transaction/Transaction';

function CustomerTransactions(props){
    var [transactions,setTransactions] = useState([]);
    var [error,setError] = useState(false);

    const customerID = sessionStorage.getItem('id');
    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        allTransactions();
    },[])

    async function allTransactions(){
        await axios.get('http://localhost:5224/api/Transactions/GetAllCustomerTransactions?customerID=' + props.account.customerID,httpHeader).then(function (response) {
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
        <div>
            {error ? 
                <div className="smallBox48">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterText">No Transaction History Found</div>
                </div> : 
                <div className="heigthBox scrolling">
                    {transactions.map(transaction =>
                        <Transaction key={transaction.transactionID} transaction={transaction}/> 
                    )}
                </div>
            }
        </div>
    );
}

export default CustomerTransactions;