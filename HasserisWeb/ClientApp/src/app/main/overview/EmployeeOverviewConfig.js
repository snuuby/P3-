import EmployeeOverview from './EmployeeOverview';
import React from "react";
import authRoles from "../../auth/authRoles";

export const EmployeeOverviewConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    auth    : authRoles.adminPlus,
    routes  : [
        {
            path     : '/employee/overview',
            component: React.lazy(() => import('./EmployeeOverview'))
        },
        {
            path        : '/employee/:EmployeeId',
            component   : React.lazy(() => import('./Employee'))
        }
    ]
};


