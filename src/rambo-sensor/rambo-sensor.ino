#include "sensors.h"
#include <rambotalk.h>

#define DEBUG             //comment out this line for lean & mean mode

#define DS_2401_PIN 2     // hardware address ROM pin
#define COMM_TX_PIN 12
#define COMM_SPEED 1200   //speed in b/s
#define SERIAL_BAUD 9600
#define TRANSMIT_INT 3000 //transmit interval in milliseconds

// hold hardware address if available
static uint8_t address[6] = { 0x0 };
static word messageid = 0;

void setup() {
  #ifdef DEBUG
  Serial.begin (SERIAL_BAUD);
  #endif

  pinMode(13, OUTPUT); // for flashing LED on TX

  discoverHwAddress(address, DS_2401_PIN);
  debugPrintHwAddress();

  setupTx(COMM_TX_PIN, COMM_SPEED);
  
  capabilities = getCapabilities();
  
}

void loop() {
  // grab measurements from available sensors
  Measurements msr = getMeasurements();

  // copy to TX packet (SensorData)
  const uint8_t SENSORDATA_LEN = sizeof(SensorData);
  SensorData sdata;
  sdata.dtype = DTYPE_SENSORDATA;
  sdata.messageId = messageid;
  sdata.capabilities = capabilities;
  sdata.measurements.soilMoisture = msr.soilMoisture;
  sdata.measurements.humidity = msr.humidity;
  sdata.measurements.temperature = msr.temperature;
  sdata.measurements.lightLevel = msr.lightLevel;
  memcpy(sdata.sensorAddress, address, DEVICE_ADDR_LEN);

  //serialize to byte array
  uint8_t * datagram = (uint8_t *) malloc(SENSORDATA_LEN);
  memcpy(datagram, &sdata, SENSORDATA_LEN);  

  //send
  digitalWrite(13, true); // flash LED on TX
  transmitBuffer(datagram, SENSORDATA_LEN);
  digitalWrite(13, false);

  //increment messageid (let it overflow eventually, doesn't really matter)
  messageid++;

  #ifdef DEBUG

  // show situation of sent data
  Serial.println("============ TO SERIALIZE ================");
  printDebugSensorData(&sdata);

  // show datagram
  Serial.println("============== DATAGRAM ==================");
  printHex(datagram, SENSORDATA_LEN);
  Serial.println(" ");

  //deserialize to SensorData
  SensorData checksdata;
  memcpy(&checksdata, datagram, SENSORDATA_LEN);  

  // show situation of sent data
  Serial.println("============ DESERIALIZED ================");
  printDebugSensorData(&checksdata);
  #endif

  free (datagram);
  delay(TRANSMIT_INT);
}

void debugPrintHwAddress(){
    Serial.print("HW Address: ");
    printHex(address, DEVICE_ADDR_LEN);
    Serial.println();
}