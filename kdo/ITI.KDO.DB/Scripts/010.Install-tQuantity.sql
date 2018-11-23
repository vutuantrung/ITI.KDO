create table dbo.tQuantity
(    
	QuantityId int identity(0, 1),
	Quantity int not null,
	RecipientId int not null,
	NominatorId int not null,
	EventId int not null,
	PresentId int not null
	
	constraint PK_tQuantity_QuantityId primary key(QuantityId),
	constraint FK_tQuantity_PresentId foreign key(PresentId) references dbo.tPresent(PresentId) ON DELETE CASCADE,
	constraint FK_tQuantity_RecipientId_EventId foreign key(RecipientId, EventId) references dbo.tParticipant(UserId, EventId),
	constraint FK_tQuantity_NominatorId_EventId foreign key(NominatorId, EventId) references dbo.tParticipant(UserId, EventId),
);
