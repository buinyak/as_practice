DROP TABLE Staffs;
DROP TABLE Departments;

CREATE TABLE Departments (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Name VARCHAR(100)
);
CREATE TABLE Staffs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Department_id INT,
    Fio VARCHAR(100),
	Salary MONEY,
	FOREIGN KEY (Department_id) REFERENCES Departments(id) ON DELETE CASCADE,
	
);

DECLARE @id int,
		@name VARCHAR(100);

SET @name = 'HR';

INSERT INTO Departments (name)
    VALUES(@name);

SET @id = (SELECT Departments.Id FROM Departments WHERE Departments.Name = @name);

INSERT INTO Staffs (Department_id,Fio,	Salary)
    VALUES
	(@id,'Andrei Andreev Andrevich',1000),
	(@id,'Kirill Andreev Andrevich',2000),
	(@id,'Sergei Andreev Andrevich',3000)

SET @name = 'Development';

INSERT INTO Departments (name)
    VALUES(@name);

SET @id = (SELECT Departments.Id FROM Departments WHERE Departments.Name = @name);

INSERT INTO Staffs (Department_id,Fio,	Salary)
    VALUES
	(@id,'Misha Andreev Andrevich',43131),
	(@id,'Kolya Andreev Andrevich',2003410),
	(@id,'Nikita Andreev Andrevich',41313);


SELECT * FROM Departments
SELECT * FROM Staffs