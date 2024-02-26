import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';
import Account from '../Account/Account';

function AllCustomerAccounts(){

    var [accounts,setAccounts] = useState(
        [{
            "accountID": 0,
            "accountNumber": "",
            "accountType": "",
            "balance": "",
        }]
    );

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        const customerID = sessionStorage.getItem('id');
        allAccounts(customerID);
    },[]);

    async function allAccounts(customerID){
        await axios.get('http://localhost:5224/api/Accounts/GetAllCustomerApprovedAccounts?customerID=' + customerID,httpHeader)
        .then(function (response) {
            console.log(response.data);
            setAccounts(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox21">
                <ul className="smallBox22 nav">
                    <li className="nav-item highlight smallBox23">
                        <a className="nav-link textDecoGreen smallBox23" to="/menu/customerAccounts">All Accounts</a>
                    </li>
                    <li className="nav-item highlight smallBox23">
                        <Link className="nav-link textDecoWhite smallBox23" to="/menu/openAccount">Open New Account</Link>
                    </li>
                </ul>
                <div className="scrolling">
                    {accounts.map(account => 
                        <Account key = {account.accountID} account={account}/>
                    )}
                </div>
            </div>
            <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                        <h6 className="modal-title" id="modalEg1">Delete Account</h6>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                        Are you sure you want to delete this Account?
                        </div>
                        <div className="modal-footer">
                        <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal">Delete</button>
                        </div>
                    </div>
                </div>
            </div>
    </div>
    );
}

export default AllCustomerAccounts;