"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://rambo-group14.azurewebsites.net/sensorHub").build();

//var connection = new signalR.HubConnectionBuilder().withUrl("https://rambo.fiery-app.be/sensorHub").build();
//var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44309/sensorHub").build();
connection.on("SoilValueUpdate", function (newValue) {
    document.getElementById("currentSoilMoisture").textContent = newValue;
    addDataToSoilMoistureChart(newValue);

});

connection.on("HumidityValueUpdate", function (newValue) {
    document.getElementById("currentHumidity").textContent = newValue;
    //addData(humidityChart, humidityDataPoints, humidityDataLabels, humidityXValue, newValue);
    addDataToHumidityChart(newValue);
});

connection.on("TemperatureValueUpdate", function (newValue) {
    document.getElementById("currentTemperature").textContent = newValue;
    addDataToTemperatureChart(newValue);
    
});

connection.on("LightLevelValueUpdate", function (newValue) {
    document.getElementById("currentLightLevel").textContent = newValue;
    addDataToLightLevelChart(newValue);
    
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});