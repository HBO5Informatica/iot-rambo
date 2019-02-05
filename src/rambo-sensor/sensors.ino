#include "Arduino.h"
#include "sensors.h"
#include <dht.h>

//String serial = "01C31B9B19000046";

dht DHT;

// Gets the sensor capabilities for this device.
SensorCap getCapabilities(){
    SensorCap cap = SENSORCAP_NONE;
    cap = SENSORCAP_MOISTURE | SENSORCAP_HYGROMETER | SENSORCAP_THERMOMETER | SENSORCAP_LUXMETER;
    return cap;
}

struct Measurements getMeasurements(){
    // read temp, humidity
    struct Measurements msr;

    // measure soil moisture
    msr.soilMoisture = analogRead(ME110_A_PIN);

    // measure humidity and temperature
    int chk = DHT.read11(DHT11_D_PIN);
    msr.humidity = (float)DHT.humidity;
    msr.temperature = (float)DHT.temperature;

    // measure LDR value
    msr.lightLevel = (float)analogRead(LDR_A_PIN);

    return msr;
}