import React from "react";
import authRoles from "../../../auth/authRoles";

export const CreateInspectionReportConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    auth    : authRoles.admin,
    routes  : [
        {
            path     : '/InspectionReport/Make',
            component: React.lazy(() => import('./CreateInspectionReport'))
}
]
};