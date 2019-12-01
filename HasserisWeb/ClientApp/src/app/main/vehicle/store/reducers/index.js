import {combineReducers} from 'redux';
import vehicles from './vehicle.reducer';

const reducer = combineReducers({
    vehicles,
});

export default reducer;
