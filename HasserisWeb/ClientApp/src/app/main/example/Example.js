import React, {Component} from 'react';
import {withStyles} from '@material-ui/core/styles';
import {
    AppBar, Button,
    Dialog, DialogActions,
    DialogContent,
    FormControlLabel, Icon, IconButton,
    Switch,
    TextField,
    Toolbar,
    Typography
} from '@material-ui/core';
import {FusePageSimple, DemoContent} from '@fuse';
import axios from 'axios';


const styles = theme => ({
    layoutRoot: {}
});


function deleteWorker(id) {
    
    if (window.confirm("Er du sikker?")) {
        axios.post(`employees/delete/` + id)
            .then(res => {

            });

        window.location.reload();
    } else {
        console.log("Answer was no to prompt");
    }


}

function editWorker(id) {
    alert("Edit worker with: "  + id);
}

class Example extends Component {
    
    
    // Test af controller, Constructor er lavet af Cholle
    constructor(props) {
        super(props);
        this.state = { forecasts: [], empList: [], loading: true };
        
        //this.renderEmployeeList.bind(this);


    }
    
    // Cholle
    componentDidMount() {
        this.populateWeatherData();
        // 2Cholle
        this.populateEmployeeData();
    }
    
    // 2Cholle
        static renderEmployeeList(empList){
        
        return(
            <table className='table' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th>ID</th>
                    <th>FirstName</th>
                    <th>LastName</th>
                    <th></th>
                    <th></th>


                </tr>
                </thead>
                <tbody>
                {empList.map(emp =>
                    <tr key={emp.id}>
                        <td>{emp.id}</td>
                        <td>{emp.firstName}</td>
                        <td>
                            {emp.lastName}
                        </td>
                        <td>                            
                            <button onClick={() => editWorker(emp.id)} className="btn btn-info" type="button">Rediger</button>
                        </td>
                        <td>
                            <button onClick={() => deleteWorker(emp.id)} className="btn btn-info" type="button">Slet</button>
                        </td>
                        
                    </tr>
                )}
                </tbody>
            </table>
        )
    }
    
    // Cholle
    static renderForecastsTable(forecasts) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
                </thead>
                <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.date}>
                        <td>{forecast.date}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
                </tbody>
            </table>
        );
    }
    
    render()
    {
            // 2Cholle
        let contentsEmployees = this.state.loading
            ? <p><em>Loading...</em></p>
            : Example.renderEmployeeList(this.state.empList);
        
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
                    <div className="px-24"><h4>Content Toolbar</h4>

                        <button className="btn btn-primary" type="button">Tilføj Medarbejder</button>

                    </div>
                }
                content={
                    <div className="p-24">

                        <div>
                            <h1 id="tabelLabel" >Employee list</h1>
                            <p>Employee data from database:</p>
                            {contentsEmployees}
                        </div>
                        
                        <h4>Content</h4>
                        <br/>
                        <DemoContent/>
                    </div>
                }
            />
        )
    }
    
    

    // Cholle
    async populateWeatherData() {
        const response = await fetch('weatherdata');
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }
    
    // 2Cholle
    async populateEmployeeData(){
        //const response = await fetch('getemployees');
        //const data = await response.json();

        //this.setState({ empList: data, loading: false });

        this.setState({ loading: false });

        axios.get(`employees/all`)
            .then(res => {
                const employees = res.data;
                this.setState({ empList: res.data });
            })
    }
    
}

export default withStyles(styles, {withTheme: true})(Example);