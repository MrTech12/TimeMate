# TimeMate

## Overview

An online agenda, build with the ASP.NET Core MVC framework.

With this application you can perform the following:

* Create an account;
* Sign into an existing account;
* Create an agenda;
* Create an appointment with a couple extra's items (description or tasks);
* Look at the appointments in detail (description or tasks);
* Check off a task of an appoinment or undo the check off;
* Delete an agenda;

The application can also keep track off the workhours and pay for the current week, if the user adds their hourly pay during registration.

---

I demonstrate the following aspect with this project:

* Multilayer architecture;
* SOLID;
* SQL Queries;
* Unit testing with Stubs;
* Integration testing with Selenium;

---

## Setup
In order to run the application, you need to create a *appsettings.json* in the "TimeMate" folder. The *appsettings - template.json* file gives the specific
keys and values that are needed. Replace the hashtag symbol (**#**) with your own credentials.

<br>

In order for the application to save userdata, it needs the following credentials of an SQL server:
* Server adress
* Database name
* Username and password of the database user.

In order for the application to send registration mails, it needs a **username** and **password** of a Google account.

---

## Current Limitations (As of 6-7-2021)
* An account can have a maximum of 2 jobs.
* The color of an agenda is not reflected in the UI.
* It is not possible to have more than **one** agenda for keeping track of jobs("Bijbanen").
* Appointments get displayed in a regular table view.
* There is no filtering or search functionality for appointments.