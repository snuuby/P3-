import React, { useCallback, useEffect, useState } from 'react';
import { TextField, Button, Dialog, DialogActions, DialogContent, Icon, Tabs, Tab, IconButton, Typography, Toolbar, AppBar, FormControlLabel, Switch } from '@material-ui/core';
import { FuseAnimate, FusePageCarded, FuseChipSelect, SelectFormsy } from '@fuse';
import { useForm } from '@fuse/hooks';
import { makeStyles } from '@material-ui/core/styles';
import { Link } from 'react-router-dom';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import withReducer from './../../../store/withReducer';
import * as Actions from '../store/actions';
import reducer from '../store/reducers';
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
    StartingAddress: null,
    Destination: null,
    Employee: null,
    Customer: null,
    Car: null,
    InspectionDate: new Date(),
    MovingDate: new Date(),
    DeliveryDate: new Date(),
    InspectionReport: null,
    Offer: null,
    End: new Date(),
    Notes: '',
    combo: '',
    Image: '',
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
    Lentboxes: null,

    //Delivery task specific properties
    Material: null,
    Quantity: null,
};

function Task(props) {
    const classes = useStyles();

    const dispatch = useDispatch();
    const { form, handleChange, setForm } = useForm(defaultFormState);
    const eventDialog = useSelector(({ taskReducer }) => taskReducer.tasks.eventDialog);
    const customers = useSelector(({ taskReducer }) => taskReducer.tasks.customers);
    const employees = useSelector(({ taskReducer }) => taskReducer.tasks.availableEmployees);
    const cars = useSelector(({ taskReducer }) => taskReducer.tasks.availableCars);

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
        dispatch(Actions.getTask(props.match.params));
    }, [props.match.params]);
    useEffect(() => {
        setLabelWidth(inputLabel.current.offsetWidth + 20);
    }, []);
    const initDialog = useCallback(
        () => {
            if (eventDialog.type === 'new') {
                dispatch(Actions.getAvailableEmployees());
                dispatch(Actions.getAvailableCars());
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



    function canBeSubmitted() {
        return (
            form.Firstname && form.Lastname && form.Address // hvad gør den her helt præcist? 
        );
    }

    function handleSubmit(event) {


        event.preventDefault();

        if (form.TaskType == "Moving") {
            dispatch(Actions.editMovingTask(form));
        }
        else {
            dispatch(Actions.editDeliveryTask(form));
        }

        // flytter os hen på 
        props.history.push('/tasks/overview');



    }

    function removeTask(event) {
        event.preventDefault();
        //dispatch(Actions.removeTask(form.ID));
        props.history.push('/tasks/overview');

    }

    if (form.TaskType == "Moving") {
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
                                    <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/tasks/overview" color="inherit">
                                        <Icon className="mr-4 text-20">arrow_back</Icon>
                                        Opgaver
                                </Typography>
                                </FuseAnimate>

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography className="text-16 sm:text-20 truncate">
                                        {form.TaskType == "Moving" ? "Flytning" : "Levering"} opgave:
                                    </Typography>
                                </FuseAnimate>
                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography variant="caption">
                                        {'Opgave ID: ' + form.ID}
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
                        <Tab className="h-64 normal-case" label="Opgave detaljer" />
                    </Tabs>
                }
                content={
                    <div>

                        <form noValidate onSubmit={handleSubmit} >
                            <div class="p-16 sm:p-24 max-w-2xl w-full">



                                <div>
                                    <FormControl variant="outlined" className={classes.formControl}>
                                        <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                            Opgave type
                                            </InputLabel>
                                        <Select
                                            labelId="demo-simple-select-outlined-label"
                                            id="TaskType"
                                            name="TaskType"
                                            value={form.TaskType}
                                            onChange={handleChange}
                                            required

                                            labelWidth={labelWidth}
                                        >
                                            <MenuItem value="Moving">Flytning</MenuItem>
                                            <MenuItem value="Delivery">Levering</MenuItem>


                                            )}

                                        </Select>
                                    </FormControl>
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
                                    <FormControl variant="outlined" className={classes.formControl}>
                                        <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                            Ansat
                                            </InputLabel>
                                        <Select
                                            labelId="demo-simple-select-outlined-label"
                                            id="Employee"
                                            name="Employee"
                                            onChange={handleChange}
                                            value={form.Employee}
                                            required

                                            labelWidth={labelWidth}
                                        >
                                            <MenuItem value={null}>Ingen</MenuItem>

                                            employees && {employees.map(employee =>
                                                <MenuItem value={employee}>{employee.ID + ' ' + employee.Firstname}</MenuItem>
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
                                            name="Car"
                                            value={form.Car}
                                            onChange={handleChange}
                                            labelWidth={labelWidth}
                                            required
                                        >
                                            <MenuItem value={null}>Ingen</MenuItem>

                                            cars && {cars.map(car =>
                                                <MenuItem value={car}>{car.ID + ' ' + car.RegNum + ' ' + car.Model}</MenuItem>
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
                                    <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/task/overview" color="inherit">
                                        <Icon className="mr-4 text-20">arrow_back</Icon>
                                        Opgaver
                                </Typography>
                                </FuseAnimate>

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography className="text-16 sm:text-20 truncate">
                                        Opret {form.TaskType} ogpave:
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
                        <Tab className="h-64 normal-case" label="Opgave detaljer" />
                    </Tabs>
                }
                content={
                    <div>

                        <form noValidate onSubmit={handleSubmit} >
                            <div class="p-16 sm:p-24 max-w-2xl w-full">



                                <div>
                                    <FormControl variant="outlined" className={classes.formControl}>
                                        <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                            Opgave type
                                            </InputLabel>
                                        <Select
                                            labelId="demo-simple-select-outlined-label"
                                            id="TaskType"
                                            name="TaskType"
                                            value={form.TaskType}
                                            onChange={handleChange}
                                            required

                                            labelWidth={labelWidth}
                                        >
                                            <MenuItem value="Moving">Flytning</MenuItem>
                                            <MenuItem value="Delivery">Levering</MenuItem>


                                            )}

                                        </Select>
                                    </FormControl>
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

                                    variant="outlined"
                                />
                                <TextField
                                    id="DeliveryDate"
                                    name="DeliveryDate"
                                    label="Leveringsdato"
                                    type="datetime-local"
                                    className={classes.formControl}
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    value={form.DeliveryDate}
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
                                    <FormControl variant="outlined" className={classes.formControl}>
                                        <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                            Ansat
                                            </InputLabel>
                                        <Select
                                            labelId="demo-simple-select-outlined-label"
                                            id="Employee"
                                            name="Employee"
                                            onChange={handleChange}
                                            value={form.Employee}
                                            required

                                            labelWidth={labelWidth}
                                        >
                                            <MenuItem value={null}>Ingen</MenuItem>

                                            employees && {employees.map(employee =>
                                                <MenuItem value={employee}>{employee.ID + ' ' + employee.Firstname}</MenuItem>
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
                                            name="Car"
                                            value={form.Car}
                                            onChange={handleChange}
                                            labelWidth={labelWidth}
                                            required
                                        >
                                            <MenuItem value={null}>Ingen</MenuItem>

                                            cars && {cars.map(car =>
                                                <MenuItem value={car}>{car.ID + ' ' + car.RegNum + ' ' + car.Model}</MenuItem>
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
                                <TextField
                                    className={classes.formControl}
                                    id="Material" label="Materiale"
                                    type="text"
                                    name="Material"
                                    value={form.Material}
                                    onChange={handleChange}
                                    variant="outlined"
                                />
                                <TextField
                                    className={classes.formControl}
                                    id="Quantity" label="Mængde"
                                    type="number"
                                    name="Quantity"
                                    value={form.Quantity}
                                    onChange={handleChange}
                                    variant="outlined"
                                />




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

export default withReducer('taskReducer', reducer)(Task);
