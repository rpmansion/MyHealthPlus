# MyHealthPlus

A web application that allows patients to register and set checkup appointments; admin to manage the appointments, and doctors to complete the appointments.

![Capture](https://user-images.githubusercontent.com/18090549/76726006-e4344500-678a-11ea-9294-ec1bc0e0d2f1.PNG)

## Project structure

```
MyHealthPlus.Core/           Framework related codes
MyHealthPlus.Data/           Domain Models and Persistent services
MyHealthPlus.Web/            Application services and User Interface
|- ClientApp/                User Interface (Angular)
```

### Built With
* [Asp.Net Core](https://github.com/dotnet/aspnetcore)
* [Asp.Net Core Identity](https://github.com/dotnet/aspnetcore/tree/master/src/Identity)
* [Asp.Net EF Core](https://github.com/dotnet/efcore)
* [Angular](https://github.com/angular/angular)
* [Bootstrap](https://github.com/twbs/bootstrap)
* [NgBootstrap](https://github.com/ng-bootstrap/ng-bootstrap)
* [TypeSript](https://github.com/microsoft/TypeScript)


## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

This is an example of how to list things you need to use the software and how to install them.
* npm
```sh
npm install npm@latest -g
```

### Installation

1. Get a free API Key at [https://example.com](https://example.com)
2. Clone the repo
```sh
git clone https://github.com/your_username_/Project-Name.git
```
3. Install NPM packages
```sh
npm install
```
4. Enter your API in `config.js`
```JS
const API_KEY = 'ENTER YOUR API';
```
