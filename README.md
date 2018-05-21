# VacationCalendar
Responsive web application for managing employee vacations built with .NET Core, EF Core and ReactJS.

## Dependencies

### Backend

* [.NET Core 2.0 Web API](https://www.microsoft.com/net/download/windows)
* [EF Core 2.1.0-rc1-final](https://github.com/aspnet/EntityFrameworkCore)
* [.NET Standard 2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)
* [Swagger (API documentation)](https://swagger.io/swagger-ui/)

### Frontend

* [React](https://reactjs.org)
* [React DatePicker](https://reactdatepicker.com)
* [React Icons](http://gorangajic.github.io/react-icons/)
* [React Responsive Modal](https://react-responsive-modal.leopradel.com)
* [React Router](https://www.npmjs.com/package/react-router)
* [React Select](http://jedwatson.github.io/react-select/)
* [React Spinkit](http://kyleamathews.github.io/react-spinkit/)
* [Facebook Emitter](https://github.com/facebook/emitter)
* [JWT Decode](https://github.com/auth0/jwt-decode)
* [Moment](https://momentjs.com)

## Getting started

### Prerequisites
1. Node
2. Visual Studio 2017
3. SQL Server 2016 LocalDB
4. IIS

### Running the application
1. Open VS project (API) and run the application
	* VS will restore nuget packages
	* Database will be created and initial data is seeded
	* Application is started on `http://localhost:56864/` and API documentation is displayed (`/docs`)
2. Open Node.js command prompt
	* Navigate to Web folder
	* Invoke `npm install`
		* Packages will be restored
	* Invoke `npm start`
		* Application is started on `http://localhost:3000`
		
### Using the application
* Home
	* Displays users and their vacation calendars
	* Navigation is enabled by year and month
	* Results can be filtered by name of the user and vacation dates
* User
	* Employee
		* Displays list of vacations by year as well as yearly calendar
		* Enables adding and modifying vacations
	* Admin
		* Displays list of entered user vacations for year
		* Enables confirmation or rejection of vacations
* Login/Logout
  * When user is not logged in, display *Login* page
  * When user is logged in, clicking on the menu will log him out
  
 *Randomly generated users and their login data can be found in AppDbContext class.*
  
 ## To do (and why)
 * DB concurrency - two users who are working on the same item can override one another when saving
 * Batch DB update - one save is called on one item add/update 
 * Paging - display and DB operations will be clustered with data if there are several times more users 
 * React render optimizations - components should be decoupled even more to enable rendering of specific components
 * Error handling (frontend) - alert window is displayed in most of the cases which is not very user friendly
 * Bundling and minifying - because...web!
 * Config (frontend) - API url is in service and it should be in config
 * CSS styles reorganization - most of the styles are in App.css, it is not very easy to find what you want
 * Some strange SQL error is thrown when starting the application for the first time, but DB gets created and initial data is seeded, so this is not a priority