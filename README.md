# Designing a Projection

This project is part of the [MessageHandler processing patterns](https://www.messagehandler.net/patterns/) library.

MessageHandler is distributed under a commercial license, for more information on the terms and conditions refer to [our license page](https://www.messagehandler.net/license/).

## What you need to get started

- The [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download) should be installed
- The sample was created using [Visual Studio 2022 community edition](https://visualstudio.microsoft.com/vs/)
- A general purpose [azure storage account](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal) is used to retrieve events from.
- The **MessageHandler.EventSourcing.AzureTableStorage** package is available from [nuget.org](https://www.nuget.org/packages/MessageHandler.EventSourcing.AzureTableStorage/)

## Running the sample

Prior to being able to run the sample, you need to [configure the user secrets file](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows#manage-user-secrets-with-visual-studio).

In the secrets file you must specify the following configuration values.

```JSON
{
  "azurestoragedata": "your azure storage connection string goes here",
  "servicebusnamespace": "your azure service bus connection string goes here"
}
```

Also ensure a topic named `orderbooking.events`, with a subscription called `orderbooking` is created up front in the service bus namespace.

## What is a Projection

Projections derive the current state from an event stream. 

![Projection](./img/projection.jpg)

## When to use it

Use this pattern to turn an event stream into state that can be interpretted by users.

## Usage of the Projection pattern

Basic usage of the projection pattern involves the following steps:
- Build up a list of events that represent the history of one, or more, Aggregate Root instances.
- Create an empty state model
- Apply projection logic to copy data from the event history into the state model
- Store the state model

```C#
TODO
```

## Implementing the Projection

Implementing a projection requires only a single step:

- Implement an `IProjection<TModel, TEvent>` which copies data from `TEvent` into the `TModel` instance.

```C#
TODO
```

## Restoring a full projection from Azure Table Storage

TODO

```C#
TODO
```

## Keeping a projection up to date in real time

TODO

```C#
TODO
```

## Designed with testing in mind

MessageHandler is intented to be test friendly.

This sample contains plenty of ideas on how to test a projection without requiring a dependency on an actual storage account, and thus keep the tests fast.

- [Unit tests](/src/Tests/UnitTests): To test the actual projection logic. Unit tests should make up the bulk of all tests in the system.
- [Component tests](/src/Tests/ComponentTests): To test the api used to expose projected data.
- [Contract tests](/src/Tests/ContractTests): To verify that the test doubles used in the unit and component tests are behaving the same as an actual dependency would. Note: contract verification files are often shared between producers and consumers of the contract.