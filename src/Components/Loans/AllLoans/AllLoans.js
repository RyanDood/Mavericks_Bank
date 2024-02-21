import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function AllLoans(){

    var [loans,setloans] = useState(
        [{
            "loanID": 0,
            "loanAmount": "",
            "loanType": "",
            "interest": "",
            "tenure": "",
        }]
    );

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    var allLoans = async() => await axios.get('http://localhost:5224/api/Loans/GetAllLoans',httpHeader)
                                .then(function (response) {
                                    setloans(response.data);
                                })
                                .catch(function (error) {
                                    console.log(error);
                                })
                     
    return(
        <div className="smallBox17 col-md-9 scrolling">
            <div className="smallBox26">
                <ul className="smallBox22 nav">
                    <li className="nav-item highlight smallBox23">
                        <Link className="nav-link textDecoGreen smallBox23" to="/menu/allLoans">All Loans</Link>
                    </li>
                    <li className="nav-item highlight smallBox23">
                        <Link className="nav-link textDecoWhite smallBox23" to="/menu/availedLoans">Availed Loans</Link>
                    </li>
                    <button onClick = {allLoans} className = 'btn btn-success'>Click</button>
                </ul>
                <div className="scrolling">
                    {loans.map(loan => 
                        <div key = {loan.loanID} className="whiteOutlineBox3">
                            <div className="whiteOutlineBoxMargin">
                                <span className="clickRegisterText">Loan Amount: {loan.loanAmount}</span>
                                <span className="clickRegisterText">Interest: {loan.interest}</span>
                                <span className="clickRegisterText">Tenure: {loan.tenure}</span>
                                <div className="smallBox23">
                                    <span className="clickRegisterText">Type: {loan.loanType}</span>
                                    <Link className="btn btn-outline-success smallBox9" to="/menu/applyLoan">
                                        <span>Apply</span>
                                    </Link>
                                </div>
                            </div>
                        </div>)
                    }
                </div>
            </div>
        </div>
    );
}

export default AllLoans;