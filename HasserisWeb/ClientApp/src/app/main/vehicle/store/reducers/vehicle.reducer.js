import * as Actions from '../actions';

const initialState = {
    vehicles   : [],
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

const vehicleReducer = function (state = initialState, action) {
    switch (action.type) {
        
        case Actions.GET_VEHICLES:
        {
            const vehicles = action.payload.map((vehicle) => (
                {
                    ...vehicle
                }
            ));

            return {
                ...state,
                vehicles
            };
        }

        case Actions.GET_VEHICLE:
            {
                return Object.assign({}, state, {
                    eventDialog: {
                        type: 'new',
                        props: {
                            open: true,
                        },
                        data: {
                            Available: action.payload.isAvailable,
                            ...action.payload,
                        },
                    }
                })
            }
        
        case Actions.SET_VEHICLEOVERVIEW_SEARCH_TEXT:{
            return {
                ...state,
                searchText: action.searchText
            };
        }

        case Actions.ADD_VEHICLE:{
            return {
                ...state
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
export default vehicleReducer;