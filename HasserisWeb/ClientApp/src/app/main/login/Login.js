import React, { Component, useState } from 'react';
import {withStyles} from '@material-ui/core/styles';
import {FusePageSimple, DemoContent} from '@fuse';
import { makeStyles, ThemeProvider } from '@material-ui/styles';
import { Card, CardContent, Typography, Tabs, Tab } from '@material-ui/core';
import { darken } from '@material-ui/core/styles/colorManipulator';
import { FuseAnimate } from '@fuse';
import { Link } from 'react-router-dom';
import clsx from 'clsx';
import axios from 'axios';
import { TextField, Button, Dialog, DialogActions, DialogContent, Icon, IconButton, Toolbar, AppBar, FormControlLabel, Switch } from '@material-ui/core';
import 'styles/index.css';
const styles = theme => ({
    layoutRoot: {}
});

class Login extends Component {

    constructor(props) {
        super(props);
        this.state = {loggedIn: false, username:'', password:'', error:''};

        this.handleUserChange = this.handleUserChange.bind(this);
        this.handlePassChange = this.handlePassChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);

    }
    handleSubmit(evt) {
        if (!this.state.username) {
            this.state.error = 'Du skal have et brugernavn. Kontakt din adminstrator hvis du ikke kan huske dit brugernavn';
            alert(this.state.error);
            console.log("username error");
        }
        else if (!this.state.password) {
            this.state.error = 'Du skal have et password. Kontakt din adminstrator hvis du ikke kan huske dit password';
            alert(this.state.error);
            console.log("password error");
        }
        else {
            //Here we need to verify the username and password with the database
			const user = {username: this.state.username, password: this.state.password};
            axios.post('login/verify/', user)
                .then(result => {
                    result ? this.setState({ loggedIn: !this.state.loggedIn }) : alert('Bruger password passer ikke med database password');
                })
                .catch(error => {
                    console.log(error);
                });
        }
    }
    handleUserChange(evt) {
        this.setState({username: evt.target.value});
    }
    handlePassChange(evt) {
        this.setState({password: evt.target.value});
    }
    toggleLogin = () => {
        this.setState(state => ({loggedIn: !state.loggedIn}));
    }
    render() {
        return(
            this.state.loggedIn ? null : <div>
            <Dialog open={!this.state.loggedIn} fullWidth maxWidth='xs' component="form">

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
                    onChange={this.handleUserChange}
                    value={this.state.username}
                        error={this.state.error}
                        margin='normal'
                        variant='outlined'
                    />

                <TextField
                    label='Password'
                    id= 'password'
                    onChange={this.handlePassChange}
                    value={this.state.password}
                        error={this.state.error}
                        margin='normal'
                        variant='outlined'
                    />

                </DialogContent>
                    <DialogActions className="justify-between pl-8 sm:pl-16">
                        <Button
                            variant="contained"
                            color="primary"
                            type="submit"
                            onClick={this.handleSubmit}
                            >Login
                       </Button>
                    </DialogActions>

            </form>
            </Dialog>
        </div>
        )
    }
}
export default withStyles(styles, {withTheme: true})(Login);