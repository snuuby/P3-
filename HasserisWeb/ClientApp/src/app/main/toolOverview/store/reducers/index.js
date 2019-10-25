import {combineReducers} from 'redux';
import employees from './overview.reducer';

const reducer = combineReducers({
    employees,
});

export default reducer;
