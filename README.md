# Nicole John Eucariza
## MoneyMeExam
### .Net Developer Challenge
A full-stack application to get an estimate of the regular payments of a loan.

### Tools Used
- ASP.NET WEB API
- Angular 13
- MSSQL

### API
- Endpoint: `https://localhost:7062/api/Quotes`
- NuGet Packages:
  - EntityFrameworkCore
  - EntityFrameworkCore.Design
  - EntityFrameworkCore.SqlServer

#### Starting the server
  - Go inside the directory MoneyMe.API\QuoteCalculator\QuoteCalculator
  - in terminal run `dotnet restore` to restore nuget packages
  - in terminal run `dotnet-ef databasee update` for database migration
  - run the project or in terminal run `dotnet run`

### Client
- packages added:
  - ngx-slider
  - ng-bootstrap

#### Starting the client
- Go inside the directory MoneyMe.client
- in terminal run `npm install` on first load
- in terminal run `ng serve` for dev server. Navigate to `http://localhost:4200/quote`
