import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../style.css';
import { Link, Outlet } from 'react-router-dom';
import { useSelector } from 'react-redux';
import Transaction from '../../../Transactions/Transaction/Transaction';

function RecentTransaction(){

    var accountID = useSelector((state) => state.accountID);
    var [transactions,setTransactions] = useState(
        [{
            "transactionID": 0,
            "amount": 0,
            "transactionType": "",
        }]
    );

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        getRecentTransactions();
    },[])

    async function getRecentTransactions(){
        await axios.get('http://localhost:5224/api/Transactions/GetRecentTenAccountTransactions?accountID=' + accountID,httpHeader)
        .then(function (response) {
            console.log(response.data);
            setTransactions(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    return(
        <div className="scrolling">
            {transactions.map(transaction => 
                <Transaction key={transaction.transactionID} transaction = {transaction}/> 
            )}
        </div> 
    );
}

export default RecentTransaction;