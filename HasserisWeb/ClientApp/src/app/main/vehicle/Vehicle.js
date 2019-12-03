import React, { useCallback, useEffect, useState } from 'react';
import { TextField, Button, Dialog, DialogActions, DialogContent, Icon, Tabs, Tab, IconButton, Typography, Toolbar, AppBar, FormControlLabel, Switch } from '@material-ui/core';
import { FuseAnimate, FusePageCarded, FuseChipSelect, SelectFormsy } from '@fuse';
import { useForm } from '@fuse/hooks';
import { makeStyles } from '@material-ui/core/styles';
import { Link } from 'react-router-dom';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import withReducer from './../../store/withReducer';
import * as Actions from './store/actions';
import reducer from './store/reducers';
import moment from 'moment';
import { useDispatch, useSelector } from 'react-redux';
import { FormControl, Select, InputLabel, MenuItem, FormHelperText } from '@material-ui/core';
const useStyles = makeStyles(theme => ({
    formControl: {
        margin: theme.spacing(1),
        minWidth: 120,
    },
    selectEmpty: {
        marginTop: theme.spacing(2),
    },
}));
const defaultFormState = {
    Name: null,
    Model: null,
    RegNum: null,
    Available: 'Yes',

};
function Vehicle(props) {
    const classes = useStyles();

    const dispatch = useDispatch();
    const eventDialog = useSelector(({ vehicleReducer }) => vehicleReducer.vehicles.eventDialog);
    const [tabValue, setTabValue] = useState(0);
    const { form, handleChange, setForm } = useForm(defaultFormState);
    const inputLabel = React.useRef(null);
    const [labelWidth, setLabelWidth] = useState(0);
    useEffect(() => {
        dispatch(Actions.getVehicle(props.match.params));
    }, [props.match.params]);
    useEffect(() => {
        setLabelWidth(inputLabel.current.offsetWidth + 20);
    }, []);

    function handleChangeTab(event, tabValue) {
        setTabValue(tabValue);
    }
    const initDialog = useCallback(
        () => {
            if (eventDialog.type === 'new') {
                setForm({
                    ...eventDialog.data,
                });
            }


        },
        [eventDialog.data, eventDialog.type, setForm],
    );

    useEffect(() => {
        /**
         * After Dialog Open
         */
        if (eventDialog.props.open) {
            initDialog();

        }
    }, [eventDialog.props.open, initDialog]);
    function handleSubmit(event) {


        event.preventDefault();
        dispatch(Actions.editVehicle(form));
        props.history.push('/vehicle/overview');


    }
    return (
        <FusePageCarded
            classes={{
                content: "flex",
                header: "min-h-72 h-72 sm:h-136 sm:min-h-136"
            }}
            header={
              
                    <div className="flex flex-1 w-full items-center justify-between">

                        <div className="flex flex-1 flex-col items-center sm:items-start">

                            <FuseAnimate animation="transition.slideRightIn" delay={300}>
                                <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/vehicle/overview" color="inherit">
                                    <Icon className="mr-4 text-20">arrow_back</Icon>
                                    Biler
                                </Typography>
                            </FuseAnimate>

                            <div className="flex flex-col min-w-0 items-center sm:items-start">

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography className="text-16 sm:text-20 truncate">
                                        Navn: {form.Name}
                                    </Typography>
                                </FuseAnimate>

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography variant="caption">
                                    {'Bil ID: ' + form.ID}
                                    </Typography>
                                </FuseAnimate>
                            </div>

                        </div>
                    </div>
                
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
                    <Tab className="h-64 normal-case" label="Køretøj detaljer" />
                </Tabs>
            }
            content={
                <form noValidate onSubmit={handleSubmit} >
                    <div className="p-16 sm:p-24 max-w-2xl w-full">
                        {/*Text Fields*/}
                        <div class="flex mb-4">
                            <div class="flex-1 bg-gray-0 h-12 pr-1 ">
                                {/*Vehicle ID*/}
                                <TextField
                                    id="Name"
                                    label="Navn"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="Name"
                                    value={form.Name}
                                    variant="outlined"
                                    onChange={handleChange}
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
                                    value={form.Model}
                                    variant="outlined"
                                    autoFocus
                                    onChange={handleChange}
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
                                    value={form.RegNum}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    onChange={handleChange}
                                    fullWidth
                                />
                                <FormControl variant="outlined" className={classes.formControl}>
                                    <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                        Er køretøjet aktivt?
                                </InputLabel>
                                    <Select
                                        labelid="demo-simple-select-outlined-label"
                                        id="Available"
                                        name="Available"
                                        value={form.Available}
                                        onChange={handleChange}
                                        required
                                        labelWidth={labelWidth}
                                    >
                                        <MenuItem value="Yes">Ja</MenuItem>
                                        <MenuItem value="No">Nej</MenuItem>

                                    </Select>
                                </FormControl>
                                <Button
                                    className={classes.formControl}
                                    variant="contained"
                                    color="primary"
                                    type="submit"
                                >
                                    GEM
                             </Button>
                            </div>


                        </div>

                    </div>
                </form>
            }
            innerScroll
        />
    )
}

export default withReducer('vehicleReducer', reducer)(Vehicle);
