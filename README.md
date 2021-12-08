# powerplant-coding-challenge

## Setup

- Make sure you have .NET installed
- Clone or download the repository (if you download it, don't forget to extract it)
- In a terminal , Move to the root folder of the repository
- type the command "dotnet run"
- You can now make a POST request at "https://localhost:8888/productionplan"
- You can make a POST request with programs such as POSTMAN
- Make sure the body/payload is sent along with the POST request
- Make sure the body/payload is sent as a json
- Make sure the body/payload fits the model (an example is provided below, you can just copy paste it and change the values)
- You will get the power needed on each powerplants to fullfill the load

## Alternative

- If you do not wish to clone or download the repository , you can make the POST request at a specific URL
- You can make a post request at "genesis-challenge.francecentral.azurecontainer.io/productionplan"
- You can make a POST request with programs such as POSTMAN
- Make sure the body/payload is sent along with the POST request
- Make sure the body/payload is sent as a json
- Make sure the body/payload fits the model (an example is provided below, you can just copy paste it and change the values)
- This URL contains the deployed docker image of the REST API

## Testing

- It is possible to test the main method ( the one that finds all the power needed for each powerplants)
- The testing file is in the "Tests" folder
- To start the test, just type "dotnet test" on the terminal
- The payloads used for testing are in the example_payloads folder. They will all be tested (You are free to change the values)
- The testing file will always test if the sum of all the power generated by the powerplants equals the load given
- It will also test on each powerplants if the power generated fits the range (meaning equal or in between the maximum power and the minimum power and ,if not used, equal to 0)

## Example payload/body for the POST Request

```json
{
  "load": 480,
  "fuels": {
    "gas(euro/MWh)": 13.4,
    "kerosine(euro/MWh)": 50.8,
    "co2(euro/ton)": 20,
    "wind(%)": 60
  },
  "powerplants": [
    {
      "name": "gasfiredbig1",
      "type": "gasfired",
      "efficiency": 0.53,
      "pmin": 100,
      "pmax": 460
    },
    {
      "name": "gasfiredbig2",
      "type": "gasfired",
      "efficiency": 0.53,
      "pmin": 100,
      "pmax": 460
    },
    {
      "name": "gasfiredsomewhatsmaller",
      "type": "gasfired",
      "efficiency": 0.37,
      "pmin": 40,
      "pmax": 210
    },
    {
      "name": "tj1",
      "type": "turbojet",
      "efficiency": 0.3,
      "pmin": 0,
      "pmax": 16
    },
    {
      "name": "windpark1",
      "type": "windturbine",
      "efficiency": 1,
      "pmin": 0,
      "pmax": 150
    },
    {
      "name": "windpark2",
      "type": "windturbine",
      "efficiency": 1,
      "pmin": 0,
      "pmax": 36
    }
  ]
}
```

## Issues

If there are any issues , make sure

- that you typed "dotnet restore" and then "dotnet run"
- that the payload is in the appropriate format (such as the example above)
- that you're sending the body to the correct url : "https://localhost:8888/productionplan" or "genesis-challenge.francecentral.azurecontainer.io/productionplan"
