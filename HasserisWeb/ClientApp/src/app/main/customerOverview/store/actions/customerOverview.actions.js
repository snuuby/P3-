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
export const ADD_PRIVATE_CUSTOMER = '[CUSTOMER APP] ADD PRIVATE CUSTOMER';
export const ADD_PUBLIC_CUSTOMER = '[CUSTOMER APP] ADD PUBLIC CUSTOMER';
export const ADD_BUSINESS_CUSTOMER = '[CUSTOMER APP] ADD BUSINESS CUSTOMER';

export const OPEN_NEW_ADD_DIALOG = '[CUSTOMER APP] OPEN NEW ADD DIALOG';
export const CLOSE_NEW_ADD_DIALOG = '[CUSTOMER APP] CLOSE NEW ADD DIALOG';
export const GET_PRIVATE_CUSTOMERS = '[CUSTOMER APP] GET PRIVATE CUSTOMERS';
export const GET_BUSINESS_CUSTOMERS = '[CUSTOMER APP] GET BUSINESS CUSTOMERS';
export const GET_PUBLIC_CUSTOMERS = '[CUSTOMER APP] GET PUBLIC CUSTOMERS';
export const GET_CUSTOMER = '[CUSTOMER APP] GET CUSTOMER';
export const CLOSE_EDIT_CUSTOMER_DIALOG = '[CUSTOMER APP]';
export const REMOVE_CUSTOMER = '[CUSTOMER APP] REMOVE CUSTOMER';
export const UPDATE_CUSTOMERS = '[CUSTOMER APP] UPDATE CUSTOMER'

export function getCustomers() {
    return (dispatch) => {
        dispatch(getPrivateCustomers());
        dispatch(getPublicCustomers());
        dispatch(getBusinessCustomers());
    }

}
export function getCustomer(customerID) {
    const request = axios.get('customers/' + customerID);
    request.then(response => {
        console.log(response.data);
    });

    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type: GET_CUSTOMER,
                payload: response.data,
            })
        );
}
export function getPrivateCustomers() {
    const request = axios.get('customers/private');
    request.then(response => {
        console.log(response.data);
    });

    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type: GET_PRIVATE_CUSTOMERS,
                payload: response.data
            })
        );
}
export function getBusinessCustomers() {
    const request = axios.get('customers/business');
    request.then(response => {
        console.log(response.data);
    });

    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type: GET_BUSINESS_CUSTOMERS,
                payload: response.data
            })
        );
}
export function getPublicCustomers() {
    const request = axios.get('customers/public');
    request.then(response => {
        console.log(response.data);
    });

    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type: GET_PUBLIC_CUSTOMERS,
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
export function addPublicCustomer(customer)
{
    return (dispatch, getState) => {


        // 404 kan ikke finde url
        const request = axios.post('customers/addpublic', customer);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: ADD_PUBLIC_CUSTOMER
                })
            ]).then(() => { dispatch(getPrivateCustomers()); dispatch(getPublicCustomers); dispatch(getBusinessCustomers) })
        );
    };
}
export function addPrivateCustomer(customer) {
    return (dispatch, getState) => {


        // 404 kan ikke finde url
        const request = axios.post('customers/addprivate', customer);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: ADD_PRIVATE_CUSTOMER
                })
            ]).then(() => { dispatch(getPrivateCustomers()); dispatch(getPublicCustomers); dispatch(getBusinessCustomers) })
        );
    };
}
export function addBusinessCustomer(customer) {
    return (dispatch, getState) => {


        // 404 kan ikke finde url
        const request = axios.post('customers/addbusiness', customer);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: ADD_BUSINESS_CUSTOMER
                })
            ]).then(() => { dispatch(getPrivateCustomers()); dispatch(getPublicCustomers); dispatch(getBusinessCustomers) })
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

export function closeEditCustomerDialog() {
    return {
        type: CLOSE_EDIT_CUSTOMER_DIALOG
    }
}

export function removeCustomer(customerId) {
    return (dispatch, getState) => {

        const request = axios.post('Customer/remove', {
            customerId
        });

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: REMOVE_CUSTOMER
                })
            ]).then(() => { dispatch(getPrivateCustomers()); dispatch(getPublicCustomers); dispatch(getBusinessCustomers) })
        );
    };
}

export function updateCustomers(newEvent) {
    return (dispatch, getState) => {

        const request = axios.post('Customer/update', {
            newEvent
        });

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: UPDATE_CUSTOMERS
                })
            ]).then(() => dispatch(getCustomer()))
        );
    };
}