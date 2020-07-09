## City Manager API v1 [![Build Status](https://dev.azure.com/devcore-org/citymanagerv1/_apis/build/status/citymanagerv1%20-%20CI?branchName=master)](https://dev.azure.com/devcore-org/citymanagerv1/_build/latest?definitionId=10&branchName=master)

![.NET Core](https://github.com/devdotcore/CityManager/workflows/.NET%20Core/badge.svg)

The purpose of this API is to add city details to the database and perform CRUD operations like add, update, delete and search. While adding the city details, API asks for the country name which is validated again the list here [Country Name](https://restcountries.eu/rest/v2/all?fields=name;), if the the country name does not match, it will return a error.

Additionally, This API uses [Rest Countries API](https://restcountries.eu/) to get details like 2 digit country code, 3 digit country code and currency code. It also uses [Weather API](https://openweathermap.org/api) to get the current weather for an city.

**This API uses [Swagger](https://swagger.io/), so you can test its endpoint easily via browser**

![Image of Swagger](https://github.com/devdotcore/CityManager/blob/master/github_static/swagger_home.png)

### Features
* **Add** a city to the database. Validate request model based on -
  * City name and Country name are mandatory
  * Date should be less than current date
  * Rating should be between range 1-5
  * Country name should be valid - API will check against [Country Name](https://restcountries.eu/rest/v2/all?fields=name;) to validate. Once the validation is successful, city details along with additional country parameters will be stored in the DB for a faster retrieval.

```markdown
POST /city/add

{
  "name": "string",
  "state": "string",
  "country": "string",
  "touristRating": 0,
  "dateEstablished": "2020-07-09T14:22:06.881Z",
  "estimatedPopulation": 0
}
```

* **Update** city details by city id 

```markdown
PUT /city/{cityId}

{
  "touristRating": 0,
  "dateEstablished": "2020-07-09T14:23:53.824Z",
  "estimatedPopulation": 0
}
```

* **Delete** a city by city id 

```markdown
DELETE /city/{cityId}
```
* **Search** city by name, return all details related to a city including additional country and current weather details details

```markdown
GET /city/{cityName}
```

```markdown
SAMPLE RESPONSE -

[
  {
    "cityId": 1,
    "countryCode2Digit": "GB",
    "countryCode3Digit": "GBR",
    "currencyCode": "GBP",
    "weatherDetails": {
      "weather": [
        {
          "main": "Clouds",
          "description": "overcast clouds"
        }
      ],
      "main": {
        "temp": 18.15
      }
    },
    "name": "Bath",
    "state": "Somerset",
    "country": "United Kingdom of Great Britain and Northern Ireland",
    "touristRating": 5,
    "dateEstablished": "2010-07-09T16:17:22.62",
    "estimatedPopulation": 560000
  }
]
```

### Setup
The following steps are for running the codebase locally on a Mac using Visual Studio Code, you find similar or better options online.

**The code use EFCore data first approach, please make sure to change connection string in appsettings.json to point the sql server with right permission. EF Migration will be applied on application start.**

1. Download and install [.Net Core 3.1.202](https://dotnet.microsoft.com/download/dotnet-core/3.1) SDK on your machine.
2. Download this repo into a working directory and take latest from master branch
```markdown
git clone https://github.com/devdotcore/CityManager.git
```
3. Open the project in [VS Code](https://code.visualstudio.com/) and build -
```markdown
dotnet build
```
4. Make sure all the test case are running -
```markdown
dotnet test
```
5. Start the project directly on VS Code or run the following command -
```markdown
dotnet run --project ./src/CityManager/CityManager.csproj
```
5. By default, the API will be available on https://localhost:5000

### Docker Build
If you have [Docker Desktop](https://www.docker.com/products/docker-desktop) installed and running on you machine, you can deploy the application locally using docker.

```markdown
 docker build -t [IMAGE_NAME]:[TAG] .  
```
once build is complete, you can start the container -

```markdown
docker run --rm -it -p [PORT_NUMBER]:5000 [IMAGE_NAME]:[TAG]
```

API should be available locally -

```markdown
https://localhost:[PORT_NUMBER]/
```

Copyright (c) 2020, Kuldeep S Bhakuni
