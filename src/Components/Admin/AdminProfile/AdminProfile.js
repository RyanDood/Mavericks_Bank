import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function AdminProfile(){

    var [oldData,setOldData] = useState({})
    var [error,setError]= useState(false);
    var [errorMessage,setErrorMessage]= useState("");

    var [profile,setProfile] = useState(
        {
            "name": "string",
            "email": "string",
    })

    var adminID = sessionStorage.getItem('id');

    var updateAdmin = {
        "adminID": adminID,
        "name": profile.name
      }

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    useEffect(() => {
        getAdminDetails();
    },[])

    async function getAdminDetails(){
        await axios.get('http://localhost:5224/api/Admin/GetAdmin?adminID=' + adminID,httpHeader)
        .then(function (response) {
            console.log(response.data);
            setProfile(response.data);
            setOldData(response.data);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    async function updateEmployeeDetails() {
        if (updateAdmin.name === "") {
            alert("Please fill all fields");
        } 
        else {
            if (areEqual(oldData, profile)) {
                alert("No changes made");
            } 
            else {
                if (updateAdmin.name.length > 2 && updateAdmin.name.length < 100) {
                    await axios.put('http://localhost:5224/api/Admin/UpdateAdminName', updateAdmin, httpHeader)
                    .then(function (response) {
                        console.log(response.data);
                    })
                    .catch(function (error) {
                        console.log(error);
                    })
                } 
                else{
                    alert("Name should be between 3 and 100 characters long");
                }
            } 
        }    
    }

    function areEqual(a, b) {
        return JSON.stringify(a) === JSON.stringify(b);
    }

    function nameValidation(eventargs){
        var name = eventargs.target.value;
        setProfile({...profile,name:name});
        if(name !== ""){
            if(name.length > 2 && name.length < 100){
                setError(false);
                document.getElementById("update").classList.remove("disabled");
            }
            else{
                document.getElementById("update").classList.add("disabled");
                setError(true);
                setErrorMessage("Name should be between 2 and 100 characters long");
            }
        }
        else{
            document.getElementById("update").classList.add("disabled");
            setError(true);
            setErrorMessage("Name cannot be empty");
        }
    }

    return (
        <div className="smallBox17 col-sm-9">
            <div className="smallBox27">
                <div className='margin3'>
                    <span className="textDecoGreen">Personal Details</span> 
                </div>
                <div>
                    <div className="smallBox19"> 
                        <div className="margin1">
                            <span className="clickRegisterText">Name</span>
                            <input className="form-control enterDiv2" type="text" value={profile.name} onChange={nameValidation}></input>
                        </div>
                        <div className="margin1">
                            <span className="clickRegisterText">Email</span>
                            <input className="form-control enterDiv2" type="email" value={profile.email} readOnly></input>
                        </div>
                    </div>
                </div>
                {error ? <div className='flexRow margin6 errorText'>{errorMessage}</div> : null}
                <a id="update" className="btn btn-outline-success smallBox9 margin1 disabled" href="" data-bs-toggle="modal" data-bs-target="#modal1">
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
                            <button type="button" className="btn btn-outline-success" id="save" data-bs-dismiss="modal" onClick={updateEmployeeDetails}>Update</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default AdminProfile;