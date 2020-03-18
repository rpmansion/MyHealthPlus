# MyHealthPlus

A web application that allows patients to register and set checkup appointments; admin to manage the appointments, and doctors to complete the appointments.

![Capture](https://user-images.githubusercontent.com/18090549/76726006-e4344500-678a-11ea-9294-ec1bc0e0d2f1.PNG)

## Project structure

```
MyHealthPlus.Core/           Framework related codes
MyHealthPlus.Data/           Domain Models and Persistent services
MyHealthPlus.Web/            Application services and User Interface
|- ClientApp/                User Interface (Angular)
|+ ...
```

* 


## Getting Started

### Installation

1. Clone the repository

```sh
git clone https://github.com/rpmansion/MyHealthPlus.git
```

2. Data Migrations

 - Create Initial Migration

```sh
Add-Migration InitialAppDbContextMigration -c AppDbContext
```

 - Update Database

```sh
Update-Database -Context AppDbContext
```

3. Install NPM packages

```sh
npm install
```



### Test Credentials

#### Patients

```sh
username: johndoe@email.com
password: Pass123$

username: janedoe@email.com
password: Pass123$
```

#### Staff

```sh
username: merissaperin@email.com
password: Pass123$
```

#### Doctor

```sh
username: markstephenson@email.com
password: Pass123$
```



## Build with:

* [Asp.Net Core](https://github.com/dotnet/aspnetcore)
* [Asp.Net EF Core](https://github.com/dotnet/efcore)
* [Asp.Net Core Identity](https://github.com/dotnet/aspnetcore/tree/master/src/Identity)
* [IdentityServer4](https://github.com/IdentityServer/IdentityServer4)
* [Angular](https://github.com/angular/angular)
* [Bootstrap](https://github.com/twbs/bootstrap)
* [TypeSript](https://github.com/microsoft/TypeScript)
