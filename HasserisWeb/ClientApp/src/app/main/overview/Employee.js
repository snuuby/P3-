import React, { useEffect, useState } from 'react';
import { Avatar, ExpansionPanel, TextField, ExpansionPanelSummary, ExpansionPanelDetails, Icon, Tab, Tabs, Tooltip, Typography } from '@material-ui/core';
import { FuseAnimate, FusePageCarded } from '@fuse';
import { Link } from 'react-router-dom';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import withReducer from '../../store/withReducer';
import * as Actions from './store/actions';
import reducer from './store/reducers';
import { useDispatch, useSelector } from 'react-redux';

function Marker(props) {
    return (
        <Tooltip title={props.text} placement="top">
            <Icon className="text-red">place</Icon>
        </Tooltip>
    );
}

function Employee(props) {
    const dispatch = useDispatch();
    const employee = useSelector(({ employeeReducer }) => employeeReducer.employees.employeeData);
    const [tabValue, setTabValue] = useState(0);
    const employeePhonenumber = ((employee || {}).ContactInfo || {}).PhoneNumber;
    const employeeEmail = ((employee || {}).ContactInfo || {}).Email;
    const employeeZIP = ((employee || {}).Address || {}).ZIP;
    const employeeCity = ((employee || {}).Address || {}).City;
    const employeeLivingaddress = ((employee || {}).Address || {}).LivingAddress;

    useEffect(() => {
        dispatch(Actions.getEmployee(props.match.params));
    }, [props.match.params]);


    function handleChangeTab(event, tabValue) {
        setTabValue(tabValue);
    }

    return (
        <FusePageCarded
            classes={{
                content: "flex",
                header: "min-h-72 h-72 sm:h-136 sm:min-h-136"
            }}
            header={
                employee && (
                    <div className="flex flex-1 w-full items-center justify-between">

                        <div className="flex flex-1 flex-col items-center sm:items-start">

                            <FuseAnimate animation="transition.slideRightIn" delay={300}>
                                <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/employee/overview" color="inherit">
                                    <Icon className="mr-4 text-20">arrow_back</Icon>
                                    Ansatte
                                </Typography>
                            </FuseAnimate>

                            <div className="flex flex-col min-w-0 items-center sm:items-start">

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography className="text-16 sm:text-20 truncate">
                                        {employee.Firstname + ' ' + employee.Lastname}
                                    </Typography>
                                </FuseAnimate>

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography variant="caption">
                                        {'Medarbejder ID: ' + employee.ID}
                                    </Typography>
                                </FuseAnimate>
                            </div>

                        </div>
                    </div>
                )
            }
            contentToolbar={
                <Tabs
                    value={tabValue}
                    onChange={handleChangeTab}
                    indicatorColor="secondary"
                    textColor="secondary"
                    variant="scrollable"
                    scrollButtons="auto"
                    classes={{ root: "w-full h-64" }}
                >
                    <Tab className="h-64 normal-case" label="Employee Details" />
                </Tabs>
            }
            content={
                employee && (
                    <div className="p-16 sm:p-24 max-w-2xl w-full">
                        {/*Text Fields*/}
                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pr-1 ">
                                {/*Employee ID*/}
                                <TextField
                                    id="EmployeeID"
                                    label="Medarbejder ID"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="EmployeeID"
                                    value={employee.ID}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                            <div class="flex-1 bg-gray-0 h-12 pl-10">
                                {/*Full Name*/}
                                <TextField
                                    id="FullName"
                                    label="Navn"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="FullName"
                                    value={employee.Firstname + ' ' + employee.Lastname}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                        </div>
                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pt-64 pb-8">
                                {/*Adresse*/}
                                <TextField
                                    id="Address"
                                    label="Adresse"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="Address"
                                    value={employeeLivingaddress + ' ' + employeeZIP + ' ' + employeeCity}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                        </div>
                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                {/*Email*/}
                                <TextField
                                    id="EmployeeEmail"
                                    label="Medarbejder email"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="EmployeeEmail"
                                    value={employeeEmail}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                            <div class="flex-1 bg-gray-0 h-12 pl-10  pt-64">
                                {/*Tlf. nummer*/}
                                <TextField
                                    id="EmployeeTelefonnummer"
                                    label="Telefonnummer"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="EmployeeTelefonnummer"
                                    value={employeePhonenumber}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                        </div>
                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                {/*Løn*/}
                                <TextField
                                    id="EmployeeWage"
                                    label="Medarbejder løn"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="EmployeeWage"
                                    value={employee.Wage}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                            <div class="flex-1 bg-gray-0 h-12 pl-10  pt-64">
                                {/*Type*/}
                                <TextField
                                    id="EmployeeType"
                                    label="Medarbejder Type"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="EmployeeType"
                                    value={employee.Type}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                        </div>
                    </div>
                )
            }
            innerScroll
        />
    )
}

export default withReducer('employeeReducer', reducer)(Employee);
