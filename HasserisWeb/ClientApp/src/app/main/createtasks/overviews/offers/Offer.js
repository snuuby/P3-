import React, { useCallback, useEffect, useState } from 'react';
import { TextField, Button, Tab, Tabs, Tooltip, Dialog, DialogActions, DialogContent, Icon, IconButton, Typography, Toolbar, AppBar, FormControlLabel, Switch } from '@material-ui/core';
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
    Customer: null,
    CustomerName: '',
    CustomerMail: '',
    Start: new Date(),
    End: new Date(),
    Notes: '',
    combo: '',
    Image: '',
    Destination: null,
    ExpectedHours: null,

    //Offer mail specification
    OfferType: '',

    InspectionDate: new Date(),
    MovingDate: new Date(),
    ExpirationDate: new Date(),
    //Address
    StartAddress: null,
    StartZIP: null,
    StartCity: null,
    DestinationAddress: null,
    DestinationZIP: null,
    DestinationCity: null,
    //Moving task specific properties
    furniture: null,
    Lentboxes: 0,

    //Delivery task specific properties
    material: null,
    quantity: null,
};

function Offer(props) {
    const classes = useStyles();
    const [tabValue, setTabValue] = useState(0);

    const dispatch = useDispatch();
    const { form, handleChange, setForm } = useForm(defaultFormState);
    const eventDialog = useSelector(({ makeReducer }) => makeReducer.offers.eventDialog);

    const customers = useSelector(({ makeReducer }) => makeReducer.offers.customers);

    let end = moment(form.end).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);
    let start = moment(form.start).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);
    const inputLabel = React.useRef(null);
    const [labelWidth, setLabelWidth] = useState(0);

    useEffect(() => {
        dispatch(Actions.getOffer(props.match.params));
    }, [props.match.params]);

    useEffect(() => {
        setLabelWidth(inputLabel.current.offsetWidth + 20);
    }, []);

    const initDialog = useCallback(
        () => {
            const event = {start: start};
            if (eventDialog.type === 'new') {
                dispatch(Actions.getCustomers());
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
        dispatch(Actions.closeNewOffer());
    }

    function canBeSubmitted() {
        return (
            form.Customer
        );
    }

    function handleSubmit(event) {
        event.preventDefault();

        if (form.wasInspection) {
            dispatch(Actions.addOfferFromInspection(form));
        }
        else
        {
            dispatch(Actions.addOffer(form));
        }
        
        closeComposeDialog();
        
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

                        <FuseAnimate animation="transition.slideRightIn" delay={300}>
                            <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/offers/overview" color="inherit">
                                <Icon className="mr-4 text-20">arrow_back</Icon>
                                Tilbud
                                </Typography>
                        </FuseAnimate>

  

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
                                        onChange={handleChange}
                                        required

                                        labelWidth={labelWidth}
                                    >
                                        <MenuItem value={null}>Ingen</MenuItem>

                                        customers && {customers.map(customer =>
                                            <MenuItem value={customer}> {customer.ID + ' ' + customer.Firstname}</MenuItem>
                                        )}

                                    </Select>
                                </FormControl>
                            <div>
                                <TextField
                                        id="CustomerName"
                                        label="Kunde navn"
                                        className={classes.formControl}
                                        name="CustomerName"
                                        value={form.CustomerName}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        disabled
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
                                        disabled
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />
                            </div>
                                    <div>
                                        <TextField
                                            id="StartingAddress"
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
                                            id="StartingZIP"
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

                                    variant="outlined"
                                />

                                <div>

                                    <TextField
                                        className={classes.formControl}
                                        id="Lentboxes" label="Lånte boxe"
                                        type="number"
                                        min="0"
                                        max="10"
                                        name="Lentboxes"
                                        value={form.Lentboxes}
                                        defaultValue={0}
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

export default withReducer('makeReducer', reducer)(Offer);
