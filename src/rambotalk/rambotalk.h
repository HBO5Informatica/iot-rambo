
#ifndef Rambotalk_h
#define Rambotalk_h

#include "Arduino.h"
#include <VirtualWire.h>

// define number of byte in a device hardware address
#define DEVICE_ADDR_LEN 6

// declare datagram types
typedef uint8_t Dtype;
const Dtype DTYPE_SENSORDATA = 0x01;  // datagram contains sensor readings
const Dtype DTYPE_ACTORCMD = 0x02;    // datagram contains actor command
const Dtype DTYPE_ACTORADVERT = 0x03;  // datagram is actor advertisement

// declare Sensor Capability flags
typedef uint8_t SensorCap;
const SensorCap SENSORCAP_NONE = 0x00;
const SensorCap SENSORCAP_MOISTURE = 0x01;
const SensorCap SENSORCAP_HYGROMETER = 0x02;
const SensorCap SENSORCAP_THERMOMETER = 0x04;
const SensorCap SENSORCAP_LUXMETER = 0x08;

// declare Actor Command flags
typedef uint8_t ActorCmd;
const ActorCmd ACTORCMD_NONE = 0x00;
const ActorCmd ACTORCMD_ADDWATER = 0x01;
const ActorCmd ACTORCMD_ADJUSTHEAT = 0x02;
const ActorCmd ACTORCMD_ADJUSTLIGHT = 0x04;
const ActorCmd ACTORCMD_STOPADVERTS = 0x80; //128

// Define struct holding all measurables by a rambo Sensor
typedef struct Measurements Measurements;
struct Measurements {
    int soilMoisture;           // 2 bytes
    float humidity;             // 4 bytes
    float temperature;          // 4 bytes
    float lightLevel;           // 4 bytes
};

// Define struct holding all data sent/received by Sensor
typedef struct SensorData SensorData;
struct SensorData {
    Dtype dtype;                // 1 byte (DTYPE_SENSORDATA)
    word messageId;             // 2 bytes 
    SensorCap capabilities;     // 1 byte
    Measurements measurements;  // 14 bytes (2 + 4 + 4 + 4)
    uint8_t sensorAddress[6];   // 6 bytes
};

// Define struct holding possible adjustments by Actor
typedef struct Adjustments Adjustments;
struct Adjustments {
    float waterAmount;          // 4 bytes
    float tempAmount;           // 4 bytes
    float lightAmount;          // 4 bytes
};

// Define struct holding actor command sent from API/user
typedef struct ActorCommand ActorCommand;
struct ActorCommand {
    Dtype dtype;                // 1 byte (DTYPE_ACTORCMD)
    ActorCmd command;           // 1 bytes
    Adjustments adjustments;    // 12 bytes
    uint8_t actorAddress[6];    // 6 bytes
};

// Define struct holding actor advertisement sent from API/user
typedef struct ActorAdvertisement ActorAdvertisement;
struct ActorAdvertisement {
    Dtype dtype;                // 1 byte (DTYPE_ACTORADVERT)
    uint8_t actorAddress[6];    // 6 bytes
};

// configure a pin for sending from txPin at a certain speed (in b/s)
void setupTx(uint8_t txPin, uint16_t speed);

// configure a pin for receiving from txPin at a certain speed (in b/s)
void setupRx(uint8_t rxPin, uint16_t speed);


void setupRadioRxTx();

// transmit a buffer by 443 MHz
void transmitBuffer(uint8_t* buffer, uint16_t length);

// receive a buffer by 443 MHz, return length of buffer
uint8_t receiveBuffer(uint8_t* buffer);

// Reads 6 byte hardware address from ROM
void discoverHwAddress(uint8_t* address, uint8_t romPin);

// compares two 6-byte hardware addresses and returns true if equal
bool compareHwAddress(uint8_t*  addr1, uint8_t*  addr2);

// prints 8-bit data in hex with leading zeroes
void printHex(uint8_t *data, int length);

// prints console-friendly summary of a SensorData struct
void printDebugSensorData(SensorData* sdata);

// prints console-friendly summary of an ActorCommand struct
void printDebugCommand(ActorCommand * acmd);

// prints console-friendly summary of an ActorAdvertisement struct
void printDebugAdvert(ActorAdvertisement * advert);

#endif
