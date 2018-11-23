create view dbo.vUserPresentEvent
as
	select
		PresentId = p.PresentId,
		PresentName = p.PresentName,
		Price = p.Price,
		LinkPresent = p.LinkPresent,
		CategoryPresentId = p.CategoryPresentId,
		QuantityId = q.QuantityId,
		Quantity = q.Quantity,
		RecipientId = q.RecipientId,
		NominatorId = q.NominatorId,
		ParticipantType = pa.ParticipantType,
		Invitation = pa.Invitation,
		EventId = e.EventId
		
	from dbo.tPresent p
		inner join dbo.tQuantity q on p.PresentId = q.PresentId
		inner join dbo.tParticipant pa on q.RecipientId = pa.UserId and q.EventId = pa.EventId
		inner join dbo.tEvent e on pa.EventId = e.EventId