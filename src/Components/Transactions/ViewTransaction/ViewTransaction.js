import axios from 'axios';
import { useState } from 'react';
import 'C:/Ryan/.NET + React/mavericks_bank/src/Components/style.css';
import { Link, useNavigate } from 'react-router-dom';

function ViewTransaction(){
    return (
        <div className="smallBox17 col-md-9">
                <div className="smallBox26">
                    <div className="upMargin">
                        <Link to="/menu/customerTransactions">
                            <div className="leftArrow change-my-color"></div>
                        </Link>
                    </div>
                    <span className="clickRegisterText7">Transfer To Tharun Kumar</span>
                    <span className="clickRegisterText8">211</span>
                    <span className="clickRegisterText7">Food Payment</span>
                    <hr className="hrS"></hr>
                    <span className="clickRegisterText7">From</span>
                    <span className="clickRegisterText7">Ryan Paul Jess C</span>
                    <span className="clickRegisterText7">Mavericks Bank Private Limited</span>
                    <hr className="hrS"></hr>
                    <span className="clickRegisterText7">Paid at 7:31 PM, 27 Jan 2024</span>
                    <span className="clickRegisterText7">Transaction ID: 232492781</span>
                </div>
            </div>
    );
}

export default ViewTransaction;