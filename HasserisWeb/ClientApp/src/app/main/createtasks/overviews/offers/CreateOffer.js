import React, { useCallback, useEffect, useState } from 'react';
import { TextField, Button, Dialog,Tab, Tabs, Tooltip, DialogActions, DialogContent, Icon, IconButton, Typography, Toolbar, AppBar, FormControlLabel, Switch } from '@material-ui/core';
import { FuseAnimate, FusePageCarded, FuseChipSelect, SelectFormsy } from '@fuse';
import { useForm } from '@fuse/hooks';
import { makeStyles } from '@material-ui/core/styles';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import FormHelperText from '@material-ui/core/FormHelperText';
import FormControl from '@material-ui/core/FormControl';
import { Link } from 'react-router-dom';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import withReducer from '../../../../store/withReducer';
import * as Actions from '../../store/actions';
import reducer from '../../store/reducers';
import { Select } from '@material-ui/core';
import moment from 'moment';
import { useDispatch, useSelector } from 'react-redux';
import Checkbox from '@material-ui/core/Checkbox';
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
    //Common task properties
    Name: '',
    CustomerName: '',
    CustomerMail: '',
    InspectionReport: null,
    Employee: null,
    Customer: null,
    Car: null,
    Start: new Date(),
    End: new Date(),
    Notes: '',
    combo: '',
    Image: '',
    Destination: null,
    ExpectedHours: null,

    //Offer mail specification
    OfferType: '',

    //Address
    StartAddress: null,
    StartZIP: null,
    StartCity: null,
    DestinationAddress: null,
    DestinationZIP: null,
    DestinationCity: null,
    //Moving task specific properties
    furniture: null,
    Lentboxes: null,

    //Delivery task specific properties
    material: null,
    quantity: null,
};

