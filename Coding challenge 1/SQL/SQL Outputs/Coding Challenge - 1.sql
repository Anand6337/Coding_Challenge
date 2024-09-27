
--Coding Challenge Queries--

--Team : Eagle Wings ( Anand Salokiya )

Create Database CrimeManagementDB;

CREATE TABLE Crime (
 CrimeID INT PRIMARY KEY,
 IncidentType VARCHAR(255),
 IncidentDate DATE,
 Location VARCHAR(255),
 Description TEXT,
 Status VARCHAR(20)
);CREATE TABLE Victim (
 VictimID INT PRIMARY KEY,
 CrimeID INT,
 Name VARCHAR(255),
 ContactInfo VARCHAR(255),
 Injuries VARCHAR(255),
 FOREIGN KEY (CrimeID) REFERENCES Crime(CrimeID)
);

CREATE TABLE Suspect (
 SuspectID INT PRIMARY KEY,
 CrimeID INT,
 Name VARCHAR(255),
 Description TEXT,
 CriminalHistory TEXT,
 FOREIGN KEY (CrimeID) REFERENCES Crime(CrimeID)
);

INSERT INTO Crime (CrimeID, IncidentType, IncidentDate, Location, Description, Status)
VALUES
 (1, 'Robbery', '2023-09-15', '123 Main St, Cityville', 'Armed robbery at a convenience store', 'Open'),
 (2, 'Homicide', '2023-09-20', '456 Elm St, Townsville', 'Investigation into a murder case', 'Under
Investigation'),
 (3, 'Theft', '2023-09-10', '789 Oak St, Villagetown', 'Shoplifting incident at a mall', 'Closed');

 INSERT INTO Victim (VictimID, CrimeID, Name, ContactInfo, Injuries)
VALUES
 (1, 1, 'John Doe', 'johndoe@example.com', 'Minor injuries'),
 (2, 2, 'Jane Smith', 'janesmith@example.com', 'Deceased'),
  (3, 3, 'Alice Johnson', 'alicejohnson@example.com', 'None');  INSERT INTO Suspect (SuspectID, CrimeID, Name, Description, CriminalHistory)
VALUES
 (1, 1, 'Robber 1', 'Armed and masked robber', 'Previous robbery convictions'),
 (2, 2, 'Unknown', 'Investigation ongoing', NULL),
 (3, 3, 'Suspect 1', 'Shoplifting suspect', 'Prior shoplifting arrests'); --1. Select all open incidents--select * from Crime where status = 'open';--2.. Find the total number of incidentsselect COUNT(*) as Total_Number_of_Incidents from Crime;--3. List all unique incident typesselect DISTINCT IncidentType from Crime;

--4.Retrieve incidents that occurred between '2023-09-01' and '2023-09-10'.
select * from crime where IncidentDate between '2023-09-01' and '2023-09-10'


ALTER TABLE Victim 
ADD Age INT;

ALTER TABLE Suspect 
ADD Age INT;

Update Victim set Age = 30 where VictimID = 1;

UPDATE Victim SET Age = 45 WHERE VictimID = 2;

UPDATE Victim SET Age = 25 WHERE VictimID = 3;

Update Suspect set Age = 40 where SuspectID = 1;

UPDATE Suspect SET Age = 35 WHERE SuspectID = 2;

UPDATE Suspect SET Age = 28 WHERE SuspectID = 3;

select * from Suspect;

--5.List persons involved in incidents in descending order of age.--
select name, age from Victim union select name, age from Suspect order by age desc;

--6. Find the average age of persons involved in incidents.
select avg(age) as Average_Age from( select age from Victim union all select age from Suspect ) 
as People_Involved;

--7.List incident types and their counts, only for open cases.

select IncidentType, COUNT(*) as Incidents from Crime where status = 'open' group by IncidentType;

--8.Find persons with names containing 'Doe'

select Name from Victim where Name like '%Doe%' union select Name from Suspect where Name like '%Doe%'

--9.Retrieve the names of persons involved in open cases and closed cases.
select v.Name as Victim_Name, s.Name as Suspect_Name from Crime c
left join Victim v on c.CrimeID = v.CrimeID
left join Suspect s on c.CrimeID = s.CrimeID
where c.Status In('open', 'closed');

--10.List incident types where there are persons aged 30 or 35 involved.

select distinct c.IncidentType from Crime c 
join Victim v on c.CrimeID = v.CrimeID where v.Age in (30,35)
union 
select distinct c.IncidentType from Crime c 
join Suspect s on c.CrimeID = s.CrimeID where s.Age in (30,35);

--11.Find persons involved in incidents of the same type as 'Robbery'

select v.Name as Victim_Name, s.name as Suspect_Name from Crime c 
left join Victim v on c.CrimeID = v.CrimeID
left join Suspect s on c.CrimeID = s.CrimeID
where c.IncidentType = 'Robbery';

--12.List incident types with more than one open case.

select IncidentType, Count(*) as Incident_Count from Crime where status = 'open'
group by IncidentType having count(*)>1;

--Since there's no more than 1 open Incident so output table is blank

--13.List all incidents with suspects whose names also appear as victims in other incidents--
select DISTINCT c.CrimeID, c.IncidentType, c.IncidentDate, c.Location, c.Status from Crime c 
join Suspect s on c.CrimeID = s.CrimeID where s.Name in (select v.name from Victim v)

--Since there's no Suspect whose name also appear as Victims in other Incidents so 
--the output table is going to be blank

--14.Retrieve all incidents along with victim and suspect details.

select c.*, v.Name as Victim_Name, s.name as Suspect_Name from Crime c
left join Victim v on c.CrimeID = v.CrimeId
left join Suspect s on s.CrimeID = s.CrimeID;

--15.Find incidents where the suspect is older than any victim.
select c.* from Crime c join Suspect s on c.CrimeID = s.CrimeID  
where s.Age > (Select Max(v.Age) from Victim v);

--Since there's no records in the crime table where the 
--suspect's age is Greater than the maximum age of any victim.

--16.Find suspects involved in multiple incidents.
select s.name, Count(c.CrimeId) as Incident_Count from Suspect s 
join Crime c on s.CrimeID = c.CrimeID group by s.Name having count(c.CrimeID) > 1;

--Since there's no suspect involved in multiple incidents so output table going to be blank

--17.List incidents with no suspects involved

select c.* from Crime c left join Suspect s on c.CrimeID = s.CrimeID 
where s.SuspectID is Null;

--Since there's no record of any incident where is no suspect is invloved

--18.List all cases where at least one incident is of type 'Homicide' and all other incidents are of type 'Robbery'.

SELECT DISTINCT c1.CrimeID, c1.IncidentType, c1.IncidentDate, c1.Location, c1.Status
FROM Crime c1
WHERE c1.IncidentType = 'Homicide' 
   OR c1.CrimeID IN (
       SELECT c2.CrimeID 
       FROM Crime c2 
       WHERE c2.IncidentType = 'Robbery'
   );

--19.Retrieve a list of all incidents and the associated suspects, 
--showing suspects for each incident, or 'No Suspect' if there are none.

select c.*, COALESCE(s.name, 'No Suspect') as Suspect_Name from Crime c 
left join Suspect s on c.CrimeID = s.CrimeID;

--20.List all suspects who have been involved in incidents with incident types 'Robbery' or 'Assault'.

Select Distinct s.Name from Suspect s join Crime c on s.CrimeID = c.CrimeID
where c.IncidentType In('Robbery','Assault');

