﻿import React, { Component } from 'react';
import {Form, FormGroup, Label, Input} from 'reactstrap';
export class Calculator extends Component {
    static displayName = Calculator.name;

    constructor(props) {
        super(props);
        this.state = {
            alphaAngle: 0,
            bSide: 0,
            distanceToObject: 0,
            distanceToCamera: 0,
            saveToDatabase: false,
            loading: false
        };
    }
    
    render() {
        
        return <React.Fragment>
            <h1>Калькулятор расстояния и угла установки камеры относительно взгляда человека</h1>

            {this.renderResultBlock()}
            
            <Form onSubmit={ (e) => this.onFormSubmit(e)}>
                <FormGroup>
                    <Label for="distanceToObject">Расстояние до объекта</Label>
                    <Input type="number" name="distanceToObject" id="distanceToObject" placeholder="Введите целое число" onChange = {(e) => this.onDistanceToObjectChange(e)}/>
                </FormGroup>
                <FormGroup>
                    <Label for="distanceToCamera">Высота установки камеры</Label>
                    <Input type="number" name="distanceToCamera" id="distanceToCamera" placeholder="Введите целое число" onChange = {(e) => this.onDistanceToCameraChange(e)}/>
                </FormGroup>

                <FormGroup check>
                    <Label check>
                        <Input onChange = {(e) => this.onSaveToDatabaseChangeChange(e)} type="checkbox" />{' '}
                        Сохранить результат в БД?
                    </Label>
                </FormGroup>
                <FormGroup>
                    <Input type="submit" value="Отправить" />
                </FormGroup>
                
            </Form>   
            
        </React.Fragment>
         
    }
    
    onDistanceToCameraChange(e){
        this.setState({distanceToCamera: parseInt(e.nativeEvent.target.value)});
    }

    onDistanceToObjectChange(e){
        this.setState({distanceToObject: parseInt(e.nativeEvent.target.value)});
    }

    onSaveToDatabaseChangeChange(e){
        const current = this.state.saveToDatabase;
        this.setState({saveToDatabase: !(current)});
    }
    
    onFormSubmit(event) {
        event.preventDefault();
        this.runCalculation();
    }
    
    renderResultBlock(){
        if(this.state.loading){
            return <p>Идет расчет...</p>
        }
        return <div>
            <p>Результаты расчета. </p>
            <p>Угол Альфа: {this.state.alphaAngle} </p>
            <p>Сторона B: {this.state.bSide} </p>    
        </div>;      
    }
    
    
    runCalculation() {
        this.setState({ loading: true });
        fetch("calculation", {
            method: "POST",
            body: JSON.stringify({
                distanceToObject: this.state.distanceToObject,
                distanceToCamera: this.state.distanceToCamera,
                saveToDatabase: this.state.saveToDatabase
            }),
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        }).then(response => {
            const data = response.json().then(json => {
                this.setState({ alphaAngle: json.alfaAngle, bSide: json.distanceAboveObject, loading: false });    
            });
        }).catch(error => {
            alert("Произошла ошибка при обращении к серверу");
            this.setState({ loading: false });
        });
    }
}
