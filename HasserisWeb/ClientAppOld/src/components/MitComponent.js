import React, { Component } from 'react';

export class MitComponent extends Component {
        static displayName = MitComponent.name;

    constructor(props) {
        super(props);
        this.state = {forecasts: [], loading: true};
    }

    static renderRandomShit(forecasts) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th>Date</th>
                </tr>
                </thead>
                <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.date}>
                        <td>{forecast.date}</td>
                    </tr>
                )}
                </tbody>
            </table>
        );
    }

    render() {
        

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : MitComponent.renderRandomShit(this.state.forecasts);

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }
}