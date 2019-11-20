import InspectionReport from './InspectionReport';
import React from "react";
import authRoles from "../../auth/authRoles";

export const MakeTaskConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    auth: authRoles.admin, auth: authRoles.adminPlus,
    routes  : [
        {
            path     : '/InspectionReport/Make',
            component: React.lazy(() => import('./InspectionReport'))
        },
        {
            path: '/Offer/Make',
            component: React.lazy(() => import('./Offer'))
        },
        {
            path: '/Task/Make',
            component: React.lazy(() => import('./Task'))
        },
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
