import axios from 'axios';
export const OPEN_NEW_INSPECTION_REPORT = '[INSPECTION REPORT] OPEN NEW INSPECTION REPORT';
export const CLOSE_NEW_INSPECTION_REPORT = '[INSPECTION REPORT] CLOSE NEW INSPECTION REPORT';
export const SET_INSPECTION_REPORT_IMAGE = '[INSPECTION REPORT] SET INSPECTION REPORT IMAGE';
export const SAVE_INSPECTION_REPORT = '[INSPECTION REPORT] SAVE INSPECTION REPORT';
export const GET_AVAILABLE_EMPLOYEES = '[INSPECTION REPORT] GET AVAILABLE EMPLOYEES';
export const GET_AVAILABLE_CARS = '[INSPECTION REPORT] GET AVAILABLE CARS';
export const GET_CUSTOMERS = '[INSPECTION REPORT] GET CUSTOMERS';



export function openNewInspectionReport(data) {
    return {
        type: OPEN_NEW_INSPECTION_REPORT,
        data,
    }
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

        const request = axios.post('Task/MakeInspectionReport', report);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: SAVE_INSPECTION_REPORT
                })
            ]).then(() => console.log("IMPLEMENT PUSH TO OVERVIEW")
            ));
    }
}

export function setInspectionReportImage(imgUrl, taskid, location, type) {
    return (dispatch) => {
        getBase64Image(imgUrl, function (base64image) {
            const imageData = { base64URL: base64image, value: taskid, location: location, type: type };
            axios.post('/images/uploadImage', imageData).then(response => {
                const image = response.data;
                dispatch({
                    type: SET_INSPECTION_REPORT_IMAGE,
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