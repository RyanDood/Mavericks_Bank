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

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    var allTransactions = async() => await axios.get('http://localhost:5224/api/Transactions/GetAllCustomerTransactions?customerID=1',httpHeader).then(function (response) {
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
                                        <Link to="/menu/viewTransaction">
                                            <div className="rightArrow change-my-color"></div>
                                        </Link>
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