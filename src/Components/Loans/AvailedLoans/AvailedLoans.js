import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link } from 'react-router-dom';

function AvailedLoans(){
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

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    var allAvailedLoans = async() => await axios.get('http://localhost:5224/api/AppliedLoans/GetAllCustomerAvailedLoans?customerID=3',httpHeader).then(function (response) {
                                console.log(response.data);
                                setAvailedLoans(response.data);
                            })
                            .catch(function (error) {
                                console.log(error);
                            })

    return(
        <div className="smallBox17 col-md-9">
            <div className="smallBox27">
                <ul className="smallBox22 nav">
                    <li className="nav-item highlight smallBox23">
                        <Link className="nav-link textDecoWhite smallBox23" to="/menu/allLoans">All Loans</Link >
                    </li>
                    <li className="nav-item highlight smallBox23">
                        <Link className="nav-link textDecoGreen smallBox23" to="/menu/availedLoans">Availed Loans</Link >
                    </li>
                    <button onClick = {allAvailedLoans} className = 'btn btn-success'>Click</button>
                </ul>
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
            </div>
        </div>
    );
}

export default AvailedLoans;