# PubSub OCPP Server
PubSub OCPP Server is a high level service layer on top of the OCPP v1.6 protocol. 
It is abstracting away all the low level concerns built into OCPP and offers a REST/Websockets API which is easily integrated into any depot management/logistics application, independently of language/platform.

## OCPP Server
At system startup, the OCPP Server reads application settings data which can contain whitelists of ID Tags and/or EVSE equipment vendors, if desired.

The application then starts up a web server with two controllers; one for incoming OCPP requests from EVSEs, and another one with the high level interface to be consumed by a client application.
## OCPP controller
Incoming OCPP connection requests must be on Secure WebSockets (WSS). 

```
wss://<host<:<port>/ocpp/ws/<Charging Point ID>
```
