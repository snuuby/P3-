import React from "react";
import authRoles from "../../../auth/authRoles";

export const OffersOverviewConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    auth    : authRoles.employee,
    routes  : [
        {
            path: '/offers/overview',
            component: React.lazy(() => import('./OffersOverview'))
        },
{
    path        : '/offers/:OfferId',
        component   : React.lazy(() => import('./Offer'))
}
    ]
};

/**
 * Lazy load Example
 */
/*
import React from 'react';

export const ExampleConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    routes  : [
        {
            path     : '/example',
            component: React.lazy(() => import('./Example'))
        }
    ]
};
*/
