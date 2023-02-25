
create table Travelagents
(
AgentId int primary key,
AgentName varchar(40)
)
go
create table TourePackages
(
PackageId int primary key,
PackageName varchar(30),
PackageCatagory varchar(30),
CostPer money,
AvailablePackage bit,
ToureTime datetime
)
go
create table PackageFeatures
(
FeatureId int primary key,
HotelBooking varchar(20),
PackageId int references Tourepackages(PackageId),
AgentId int references Travelagents (AgentId)
)
go
create table Tourists 
(
TouristId int primary key,
TouristName varchar(40),
TouristOcupation varchar(30),
Picture nvarchar(30) not null,
FeatureId int references Tourepackages(PackageId)
)
go