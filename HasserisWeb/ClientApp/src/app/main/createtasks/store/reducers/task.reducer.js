import * as Actions from '../actions';

const initialState = {
    entities   : [],
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

const taskReducer = function (state = initialState, action) {
    switch (action.type) {
        
        
        default: {
            return state;
        }    
        
        
        
        
        
    }

}
export default taskReducer;