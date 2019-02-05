#ifndef SENSORS_H
#define SENSORS_H

#include "rambotalk.h"

static SensorCap capabilities = SENSORCAP_NONE;

#define DHT11_D_PIN 4
#define ME110_A_PIN A0
#define LDR_A_PIN A1

// Gets the sensor capabilities for this device.
SensorCap getCapabilities();

// Measures parameters one time
struct Measurements getMeasurements();

#endif