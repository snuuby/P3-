import React, { Component, useState } from 'react';
import {withStyles} from '@material-ui/core/styles';
import {FusePageSimple, DemoContent} from '@fuse';
import { makeStyles, ThemeProvider } from '@material-ui/styles';
import { Card, CardContent, Typography, Tabs, Tab } from '@material-ui/core';
import { darken } from '@material-ui/core/styles/colorManipulator';
import { FuseAnimate } from '@fuse';
import { Link } from 'react-router-dom';
import clsx from 'clsx';
import { TextField, Button, Dialog, DialogActions, DialogContent, Icon, IconButton, Toolbar, AppBar, FormControlLabel, Switch } from '@material-ui/core';
import 'styles/index.css';
const styles = theme => ({
    layoutRoot: {}
});


function LoginDialog(props)
{
    let username;
    let password;
    let error;
    let open = true;


    function handleSubmit(evt) {
        if (username == null) {
            error = 'Du skal have et brugernavn. Kontakt din adminstrator hvis du ikke kan huske dit brugernavn';
            alert(error);
            console.log("hej");
        }
        else if (password == null) {
            error = 'Du skal have et password. Kontakt din adminstrator hvis du ikke kan huske dit password';
            alert(error);
            console.log("hejsa");
        }
        else {
            //Here we need to verify the username and password with the database
            props.toggleLogin();
            error = 'Forkert brugernavn eller password';
            alert(error);
            console.log("hejukuku");
        }
    }
    function handleUserChange(evt) {
        username = evt.target.value;
    }
    function handlePassChange(evt) {
        password = evt.target.value;
    }
    return (
            <div>
                <Dialog open={open} fullWidth maxWidth='xs' component="form">

                <AppBar position="static">
                    <Toolbar className="flex w-full">
                        <Typography variant="subtitle1" color="inherit">
                            Login
                        </Typography>
                    </Toolbar>
                </AppBar>

                <form>
                    <DialogContent classes={{ root: "p-16 pb-0 sm:p-24 sm:pb-0" }}>
                    
                    <TextField
                        label='Brugernavn'
                        id='brugernavn'
                        onChange={handleUserChange}
                        value={username}
                            error={error}
                            margin='normal'
                            variant='outlined'
                        />

                    <TextField

                        label='Password'
                        id= 'password'
                        onChange={handlePassChange}
                        value={password}
                            error={error}
                            margin='normal'
                            variant='outlined'
                        />

                    </DialogContent>
                        <DialogActions className="justify-between pl-8 sm:pl-16">
                            <Button
                                variant="contained"
                                color="primary"
                                type="submit"
                                onClick={handleSubmit}
                                >Login
                           </Button>
                        </DialogActions>

                </form>
                </Dialog>
            </div>
    )
}
export default LoginDialog;
