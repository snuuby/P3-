import * as Actions from '../actions';

const initialState = {
    entities   : [],
    searchText: '',
    loading: true,
    customerData: null,
    eventDialog: {
        type : 'new',
        props: {
            open: false
        },
        data : null
    }
};

const customerReducer = function (state = initialState, action) {
    switch (action.type) {
        
        case Actions.GET_CUSTOMERS:
        {
            const entities = action.payload.map((customer) => (
                {
                    ...customer
                }
            ));

            return {
                ...state,
                entities
            };
        }

        case Actions.GET_CUSTOMER:
            {
                return Object.assign({}, state, {
                    customerData: action.payload
                })
            }
        
        case Actions.SET_CUSTOMEROVERVIEW_SEARCH_TEXT:{
            return {
                ...state,
                searchText: action.searchText
            };
        }

        case Actions.ADD_CUSTOMER:{
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
                        ...action.payload
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
export default customerReducer;