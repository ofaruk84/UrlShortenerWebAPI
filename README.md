
# URL Shortener

A Url Shortener Which Is Developed With ASP.NET CORE Web API And MSSQL

## Project Overview

This project has been written in N-Layered Architecture and used frameworks/libraries are:

* Entity Framework Core
* Fluent Validation

And Built-In IoC container are used as a dependency resolver.

### Layers
#### Entity

This layer contains Database model(entity) and Data Transfer Objects(DTO)

#### Data Access

The communication between database and application are handled by this layer via Entity Framework Core.

* Data Access Layer Interface: DataAccess>Abstract>IUrlModalDal.cs
* Data Access Layer Class: DataAccess>Concrete>EntityFramework>EfUrlModalDal.cs
* FluentAPI Class: DataAccess>Concrete>EntityFramework>Mappings>UrlModalMapper.cs

#### Business

This layer contains all the staff for the business.

* Service Interface: Business>Abstract>IUrlModalService.cs
* Manager Class: Business>Concrete>UrlModalManager.cs
* Response Messages:Business>Constants>Messages>Messages.cs
* UrlHasher:Business>Utilities>Hashing>UrlHasher.cs
* Validation Class :Business>ValidationRules>UrlRequestModalValidator.cs
* Extension Class For IServiceCollection :Business>Extensions>ServiceCollectionExtensions.cs

#### Core
This layer is project independent and contains generic classes.


* CRUD operations Repository: Core>DataAccess>IEntityRepository.cs
* CRUD Operations Class For Entity Framework:Core>DataAccess>EntityFramework>EfEntityRepositoryBase.cs
* API Response Result Structure Messages:Core>Utilities>Results
* Business Engine For FluentValidation : Core>CrossCuttingConcerns>Validation>FluentValidation

#### WebAPI
This layer contains api configuration and controller classes.
