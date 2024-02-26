import { configureStore } from "@reduxjs/toolkit";
import loanReducer from "./loanSlice";
import transactionReducer from "./transactionSlice";
import accountReducer from "./accountSlice";
import statementReducer from "./statementSlice";

const store = configureStore({
    reducer: {
        loanID: loanReducer,
        transactionID: transactionReducer,
        accountID: accountReducer,
        statement: statementReducer
    },
});

export default store;