import React, {useCallback, useEffect} from 'react';
import {TextField, Button, Dialog, DialogActions, DialogContent, Icon, IconButton, Typography, Toolbar, AppBar, FormControlLabel, Switch} from '@material-ui/core';
import FuseUtils from '@fuse/FuseUtils';
import {useForm} from '@fuse/hooks';
import {useDispatch, useSelector} from 'react-redux';
import moment from 'moment';
import * as Actions from './store/actions';
import * as ActionsAdd from './store/actions';
import Checkbox from "@material-ui/core/Checkbox";
import Select from "react-select";



const defaultFormState = {
    id    : '',
    name  : ''
};

function AddDialog(props)
{
    const dispatch = useDispatch();
    const eventDialog = useSelector(({toolReducer}) => toolReducer.tools.eventDialog);

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
        eventDialog.type === 'edit' ? dispatch(Actions.closeEditToolDialog()) : dispatch(ActionsAdd.closeNewAddDialog());
    }

    function canBeSubmitted()
    {
        return (
            form.name.length > 0
        );
    }

    function handleSubmit(event)
    {
        event.preventDefault();

        if ( eventDialog.type === 'new' )
        {
            dispatch(Actions.addTool(form));
        }
        else
        {
            dispatch(Actions.updateTool(form));
        }
        closeComposeDialog();
    }

    function handleRemove()
    {
        dispatch(Actions.removeTool(form.id));
        closeComposeDialog();
    }

    return (
        <Dialog {...eventDialog.props} onClose={closeComposeDialog} fullWidth maxWidth="xs" component="form">

            <AppBar position="static">
                <Toolbar className="flex w-full">
                    <Typography variant="subtitle1" color="inherit">
                        {eventDialog.type === 'new' ? 'Nyt Udstyr' : 'Edit Event'}
                    </Typography>
                </Toolbar>
            </AppBar>

            <form noValidate onSubmit={handleSubmit}>
                <DialogContent classes={{ root: "p-16 pb-0 sm:p-24 sm:pb-0" }}>
                    {
                        // Name textfield
                    }
                    <TextField
                        id="Name"
                        label="Navn"
                        className="mt-8 mb-16"
                        InputLabelProps={{
                            shrink: true
                        }}
                        inputProps={{
                            max: end
                        }}
                        name="name"
                        value={form.name}
                        onChange={handleChange}
                        variant="outlined"
                        autoFocus
                        required
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
                            Tilf�j
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
