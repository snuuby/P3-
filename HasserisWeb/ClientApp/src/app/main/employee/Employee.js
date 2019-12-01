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
    FirstName: null,
    LastName: null,
    Type: "AdminPlus",
    Wage: null,
    Available: 'Yes',

    //Address
    LivingAddress: null,
    ZIP: null,
    City: null,
    Note: null,

    //Contact info
    Email: null,
    PhoneNumber: null
};
function Employee(props) {
    const classes = useStyles();

    const dispatch = useDispatch();
    const { form, handleChange, setForm } = useForm(defaultFormState);
    const eventDialog = useSelector(({ employeeReducer }) => employeeReducer.employees.eventDialog);
    const [tabValue, setTabValue] = useState(0);


    const inputLabel = React.useRef(null);
    const [labelWidth, setLabelWidth] = useState(0);

    function handleChangeTab(event, tabValue) {
        setTabValue(tabValue);
    }
    useEffect(() => {
        dispatch(Actions.getEmployee(props.match.params));
    }, [props.match.params]);

    useEffect(() => {
        setLabelWidth(inputLabel.current.offsetWidth + 20);
    }, []);
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


    function closeComposeDialog() {
        // Er der nogen dialogs overhovedet?
        dispatch(Actions.closeNewAddDialog());
    }

    function canBeSubmitted() {
        return (
            form.Firstname && form.Lastname && form.Address // hvad g�r den her helt pr�cist? 
        );
    }

    function handleSubmit(event) {


        event.preventDefault();
        dispatch(Actions.editEmployee(form));
        props.history.push('/employee/overview');

        closeComposeDialog();

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


                        <div className="flex flex-col min-w-0 items-center sm:items-start">
                            <FuseAnimate animation="transition.slideRightIn" delay={300}>
                                <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/employee/overview" color="inherit">
                                    <Icon className="mr-4 text-20">arrow_back</Icon>
                                    Ansatte
                                </Typography>
                            </FuseAnimate>
                            <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                <Typography className="text-16 sm:text-20 truncate">
                                    Ansat: {form.Firstname + ' ' + form.Lastname}
                                    </Typography>
                            </FuseAnimate>
                            <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                <Typography variant="caption">
                                    {'Ansat ID: ' + form.ID}
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
                    <Tab className="h-64 normal-case" label="Ansat detaljer" />
                </Tabs>
            }
            content={
                <div>

                    <form noValidate onSubmit={handleSubmit} >
                        <div className="p-16 sm:p-24 max-w-2xl w-full">
                            <FormControl variant="outlined" className={classes.formControl}>
                                <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                    Ansat Type
                                </InputLabel>
                                <Select
                                    labelid="demo-simple-select-outlined-label"
                                    id="Type"
                                    name="Type"
                                    value={form.Type}
                                    onChange={handleChange}
                                    required
                                    labelWidth={labelWidth}
                                >
                                    <MenuItem value="Admin">Admin</MenuItem>
                                    <MenuItem value="AdminPlus">AdminPlus</MenuItem>
                                    <MenuItem value="Field Worker">Flyttemand</MenuItem>

                                </Select>
                            </FormControl>
                            <div>

                                <TextField
                                    id="Firstname"
                                    label="Fornavn"
                                    className={classes.formControl}
                                    name="Firstname"
                                    value={form.Firstname}
                                    onChange={handleChange}
                                    variant="outlined"
                                    autoFocus
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    required
                                />

                                <TextField
                                    id="Lastname"
                                    label="Efternavn"
                                    className={classes.formControl}
                                    name="Lastname"
                                    value={form.Lastname}
                                    onChange={handleChange}
                                    variant="outlined"
                                    autoFocus
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    required
                                />

                                <div>
                                    <TextField
                                        id="LivingAddress"
                                        label="Addresse"
                                        className={classes.formControl}
                                        name="LivingAddress"
                                        value={form.LivingAddress}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />
                                    <TextField
                                        id="ZIP"
                                        label="Postnummer"
                                        className={classes.formControl}
                                        name="ZIP"
                                        value={form.ZIP}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />
                                    <TextField
                                        id="City"
                                        label="By"
                                        className={classes.formControl}
                                        name="City"
                                        value={form.City}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />
                                </div>
                                <div>
                                    <TextField
                                        id="Email"
                                        label="Mail"
                                        className={classes.formControl}
                                        name="Email"
                                        value={form.Email}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />
                                    <TextField
                                        id="Phonenumber"
                                        label="Telefonnummer"
                                        className={classes.formControl}
                                        name="Phonenumber"
                                        value={form.Phonenumber}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />

                                </div>

                            </div>

                            <div>
                                <TextField
                                    id="Wage"
                                    label="Timel�n"
                                    className={classes.formControl}
                                    name="Wage"
                                    value={form.Wage}
                                    onChange={handleChange}
                                    variant="outlined"
                                    autoFocus
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    required
                                />
                                <FormControl variant="outlined" className={classes.formControl}>
                                    <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                        Er ansat aktiv?
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
                            </div>
                            <Button
                                className={classes.formControl}
                                variant="contained"
                                color="primary"
                                type="submit"
                            >
                                GEM
                                </Button>

                        </div>

                    </form>

                </div>
            }
            innerScroll
        />

    );
}

export default withReducer('employeeReducer', reducer)(Employee);
