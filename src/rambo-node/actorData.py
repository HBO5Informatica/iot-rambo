import RPi.GPIO as GPIO
from lib_nrf24 import NRF24
import time
import spidev
import wsclient

def sendActorCommand(buffer):
    GPIO.setmode(GPIO.BCM)
    pipes= [[0xE8,0xE8,0xF0,0xF0,0xE1], [0xF0,0xF0,0xF0,0xF0,0xE1]]

    radio = NRF24(GPIO,spidev.SpiDev())

    radio.begin(0,17)
    radio.setChannel(0x76)
    radio.setPayloadSize(20)
    radio.setDataRate(NRF24.BR_1MBPS)
    radio.setPALevel(NRF24.PA_MIN)
    radio.setAutoAck(False)
    radio.enableDynamicPayloads()

    radio.openWritingPipe(pipes[0])

    radio.write(list(buffer))
    #radio.write([0x02,0x07,0x00,0x00,0xAC,0x41,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x08,0x00,0x19,0xFF])
    