import React, {useCallback, useEffect} from 'react';
import {TextField, Button, Dialog, DialogActions, DialogContent, Icon, IconButton, Typography, Toolbar, AppBar, FormControlLabel, Switch} from '@material-ui/core';
import FuseUtils from '@fuse/FuseUtils';
import {useForm} from '@fuse/hooks';
import {useDispatch, useSelector} from 'react-redux';
import moment from 'moment';
import * as Actions from '../apps/calendar/store/actions';
import * as ActionsAdd from './store/actions';
import Checkbox from "@material-ui/core/Checkbox";
import Select from "react-select";



const defaultFormState = {
    id    : FuseUtils.generateGUID(),
    title : '',
    allDay: true,
    desc  : ''
};

function AddDialog(props)
{
    const dispatch = useDispatch();
    const eventDialog = useSelector(({overviewReducer}) => overviewReducer.employees.eventDialog);

    const {form, handleChange, setForm} = useForm(defaultFormState);
    let start = moment(form.start).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);
    let end = moment(form.end).format(moment.HTML5_FMT.DATETIME_LOCAL_SECONDS);

    const initDialog = useCallback(
        () => {
            /**
             * Dialog type: 'edit'
             */
            if ( eventDialog.type === 'edit' && eventDialog.data )
            {
                setForm({...eventDialog.data});
            }

            /**
             * Dialog type: 'new'
             */
            if ( eventDialog.type === 'new' )
            {
                setForm({
                    ...defaultFormState,
                    ...eventDialog.data,
                    id: FuseUtils.generateGUID()
                });
            }
        },
        [eventDialog.data, eventDialog.type, setForm],
    );

    useEffect(() => {
        /**
         * After Dialog Open
         */
        if ( eventDialog.props.open )
        {
            initDialog();
        }
    }, [eventDialog.props.open, initDialog]);

    function closeComposeDialog()
    {
        eventDialog.type === 'edit' ? dispatch(Actions.closeEditEventDialog()) : dispatch(ActionsAdd.closeNewAddDialog());
    }

    function canBeSubmitted()
    {
        return (
            form.title.length > 0
        );
    }

    function handleSubmit(event)
    {
        event.preventDefault();

        if ( eventDialog.type === 'new' )
        {
            dispatch(Actions.addEvent(form));
        }
        else
        {
            dispatch(Actions.updateEvent(form));
        }
        closeComposeDialog();
    }

    function handleRemove()
    {
        dispatch(Actions.removeEvent(form.id));
        closeComposeDialog();
    }

    return (
        <Dialog {...eventDialog.props} onClose={closeComposeDialog} fullWidth maxWidth="xs" component="form">

            <AppBar position="static">
                <Toolbar className="flex w-full">
                    <Typography variant="subtitle1" color="inherit">
                        {eventDialog.type === 'new' ? 'New Event' : 'Edit Event'}
                    </Typography>
                </Toolbar>
            </AppBar>

            <form noValidate onSubmit={handleSubmit}>
                <DialogContent classes={{root: "p-16 pb-0 sm:p-24 sm:pb-0"}}>
                    <TextField
                        id="fornavn"
                        label="Fornavn"
                        className="mt-8 mb-16"
                        InputLabelProps={{
                            shrink: true
                        }}
                        inputProps={{
                            max: end
                        }}
                        name="fornavn"
                        value="jim"
                        onChange={handleChange}
                        variant="outlined"
                        autoFocus
                        required
                        fullWidth
                    />

                    <TextField
                        id="efternavn"
                        label="Efternavn"
                        className="mt-8 mb-16"
                        InputLabelProps={{
                            shrink: true
                        }}
                        inputProps={{
                            max: end
                        }}
                        name="efternavn"
                        value="testson"
                        variant="outlined"
                        autoFocus
                        required
                        fullWidth
                    />

                    <Select
                        textField='name'
                        groupBy='lastName'
                    />
                    
                    <Checkbox
                        id="admin"
                        name="admin"
                        label="Admin"
                        variant="Outlined"
                        value="checked"
                        />

                    <TextField
                        id="end"
                        name="end"
                        label="End"
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
                        fullWidth
                    />

                    <TextField
                        className="mt-8 mb-16"
                        id="desc" label="Description"
                        type="text"
                        name="desc"
                        value={form.desc}
                        onChange={handleChange}
                        multiline rows={5}
                        variant="outlined"
                        fullWidth
                    />
                </DialogContent>

                {eventDialog.type === 'new' ? (
                    <DialogActions className="justify-between pl-8 sm:pl-16">
                        <Button
                            variant="contained"
                            color="primary"
                            type="submit"
                            disabled={!canBeSubmitted()}
                        >
                            Add
                        </Button>
                    </DialogActions>
                ) : (
                    <DialogActions className="justify-between pl-8 sm:pl-16">
                        <Button
                            variant="contained"
                            color="primary"
                            type="submit"
                            disabled={!canBeSubmitted()}
                        > Save
                        </Button>
                        <IconButton onClick={handleRemove}>
                            <Icon>delete</Icon>
                        </IconButton>
                    </DialogActions>
                )}
            </form>
        </Dialog>
    );
}

export default AddDialog;
