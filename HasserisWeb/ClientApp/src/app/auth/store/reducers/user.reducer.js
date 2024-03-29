import * as Actions from '../actions';

const initialState = {
    role: [], //guest
    data: {
        'displayName': 'John Doe',
        'photoURL'   : 'assets/images/avatars/Velazquez.jpg',
        'email': 'johndoe@withinpixels.com',
        'firstName': '',
        'lastName': '',
        shortcuts    : [
            'calendar',
            'mail',
            'contacts',
            'todo'
        ]
    }
};

const user = function (state = initialState, action) {
    switch ( action.type )
    {
        case Actions.SET_USER_DATA:
        {
            return {
                ...initialState,
                ...action.payload,
            };
        }
        case Actions.REMOVE_USER_DATA:
        {
            return {
                ...initialState
            };
        }
        case Actions.USER_LOGGED_OUT:
        {
            return initialState;
            }
        case Actions.SET_USER_IMAGE:
            {
                return Object.assign({}, state, {
                     photoURL: action.payload

                })
            }

        default:
        {
            return state
        }
    }
};

export default user;
