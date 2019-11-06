import React, { useEffect, useState } from 'react';
import { Avatar, TextField, ExpansionPanel, ExpansionPanelSummary, ExpansionPanelDetails, Icon, Tab, Tabs, Tooltip, Typography } from '@material-ui/core';
import { FuseAnimate, FusePageCarded } from '@fuse';
import { Link } from 'react-router-dom';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import withReducer from '../../store/withReducer';
import * as Actions from './store/actions';
import reducer from './store/reducers';
import { useDispatch, useSelector } from 'react-redux';
import { getCustomer } from './store/actions';

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
                                        {customer.firstName + ' ' + customer.lastName}
                                    </Typography>
                                </FuseAnimate>

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography variant="caption">
                                        {'Customer ID: ' + customer.id}
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
                        {/*Customer Details*/}
                        {tabValue === 0 &&
                            (
                                <div>
                                    <div className="pb-48">

                                        <div className="pb-16 flex items-center">
                                            <Icon className="mr-16" color="action">account_circle</Icon>
                                            <Typography className="h2" color="textSecondary">Customer</Typography>
                                        </div>

                                        <div className="mb-24">

                                            <div className="table-responsive mb-16">
                                                <table className="simple">
                                                    <thead>
                                                        <tr>
                                                            <th>Navn</th>
                                                            <th>email</th>
                                                            <th>tlf nummer</th>
                                                            <th>type</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                        <td>
                                                            <Typography className="truncate">{customer.firstName + ' ' + customer.lastName}</Typography>
                                                            </td>
                                                            <td>
                                                                <Typography className="truncate">{customer.email}</Typography>
                                                            </td>
                                                            <td>
                                                                <Typography className="truncate">{customer.phoneNumber}</Typography>
                                                            </td>
                                                            <td>
                                                                <span className="truncate">{customer.type}</span>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            )}
                        {/*Text Fields*/}
                        <div class="flex mb-4">
                            <div class="w-1/3 bg-gray-0 h-12">
                                <TextField
                                    id="Name"
                                    label="Navn"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="Name"
                                    value={customer.firstName}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                            <div class="w-1/3 bg-gray-500 h-12">
                                <TextField
                                    id="Name"
                                    label="Navn"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="Name"
                                    value={customer.firstName}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                            <div class="w-1/3 bg-gray-400 h-12">
                                <TextField
                                    id="Name"
                                    label="Navn"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="Name"
                                    value={customer.firstName}
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

export default Customer
