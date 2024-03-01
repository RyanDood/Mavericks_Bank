import axios from 'axios';
import { useEffect, useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, Outlet, useNavigate } from 'react-router-dom';

function Menu(){

    var [clicked,setClicked] = useState([false,false,false,false,false,false]);
    var navigate = useNavigate();

    var removeSession = () => {
        sessionStorage.removeItem("email");
        sessionStorage.removeItem("token");
        sessionStorage.removeItem("id");
        navigate("/");
    }

    function navigateToDashboard(){
        setClicked([true,false,false,false,false,false]);
        navigate("/menu/dashboard");
    }

    function navigateToAccounts(){
        setClicked([false,true,false,false,false,false]);
        navigate("/menu/customerAccounts");
    }

    function navigateToTransactions(){
        setClicked([false,false,true,false,false,false]);
        navigate("/menu/customerTransactions");
    }

    function navigateToLoans(){
        setClicked([false,false,false,true,false,false]);
        navigate("/menu/allLoans");
    }

    function navigateToBeneficiaries(){
        setClicked([false,false,false,false,true,false]);
        navigate("/menu/customerBeneficiaries");
    }

    function navigateToProfile(){
        setClicked([false,false,false,false,false,true]);
        navigate("/menu/profile");
    }

    return (
        <div className="container">
            <div className="row">
                <div className="smallBox15 col-md-3">
                    <div className="smallBox34">
                        <div className="flexRow4">
                            <div className="logoImage change-my-color3"></div>
                            <span className="logo">mavericks</span>
                        </div>
                        <ul className="smallBox16 nav">
                            <div className="flexRow3">
                                <div className="dashboard change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[0] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToDashboard}>DashBoard</span> : 
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToDashboard}>DashBoard</span>}
                                </li>
                            </div>
                            <hr className="navBarLine"></hr>
                            <div className="flexRow3">
                                <div className="account change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[1] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToAccounts}>Accounts</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToAccounts}>Accounts</span>}
                                </li>
                            </div>
                            <hr className="navBarLine"></hr>
                            <div className="flexRow3">
                                <div className="transaction change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[2] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToTransactions}>Transactions</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToTransactions}>Transactions</span>}
                                </li>
                            </div>
                            <hr className="navBarLine"></hr>
                            <div className="flexRow3">
                                <div className="loan change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[3] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToLoans}>Loans</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToLoans}>Loans</span>}
                                </li>
                            </div>
                            <hr className="navBarLine"></hr>
                            <div className="flexRow3">
                                <div className="beneficiary change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[4] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToBeneficiaries}>Beneficiaries</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToBeneficiaries}>Beneficiaries</span>}
                                </li>
                            </div>
                            <hr className="navBarLine"></hr>
                            <div className="flexRow3">
                                <div className="profile change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[5] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToProfile}>Profile</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToProfile}>Profile</span>}
                                </li>
                            </div>
                            <hr className="navBarLine"></hr>
                            <div className="flexRow3">
                                <div className="signout change-my-color3"></div>
                                <li className="nav-item highlight">
                                    <span className="nav-link textDecoWhite pointer" href="index.html" data-bs-toggle="modal" data-bs-target="#modal2">Signout</span>
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
            <div className="wrapper">
        <nav id="sidebar" className="mCustomScrollbar _mCS_1 mCS-autoHide mCS_no_scrollbar active flexColumn" style={{overflow: 'visible'}}><div id="mCSB_1" className="mCustomScrollBox mCS-minimal mCSB_vertical mCSB_outside" style={{ maxHeight: 'none' }} tabIndex="0"><div id="mCSB_1_container" className="mCSB_container mCS_y_hidden mCS_no_scrollbar_y" style={{ position: 'relative', top: 0, left: 0 }} dir="ltr">
            <div className="leftArrow change-my-color" id="dismiss"></div>
            <div className="flexRow4 margin3">
                <div className="logoImage change-my-color3"></div>
                <span className="logo">mavericks</span>
            </div>
            <ul className="smallBox16 nav">
                            <div className="flexRow3">
                                <div className="dashboard change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[0] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToDashboard}>DashBoard</span> : 
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToDashboard}>DashBoard</span>}
                                </li>
                            </div>
                            <hr></hr>
                            <div className="flexRow3">
                                <div className="account change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[1] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToAccounts}>Accounts</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToAccounts}>Accounts</span>}
                                </li>
                            </div>
                            <hr></hr>
                            <div className="flexRow3">
                                <div className="transaction change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[2] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToTransactions}>Transactions</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToTransactions}>Transactions</span>}
                                </li>
                            </div>
                            <hr></hr>
                            <div className="flexRow3">
                                <div className="loan change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[3] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToLoans}>Loans</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToLoans}>Loans</span>}
                                </li>
                            </div>
                            <hr></hr>
                            <div className="flexRow3">
                                <div className="beneficiary change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[4] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToBeneficiaries}>Beneficiaries</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToBeneficiaries}>Beneficiaries</span>}
                                </li>
                            </div>
                            <hr></hr>
                            <div className="flexRow3">
                                <div className="profile change-my-color3"></div>
                                <li className="nav-item highlight">
                                    {clicked[5] ? <span className="nav-link textDecoGreen pointer" onClick={navigateToProfile}>Profile</span> :
                                    <span className="nav-link textDecoWhite pointer" onClick={navigateToProfile}>Profile</span>}
                                </li>
                            </div>
                            <hr></hr>
                            <div className="flexRow3">
                                <div className="signout change-my-color3"></div>
                                <li className="nav-item highlight">
                                    <span className="nav-link textDecoWhite pointer" href="index.html" data-bs-toggle="modal" data-bs-target="#modal2">Signout</span>
                                </li>
                            </div>
                        </ul>
        </div></div><div id="mCSB_1_scrollbar_vertical" className="mCSB_scrollTools mCSB_1_scrollbar mCS-minimal mCSB_scrollTools_vertical" style={{ display: 'none' }}><div className="mCSB_draggerContainer"><div id="mCSB_1_dragger_vertical" className="mCSB_dragger" style={{ position: 'absolute', minHeight: '50px', height: '0px', top: '0px' }}><div className="mCSB_dragger_bar" style={{ lineHeight: '50px' }}></div></div><div className="mCSB_draggerRail"></div></div></div></nav>

        <div id="content">
            <nav className="navbar navbar-expand-lg navbar">
                <div className="container-fluid">
                    <div className="menu change-my-color pointer" id="sidebarCollapse"></div>
                </div>
            </nav>
            <Outlet/>
        </div>
        </div>
        </div>
    );
}

export default Menu;