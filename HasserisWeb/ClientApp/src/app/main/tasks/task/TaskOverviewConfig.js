import TaskOverview from './TaskOverview';
import React from "react";
import authRoles from "../../../auth/authRoles";

export const TaskOverviewConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    auth    : authRoles.employee,
    routes  : [
        {
            path     : '/tasks/overview',
            component: React.lazy(() => import('./TaskOverview'))
        },
        {
            path        : '/tasks/:TaskId',
            component   : React.lazy(() => import('./Task')) // 
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
