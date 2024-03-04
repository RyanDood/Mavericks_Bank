import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';
import Beneficiary from '../Beneficiary/Beneficiary';

function AllCustomerBeneficiaries(){

    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");
    var [beneficiaries,setBeneficiaries] = useState([]);

    const customerID = sessionStorage.getItem('id');
    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        allBeneficiaries();
    },[])

    async function allBeneficiaries(){
        await axios.get('http://localhost:5224/api/Beneficiaries/GetAllCustomerBeneficiaries?customerID=' + customerID,httpHeader).then(function (response) {
            console.log(response.data);
            setBeneficiaries(response.data);
            setError(false);
        })
        .catch(function (error) {
            console.log(error);
            setError(true);
            setErrorMessage(error.response.data);
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
                    {error ? 
                    <div className="smallBox48">
                        <div className="errorImage2 change-my-color2"></div>
                        <div className="clickRegisterText">{errorMessage}</div>
                    </div> :
                    <div className="scrolling">
                        {beneficiaries.map(beneficiary =>
                        <Beneficiary key = {beneficiary.beneficiaryID} beneficiary = {beneficiary}/>
                        )}
                    </div>}
                </div>
        </div>
    )
}

export default AllCustomerBeneficiaries;