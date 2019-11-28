import * as Actions from '../actions';

const initialState = {
    entities   : [],
    loading: true,
    toolData: null,
    eventDialog: {
        type : 'new',
        props: {
            open: false
        },
        data : null
    }
};

const toolReducer = function (state = initialState, action) {
    switch (action.type) {
        
        case Actions.GET_TOOLS:
        {
            const entities = action.payload.map((tool) => (
                {
                    ...tool
                }
            ));

            return {
                ...state,
                entities
            };
        }

        case Actions.GET_TOOL:
            {
                return Object.assign({}, state, {
                    toolData: action.payload
                })
            }
        
        case Actions.SET_TOOLOVERVIEW_SEARCH_TEXT:{
            return {
                ...state,
                searchText: action.searchText
            };
        }

        case Actions.ADD_TOOL:{
            return {
                ...state,
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
export default toolReducer;