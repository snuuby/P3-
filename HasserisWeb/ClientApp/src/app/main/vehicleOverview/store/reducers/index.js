import {combineReducers} from 'redux';
import vehicles from './vehicleOverview.reducer';

const reducer = combineReducers({
    vehicles,
});

export default reducer;
