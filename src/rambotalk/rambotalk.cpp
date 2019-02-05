
#include "Arduino.h"
#include "rambotalk.h"
#include <OneWire.h>
#include <SPI.h>
#include <RF24.h>

RF24 radio(9,10);

void setupTx(uint8_t txPin, uint16_t speed){
    // Initialise the IO and ISR
    // vw_set_ptt_inverted(true);
    // vw_set_tx_pin(txPin);
    // vw_setup(speed); //bps


    radio.begin();
    radio.setPALevel(RF24_PA_MAX);
    //radio.setDataRate(RF24_1MBPS);
    radio.setChannel(0x76);
    radio.openWritingPipe(0xf0f0f0f0e1LL);
    //const uint64_t pipe = 0xe8e8f0f0e1LL;
    //radio.openReadingPipe(1, pipe);
    radio.enableDynamicPayloads();
    radio.powerUp();
}

void setupRx(uint8_t rxPin, uint16_t speed){
    
    // // Initialise the IO and ISR
    // vw_set_ptt_inverted(true); // Required
    // vw_set_rx_pin(rxPin);
    // vw_setup(speed);	 // Bits per sec

    // // start receiving
    // vw_rx_start();

    radio.begin();
    radio.setPALevel(RF24_PA_MAX);
    radio.setChannel(0x76);
    //radio.openWritingPipe(0xf0f0f0f0e1LL);
    const uint64_t pipe = 0xe8e8f0f0e1LL;
    radio.openReadingPipe(1, pipe);
    
    radio.enableDynamicPayloads();
    radio.powerUp();
}

void setupRadioRxTx(){
    
    radio.begin();
    radio.setPALevel(RF24_PA_MAX);
    radio.setChannel(0x76);
    radio.openWritingPipe(0xf0f0f0f0e1LL);
    radio.openReadingPipe(1, 0xe8e8f0f0e1LL);
    
    radio.enableDynamicPayloads();
    radio.powerUp();
}

void transmitBuffer(uint8_t* buffer, uint16_t length){
    // vw_send(buffer, length);
    // vw_wait_tx(); // Wait until the whole message is gone

    //const char text[] = "Een langere tekst";
    //radio.write(text,sizeof(text));
    radio.stopListening();
    radio.write(buffer, length, false);

}

uint8_t receiveBuffer(uint8_t * buffer) {
    // uint8_t buf[VW_MAX_MESSAGE_LEN];
    // uint8_t buflen = VW_MAX_MESSAGE_LEN;

    // if (vw_get_message(buf, &buflen)) // Non-blocking
    // {
    //     for (int i = 0; i < buflen; i++){
    //         buffer[i] = buf[i];
    //     }
    //     return buflen;
    // }
    // return -1;

    
    // uint16_t buflen = 0;
    // radio.startListening();

    // Serial.println("N/A");
    // if (radio.available()){
    //     Serial.print("AVAILABLE: ");
    //     uint16_t buflen = radio.getPayloadSize();
    //     Serial.print(buflen);
    //     uint8_t buf[buflen];
        
    //     radio.read(buf, sizeof(buf));
    // }
    // radio.stopListening();


    // radio.startListening();
    // //Serial.println("Starting loop, Radio On.");
    // char receivedMessage[32] = {};
    // if (radio.available()){
    //     radio.read(receivedMessage, sizeof(receivedMessage));
    //     Serial.println(receivedMessage);
    //     Serial.println("Turning of the radio");
    //     radio.stopListening();
    // }

    radio.startListening();
    if (radio.available()){
        uint16_t buflen = radio.getPayloadSize();
        uint8_t buf[buflen];
        radio.read(buf, sizeof(buf));
        radio.stopListening();

        for (int i = 0; i < buflen; i++){
            buffer[i] = buf[i];
        }
        return buflen;
    }
    return 0;
}

