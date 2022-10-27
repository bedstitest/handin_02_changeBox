<!-- 
Journalen skal være 5-15 sider lang. Den skal indeholde:
x1. Gruppenummer
x2. Gruppens medlemmer med studienumre
x3. URL for GitHub-repositoriet
x4. URL for Jenkins-jobbet
x5. Klasse-, sekvens- og andre nyttige diagrammer med forklaringer, som beskriver jeres testbare design, opbygningen af jeres løsning og dens opførsel
x6. Jeres design skal tage højde for den ikke eksisterende hardware og andre svært kontrollerbare afhængigheder og indkapsle dem, således at der kan testes gennem fakes
x7. En refleksion over jeres valgte design (hvorfor, fordele og ulemper, ikke en beskrivelse, den gav I ovenfor)
8. En beskrivelse og refleksion over hvordan I har valgt at teste
?9. En beskrivelse og refleksion over hvordan I fordelte arbejdet imellem jer (hvordan, hvorfor, fordele og ulemper)
?10. En refleksion over hvordan arbejdet gik med at bruge et fælles repository og et continuous integration system (observationer, fordele og ulemper))
-->

<!-- omit in toc -->
# SWT Handin 2 - Chargebox

<!-- omit in toc -->
## Group 4 handin information
|   | Name                         | Student Number |
|---|------------------------------|----------------|
| 1 | Atren Amanoel Darvesh        | 201405993      |
| 2 | Simon Hjortgaard Bos         | 201910459      |
| 3 | Mathias Birk Olsen           | 202008722      |
| 4 | Oliver Vestergaard Schousboe | 202008211      |

<div style="page-break-after: always;"></div>

<!-- omit in toc -->
## Table of contents
- [Links](#links)
- [Diagrams](#diagrams)
  - [Class diagram](#class-diagram)
  - [Sequence diagram](#sequence-diagram)
- [Seperation of work and collaboration](#seperation-of-work-and-collaboration)
- [Descriptions and reflections about testing](#descriptions-and-reflections-about-testing)
  - [Door](#door)
  - [Display](#display)
  - [LogFile](#logfile)
  - [RfidReader](#rfidreader)
  - [UsbCharger](#usbcharger)
  - [ChargeControl](#chargecontrol)
  - [StationControl](#stationcontrol)
- [Using CI and GitHub](#using-ci-and-github)
  - [Jenkins](#jenkins)
  - [GitHub](#github)

<div style="page-break-after: always;"></div>

## Links
[Jenkins](http://ci3.ase.au.dk:8080/job/team04E22ChargeBox/)

[GitHub](https://github.com/bedstitest/handin_02_changeBox)

## Diagrams
### Class diagram
![class diagram](figs/classDiagram.svg)

- We have created interfaces to all depenencies to ensure that the two control classes can be tested
- We discussed whether the class Door should have two seperate events. We ended up with two different events to ensure that the two events are easily distinguishable. The same could probably have been acomlished with one event containing an additional property.
- All peripheral classes, `Door`, `Display`, `RfidReader`, `LogFile`, `UsbCharger` were made in simulated versions, to make the system testable.
- We chose deviate as little as possible from the design sketch in the assignment description to be able to focus as much as possible on testing rather than development.

### Sequence diagram
The sequence diagram from the assignment description was followed. We did not create a new one.

## Seperation of work and collaboration

**The design** was done in collaboration, collected around a (digital) white board. 
This meant that everbody was updated and informed about the desition, aswell as making sure everbodys ideas were heard. 

**The implementation** 
The responsiblity of implementation of the classes and the corresponding tests where separated between group members, to achive higher black box testing and to make sure that everybody could work independantly of each other.

## Descriptions and reflections about testing
### Door
When testing this class it was important to check whether an event was received after opening or closing the door and that the door opened event could not be received if the door was locked.
In the `[Setup]` each of the two door events were arranged with lambda expressions to store the event occurence and data when called.
In the `Test`s the action was to raise an event using the methods `OnDoorOpen()` and `OnDoorClosed()`.
The assertions were to check whether the arranged lambda expressions had actually received something (were not `null`)
To test the the door lock the method `LockDoor()` was called as the action and then the assertion was that no event had been received by the lambda.

### Display


### LogFile


### RfidReader


### UsbCharger
The `UsbChargerSimulator` class was a part of the handout along with an interface and tests. All we had to do was adapt them with our namespace.

### ChargeControl


### StationControl
To test this class NSubstitute was used to make fakes of all dependencies. To use the dependancy-fakes the StationContol class uses constructor injection. It was then possible to raise a made up event to check if it is received by the UUT. 
In a few cases it was necessary to Assert two times in the same test, since we did not wish to have public setter on the state of the state machine. The multiple asserts were namely when checking if the door actually unlocked after having been locked, and closed after having been opened.

NSubstitude allowed the tests to simulate that a charger was connected to a phone, which is a condition for the door to be allowed to lock and a new ID to be saved. This was done with the `.Returns()` method that is a part of the fakes methods. The call look like this `_chargeControl.IsConnected.Returns(false);`

`OldId` and `DoorOpen` were both originally attributes, but since they were found necessary in some tests, they were made into properties with private setters. This allowed for the tests to actually assert on their value.

## Using CI and GitHub 
### Jenkins 
The Jenkins CI system was set up as the first thing, besides creating the empty projects. 
This meant that from the early development, writing testable code and its test where on the top of the minds of the developers. 
It also allowed the development to be tracked, with the additions of test, and their results. 
It was discovered quite late in the project that the TestLogger Nuget package was not installed, which made Jenkins a little useless. The problem was fixed though, in time for many commits, meaning that it tracked a lot of the changes and tests.
Using Jenkins has given valuable insight into how well our tests were written and what remained untested, thanks to coverage tests.

### GitHub
The group used branching and pull requests to ensure that the main branch was always in a good state.
This allowed us to review each others code before merging it into the main branch.