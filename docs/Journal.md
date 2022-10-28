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
x9. En beskrivelse og refleksion over hvordan I fordelte arbejdet imellem jer (hvordan, hvorfor, fordele og ulemper)
x10. En refleksion over hvordan arbejdet gik med at bruge et fælles repository og et continuous integration system (observationer, fordele og ulemper))
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
  - [Workflow](#workflow)

<div style="page-break-after: always;"></div>

## Links
[Jenkins](http://ci3.ase.au.dk:8080/job/team04E22ChargeBox/)

[GitHub](https://github.com/bedstitest/handin_02_changeBox)

## Diagrams
### Class diagram
![class diagram](figs/classDiagram.svg)

- We have created interfaces to all dependencies to ensure that the two control classes can be tested
- We discussed whether the class Door should have two seperate events. We ended up with two different events to ensure that the two events are easily distinguishable. The same could probably have been accomplished with one event containing an additional property.
- All peripheral classes, `Door`, `Display`, `RfidReader`, `LogFile`, `UsbCharger` were made in simulated versions, to make the system testable.
- We chose to deviate as little as possible from the design sketch in the assignment description to be able to focus as much as possible on testing rather than development.

### Sequence diagram
The sequence diagram from the assignment description was followed. We did not create a new one.

## Seperation of work and collaboration

**The design** was done in collaboration, collected around a (digital) white board. 
This meant that everbody was updated and informed about the decision, as well as making sure everbody's ideas were heard. 

**The implementation** 
Each group member was chared with implenting one or more sets of interfaces and implementation. The tests of the implementaions where for the most part allso done by the group members who implemented them. 
Once the implementation where close to being finished and bogs, errors and other fun stuff started to appear, the group tackeled these things in a more dynamic manner. Contributing all over the project. 
To cordinate the implementation process, each group member broke off into feature or fixing branches, that could be merged back into main when a accetble state was acchived. 

These merges where done primaryly though git pull request between the master branch and the development branch. 

## Descriptions and reflections about testing
### Door
When testing this class it was important to check whether an event was received after opening or closing the door and that the door opened event could not be received if the door was locked.
In the `[Setup]` each of the two door events were arranged with lambda expressions to store the event occurence and data when called.
In the `Test`s the action was to raise an event using the methods `OnDoorOpen()` and `OnDoorClosed()`.
The assertions were to check whether the arranged lambda expressions had actually received something (were not `null`)
To test the the door lock the method `LockDoor()` was called as the action and then the assertion was that no event had been received by the lambda.

### Display

The display used the system.Console class to do must of this displaying. 
This resulted in quite a few funny situations, where diffrent parts of the system class, where not implemened the group members different operating systems. 
This in its self where pretty harmless, up on till the point where it was discovered that it was not only missing implementations, but allso diffrennt implementations  on the the OS's. 

The issuse was to confirm that the output from the display reached its intented locations. 
To do this the console output buffer was replaced with a stringWriter. 
The actions performed on the strign writer in the unix enviorment was perfectly fine. 
since the output of clear(), setCursorPosition() and other console methoed unrolled to nothing.
Where on the windows, these calles on a strignWriter, resulted with a natAHandle exception. 

This made it really hard to discovered the issuses before they landed on jenkins. 

To fixit, an interface defining the used parts of the console class was created. 
This interface could then be used sustitute the problematic console metoeds and propertyes. 
On commit 
### LogFile
Since it is the logfile class' responsibility to create logs when the door unlocks and locks. The way to test this is to validate that the information being written into a file is the same as the data that is being expected. Some of the tests in LogFile therefore assert on whether or not the file is created properly and if the file isn't empty. 

Originally the LogFile class was implemented by the use of async, await and tasks. However, this resulted in race conditions between the respective tests and the teardown function which lead to errors from time to time. It was unpredictable to foresee the outcome of a test because it was determined by how fast each of the tests ran in that instance and how the scheduler had assigned timeslots for the different tasks. In the end it was decided to remove the async await and implement the write functionality simpler with the use of a void method.

### RfidReader
The RfidReaderSimulator class works a lot like the other "boundary" classes such as door. The class has an interface called IRfidReader where an event is defined. The event is based on a class called RfidDetectedEventArgs which inherits from EventArgs and contains a single Id which is used to identify what kind of Rfid that has been scanned. The event connected to the RfidReader triggers whenever someone scans their ID. In order to test this class another class was made RfidReaderSimulatorTest. The test class contains a `[SetUp]` function where the UUT is initialised and not much differs from normal testing classes in that sense. Since there is only a property and an event there isn't a lot to test. The interesting part is the event. The event has to be triggered exactly when a user scans their rfid. The test method `[SetId_OnDetectedEvent]` sets the Id by using the property in the RfidReaderSimulator class and test that an event has been sent and that the Id in that event matches the one that has been set by the property. Therefore the assertion is made on the RfidDetectedEventArgs Id property. The event is faked and setup in the `[SetUp]` function of the test class.

### UsbCharger
The `UsbChargerSimulator` class was a part of the handout along with an interface and tests. All we had to do was adapt them with our namespace.

### ChargeControl

In order to test the ChargeControl class, NSubstitute was used to make fakes of the "boundary" classes that are used in the ChargeControl class. The constructor in ChargeControl is used to inject dependencies from the Display and UsbCharger classes by using their interfaces. The interfaces were then also used to create fakes by using NSubstitude. This was done in the `[SetUp]` method in the TestChargeControl class. The UUT (unit under test) could then be created by injecting the fakes into the constructor of the new ChargeControl object. 

For the ChargeControl there are 2 sets of test. The first one is events. By 'events' it is meant that the ChargeControl uses the fakes created by NSubstitute to raise events and assert on the raised events. The other kind of tests were done by using the methods in the ChargeControl class which would call the methods in the "boundary" (Display and UsbCharger) classes, that would then invoke an event that the ChargeControl class could handle.

### StationControl
To test this class NSubstitute was used to make fakes of all dependencies. To use the dependancy-fakes the StationContol class uses constructor injection. It was then possible to raise a made up event to check if it is received by the UUT. 

In a few cases it was necessary to Assert multiple times in the same test, since we did not wish to have public setter on the state of the state machine. The multiple asserts were to check if all methods in the different states of the state machine were called. The test become rather large, but the alternative would have been to make one test for each method called with the same event fired in each of them which seems like more work to do the same thing.

NSubstitude allowed the tests to simulate that a charger was connected to a phone, which is a condition for the door to be allowed to lock and a new ID to be saved. This was done with the `.Returns()` method that is a part of the fakes methods. The call look like this `_chargeControl.IsConnected.Returns(false);`

`OldId` and `DoorOpen` were both originally attributes, but since they were found necessary in some tests, they were made into properties with private setters. This allowed for the tests to actually assert on their value.

The line coverage for the StationControl class is currently on 98.4% because the case where the user tries to scan an rfid while the door is open doesn't impact the system in any way.

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

### Workflow
During the completion of the assignment the group has been using GitHub and Jenkins. As mentioned earlier, github has been used to review code but the most important part was (obviously) parrallel workflows. It was of great assistance to split the work between all the group members and work on seperate occations especially with branching. This way the individual group members could work on their own classes and create tests for said classes. The only problem that could occur was if the tests hadn't been run locally before merging into the main git branch as this would result in errors for the group members pulling said tests. The simple fix would be to always run tests locally and make sure they work before actually merging them into the main branch. It was problematic if the merge (pull) request weren't reviewed before they were merged as this didn't provide optimal coverage of the code.