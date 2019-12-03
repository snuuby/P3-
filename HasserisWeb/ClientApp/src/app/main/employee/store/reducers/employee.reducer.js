import * as Actions from '../actions';

const initialState = {
    employees   : [],
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
                if (!Array.isArray(action.payload)) {
                    return {
                        Available: action.payload.IsAvailable ? "Yes" : "No",
                        ...state,
                        employees: { ...action.payload },
                    };
                }
            const employees = action.payload.map((employee) => (
                {
                    Available: employee.IsAvailable ? "Yes" : "No",
                    ...employee
                }
            ));

            return {
                ...state,
                employees
            };
        }

        case Actions.GET_EMPLOYEE:
            {
                return Object.assign({}, state, {
                    eventDialog: {
                        type: 'new',
                        props: {
                            open: true
                        },
                        data: {
                            LivingAddress: action.payload.Address.LivingAddress,
                            ZIP: action.payload.Address.ZIP,
                            City: action.payload.Address.City,
                            Email: action.payload.ContactInfo.Email,
                            Phonenumber: action.payload.ContactInfo.PhoneNumber,
                            Available: action.payload.IsAvailable ? "Yes" : "No",
                            ...action.payload ,
                        }
                    }
                })
            }
        
        case Actions.SET_OVERVIEW_SEARCH_TEXT:
            {
            return {
                ...state,
                searchText: action.searchText
            };
        }

        case Actions.ADD_EMPLOYEE:
            {
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
export default overviewReducer;