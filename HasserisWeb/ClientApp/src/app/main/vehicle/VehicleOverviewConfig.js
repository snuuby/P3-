import VehicleOverview from './VehicleOverview';
import React from "react";
import authRoles from "../../auth/authRoles";

export const VehicleOverviewConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    auth    : authRoles.adminPlus,
    routes  : [
        {
            path     : '/vehicle/overview',
            component: React.lazy(() => import('./VehicleOverview'))
        },
        {
            path: '/vehicle/Create',
            component: React.lazy(() => import('./CreateVehicle'))
        },
        {
            path        : '/vehicle/:VehicleId',
            component   : React.lazy(() => import('./Vehicle'))
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
