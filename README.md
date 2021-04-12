# VoiceModTechTest
Test for the application in VoiceMod.

# Patterns and architecture used:
I have used native Dependency injections, Fleck and log4net packages, besides some Microsoft.Extensions for the configuration and Dependency Injections.
The solution uses Services as the main approach, one main service determines whether to use Client or Server services.

The main reason to do the ConsoleWrapper was due to the tests, since Console is a static class and cannot be mocked.

I have also used a Factory Pattern (declared as singletons) to generate either clients or servers given the port or the port and the username respectively.
They inherit from an AbstractFactory class that contains a const value to get the local IP. (this could have been done better through a configuration value).

I also created a ClientModel to wrap the username, their socket and the token. this instance is used in the ClientService to send the messages to the server.

The ServerService just creates the server and listens to any connections made to the specified port. It receives and prints the messages written from the clients.

The ClientService using the ClientModel allow the clients to write and send messages. These messages automatically have added a timestamp and the username at the beginning of the message, as if it were a template.

I have tested this with many clients connected at the same time and the server displays the messages from all the clients, and also notifies when a client has logged in or out.

Regarding to the Tests, I have used Moq and NUnit to create the tests. I know they are not covering all possible cases, but I hadn't more time :( 
However, I made some mockups and different asserts.

The ServerFactory also reads a configuration value when creating the server from the appconfig.json.

## Time invested: 
I have invested about 5h~ including:
- Some research about Fleck (I have never used it before).
- Design of the best approach.
- Setting up the solution and downloading the packages.
- Test correction and ConsoleWrapper refactor.

## Personal observations
I would have done this creating separate projects for Client and Server side, and creating a third one with classes used by both.
One of those classes would be the ConsoleWrapper.

A lot of improvements can be made, such as encripting the messages from the client to the server, as well as using WSS with a certificate.

Any feedback is appreciated! :)
