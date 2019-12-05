import {combineReducers} from 'redux';
import tools from './tool.reducer';

const reducer = combineReducers({
    tools,
});

export default reducer;
