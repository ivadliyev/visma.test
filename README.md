
# Visma test task




## FAQ

#### For running locally:

Install .net SDK v8 from [https://dotnet.microsoft.com/en-us/download/dotnet/7.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)



## Running the broker API
Using bash or powershell

```bash
  cd visma.test.broker
  dotnet restore
  dotnet run
```
Swagger UI is available under http://localhost:5067/swagger/index.html

## Running the subscriber console app
Using bash or powershell

```bash
  cd visma.test.subscriber
  dotnet restore
  dotnet run
```
    
## Running Tests

To run tests, run the following command

```bash
  cd visma.test.tests
  dotnet test
```