function OfferReport(props) {
    const classes = useStyles();
    const [tabValue, setTabValue] = useState(0);

    const dispatch = useDispatch();
    const { form, handleChange, setForm } = useForm(defaultFormState);
    const eventDialog = useSelector(({ makeReducer }) => makeReducer.offers.eventDialog);

    const customers = useSelector(({ makeReducer }) => makeReducer.offers.customers);
    const employees = useSelector(({ makeReducer }) => makeReducer.offers.availableEmployees);
    const cars = useSelector(({ makeReducer }) => makeReducer.offers.availableCars);

    let end = moment(form.end).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);
    let start = moment(form.start).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);
    const inputLabel = React.useRef(null);
    const [labelWidth, setLabelWidth] = useState(0);

    useEffect(() => {
        setLabelWidth(inputLabel.current.offsetWidth + 20);
    }, []);
    const initDialog = useCallback(
        () => {
            const event = {start: start};
            if (!eventDialog.wasInspection) {
                dispatch(Actions.getCustomers());
                setForm({
                    ...defaultFormState,        
                    ...eventDialog.data,
                });
            }
            if (eventDialog.wasInspection) {

                setForm({
                    ...eventDialog.data,
                });
            }


        },
        [eventDialog.data, eventDialog.type, setForm, eventDialog.wasInspection],
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
        dispatch(Actions.closeNewOffer());
    }

    function canBeSubmitted() {
        return (
            form.InspectionDate && form.MovingDate && form.ExpirationDate && form.OfferType
        );
    }

    function handleSubmit(event) {
        event.preventDefault();
        props.history.push('/offers/overview');

        dispatch(Actions.addOffer(form));
        
        closeComposeDialog();
        
    }
    function handleCustomer(customer) {
        customer.preventDefault();
        if (customer.target.value != null) {
            form.CustomerName = customer.target.value.Firstname + ' ' + customer.target.value.Lastname;
            form.CustomerMail = customer.target.value.ContactInfo.Email;
        }
        else {
            form.CustomerName = '';
            form.CustomerMail = '';
        }

        handleChange(customer);

    }
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
                <div className="flex flex-1 w-full items-center justify-between">

                    <div className="flex flex-1 flex-col items-center sm:items-start">


                        <div className="flex flex-col min-w-0 items-center sm:items-start">

                            <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                <Typography className="text-16 sm:text-20 truncate">
                                    Lav Tilbud
                                    </Typography>
                            </FuseAnimate>
                            {eventDialog.data.wasInspection && <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                <Typography className="text-16 sm:text-20 truncate">
                                    (Var besigtigelsesrapport ID): {form.InspectionReport}
                                    </Typography>
                                </FuseAnimate>  }   

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
                    <Tab className="h-64 normal-case" label="Tilbud detaljer" />
                </Tabs>
            }
            content={
                <div>

                        <form noValidate onSubmit={handleSubmit} >
                            <div class="p-16 sm:p-24 max-w-2xl w-full">
                            <FormControl variant="outlined" className={classes.formControl}>
                                    <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                        Tilbuds type
                                        </InputLabel>
                                    <Select
                                        labelId="demo-simple-select-outlined-label"
                                        id="OfferType"
                                        name="OfferType"
                                        value={form.OfferType}
                                        onChange={handleChange}
                                        required

                                        labelWidth={labelWidth}
                                    >
                                        <MenuItem value="Private">Privat</MenuItem>
                                        <MenuItem value="With Packing">Privat med nedpakning</MenuItem>
                                        <MenuItem value="Business">Virksomhed</MenuItem>


                                    </Select>
                                </FormControl>
                                <FormControl variant="outlined" className={classes.formControl}>
                                    <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                        Kunde
                                        </InputLabel>
                                    <Select
                                        labelId="demo-simple-select-outlined-label"
                                        id="Customer"
                                        name="Customer"
                                        value={form.Customer}
                                        onChange={handleCustomer}
                                        required

                                        labelWidth={labelWidth}
                                    >
                                        <MenuItem value={null}>Ingen</MenuItem>

                                        customers && {customers.map(customer =>
                                            <MenuItem value={customer}> {customer.ID + ' ' + customer.Firstname}</MenuItem>
                                        )}

                                    </Select>
                                </FormControl>
                                <TextField
                                        id="CustomerName"
                                        label="Kunde navn"
                                        className={classes.formControl}
                                        name="CustomerName"
                                        value={form.CustomerName}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />
                                    <TextField
                                        id="CustomerMail"
                                        label="Kunde email"
                                        className={classes.formControl}
                                        name="CustomerMail"
                                        value={form.CustomerMail}
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
                                        id="StartAddress"
                                        label="Fra addresse"
                                        className={classes.formControl}
                                        name="StartAddress"
                                        value={form.StartAddress}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />
                                    <TextField
                                        id="StartZIP"
                                        label="Fra ZIP"
                                        className={classes.formControl}
                                        name="StartZIP"
                                        value={form.StartZIP}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />
                                    <TextField
                                        id="StartCity"
                                        label="Fra by"
                                        className={classes.formControl}
                                        name="StartCity"
                                        value={form.StartCity}
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
                                        id="DestinationAddress"
                                        label="Til addresse"
                                        className={classes.formControl}
                                        name="DestinationAddress"
                                        value={form.DestinationAddress}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />
                                    <TextField
                                        id="DestinationZIP"
                                        label="Til ZIP"
                                        className={classes.formControl}
                                        name="DestinationZIP"
                                        value={form.DestinationZIP}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />
                                    <TextField
                                        id="DestinationCity"
                                        label="Til by"
                                        className={classes.formControl}
                                        name="DestinationCity"
                                        value={form.DestinationCity}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />
                                </div>


                                <TextField
                                    id="InspectionDate"
                                    name="InspectionDate"
                                    label="Besigtigelses dato"
                                    type="datetime-local"
                                    className={classes.formControl}
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    value={form.InspectionDate}
                                    onChange={handleChange}
                                    required

                                    variant="outlined"
                                />
                                <TextField
                                    id="MovingDate"
                                    name="MovingDate"
                                    label="Flyttedato"
                                    type="datetime-local"
                                    className={classes.formControl}
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    value={form.MovingDate}
                                    onChange={handleChange}
                                    required

                                    variant="outlined"
                                />
                                <TextField
                                    id="ExpirationDate"
                                    name="ExpirationDate"
                                    label="Tilbudsophør dato"
                                    type="datetime-local"
                                    className={classes.formControl}
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    value={form.ExpirationDate}
                                    onChange={handleChange}
                                    required
                                    helperText="14 dage efter flyttedato"
                                    variant="outlined"
                                />


                                <div>

                                <TextField
                                    className={classes.formControl}
                                    id="Lentboxes" label="Lånte boxe"
                                    type="number"
                                    min="0"
                                    defaultValue={0}
                                    max="10"
                                    name="Lentboxes"
                                    value={form.Lentboxes}
                                    onChange={handleChange}
                                    variant="outlined"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                />

                                <TextField
                                    className={classes.formControl}
                                    id="ExpectedHours" label="Forventet timeantal"
                                    type="number"
                                    min="0"
                                    max="10"
                                    name="ExpectedHours"
                                    defaultValue={2}
                                    value={form.ExpectedHours}
                                    onChange={handleChange}
                                    variant="outlined"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                />
                                    </div>
                                <Button
                                    className={classes.formControl}
                                    variant="contained"
                                    color="primary"
                                    type="submit"
                                    disabled={!canBeSubmitted()}
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

export default withReducer('makeReducer', reducer)(OfferReport);