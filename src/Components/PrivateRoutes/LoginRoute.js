import { Navigate, Outlet } from "react-router-dom";

function LoginRoute(){
    var token = sessionStorage.getItem('token');
    return (
        token ? <Navigate to='/menu/dashboard'/> :  <Outlet/> 
    );
}

export default LoginRoute;