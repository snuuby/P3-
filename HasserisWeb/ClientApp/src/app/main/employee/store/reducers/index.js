import {combineReducers} from 'redux';
import employees from './employee.reducer';

const reducer = combineReducers({
    employees,
});

export default reducer;
