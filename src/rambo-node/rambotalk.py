import ssl, sys, json, struct

nodeAddress = b'\x03\x04\x05\x06\x07\x08'

def sensordata_to_json(raw_sensordata):
    try:
        datatype = struct.unpack_from('<B', raw_sensordata, 0)[0]
        #print("datatype: " + str(datatype))

        data = {
            "messageid": struct.unpack_from('<H', raw_sensordata, 1)[0],
            "capabilities": struct.unpack_from('<B', raw_sensordata, 3)[0],
            "soilmoisture": struct.unpack_from('<H', raw_sensordata, 4)[0],
            "humidity": struct.unpack_from('<f', raw_sensordata, 6)[0],
            "temperature": struct.unpack_from('<f', raw_sensordata, 10)[0],
            "lightlevel": struct.unpack_from('<f', raw_sensordata, 14)[0],
            "sensoraddress": raw_sensordata[18:25].hex(),
            "nodeaddress": nodeAddress.hex()
        }

        return json.dumps(data)

    except ():
        print('Invalid sensordata, cancelling parse')


def actorcommand_to_byte(json_actorcommand):
    try:
        # Convert json to bytes
        bytearr = []

        # Message type
        bytearr.append(0x02)
        # Command flags
        bytearr.append(int(json_actorcommand['command']))
        # Adjustments
        for i in bytearray(struct.pack("<f", json_actorcommand['water'])):
            bytearr.append(i)
        for i in bytearray(struct.pack("<f", json_actorcommand['temperature'])):
            bytearr.append(i)
        for i in bytearray(struct.pack("<f", json_actorcommand['lightLevel'])):
            bytearr.append(i)                
        # Actor address
        for i in bytearray(bytes.fromhex(json_actorcommand['actorAddress'])):
            bytearr.append(i)

        return bytes(bytearr)

    except ():
        print('Invalid sensordata, cancelling parse')