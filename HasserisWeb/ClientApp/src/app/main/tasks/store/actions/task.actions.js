import axios from 'axios';
export const ADD_MOVING_TASK = '[TASK] ADD MOVING TASK';
export const SAVE_EDIT_MOVING_TASK= '[TASK] SAVE EDIT MOVING TASK';
export const ADD_DELIVERY_TASK = '[TASK] ADD DELIVERY TASK';
export const SAVE_EDIT_DELIVERY_TASK = '[TASK] SAVE EDIT DELIVERY TASK';
export const GET_AVAILABLE_EMPLOYEES = '[TASK] GET AVAILABLE EMPLOYEES';
export const GET_AVAILABLE_CARS = '[TASK] GET AVAILABLE CARS';
export const GET_AVAILABLE_TOOLS = '[TASK] GET AVAILABLE CARS';

export const GET_CUSTOMERS = '[TASK] GET CUSTOMERS';
export const GET_TASK = '[TASK] GET TASK';
export const INSPECTION_TO_TASK = '[TASK] COPY DATA FROM INSPECTION REPORT TO TASK';
export const OFFER_TO_TASK = '[TASK] COPY DATA FROM OFFER TO TASK';

export const GET_ALL_MOVING_TASKS = '[TASK] GET ALL MOVING TASKS';
export const GET_ALL_DELIVERY_TASKS = '[TASK] GET ALL DELIVERY TASKS';

export const SET_TASKOVERVIEW_SEARCH_TEXT = '[TASK] SET OVERVIEW SEARCH TEXT';


export function setTaskOverviewSearchText(event) {
    return {
        type: SET_TASKOVERVIEW_SEARCH_TEXT,
        searchText: event.target.value
    }
}
export function transferInspectionInfoToTask(report) {
    return (dispatch) => 
        dispatch({
            type: INSPECTION_TO_TASK,
            payload: report,
        })
    
}
export function transferOfferInfoToTask(offer) {
    return (dispatch) => 
        dispatch({
            type: OFFER_TO_TASK,
            payload: offer,
        })
    
}
export function addTaskFromInspectionReport(report) {

    const request = axios.post('task/create/moving/from/inspection', report);
    return (dispatch) => request.then((response) =>
        dispatch({
            type: INSPECTION_TO_TASK,
            payload: report,
        })
    );
}
export function addTaskFromOffer(offer) {

    const request = axios.post('task/create/moving/from/offer', offer);
    return (dispatch) => request.then((response) =>
        dispatch({
            type: OFFER_TO_TASK,
            payload: offer,
        })
    );
}
export function getTask(params)
{
    const request = axios.get('task/' + params.TaskId);
    return (dispatch) => request.then((response) =>
        Promise.all([
            dispatch({
                type: GET_TASK,
                payload: response.data,
            })
        ])
        );
}
export function getAllTasks()
{

    return (dispatch) => {
        dispatch(getMovingTasks());
        dispatch(getDeliveryTasks());
    }
}
export function getMovingTasks() {
    const request = axios.get('task/get/moving');
    console.log(request);
    return (dispatch) => request.then((response) =>
            dispatch({
                type: GET_ALL_MOVING_TASKS,
                payload: response.data,
            })
        );
}
export function getDeliveryTasks() {
    const request = axios.get('task/get/delivery');
    request.then(response => console.log(response.data));

    console.log(request);
    return (dispatch) => request.then((response) =>
            dispatch({
                type: GET_ALL_DELIVERY_TASKS,
                payload: response.data,
            })
        );
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
export function getAvailableTools() {
    const request = axios.get('Tools/available');
    request.then(response => console.log(response.data));

    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type: GET_AVAILABLE_TOOLS,
                payload: response.data
            })
        );
}
export function addMovingTask(moving) {
    return (dispatch, getState) => {
        const request = axios.post('task/create/moving', moving);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: ADD_MOVING_TASK
                })
            ])
            );
    }
}
export function addDeliveryTask(delivery) {
    return (dispatch, getState) => {
        const request = axios.post('task/create/delivery', delivery);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: ADD_DELIVERY_TASK
                })
            ])
        );
    }
}
export function editDeliveryTask(delivery) {
    return (dispatch, getState) => {
        const request = axios.post('task/edit/delivery', delivery);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: SAVE_EDIT_DELIVERY_TASK
                })
            ])
            );
    }
}
export function editMovingTask(moving) {
    return (dispatch, getState) => {
        const request = axios.post('task/edit/moving', moving);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: SAVE_EDIT_MOVING_TASK
                })
            ])
            );
    }
}

