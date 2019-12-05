import * as Actions from '../actions';

const initialState = {
    Deliveries: [],
    Movings: [],
    eventDialog: {
        type : 'new',
        props: {
            open: false
        },
        eventType : null,
        data : null
    }
};

const eventsReducer = function (state = initialState, action) {
    switch ( action.type )
    {
        case Actions.GET_DELIVERY_EVENTS:
        {

            const Deliveries = action.payload.map((delivery) => (
                {
                    /*
                    id: delivery.task.ID,
                    title: delivery.task.Name,
                    desc: delivery.task.Description,
                    customer: delivery.Customer,
                    employees: delivery.Employees,
                    equipment: delivery.Equipment,
                    quantity: delivery.task.Quantity,
                    material: delivery.task.Material,
                    start: new Date(delivery.Dates[0].Date),
                    end: new Date(delivery.Dates[delivery.Dates.length - 1].Date),
                    image: delivery.task.PhotoPath
                    */
                    start: new Date(delivery.Dates[0].Date),
                    end: new Date(delivery.Dates[delivery.Dates.length - 1].Date),
                    title: delivery.Name,
                    ...delivery,
                }

            ));

            return {
                ...state,
                Deliveries,
            };
        }
        case Actions.GET_MOVING_EVENTS:
            {

                const Movings = action.payload.map((moving) => (
                    {
                        /* 
                        id: moving.task.ID,
                        title: moving.task.Name,
                        desc: moving.task.Description,
                        customer: moving.Customer,
                        employees: moving.Employees,
                        equipment: moving.Equipment,
                        lentboxes: moving.task.LentBoxes,
                        start: new Date(moving.Dates[0].Date),
                        startingaddress: moving.StartingAddress,
                        destination: moving.Destination,
                        furniture: moving.Furniture,
                        end: new Date(moving.Dates[moving.Dates.length - 1].Date),
                        image: moving.task.PhotoPath
                        */
                        start: new Date(moving.Dates[0].Date),
                        end: new Date(moving.Dates[moving.Dates.length - 1].Date),                    
                        title: moving.Name,
                        ...moving,
                    }
                ));

                return {
                    ...state,
                    Movings,
                };
            }

        case Actions.OPEN_NEW_EVENT_DIALOG:
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
        case Actions.CLOSE_NEW_EVENT_DIALOG:
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
        case Actions.OPEN_EDIT_EVENT_DIALOG:
        {
            return {
                ...state,
                eventDialog: {
                    type : 'edit',
                    props: {
                        open: true
                    },
                    data : {
                        ...action.data,
                        start: new Date(action.data.start),
                        end  : new Date(action.data.end)
                    }
                }
            };
        }
        case Actions.CLOSE_EDIT_EVENT_DIALOG:
        {
            return {
                ...state,
                eventDialog: {
                    type : 'edit',
                    props: {
                        open: false
                    },
                    data : null
                }
            };
            }
        case Actions.SET_TASK_IMAGE:
            {
                return Object.assign({}, state, {
                    image: action.payload
                })
            }
        default:
        {
            return state;
        }
    }
};

export default eventsReducer;
