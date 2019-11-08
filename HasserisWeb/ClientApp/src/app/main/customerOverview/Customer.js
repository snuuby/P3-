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

function Customer(props) {
    const dispatch = useDispatch();
    const customer = useSelector(({ customerReducer }) => customerReducer.customers);
    const [tabValue, setTabValue] = useState(0);
    const customerPhonenumber = ((customer || {}).ContactInfo || {}).PhoneNumber;
    const customerEmail = ((customer || {}).ContactInfo || {}).Email;
    const customerLivingaddress = ((customer || {}).Address || {}).LivingAddress;
    const customerZip = ((customer || {}).Address || {}).ZIP;
    const customerCity = ((customer || {}).Address || {}).City;

    useEffect(() => {
        dispatch(Actions.getCustomer(props.match.params));
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
                customer && (
                    <div className="flex flex-1 w-full items-center justify-between">

                        <div className="flex flex-1 flex-col items-center sm:items-start">

                            <FuseAnimate animation="transition.slideRightIn" delay={300}>
                                <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/customer/overview" color="inherit">
                                    <Icon className="mr-4 text-20">arrow_back</Icon>
                                    Customers
                                </Typography>
                            </FuseAnimate>

                            <div className="flex flex-col min-w-0 items-center sm:items-start">

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography className="text-16 sm:text-20 truncate">
                                        {customer.Firstname + ' ' + customer.Lastname}
                                    </Typography>
                                </FuseAnimate>

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography variant="caption">
                                        {'Kunde ID: ' + customer.ID}
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
                    <Tab className="h-64 normal-case" label="Customer Details" />
                </Tabs>
            }
            content={
                customer && (
                    <div className="p-16 sm:p-24 max-w-2xl w-full">
                        {/*Text Fields*/}
                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pr-1 ">
                                {/*Customer ID*/}
                                <TextField
                                    id="CustomerID"
                                    label="Kunde ID"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="CustomerID"
                                    value={customer.ID}
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
                                    value={customer.Firstname + ' ' + customer.Lastname}
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
                                    id="address"
                                    label="Adresse"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="CustomerID"
                                    value={customerLivingaddress + ' ' + customerZip + ' ' + customerCity}
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
                                    id="CustomerEmail"
                                    label="Kunde email"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="CustomerEmail"
                                    value={customerEmail}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                            <div class="flex-1 bg-gray-0 h-12 pl-10  pt-64">
                                {/*Tlf. nummer*/}
                                <TextField
                                    id="CustomerTelefonnummer"
                                    label="Telefonnummer"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="CustomerTelefonnummer"
                                    value={customerPhonenumber}
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

export default withReducer('customerReducer', reducer)(Customer);
