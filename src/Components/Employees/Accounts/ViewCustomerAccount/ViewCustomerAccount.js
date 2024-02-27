import { Link, useNavigate } from "react-router-dom";
import axios from 'axios';
import '../../../style.css';
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { updateAccountID } from "../../../../accountSlice";

function ViewCustomerAccount(){

    var accountID = 1;
    var [errorMessage,setErrorMessage] = useState("");
    var [account,setAccount] = useState({});

    useEffect(() => {
        getAccount();
    },[]);

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function getAccount(){
        await axios.get('http://localhost:5224/api/Accounts/GetAccount?accountID=' + accountID,httpHeader)
        .then(function (response) {
            console.log(response.data);
            setAccount(response.data);
        })
        .catch(function (error) {
            console.log(error);
            setErrorMessage(error.response.data);
        })
    }

    var dispatch = useDispatch();
    var navigate = useNavigate();

    function updateAccountId(){
        navigate("/employeeMenu/viewDetails");
        dispatch(
            updateAccountID(account.accountID)
        );
    }

    return (
        <div>
            <ul className="smallBox22 nav">
                <li className="nav-item highlight smallBox23">
                    <Link className="nav-link textDecoWhite smallBox23" to="/employeeMenu/accounts/openRequests">Open Account Requests</Link>
                </li>
                <li className="nav-item highlight smallBox23">
                    <Link className="nav-link textDecoWhite smallBox23" to="/employeeMenu/accounts/closeRequests">Close Account Requests</Link>
                </li>
                <li className="nav-item highlight smallBox23">
                    <Link className="nav-link textDecoGreen smallBox23" to="/employeeMenu/accounts/viewCustomerAccount">View Account</Link>
                </li>
            </ul>
            {errorMessage === "Account ID " + accountID + " not found" ?
                <div className="smallBox48">
                    <div className="errorImage2 change-my-color2"></div>
                    <div className="clickRegisterText">Account ID {accountID} not found</div>
                </div> : 
                <div className="heigthBox2">
                    <div className="scrolling">
                        <div className="whiteOutlineBox1">
                            <div className="whiteOutlineBoxMargin">
                                <span className="clickRegisterText">Account No: {account.accountNumber}</span>
                                <div className="smallBox23">
                                    <span className="clickRegisterText">Account type: {account.accountType}</span>
                                    <span className="pointer" onClick={updateAccountId}>
                                        <div className="rightArrow2 change-my-color"></div>
                                    </span>
                                </div>
                                <span className="clickRegisterText">Balance: {account.balance}</span>
                            </div>
                        </div>
                    </div>
                </div>}
        </div>
    );  
}

export default ViewCustomerAccount;