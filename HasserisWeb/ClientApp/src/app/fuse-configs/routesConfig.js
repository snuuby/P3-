import React from 'react';
import {Redirect} from 'react-router-dom';
import {FuseUtils} from '@fuse';
import {LoginConfig} from "app/main/login/LoginConfig";
import {appsConfigs} from "app/main/appsConfigs";
import CalendarApp from "../main/calendar/CalendarApp";
import {ProfilePageConfig} from "app/main/profile/ProfilePageConfig";
import { EmployeeOverviewConfig } from "../main/employee/EmployeeOverviewConfig";
import { ToolOverviewConfig } from "../main/tool/ToolOverviewConfig";
import { VehicleOverviewConfig } from "../main/vehicle/VehicleOverviewConfig";
import { CustomerOverviewConfig } from "../main/customer/CustomerOverviewConfig";
import * as Actions from 'app/store/actions';
import { useDispatch, useSelector } from 'react-redux';


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
