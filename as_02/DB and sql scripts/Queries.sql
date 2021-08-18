DROP TABLE Staffs_skills;
DROP TABLE Skills;

CREATE TABLE Skills (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Name VARCHAR(100)
);
CREATE TABLE Staffs_skills (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Staff_id INT,
    Skill_id INT,
	FOREIGN KEY (Staff_id) REFERENCES Staffs(id) ON DELETE CASCADE,
	FOREIGN KEY (Skill_id) REFERENCES Skills(id) ON DELETE CASCADE,
	
);

DECLARE @id int;

INSERT INTO Skills(name)
    VALUES('Усидчивость'),('Целеустремленность'),('Скорость');


SELECT * FROM Staffs_skills

SELECT * FROM Skills