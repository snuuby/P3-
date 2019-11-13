import * as Actions from '../actions';

const initialState = {
    entities   : [],
    searchText: '',
    loading: true,
    eventDialog: {
        type : 'new',
        props: {
            open: false
        },
        data : null
    }
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

        case Actions.GET_EMPLOYEE:
            {
                return {
                    ...action.payload
                };
            }
        
        case Actions.SET_OVERVIEW_SEARCH_TEXT:{
            return {
                ...state,
                searchText: action.searchText
            };
        }

        case Actions.ADD_EMPLOYEE:{
            return {
                ...state,
                searchText: action.searchText
            };
        }

        case Actions.OPEN_NEW_ADD_DIALOG:
        {
            return {
                ...state,
                eventDialog: {
                    type : 'new',
                    props: {
                        open: true
                    },
                    data : {
                        ...action.data
                    }
                }
            };
        }

        case Actions.CLOSE_NEW_ADD_DIALOG:
        {
            return {
                ...state,
                eventDialog: {
                    type : 'new',
                    props: {
                        open: false
                    },
                    data : null
                }
            };
        }
        
        default: {
            return state;
        }    
        
        
        
        
        
    }

}
export default overviewReducer;