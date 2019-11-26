import * as Actions from '../actions';

const initialState = {
    offers: [],
    customers: [],
    searchText: '',
    eventDialog: {
        type: 'new',
        made: false,
        image: '',
        props: {
            open: true,
        },
        data: {
            inspectionReport: null,
            wasInspection: false, 
        },
    }
};

const offerReducer = function (state = initialState, action) {
    switch (action.type) {
        case Actions.SET_OFFER_SEARCH_TEXT:
            {
                return {
                    ...state,
                    searchText: action.searchText
                };
            }
        case Actions.GET_ALL_OFFERS:
            {
                const offers = action.payload.map((offer) => (
                    {
                        ...offer
                    }
                ));

                return {
                    ...state,
                    offers
                };
            }

        case Actions.GET_OFFER:
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
                            ...action.payload
                        }
                    }
                };
            }
        case Actions.OPEN_NEW_OFFER:
            {
                return {
                    ...state,
                    eventDialog: {
                        type: 'new',
                        props: {
                            open: true
                        },
                        data: {
                            wasInspection: false,
                            ...action.data
                        }
                    }
                };
            }
        case Actions.INSPECTION_TO_OFFER:
            {
                return {
                    ...state,
                    eventDialog: {
                        type: 'new',
                        props: {
                            open: true
                        },
                        data: {
                            InspectionReport: action.payload.ID,
                            wasInspection: true,
                            ...action.payload
                        }
                    }
                };
            }
        case Actions.CLOSE_NEW_OFFER:
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
       

        case Actions.GET_CUSTOMERS:
            {
                return Object.assign({}, state, {
                    customers: action.payload
                })
            }
        case Actions.SAVE_OFFER:
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
export default offerReducer;