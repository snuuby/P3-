import TaskOverview from './TaskOverview';
import React from "react";
import authRoles from "../../../auth/authRoles";

export const CreateTaskConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    auth    : authRoles.admin,
    routes  : [
        {
    path: '/tasks/create',
        component   : React.lazy(() => import('./CreateTask')) // 
}
]
};