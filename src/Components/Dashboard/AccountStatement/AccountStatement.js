import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import Transaction from '../../Transactions/Transaction/Transaction';
import { Link, useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';

function AccountStatement() {

    var [accountStatement,setAccountStatement] = useState({});
    var [transactions,setTransactions] = useState([]);
    var navigate = useNavigate();

    var statementData = useSelector((state) => state.statement);

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        generateReport();
    },[])

    async function generateReport(){
        if(statementData.accountID=== 0){
            navigate("/menu/dashboard");
        }
        else{
            await axios.get('http://localhost:5224/api/Transactions/GetAccountStatement?accountID=' + statementData.accountID +'&fromDate=' + statementData.fromDate +'&toDate=' + statementData.toDate,httpHeader).then(function (response) {
                console.log(response.data);
                setAccountStatement(response.data);
            })
            .catch(function (error) {
                console.log(error);
            })
            transactionReport();
        }
    }

    async function transactionReport(){
        await axios.get('http://localhost:5224/api/Transactions/GetTransactionsBetweenTwoDates?accountID=' + statementData.accountID +'&fromDate=' + statementData.fromDate +'&toDate=' + statementData.toDate,httpHeader).then(function (response) {
            console.log(response.data);
            setTransactions(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox40">
                <Link to = "/menu/dashboard">
                    <div className="leftArrow change-my-color margin3"></div>
                </Link>
                <span className="clickRegisterText10">Account Statement</span>
                <div className="flexRow2">
                    <div className="smallBox32">
                        <span className="clickRegisterText">Balance</span>
                        <span className="clickRegisterText3">{accountStatement.balance}</span>
                    </div>
                    <div className="smallBox32">
                        <span className="clickRegisterText">Deposits</span>
                        <span className="clickRegisterText3">{accountStatement.totalDeposit}</span>
                    </div>
                    <div className="smallBox32">
                        <span className="clickRegisterText">Withdrawal</span>
                        <span className="clickRegisterText3">{accountStatement.totalWithdrawal}</span>
                    </div>
                </div>
                <hr className="hrS"></hr>
                <span className="clickRegisterText10">Total Transactions</span>
                <div className="scrolling">
                            {transactions.map(transaction =>
                            <Transaction key={transaction.transactionID} transaction = {transaction}/> 
                            )
                            }
                        </div>
            </div>
        </div>
    );
}

export default AccountStatement;