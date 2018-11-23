create table dbo.tFacebookContact
(
	ContactId int identity(0, 1),
	FacebookId int not null,
    UserId int not null,

	constraint PK_tFacebookContact_ContactId primary key(ContactId),
    constraint FK_tFacebookContact_UserId foreign key(UserId) references dbo.tUser(UserId),
);
insert into dbo.tFacebookContact(UserId, FacebookId)
                          values(0     , 0         )