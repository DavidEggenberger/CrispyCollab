
[![CrispyCollab Build and Test](https://github.com/DavidEggenberger/CrispyCollab/actions/workflows/build.yml/badge.svg)](https://github.com/DavidEggenberger/CrispyCollab/actions/workflows/build.yml)

# CrispyCollab

This repository is a **reference application** for building monolithic SaaS solutions with ASP.NET Core, Blazor and EF Core. CrispyCollab features:

- Subscription-based billing (<a href="https://stripe.com/docs/payments/checkout">Stripe Checkout</a>)
- Multi-tenancy
- Tenant wide operations (sending chat messages)
- Admin Section (e.g. inviting users)
- Domain Driven Design
- CQRS (no library, own implementation through using Scrutor)

## Architecture

CrsipyCollab is a web application. The backend, ASP.NET Core, is consumed by the Blazor WebAssembly client. DTO's, constants and shared authorization logic resides in the WebShared class library. To simplifiy deployment/hosting the ASP.NET Core backend references the WebAssembly client. The whole application (excluding SQL Server) subsequently runs in one process. CrsipyCollab follows the paradigms of clean architecture. The application layer loads the specified data by the controllers (Queries), facilitates applying the desired changes (Commands) and reacts to subsequently invoked events (EventHandlers). To do so services defined in the infrastucture layer are used.              

<img src="https://raw.githubusercontent.com/DavidEggenberger/CrispyCollab/main/Img/ProjectDependencies.png" height=350/>

### Projects

**WebWasmClient**: Blazor WebAssembly Application \
**WebCommon**: DTOs, Constants, shared authorization logic \
**WebServer**: API, SignalR Hubs, Razor Pages (Identity, Landing Pages) \
**Application**: Application Logic (Queries, Commands, Events) \
**Infrastructure**: Infrastructure Services (e.g. Database access) \
**Domain**: Entities (Rich domain model, DDD)

### Architectural Concepts
#### Clean Architecture
CrsipyCollab follows the paradigms of Clean Architecture. Layering, dependency inversion, framework and database independence are the main principles of clean architecture. Structuring an application thereafter means spliting-up its code into three distinct layers, Application, Infrastructure and Domain. These layers are implemented as class libraries. Layering in the context of Clean Architecture now means that the dependencies of the respective class library can only point inwards. Referencing an outer circle is not possible. The innermost layer, the Domain layer, has subsequently no external dependencies. The infrastructure layer references the Domain layer. Through referencing the infrastructure layer the application layer has access to the types defined in both the infrastucture and domain class libraries. When an inner layer relies on functionality from an outer layer the inner layer defines an interface for which an outer layer will provide an implementation. This dependency inversion is of particular importance for the domain layer as it can't take on external dependencies. This allows to swap out infrastructure components, e.g. database access, at will without the need to change the domain layer.  

##### Application Layer
The application layer defines the tasks the application is supposed to do. Traditionally most application layers consist of services, e.g. ProductService, which encapsulate the needed CRUD functionality. These services in turn then orchestrate infrastructure components, e.g. database access logic from repositories, to persist the respective changes. As the application grows in functionality this approach can result in bloated services. CrispyCollab uses CQRS to organize its application logic. The principle of the Command Query Responsibility Segregation Pattern is to seperate the read (Queries) from the update (Commands) operations. Once dispatched by the ASP.NET Core backend the Queries/Commands are then handled by their respective Query/CommandHandlers. This subsequently means that every operation is defined in a seperate file. They are grouped by the aggregate of the updated or queried entity. The application layer also defines EventHandlers that handle the events that are dispatched by the domain or infrastructure layer. Note, an event can be handled more than once.    

##### Infrastructure Layer
The infrastructure layer defines infrastructure components. This means most notably database access, authentication and the integration of third-party components (e.g. Stripe). CrispyCollab uses EF Core to persist the data that is initially held in domain entities (in memory) into a Microsoft SQL Server. Besides the ApplicationDbContext also the configuration of the entities (through implementing IEntityTypeConfiguration<T>) resides in the EFCore folder. Like for every Infrastructure component also an extension method used for registering the respective services to ASP.NET Core's Inversion of Control container is defined. The Application and WebServer projects can now receive the services through constructor injection. Note, the type the services are registered for are the respective interfaces. This makes swapping out the concrete implementation for an alternative implemenation a breeze.               

##### Domain Layer
The domain layer contains the domain logic. Domain logic models the business problem the application is tasked to solve. For doing so CrispyCollab follows the principles of Domain Driven Design. With the domain logic at the heart of the application and the entities/value objects containing both data and behaviour complex problem domains can be reliably modelled. The domain layer also includes common exstension methods. The domain layer can only communicate with the outer layers through defining interfaces that are then accordingly implemented by the Infrastructure/Application layer or by dispatching events. When an event is dispatched it gets handled by all its respective EventHandlers defined in the application layer.  
  
#### Domain Driven Design
CrispyCollab models its entities after the paradigms of Domain Driven Design. Entities denote objects that are persisted in a database. The fields and properties of their respective classes, structs or records they were instantiated from then determines the database scheme.

#### Backend for Frontend Pattern

## Showcased SaaS Concepts
Software as a Service, abbreviated as SaaS, is a licensing and delivery model for software. SaaS solutions are mostly billed on a subscription basis and distributed through the web. This frees the users from long-term commitments and the need to install updates. Because the server infrastructure operated by the SaaS vendor stores and computes all the application’s data, the subscription fee can be priced upon usage parameters such as made transactions or the user count in a tenant. 

### Multitenancy
Multitenancy denotes the application simultaniously serving multiple tenants. A tenant is a user group of a SaaS solution. In the context of B2B (Business to Business) a user group is mostly congruent with the employees of the company that subscribes to the SaaS application. The respective admins can then manage the tenant (e.g. changing its name and appearance, modyfiny the functionality, inviting users, giving them admin rights etc.). To enforce that members of a tenant can only operate on data that belongs to their respective tenant every entity is marked with a TenantIdentifier. By using Global Query Filters they are then accordingly filtered. The TenantIdentifier is retrieved from the AuthenticationCookie. It also persists the user's role in the tenant. This implies that whenever the user changes his/her current tenant the AuthenticationCookie is deleted and replaced with a new one.  

### Subscription-based billing
CrispyCollab uses <a href="https://stripe.com/docs/payments/checkout">Stripe Checkout</a>. Through Stripe's dashboard the subscription plans can be created. Each one is identifiable through an Id. The<a href="https://github.com/DavidEggenberger/CrispyCollab/blob/main/Source/Infrastructure/Identity/IdentityDbSeeder.cs"> IdentityDbSeeder</a> then stores the according subscriptions in the database. 

## Running CrispyCollab
### Docker
When running through Docker the appsettings are read from the appsettings.docker.json file. You can run CrispyCollab by running these commands from the root folder (where CrispyCollab.sln file is located):
```
docker-compose --profile crispycollab up
docker-compose build
docker-compose up
```

### Local
When running CrispyCollab locally SQL Server must be installed. The connection string must be added into appsettings.json. From the package manager console you can run the following commands:
```
add-migration initialApplication -context ApplicationDbContext
add-migration initialIdentity -context IdentityDbContext
update-database -context ApplicationDbContext
update-database -context IdentityDbContext
```
