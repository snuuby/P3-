import React from 'react';
import {Redirect} from 'react-router-dom';
import {FuseUtils} from '@fuse';
import {ExampleConfig} from 'app/main/example/ExampleConfig';
import {LoginConfig} from "app/main/login/LoginConfig";
import {appsConfigs} from "app/main/apps/appsConfigs";
import CalendarApp from "../main/apps/calendar/CalendarApp";

// Vi skal have flere routeConfigs her
const routeConfigs = [
    ExampleConfig,
    LoginConfig,
    ...appsConfigs,
];

const routes = [
    ...FuseUtils.generateRoutesFromConfigs(routeConfigs),
    {
        path     : '/',
        component: () => <Redirect to="/apps/calendar"/>
        
    }
];

export default routes;
