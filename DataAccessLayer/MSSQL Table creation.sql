CREATE TABLE [Account] (
  [AccountID] int identity(0,1) PRIMARY KEY,
  [FirstName] nvarchar(100),
  [Mail] varchar(100),
  [Password] nvarchar(500),
);

CREATE INDEX [U] ON [Account] ([Mail]);

CREATE TABLE [PasswordReset] (
  [PasswordID] int identity(0,1) PRIMARY KEY,
  [AccountID] int FOREIGN KEY REFERENCES [Account] (AccountID) ON DELETE CASCADE,
  [Token] nvarchar(500),
  [CreationMoment] datetime,
  [Resetted] bit,
);

CREATE INDEX [U] ON [PasswordReset] ([Token]);

CREATE TABLE [Job] (
  [JobID] int identity(0,1) PRIMARY KEY,
  [AccountID] int FOREIGN KEY REFERENCES [Account] (AccountID) ON DELETE CASCADE,
  [HourlyWageBuss] decimal(9,2),
  [HourlyWageWeek] decimal(9,2),
);

CREATE INDEX [N] ON [Job] ([HourlyWageBuss], [HourlyWageWeek]);

CREATE TABLE [Agenda] (
  [AgendaID] int identity(0,1) PRIMARY KEY,
  [AccountID] int FOREIGN KEY REFERENCES [Account] (AccountID) ON DELETE CASCADE,
  [Name] nvarchar(100),
  [Color] nvarchar(100),
  [IsWorkAgenda] bit  
);

CREATE TABLE [Appointment] (
  [AppointmentID] int identity(0,1) PRIMARY KEY,
  [AgendaID] int FOREIGN KEY REFERENCES [Agenda] (AgendaID) ON DELETE CASCADE,
  [Name] nvarchar(100),
  [Starting] datetime,
  [Ending] datetime,
);

CREATE TABLE [Appointment_Description] (
  [DescriptionID] int identity(0,1) PRIMARY KEY,
  [AppointmentID] int FOREIGN KEY REFERENCES [Appointment] (AppointmentID) ON DELETE CASCADE,
  [Description] nvarchar(900),
);

CREATE TABLE [Appointment_Task] (
  [TaskID] int identity(0,1) PRIMARY KEY ,
  [AppointmentID] int FOREIGN KEY REFERENCES [Appointment] (AppointmentID) ON DELETE CASCADE,
  [TaskName] nvarchar(100),
  [TaskChecked] bit,
);