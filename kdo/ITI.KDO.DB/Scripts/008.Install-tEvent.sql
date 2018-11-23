create table dbo.tEvent
(
    EventId int identity(0, 1),
	EventName nvarchar(32) not null,
	Descriptions nvarchar(200),
	Picture varbinary(max),
	Dates date not null,	
	UserId int not null

    constraint PK_tEvent_EventId primary key(EventId),
	constraint FK_tEvent_UserId foreign key(UserId) references dbo.tUser(UserId) ON DELETE CASCADE,
);
