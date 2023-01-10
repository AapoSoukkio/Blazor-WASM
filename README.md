# Project: Blazor WASM

Made by: Aapo Soukkio

***

## Purpose of this project

The purposes of this project are:

- to learn to make a Blazor WebAssembly app
- to learn to use external web api in a Blazor app
- to learn about basic controls in Blazor

## About the project

Blazor WebAssembly (Blazor WASM) app to handle person and address data. UI for all people from the API and UIs for adding new persons, addresses and joining an address to a person. The API uses Sqlite as database but the data model is simplified with no database relations with different types of data.

The Blazor app handles joining the data so that proper data is shown (i.e. only addresses linked to the person is shown for the person). Uses async calls to the API and handles them properly in the client app.

`src/BlazorPeople` is the BlazorWASM app. 
`src/PeopleApi` is the web API.
`src/PeopleLib` is a class library containing the data models used by both the Blazor app and the web API.

### How to run the project

To run both the Web API and the Blazor client app:

1. If using Visual Studio, set both as startup projects
2. If using dotnet you can use following commands when in src folder

To run the API in localhost on desired port (the sample below uses 8001).

```sh
dotnet run --project PeopleApi/PeopleApi.csproj --urls "https://localhost:8001"
```

> Note! The Web API has a port setting in Properties/launchSettings.json file to run on **https://localhost:5000** by default.


To run the Blazor app in localhost on desired port (the sample below uses 9001).

```sh
dotnet run --project BlazorPeople/BlazorPeople.csproj --urls "https://localhost:9001"
```

> Note! The BlazorPeople app has a port setting in Properties/launchSettings.json file to run on **https://localhost:3000** by default.


## Tests

Because this was originally a school project, there is some unittests included in test-folder. Purpose of tests was to make grading easier for the course lecturer. 
Checkpoints are designed so that they may require additional code,
that is not checked or tested, to function correctly.
There is instructions how to run the tests in test/README.md file.


## Helpful Links

> **Following material will help to understand this project and learn more about Blazor WASM.**


1. [ASP.NET Core Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-6.0)
2. [Build a Blazor todo list app](https://docs.microsoft.com/en-us/aspnet/core/blazor/tutorials/build-a-blazor-app?view=aspnetcore-6.0&pivots=webassembly)
3. [Call a web API from ASP.NET Core Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-6.0&pivots=webassembly)
4. [ASP.NET Core Blazor routing and navigation](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-6.0)
5. [ASP.NET Core Blazor forms and validation](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-6.0)
