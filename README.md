# Soltimate README #

The ultimate solution for gamers.


### What is this repository for? ###

* Quick summary
* Version
* [Learn Markdown](https://bitbucket.org/tutorials/markdowndemo)


## How do I get set up? ##

### Development w/ VS2017 ###
1. Set configurations into appsettings.json
1. run `Update-Database` in Package Manager

### Production ###
* Summary of set up
* Configuration
* Dependencies
* Database configuration
* How to run tests
* Deployment instructions


### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines


### Who do I talk to? ###

* Repo owner or admin
* Other community or team contact


### Help for Migrations ###

In Visual Studio, use the Package Manager Console to scaffold a new migration and apply it to the database:
```
PM> Add-Migration [migration name] 
PM> Update-Database
```
Alternatively, you can scaffold a new migration and apply it from a command prompt at your project directory:
```
> dotnet ef migrations add [migration name] 
> dotnet ef database update
```
