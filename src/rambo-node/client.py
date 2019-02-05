#!/usr/bin/env python

import http.client
import rambotalk
import ssl, sys, json, struct 

#define a byte array (cmd)
#01 0F 2C 01 66 66 E6 3E 00 00 AC 41 66 66 48 43 01 00 08 00 19 FF


# Forwards a raw command from local infrastructure to API
def forward_sensordata_to_api(host, port, raw_data):
    
    print(raw_data)
    jsondata = rambotalk.sensordata_to_json(raw_data)
    print("Sending: ")
    print(jsondata)

    #create a connection
    conn = http.client.HTTPSConnection(host, port, context=ssl._create_unverified_context())

    #request command to server  
    conn.request('POST', '/api/sensors/new', body=jsondata, headers={ "content-type" : "application/json" }) 
 
    #get response from server  
    rsp = conn.getresponse()

    #print server response and data  
    print(rsp.status, rsp.reason)
    data_received = rsp.read()
    #datajson = json.loads(data_received)
    print(data_received)

    conn.close()

# Forward sensordata to API
#raw_sensordata = bytes([0x01, 0x02, 0x00, 0x0F, 0x2C, 0x01, 0x66, 0x66, 0xE6, 0x3E, 0x00, 0x00, 0xAC, 0x41, 0x66, 0x66, 0x48, 0x43, 0x01, 0x00, 0x08, 0x00, 0x19, 0xFF])
#forward_sensordata_to_api(raw_sensordata)

#wsclient.connect_to_apihub(SERVERHOST, SERVERPORT)