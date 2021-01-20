# Microservices Web Application

 The project was developed as part of the assignment for the course Advanced software technologies on Master's studies - Software Engineering and Computer Sciences at the Faculty of Organization Sciences, University of Belgrade, Serbia.
 
 ## About the application
 
 The online shopping software system is an example of a web application that allows users to purchase wines from different producers. Product information can be found in the wine catalog that is shown to customers. The user logs in to the system using his credentials, and if he does not have his own profile, he needs to register. The user can search the wine catalog and add products to their cart. It is necessary to enable the user to view the contents of the basket, remove products from it and update the amount of products in the basket. When deciding to purchase, it is necessary for the user to fill in the form with the necessary payment information and submit it, and finally to click the button to confirm the purchase. For certain users, there are coupons with which the user can get a discount on the purchase. The user can see the ordering history at any time.
 
 ## Use case diagram
 
Based on the verbal model, these are the cases that were identified:
 
 ![Screenshot](/img/Screenshot_2.png)
 
 ## Class diagram
 
 ![Screenshot](/img/Screenshot_3.png)
 
 ## Overall picture
 
 See the overall picture of implementations on microservices with .net tools on real-world e-commerce microservices project.
 
 ![Screenshot](/img/architecture.jpg)
 
 There is a couple of microservices which implemented e-commerce modules over Catalog, Discount, ShoppingBasket, Payment and Ordering microservices with relational databases (Sql Server) with communicating over Azure Service Bus and using Ocelot API Gateway and IdentityServer4 with OAuth2 and OpenID Connect tools.
 
# Whats Including In This Repository
 
 We have implemented below features over the run-aspnetcore-microservices repository.

## Identity microservice which includes;

- Handles the authentication part using username, password as input parameter and issues a JWT Bearer token with Claims-Identity info in it
- Identity Server4 is an open source framework which implements OpenId Connect and OAuth2 protocols for .Net Core. With Identity Server, we can provide authentication and access control for our web applications or Web APIs from a single point between applications or on a user basis.

## WineCatalog microservice which includes;

- ASP.NET Core Web API application
- REST API principles, CRUD operations
- Entity Framework Core on SQL Server
- N-Layer implementation with Repository Pattern
- Swagger Open API implementation
- Implementation of AutoMapper

## Discount microservice which includes;

- ASP.NET Core Web API application
- REST API principles, CRUD operations
- Entity Framework Core on SQL Server
- N-Layer implementation with Repository Pattern
- Swagger Open API implementation
- Implementation of AutoMapper
- Publishing message to Azure Service Bus

## ShoppingBasket microservice which includes;

- ASP.NET Core Web API application
- REST API principles, CRUD operations
- Entity Framework Core on SQL Server
- Azure Service Bus trigger topic when checkout cart, publishing message to Azure Service Bus
- Swagger Open API implementation
- Implementation of AutoMapper
- Implementation of IHostedService as ServiceBusListener to listen to the bus and receive a message when the price in the catalog is updated 

## Azure Service Bus messaging library which includes;

- Base IMessageBuss implementation and base message

## Ordering microservice which includes;

- ASP.NET Core Web API application
- Entity Framework Core on SQL Server docker
- REST API principles, CRUD operations
- Consuming Azure Service Bus messages
- Swagger Open API implementation
- Publishing message to Azure Service Bus when the order payment status needs to be updated

## Payment microservice which includes;

- ASP.NET Core Web API application
- Entity Framework Core on SQL Server docker
- REST API principles, CRUD operations
- Consuming Azure Service Bus messages
- Swagger Open API implementation
- Publishing message to Azure Service Bus when the order payment status needs is updated
- Implementation of IExternalGatewayPaymentService in order to communicate with Payment Gateway

## API Gateway Ocelot which includes;

- Validates the incoming Http request by checking for authorized JWT token in it.
- Reroute the Http request to a downstream service.

## Fake Payment Gateway which includes;

- Routing to inside microservices

## WineShopMVC microservice which includes;

- Asp.net Core Web Application with Razor pages
- Call Ocelot APIs with HttpClientFactory
- Aspnet core razor tools - View Components, partial Views, Tag Helpers, Model Bindings and Validations etc..
- This interactive MVC Client application will be secured with OpenID Connect in IdentityServer4. Our client application pass credentials with logging to an Identity Server and receive back a JSON Web Token (JWT).

# Run The Project

You will need the following tools:

Visual Studio 2019
.Net Core 3.1 or later

## Installing

Follow these steps to get your development environment set up: 

1. Clone the repository
2. Run update-database for each microservice that uses SQL Server database
3. Multiple startup projects. Right click the solution, open Properties, and set Multiple startup project and Start all except Integration library, click apply and ok.
4. The browser will open, and you will be redirected to the Login page. If there is need, you can register a new user, or use existing:
username: "alice" password: "Pass123"

 ![Screenshot](/img/Screenshot_4.png)
