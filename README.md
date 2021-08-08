# README #

This README would normally document whatever steps are necessary to get your application up and running.

### What is this repository for? ###

* Giang's work for Jim's Group Interview Coding Test

### Solution structure ###

* Projects
  * JimsGroupCodingTest.LambdaWebApi - Web API project that can be deployed to Lambda and uses API Gateway to expost Calculator API. This can be deployed to AWS CloudFormation using AWS Toolkit
  * JimsGroupCodingTest.LambdaWebApi.Tests - Contains unit tests for classes in JimsGroupCodingTest.LambdaWebApi
  * JimsGroupCodingTest.ConsoleApp - Console App project that can be run to perform calculation using the Calculator API
  * JimsGroupCodingTest.ConsoleApp.Tests - Contains unit tests for classes in JimsGroupCodingTest.ConsoleApp

### How to run ###
The Console App can be run with two required arguments:
* The first argument is type of calculation. Supported types of calculation include Addition, Subtraction, Multiplication, Division
* The second argument is the type of data provider. Two data providers are available including
  * Hardcoded - Run calculation using hardcoded values
  * Random - Get random numbers from RandomNumber API (http://www.randomnumberapi.com/) to run calculation

Example: 
````
dotnet JimsGroupCodingTest.ConsoleApp.dll Addition Random
````

### How to run tests ###
Unit tests can be run using Visual Studio or via command line using **dotnet**