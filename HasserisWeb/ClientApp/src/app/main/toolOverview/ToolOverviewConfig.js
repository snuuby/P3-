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
    ]
};