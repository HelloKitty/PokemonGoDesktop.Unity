# PokemonGoDesktop.Unity [Project Closing]

#**I've decided to end this project due to the action Niantic has been taking against developers in the community. This is a fully functional base layer for what is required to build a Pokemon Go client that connects to the offical servers in Unity3D. You can see examples of how request/response handling is implemented in the game repo. I will be moving on to an IRL MMO project similar to Pokemon Go which will be open-source if you're interested.**

PokemonGoDesktop.Unity is an Implementation of the PokemonGoDesktop.API for Unity3D. Based on async/await practices in Pokemon Go HTTP bots PokemonGoDesktop utilizes a custom async API to allow for easy coroutine and callback-based HTTP handling on the main thread.

**What is PokemonGoDesktop.API?**

[PokemonGODesktop.API](https://github.com/HelloKitty/PokemonGoDesktop.API) is a collection of **net35** libraries and APIs that can be used to implement a fully functional desktop version of Pokemon Go. It's built on top of reverse engineered work from the community, based on the protobuf defintions ranging from python projects to .Net projects. PokemonGODesktop.API channels the entire community's work into a push for a standalone version of the game.

## Project Overview

[Unity HTTP](https://github.com/HelloKitty/PokemonGoDesktop.Unity/tree/master/src/PokemonGoDesktop.Unity.HTTP): Contains barebones POCOs, services and async interfaces and contracts for dealing with Pokemon Go over HTTP. Defines the majority of the async contracts in the library and is agnostic to any particular HTTP implementation.

[Unity HTTP RestSharp](https://github.com/HelloKitty/PokemonGoDesktop.Unity/tree/master/src/PokemonGoDesktop.Unity.HTTP.RestSharp): Contains a RestSharp (custom Unity3D fork) implementation for the HTTP contracts and async mentioned earlier.

[Unity IoC](https://github.com/HelloKitty/PokemonGoDesktop.Unity/tree/master/src/PokemonGoDesktop.Unity.IoC): Contains Inversion of Control container registration modules for Sceneject. A basic barebones API for rigging up depedency injection in the Unity3D editor with the concept of a depdency being scene-wide.

## Attributions

Proto Definitions: https://github.com/AeonLucid/POGOProtos

Auth and Bot Logic: https://github.com/FeroxRev/Pokemon-Go-Rocket-API

## Setup

To use this project you'll first need a couple of things:
  - Visual Studio 2015
  - Unity3D 5.3.4f1

## Builds

Soon to be on Nuget!

PokemonGoDesktop.API is already on Nuget though; check the project [README](https://github.com/HelloKitty/PokemonGoDesktop)

##Tests

#### Linux/Mono - Unit Tests
||Debug x86|Debug x64|Release x86|Release x64|
|:--:|:--:|:--:|:--:|:--:|:--:|
|**master**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/HelloKitty/PokemonGoDesktop.Unity.svg?branch=master)](https://travis-ci.org/HelloKitty/PokemonGoDesktop.Unity) |
|**dev**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/HelloKitty/PokemonGoDesktop.Unity.svg?branch=dev)](https://travis-ci.org/HelloKitty/PokemonGoDesktop.Unity)|

#### Windows - Unit Tests

(Done locally)

##Licensing

This project is licensed under the GPL.
