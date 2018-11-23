create table dbo.tContact
(
	ContactId int identity(0, 1),
	FriendId int not null,
	UserId int not null,
	Invitation bit

	constraint PK_tContact_ContactId primary key(ContactId),
	constraint FK_tContact_FriendId foreign key(FriendId) references dbo.tUser(UserId),
	constraint FK_tContact_UserId foreign key(UserId) references dbo.tUser(UserId),
);