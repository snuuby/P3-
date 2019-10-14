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



class Login extends Component {

    constructor(props) {
        super(props);
        this.state = { username: '', password: '', error: '', open: true };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.dismissError = this.dismissError.bind(this);
        this.handleUserChange = this.handleUserChange.bind(this);
        this.handlePassChange = this.handlePassChange.bind(this);

            }

    dismissError() {
        this.setState({ error: '' });
    }

    handleSubmit(evt) {

        if (!this.state.username) {
            this.setState({ error: 'Du skal have et brugernavn. Kontakt din adminstrator hvis du ikke kan huske dit brugernavn' });
        }

        if (!this.state.password) {
            this.setState({ error: 'Du skal have et password. Kontakt din adminstrator hvis du ikke kan huske dit password' });
        }

        return this.setState({ error: 'Ukendt fejl' });
        }

    handleUserChange(evt) {
        this.setState({
            username: evt.target.value,
        });
    };

    handlePassChange(evt) {
        this.setState({
            password: evt.target.value,
        });
    }


    render()
    {
        const {classes} = this.props;
        
        return (
            <div>
                <Dialog open={this.state.open} fullWidth maxWidth='xs' component="form">

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
                        onChange={this.handleUserChange}
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
                                onClick={() => this.handleSubmit}
                                >Login
                           </Button>
                        </DialogActions>

                </form>
                </Dialog>
                </div>
            )
        }
      handleUserChange(evt) {
    this.setState({
      username: evt.target.value,
    });
  };

  handlePassChange(evt) {
    this.setState({
      password: evt.target.value,
    });
  }
}

export default withStyles(styles, {withTheme: true})(Login);
