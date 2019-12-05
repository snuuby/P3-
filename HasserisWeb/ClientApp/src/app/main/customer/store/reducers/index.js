import {combineReducers} from 'redux';
import customers from './customer.reducer';

const reducer = combineReducers({
    customers
});

export default reducer;
