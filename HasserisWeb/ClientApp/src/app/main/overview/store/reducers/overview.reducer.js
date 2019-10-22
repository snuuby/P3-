import * as Actions from '../actions';

const initialState = {
    entities   : [],
    searchText: '',
    loading: true
};

const overviewReducer = function (state = initialState, action) {
    switch (action.type) {
        
        case Actions.GET_EMPLOYEES:
        {
            const entities = action.payload.map((employee) => (
                {
                    ...employee
                }
            ));

            return {
                ...state,
                entities
            };
        }
        
        case Actions.SET_OVERVIEW_SEARCH_TEXT:{
            return {
                ...state,
                searchText: action.searchText
            };
        }
        
        default: {
            return state;
        }    
        
        
        
        
        
    }

}
export default overviewReducer;