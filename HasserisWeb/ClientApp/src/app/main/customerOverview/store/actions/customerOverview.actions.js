import axios from 'axios';
import {
    ADD_EVENT,
    CLOSE_NEW_EVENT_DIALOG,
    GET_EVENTS,
    getEvents,
    OPEN_NEW_EVENT_DIALOG
} from "../../../apps/calendar/store/actions";

export const GET_CUSTOMERS = '[CUSTOMER APP] GET CUSTOMERS';
export const SET_CUSTOMEROVERVIEW_SEARCH_TEXT = '[CUSTOMER APP] SET OVERVIEW SEARCH TEXT';
export const ADD_CUSTOMER = '[CUSTOMER APP] ADD CUSTOMER';
export const OPEN_NEW_ADD_DIALOG = '[CUSTOMER APP] OPEN NEW ADD DIALOG';
export const CLOSE_NEW_ADD_DIALOG = '[CUSTOMER APP] CLOSE NEW ADD DIALOG';

// Gets all customer
export function getCustomers()
{
    const request = axios.get('customers/all');
    request.then(response => console.log(response.data));
    
    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type: GET_CUSTOMERS,
                payload: response.data
            })
        );
}

// Is required for the SearchText
export function setCustomerOverviewSearchText(event)
{
    return {
        type: SET_CUSTOMEROVERVIEW_SEARCH_TEXT,
        searchText: event.target.value
    }
}

// Action to add customer
export function addCustomer(newEvent)
{
    return (dispatch, getState) => {

        const request = axios.post('/api/calendar-app/add-event', {
            newEvent
        });

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: ADD_EVENT
                })
            ]).then(() => dispatch(getCustomers()))
        );
    };
}

export function openNewAddDialog(data)
{
    return {
        type: OPEN_NEW_ADD_DIALOG,
        data
    }
}


export function closeNewAddDialog()
{
    return {
        type: CLOSE_NEW_ADD_DIALOG
    }
}