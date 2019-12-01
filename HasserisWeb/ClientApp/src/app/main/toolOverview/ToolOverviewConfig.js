import ToolOverview from './ToolOverview';
import React from "react";
import authRoles from "../../auth/authRoles";

export const ToolOverviewConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    auth    : authRoles.adminPlus,
    routes  : [
        {
            path     : '/tool/overview',
            component: React.lazy(() => import('./ToolOverview'))
        },
        {
            path: '/tool/:ToolId',
            component: React.lazy(() => import('./Tool'))
        }
        ,
        {
            path: '/tool/create',
            component: React.lazy(() => import('./CreateTool'))
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
