import React from 'react';

export const ProfilePageConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    routes  : [
        {
            path     : '/profile',
            component: React.lazy(() => import('./ProfilePage'))
        }
    ]
};
