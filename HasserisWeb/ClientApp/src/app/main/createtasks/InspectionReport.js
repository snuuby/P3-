import React, { useCallback, useEffect, useState } from 'react';
import { TextField, Button, Dialog, DialogActions, DialogContent, Icon, IconButton, Typography, Toolbar, AppBar, FormControlLabel, Switch } from '@material-ui/core';
import { FuseAnimate, FusePageCarded } from '@fuse';
import { useForm } from '@fuse/hooks';
import { makeStyles } from '@material-ui/core/styles';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import FormHelperText from '@material-ui/core/FormHelperText';
import FormControl from '@material-ui/core/FormControl';
import { Link } from 'react-router-dom';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import withReducer from '../../store/withReducer';
import * as Actions from './store/actions';
import reducer from './store/reducers';
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
    name: '',
    employee: null,
    customer: null,
    car: null,
    start: new Date(),
    end: new Date(),
    desc: '',
    combo: '',
    image: '',
    destination: null,

    //Moving task specific properties
    furniture: null,
    startingaddress: null,
    lentboxes: null,

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


    let start = moment(form.start).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);
    let end = moment(form.end).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);
    const inputLabel = React.useRef(null);
    const [labelWidth, setLabelWidth] = useState(0);

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
                    ...defaultFormState,
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
            form.availableCars && form.availableEmployees && form.customers && (form.title.length > 0)
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


                        <div className="flex flex-col min-w-0 items-center sm:items-start">

                            <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                <Typography className="text-16 sm:text-20 truncate">
                                    Lav besigtigelsesrapport
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
            content={
                <div>
                    <AppBar position="absolute">
                    
                    <form noValidate onSubmit={handleSubmit} >
                            <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                
                                <TextField
                                    id="name"
                                    label="Navn"
                                    className="mt-8 mb-16"
                                    name="name"
                                    value={form.Name}
                                    onChange={handleChange}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                                />



                                <TextField
                                    id="start"
                                    name="start"
                                    label="Start"
                                    type="datetime-local"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    inputProps={{
                                        max: end
                                    }}
                                    value={start}
                                    onChange={handleChange}
                                    variant="outlined"
                                />

                                <TextField
                                    id="end"
                                    name="end"
                                    label="Slut"
                                    type="datetime-local"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    inputProps={{
                                        min: start
                                    }}
                                    value={end}
                                    onChange={handleChange}
                                    variant="outlined"
                                />


                                <div>
                                    <FormControl variant="outlined" className={classes.formControl}>
                                        <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                            Kunde
                                        </InputLabel>
                                        <Select
                                            labelId="demo-simple-select-outlined-label"
                                            id="customer"
                                            name="customer"
                                            value={form.customer}
                                            onChange={handleChange}
                                            labelWidth={labelWidth}
                                            >
                                            customers && {customers.map(customer =>
                                                <MenuItem>{customer.ID + ' ' + customer.Firstname}</MenuItem>
                                            ) }

                                        </Select>
                                    </FormControl>
                                    <FormControl variant="outlined" className={classes.formControl}>
                                        <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                            Ansat
                                        </InputLabel>
                                        <Select
                                            labelId="demo-simple-select-outlined-label"
                                            id="employee"
                                            name="employee"                                            
                                            onChange={handleChange}
                                            name="employee"
                                            value={form.employee}

                                            labelWidth={labelWidth}
                                        >
                                            employees && {employees.map(employee => 
                                                <MenuItem>{employee.ID + ' ' + employee.Firstname}</MenuItem>
                                            )}

                                        </Select>
                                    </FormControl>
                                        <FormControl variant="outlined" className={classes.formControl}>
                                            <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                                Bil
                                            </InputLabel>
                                            <Select
                                                labelId="demo-simple-select-outlined-label"
                                                id="car"
                                                name="car"   
                                                value={form.car}
                                                onChange={handleChange}
                                                labelWidth={labelWidth}
                                            >
                                                cars && {cars.map(car =>
                                                    <MenuItem>{car.ID + ' ' + car.RegNum + ' ' + car.Model}</MenuItem>
                                                )}

                                            </Select>
                                    </FormControl>
                                 </div>

                                <TextField
                                    className="mt-8 mb-16"
                                    id="desc" label="Beskrivelse"
                                    type="text"
                                    name="desc"
                                    value={form.desc}
                                    onChange={handleChange}
                                    multiline rows={5}
                                    variant="outlined"
                                    fullWidth
                                />


                                <Button
                                    variant="contained"
                                    color="primary"
                                    type="submit"
                                    disabled={!canBeSubmitted()}
                                >
                                    Tilføj
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
