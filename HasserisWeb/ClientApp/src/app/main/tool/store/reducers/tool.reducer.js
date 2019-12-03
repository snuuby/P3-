import * as Actions from '../actions';

const initialState = {
    tools   : [],
    loading: true,
    eventDialog: {
        type : 'new',
        props: {
            open: false
        },
        data : null
    },
    searchText: '',
};

const toolReducer = function (state = initialState, action) {
    switch (action.type) {
        
        case Actions.GET_TOOLS:
            {
                if (!Array.isArray(action.payload)) {
                    return {
                        ...state,
                        tools: {
                            Available: action.payload.IsAvailable ? "Yes" : "No",
                            ...action.payload
                        },
                    };
                }
            const tools = action.payload.map((tool) => (
                {
                    Available: tool.IsAvailable ? "Yes" : "No",
                    ...tool
                }
            ));

            return {
                ...state,
                tools
            };
        }

        case Actions.GET_TOOL:
            {
                return Object.assign({}, state, {
                    eventDialog: {
                        type: 'new',
                        props: {
                            open: true,
                        },
                        data: {
                            Available: action.payload.IsAvailable ? "Yes" : "No",
                            ...action.payload,
                        },
                    }
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