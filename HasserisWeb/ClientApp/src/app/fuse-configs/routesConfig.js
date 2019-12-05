import React from 'react';
import {Redirect} from 'react-router-dom';
import {FuseUtils} from '@fuse';
import {appsConfigs} from "app/main/appsConfigs";


// Vi skal have flere routeConfigs her
const routeConfigs = [
    ...appsConfigs
];

const routes = [
    ...FuseUtils.generateRoutesFromConfigs(routeConfigs, ['adminplus', 'admin', 'employee']),
    {
        path: '/',
        component: () => <Redirect to="/apps/calendar" />
    }
];
export default routes;
