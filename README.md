# CRUD Project - Product
## The goal of the project is to perform CRUD operations through REST API and Entity Framework. 

## How to run
```
dotnet run 
```

## Run the tests
```
docker-compose up integration-tests
```

## Project SDK
```
- [.Net 6](https://dotnet.microsoft.com/download/dotnet/6.0)
```

### Endpoints

| Endpoint                                                             | REST Verb | Description             |
|----------------------------------------------------------------------|:---------:|-------------------------|
| [/products](http://localhost:5284/api/products)                      | GET       | Get all products        | 
| [/products/{id}](http://localhost:5284/api/products/{id})            | GET       | Get products by id      | 
| [/products](http://localhost:5284/api/products)                      | POST      | Add product             | 
| [/products/{id}](http://localhost:5284/api/products/{id})            | PUT       | Update product          | 
| [/products{id}](http://localhost:5284/api/products/{id})             | DELETE    | Delete product          | 
