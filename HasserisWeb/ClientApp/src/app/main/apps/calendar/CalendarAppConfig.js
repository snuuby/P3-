import React from 'react';
import CalendarApp from "./CalendarApp";

export const CalendarAppConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    routes  : [
        {
            path     : '/apps/calendar',
            component: React.lazy(() => import('./CalendarApp'))
        }
    ]
};
