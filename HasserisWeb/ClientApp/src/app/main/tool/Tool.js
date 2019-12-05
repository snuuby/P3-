import React, { useCallback, useEffect, useState } from 'react';
import { TextField, Button, Dialog, DialogActions, DialogContent, Icon, Tabs, Tab, IconButton, Typography, Toolbar, AppBar, FormControlLabel, Switch } from '@material-ui/core';
import { FuseAnimate, FusePageCarded, FuseChipSelect, SelectFormsy } from '@fuse';
import { useForm } from '@fuse/hooks';
import { makeStyles } from '@material-ui/core/styles';
import { Link } from 'react-router-dom';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import withReducer from './../../store/withReducer';
import * as Actions from './store/actions';
import reducer from './store/reducers';
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
    Name: null,
    Available: 'Yes',
};

function Tool(props) {
    const classes = useStyles();

    const dispatch = useDispatch();
    const { form, handleChange, setForm } = useForm(defaultFormState);
    const eventDialog = useSelector(({ toolReducer }) => toolReducer.tools.eventDialog);
    const searchText = useSelector(({ toolReducer }) => toolReducer.tools.searchText);
    const inputLabel = React.useRef(null);
    const [labelWidth, setLabelWidth] = useState(0);

    const [tabValue, setTabValue] = useState(0);


    useEffect(() => {
        setLabelWidth(inputLabel.current.offsetWidth + 20);
    }, []);
    useEffect(() => {
        dispatch(Actions.getTool(props.match.params));
    }, [props.match.params]);

    const initDialog = useCallback(
        () => {
            if (eventDialog.type === 'new') {
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
    function handleChangeTab(event, tabValue) {
        setTabValue(tabValue);
    }
    function handleSubmit(event) {


        event.preventDefault();
        dispatch(Actions.editTool(form));
        props.history.push('/tool/overview');


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
                                <Typography className="normal-case flex items-center sm:mb-12" component={Link} role="button" to="/tool/overview" color="inherit">
                                    <Icon className="mr-4 text-20">arrow_back</Icon>
                                    Tools
                                </Typography>
                            </FuseAnimate>

                            <div className="flex flex-col min-w-0 items-center sm:items-start">

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography className="text-16 sm:text-20 truncate">
                                        Udstyr: {form.Name}
                                    </Typography>
                                </FuseAnimate>

                                <FuseAnimate animation="transition.slideLeftIn" delay={300}>
                                    <Typography variant="caption">
                                        Udstyr ID:  {form.ID}
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
                    <Tab className="h-64 normal-case" label="Tool Details" />
                </Tabs>
            }
            content={
                <form noValidate onSubmit={handleSubmit} >

                    <div className="p-16 sm:p-24 max-w-2xl w-full">
                        {/*Text Fields*/}


                            <div className="flex-1 bg-gray-0 h-12 pl-10">
                                {/*Tool Name*/}
                                <TextField
                                    id="Name"
                                    label="name"
                                    className="mt-8 mb-16"
                                    InputLabelProps={{
                                        shrink: true
                                    }}
                                    name="Name"
                                    value={form.Name}
                                    variant="outlined"
                                    autoFocus
                                    required
                                    fullWidth
                            />
                            <FormControl variant="outlined" className={classes.formControl}>
                                <InputLabel ref={inputLabel} id="demo-simple-select-outlined-label">
                                    Er udstyret aktivt?
                                </InputLabel>
                                <Select
                                    labelid="demo-simple-select-outlined-label"
                                    id="Available"
                                    name="Available"
                                    value={form.Available}
                                    onChange={handleChange}
                                    required
                                    labelWidth={labelWidth}
                                >
                                    <MenuItem value="Yes">Ja</MenuItem>
                                    <MenuItem value="No">Nej</MenuItem>

                                </Select>
                            </FormControl>
                            <div className="flex-1 bg-gray-0 h-12 pl-10">
                                <Button
                                    className={classes.formControl}
                                    variant="contained"
                                    color="primary"
                                    type="submit"
                                >
                                    GEM
                            </Button>
                            </div>
                        </div>

                    </div>
                </form>
            }
            innerScroll
        />
    )
}

export default withReducer('toolReducer', reducer)(Tool);
