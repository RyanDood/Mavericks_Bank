import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function AllCustomerTransactions(){
    var [transactions,setTransactions] = useState(
        [{
            "transactionID": 0,
            "amount": 0,
            "transactionType": "",
        }]
    );

    var allTransactions = async() => await axios.get('http://localhost:5224/api/Transactions/GetAllCustomerTransactions?customerID=1').then(function (response) {
                                        console.log(response.data);
                                        setTransactions(response.data);
                                    })
                                    .catch(function (error) {
                                        console.log(error);
                                    })

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox20">
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            <a href="allTransaction.html" className="nav-link textDecoGreen smallBox23">History</a>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <a href="transferMoney.html" className="nav-link textDecoWhite smallBox23">Transfer Money</a>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <a href="depositMoney.html" className="nav-link textDecoWhite smallBox23">Deposit</a>
                        </li>
                        <button onClick = {allTransactions} className = 'btn btn-success'>Click</button>
                    </ul>
                    <div className="scrolling">
                        {transactions.map(transaction => 
                        <div  key = {transaction.transactionID} className="whiteOutlineBox2">
                            <div className="whiteOutlineBoxMargin">
                                <div className="smallBox23">
                                    <span className="clickRegisterText">{transaction.transactionType}</span>
                                    <div className="transactiondetails">
                                        <span className="clickRegisterText">Rs.{transaction.amount}</span>
                                        <a href="viewTransaction.html">
                                            <div className="rightArrow change-my-color"></div>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>)
                        }
                    </div>
                </div>
        </div>
    );
}

export default AllCustomerTransactions;