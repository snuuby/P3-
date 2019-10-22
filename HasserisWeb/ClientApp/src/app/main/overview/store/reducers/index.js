import {combineReducers} from 'redux';
import employees from './events.reducer';

const reducer = combineReducers({
    employees
});

export default reducer;
