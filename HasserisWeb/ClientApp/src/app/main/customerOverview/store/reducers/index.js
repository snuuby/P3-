import {combineReducers} from 'redux';
import customers from './customerOverview.reducer';

const reducer = combineReducers({
    customers
});

export default reducer;
