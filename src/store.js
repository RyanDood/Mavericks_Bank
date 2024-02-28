import { configureStore } from "@reduxjs/toolkit";
import loanReducer from "./loanSlice";
import transactionReducer from "./transactionSlice";
import accountReducer from "./accountSlice";
import statementReducer from "./statementSlice";
import accountNumberReducer from "./accountNumberSlice";
import dateReducer from "./dateSlice";

const store = configureStore({
    reducer: {
        loanID: loanReducer,
        transactionID: transactionReducer,
        accountID: accountReducer,
        statement: statementReducer,
        accountNumber: accountNumberReducer,
        date: dateReducer
    },
});

export default store;