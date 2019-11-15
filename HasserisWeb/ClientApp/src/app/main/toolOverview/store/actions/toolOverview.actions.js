import axios from 'axios';
import {
    ADD_EVENT,
    CLOSE_NEW_EVENT_DIALOG,
    GET_EVENTS,
    getEvents,
    OPEN_NEW_EVENT_DIALOG
} from "../../../apps/calendar/store/actions";

export const GET_TOOLS = '[TOOL APP] GET TOOLS';
export const SET_TOOLOVERVIEW_SEARCH_TEXT = '[TOOL APP] SET TOOL SEARCH TEXT';
export const ADD_TOOL = '[TOOL APP] ADD TOOL';
export const OPEN_NEW_ADD_DIALOG = '[TOOL APP] OPEN NEW ADD DIALOG';
export const CLOSE_NEW_ADD_DIALOG = '[TOOL APP] CLOSE NEW ADD DIALOG';
export const GET_TOOL = '[TOOL APP] GET TOOL';
export const CLOSE_EDIT_TOOL_DIALOG = '[TOOL APP]';
export const REMOVE_TOOL = '[TOOL APP] REMOVE TOOL';
export const UPDATE_TOOLS = '[TOOL APP] UPDATE TOOL'

// Gets all tools
export function getTools()
{
    const request = axios.get('Tools/all');
    request.then(response => console.log(response.data));
    
    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type   : GET_TOOLS,
                payload: response.data
            })
        );
}

// Get specific tool
export function getTool(params) {
    const request = axios.get("tools/" + params.ToolId);
    request.then(response => console.log(response.data));

    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type: GET_TOOL,
                payload: response.data
            })
        );
}
// Is required for the SearchText
export function setToolOverviewSearchText(event)
{
    return {
        type      : SET_TOOLOVERVIEW_SEARCH_TEXT,
        searchText: event.target.value
    }
}

// Action to add tool
export function addTool(newTool) {
    return (dispatch, getState) => {

        const request = axios.post('Tools/add', {
            newTool
        });

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: ADD_TOOL
                })
            ]).then(() => dispatch(getTools()))
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

export function closeEditToolDialog() {
    return {
        type: CLOSE_EDIT_TOOL_DIALOG
    }
}

export function removeTool(toolId) {
    return (dispatch, getState) => {

        const request = axios.post('Tools/remove', {
            toolId
        });

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: REMOVE_TOOL
                })
            ]).then(() => dispatch(getTools()))
        );
    };
}

export function updateTool(newEvent) {
    return (dispatch, getState) => {

        const request = axios.post('Tools/update', {
            newEvent
        });

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: UPDATE_TOOLS
                })
            ]).then(() => dispatch(getEvents()))
        );
    };
}