import {combineReducers} from 'redux';
import inspections from './inspection.reducer';
import offers from './offer.reducer';
import tasks from './task.reducer';


const reducer = combineReducers({
    inspections,
    offers,
    tasks
});

export default reducer;
