# This is our Journal
Its a bad idea to have the dock here. This

## Links
[Jenkins](http://ci3.ase.au.dk:8080/job/team04E22ChargeBox/)
[GitHub](https://github.com/bedstitest/handin_02_changeBox)

## Design for testablility

![class diagram](figs/classDiagram.svg)

- We have created interfaces to all depenencies to ensure that the two control classes can be tested
- We discussed whether the class Door should have two seperate events. We ended up with two different events to ensure that the event are easily distinguishable. The same could probably have been acomlished with one event containing an additional property.
- 

## Seperation of work and collaboration

**The design** was done in colaberation, collected around a (digital) white board. 
This meant that everbody was updated and informed about the desition, aswell as making sure everbodys ideas were heard. 

**The implementation** 
The resposiblity of implementation of the app and the implementation of the tests where separated between group members, to achive higher black box testing. 

Futher more, the implementation of the tests should be the first thing tackled. 



## Using CI with Jinkins 
The Jenkins CI system was set up as the first thing, besides creating the empty projects. 
This meant that from the early development, writing testable code and its test where on the top of the minds of the developers. 
It also allowed the development to be tracked, with the additions of test, and their results. 

## Door
The door has to be simulated. The interface only consists of two methods, `lockDoor` and `unlockDoor` along with two events `DoorOpenedEvent` and `DoorClosedEvent`. 
In the simulator class, we need methods to manually test what happens when the door is opened and closed, and both in locked and unlocked state. This is achieved with an `OnDoorOpened` method, that calls the `OnDoorOpenedEvent` method. This method should then generate an event, that is sent to the handler. Corresponding methods are made for closing the door.
When testing this class it is important to check whether an event is received after opening or closing the door.

## Display


## LogFile


## RfidReader


## UsbCharger
The USB charger has to be simulated. This simulator class was a part of the handout along with an interface and tests. All we had to do was adapt them with our namespace.

## ChargeControl


## StationControl