import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function Profile(){

    var [oldData,setOldData] = useState({})

    var [profile,setProfile] = useState(
        {
            "name": "",
            "dob": "",
            "age": "",
            "phoneNumber": "",
            "address": "",
            "aadharNumber": 0,
            "panNumber": "",
            "gender": "",
            "email": "",
    })

    var customerID = 8;

    var updateCustomer = {
        "customerID": customerID,
        "name": profile.name,
        "dob": profile.dob,
        "age": profile.age,
        "phoneNumber": profile.phoneNumber,
        "address": profile.address,
        "aadharNumber": profile.aadharNumber,
        "panNumber": profile.panNumber,
        "gender": profile.gender
    }

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
            convertDate(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    function calculateAge(eventargs){
        var age = Math.floor((new Date() - new Date(eventargs.target.value).getTime()) / 3.15576e+10);
        setProfile({...profile,dob:eventargs.target.value,age:age});
    }

    function convertDate(data){
        const date = new Date(data.dob);
        const year = date.getFullYear();
        const month = (date.getMonth() + 1).toString().padStart(2, '0'); // Adding 1 because months are zero-indexed
        const day = date.getDate().toString().padStart(2, '0');
        const formattedDate =  year + "-" + month + "-" + day;
        console.log(formattedDate);
        data.dob = formattedDate;
        data.phoneNumber = data.phoneNumber.toString();
        setProfile(data);
        setOldData(data);
    }

    async function updateCustomerDetails() {
        if (updateCustomer.name === "" || updateCustomer.phoneNumber === "" || updateCustomer.address === "") {
            console.log("Please fill all fields");
        } 
        else {
            if (areEqual(oldData, profile)) {
                console.log("No changes made");
            } 
            else {
                if (updateCustomer.name.length > 2 && updateCustomer.name.length < 100) {
                    if (updateCustomer.age >= 18) {
                        if (updateCustomer.phoneNumber.length === 10) {
                            if (updateCustomer.address.length > 5  && updateCustomer.address.length < 100) {
                                await axios.put('http://localhost:5224/api/Customers/UpdateCustomerDetails', updateCustomer, httpHeader)
                                .then(function (response) {
                                    console.log(response.data);
                                })
                                .catch(function (error) {
                                    console.log(error);
                                })
                            }
                            else{
                                console.log("Address should be between 6 and 100 characters long");
                            }
                        }
                        else{
                            console.log("Phone number should be 10 digits");
                        }
                    }
                    else {
                        console.log("Age should be greater than or equal to 18");
                    }
                } 
                else{
                    console.log("Name should be between 3 and 100 characters long");
                }
            } 
        }    
    }

    function areEqual(a, b) {
        return JSON.stringify(a) === JSON.stringify(b);
    }

    return (
        <div className="smallBox17 col-sm-9">
            <div className="smallBox18">
                <div>
                    <span className="textDecoGreen">Personal Details</span> 
                </div>
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
                            <input className="form-control enterDiv2" type="date" value={profile.dob} onChange={calculateAge}></input>
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
                            <input className="form-control enterDiv2" type="text" value={profile.address} onChange={(eventargs) => setProfile({...profile,address:eventargs.target.value})}></input>
                        </div>
                    </div>
                </div>
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
                            <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={updateCustomerDetails}>Update</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Profile;