import CustomerOverview from './CustomerOverview';
import React from "react";
import authRoles from "../../auth/authRoles";

export const CustomerOverviewConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    auth    : authRoles.admin,
    routes  : [
        {
            path: '/customer/create',
            component   : React.lazy(() => import('./CreateCustomer')) // Customer before
        },
        {
            path        : '/customer/:CustomerId',
            component   : React.lazy(() => import('./Customer')) // Customer before
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
