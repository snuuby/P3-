import {combineReducers} from 'redux';
import tools from './toolOverview.reducer';

const reducer = combineReducers({
    tools,
});

export default reducer;
