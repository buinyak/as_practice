DROP TABLE Staffs;
DROP TABLE Departments;
CREATE TABLE Departments (
  id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(100)
);
CREATE TABLE Staffs (
    id INT IDENTITY(1,1) PRIMARY KEY,
    departament_id INT,
    fio VARCHAR(100),
	Salary MONEY,
	FOREIGN KEY (departament_id) REFERENCES Departments(id)
);
INSERT INTO Departments (name)
    VALUES('HR');
INSERT INTO Staffs (departament_id,fio,	Salary)
    VALUES
	(scope_identity(),'Andrei Andreev Andrevich',1000),
	(scope_identity(),'Kirill Andreev Andrevich',2000),
	(scope_identity(),'Sergei Andreev Andrevich',3000)

INSERT INTO Departments (name)
    VALUES('Development');
INSERT INTO Staffs (departament_id,fio,	Salary)
    VALUES
	(scope_identity(),'Misha Andreev Andrevich',43131),
	(scope_identity(),'Kolya Andreev Andrevich',2003410),
	(scope_identity(),'Nikita Andreev Andrevich',41313);
SELECT TOP (1000) [id]
      ,[name]
	  FROM [AnReshProbation].[dbo].[Departments]
SELECT TOP (100) [id]
      ,[departament_id]
      ,[fio]
      ,[Salary]
	  FROM [AnReshProbation].[dbo].[Staffs]