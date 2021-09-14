# Weather API

Simple dotnet app that exposes a graph schema for open weather maps api. 
This app uses Hot Chocolate 11 and Polly for resilience. 

## Setup

Set the environment variables for the [api key](https://openweathermap.org).

### Windows

```shell
set ApiKey="XXXXXXXXXXXXX"
```

### Unix

```shell
export ApiKey="XXXXXXXXXXXXX"
```

Then stat with `dotnet run`.

## Local Development

Banana cake pop can be found at http://localhost:5000/graphql/
