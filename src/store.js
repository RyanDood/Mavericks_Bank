import { configureStore } from "@reduxjs/toolkit";
import loanReducer from "./loanSlice";
import transactionReducer from "./transactionSlice";
import accountReducer from "./accountSlice";
import customerReducer from "./customerSlice";
import statementReducer from "./statementSlice";

const store = configureStore({
    reducer: {
        customerID: customerReducer,
        loanID: loanReducer,
        transactionID: transactionReducer,
        accountID: accountReducer,
        statement: statementReducer
    },
});

export default store;