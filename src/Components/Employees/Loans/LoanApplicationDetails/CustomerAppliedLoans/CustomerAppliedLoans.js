import { useEffect, useState } from 'react';
import axios from 'axios';
import '../../../../style.css';

function CustomerAppliedLoans(props) {

    var [availedLoans,setAvailedLoans] = useState(
        [{
            "loanApplicationID": 0,
            "amount": 0,
            "appliedDate": "",
            "loans": {
                "loanType": "",
                "interest": "",
                "tenure": "",
            },
            "purpose": "",
            "status": "",
        }]
    );

    useEffect(() => {
        getCustomerAppliedLoans();
    }, []);

    const token = sessionStorage.getItem('token');
    const httpHeader = {
        headers: { 'Authorization': 'Bearer ' + token }
    };

    async function getCustomerAppliedLoans() {
        await axios.get('http://localhost:5224/api/AppliedLoans/GetAllCustomerAppliedLoans?customerID=' + props.loan.customerID, httpHeader)
            .then(function (response) {
                console.log(response.data);
                setAvailedLoans(response.data);
            })
            .catch(function (error) {
                console.log(error);
            })
    }

    return (
        <div className="scrolling">
                    {availedLoans.map((availedLoan) => 
                        <div key = {availedLoan.loanApplicationID} className="whiteOutlineBox4">
                            <div className="whiteOutlineBoxMargin">
                                <div className="smallBox23">
                                    <span className="clickRegisterText">Applied Loan Amount: {availedLoan.amount}</span>
                                    <span className="clickRegisterText4">{availedLoan.status}</span>
                                </div>
                                <span className="clickRegisterText">Interest: {availedLoan.loans.interest}</span>
                                <span className="clickRegisterText">Tenure: {availedLoan.loans.tenure} yrs</span>
                                <span className="clickRegisterText">{availedLoan.loans.loanType} - Applied on {availedLoan.appliedDate}</span>
                                <span className="clickRegisterText">Purpose - {availedLoan.purpose}</span>
                            </div>
                        </div>
                    )}
                </div>
    );
}

export default CustomerAppliedLoans;