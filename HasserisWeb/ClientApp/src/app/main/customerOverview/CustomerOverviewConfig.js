import CustomerOverview from './CustomerOverview';
import React from "react";
import authRoles from "../../auth/authRoles";

export const CustomerOverviewConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    auth    : authRoles.adminPlus,
    routes  : [
        {
            path     : '/customer/overview',
            component: React.lazy(() => import('./CustomerOverview'))
        },
        {
            path        : '/customer/:CustomerId',
            component   : React.lazy(() => import('./Customer'))
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
