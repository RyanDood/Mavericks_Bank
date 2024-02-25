import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';

function ApplyLoan(){

    var loanID = useSelector((state) => state.loanID);
    var customerID = 3;
    var [amount,setAmount] = useState("");
    var [purpose,setPurpose] = useState("");

    var [loan,setloan] = useState(
        {
            "loanID": 0,
            "loanAmount": "",
            "loanType": "",
            "interest": "",
            "tenure": "",
        }
    );

    var newLoan = {
        "amount": amount,
        "purpose": purpose,
        "loanID": loanID,
        "customerID": customerID 
    }

    var navigate = useNavigate();

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        allLoans();
    },[])

    async function allLoans(){
        if(loanID === 0){
            navigate("/menu/allLoans")
        }
        else{
            await axios.get('http://localhost:5224/api/Loans/GetLoan?loanID=' + loanID,httpHeader)
            .then(function (response) {
                console.log(response.data);
                setloan(response.data);
            })
            .catch(function (error) {
                console.log(error);
            })
        }
    }

    async function applyForLoan(){
        if(amount === "" || purpose === ""){
            console.log("Please fill in all the details");
        }
        else{
            if(amount > 0){
                if(purpose.length > 3 &&purpose.length < 100){
                    await axios.post('http://localhost:5224/api/AppliedLoans/AddAppliedLoan', newLoan, httpHeader)
                    .then(function (response) {
                        console.log(response.data);
                    })
                    .catch(function (error) {
                        console.log(error);
                    })
                }
                else{
                    console.log("Purpose field should be between 4 and 100 characters long");
                }
            }
            else{
                console.log("Please enter a valid amount");
            }
        }
    }

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox28">
                    <div className="upMargin">
                        <Link to="/menu/allLoans">
                            <div className="leftArrow change-my-color"></div>
                        </Link >
                    </div>
                    <span className="clickRegisterText">Loan Amount: {loan.loanAmount}</span>
                    <span className="clickRegisterText">Interest: {loan.interest}</span>
                    <span className="clickRegisterText">Tenure: {loan.tenure}</span>
                    <span className="clickRegisterText">Loan Type: {loan.loanType}</span>
                    <div>
                        <span className="clickRegisterText">Your Amount</span>
                        <input className="form-control enterDiv3" type="number" onChange={(eventargs) => setAmount(eventargs.target.value)}></input>
                    </div>
                    <div>
                        <span className="clickRegisterText">Purpose</span>
                        <textarea className="form-control enterDiv4" type="text" onChange={(eventargs) => setPurpose(eventargs.target.value)}></textarea>
                    </div>
                    <a className="btn btn-outline-success smallBox9" href="applyLoan.html" data-bs-toggle="modal" data-bs-target="#modal1">
                        <span>Apply</span>
                    </a>
                </div>
                <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Apply Loan</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to apply for this Loan?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={applyForLoan}>Apply</button>
                        </div>
                    </div>
                    </div>
                </div>
        </div>
    );
}

export default ApplyLoan;