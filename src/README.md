# This folder must containt the task source(s)

The solution to the task is created in the `src` folder.

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
