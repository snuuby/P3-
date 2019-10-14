import React from 'react';
import {Redirect} from 'react-router-dom';
import {FuseUtils} from '@fuse';
import {LoginConfig} from "app/main/login/LoginConfig";
import {appsConfigs} from "app/main/apps/appsConfigs";
import CalendarApp from "../main/apps/calendar/CalendarApp";
import {EmployeeOverviewConfig} from "../main/overview/EmployeeOverviewConfig";

// Vi skal have flere routeConfigs her
const routeConfigs = [
    EmployeeOverviewConfig,
    LoginConfig,
    ...appsConfigs,
];

const routes = [
    ...FuseUtils.generateRoutesFromConfigs(routeConfigs),
    {
        path     : '/',
        component: () => <Redirect to="/login"/>
        
    }
];

export default routes;
