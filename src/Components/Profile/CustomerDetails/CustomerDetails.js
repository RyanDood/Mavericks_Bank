import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';

function CustomerDetails() {

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
        <div>
            <div className="smallBox19"> 
                <div className="margin1">
                    <span className="clickRegisterText">Name</span>
                    <input className="form-control enterDiv2" type="text" value={profile.name} onChange={(eventargs) => setProfile({...profile,name:eventargs.target.value})}></input>
                </div>
                <div className="margin1">
                    <span className="clickRegisterText">Email</span>
                    <input className="form-control enterDiv2" type="email" value={profile.email} readOnly></input>
                </div>
            </div>
            <div className="smallBox19">
                <div className="margin1">
                    <span className="clickRegisterText">Date of Birth</span>
                    <input className="form-control enterDiv2" type="date" value={profile.dob} onChange={(eventargs) => setProfile({...profile,dob:eventargs.target.value})}></input>
                </div>
                <div className="margin1">
                    <span className="clickRegisterText">Phone Number</span>
                    <input className="form-control enterDiv2" type="number" value={profile.phoneNumber} onChange={(eventargs) => setProfile({...profile,phoneNumber:eventargs.target.value})}></input>
                </div>
            </div>
            <div className="smallBox19">
                <div className="margin1">
                    <span className="clickRegisterText">Aadhaar Number</span>
                    <input className="form-control enterDiv2" type="number" value={profile.aadharNumber} readOnly></input>
                </div>
                <div className="margin1">
                    <span className="clickRegisterText">PAN Number</span>
                    <input className="form-control enterDiv2" type="text" value={profile.panNumber} readOnly></input>
                </div>
            </div>
            <div className="smallBox19">
                <div className="margin1">
                    <span className="clickRegisterText">Gender</span>
                    <select className="form-control enterDiv2" value={profile.gender} onChange={(eventargs) => setProfile({...profile,gender:eventargs.target.value})}>
                        <option value="Male">Male</option>
                        <option value="Female">Female</option>
                        <option value="Others">Others</option>
                    </select>
                </div>
                <div className="margin1">
                    <span className="clickRegisterText">Address</span>
                    <input className="form-control enterDiv2" type="text" value={profile.address} onChange={(eventargs) => setProfile({...profile,gender:eventargs.target.value})}></input>
                </div>
            </div>
        </div>
    );
}

export default CustomerDetails;