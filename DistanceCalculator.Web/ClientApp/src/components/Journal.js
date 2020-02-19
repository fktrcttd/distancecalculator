import React, { Component } from 'react';

export class Journal extends Component {
    static displayName = Journal.name;

    constructor(props) {
        super(props);
        this.state = { items: [], loading: true };
    }

    componentDidMount() {
        this.populateJournalData();
    }

    static renderForecastsTable(items) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th>ИД</th>
                    <th>Дата создания</th>
                    <th>Высота камеры, см</th>
                    <th>Дистанция до объекта, см </th>
                    <th>Угол Альфа, гр </th>
                    <th>Вертикальная дистанция от камеры до объекта, см </th>
                </tr>
                </thead>
                <tbody>
                {items.map(item =>
                    <tr key={item.id}>
                        <td>{item.id }</td>
                        <td>{(new Date(item.creationDateTime)).toLocaleString() }</td>
                        <td>{item.height}</td>
                        <td>{item.distanceToObject}</td>
                        <td>{item.alfaAngle}</td>
                        <td>{item.distanceAboveObject}</td>
                    </tr>
                )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Journal.renderForecastsTable(this.state.items);

        return (
            <div>
                <h1 id="tabelLabel" >Журнал расчетов</h1>
                <p>В данном разделе представлены ранее рассчитанные параметры установки камеры</p>
                {contents}
            </div>
        );
    }

    async populateJournalData() {
        const response = await fetch('calculation/all');
        const data = await response.json();
        this.setState({ items: data, loading: false });
    }
}
