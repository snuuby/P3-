import React, { useEffect, useState } from 'react';
import { Avatar, ExpansionPanel, TextField, ExpansionPanelSummary, ExpansionPanelDetails, Icon, Tab, Tabs, Tooltip, Typography, Button } from '@material-ui/core';
import { FuseAnimate, FusePageCarded } from '@fuse';
import { Link } from 'react-router-dom';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import withReducer from '../../store/withReducer';
import * as Actions from './store/actions';
import reducer from './store/reducers';
import { useDispatch, useSelector } from 'react-redux';



function Offer(props) {
    const dispatch = useDispatch();

    

    

    return (
        <FusePageCarded
            classes={{
                content: "flex",
                header: "min-h-72 h-72 sm:h-136 sm:min-h-136"
            }}
            header={
                    <div className="flex flex-1 w-full items-center justify-between">

                        <div className="flex flex-1 flex-col items-center sm:items-start">


                            <div className="flex flex-col min-w-0 items-center sm:items-start">

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography className="text-16 sm:text-20 truncate">
                                        Lav et tilbud
                                    </Typography>
                                </FuseAnimate>

                            </div>
                    </div>
                    <Button
                        onClick={() => console.log("h")}
                        variant="contained" color="green" className="max-w-512 px-8 py-100 hidden sm:flex">
                        Save
                    </Button>
                    </div>
                
            }
            contentToolbar={
                <Tabs
                    //value={tabValue}
                    //onChange={handleChangeTab}
                    indicatorColor="secondary"
                    textColor="secondary"
                    variant="scrollable"
                    scrollButtons="auto"
                    classes={{ root: "w-full h-64" }}
                >
                    <Tab className="h-64 normal-case" label="Tilbud Info" />
                </Tabs>
            }
            content={
                 
                <div className="p-16 sm:p-24 max-w-2xl w-full">
                    {/*Text Fields*/}
                    <div class="flex mb-4">
                        <div class="flex-1 bg-gray-0 h-12 pr-1">
                            {/*Navn*/}
                            <TextField
                                id="Name"
                                label="Navn"
                                className="mt-8 mb-16"
                                InputLabelProps={{
                                    shrink: true
                                }}
                                name="Name"
                                //value="ASDASDADADADADADAD"
                                variant="outlined"
                                autoFocus
                                required
                                fullWidth
                            />
                        </div>
                    </div>
                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                {/*Customer*/}
                                <TextField
                                    id="Customer"
                                    label="Kunde"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="Customer"
                                    //value={customer.ID}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>
                        </div>
                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                {/*VisitingDate*/}
                                <TextField
                                    id="InspectionReportDate"
                                    label="Besigtigelsesdato"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="InspectionReportDate"
                                    //value={customer.ID}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>

                            <div class="flex-1 bg-gray-0 h-12 pl-10  pt-64">
                                {/*MovingDate*/}
                                <TextField
                                    id="MovingDate"
                                    label="Flyttedato"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="MovingDate"
                                    //value={customer.ID}
                                    variant="outlined"
                                    autoFocus
                                    required={false}
                                    fullWidth
                                />
                            </div>
                        </div>

                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                
                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography className="text-16 sm:text-20 truncate">
                                        Start ved:
                                    </Typography>
                                </FuseAnimate>
                            </div>
                            <div class="flex mb-4">
                                <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                    {/*Start Addresse*/}
                                    
                                    <TextField
                                        id="Startaddress"
                                        label="addresse"
                                        className="mt-8 mb-16"
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        name="Startaddress"
                                        //value={customer.ID}
                                        variant="outlined"
                                        autoFocus
                                        required
                                        fullWidth
                                    />
                                </div>
                                <div class="flex-1 bg-gray-0 h-12 pl-10  pt-64">
                                    {/*city*/}
                                    <TextField
                                        id="StartCity"
                                        label="By"
                                        className="mt-8 mb-16"
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        name="StartCity"
                                        //value={customer.ID}
                                        variant="outlined"
                                        autoFocus
                                        required={false}
                                        fullWidth
                                    />
                                </div>
                                <div class="flex-1 bg-gray-0 h-12 pl-10  pt-64">
                                    {/*ZIP*/}
                                    <TextField
                                        id="StartZIP"
                                        label="ZIP"
                                        className="mt-8 mb-16"
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        name="StartZIP"
                                        //value={customer.ID}
                                        variant="outlined"
                                        autoFocus
                                        required={false}
                                        fullWidth
                                    />
                                </div>
                                <div class="flex-1 bg-gray-0 h-12 pl-10  pt-64">
                                    {/*Note*/}
                                    <TextField
                                        id="StartNote"
                                        label="Note"
                                        className="mt-8 mb-16"
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        name="StartNote"
                                        //value={customer.ID}
                                        variant="outlined"
                                        autoFocus
                                        required={false}
                                        fullWidth
                                    />
                                </div>
                            </div>  
                        </div>   

                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                
                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography className="text-16 sm:text-20 truncate" >
                                        Flyt til:
                                    </Typography>
                                </FuseAnimate>
                            </div>
                            <div class="flex mb-4">
                                <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                    {/*Destination Addresse*/}
                                    <TextField
                                        id="Destinationaddress"
                                        label="addresse"
                                        className="mt-8 mb-16"
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        name="Destinationaddress"
                                        //value={customer.ID}
                                        variant="outlined"
                                        autoFocus
                                        required
                                        fullWidth
                                    />
                                </div>
                                <div class="flex-1 bg-gray-0 h-12 pl-10  pt-64">
                                    {/*Destinationcity*/}
                                    <TextField
                                        id="DestinationCity"
                                        label="By"
                                        className="mt-8 mb-16"
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        name="DestinationCity"
                                        //value={customer.ID}
                                        variant="outlined"
                                        autoFocus
                                        required={false}
                                        fullWidth
                                    />
                                </div>
                                <div class="flex-1 bg-gray-0 h-12 pl-10  pt-64">
                                    {/*DestinationZIP*/}
                                    <TextField
                                        id="DestinationZIP"
                                        label="ZIP"
                                        className="mt-8 mb-16"
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        name="DestinationZIP"
                                        //value={customer.ID}
                                        variant="outlined"
                                        autoFocus
                                        required={false}
                                        fullWidth
                                    />
                                </div>
                                <div class="flex-1 bg-gray-0 h-12 pl-10  pt-64">
                                    {/*DestinationNote*/}
                                    <TextField
                                        id="DestinationNote"
                                        label="Note"
                                        className="mt-8 mb-16"
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        name="DestinationNote"
                                        //value={customer.ID}
                                        variant="outlined"
                                        autoFocus
                                        required={false}
                                        fullWidth
                                    />
                                </div>
                            </div>    
                        </div>
                        
                        <div class="flex mb-4">

                            <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                {/*Employee*/}
                                <TextField
                                    id="Employee"
                                    label="Ansat på besøg"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="Employee"
                                    //value={customer.ID}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                            </div>

                        </div>
                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                {/*ExpectedHours*/}
                                <TextField
                                    id="ExpectedHours"
                                    label="Forventet timeantal"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="ExpectedHours"
                                    //value={customer.ID}
                                    variant="outlined"
                                    autoFocus
                                    required={false}
                                    fullWidth
                                />
                            </div>
                            <div class="flex-1 bg-gray-0 h-12 pl-10  pt-64">
                                {/*LentBoxes*/}
                                <TextField
                                    id="LentBoxes"
                                    label="Lånte flyttekasser"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="LentBoxes"
                                    //value={customer.ID}
                                    variant="outlined"
                                    autoFocus
                                    required={false}
                                    fullWidth
                                />
                            </div>
                        </div>
                    


                </div>

        }
            innerScroll
        />
    )
}

export default withReducer('makeReducer', reducer)(Offer);
