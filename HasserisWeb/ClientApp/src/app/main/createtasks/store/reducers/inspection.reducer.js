import * as Actions from '../actions';

const initialState = {
    availableEmployees: [],
    availableCars: [],
    customers: [],
    eventDialog: {
        type: 'new',
        made: false,
        image: '',
        props: {
            open: true,
        },
        data : null
    }
};

const inspectionReducer = function (state = initialState, action) {
    switch (action.type) {
        case Actions.OPEN_NEW_INSPECTION_REPORT:
            {
                return {
                    ...state,
                    eventDialog: {
                        type: 'new',
                        props: {
                            open: true
                        },
                        data : {
                            ...action.data
                        }                    
                    }
                };
            }
        case Actions.CLOSE_NEW_INSPECTION_REPORT:
            {
                return {
                    ...state,
                    eventDialog: {
                        type: 'new',
                        props: {
                            open: false
                        },
                        data: null
                    }
                };
            }
        case Actions.SET_INSPECTION_REPORT_IMAGE:
            {
                return Object.assign({}, state, {
                    image: action.payload
                })
            }
        case Actions.GET_AVAILABLE_EMPLOYEES:
            {
                return Object.assign({}, state, {
                    availableEmployees: action.payload
                })
            }
        case Actions.GET_AVAILABLE_CARS:
            {
                return Object.assign({}, state, {
                    availableCars: action.payload
                })
            }
        case Actions.GET_CUSTOMERS:
            {
                return Object.assign({}, state, {
                    customers: action.payload
                })
            }
        case Actions.SAVE_INSPECTION_REPORT:
            {
                return Object.assign({}, state, {
                    made: true
                })
            }

        
        default: {
            return state;
        }    
        
        
        
        
        
    }

}
export default inspectionReducer;