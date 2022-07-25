
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

CrsipyCollab is a web application. The backend, ASP.NET Core, is consumed by the Blazor WebAssembly client. DTO's, constants and shared authorization logic resides in the WebShared class library. To simplifiy deployment/hosting the ASP.NET Core backend references the WebAssembly client. The whole application (excluding SQL Server) subsequently runs in one process. CrsipyCollab follows the paradigms of clean architecture. Summarized the application layer loads the specified data by the controllers (Queries), facilitates applying the desired changes (Commands) and reacts to subsequently invoked events (EventHandlers). To do so services defined in the infrastucture layer are used.             

<img src="https://raw.githubusercontent.com/DavidEggenberger/CrispyCollab/main/Img/ProjectDependencies.png" height=350/>

**WebWasmClient**: Blazor WebAssembly Application \
**WebCommon**: DTOs, Path Constants \
**WebServer**: API, Razor Pages for Identity \
**Application**: Business Logic \
**Infrastructure**: Infrastructure Components (e.g. Database access) \
**Domain**: Entities 


CrsipyCollab models its entities after the paradigms of Domain Driven Design. CQRS is used to organize the application logic. For each aggregate the Application class library defines the supported Commands and Queries. The respective Command and QueryHandlers reside in the same file. They get executed when the controllers dispatch the according Query or Command.

CrispyCollab models its entities after the paradigms of Domain Driven Design. Entities denote objects that are persisted in a database. The fields and properties of their respective classes, structs or records they were instantiated from then determines the database scheme.


### SaaS Concepts

#### Multitenancy
Multitenancy denotes the application being shared by multiple tenants. A tenant describes a group of users. In the context of SaaS they mostly work together in a company. Through a plan (e.g. premium) they subscribe to the SaaS application. Admins can manage their tenant (e.g. changing its name, inviting users or giving them admin rights). To enforce that members of a tenant can only operate on data that belongs to their respective tenant every entity is marked with a TenantIdentifier. By using Global Query Filters they are then accordingly filtered. The TenantIdentifier is retrieved from the AuthenticationCookie. It also persists the user's role in the tenant. This implies that whenever the user changes his/her current tenant the AuthenticationCookie is deleted and replaced with a new one.  

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
