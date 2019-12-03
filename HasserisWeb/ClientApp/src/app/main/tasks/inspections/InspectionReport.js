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
import withReducer from '../../../store/withReducer';
import * as Actions from '../store/actions';
import reducer from '../store/reducers';
import { Select } from '@material-ui/core';
import moment from 'moment';
import { useDispatch, useSelector } from 'react-redux';
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
    CustomerName: '',
    EmployeeID: null,
    CustomerID: null,
    CarID: null,
    Employee: null,
    Customer: null,
    Car: null,
   
    Tool: null,
    InspectionDate: new Date(),
    MovingDate: new Date(),
    End: new Date(),
    Notes: '',
    combo: '',
    Image: '',
    Destination: null,
    ExpectedHours: null,

    //Address
    StartAddress: null,
    StartZIP: null,
    StartCity: null,
    DestinationAddress: null,
    DestinationZIP: null,
    DestinationCity: null,
    //Moving task specific properties
    furniture: null,
    startingaddress: null,
    Lentboxes: null,

    //Delivery task specific properties
    material: null,
    quantity: null,
};

function InspectionReport(props) {
    const classes = useStyles();
    const [tabValue, setTabValue] = useState(0);

    const dispatch = useDispatch();
    const { form, handleChange, setForm } = useForm(defaultFormState);
    const eventDialog = useSelector(({ inspectionReducer }) => inspectionReducer.inspections.eventDialog);

    const customers = useSelector(({ inspectionReducer }) => inspectionReducer.inspections.customers);
    const employees = useSelector(({ inspectionReducer }) => inspectionReducer.inspections.availableEmployees);
    const cars = useSelector(({ inspectionReducer }) => inspectionReducer.inspections.availableCars);


    let end = moment(form.end).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);
    let start = moment(form.start).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);
    const inputLabel = React.useRef(null);
    const [labelWidth, setLabelWidth] = useState(0);

    useEffect(() => {
        dispatch(Actions.getInspectionReport(props.match.params));
    }, [props.match.params]);

    useEffect(() => {
        setLabelWidth(inputLabel.current.offsetWidth + 20);
    }, []);

    const initDialog = useCallback(
        () => {
            const event = {start: start};
            if (eventDialog.type === 'new') {
                dispatch(Actions.getAvailableEmployees());
                dispatch(Actions.getAvailableCars());
                dispatch(Actions.getCustomers());
                dispatch(Actions.getAvailableTools());

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
        dispatch(Actions.closeNewInspectionReport());
    }

    function canBeSubmitted() {
        return (
            form.Customer && form.Car && form.Employee 
        );
    }

    function handleSubmit(event) {
        event.preventDefault();

        dispatch(Actions.editInspectionReport(form));
        
        closeComposeDialog();
        
    }
    function OfferSubmit(event)
    {
        event.preventDefault();
        dispatch(Actions.addFromInspectionReport(form));

        props.history.push('/Offers/Create/');
    }
    function TaskSubmit(event) {
        event.preventDefault();
        dispatch(Actions.addTaskFromInspectionReport(form));

        props.history.push('/Offers/Create/');
    }
    function handleChangeTab(event, tabValue) {
        setTabValue(tabValue);
    }
    function handleCustomer(customer) {
        customer.preventDefault();
        if (customer.target.value != null) {
            form.CustomerName = customer.target.value.Firstname + ' ' + customer.target.value.Lastname;
        }
        else {
            form.CustomerName = '';
        }

        handleChange(customer);

    }
    function transferToTask() {

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
                            <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/inspections/overview" color="inherit">
                                <Icon className="mr-4 text-20">arrow_back</Icon>
                                Besigtigelsesrapporter
                                </Typography>
                        </FuseAnimate>
                        <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                            <Typography variant="caption">
                                {'Besigtigelses ID: ' + form.ID}
                            </Typography>
                        </FuseAnimate>
                        <div>
                            <Button
                                className={classes.formControl}
                                variant="contained"
                                color="primary"
                                type="submit"
                                disabled={!canBeSubmitted()}
                                onClick={OfferSubmit}
                            >
                                Overfør til tilbud
                                </Button>
                            <Button
                                className={classes.formControl}
                                variant="contained"
                                color="primary"
                                type="submit"
                                disabled={!canBeSubmitted()}
                                onClick={TaskSubmit}
                            >
                                Overfør til opgave
                                </Button>
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
                    <Tab className="h-64 normal-case" label="Besigtigelsesrapport detaljer" />
                </Tabs>
            }
            content={
                <div>
                    
                    <form noValidate onSubmit={handleSubmit} >
                            <div class="p-16 sm:p-24 max-w-2xl w-full">
                                
                                <TextField
                                    id="CustomerName"
                                    label="Kunde navn"
                                    className={classes.formControl}
                                    name="CustomerName"
                                    value={form.CustomerName}
                                    variant="outlined"
                                    autoFocus
                                    fullWidth
                                    disabled
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

                                    <div>
                                        <FormControl variant="outlined" className={classes.formControl}>
                                            <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                                Kunde
                                            </InputLabel>
                                            <Select
                                                labelId="demo-simple-select-outlined-label"
                                                id="Customer"
                                                name="CustomerID"
                                                value={form.CustomerID}
                                                onChange={handleCustomer}
                                                required

                                                labelWidth={labelWidth}
                                                >
                                                <MenuItem value={null}>Ingen</MenuItem>
    
                                                customers && {customers.map(customer =>
                                                    <MenuItem value={customer.ID}> {customer.CustomerType == "Private" ? customer.ID + ' ' + customer.Firstname + ' ' + customer.Lastname : customer.ID + ' ' + customer.Name}</MenuItem>
                                                ) }

                                            </Select>
                                        </FormControl>
                                        <FormControl variant="outlined" className={classes.formControl}>
                                            <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                                Ansat
                                            </InputLabel>
                                        <Select
                                                labelId="demo-simple-select-outlined-label"
                                                id="Employee"
                                                name="EmployeeID"                                            
                                                onChange={handleChange}
                                                value={form.EmployeeID}
                                                required

                                                labelWidth={labelWidth}
                                            >
                                                <MenuItem value={null}>Ingen</MenuItem>
                                            employees && {employees.map(employee =>
                                                <MenuItem value={employee.ID}> {employee.ID + ' ' + employee.Firstname + ' ' + employee.Lastname}</MenuItem>
                                            )}

                                            </Select>
                                        </FormControl>
                                            <FormControl variant="outlined" className={classes.formControl}>
                                                <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                                    Bil
                                                </InputLabel>
                                                <Select
                                                    labelId="demo-simple-select-outlined-label"
                                                    id="Car"
                                                    name="CarID"   
                                                    value={form.CarID}
                                                    onChange={handleChange}
                                                    labelWidth={labelWidth}
                                                    required
                                                >
                                                <MenuItem value={null}>Ingen</MenuItem>

                                                    cars && {cars.map(car =>
                                                        <MenuItem value={car.ID}>{car.ID + ' ' + car.RegNum + ' ' + car.Model}</MenuItem>
                                                    )}

                                                </Select>
                                </FormControl>

                                    </div>

                                <TextField
                                    className={classes.formControl}
                                    id="Notes" label="Beskrivelse"
                                    type="text"
                                    name="Notes"
                                    value={form.Notes}
                                    onChange={handleChange}
                                    multiline rows={5}
                                    variant="outlined"

                                    fullWidth
                                />


                                

                                


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

export default withReducer('inspectionReducer', reducer)(InspectionReport);
