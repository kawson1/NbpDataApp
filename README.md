# NbpDataWebApp
This is a .NET 6 web application that retrieves and processes data from a banking service. 


## Setup
To run the application, clone repository and execute the command
```docker build -t nbp-data-app -f Dockerfile .```
from the repository folder (NbpDataWebApp). Then run the command 
```docker create -p 80:80 --name nbpapp nbp-data-app```
 to create a container for the application.

Alternatively you can download image from docker hub
```
https://hub.docker.com/layers/kawson/nbp_data_application/latest/images/sha256-145de87bf05d3486cb637cb75de43c20afa3f91aa3aa6009b89055c7dbfa5985?context=repo
```

## Endpoints
After running the container, you can use the following endpoints:

* `http://localhost/NbpData/exchanges/{code}/{yyyy-MM-dd}` shows data for the currency with the given code on the specified date.
* `http://localhost/NbpData/exchanges/{code}/{count}`:` shows data for the currency with the given code for the N days with the highest and lowest exchange values.
* `http://localhost/NbpData/buyselldiff/{code}/{count}` finds the largest difference between the purchase and sale prices for the currency with the given code over the past N days.

## Examples
For example:

http://localhost/NbpData/exchanges/aud/2016-02-02
http://localhost/NbpData/exchanges/aud/10
http://localhost/NbpData/buyselldiff/aud/5

## Unit tests
Additionally, there are simple unit tests that check the functionality of the NbpDataController methods.