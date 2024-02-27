import { useState } from "react";
import '../../style.css';
import { Outlet, useNavigate } from "react-router-dom";
import Welcome from "../Welcome/Welcome";

function Accounts() {
    return (
        <div className="smallBox17 col-md-9">
            <div className="smallBox40">
                <Welcome/>
                <hr className="hrS"></hr>
                <Outlet/>
            </div>
        </div>
    );
}

export default Accounts;