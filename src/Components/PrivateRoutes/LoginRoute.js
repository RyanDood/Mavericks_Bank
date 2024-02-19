import { Navigate, Outlet } from "react-router-dom";

function LoginRoute(){
    var token = sessionStorage.getItem('token');
    return (
        token ? <Navigate to='/customerAccounts'/> :  <Outlet/> 
    );
}

export default LoginRoute;