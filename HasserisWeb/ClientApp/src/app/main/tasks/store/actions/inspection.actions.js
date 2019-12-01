import axios from 'axios';
export const OPEN_NEW_INSPECTION_REPORT = '[INSPECTION REPORT] OPEN NEW INSPECTION REPORT';
export const CLOSE_NEW_INSPECTION_REPORT = '[INSPECTION REPORT] CLOSE NEW INSPECTION REPORT';
export const SAVE_INSPECTION_REPORT = '[INSPECTION REPORT] SAVE INSPECTION REPORT';
export const SAVE_EDIT_INSPECTION_REPORT = '[INSPECTION REPORT] SAVE EDIT INSPECTION REPORT';

export const GET_AVAILABLE_EMPLOYEES = '[INSPECTION REPORT] GET AVAILABLE EMPLOYEES';
export const GET_AVAILABLE_CARS = '[INSPECTION REPORT] GET AVAILABLE CARS';
export const GET_CUSTOMERS = '[INSPECTION REPORT] GET CUSTOMERS';
export const GET_INSPECTION_REPORT = '[INSPECTION REPORT} GET INSPECTION REPORT';
export const GET_ALL_INSPECTION_REPORT = '[INSPECTION REPORT} GET ALL INSPECTION REPORT';
export const SET_INSPECTIONOVERVIEW_SEARCH_TEXT = '[INSPECTION REPORT] SET OVERVIEW SEARCH TEXT';


export function setInspectionOverviewSearchText(event) {
    return {
        type: SET_INSPECTIONOVERVIEW_SEARCH_TEXT,
        searchText: event.target.value
    }
}

export function openNewInspectionReport(data) {
    return {
        type: OPEN_NEW_INSPECTION_REPORT,
        data,
    }
}

export function getInspectionReport(params)
{
    const request = axios.get('inspection/' + params.InspectionId);
    return (dispatch) => request.then((response) =>
        Promise.all([
            dispatch({
                type: GET_INSPECTION_REPORT,
                payload: response.data,
            })
        ])
        );
}
export function getAllInspectionReports()
{
    const request = axios.get('inspection/getall');

    return (dispatch) => request.then((response) =>
        Promise.all([
            dispatch({
                type: GET_ALL_INSPECTION_REPORT,
                payload: response.data,
            })
        ]));
}
export function closeNewInspectionReport() {
    return {
        type: CLOSE_NEW_INSPECTION_REPORT
    }
}
export function getAvailableEmployees() {
    const request = axios.get('employees/available');
    request.then(response => console.log(response.data));

    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type: GET_AVAILABLE_EMPLOYEES,
                payload: response.data
            })
        );
}
export function getCustomers() {
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
export function getAvailableCars() {
    const request = axios.get('Vehicles/available');
    request.then(response => console.log(response.data));

    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type: GET_AVAILABLE_CARS,
                payload: response.data
            })
        );
}
export function addInspectionReport(report) {
    return (dispatch, getState) => {
        const request = axios.post('inspection/make', report);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: SAVE_INSPECTION_REPORT
                })
            ]).then(() => console.log("IMPLEMENT PUSH TO OVERVIEW")
            ));
    }
}
export function editInspectionReport(report) {
    return (dispatch, getState) => {
        console.log("a");
        const request = axios.post('inspection/edit', report);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: SAVE_EDIT_INSPECTION_REPORT
                })
            ]).then(() => console.log("IMPLEMENT PUSH TO OVERVIEW")
            ));
    }
}

