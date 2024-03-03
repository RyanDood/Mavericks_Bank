import axios from 'axios';
import { useEffect, useState } from 'react';
import '../../style.css';
import { Link, Outlet, useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';

function ViewAccount(){

    var [clicked,setClicked] = useState([false,false,false]);
    var accountID = useSelector((state) => state.accountID);
    var navigate = useNavigate();
    
    var [account,setAccount] = useState(
        {
            "accountID": 0,
            "accountNumber": 0,
            "balance": 0,
            "accountType": "",
            "branches": {
              "ifscNumber": "",
              "branchName": "",
              "banks": {
                "bankName": ""
              }
            },
            "customers": {
              "name": "Ryan",
            }
          }
    );

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    
    useEffect(() => {
        getAccount();
    },[])

    async function getAccount(){
        if(accountID === 0){
            navigate("/menu/customerAccounts");
        }
        else{
            await axios.get('http://localhost:5224/api/Accounts/GetAccount?accountID=' + accountID,httpHeader)
            .then(function (response) {
                console.log(response.data);
                setAccount(response.data);
            })
            .catch(function (error) {
                console.log(error);
            })
        }
    }

    async function closeAccount(){
        await axios.put('http://localhost:5224/api/Accounts/CloseAccount?accountID=' + accountID,account,httpHeader)
        .then(function (response) {
            console.log(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    function navigateToRecentTransactions(){
        setClicked([true,false,false]);
        navigate("/menu/viewAccount/recentTransaction");
    }

    function navigateToLastMonthTransactions(){
        setClicked([false,true,false]);
        navigate("/menu/viewAccount/lastMonthTransaction");
    }

    function navigateToFilterTransactions(){
        setClicked([false,false,true]);
        navigate("/menu/viewAccount/filterTransaction");
    }

    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox40">
                    <div className="upMargin3">
                        <Link to = "/menu/customerAccounts">
                            <div className="leftArrow change-my-color"></div>
                        </Link>
                        <div className="closeAccount">
                            <a href="" data-bs-toggle="modal" data-bs-target="#modal1">
                                <div className="delete change-my-color2"></div>
                            </a>
                            <span className="clickRegisterText">Close Account</span>
                        </div>
                    </div>
                    <span className="clickRegisterText7">Balance Remaining: {account.balance}</span>
                    <span className="clickRegisterText7">Account No: {account.accountNumber} - {account.accountType} Account</span>
                    <span className="clickRegisterText7">IFSC: {account.branches.ifscNumber}, {account.branches.branchName} - {account.branches.banks.bankName}</span>
                    <hr className='hrS' ></hr>
                    <ul className="smallBox22 nav">
                        <li className="nav-item highlight smallBox23">
                            {clicked[0] ? <span className="nav-link textDecoGreen pointer smallBox23" onClick={navigateToRecentTransactions}>Recent Transactions</span> :
                            <span className="nav-link textDecoWhite pointer smallBox23" onClick={navigateToRecentTransactions}>Recent Transactions</span>}
                        </li>
                        <li className="nav-item highlight smallBox23">
                            {clicked[1] ? <span className="nav-link textDecoGreen pointer smallBox23" onClick={navigateToLastMonthTransactions}>Last Month</span> :
                            <span className="nav-link textDecoWhite pointer smallBox23" onClick={navigateToLastMonthTransactions}>Last Month</span>}
                        </li>
                        <li className="nav-item highlight smallBox23">
                            {clicked[2] ? <span className="nav-link textDecoGreen pointer smallBox23" onClick={navigateToFilterTransactions}>Filter Transactions</span> :
                            <span className="nav-link textDecoWhite pointer smallBox23" onClick={navigateToFilterTransactions}>Filter Transactions</span>}
                        </li>
                    </ul>
                    <Outlet/>
                </div>
                <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                    <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Close Account</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to close this Account?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={closeAccount}>Close</button>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
    );
}

export default ViewAccount;