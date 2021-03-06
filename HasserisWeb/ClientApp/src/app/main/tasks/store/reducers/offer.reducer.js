import * as Actions from '../actions';
import { arrowFunctionExpression } from '@babel/types';
import moment from 'moment';

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
                if (!Array.isArray(action.payload)) {
                    return {
                        ...state,
                        offers: { ...action.payload },
                    };
                }
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
                            WasInspection: action.payload.WasInspection,
                            InspectionReportID: action.payload.WasInspection ? action.payload.InspectionReportID : null,
                            WasOffer: action.payload.WasOffer,
                            WithPacking: action.payload.WithPacking,
                            StartAddress: action.payload.StartingAddress.LivingAddress,
                            StartZIP: action.payload.StartingAddress.ZIP,
                            StartCity: action.payload.StartingAddress.City,
                            DestinationAddress: action.payload.Destination.LivingAddress,
                            DestinationZIP: action.payload.Destination.ZIP,
                            DestinationCity: action.payload.Destination.City,
                            Customer: action.payload.Customer,
                            CustomerID: action.payload.Customer.ID,
                            CustomerName: action.payload.Customer.CustomerType == "Private" ? action.payload.Customer.Firstname + ' ' + action.payload.Customer.Lastname : action.payload.Customer.Name,
                            CustomerMail: action.payload.Customer.ContactInfo.Email,
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
                            WasInspection: false,
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
                            CustomerName: action.payload.Customer.CustomerType == "Private" ? action.payload.Customer.Firstname + ' ' + action.payload.Customer.Lastname : action.payload.Customer.Name,
                            CustomerMail: action.payload.Customer.ContactInfo.Email,
                            CustomerID: action.payload.Customer.ID,
                            InspectionReportID: action.payload.ID,
                            WasInspection: true,
                            ExpectedHours: 0, 
                            Lentboxes: 0,
                            WithPacking: true,
                            ExpirationDate: moment(action.payload.MovingDate).add(14, 'days').
                            format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS),
                                            
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
            case Actions.SAVE_EDIT_OFFER:
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