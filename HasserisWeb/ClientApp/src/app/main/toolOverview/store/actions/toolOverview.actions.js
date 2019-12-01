import axios from 'axios';
import {
    ADD_EVENT,
    CLOSE_NEW_EVENT_DIALOG,
    GET_EVENTS,
    getEvents,
    OPEN_NEW_EVENT_DIALOG
} from "../../../apps/calendar/store/actions";

export const GET_TOOLS = '[TOOL] GET TOOLS';
export const SET_TOOLOVERVIEW_SEARCH_TEXT = '[TOOL] SET TOOL SEARCH TEXT';
export const ADD_TOOL = '[TOOL] ADD TOOL';
export const OPEN_NEW_ADD_DIALOG = '[TOOL] OPEN NEW ADD DIALOG';
export const CLOSE_NEW_ADD_DIALOG = '[TOOL] CLOSE NEW ADD DIALOG';
export const GET_TOOL = '[TOOL] GET TOOL';
export const CLOSE_EDIT_TOOL_DIALOG = '[TOOL]';
export const REMOVE_TOOL = '[TOOL] REMOVE TOOL';
export const EDIT_TOOLS = '[TOOL] UPDATE TOOL'

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
export function addTool(tool) {
    return (dispatch, getState) => {

        const request = axios.post('tool/add', tool);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: ADD_TOOL
                })
            ])
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
            ])
        );
    };
}

export function editTool(tool) {
    return (dispatch, getState) => {

        const request = axios.post('Tools/edit', tool);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: EDIT_TOOLS
                })
            ])
        );
    };
}