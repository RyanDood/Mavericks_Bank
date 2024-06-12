import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import Transaction from '../../Transactions/Transaction/Transaction';
import { Link, useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';
import jsPDF from 'jspdf';

function AccountStatement() {

    var [customer,setCustomer] = useState({});
    var[account,setAccount] = useState({});
    var [accountStatement, setAccountStatement] = useState({});
    var [transactions, setTransactions] = useState([]);
    var [error, setError] = useState(false);
    var [errorMessage, setErrorMessage] = useState("");
    var navigate = useNavigate();

    var customerID = sessionStorage.getItem('id');

    var statementData = useSelector((state) => state.statement);

    // const downloadPDF = () => {
    //     const capture = document.querySelector('.smallBox17');
    //     const but1 = document.querySelector("#pdf");
    //     capture.style.backgroundColor = 'rgb(67,65,65)';
    //     but1.style.display = 'none';

    //     html2canvas(capture).then((canvas) => {
    //         const imgData = canvas.toDataURL('img/png');
    //         const doc = new jsPDF('p', 'mm', 'a4');
    //         const componentWidth = doc.internal.pageSize.getWidth();
    //         const componentHeight = doc.internal.pageSize.getHeight();
    //         doc.addImage(imgData, 'PNG', 0, 0, componentWidth, componentHeight);
    //         doc.save('Account Statement.pdf');
    //         capture.style.backgroundColor = '';
    //         but1.style.display = '';
    //     })
    // }

    const token = sessionStorage.getItem('token');
    const httpHeader = {
        headers: { 'Authorization': 'Bearer ' + token }
    };

    useEffect(() => {
        transactionReport();
    }, [])

    async function transactionReport() {
        if (statementData.accountID === 0) {
            navigate("/menu/dashboard");
        }
        else {
            await axios.get('http://localhost:5224/api/Transactions/GetTransactionsBetweenTwoDates?accountID=' + statementData.accountID + '&fromDate=' + statementData.fromDate + '&toDate=' + statementData.toDate, httpHeader).then(function (response) {
                setError(false);
                setTransactions(response.data);
                getCustomer();
                getAccount();
                generateReport();
            })
                .catch(function (error) {
                    console.log(error);
                    setError(true);
                    setErrorMessage(error.response.data);
                })
        }
    }


    async function getCustomer() {
        await axios.get('http://localhost:5224/api/Customers/GetCustomer?customerID=' + customerID, httpHeader).then(function (response) {
            setCustomer(response.data);
        })
            .catch(function (error) {
                console.log(error);
            })
    }

    async function getAccount() {
        await axios.get('http://localhost:5224/api/Accounts/GetAccount?accountID=' + statementData.accountID, httpHeader).then(function (response) {
            setAccount(response.data);
        })
            .catch(function (error) {
                console.log(error);
            })
    }

    async function generateReport() {
        await axios.get('http://localhost:5224/api/Transactions/GetAccountStatement?accountID=' + statementData.accountID + '&fromDate=' + statementData.fromDate + '&toDate=' + statementData.toDate, httpHeader).then(function (response) {
            setAccountStatement(response.data);
        })
            .catch(function (error) {
                console.log(error);
            })
    }


    const downloadPDF = () => {
        const doc = new jsPDF();

        doc.setFontSize(18);
        doc.setTextColor(76, 175, 80);
        doc.text('mavericks', 14, 20);

        doc.setFontSize(14);
        doc.setTextColor(0, 0, 0);
        doc.text('Account Statement', 14, 40);

        doc.setFontSize(18);
        doc.text(`${customer.name}`, 14, 60);
        doc.setFontSize(12);
        doc.text(`Customer ID: ${customer.customerID}`, 14, 70);
        doc.text(`Address: ${customer.address}`, 14, 80);

        doc.setFontSize(18);
        doc.text(`${account.accountType} Account`, 14, 130);
        doc.setFontSize(12);
        doc.text('Currency: Indian', 14, 140);
        doc.text(`Account Number: ${account.accountNumber}`, 14, 150);
        doc.text(`IFSC Number: ${account.branches.ifscNumber}`, 14, 160);
        doc.text(`Branch: ${account.branches.branchName}`, 14, 170);

        doc.setFontSize(14);
        doc.setTextColor(76, 175, 80);
        doc.text(`Balance: ${accountStatement.balance}`, 14, 190);
        doc.text(`Deposits: ${accountStatement.totalDeposit}`, 14, 200);
        doc.text(`Withdrawals: ${accountStatement.totalWithdrawal} `, 14, 210);

        doc.setFontSize(14);
        doc.setTextColor(0, 0, 0);
        doc.line(14, 230, 196, 230);
        doc.text('Thank you for Banking with us', 14, 250);
       
        const img = new Image();
        img.src = 'https://dm0qx8t0i9gc9.cloudfront.net/watermarks/image/rDtN98Qoishumwih/barcode_GJUh5KF__SB_PM.jpg'; // Replace with the actual URL of the barcode image
        img.onload = () => {
            doc.addImage(img, 'PNG', 11, 90, 70, 20); // Adjust coordinates and size as needed
            doc.save(`${customer.name} - Account Statement.pdf`);
        };
    }

    return (
        <div className="smallBox17 col-md-9">
            {error ?
                <div>
                    <Link to="/menu/dashboard">
                        <div className="leftArrow change-my-color margin3"></div>
                    </Link>
                    <div className="smallBox66">
                        <div className="errorImage2 change-my-color2"></div>
                        <div className="clickRegisterText">{errorMessage}</div>
                    </div>
                </div>
                :
                <div>
                    <Link to="/menu/dashboard">
                        <div className="leftArrow change-my-color margin3"></div>
                    </Link>
                    <div className='smallBox40'>
                        <div className='smallBox67'>
                            <span className="clickRegisterText14">Account Statement</span>
                            <span id="pdf" className="btn btn-outline-success buttonWidth2 pointer" onClick={downloadPDF}>
                                <span>Download</span>
                            </span>
                        </div>
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
                        <div className="scrolling phoneBox">
                            {transactions.map(transaction =>
                                <Transaction key={transaction.transactionID} transaction={transaction} />
                            )
                            }
                        </div>
                    </div>
                </div>
            }
        </div>
    );
}

export default AccountStatement;