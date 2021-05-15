# TimeMate

## Overview

An online agenda, build with the ASP.NET Core MVC framework.

With this application you can perform the following:

* Create an account;
* Sign into an existing account;
* Create an agenda;
* Create an appointment with a couple extra's items (description or tasks);
* Look at the appointments in detail;
* Check off a task of an appoinment or undo the check off;
* Delete an agenda;

---

I demonstrate the following aspect with this project:

* Multilayer architecture;
* SOLID;
* Exception handling for a databaseconnection;
* SQL Queries;
* Unit testing with Stubs;

---

## Setup
In order to run the application, you need to create a *appsettings.json* in the "TimeMate" folder. The *appsettings - template.json* file gives the specific
keys and values that are needed. Replace the hashtag symbol (**#**) with your own credentials.

---

## Current Limitations (As of 3-5-2021)
* An account can have a maximum of 2 jobs.
* The color of an agenda is not reflected in the UI.
* The type of notification, that can be set per agenda, is not reflected in the application. There aren't any sounds that play when an appointment is almost due. There is also no notification system in place.
* The color and notification type of the "Bijbaan" agenda cannot be adjusted by the user.
* The "Bijbaan" agenda cannot be deleted by the user.
* It is not possible to have another agenda for keeping track of jobs("Bijbanen").
* Appointments get displayed in a regular table view.
* There is no filtering or search functionality for appointments.