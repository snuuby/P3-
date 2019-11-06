import * as Actions from '../actions';

const initialState = {
    entities   : [],
    eventDialog: {
        type : 'new',
        props: {
            open: false
        },
        image: 'assets/images/tasks/placeholder.png',
        data : null
    }
};

const eventsReducer = function (state = initialState, action) {
    switch ( action.type )
    {
        case Actions.GET_EVENTS:
        {

            const entities = action.payload.map((event) => (
                {
                    ...event,

                    id: event.ID,
                    title : event.Name,
                    desc: event.Description,
                    start: new Date(event.Dates[0].Date),
                    end  : new Date(event.Dates[1].Date)
                }
                
            ));

            return {
                ...state,
                entities,
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