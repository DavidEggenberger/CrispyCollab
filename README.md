
[![CrispyCollab Build and Test](https://github.com/DavidEggenberger/CrispyCollab/actions/workflows/build.yml/badge.svg)](https://github.com/DavidEggenberger/CrispyCollab/actions/workflows/build.yml)

# CrispyCollab

This repository is a **reference application** for building monolithic SaaS solutions with ASP.NET Core, Blazor and EF Core. CrispyCollab features:

- Subscription-based billing (<a href="https://stripe.com/docs/payments/checkout">Stripe Checkout</a>)
- Multi-tenancy
- Tenant wide operations (sending chat messages)
- Admin Section (e.g. inviting users)
- Domain Driven Design
- CQRS (no library, own implementation through using Scrutor)

### Architecture

CrispyCollab models its entities after the paradigms of Domain Driven Design. CQRS is used to organize the business logic. For each aggregate the Application class library defines the supported Commands and Queries. The respective Command and QueryHandlers reside in the same file. They get executed when the controllers dispatch the according Query or Command.

<img src="https://raw.githubusercontent.com/DavidEggenberger/CrispyCollab/main/Img/ProjectDependencies.png" height=350/>

**WebWasmClient**: Blazor WebAssembly Application \
**WebCommon**: DTOs, Path Constants \
**WebServer**: API, Razor Pages for Identity \
**Application**: Business Logic \
**Infrastructure**: Infrastructure Components (e.g. Database access) \
**Domain**: Entities 

### SaaS Concepts

The possibilities and methods for developping SaaS applications with .NET are endless. For the application showcased three principles are of particular significance.

#### Multitenancy
Multitenancy denotes the application being shared by multiple tenants. A tenant describes a group of users. In the context of SaaS they mostly work together in a company.

#### Business Logic respecting tenant


#### Subscription-based billing
CrispyCollab uses <a href="https://stripe.com/docs/payments/checkout">Stripe Checkout</a>. Through Stripe's dashboard the subscription plans can be created. Each one is identifiable through an Id. The<a href="https://github.com/DavidEggenberger/CrispyCollab/blob/main/Source/Infrastructure/Identity/IdentityDbSeeder.cs"> IdentityDbSeeder</a> then stores the according subscriptions in the database. 

## Running CrispyCollab
#### Docker
When running through Docker the appsettings are read from the appsettings.docker.json file. You can run CrispyCollab by running these commands from the root folder (where CrispyCollab.sln file is located):
```
docker-compose build
docker-compose up
```

#### Local
When running CrispyCollab locally SQL Server must be installed. The connection string must be added into appsettings.json. From the package manager console you can run the following commands:
```
add-migration initialApplication -context ApplicationDbContext
add-migration initialIdentity -context IdentityDbContext
update-database -context ApplicationDbContext
update-database -context IdentityDbContext
```
