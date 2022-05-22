
[![CrispyCollab Build and Test](https://github.com/DavidEggenberger/CrispyCollab/actions/workflows/build.yml/badge.svg)](https://github.com/DavidEggenberger/CrispyCollab/actions/workflows/build.yml)

# CrispyCollab

This repository is a **reference application** for building monolithic SaaS solutions with ASP.NET Core, Blazor and EF Core. CrispyCollab features:

- Subscription-based billing (Stripe)
- Multi-tenancy
- Tenant wide operations (sending chat messages)
- Admin Section (e.g. inviting users)
- Domain Driven Design
- CQRS (no library)

### Architecture

CrispyCollab models its entities after the paradigms of Domain Driven Design. CQRS is used to organize the business logic. For each aggregate the Application class library defines the supported Commands and Queries. The respective Command and QueryHandlers reside in the same file. They get executed when the controllers dispatch the according Query or Command.

<img src="https://raw.githubusercontent.com/DavidEggenberger/CrispyCollab/main/Img/Dependencies.png" height=350/>

**WebWasmClient**: Blazor WebAssembly Application \
**WebCommon**: DTOs, Path Constants \
**WebServer**: API, Razor Pages for Identity \
**Application**: Business Logic \
**Infrastructure**: Infrastructure Components (e.g. Database access) \
**Domain**: Entities 

### SaaS Concepts

The possibilities and methods for developping SaaS applications with .NET are endless. For the application showcased three principles are of particular significance.

#### Multitenancy


#### Business Logic respecting tenant


#### Subscription-based billing
