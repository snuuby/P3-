import React from "react";
import authRoles from "../../../auth/authRoles";

export const InspectionsOverviewConfig = {
    settings: {
        layout: {s
            config: {}
        }
    },
    auth    : authRoles.employee,
    routes  : [
        
        {
            path: '/inspections/overview',
            component: React.lazy(() => import('./InspectionsOverview'))
        },
{
    path        : '/inspections/:InspectionId',
        component   : React.lazy(() => import('./InspectionReport'))
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
