import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';
import Beneficiary from '../Beneficiary/Beneficiary';

function AllCustomerBeneficiaries(){

    var [beneficiaries,setBeneficiaries] = useState(
        [{
            "beneficiaryID": 0,
            "accountNumber": "",
            "name": "",
            "branches": {
                "ifscNumber": "",
                "banks": {
                    "bankName": ""
                }
            }
        }]
    );

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        const customerID = sessionStorage.getItem('id');
        allBeneficiaries(customerID);
    },[])

    async function allBeneficiaries(customerID){
        await axios.get('http://localhost:5224/api/Beneficiaries/GetAllCustomerBeneficiaries?customerID=' + customerID,httpHeader).then(function (response) {
            console.log(response.data);
            setBeneficiaries(response.data);
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
                            <Link className="nav-link textDecoGreen smallBox23" to="/menu/customerBeneficiaries">All Beneficiaries</Link>
                        </li>
                        <li className="nav-item highlight smallBox23">
                            <Link className="nav-link textDecoWhite smallBox23" to="/menu/addBeneficiary">Add Beneficiary</Link>
                        </li>
                    </ul>
                    <div className="scrolling">
                        {beneficiaries.map(beneficiary =>
                        <Beneficiary key = {beneficiary.beneficiaryID} beneficiary = {beneficiary}/>
                        )}
                    </div>
                </div>
        </div>
    )
}

export default AllCustomerBeneficiaries;