import axios from 'axios';
import {
    ADD_EVENT,
    CLOSE_NEW_EVENT_DIALOG,
    GET_EVENTS,
    getEvents,
    OPEN_NEW_EVENT_DIALOG
} from "../../../apps/calendar/store/actions";

export const GET_EMPLOYEES = '[EMPLOYEE APP] GET EMPLOYEES';
export const SET_OVERVIEW_SEARCH_TEXT = '[EMPLOYEE APP] SET OVERVIEW SEARCH TEXT';
export const ADD_EMPLOYEE = '[EMPLOYEE APP] ADD EMPLOYEE';
export const OPEN_NEW_ADD_DIALOG = '[EMPLOYEE APP] OPEN NEW ADD DIALOG';
export const CLOSE_NEW_ADD_DIALOG = '[EMPLOYEE APP] CLOSE NEW ADD DIALOG';
export const GET_EMPLOYEE = '[EMPLOYEE APP] GET SPECIFIC EMPLOYEE';


// Gets all employees
export function getEmployees()
{
    const request = axios.get('employees/all');
    request.then(response => console.log(response.data));
    
    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type   : GET_EMPLOYEES,
                payload: response.data
            })
        );
}

export function getEmployee(params) {
    const request = axios.get("employees/" + params.EmployeeId);
    request.then(response => console.log(response.data));

    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type: GET_EMPLOYEE,
                payload: response.data
            })
        );
}

// Is required for the SearchText
export function setOverviewSearchText(event)
{
    return {
        type      : SET_OVERVIEW_SEARCH_TEXT,
        searchText: event.target.value
    }
}

// Action to add employees
export function addEmployee(newEvent)
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
            ]).then(() => dispatch(getEmployees()))
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