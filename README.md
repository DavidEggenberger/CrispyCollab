
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

## Using and Running CrispyCollab
### TailwindCSS
If you want to start building with the template and adding your own tailwind classes you must run the following commands from the Source directory:
```
npm install -D tailwindcss
npx tailwindcss init
npx tailwindcss -i ./TailwindSource.css -o ./Web/Server/wwwroot/dist/output.css --watch
```

### Infrastructure
The most convient way to run a Redis and SQL Server instance is through Docker. To do so run this command from the root folder 
(where CrispyCollab.sln file is located):
```
docker-compose -f docker-compose.infrastructure.yml up
```

The SQL Server must be setup before the template can be successfully run. Because the EF Core migrations were already created they only must be applied to the database. Open the Package Manager Console inside Visual Studio and execute the following two commands:
```
update-database -context TenantIdentityDbContext
update-database -context SubscriptionsDbContext
```
These commands will create two seperate shemes with their respective tables on the same database (the configuration string is read from Web/Server/appsettings.Development.json).

### Web
Before running the WebServer (it serves also the Blazor WebAssembly client) configuration values must be set. They are then accessible through the IConfiguration interface for which ASP.NET Core automatically registers an implementation in the inversion-of-control (DI) container (assuming the configuration resides in appsettings.json or secrets.json). Especially the Infrastructure layer relies on the configuration values (e.g. database connection strings, Stripe API Key). It is highly recommended to keep the following secrets out of source control. For local development right click on the Web.Server project and then click on manage user secrets. The opened secrets.json file should then updated to hold the following configuration (the values can be retrieved by following the respective links):

```json
{
  ,
  "EFCoreConfiguration": {
    "SQLServerConnectionString_Dev": "Server=127.0.0.1,1433;Database=ModularMonolith;User Id=SA;Password=YourSTRONG!Passw0rd;Encrypt=False;"
  },
  "SubscriptionsConfiguration": {
    "StripeProfessionalPlanId": "_"
  },
  "TenantIdentityConfiguration": {
    "GoogleClientId": "_",
    "GoogleClientSecret": "_",
    "MicrosoftClientId": "_",
    "MicrosoftClientSecret": "_",
    "LinkedinClientId": "_",
    "LinkedinClientSecret": "_"
  }
}
```

With the configuration set, the Web.Server project can be started through Visual Studio.
