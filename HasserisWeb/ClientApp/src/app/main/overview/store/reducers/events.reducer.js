import * as Actions from '../actions';

const initialState = {
    entities   : [],
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
        default: {
            return state;
        }    
        
        
        
    }

}
export default overviewReducer;