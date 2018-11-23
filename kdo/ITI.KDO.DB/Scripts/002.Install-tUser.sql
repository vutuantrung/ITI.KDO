create table dbo.tUser
(
    UserId int identity(0, 1),
    FirstName nvarchar(32) not null,
    LastName  nvarchar(32) not null,
	Birthdate date,
	Email nvarchar(128) not null,
	Phone nvarchar(12),
	Photo varbinary(max)

    constraint PK_tUser_UserId primary key(UserId),
    constraint CK_tUser_FirstName check(FirstName <> N''),
    constraint CK_tUser_LastName check(LastName <> N'')
);
insert into dbo.tUser(FirstName, LastName, Birthdate   , Email              , Phone       )
               values('N'      , 'N'     , '0001-01-01', 'nameEx@outlook.fr', '0712548565');
