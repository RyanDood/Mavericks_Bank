import './App.css';
import Landing from './Components/Landing/Landing';
import AllLoans from './Components/Loans/AllLoans';
import RegisterCustomer from './Components/RegisterCustomer/RegisterCustomer';
import ForgotPassword from './Components/ForgotPassword/ForgotPassword';
import AllCustomerAccounts from './Components/Accounts/AllCustomerAccounts';
import AllCustomerBeneficiaries from './Components/Beneficiaries/AllCustomerBeneficiaries';
import AllCustomerTransactions from './Components/Transactions/AllCustomerTransactions';
import { BrowserRouter, Route ,Routes} from 'react-router-dom';
import InvalidPage from './Components/Errors/InvalidPage';
import LoginRoute from './Components/PrivateRoutes/LoginRoute';
import AvailedLoans from './Components/Loans/AvailedLoans';
import Menu from './Components/Menu/Menu';

function App() {
  return (
    <BrowserRouter>
        <Routes>
            <Route element={<LoginRoute/>}>
                <Route path="/" element={<Landing/>}/>
            </Route>
            <Route path="forgotPassword" element={<ForgotPassword/>}/>
            <Route path="registerCustomer" element={<RegisterCustomer/>}/>
            <Route path="menu" element={<Menu/>}>
                <Route path="availedLoans" element={<AvailedLoans/>}/>
                <Route path="allLoans" element={<AllLoans/>}/>
                <Route path="customerAccounts" element={<AllCustomerAccounts/>}/>
                <Route path="customerBeneficiaries" element={<AllCustomerBeneficiaries/>}/>
                <Route path="customerTransactions" element={<AllCustomerTransactions/>}/>
            </Route>
            <Route path="*" element={<InvalidPage/>}/>
        </Routes>
    </BrowserRouter>
  );
}

export default App;
