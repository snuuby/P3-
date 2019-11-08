import axios from 'axios';

export const GET_EVENTS = '[CALENDAR APP] GET EVENTS';
export const OPEN_NEW_EVENT_DIALOG = '[CALENDAR APP] OPEN NEW EVENT DIALOG';
export const CLOSE_NEW_EVENT_DIALOG = '[CALENDAR APP] CLOSE NEW EVENT DIALOG';
export const OPEN_EDIT_EVENT_DIALOG = '[CALENDAR APP] OPEN EDIT EVENT DIALOG';
export const CLOSE_EDIT_EVENT_DIALOG = '[CALENDAR APP] CLOSE EDIT EVENT DIALOG';
export const ADD_EVENT = '[CALENDAR APP] ADD EVENT';
export const UPDATE_EVENT = '[CALENDAR APP] UPDATE EVENT';
export const REMOVE_EVENT = '[CALENDAR APP] REMOVE EVENT';
export const SET_TASK_IMAGE = '[CALENDAR APP] SET IMAGE'

export function getEvents()
{
    const request = axios.get('calendar/all');

    return (dispatch) =>
        request.then((response) =>
            dispatch({
                type   : GET_EVENTS,
                payload: response.data
            })
        );
}


export function openNewEventDialog(data)
{
    return {
        type: OPEN_NEW_EVENT_DIALOG,
        data
    }
}

export function closeNewEventDialog()
{
    return {
        type: CLOSE_NEW_EVENT_DIALOG
    }
}

export function openEditEventDialog(data)
{
    return {
        type: OPEN_EDIT_EVENT_DIALOG,
        data
    }
}

export function closeEditEventDialog()
{
    return {
        type: CLOSE_EDIT_EVENT_DIALOG
    }
}


export function addEvent(newEvent)
{
    return (dispatch, getState) => {

        const request = axios.post('calendar/add', {
            newEvent
        });

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: ADD_EVENT
                })
            ]).then(() => dispatch(getEvents()))
        );
    };
}

export function updateEvent(newEvent)
{
    return (dispatch, getState) => {

        const request = axios.post('calendar/update', {
            newEvent
        });

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: UPDATE_EVENT
                })
            ]).then(() => dispatch(getEvents()))
        );
    };
}

export function removeEvent(eventId)
{
    return (dispatch, getState) => {

        const request = axios.post('calendar/remove', {
            eventId
        });

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: REMOVE_EVENT
                })
            ]).then(() => dispatch(getEvents()))
        );
    };
}

export function setTaskImage(imgUrl, taskid, location, type) {
    return (dispatch) => {
        getBase64Image(imgUrl, function (base64image) {
            const imageData = { base64URL: base64image, value: taskid, location: location, type: type };
            axios.post('/images/uploadImage', imageData).then(response => {
                const image = response.data;
                dispatch({
                    type: SET_TASK_IMAGE,
                    payload: image
                })
            });
            console.log(base64image);
        });


        /*
        Set User Image
         */

    }
    function getBase64Image(imgUrl, callback) {

        var img = new Image();

        // onload fires when the image is fully loadded, and has width and height

        img.onload = function () {

            var canvas = document.createElement("canvas");
            canvas.width = img.width;
            canvas.height = img.height;
            var ctx = canvas.getContext("2d");
            ctx.drawImage(img, 0, 0);
            var dataURL = canvas.toDataURL("image/png"),
                dataURL = dataURL.replace(/^data:image\/(png|jpg);base64,/, "");

            callback(dataURL); // the base64 string

        };

        // set attributes and src 
        img.setAttribute('crossOrigin', 'anonymous'); //
        img.src = imgUrl;

    }
}
