import axios from 'axios';
import {
    ADD_EVENT,
    CLOSE_NEW_EVENT_DIALOG,
    GET_EVENTS,
    getEvents,
    OPEN_NEW_EVENT_DIALOG
} from "../../../apps/calendar/store/actions";

export const GET_VEHICLES = '[VEHCILE APP] GET VEHICLES';
export const SET_VEHICLEOVERVIEW_SEARCH_TEXT = '[VEHICLE APP] SET OVERVIEW SEARCH TEXT';
export const ADD_VEHICLE = '[VEHICLE APP] ADD VEHICLE';
export const OPEN_NEW_ADD_DIALOG = '[VEHICLE APP] OPEN NEW ADD DIALOG';
export const CLOSE_NEW_ADD_DIALOG = '[VEHICLE APP] CLOSE NEW ADD DIALOG';
export const GET_VEHICLE = '[VEHICLE APP] GET VEHICLE';
export const CLOSE_EDIT_VEHICLE_DIALOG = '[VEHICLE APP]';
export const REMOVE_VEHICLE = '[VEHICLE APP] REMOVE VEHICLE';
export const UPDATE_VEHICLES = '[VEHICLE APP] UPDATE VEHICLE';

// Gets all vehicles
export function getVehicles()
{
    const request = axios.get('Vehicles/all');
    request.then(response => console.log(response.data));
    
    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type: GET_VEHICLES,
                payload: response.data
            })
        );
}

//Get specific vehicle
export function getVehicle(params) {
    const request = axios.get("vehicles/" + params.VehicleId);
    request.then(response => console.log(response.data));

    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type: GET_VEHICLE,
                payload: response.data
            })
        );
}

// Is required for the SearchText
export function setVehicleOverviewSearchText(event)
{
    return {
        type      : SET_VEHICLEOVERVIEW_SEARCH_TEXT,
        searchText: event.target.value
    }
}

// Action to add tool
export function addVehicle(newVehicle) {
    return (dispatch, getState) => {

        const request = axios.post('Vehicles/add', {
            newVehicle
        });

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: ADD_VEHICLE
                })
            ]).then(() => dispatch(getVehicles()))
        );
    };
}

export function openNewAddDialog(data) {
    return {
        type: OPEN_NEW_ADD_DIALOG,
        data
    }
}


export function closeNewAddDialog() {
    return {
        type: CLOSE_NEW_ADD_DIALOG
    }
}

export function closeEditVehicleDialog() {
    return {
        type: CLOSE_EDIT_VEHICLE_DIALOG
    }
}

export function removeVehicle(vehicleId) {
    return (dispatch, getState) => {

        const request = axios.post('Vehicles/remove', {
            vehicleId
        });

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: REMOVE_VEHICLE
                })
            ]).then(() => dispatch(getVehicles()))
        );
    };
}

export function updateVehicles(newEvent) {
    return (dispatch, getState) => {

        const request = axios.post('Vehicles/update', {
            newEvent
        });

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: UPDATE_VEHICLES
                })
            ]).then(() => dispatch(getVehicles()))
        );
    };
}