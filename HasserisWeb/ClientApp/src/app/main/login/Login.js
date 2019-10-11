import React, {Component} from 'react';
import {withStyles} from '@material-ui/core/styles';
import {FusePageSimple, DemoContent} from '@fuse';

const styles = theme => ({
    layoutRoot: {}
});



class Login extends Component {

    // Test af controller, Constructor er lavet af Cholle
    constructor(props) {
        super(props);
        this.state = { forecasts: [], empList: [], loading: true };
    }
    
    render()
    {
        const {classes} = this.props;
        
        return (
            <FusePageSimple
                classes={{
                    root: classes.layoutRoot
                }}
                header={
                    <div className="p-24"><h4>Header</h4></div>
                }
                contentToolbar={
                    <div className="px-24"><h4>Content Toolbar</h4></div>
                }
                content={
                    <div className="p-24">
                        <h4>LOGIN</h4>
                    </div>
                }
            />
        )
    }


}

export default withStyles(styles, {withTheme: true})(Login);