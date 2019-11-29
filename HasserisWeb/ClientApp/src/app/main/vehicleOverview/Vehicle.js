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

function Vehicle(props) {
    const dispatch = useDispatch();
    const vehicle = useSelector(({ vehicleReducer }) => vehicleReducer.vehicles.vehicleData);
    const [tabValue, setTabValue] = useState(0);

    useEffect(() => {
        dispatch(Actions.getVehicle(props.match.params));
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
                vehicle && (
                    <div className="flex flex-1 w-full items-center justify-between">

                        <div className="flex flex-1 flex-col items-center sm:items-start">

                            <FuseAnimate animation="transition.slideRightIn" delay={300}>
                                <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/vehicle/overview" color="inherit">
                                    <Icon className="mr-4 text-20">arrow_back</Icon>
                                    Vehicles
                                </Typography>
                            </FuseAnimate>

                            <div className="flex flex-col min-w-0 items-center sm:items-start">

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography className="text-16 sm:text-20 truncate">
                                        {vehicle.Name}
                                    </Typography>
                                </FuseAnimate>

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography variant="caption">
                                        {'Koretoj ID: ' + vehicle.ID}
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
                    <Tab className="h-64 normal-case" label="Vehicle Details" />
                </Tabs>
            }
            content={
                vehicle && (
                    <div className="p-16 sm:p-24 max-w-2xl w-full">
                        {/*Text Fields*/}
                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pr-1 ">
                                {/*Vehicle ID*/}
                                <TextField
                                    id="VehicleID"
                                    label="Koretoj ID"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="Koretoj ID"
                                    value={vehicle.ID}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                            <div class="flex-1 bg-gray-0 h-12 pl-10">
                                {/*Model*/}
                                <TextField
                                    id="Model"
                                    label="Model"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="Model"
                                    value={vehicle.Model}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                        </div>
                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pt-64 pb-8">
                                {/*RegNum*/}
                                <TextField
                                    id="Registration number"
                                    label="Registration number"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="Regustration number"
                                    value={vehicle.RegNum}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                        </div>
                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                {/*Test*/}
                                <TextField
                                    id="Test"
                                    label="Test"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="Test"
                                    value={vehicle.IsAvailable}
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

export default withReducer('vehicleReducer', reducer)(Vehicle);
