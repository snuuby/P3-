import * as Actions from '../actions';

const initialState = {
    availableEmployees: [],
    movingTasks: [],
    availableTools: [],
    deliveryTasks: [],
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
        data: null
    }
};

const taskReducer = function (state = initialState, action) {
    switch (action.type) {
        case Actions.SET_TASKOVERVIEW_SEARCH_TEXT:
            {
                return {
                    ...state,
                    searchText: action.searchText
                };
            }
        case Actions.GET_ALL_MOVING_TASKS:
            {
                if (!Array.isArray(action.payload)) {
                    return {
                        ...state,
                        movingTasks: { ...action.payload },
                    };
                }
                else {
                    const movingTasks = action.payload.map((moving) => (
                        {
                            ...moving,
                        }
                    ));

                    return {
                        ...state,
                        movingTasks
                    };
                }

            }
        case Actions.GET_ALL_DELIVERY_TASKS:
            {

                if (!Array.isArray(action.payload)) {
                    return {
                        ...state,
                        deliveryTasks: { ...action.payload },
                    };
                }
                else {
                    const deliveryTasks = action.payload.map((delivery) => (
                        {
                            ...delivery,
                        }
                    ));

                    return {
                        ...state,
                        deliveryTasks
                    };
                }

            }

        case Actions.GET_TASK:
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
                            StartAddress: action.payload.StartingAddress.LivingAddress,
                            StartZIP: action.payload.StartingAddress.ZIP,
                            StartCity: action.payload.StartingAddress.City,
                            DestinationAddress: action.payload.Destination.LivingAddress,
                            DestinationZIP: action.payload.Destination.ZIP,
                            DestinationCity: action.payload.Destination.City,
                            Customer: action.payload.Customer,
                            Employee: action.payload.Employees[0],
                            Car: action.payload.Equipment[0],
                            InspectionDate: action.payload.InspectionDate,
                            CustomerID: action.payload.Customer.ID,
                            EmployeeID: action.payload.Employees[0].ID,
                            CarID: action.payload.Equipment[0].ID,
                            WasInspection: action.payload.WasInspection,
                            WasOffer: action.payload.WasOffer,
                            ...action.payload
                        }
                    }
                };
            }
        case Actions.INSPECTION_TO_TASK:
            {
                return {
                    ...state,
                    eventDialog: {
                        type: 'new',
                        props: {
                            open: true
                        },
                        data: {
                            CustomerName: action.payload.Customer.$type == "Priate" ? action.payload.Customer.Firstname + ' ' + action.payload.Customer.Lastname : action.payload.Customer.Name,
                            CustomerMail: action.payload.Customer.ContactInfo.Email,
                            InspectionReportID: action.payload.ID,
                            CustomerID: action.payload.Customer.ID,
                            EmployeeID: action.payload.Employees[0].ID,
                            CarID: action.payload.Equipment[0].ID,
                            WasInspection: true,
                            WasOffer: false,

                            ...action.payload
                        }
                    }
                };
            }
        case Actions.OFFER_TO_TASK:
            {
                return {
                    ...state,
                    eventDialog: {
                        type: 'new',
                        props: {
                            open: true
                        },
                        data: {
                            
                            CustomerName: action.payload.Customer.$type == "Priate" ? action.payload.Customer.Firstname + ' ' + action.payload.Customer.Lastname : action.payload.Customer.Name,
                            CustomerMail: action.payload.Customer.ContactInfo.Email,
                            InspectionReportID: action.payload.ID,
                            CustomerID: action.payload.Customer.ID,
                            EmployeeID: action.payload.Employees[0].ID,
                            CarID: action.payload.Equipment[0].ID,
                            WasInspection: action.payload.WasInspection,
                            WasOffer: true,
                            ...action.payload
                        }
                    }
                };
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
        case Actions.GET_AVAILABLE_TOOLS:
            {
                return Object.assign({}, state, {
                    availableTools: action.payload
                })
            }
        case Actions.GET_CUSTOMERS:
            {
                return Object.assign({}, state, {
                    customers: action.payload
                })
            }
        case Actions.ADD_MOVING_TASK:
            {
                return Object.assign({}, state, {
                    made: true
                })
            }
        case Actions.ADD_DELIVERY_TASK:
            {
                return Object.assign({}, state, {
                    made: true
                })
            }
        case Actions.SAVE_EDIT_MOVING_TASK:
            {
                return Object.assign({}, state, {
                    edit: true
                })
            }
        case Actions.SAVE_EDIT_DELIVERY_TASK:
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
export default taskReducer;