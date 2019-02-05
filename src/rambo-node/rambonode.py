#!/usr/bin/python
import RPi.GPIO as GPIO
from lib_nrf24 import NRF24
import time
import spidev
import client
import _thread
import websocket, json, ssl, struct
import rambotalk, actorData

SERVERHOST = 'your-api-url'
SERVERPORT = 443

SEND_PIPE = [0xE8,0xE8,0xF0,0xF0,0xE1]
READ_PIPE = [0xF0,0xF0,0xF0,0xF0,0xE1]

HANDLING_ACTOR_TX = False

## SEND DATA
GPIO.setmode(GPIO.BCM)

radio = NRF24(GPIO,spidev.SpiDev())

radio.begin(0,17)
radio.setChannel(0x76)
radio.setPayloadSize(32)
radio.setDataRate(NRF24.BR_1MBPS)
radio.setPALevel(NRF24.PA_MIN)
radio.setAutoAck(False)
radio.enableDynamicPayloads()

radio.openWritingPipe(SEND_PIPE)
radio.openReadingPipe(1,READ_PIPE)

radio.printDetails()

def listenForSensorData():
    radio.startListening()

    while True:
        while not radio.available(0) or HANDLING_ACTOR_TX:
            time.sleep(1/10)
        
        receivedMessage = []
        radio.read(receivedMessage, radio.getDynamicPayloadSize())
        print("====== SENSOR DATA ==========")
        print("Received :{}".format(receivedMessage))
        receivedMessage = bytes(receivedMessage)
        print(receivedMessage.hex())
        client.forward_sensordata_to_api(SERVERHOST, SERVERPORT, receivedMessage)

def sendActorCommand(buffer):
    radio.write(list(buffer))


# websocket API to actor
def encode_json(obj):
    # All JSON messages must be terminated by the ASCII character 0x1E (record separator).
    # Reference: https://github.com/aspnet/SignalR/blob/dev/specs/HubProtocol.md#json-encoding
    return json.dumps(obj) + chr(0x1E)

def ws_on_message(ws, message: str):
    # Strip away the record seperator
    message = message.replace(chr(0x1E), "")

    ignore_list = ['{"type":6}', '{}'] #type 6 = Ping message
    if message not in ignore_list:
        # Everything else not on ignore list
        print("====== API SOCKET ==========")
        #print(f"From server: {message}")
        message = json.loads(message)
        #print(f"Of type: {type(message)}")

        if(message['type'] == 1):
            if(message['target'] == "ActorCommand"):
                print(f"From Server: {message['arguments'][0]}")

                # Convert json to bytes
                bytearr = rambotalk.actorcommand_to_byte(message['arguments'][0])
                print(f"RT Data: {bytearr.hex()}")

                # send bytes to Actor using RF24
                HANDLING_ACTOR_TX = True
                radio.stopListening()
                sendActorCommand(bytearr)
                radio.startListening()
                HANDLING_ACTOR_TX = False
                
def ws_on_error(ws, error):
    print(error)

def ws_on_close(ws):
    print("### Disconnected from SignalR Server ###")


def ws_on_open(ws):
    print("### Connected to SignalR Server via WebSocket ###")
    
    # Do a handshake request
    #print("### Performing handshake request ###")
    ws.send(encode_json({
        "protocol": "json",
        "version": 1
    }))

    # Handshake completed
    print("### Websocket connected ###")

    # # Call chathub's send message method
    # # Reference: https://github.com/aspnet/SignalR/blob/dev/specs/HubProtocol.md#invocation-message-encoding
    # ws.send(encode_json({
    #     "type": 1,
    #     "target": "SendMessage",
    #     "arguments": ["Python websocket", "Hello world!"]
    # }))
    # print("### Hello world message sent to ChatHub ###")

def connect_to_apihub():
    #websocket.enableTrace(True)
    ws = websocket.WebSocketApp("wss://" + SERVERHOST + ":" + str(SERVERPORT) + "/mainHub",
                              on_message = ws_on_message,
                              on_error = ws_on_error,
                              on_close = ws_on_close)
    ws.on_open = ws_on_open
    ws.run_forever(sslopt={"cert_reqs": ssl.CERT_NONE})

print("Setting up websockets for actor commands")
_thread.start_new_thread(connect_to_apihub, ())
print("Setting up sensor data listener")
listenForSensorData()
#_thread.start_new_thread(listenForSensorData, ())

while 1:
    pass
