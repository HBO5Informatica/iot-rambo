#include <rambotalk.h>

#include "Arduino.h"
#include <OneWire.h>
#include <SPI.h>
#include <RF24.h>

#define DEBUG             // comment out this line for lean & mean mode

#define DS_2401_PIN 2     // hardware address ROM pin
#define COMM_RX_PIN 11
#define COMM_TX_PIN 12
#define COMM_SPEED 1200   // speed in b/s
#define SERIAL_BAUD 9600
#define POLL_INT 100      // polling interval in milliseconds
#define TRANSMIT_INT 3000 // transmit interval in milliseconds

#define LED_HEAT 5
#define LED_LIGHT 6
#define LED_WATER 7

// hold hardware address if available
static uint8_t address[6] = { 0x0 };

// stores time of last advertisement
unsigned long previousAdvertisement = 0;
bool advertise = true;

void setup()
{
    #ifdef DEBUG
    Serial.begin (SERIAL_BAUD);
    #endif

    pinMode(13, OUTPUT); // for flashing LED on RX
    pinMode(LED_HEAT,OUTPUT);   
    pinMode(LED_LIGHT,OUTPUT);   
    pinMode(LED_WATER,OUTPUT);   

    discoverHwAddress(address, DS_2401_PIN);
    debugPrintHwAddress();

    setupRadioRxTx();
}

void loop()
{
    digitalWrite(13, false);

    receiveCommand();
    advertiseAddress();

    delay(POLL_INT);
}

void receiveCommand(){
    // prepare buffer
    uint8_t * datagram = (uint8_t *) malloc(VW_MAX_MESSAGE_LEN);
    // get some air
    uint8_t recv = receiveBuffer(datagram); // = non-blocking read

    if(recv > 0){
        if(datagram[0] == DTYPE_ACTORCMD)
        {
            digitalWrite(13, true);

            //deserialize to ActorCommand
            ActorCommand acmd;
            memcpy(&acmd, datagram, sizeof(ActorCommand));  

            //is this command for me?
            if(compareHwAddress(address, (&acmd)->actorAddress)){
                #ifdef DEBUG
                // command is for me :-)
                printDebugCommand(&acmd);

                // analyse command
                if(acmd.command & ACTORCMD_STOPADVERTS){
                    if(advertise)
                        Serial.println(">> Actor will now stop ADVERTS");
                    else 
                        Serial.println(">> Actor already stopped ADVERTS");
                    advertise = false;
                }

                digitalWrite(LED_HEAT, (acmd.command & ACTORCMD_ADJUSTHEAT) ? HIGH : LOW);
                digitalWrite(LED_LIGHT, (acmd.command & ACTORCMD_ADJUSTLIGHT) ? HIGH : LOW);
                digitalWrite(LED_WATER, (acmd.command & ACTORCMD_ADDWATER) ? HIGH : LOW);

                #endif
            }else{
                #ifdef DEBUG
                Serial.print("DISCARDING command with destination ");
                printHex((&acmd)->actorAddress, DEVICE_ADDR_LEN);
                Serial.print("(we are ");
                printHex(address, DEVICE_ADDR_LEN);
                Serial.println(")");
                #endif
            }
        }
        else
        {
            //ignore all other data
        }
    }
    
    free (datagram);
}

void advertiseAddress(){
    if(!advertise) return;

    unsigned long currentMillis = millis();
    if(currentMillis - previousAdvertisement > TRANSMIT_INT) {
    // save the last time we advertised
    previousAdvertisement = currentMillis;   
 
    // transmit advertisement
    ActorAdvertisement advert;
    advert.dtype = DTYPE_ACTORADVERT;
    memcpy(advert.actorAddress, address, DEVICE_ADDR_LEN);

    //serialize to byte array
    uint8_t * datagram = (uint8_t *) malloc(sizeof(ActorAdvertisement));
    memcpy(datagram, &advert, sizeof(ActorAdvertisement));  

    //send
    digitalWrite(13, true); // flash LED on TX
    transmitBuffer(datagram, sizeof(ActorAdvertisement));
    digitalWrite(13, false);

    printDebugAdvert(&advert);
  }
}

void debugPrintHwAddress(){
    Serial.print("HW Address: ");
    printHex(address, DEVICE_ADDR_LEN);
    Serial.println();
}