import React, { useCallback, useEffect, useDispatch } from 'react';
import * as Actions from './store/actions';
import {TextField, Button, NativeSelect, Dialog, DialogActions, DialogContent, Icon, IconButton, Typography, Toolbar, AppBar, FormControlLabel, Switch} from '@material-ui/core';




function DecideTask(slotInfo)
{
    const dispatch = useDispatch();

    function handleSubmit(eventType)
    {
        dispatch(Actions.openNewEventDialog({
            start: slotInfo.start.toLocaleString(),
            end: slotInfo.end.toLocaleString(),
            eventType: eventType
        }));
    }
    return (
        
        
        <div>
            ll
        <Dialog fullWidth maxWidth="xs" >

            
            
            <AppBar position="static">
                <Toolbar className="flex w-full">
                    <Typography variant="subtitle1" color="inherit">
                        Vælg opgave type
                    </Typography>
                </Toolbar>
            </AppBar>

            <form noValidate onSubmit={() => handleSubmit("Delivery")}>


                    <DialogActions className="justify-between pl-8 sm:pl-16">
                        <Button
                            variant="contained"
                            color="primary"
                            type="submit"
                        >
                            Afleverings Opgave
                        </Button>
                    </DialogActions>
            </form>
            <form noValidate onSubmit={() => handleSubmit("Moving")}>

                    <DialogActions className="justify-between pl-8 sm:pl-16">
                        <Button
                            variant="contained"
                            color="primary"
                            type="submit"
                        > Flytte Opgave
                        </Button>
                    </DialogActions>
            </form>

            <form noValidate onSubmit={() => handleSubmit("test")}>

                <DialogActions className="justify-between pl-8 sm:pl-16">
                    <Button
                        variant="contained"
                        color="primary"
                        type="submit"
                    > Besigtigelsesrapport
                    </Button>
                </DialogActions>
            </form>

            </Dialog>
            </div>
    );
}

export default DecideTask;
