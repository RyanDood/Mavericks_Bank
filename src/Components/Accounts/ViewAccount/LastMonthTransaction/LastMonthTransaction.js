import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../../style.css';
import { Link, Outlet } from 'react-router-dom';

function LastMonthTransaction() {

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
        getLastMonthTransactions();
    },[])

    async function getLastMonthTransactions(){
        await axios.get('http://localhost:5224/api/Transactions/GetLastMonthAccountTransactions?accountID=1',httpHeader)
        .then(function (response) {
            console.log(response.data);
            setTransactions(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    return (
        <div className="scrolling">
            {transactions.map(transaction => 
                <div key={transaction.transactionID} className="whiteOutlineBox2">
                    <div className="whiteOutlineBoxMargin">
                        <div className="smallBox23">
                            <span className="clickRegisterText">{transaction.transactionType}</span>
                            <div className="transactiondetails">
                                <span className="clickRegisterText">Rs.{transaction.amount}</span>
                                <Link to="/menu/viewTransaction">
                                    <div className="rightArrow change-my-color"></div>
                                </Link>
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </div> 
    );
} 

export default LastMonthTransaction;