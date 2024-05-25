
[![CrispyCollab Build and Test](https://github.com/DavidEggenberger/CrispyCollab/actions/workflows/build_test.yml/badge.svg)](https://github.com/DavidEggenberger/CrispyCollab/actions/workflows/build_test.yml)

# CrispyCollab

This repository builds upon <a href="https://github.com/DavidEggenberger/ModularMonolith.SaaS.Template">ModularMonolith.SaaS.Template</a>. CrispyCollab is a **reference application** for building monolithic SaaS solutions with ASP.NET Core, Blazor and EF Core. CrispyCollab features:

- Subscription-based billing (<a href="https://stripe.com/docs/payments/checkout">Stripe Checkout</a>)
- Multi-tenancy
- Tenant wide operations (sending chat messages)
- Admin Section (e.g. inviting users)
- Domain Driven Design
- CQRS

## Architecture

Please visit the <a href="https://github.com/DavidEggenberger/ModularMonolith.SaaS.Template">ModularMonolith.SaaS.Template</a> repo for more information. 

## Running CrispyCollab
CrispyCollab's WebServer (it also serves the Blazor WebAssembly client) relies on a Redis instance to store cached values and on a SQL Server to store the application's data. Running CrispyCollab therefore requires these two Infrastructure components to be running as well.  

### Infrastructure
The most convient way to run a Redis and SQL Server instance is through Docker. To do so run this command from the root folder 
(where CrispyCollab.sln file is located):
```
docker-compose -f docker-compose.infrastructure.yml up
```

The SQL Server must be setup before CrispyCollab can be started. Open the Package Manager Console inside Visual Studio and execute the following commands:
```
update-database -context ApplicationDbContext
update-database -context IdentityDbContext
```
These commands will create two seperate databases on the same server (the configuration strings are read from Web/WebServer/appsettings). The identity database storeas all the users informations and the application database all the other data.    

### Web
Before running the WebServer (it serves also the Blazor WebAssembly client) configuration values must be set. They are then accessible through the IConfiguration interface for which ASP.NET Core automatically registers an implementation in the inversion-of-control (DI) container (assuming the configuration resides in appsettings.json or secrets.json). Especially the Infrastructure layer relies on the configuration values (e.g. database connection strings, Stripe API Key). It is highly recommended to keep the following secrets out of source control. For local development right click on the WebServer project and then click on manage user secrets. The opened secrets.json file should then updated to hold the following configuration (the values can be retrieved by following the respective links):

```json
{
  "StripeKey": "to register a stripe account and retrieve the API Key visit: https://dashboard.stripe.com/login",
  "SocialLogins": {
    "Google": {
      "ClientId": "https://chsakell.com/2019/07/28/asp-net-core-identity-series-external-provider-authentication-registration-strategy",
      "ClientSecret": ""
    }
  }
}
```

With the configuration set, the WebServer can either be started through Visual Studio (e.g. for debugging) or by running this command from the root folder 
(where CrispyCollab.sln file is located):
```
docker-compose -f docker-compose.web.yml up
```
