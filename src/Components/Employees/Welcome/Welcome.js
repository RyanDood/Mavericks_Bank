import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';

function Welcome(){

    var [name,setName] = useState("");
    var employeeID = sessionStorage.getItem('id');

    useEffect(() => {
        getEmployeeDetails();
    },[])

    const token = sessionStorage.getItem('token');
    const httpHeader = { 
        headers: {'Authorization': 'Bearer ' + token}
    };

    async function getEmployeeDetails(){
        await axios.get('http://localhost:5224/api/BankEmployees/GetBankEmployee?employeeID=' + employeeID,httpHeader)
        .then(function (response) {
            console.log(response.data);
            setName(response.data.name);
        })
        .catch(function (error) {
            console.log(error);
        })
    }

    return(
        <div className="flexRow2">
            <span className="clickRegisterText5">Hi {name},</span>
        </div>
    );
}

export default Welcome;