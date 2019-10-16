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
import LoginDialog from './LoginDialog';
const styles = theme => ({
    layoutRoot: {}
});

class Login extends Component {

    constructor(props) {
        super(props);
        this.state = {loggedIn: false}
    }

    toggleLogin = () => {
        this.setState(state => ({loggedIn: !state.loggedIn}))
    }
    render() {
        return(
            this.state.loggedIn ? null : <LoginDialog toggleLogin={this.toggleLogin}/>
        )
    }
}
export default withStyles(styles, {withTheme: true})(Login);