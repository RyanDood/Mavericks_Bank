import './App.css';
import Landing from './Components/Landing/Login/Login';
import AllLoans from './Components/Loans/AllLoans/AllLoans';
import RegisterCustomer from './Components/Landing/RegisterCustomer/RegisterCustomer';
import ForgotPassword from './Components/Landing/ForgotPassword/ForgotPassword';
import AllCustomerAccounts from './Components/Accounts/AllCustomerAccounts/AllCustomerAccounts';
import AllCustomerBeneficiaries from './Components/Beneficiaries/AllCustomerBeneficiaries/AllCustomerBeneficiaries';
import AllCustomerTransactions from './Components/Transactions/AllCustomerTransactions/AllCustomerTransactions';
import { BrowserRouter, Route ,Routes} from 'react-router-dom';
import InvalidPage from './Components/Errors/InvalidPage/InvalidPage';
import LoginRoute from './Components/PrivateRoutes/LoginRoute';
import AvailedLoans from './Components/Loans/AvailedLoans/AvailedLoans';
import Menu from './Components/Menu/Menu';
import ViewAccount from './Components/Accounts/ViewAccount/ViewAccount';
import OpenNewAccount from './Components/Accounts/OpenNewAccount/OpenNewAccount';
import AddBeneficiary from './Components/Beneficiaries/AddBeneficiary/AddBeneficiary';
import ApplyLoan from './Components/Loans/ApplyLoan/ApplyLoan';
import ViewTransaction from './Components/Transactions/ViewTransaction/ViewTransaction';
import TransferMoney from './Components/Transactions/TransferMoney/TransferMoney';
import DepositMoney from './Components/Transactions/DepositMoney/DepositMoney';
import Withdraw from './Components/Transactions/Withdraw/Withdraw';
import Profile from './Components/Profile/Profile';
import RecentTransaction from './Components/Accounts/ViewAccount/RecentTransaction/RecentTransaction';
import LastMonthTransaction from './Components/Accounts/ViewAccount/LastMonthTransaction/LastMonthTransaction';
import FilterTransaction from './Components/Accounts/ViewAccount/FilterTransaction.js/FilterTransaction';

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
                <Route path="customerAccounts" element={<AllCustomerAccounts/>}/>
                <Route path="viewAccount" element={<ViewAccount/>}>
                    <Route index element={<RecentTransaction/>}/>
                    <Route path="recentTransaction" element={<RecentTransaction/>}/>
                    <Route path="lastMonthTransaction" element={<LastMonthTransaction/>}/>
                    <Route path="filterTransaction" element={<FilterTransaction/>}/>
                </Route>
                <Route path="openAccount" element={<OpenNewAccount/>}/>
                <Route path="customerTransactions" element={<AllCustomerTransactions/>}/>
                <Route path="viewTransaction" element={<ViewTransaction/>}/>
                <Route path="transferMoney" element={<TransferMoney/>}/>
                <Route path="depositMoney" element={<DepositMoney/>}/>
                <Route path="withdrawMoney" element={<Withdraw/>}/>
                <Route path="allLoans" element={<AllLoans/>}/>
                <Route path="availedLoans" element={<AvailedLoans/>}/>
                <Route path="applyLoan" element={<ApplyLoan/>}/>
                <Route path="customerBeneficiaries" element={<AllCustomerBeneficiaries/>}/>
                <Route path="addBeneficiary" element={<AddBeneficiary/>}/>
                <Route path="profile" element={<Profile/>}/>
            </Route>
            <Route path="*" element={<InvalidPage/>}/>
        </Routes>
    </BrowserRouter>
  );
}

export default App;
