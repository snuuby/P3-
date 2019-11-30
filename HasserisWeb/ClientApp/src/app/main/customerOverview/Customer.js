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
    CustomerType: 'Private',
    LivingAddress: null,
    ZIP: null,
    City: null,
    Phonenumber: null,
    Email: null,

    // Private specific
    Firstname: null,
    Lastname: null,

    // Business specific
    Name: null,
    CVR: null,

    // Public specific
    Name: null,
    EAN: null,
};

function Customer(props) {
    const classes = useStyles();

    const dispatch = useDispatch();
    const { form, handleChange, setForm } = useForm(defaultFormState);
    const eventDialog = useSelector(({ customerReducer }) => customerReducer.customers.eventDialog);

    // skal skiftes til customers, men har vi overhovedet brug for at loade vores customers her? :/ når vi laver
    const [tabValue, setTabValue] = useState(0);


    let end = moment(form.end).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);
    let start = moment(form.start).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);
    const inputLabel = React.useRef(null);
    const [labelWidth, setLabelWidth] = useState(0);

    function handleChangeTab(event, tabValue) {
        setTabValue(tabValue);
    }
    useEffect(() => {
        dispatch(Actions.getCustomer(props.match.params));
    }, [props.match.params]);
    useEffect(() => {
        setLabelWidth(inputLabel.current.offsetWidth + 20);
    }, []);
    const initDialog = useCallback(
        () => {
            const event = { start: start };
            if (eventDialog.type === 'new') {
                //dispatch(Actions.getAvailableEmployees());
                //dispatch(Actions.getAvailableCars());
                //dispatch(Actions.getCustomers());
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
            form.Firstname && form.Lastname && form.Address // hvad gør den her helt præcist? 
        );
    }
    function printCustomerName() {
        if (form.CustomerType == "Private") {
            return form.Firstname + ' ' + form.Lastname;
        }
        else if (form.CustomerType == "Public") {
            return form.Name + ' EAN: ' + form.EAN;
        }
        else if (form.CustomerType == "Business") {
            return form.Name + ' CVR: ' + form.CVR;
        }
    }
    function handleSubmit(event) {


        event.preventDefault();

        if (form.CustomerType == "Private") {
            dispatch(Actions.addPrivateCustomer(form));
        }
        else if (form.CustomerType == "Business") {
            dispatch(Actions.addBusinessCustomer(form));
        }
        else {
            dispatch(Actions.addPublicCustomer(form));
        }

        // flytter os hen på 
        props.history.push('/customer/overview');


        closeComposeDialog();

    }



    if (form.CustomerType == "Private") {
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
                                    <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/customer/overview" color="inherit">
                                        <Icon className="mr-4 text-20">arrow_back</Icon>
                                        Kunder
                                </Typography>
                                </FuseAnimate>

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography className="text-16 sm:text-20 truncate">
                                        {form.CustomerType} Kunde: {form.Firstname} {form.Lastname}
                                    </Typography>
                                </FuseAnimate>
                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography variant="caption">
                                        {'Kunde ID: ' + form.ID}
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
                        <Tab className="h-64 normal-case" label="Kunde detaljer" />
                    </Tabs>
                }
                content={
                    <div>

                        <form noValidate onSubmit={handleSubmit} >
                            <div className="flex-1 bg-gray-0 h-12 pr-1 pt-64">

                                <div>
                                    <FormControl variant="outlined" className={classes.formControl}>
                                        <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                            Kunde Type
                                </InputLabel>
                                        <Select
                                            labelid="demo-simple-select-outlined-label"
                                            id="CustomerType"
                                            name="CustomerType"
                                            value={form.CustomerType}
                                            onChange={handleChange}
                                            required
                                            labelWidth={labelWidth}
                                        >
                                            <MenuItem value="Private">Privat</MenuItem>
                                            <MenuItem value="Business">Virksomhed</MenuItem>
                                            <MenuItem value="Public">Offentlig</MenuItem>

                                        </Select>
                                    </FormControl>
                                </div>
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

                                </div>

                                <div>

                                    <TextField
                                        id="LivingAddress"
                                        label="Adresse"
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

                                </div>


                                <Button
                                    className={classes.formControl}
                                    variant="contained"
                                    color="primary"
                                    type="submit"
                                    disabled={!canBeSubmitted()}
                                >
                                    Tilføj
                                </Button>

                            </div>

                        </form>

                    </div>
                }
                innerScroll
            />

        );
    }
    else if (form.CustomerType == "Business") {
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
                                    <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/customer/overview" color="inherit">
                                        <Icon className="mr-4 text-20">arrow_back</Icon>
                                        Kunder
                                </Typography>
                                </FuseAnimate>

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography className="text-16 sm:text-20 truncate">
                                        {form.CustomerType} Kunde: {form.Name} EAN: {form.EAN}
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
                        <Tab className="h-64 normal-case" label="Kunde detaljer" />
                    </Tabs>
                }
                content={
                    <div>

                        <form noValidate onSubmit={handleSubmit} >
                            <div className="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                <FormControl variant="outlined" className={classes.formControl}>
                                    <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                        Kunde Type
                                </InputLabel>
                                    <Select
                                        labelid="demo-simple-select-outlined-label"
                                        id="CustomerType"
                                        name="CustomerType"
                                        value={form.CustomerType}
                                        onChange={handleChange}
                                        required
                                        labelWidth={labelWidth}
                                    >
                                        <MenuItem value="Private">Privat</MenuItem>
                                        <MenuItem value="Business">Virksomhed</MenuItem>
                                        <MenuItem value="Public">Offentlig</MenuItem>

                                    </Select>
                                </FormControl>
                                <div>

                                    <TextField
                                        id="Name"
                                        label="Navn"
                                        className={classes.formControl}
                                        name="Name"
                                        value={form.Name}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />
                                    <TextField
                                        id="EAN"
                                        label="EAN"
                                        className={classes.formControl}
                                        name="EAN"
                                        value={form.EAN}
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
                                        id="LivingAddress"
                                        label="Adresse"
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

                                </div>


                                <Button
                                    className={classes.formControl}
                                    variant="contained"
                                    color="primary"
                                    type="submit"
                                    disabled={!canBeSubmitted()}
                                >
                                    Tilføj
                                </Button>

                            </div>

                        </form>

                    </div>
                }
                innerScroll
            />

        );
    }
    else {
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
                                    <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/customer/overview" color="inherit">
                                        <Icon className="mr-4 text-20">arrow_back</Icon>
                                        Kunder
                                </Typography>
                                </FuseAnimate>

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography className="text-16 sm:text-20 truncate">
                                        {form.CustomerType} Kunde: {form.Name} CVR: {form.CVR}
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
                        <Tab className="h-64 normal-case" label="Kunde detaljer" />
                    </Tabs>
                }
                content={
                    <div>

                        <form noValidate onSubmit={handleSubmit} >
                            <div className="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                <FormControl variant="outlined" className={classes.formControl}>
                                    <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                        Kunde Type
                                </InputLabel>
                                    <Select
                                        labelid="demo-simple-select-outlined-label"
                                        id="CustomerType"
                                        name="CustomerType"
                                        value={form.CustomerType}
                                        onChange={handleChange}
                                        required
                                        labelWidth={labelWidth}
                                    >
                                        <MenuItem value="Private">Privat</MenuItem>
                                        <MenuItem value="Business">Virksomhed</MenuItem>
                                        <MenuItem value="Public">Offentlig</MenuItem>

                                    </Select>
                                </FormControl>
                                <div>

                                    <TextField
                                        id="Name"
                                        label="Navn"
                                        className={classes.formControl}
                                        name="Name"
                                        value={form.Name}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />
                                    <TextField
                                        id="CVR"
                                        label="CVR"
                                        className={classes.formControl}
                                        name="CVR"
                                        value={form.CVR}
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
                                        id="LivingAddress"
                                        label="Adresse"
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

                                </div>


                                <Button
                                    className={classes.formControl}
                                    variant="contained"
                                    color="primary"
                                    type="submit"
                                    disabled={!canBeSubmitted()}
                                >
                                    Tilføj
                                </Button>

                            </div>

                        </form>

                    </div>
                }
                innerScroll
            />

        );
    }
    
    
}

export default withReducer('customerReducer', reducer)(Customer);
