# _Csharp Sql Band Tracker_

#### _C# exercise for Epicodus , July 27th, 2016_

#### By _**Brittany Rausch**_

## Description

_This application allows the user to manage a bands and venues database that matches Bands to Venues in a many to many relationship. The user can add Bands and Venues on the main page, and then add Band and Venue connections on each individual Venue and Band page. This application, which uses Sql, allows a Band to have many Venues, and allows a Venue to have many Bands. Once the Venues and Bands are entered, only the Venues can each be individually edited or deleted.  This app is an exercise in csharp and Sql many to many integration._

## Setup/Installation Requirements

* _Clone this repository_
* _Load the Sql Databases band_tracker and band_tracker_test into your sql program_
* _Execute the sql files_
* _Or In sqlcmd, run the following commands-_
* CREATE DATABASE band_tracker;
* GO
* USE band_tracker;
* GO
* CREATE TABLE venues (
* id int IDENTITY(1,1),
* name varchar(255),
* )
* CREATE TABLE bands(
* id int IDENTITY(1,1),
* name varchar(255),
* )
* GO
* _navigate in powershell to the Cloned folder_
* _run the command "dnu restore"_
* _run the command "dnx kestrel"_
* _open your preferred internet browser to localhost:5004_
* _Start Counting words!_

## Technologies Used

* _C#_
* _SQL_
* _Microsoft SQL Server Management Studio_
* _Xunit_
* _Nancy_
* _Windows Powershell_
* _Mono_
* _Html_
* _Razor_


### License

*{This software is licensed under MIT license}*

Copyright (c) 2016 **_Brittany Rausch_**
