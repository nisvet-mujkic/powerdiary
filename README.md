# Power Diary coding assignment

### Description

In this assignment chat room interface has been modeled and implemented.
Most important aspect is the time-based aggregation of events.

Events can be of type:

- enter-the-room
- leave-the-room
- comment
- high-five-another-user

User can see events in minute by minute aggregation and in hourly aggregation
where the events are grouped together.

### Requirements

Must have installed:

- .NET 7

### How to get the project up and running

- clone the repository
- start the project from Visual Studio
- if everything is ok then a console window will opem
- console window will show both minute by minute and hourly events aggregation

### Project structure

- src
  - Console
    - `PowerDiary.Messaging.Console` - console app implementation
  - Code
    - `PowerDiary.Messaging.Application` - contains business logic of the app
    - `PowerDiary.Messaging.Domain` - contains Entities classes
- tests:
  - `PowerDiary.Messaging.UnitTests` - unit tests for Core projects

### Contributors

Code is written and maintained by Nisvet Mujkic. You can reach me at: nisvet.mujkic@outlook.com.
