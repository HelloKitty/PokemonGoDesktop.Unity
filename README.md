# PokemonGODesktop.API

PokemonGODesktop.API is a collection of **net35** libraries and APIs that can be used to implement a fully functional desktop version of Pokemon Go. It's built on top of reverse engineered work from the community, based on the protobuf defintions ranging from python projects to .Net projects. PokemonGODesktop.API channels the entire community's work into a push for a standalone version of the game.

## Project Overview

[Proto Definitions](https://github.com/HelloKitty/PokemonGoDesktop.API/tree/master/src/PokemonGoDesktop.API.Proto): Contains the .proto definitions for serializable types and request/response message types. Additionally it contains the code generated Google.Protobuf classes that are actually compilable.

[Proto Compiler](https://github.com/HelloKitty/PokemonGoDesktop.API/tree/master/src/PokemonGoDesktop.API.Proto.Compiler): Contains the source for the simple directory recursive argument builder that are needed to compile the .proto definitions with Protoc. Also invokes Protoc with those arguments generating the code in the **Gen** directory of the Proto project. Additionally, this compiler generates marker interfaces on some proto classes to help enforce some compiler protection for things.

[Proto Services](https://github.com/HelloKitty/PokemonGoDesktop.API/tree/master/src/PokemonGoDesktop.API.Proto.Services): Contains extensions for various important proto generated classes for Pokemon Go. Also implementing some services for creating or reading from commonly used proto classes in Pokemon Go.

[Client Services](https://github.com/HelloKitty/PokemonGoDesktop.API/tree/master/src/PokemonGoDesktop.API.Client.Services): Contains interfaces, contracts and types important to building a Pokemon Go client. Including only the bare minimum. Contains nothing related to game logic.

## Attributions

Proto Definitions: https://github.com/AeonLucid/POGOProtos

Auth and Bot Logic: https://github.com/FeroxRev/Pokemon-Go-Rocket-API

## Setup

To use this project you'll first need a couple of things:
  - Visual Studio 2015

## Builds

Available on a Nuget Feed: https://www.myget.org/F/hellokitty/api/v2 [![hellokitty MyGet Build Status](https://www.myget.org/BuildSource/Badge/hellokitty?identifier=a8048ae0-adcd-4997-8862-c3f5fc6adf34)](https://www.myget.org/feed/Packages/hellokitty)

##Tests

#### Linux/Mono - Unit Tests
||Debug x86|Debug x64|Release x86|Release x64|
|:--:|:--:|:--:|:--:|:--:|:--:|
|**master**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/HelloKitty/PokemonGoDesktop.API.svg?branch=master)](https://travis-ci.org/HelloKitty/PokemonGoDesktop.API) |
|**dev**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/HelloKitty/PokemonGoDesktop.API.svg?branch=dev)](https://travis-ci.org/HelloKitty/PokemonGoDesktop.API)|

#### Windows - Unit Tests

(Done locally)

##Licensing

This project is licensed under the GPL.
