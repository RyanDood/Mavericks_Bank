import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link } from 'react-router-dom';

function AvailedLoans(){
    var [availedLoans,setAvailedLoans] = useState([]);
    
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");

    const customerID = sessionStorage.getItem('id');
    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };
    
    useState(() => {
        allAvailedLoans();
    },[])

    async function allAvailedLoans(){
        await axios.get('http://localhost:5224/api/AppliedLoans/GetAllCustomerAvailedLoans?customerID=' + customerID,httpHeader).then(function (response) {
        console.log(response.data);
            setAvailedLoans(response.data);
            setError(false);
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
         })
    }

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
                </ul>
                {error ? 
                <div className="smallBox48">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterText">{errorMessage}</div>
                </div> :
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
                                <span className="clickRegisterText">Loan Type: {availedLoan.loans.loanType}</span>
                                <span className="clickRegisterText">Purpose: {availedLoan.purpose}</span>
                                <span className="clickRegisterText">Applied Date: {availedLoan.appliedDate}</span>
                            </div>
                        </div>
                    )}
                </div>}
            </div>
        </div>
    );
}

export default AvailedLoans;