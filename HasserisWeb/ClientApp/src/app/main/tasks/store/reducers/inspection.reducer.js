import * as Actions from '../actions';

const initialState = {
    availableEmployees: [],
    inspections: [],
    availableCars: [],
    customers: [],
    searchText: '',
    eventDialog: {
        type: 'new',
        made: false,
        edit: false,
        image: '',
        props: {
            open: true,
        },
        data : null
    }
};

const inspectionReducer = function (state = initialState, action) {
    switch (action.type) {
        case Actions.SET_INSPECTIONOVERVIEW_SEARCH_TEXT:
        {
            return {
                ...state,
                searchText: action.searchText
            };
        }
        case Actions.GET_ALL_INSPECTION_REPORT:
            {
                if (!Array.isArray(action.payload)) {
                    return {
                        ...state,
                        inspections: { ...action.payload },
                    };
                }
                const inspections = action.payload.map((inspection) => (
                    {
                        ...inspection
                    }
                ));

                return {
                    ...state,
                    inspections
                };
            }

        case Actions.GET_INSPECTION_REPORT:
            {
                return {
                    ...state,
                    eventDialog: {
                        type: 'new',
                        props: {
                            open: true
                        },
                        made: true,
                        data: {
                            CustomerName: action.payload.Customer.CustomerType == "Private" ? action.payload.Customer.Firstname + ' ' + action.payload.Customer.Lastname : action.payload.Customer.Name, 
                            StartAddress: action.payload.StartingAddress.LivingAddress,
                            StartZIP: action.payload.StartingAddress.ZIP,
                            StartCity: action.payload.StartingAddress.City,
                            DestinationAddress: action.payload.Destination.LivingAddress,
                            DestinationZIP: action.payload.Destination.ZIP,
                            DestinationCity: action.payload.Destination.City,
                            Customer: action.payload.Customer,
                            Employee: action.payload.Employee,
                            Car: action.payload.Car,
                            CustomerID: action.payload.Customer.ID,
                            EmployeeID: action.payload.Employee.ID,
                            CarID: action.payload.Car.ID,
                            InspectionDate: action.payload.InspectionDate,
                            ...action.payload
                        }
                    }
                };
            }
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

        case Actions.GET_AVAILABLE_EMPLOYEES:
            {
                return Object.assign({}, state, {
                    availableEmployees:  action.payload 
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
            case Actions.SAVE_EDIT_INSPECTION_REPORT:
                {
                    return Object.assign({}, state, {
                        edit: true
                    })
                }
        
        default: {
            return state;
        }    
        
        
        
        
        
    }

}
export default inspectionReducer;