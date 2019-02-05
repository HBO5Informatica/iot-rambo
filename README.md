# 1 - General Overview

The Retain and Maintain Botanical Objectives project is a fully automated and extensible system to monitor and sustain plant farms or clusters of plants at home.

It uses an array of sensors to detect the requirements for this sustainance and undertakes actions (such as watering, adjusting light) using actor devices.

This project is by no means complete and serves as an example of an independent IoT platform.

![General Overview](https://imgurl)

# 2 - Communication

## 2.1 - Overview

The R.A.M.B.O devices must communicate with each other and the cloud. These messages are usually one-way only.

1. **BO-Sensor > BO-Node**
   
   When a BO-Sensor device is turned on, it periodically transmits sensor data and its hardware ID using the RamboTalk protocol, so that a BO-Node device may pick this up.
   A BO-Sensor is unable to receive anything. 

   *See 2.2: BO-Sensor > BO-Node*

2. **BO-Actor < > BO-Node**
   
   When a BO-Actor device is turned on, it periodically **advertises** its hardware ID using the RamboTalk protocol, so that a BO-Node device may pick this up.

   The BO-Node broadcasts **commands** for a particular BO-Actor partipating in the system using RamboTalk.

   *See 2.3: Communication between BO-Node and BO-Actor*

3. **BO-Node < > R.A.M.B.O. Cloud**
   
   When a BO-Node picks up a message destined for the Cloud, it will forward it to the REST service using HTTP.

   Certain messages from the Cloud, such as commands for BO-Actor devices, must be routed back into the local infrastructure. BO-Node is reponsible for receiving these (using HTTP and/or web sockets).

   *See 2.4: Communication with R.A.M.B.O. Cloud*

<hr />

## 2.2 - BO-Sensor > BO-Node

Communication between the BO-Sensor and BO-Node is done by 443MHz RF, with messages structured according to the RamboTalk protocol.

- (BO-Sensor Device: **FS1000A** - 433MHz RF Transmitter)
- (BO-Node Device: **XD-RF-5V** - 433MHz RF Receiver)
- BO- Node/Sensor : **NRF24L01** - 2,4GHz Transceiver

### 2.2.1 - Sensor Device parameters

The BO-Sensor device is comprimes of multiple **optional** sensors, and defines these sensor capabilities in a single byte (0 - 255) called `SensorCap` which is bitwise enumerated. The possible values are:

| Byte     | Capability                |
| -------- | ------------------------- |
| 0x00     | None                      |
| 0x01     | Soil moisture             |
| 0x02     | Hygrometer (air humidity) |
| 0x04     | Thermometer               |
| 0x08     | Luxmeter (light)          |

The BO-Sensor device transmits a unique Hardware ID, which is read from a DS-2401+ ROM.

| Parameter     | Type        | Component      | Description                               |
| ------------- | ----------- | -------------- | ----------------------------------------- |
| Capabilities  | `byte`      | [auto detect]  | bitwise enumerated value                  |
| Serial number | `byte[6]`   | DS2401+        | 24 bits ID e.g. `0x0100080019FF`          |


### 2.2.2 - Sensor Device measurements
The BO-Sensor device reads following measurments from its sensors:

| Parameter     | Type        | Sensor       | Description                         |
| ------------- | ----------- | ------------ | ----------------------------------- |
| Soil moisture | `int`       | Iduino ME110 | Arbitrary number, to be calibrated  |
| Humidity      | `float`     | DHT-11       |                                     |
| Temperature   | `float`     | DHT-11       |                                     |
| Light level   | `float`     | LDR (gen.)   | Measured by generic LDR - No LUX :( |          

This data is structures in the `Measurements` structure of the RamboTalk protocol.

### 2.2.3 - Sensor Data - Datagram

When the sensor broadcasts the device information and measurements, it structures this information using the `SensorData` structure

`D MM C SS HHHH TTTT LLLL NNNNNN` (24 Bytes)

| byte index    | Length (B)   | Type      | Description                               |
|:-------------:|:------------:| --------- | ----------------------------------------- |
| 0             | 1            | `byte`    | `D` = `DTYPE_SENSORDATA`, always `0x01` * |
| 1             | 2            | `word`    | `M` = Message ID **                       |
| 3             | 1            | `byte`    | `C` = Capabilties (flags)                 |
| 4             | 2            | `int`     | `S` = Soil moisture                       |
| 6             | 4            | `float`   | `H` = Humidity                            |
| 10            | 4            | `float`   | `T` = Temperature (C)                     |
| 14            | 4            | `float`   | `L` = Light level (LDR Ω)                 |
| 18            | 6            | `byte[6]` | `N` = 24 bits Source BO-Sensor ID         |

All numbers (floats and ints) are byte-arranged in the native Arduino machine format, which is  **Little Endian**.

\* The first byte is the `Dtype`, which occurs in every RamboTalk datagram, identifies the message as SensorData. SensorData always has a `Dtype` of `0x01`.

\** The message ID are sequential 16-bit numbers which may be used by the API to distinguish duplicate messages from the same device and may enable a rough estimate of packet loss.

For example, when a BO-Sensor broadcasts this byte array (HEX):

`01 02 00 0F 2C 01 66 66 E6 3E 00 00 AC 41 66 66 48 43 01 00 08 00 19 FF`

This means the following:

- `01` = This message is SensorData
- `02 00` = Message Id equals 2
- `0F` = 15 (moisture, hygro-, thermo-, and luxmeter capabilities)
- `2C 01` = 300 (Soil moisture)
- `66 66 E6 3E` = 0.45 (Humidity)
- `00 00 AC 41` = 21.50 (Temperature)
- `66 66 48 43` = 200.40 (Light level)
- `01 00 08 00 19 FF` = [exact binary] (Sensor Unique ID)

You may verify these values seperately here (check Little Endian) https://www.scadacore.com/tools/programming-calculators/online-hex-converter/

<hr />

## 2.3 - Communication between BO-Node and BO-Actor

Communication between the BO-Actor and BO-Node is done by 443MHz RF, with messages structured according to the RamboTalk protocol.

- BO-Actor : **NRF24L01** - 2,4GHz Transceiver


### 2.3.1 - Actor Command

    Node -> Actor

When the BO-Node must broadcast all Actor Commands it receives from the Cloud. This way a, BO-Actor devices may pick up the command. The Actor Device expects these messages to formatted using the `ActorCommand` structure.

`D C SS WWWW TTTT LLLL NNNNNN` (20 Bytes)

| byte index    | Length (B)   | Type      | Description                             |
|:-------------:|:------------:| --------- | --------------------------------------- |
| 0             | 1            | `byte`    | `D` = `DTYPE_ACTORCMD`, always `0x02` * |
| 1             | 1            | `byte`    | `C` = Commands (`ActorCmd` flags)       |
| 2             | 4            | `float`   | `W` = Water to add                      |
| 6             | 4            | `float`   | `T` = Temperature to adjust             |
| 10            | 4            | `float`   | `L` = Light level to adjust             |
| 14            | 6            | `byte[6]` | `N` = 24 bits Destination BO-Actor ID   |

Any receiving Actor device will check the Destination BO-Actor ID and will only execute the command when it's own Hardware ID is a match.

The possible commands are defined in a single byte (0 - 255) called `ActorCmd` which is bitwise enumerated. The possible values are:

| Byte     | Actor Command      |
| -------- | ------------------ |
| 0x00     | None               |
| 0x01     | Add water          |
| 0x02     | Adjust heat        |
| 0x04     | Adjust light       |
| 0x80     | Stop advertising   |

Some commands have an accompanying float value. For example, the 0x01 command (Add Water) will have the WWWW (Water to add) float set.

For example, when a BO-Node broadcasts this byte array (HEX):

`01 81 00 00 AC 41 00 00 00 00 00 00 00 00 01 00 08 00 19 FF`

This means the following:

- `02` = This message is an Actor Command
- `81` = 2 commands flags: Add Water + Stop Advertising (0x01 | 0x80)
- `00 00 AC 41` = 21.50 (Water to add)
- `00 00 00 00` = 0 (Temperature to adjust - ignored with these flags)
- `00 00 00 00` = 0 (Light level to adjust - ignored with these flags)
- `01 00 08 00 19 FF` = [exact binary] (Destination Actor ID)

### 2.3.2 - Actor Advertisement

    Actor -->  Node

When a BO-Actor device is turned on, it periodically advertises its hardware ID using the RamboTalk protocol, so that a BO-Node device may pick this up. It structures this information using the `ActorAdvertisement` structure

`D NNNNNN` (7 Bytes)

| byte index    | Length (B)   | Type      | Description                                |
|:-------------:|:------------:| --------- | ------------------------------------------ |
| 0             | 1            | `byte`    | `D` = `DTYPE_ACTORADVERT`, always `0x03` * |
| 1             | 6            | `byte[6]` | `N` = 24 bits Source BO-Actor ID           |

The router will pick this up and send it to the Cloud. This enables the cloud to automatically add new Actor devices. When Actor device is known to the Cloud, the system may send a `ActorCommand` back to the Actor device telling it to stop advertising to save battery life.

For example, when a BO-Actor Device broadcasts this byte array (HEX):

`03 01 00 08 00 19 FF`

This means the following:

- `03` = This message is an Actor Advertisement
- `01 00 08 00 19 FF` = [exact binary] (Actor's ID)

<hr />

## 2.4 - Communication with R.A.M.B.O. Cloud

### 2.4.1 - Node Responsibilities

The BO-Node device is the man-in-the-middle between Sensors, Actors in the local infrastructure, and the R.A.M.B.O. Cloud on the internet. It is effectively a gateway, which speaks multiple protocols:

- RamboTalk via 433 MHz RF - to communicate with BO-Actors and BO-Sensors

        BO-Sensor -> BO-Node    (SensorData)
        BO-Actor -> BO-Node     (Actor Advertisement)
        BO-Node <- BO-Actor     (Actor Commands)

- HTTP via Wi-Fi/Ethernet - to communicate with the Cloud API

        BO-Node -> Cloud    (SensorData and Actor Advertisement)
        BO-Node <- Cloud    (Actor Commands)

### 2.4.2 - RamboTalk

This topic has been discussed extensively in the previous chapter. Refer to *2.2* and *2.3* for more information.

### 2.4.3 - HTTP

... to do ...

