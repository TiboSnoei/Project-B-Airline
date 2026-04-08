DROP TABLE IF EXISTS CustomerFlight;
DROP TABLE IF EXISTS Flight;
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Plane;

CREATE TABLE Plane
(
  TailNumber VARCHAR(100) NOT NULL,
  SeatCount  INTEGER      NOT NULL,
  Model      VARCHAR(100) NOT NULL,
  PRIMARY KEY (TailNumber)
);

CREATE TABLE Users
(
  UserID        INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
  UserType      VARCHAR(100) NOT NULL,
  FirstName     VARCHAR(100) NOT NULL,
  LastName      VARCHAR(100) NOT NULL,
  Password      VARCHAR(100) NOT NULL,
  Email         VARCHAR(100) NOT NULL,
  created_at    DATETIME     NOT NULL,
  TelNum        VARCHAR(100) NOT NULL,
  LoyaltyPoints INTEGER      NOT NULL,
  PRIMARY KEY (UserID)
);

CREATE TABLE Flight
(
  FlightID        INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
  TailNumber      VARCHAR(100)  NOT NULL,
  Destination     VARCHAR(100)  NOT NULL,
  Origin          VARCHAR(100)  NOT NULL,
  ArrivalTime     DATETIME      NOT NULL,
  DepartureTime   DATETIME      NOT NULL,
  LegroomFee      DECIMAL(10,2) NOT NULL,
  DefaultPrice    DECIMAL(10,2) NOT NULL,
  MealFee         DECIMAL(10,2) NOT NULL,
  ChosenSeatFee   DECIMAL(10,2) NOT NULL,
  ExtraLuggageFee DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (FlightID),
  CONSTRAINT FK_Plane_TO_Flight
    FOREIGN KEY (TailNumber)
    REFERENCES Plane (TailNumber)
);

CREATE TABLE CustomerFlight
(
  UserID       INTEGER      NOT NULL,
  FlightID     INTEGER      NOT NULL,
  Seat         VARCHAR(100) NOT NULL,
  SeatChosen   BOOLEAN      NOT NULL,
  ExtraLegroom BOOLEAN      NOT NULL,
  OnflightMeal BOOLEAN      NOT NULL,
  ExtraLuggage BOOLEAN      NOT NULL,

  PRIMARY KEY (UserID, FlightID),

  CONSTRAINT FK_Users_TO_CustomerFlight
    FOREIGN KEY (UserID)
    REFERENCES Users (UserID),
    
  CONSTRAINT FK_Flight_TO_CustomerFlight
    FOREIGN KEY (FlightID)
    REFERENCES Flight (FlightID)
);