# EtteplanMore-CodeTest
Kooditehtävä Etteplan More:lle, Asp.Net Core RESTful Api

Sovellus aukeaa kun käyttäjä valitsee "Startup Projects" valikosta "EtteplanMORE.ServiceManual.Web", jonka jälkeen "IIS Express" aukaisee selaimen, jolloin rajapinta on käynnissä. 

Sovellus käyttää paikallista tietokantaa, jonka se luo ensimmäisen käynnistyksen yhteydessä. Tietokantaa ei ole alustettu tämän takia.

Sovelluksessa on kaksi Controlleria, joiden avulla käyttäjä voi vaikuttaa tietokantoihin.

FactoryDeviceController: ---------------------------------------------------------------------------------------------------------------
Tehdaslaitteiden ohjaaja, jossa on kaksi HTTP fuktiota.

Get(): ------------------------------------------------------------------------------------------------------------
Hakee kaikki laitteet, lisäksi listaa kaikki huoltotarpeet kyseisille laitteille. 

Esimerkkihaku:
https://localhost:44376/api/FactoryDevices/

Esimerkkivastaus:
[
    {
        "id": 2,
        "name": "Device 2",
        "year": 1885,
        "type": "29",
        "maintenances": [
            {
                "id": 2,
                "timeReported": "2015-04-20T00:00:00",
                "description": "Öljyn vaihto",
                "urgencyLevel": 0,
                "fixed": true,
                "factoryDeviceId": 2
            }
        ]
    }
]
Get() end-----------------------------------------------------------------------------------------------------------

Get(int id): -------------------------------------------------------------------------------------------------------
Hakee yhden laitteen tietyllä id:llä, lisäksi listaa kaikki huoltotarpeet kyseisille laitteille. 

Esimerkkihaku:
https://localhost:44376/api/FactoryDevices/2

Esimerkkivastaus:
[
    {
        "id": 2,
        "name": "Device 2",
        "year": 1885,
        "type": "29",
        "maintenances": [
            {
                "id": 2,
                "timeReported": "2015-04-20T00:00:00",
                "description": "Öljyn vaihto",
                "urgencyLevel": 0,
                "fixed": true,
                "factoryDeviceId": 2
            }
        ]
    }
]
Get(int id) end-----------------------------------------------------------------------------------------------------

FactoryDeviceController end-------------------------------------------------------------------------------------------------------------

FactoryDeviceMaintenanceController: ----------------------------------------------------------------------------------------------------
Tehdaslaitehuolto ohjaaja, jossa on viisi HTTP fuktiota.

GetMaintenances(): -------------------------------------------------------------------------------------------------
Hakee kaikki huollot sekä id:t huollettavalle laitteelle. Haku on järjestelty niin, että korkeat urgencyLevel huollot ovat ensin, sitten tulevat aikajärjestyksessä uusimmat. Haku ei ota huomioon, että onko huolto tehty vai ei.

Esimerkkihaku: 
https://localhost:44376/api/FactoryDeviceMaintenance

Esimerkkivastaus: 
[
    {
        "id": 2004,
        "timeReported": "2015-04-20T00:00:00",
        "description": "Ruuvi löysällä",
        "urgencyLevel": 2,
        "fixed": true,
        "factoryDeviceId": 1
    },
    {
        "id": 5,
        "timeReported": "2019-04-20T00:00:00",
        "description": "Öljyjen vaihto",
        "urgencyLevel": 1,
        "fixed": true,
        "factoryDeviceId": 1
    }
]
GetMaintenances() end ----------------------------------------------------------------------------------------------

GetMaintenance(int id): --------------------------------------------------------------------------------------------
Hakee id:tä vastaavan huollon sekä id:n huollettavalle laitteelle.

Esimerkkihaku: 
https://localhost:44376/api/FactoryDeviceMaintenance/2004

Esimerkkivastaus: 
[
    {
        "id": 2004,
        "timeReported": "2015-04-20T00:00:00",
        "description": "Aikainen homma",
        "urgencyLevel": 2,
        "fixed": true,
        "factoryDeviceId": 1
    }
]
GetMaintenance(int id) end -----------------------------------------------------------------------------------------

Post([FromBody]FactoryDeviceMaintenance maintenance): --------------------------------------------------------------
Lähettää uuden datan tietokantaan. Jos käyttäjä yrittää laittaa tietoa ilman, että hän käyttää olemassa olevaa FactoryDeviceId:tä, niin sovellus kaatuu.

Esimerkkilähetys: 
HTTP Post: https://localhost:44376/api/FactoryDeviceMaintenance/

Esimerkkidata:
{
      "timeReported": "2017-04-20T00:00:00",
      "description": "Kahvit kaatuivat tietokoneen päälle",
      "urgencyLevel": "Kriittiset",
      "fixed": false,
      "FactoryDeviceId": 2
}

Huomaa, että urgencyLevel on enum muuttuja, joten siihen voi käyttää myös numeroita tiedonsyötössä:
{
      "timeReported": "2017-04-20T00:00:00",
      "description": "Kahvit kaatuivat tietokoneen päälle",
      "urgencyLevel": 2,
      "fixed": false,
      "FactoryDeviceId": 2
}

Esimerkkivastaus: Status: 201 Created
Post([FromBody]FactoryDeviceMaintenance maintenance) end -----------------------------------------------------------

Put(int id, [FromBody] FactoryDeviceMaintenance maintenance): ------------------------------------------------------
Muokkaa jo olemassa olevaa tietoa käyttäjän antaessa tieto ja huolto id. Sovellus ei vaihda FactoryDeviceId:tä, sillä vanha huolto ei voi vaihtaa kohdettaan yhtäkkiä. Myöskään Id:tä (huolto id) ei tarvitse käyttää, sillä sovellus hakee sen linkin lopusta.

Esimerkkilähetys: 
HTTP Put: https://localhost:44376/api/FactoryDeviceMaintenance/1

Esimerkkidata:
{
      "Id": 1;
      "timeReported": "2017-04-20T00:00:00",
      "description": "Kahvit kaatuivat tietokoneen päälle",
      "urgencyLevel": 2,
      "fixed": true,
      "FactoryDeviceId": 2
}

Esimerkkivastaus: Status: 200 Ok

Put(int id, [FromBody] FactoryDeviceMaintenance maintenance) end ---------------------------------------------------

Delete(int id): ----------------------------------------------------------------------------------------------------
Poistaa olevassa olevaa dataa Id:llä joka annetaan. 

Esimerkkilähetys: 
HTTP Delete: https://localhost:44376/api/FactoryDeviceMaintenance/1

Esimerkkivastaus: Status: 200 Ok

Delete(int id) end--------------------------------------------------------------------------------------------------

FactoryDeviceMaintenanceController end--------------------------------------------------------------------------------------------------
