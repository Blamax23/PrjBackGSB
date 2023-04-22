CREATE TABLE Departements(
   IdDepartement CHAR(3),
   NomDep VARCHAR(50) NOT NULL,
   NomRegion VARCHAR(50),
   PRIMARY KEY(IdDepartement)
);

CREATE TABLE Users(
   Id INT,
   Pseudo VARCHAR(50) NOT NULL,
   Password VARCHAR(100) NOT NULL,
   PRIMARY KEY(Id)
);

CREATE TABLE Medecins(
   IdMedecin INT IDENTITY,
   NomMed VARCHAR(50) NOT NULL,
   PrenomMed VARCHAR(50) NOT NULL,
   AdresseMed VARCHAR(80) NOT NULL,
   TelephoneMed VARCHAR(10) NOT NULL,
   SpecialiteComplementaire BIT NOT NULL,
   IdDepartement CHAR(3) NOT NULL,
   PRIMARY KEY(IdMedecin),
   FOREIGN KEY(IdDepartement) REFERENCES Departements(IdDepartement)
);
