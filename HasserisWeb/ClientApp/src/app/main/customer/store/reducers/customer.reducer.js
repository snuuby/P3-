import * as Actions from '../actions';

const initialState = {
    privateCustomers: [],
    businessCustomers: [],
    publicCustomers: [],
    searchText: '',
    loading: true,
    eventDialog: {
        type: 'new',
        editted: false,
        props: {
            open: false
        },
        data : null
    }
};

const customerReducer = function (state = initialState, action) {
    switch (action.type) {

        case Actions.GET_PRIVATE_CUSTOMERS:
            {
                if (!Array.isArray(action.payload)) {
                    return {
                        ...state,
                        privateCustomers: { CustomerType: "Private", ...action.payload },
                    };
                }
            const privateCustomers = action.payload.map((customers) => (
                {
                    CustomerType: "Private",
                    ...customers
                }
            ));

            return {
                ...state,
                privateCustomers
            };
            }
        case Actions.GET_BUSINESS_CUSTOMERS:
            {
                if (!Array.isArray(action.payload)) {
                    return {
                        ...state,
                        businessCustomers: { CustomerType: "Business",...action.payload },
                    };
                }
                const businessCustomers = action.payload.map((customers) => (
                    {
                        CustomerType: "Business",
                        ...customers
                    }
                ));

                return {
                    ...state,
                    businessCustomers
                };
            }
        case Actions.GET_PUBLIC_CUSTOMERS:
            {
                if (!Array.isArray(action.payload)) {
                    return {
                        ...state,
                        publicCustomers: { CustomerType: "Public", ...action.payload },
                    };
                }
                const publicCustomers = action.payload.map((customers) => (
                    {
                        CustomerType: "Public",

                        ...customers
                    }
                ));

                return {
                    ...state,
                    publicCustomers
                };
            }

        case Actions.GET_CUSTOMER:
            {
                return Object.assign({}, state, {
                    eventDialog: {
                        type: 'new',
                        props: {
                            open: true,
                        },
                        data: {
                            LivingAddress: action.payload.Address.LivingAddress,
                            ZIP: action.payload.Address.ZIP,
                            City: action.payload.Address.City,
                            Email: action.payload.ContactInfo.Email,
                            Phonenumber: action.payload.ContactInfo.PhoneNumber,

                            ...action.payload ,
                        },
                    }
                })
            }

        case Actions.SET_CUSTOMEROVERVIEW_SEARCH_TEXT:{
            return {
                ...state,
                searchText: action.searchText
            };
        }

        case Actions.ADD_PRIVATE_CUSTOMER:
        {
            return {
                ...state
            };
        }
        case Actions.ADD_PUBLIC_CUSTOMER:
            {
                return {
                    ...state
                };
            }
        case Actions.ADD_BUSINESS_CUSTOMER:
            {
                return {
                    ...state
                };
            }
        case Actions.EDIT_CUSTOMER:
            {
                return {
                    ...state,
                    editted: true,
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
                        ...action.payload
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
export default customerReducer;
