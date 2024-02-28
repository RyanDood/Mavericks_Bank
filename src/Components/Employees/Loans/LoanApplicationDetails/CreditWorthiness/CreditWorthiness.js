import { useEffect, useState } from 'react';
import axios from 'axios';
import '../../../../style.css';

function CreditWorthiness(props) {

    var [report, setReport] = useState({
        "totalTransactions": 0,
        "inboundTransactions": 0,
        "outboundTransactions": 0,
        "ratio": 0,
        "creditWorthiness": "string"
    });

    useEffect(() => {
        getCustomerReport();
    }, []);

    const token = sessionStorage.getItem('token');
    const httpHeader = {
        headers: { 'Authorization': 'Bearer ' + token }
    };

    async function getCustomerReport() {
        await axios.get('http://localhost:5224/api/Transactions/CustomerRegulatoryReport?customerID=' + props.loan.customerID, httpHeader)
            .then(function (response) {
                console.log(response.data);
                setReport(response.data);
            })
            .catch(function (error) {
                console.log(error);
            })
    }

    return (
        <div className="smallBox40 scrolling margin4">
            <span className="clickRegisterText7">Total Transactions: {report.totalTransactions}</span>
            <span className="clickRegisterText7">Inbound Transactions: {report.inboundTransactions}</span>
            <span className="clickRegisterText7">Outbound Transactions: {report.outboundTransactions}</span>
            <span className="clickRegisterText7">Ratio: {report.ratio}</span>
            <span className="clickRegisterText7">CreditWorthiness: {report.creditWorthiness}</span>
        </div>
    );
}

export default CreditWorthiness;