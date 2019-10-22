import axios from 'axios';
import {GET_EVENTS} from "../../../apps/calendar/store/actions";

export const GET_EMPLOYEES = '[EMPLOYEE APP] GET EMPLOYEES';

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
