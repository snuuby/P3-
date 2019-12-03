import axios from 'axios';
export const OPEN_NEW_OFFER = '[OFFER] OPEN NEW OFFER';
export const CLOSE_NEW_OFFER = '[OFFER] CLOSE NEW OFFER';
export const SAVE_OFFER = '[OFFER] SAVE OFFER';
export const SAVE_EDIT_OFFER = '[OFFER] SAVE EDIT OFFER';
export const GET_CUSTOMERS = '[OFFER] GET CUSTOMERS';
export const GET_OFFER = '[OFFER} GET OFFER';
export const GET_ALL_OFFERS = '[OFFER} GET ALL OFFERS';
export const SET_OFFER_SEARCH_TEXT = '[OFFER] SET OVERVIEW SEARCH TEXT';
export const INSPECTION_TO_OFFER = '[OFFER] COPY DATA FROM INSPECTION REPORT TO OFFER';



export function setOfferSearchText(event) {
    return {
        type: SET_OFFER_SEARCH_TEXT,
        searchText: event.target.value
    }
}

export function openNewOffer(data) {
    return {
        type: OPEN_NEW_OFFER,
        data,
    }
}
export function addFromInspectionReport(data)
{
    return {
        type: INSPECTION_TO_OFFER,
        payload: data,
    }
}
export function getOffer(params) {
    const request = axios.get('offer/' + params.OfferId);

    return (dispatch) => request.then((response) =>
        Promise.all([
            dispatch({
                type: GET_OFFER,
                payload: response.data,
            })
        ])
    );
}
export function getAllOffers() {
    const request = axios.get('offer/getall');

    return (dispatch) => request.then((response) =>
        Promise.all([
            dispatch({
                type: GET_ALL_OFFERS,
                payload: response.data,
            })
        ]));
}
export function closeNewOffer() {
    return {
        type: CLOSE_NEW_OFFER
    }
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

export function addOffer(offer) {
    return (dispatch, getState) => {

        const request = axios.post('offer/makenew', offer);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: SAVE_OFFER
                })
            ]).then(() => console.log("IMPLEMENT PUSH TO OVERVIEW")
            ));
    }
}
export function addOfferFromInspection(offer) {
    return (dispatch, getState) => {

        const request = axios.post('offer/makefrominspection', offer);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: SAVE_OFFER
                })
            ]).then(() => console.log("IMPLEMENT PUSH TO OVERVIEW")
            ));
    }
}
export function editOffer(report) {
    return (dispatch, getState) => {
        const request = axios.post('Offer/edit', report);

        return request.then((response) =>
            Promise.all([
                dispatch({
                    type: SAVE_EDIT_OFFER
                })
            ]).then(() => console.log("IMPLEMENT PUSH TO OVERVIEW")
            ));
    }
}
