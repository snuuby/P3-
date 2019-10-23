import React from 'react';
import {Redirect} from 'react-router-dom';
import {FuseUtils} from '@fuse';
import {LoginConfig} from "app/main/login/LoginConfig";
import {appsConfigs} from "app/main/apps/appsConfigs";
import CalendarApp from "../main/apps/calendar/CalendarApp";
import {ProfilePageConfig} from "app/main/profile/ProfilePageConfig";
import { EmployeeOverviewConfig } from "../main/overview/EmployeeOverviewConfig";
import * as Actions from 'app/store/actions';
import { useDispatch, useSelector } from 'react-redux';


// Vi skal have flere routeConfigs her
const routeConfigs = [
    EmployeeOverviewConfig,
    LoginConfig,
    ProfilePageConfig,
    ...appsConfigs,
];

const routes = [
    ...FuseUtils.generateRoutesFromConfigs(routeConfigs, ['adminPlus', 'admin', 'employee']),
    {
        path: '/',
        component: () => <Redirect to="/apps/calendar" />
    }
];
export default routes;
