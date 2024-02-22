import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';
import CustomerDetails from './CustomerDetails/CustomerDetails';

function Profile(){

    var [profile,setProfile] = useState(
        {
            "name": "",
            "dob": "",
            "age": 0,
            "phoneNumber": 0,
            "address": "",
            "aadharNumber": 0,
            "panNumber": "",
            "gender": "",
            "email": "",
    })

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        getCustomerDetails();
    },[])

    async function getCustomerDetails(){
        await axios.get('http://localhost:5224/api/Customers/GetCustomer?customerID=8',httpHeader)
        .then(function (response) {
            console.log(response.data);
            covertDate(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    function covertDate(data){
        const date = new Date(data.dob);
        const formattedDate = date.toISOString().split('T')[0];
        console.log(formattedDate);
        var fetchedData = data;
        fetchedData.dob = formattedDate;
        setProfile(fetchedData);
    }

    return (
        <div className="smallBox17 col-sm-9">
            <div className="smallBox18">
                <div>
                    <span className="textDecoGreen">Personal Details</span> 
                </div>
                <CustomerDetails/>
                <a className="btn btn-outline-success smallBox9 margin1" href="" data-bs-toggle="modal" data-bs-target="#modal1">
                    <span>Update</span>
                </a>
            </div>
            <div className="modal fade" id="modal1" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                    <h6 className="modal-title" id="modalEg1">Update Details</h6>
                    <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body">
                    Are you sure you want to update your details?
                    </div>
                    <div className="modal-footer">
                    <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                    <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal">Update</button>
                    </div>
                </div>
                </div>
            </div>
        </div>
    );
}

export default Profile;