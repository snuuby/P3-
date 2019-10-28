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

// Gets all vehicles
export function getVehicles()
{
    const request = axios.get('Vehicle/all');
    request.then(response => console.log(response.data));
    
    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type: GET_VEHICLES,
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

// Action to add vehicle
export function addVehicle(newEvent)
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
            ]).then(() => dispatch(getVehicles()))
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