void discoverHwAddress(uint8_t* address, uint8_t romPin) {

    OneWire addressRom(romPin);  // Pin 1-Wire Bus

    uint8_t addr[8] = { 0x0 };
    
    while (addressRom.search(addr)) {
        // for (uint8_t i = 0; i < 8; i++) {
        //     Serial.print(addr[i], HEX);
        // }
        //check CRC
        if (OneWire::crc8( addr, 7) == addr[7]) { 
            for (uint8_t i = 0; i < DEVICE_ADDR_LEN; i++) {
                address[i] = addr[i+1];
            }
        } else {
            Serial.print("HW ADDRESS - CRC ERROR\n");
            // ouch! fall back to 0x0 address...
            for (uint8_t i = 0; i < DEVICE_ADDR_LEN; i++) {
                address[i] = { 0x0 };
            }
        }
    }
    addressRom.reset_search();
}

// compares two 6-byte hardware addresses and returns true if equal
bool compareHwAddress(uint8_t*  addr1, uint8_t*  addr2){
    for (int i = 0; i < DEVICE_ADDR_LEN; i++)
        if(addr1[i] != addr2[i])
            return false;
    return true;
}

// prints byte array in hex 
void printHex(uint8_t *data, int length) // prints 8-bit data in hex with leading zeroes
{
    for(int i = 0; i < length; i++){
        if(data[i] < 0x10) Serial.print("0");
        Serial.print(data[i], HEX); 
        Serial.print(" ");
    }
}

// prints console-friendly summary of a SensorData struct
void printDebugSensorData(SensorData* sdata){
    Serial.println("------------ SENSOR DATA -----------------");
    Serial.println("Type\tMsgId\tCap\t\tSoil\tHum\t\tTemp\tLight\t\tSensoraddr");
    Serial.print(sdata->dtype);
    Serial.print("\t\t");
    Serial.print(sdata->messageId);
    Serial.print("\t\t");
    Serial.print(sdata->capabilities);
    Serial.print("\t\t");
    Serial.print(sdata->measurements.soilMoisture);
    Serial.print("\t\t");
    Serial.print(sdata->measurements.humidity);
    Serial.print("\t");
    Serial.print(sdata->measurements.temperature);
    Serial.print("\t");
    Serial.print(sdata->measurements.lightLevel);
    Serial.print("\t\t");
    printHex(sdata->sensorAddress, DEVICE_ADDR_LEN);
    Serial.println(" ");
}

// prints console-friendly summary of an ActorCommand struct
void printDebugCommand(ActorCommand* acmd){
    Serial.println("------------ COMMAND -----------------");
    Serial.print(  "|  Destination    :  ");
    printHex(acmd->actorAddress, DEVICE_ADDR_LEN);
    Serial.println();
    Serial.print("|  Adjust water?  :  ");
    if(acmd->command & ACTORCMD_ADDWATER){
        Serial.print("YES   ( ");
    }
    else{
        Serial.print("NO    ( ");
    }
    Serial.print(acmd->adjustments.waterAmount);
    Serial.println(" )");

    Serial.print("|  Adjust heat?   :  ");
    if(acmd->command & ACTORCMD_ADJUSTHEAT){
        Serial.print("YES   ( ");
    }
    else{
        Serial.print("NO    ( ");
    }
    Serial.print(acmd->adjustments.tempAmount);
    Serial.println(" )");
    
    Serial.print("|  Adjust light?  :  ");
    if(acmd->command & ACTORCMD_ADJUSTLIGHT){
        Serial.print("YES   ( ");
    }
    else{
        Serial.print("NO    ( ");
    }
    Serial.print(acmd->adjustments.lightAmount);
    Serial.println(" )");

    Serial.print("|  Stop Adverts ? :  ");
    if(acmd->command & ACTORCMD_STOPADVERTS){
        Serial.print("YES");
    }
    else{
        Serial.print("NO");
    }
    Serial.println("");

    Serial.println("--------------------------------------");
}


// prints console-friendly summary of an ActorAdvertisement struct
void printDebugAdvert(ActorAdvertisement * advert){
    Serial.println("------------ ADVERT -----------------");
    Serial.print(  "|  Address    :  ");
    printHex(advert->actorAddress, DEVICE_ADDR_LEN);
    Serial.println();
    Serial.println("--------------------------------------");
}
