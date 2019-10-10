import React, {Component} from 'react';
import {withStyles} from '@material-ui/core/styles';
import {FusePageSimple, DemoContent} from '@fuse';

const styles = theme => ({
    layoutRoot: {}
});



class Example extends Component {

    // Test af controller, Constructor er lavet af Cholle
    constructor(props) {
        super(props);
        this.state = { forecasts: [], empList: [], loading: true };
        


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
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th>Date</th>
                    <th>FirstName</th>
                    <th>LastName</th>

                </tr>
                </thead>
                <tbody>
                {empList.map(emp =>
                    <tr key={emp.id}>
                        <td>{emp.firstName}</td>
                        <td>{emp.lastName}</td>
                        
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
        // Cholle
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Example.renderForecastsTable(this.state.forecasts);
            
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
                    <div className="px-24"><h4>Content Toolbar</h4></div>
                }
                content={
                    <div className="p-24">
                        <h4>Controller data - Cholle</h4>
                        <div>
                            <h1 id="tabelLabel" >Weather forecast</h1>
                            <p>This component demonstrates fetching data from the server.</p>
                            {contents}
                        </div>

                        <div>
                            <h1 id="tabelLabel" >Weather forecast</h1>
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
        const response = await fetch('getemployees');
        const data = await response.json();
        this.setState({ empList: data, loading: false });
    }
    
}

export default withStyles(styles, {withTheme: true})(Example);