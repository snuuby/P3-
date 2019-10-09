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
        this.state = { forecasts: [], loading: true };
    }
    
    // Cholle
    componentDidMount() {
        this.populateWeatherData();
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
        const response = await fetch('weatherforecast');
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }
    
}

export default withStyles(styles, {withTheme: true})(Example);