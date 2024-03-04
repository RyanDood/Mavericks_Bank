import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../style.css';
import { Link, Outlet } from 'react-router-dom';
import { useSelector } from 'react-redux';
import Transaction from '../../../Transactions/Transaction/Transaction';

function LastMonthTransaction() {

    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");
    var accountID = useSelector((state) => state.accountID);
    var [transactions,setTransactions] = useState([]);

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        getLastMonthTransactions();
    },[])

    async function getLastMonthTransactions(){
        await axios.get('http://localhost:5224/api/Transactions/GetLastMonthAccountTransactions?accountID=' + accountID,httpHeader)
        .then(function (response) {
            console.log(response.data);
            setTransactions(response.data);
            setError(false);
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
        })
    }

    return (
        <div className="scrolling phoneBox2">
            {error ? 
                <div className="smallBox64">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterText">{errorMessage}</div>
                </div> : 
                <div>
                    {transactions.map(transaction => 
                        <Transaction key={transaction.transactionID} transaction = {transaction}/> 
                    )}
                </div>
            }
        </div> 
    );
} 

export default LastMonthTransaction;