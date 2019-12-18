import React from "react";
import authRoles from "../../../auth/authRoles";

export const CreateOfferConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    auth    : authRoles.admin,
    routes  : [
        {
            path     : '/Offers/Create',
            component: React.lazy(() => import('./CreateOffer'))
}
]
};