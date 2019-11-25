import React, { useCallback, useEffect, useState } from 'react';
import { TextField, Button, Dialog, DialogActions, DialogContent, Icon, IconButton, Typography, Toolbar, AppBar, FormControlLabel, Switch } from '@material-ui/core';
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
import InputNumber from 'react-input-number';
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
    Name: '',
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

    //Address
    Address: null,
    ZIP: null, 
    City: null, 
    AddressNote: null,
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

    const dispatch = useDispatch();
    const { form, handleChange, setForm } = useForm(defaultFormState);
    const eventDialog = useSelector(({ makeReducer }) => makeReducer.inspections.eventDialog);

    const customers = useSelector(({ makeReducer }) => makeReducer.inspections.customers);
    const employees = useSelector(({ makeReducer }) => makeReducer.inspections.availableEmployees);
    const cars = useSelector(({ makeReducer }) => makeReducer.inspections.availableCars);

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
            form.Customer && form.Car && form.Employee && (form.Name.length > 0)
        );
    }

    function handleSubmit(event) {
        event.preventDefault();

        dispatch(Actions.addInspectionReport(form));
        
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

                        <FuseAnimate animation="transition.slideRightIn" delay={300}>
                            <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/inspections/overview" color="inherit">
                                <Icon className="mr-4 text-20">arrow_back</Icon>
                                Besigtigelsesrapporter
                                </Typography>
                        </FuseAnimate>

  

                    </div>
                </div>
            }
            content={
                <div>
                    <AppBar position="static">
                    
                    <form noValidate onSubmit={handleSubmit} >
                            <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                
                                <TextField
                                    id="Name"
                                    label="Navn"
                                    className={classes.formControl}
                                    name="Name"
                                    value={form.Name}
                                    onChange={handleChange}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />
                                
                                    <TextField
                                        id="Address"
                                        label="Addresse"
                                        className={classes.formControl}
                                        name="Address"
                                        value={form.Address}
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
                                        label="ZIP"
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
                                        label="City"
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
                                    <TextField
                                        id="AddressNote"
                                        label="Noter til addressen"
                                        className={classes.formControl}
                                        name="AddressNote"
                                        value={form.AddressNote}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />




                                <TextField
                                    id="Start"
                                    name="Start"
                                    label="Start"
                                    type="datetime-local"
                                    className={classes.formControl}
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    value={form.Start}
                                    onChange={handleChange}
                                    required

                                    variant="outlined"
                                />


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
                                    id="Lentboxes" label="Lånte boxe"
                                    type="number"
                                    name="Lentboxes"
                                    value={form.Lentboxes}
                                    onChange={handleChange}
                                    multiline rows={5}
                                    variant="outlined"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                />

                                <TextField
                                    className={classes.formControl}
                                    id="ExpectedHours" label="Forventet timeantal"
                                    type="number"

                                    name="ExpectedHours"
                                    value={form.ExpectedHours}
                                    onChange={handleChange}
                                    multiline rows={5}
                                    variant="outlined"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
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
                    </AppBar>

                    </div>
            }
            innerScroll
        />

    );
}

export default withReducer('makeReducer', reducer)(InspectionReport);
