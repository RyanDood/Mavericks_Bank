import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../../style.css';
import { Link, Outlet } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import Transaction from '../../../../Transactions/Transaction/Transaction';
import { updateDate } from '../../../../../dateSlice';

function AccountTransaction(){

    var dispatch = useDispatch();

    var accountID = useSelector((state) => state.accountID);
    var date = useSelector((state) => state.date);
    var [fromDate,setFromDate] = useState(date.fromDate);
    var [toDate,setToDate] = useState(date.toDate);
    var [transactions,setTransactions] = useState(
        [{
            "transactionID": 0,
            "amount": 0,
            "transactionType": "",
        }]
    );
    var [getTransactions,setGetTransactions] = useState(false);

    useEffect(() => {
        if(fromDate !== "" && toDate !== ""){
            getTransactionsBetweenTwoDates();
        }
    },[])

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function getTransactionsBetweenTwoDates(){
        dispatch(
            updateDate({
                "fromDate": fromDate,
                "toDate": toDate
            })
        )
        await axios.get('http://localhost:5224/api/Transactions/GetTransactionsBetweenTwoDates?accountID=' + accountID +'&fromDate=' + fromDate +'&toDate=' + toDate,httpHeader)
        .then(function (response) {
            setGetTransactions(true);
            console.log(response.data);
            setTransactions(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    function getFilter(){
        setGetTransactions(false);
        setFromDate("");
        setToDate("");
        dispatch(
            updateDate({
                "fromDate": "",
                "toDate": ""
            })
        )
    }

    return (
        <div className="heigthBox">
            {getTransactions === true ?
            <div className="scrolling">
                <span className="btn btn-outline-success pointer margin5" onClick={getFilter}>
                    <span>Filter Transaction</span>
                </span>
                {transactions.map(transaction => 
                    <Transaction key={transaction.transactionID} transaction = {transaction}/>
                )}
            </div>
             :
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
                    <span className="btn btn-outline-success pointer" onClick={getTransactionsBetweenTwoDates}>
                        <span>Get Transactions</span>
                    </span>
                </div>
            </div>}
        </div>
    );
}

export default AccountTransaction;