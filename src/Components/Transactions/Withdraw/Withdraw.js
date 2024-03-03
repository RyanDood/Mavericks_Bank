import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function Withdraw(){

    var [accounts,setAccounts] = useState([]);
    var [accountID,setAccountID] = useState("");
    var [amount,setAmount] = useState("");
    var [description,setDescription] = useState("");
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    var newWithdrawal = {
        "amount": amount,
        "description": description,
        "accountID": accountID
    }

    useEffect(() => {
        const customerID = sessionStorage.getItem('id');
        getAllCustomerAccounts(customerID);
    },[])

    async function getAllCustomerAccounts(customerID){
        await axios.get('http://localhost:5224/api/Accounts/GetAllCustomerApprovedAccounts?customerID=' + customerID,httpHeader)
        .then(function (response) {
            console.log(response.data);
            setAccounts(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function withdrawMoney(){
        if(amount === "" || accountID === ""){
            alert("Please fill in all the fields");
        }
        else{
            if(amount > 0){
                if(description.length < 20){
                    await axios.post('http://localhost:5224/api/Transactions/Withdrawal',newWithdrawal,httpHeader).then(function (response) {
                        console.log(response.data);
                        setError(false);
                    })
                    .catch(function (error) {
                        console.log(error);
                        setError(true);
                        setErrorMessage(error.response.data);
                    })
                }
                else{
                    alert("Description is too long");
                }
            }
            else{
                alert("Please enter a valid amount to deposit");
            }
        }
        
    }

    function amountValidation(eventargs){
        var amount = eventargs.target.value;
        setAmount(amount);
        if(amount !== ""){
            if(amount > 0){
                setError(false);
                if(amount !== "" && accountID !== ""){
                    document.getElementById("withdraw").classList.remove("disabled");
                }
            }
            else{
                setError(true);
                setErrorMessage("Invalid Amount Entered");
                document.getElementById("withdraw").classList.add("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Enter an Amount");
            document.getElementById("withdraw").classList.add("disabled");
        }
    }

    function descriptionValidation(eventargs){
        var description = eventargs.target.value;
        setDescription(description);
        if(description.length < 35){
            setError(false);
            if(amount !== "" && accountID !== ""){
                document.getElementById("withdraw").classList.remove("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Description too long");
            document.getElementById("withdraw").classList.add("disabled");
        }
    }

    function sourceAccountValidation(eventargs){
        var accountID = eventargs.target.value;
        setAccountID(accountID);
        if(accountID !== ""){
            setError(false);
            if(amount !== "" && accountID !== ""){
                document.getElementById("withdraw").classList.remove("disabled");
            }
        }
        else{
            setError(true);
            setErrorMessage("Please Select an Account");
            document.getElementById("withdraw").classList.add("disabled");
        }
    }

    return(
        <div className="smallBox17 col-md-9">
            <div className="smallBox29">
                <ul className="smallBox22 nav">
                    <li className="nav-item highlight">
                        <Link className="nav-link textDecoWhite" to="/menu/customerTransactions">History</Link>
                    </li>
                    <li className="nav-item highlight">
                        <Link className="nav-link textDecoWhite" to="/menu/transferMoney">Transfer Money</Link>
                    </li>
                    <li className="nav-item highlight">
                        <Link className="nav-link textDecoWhite" to="/menu/depositMoney">Deposit</Link>
                    </li>
                    <li className="nav-item highlight">
                        <Link className="nav-link textDecoGreen" to="/menu/withdrawMoney">Withdraw</Link>
                    </li>
                </ul>
                <div className="smallBox30"> 
                    <div className='phoneMargin2'>
                        <span className="clickRegisterText">Amount</span>
                        <input className="form-control enterDiv2" type="number" onChange={amountValidation}></input>
                    </div>
                    <div>
                        <span className="clickRegisterText">Description</span>
                        <input className="form-control enterDiv2" type="text" onChange={descriptionValidation}></input>
                    </div>
                </div>
                <div className="smallBox47"> 
                    <div>
                        <span className="clickRegisterText">From (Account Number)</span>
                        <select className="form-control enterDiv2" value = {accountID} onChange={sourceAccountValidation}>
                            <option value="">Select</option>
                            {accounts.map(account => 
                                <option key={account.accountID} value={account.accountID}>{account.accountType} - {account.accountNumber}</option>
                            )}
                        </select>
                    </div>
                </div>
                {error ? <div className='flexRow margin6 errorText'>{errorMessage}</div> : null}
                <a id="withdraw" className="btn btn-outline-success smallBox31 disabled" href="" data-bs-toggle="modal" data-bs-target="#modal1">
                    <span>Withdraw</span>
                </a>
            </div>
            <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h6 className="modal-title" id="modalEg1">Withdraw Money</h6>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                            Are you sure you want to withdraw money?
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                            <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={withdrawMoney}>Withdraw</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Withdraw;