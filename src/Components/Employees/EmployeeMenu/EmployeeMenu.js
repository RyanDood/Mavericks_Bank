import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, Outlet, useNavigate } from 'react-router-dom';

function EmployeeMenu(){

    var [clicked,setClicked] = useState([false,false,false]);
    var navigate = useNavigate();

    var removeSession = () => {
        sessionStorage.removeItem("email");
        sessionStorage.removeItem("token");
        sessionStorage.removeItem("id");
        navigate("/");
    }

    function navigateToAccounts(){
        setClicked([true,false,false]);
        navigate("/employeeMenu/accounts/openRequests");
    }

    function navigateToLoans(){
        setClicked([false,true,false]);
        navigate("/employeeMenu/loans");
    }

    function navigateToProfile(){
        setClicked([false,false,true]);
        navigate("/employeeMenu/employeeProfile");
    }

    return (
        <div className="container">
            <div className="row">
                <div className="smallBox50 col-md-3">
                    <div className="smallBox51">
                        <div className="flexRow4">
                            <div className="logoImage change-my-color3"></div>
                            <span className="logo">mavericks</span>
                        </div>
                        <ul className="smallBox52 nav">
                            <div className="flexRow3">
                                <div className="account change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[0] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToAccounts}>Accounts</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToAccounts}>Accounts</span>}
                                </li>
                            </div>
                            <hr className="navBarLine"></hr>
                            <div className="flexRow3">
                                <div className="loan change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[1] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToLoans}>Loans</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToLoans}>Loans</span>}
                                </li>
                            </div>
                            <hr className="navBarLine"></hr>
                            <div className="flexRow3">
                                <div className="loan change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[2] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToProfile}>Profile</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToProfile}>Profile</span>}
                                </li>
                            </div>
                            <hr className="navBarLine"></hr>
                            <div className="flexRow3">
                                <div className="signout change-my-color3"></div>
                                <li className="nav-item highlight">
                                    <a className="nav-link textDecoWhite" href="index.html" data-bs-toggle="modal" data-bs-target="#modal2">Signout</a>
                                </li>
                            </div>
                        </ul>
                    </div>
                    <div className="modal fade" id="modal2" tabIndex="-1" aria-labelledby="modalEg1" aria-hidden="true">
                        <div className="modal-dialog">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h6 className="modal-title" id="modalEg1">Sign Out</h6>
                                    <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="No"></button>
                                </div>
                                <div className="modal-body">
                                    Are you sure you want to Sign Out?
                                </div>
                                <div className="modal-footer">
                                    <button type="button" className="btn btn-outline-danger" data-bs-dismiss="modal">Back</button>
                                    <a className="btn btn-outline-success" onClick={removeSession} data-bs-dismiss="modal">Sign Out</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <Outlet/>
            </div>
        </div>
    );
}

export default EmployeeMenu;