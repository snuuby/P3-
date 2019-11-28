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
import withReducer from './../../store/withReducer';
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
    Firstname: '',
    Lastname: '',
    Type: '',
    Address: '',
    ZIP: '',
    City: '',
    Note: '',
    Phonenumber: '',
    Email: ''
    
    // NAME
    // EAN
    // CVR
};

function InspectionReport(props) {
    const classes = useStyles();

    const dispatch = useDispatch();
    const { form, handleChange, setForm } = useForm(defaultFormState);
    const eventDialog = useSelector(({ makeReducer }) => makeReducer.customers.eventDialog);

    // skal skiftes til customers, men har vi overhovedet brug for at loade vores customers her? :/ når vi laver
    const customers = useSelector(({ makeReducer }) => makeReducer.customers.entities);

    let end = moment(form.end).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);
    let start = moment(form.start).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);
    const inputLabel = React.useRef(null);
    const [labelWidth, setLabelWidth] = useState(0);


    const initDialog = useCallback(
        () => {
            const event = {start: start};
            if (eventDialog.type === 'new') {
                //dispatch(Actions.getAvailableEmployees());
                //dispatch(Actions.getAvailableCars());
                //dispatch(Actions.getCustomers());
                setForm({
                    ...defaultFormState,        
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

    function handleSubmit(event) {


        event.preventDefault();

        dispatch(Actions.addCustomer(form));

        // flytter os hen på 
        props.history.push('/customer/overview');

        
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
                                    cReAtE cuStoMeR
                                    </Typography>
                            </FuseAnimate>

                        </div>
                    </div>
                </div>

            }
            content={
                <div>
                    <AppBar position="static">
                    
                    <form noValidate onSubmit={handleSubmit} >
                            <div class="flex-1 bg-gray-0 h-12 pr-1 pt-64">
                                
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

                                    <TextField
                                        id="Type"
                                        label="Rolle"
                                        className={classes.formControl}
                                        name="Type"
                                        value={form.Type}
                                        onChange={handleChange}
                                        variant="outlined"
                                        autoFocus
                                        InputLabelProps={{
                                            shrink: true
                                        }}
                                        required
                                    />

                                    <TextField
                                        id="Address"
                                        label="Adresse"
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
                                        id="Note"
                                        label="Note"
                                        className={classes.formControl}
                                        name="Note"
                                        value={form.Note}
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
                    </AppBar>

                    </div>
            }
            innerScroll
        />

    );
}

export default withReducer('makeReducer', reducer)(InspectionReport);
