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
            path        : '/vehicle/:VehicleId',
            component   : React.lazy(() => import('./Vehicle'))
        }
    ]
};

