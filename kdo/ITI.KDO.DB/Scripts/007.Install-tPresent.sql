create table dbo.tPresent
(
    PresentId int identity(0, 1),
	PresentName nvarchar(32) not null,
	Price float not null,
	LinkPresent nvarchar(32) not null,
	Picture varbinary(max),
	CategoryPresentId int not null,
	UserId int not null

    constraint PK_tPresent_PresentId primary key(PresentId),
    constraint CK_tPresent_CategoryPresentId foreign key(CategoryPresentId) references dbo.tCategoryPresent(CategoryPresentId),
	constraint FK_tPresent_UserId foreign key(UserId) references dbo.tUser(UserId) ON DELETE CASCADE,
);