create view dbo.vQuantity
as
	select
	    QuantityId = q.QuantityId,
		Quantity = q.Quantity,
		RecipientId = q.RecipientId,
		NominatorId = q.NominatorId,
		EventId = q.EventId,
		PresentId = q.PresentId

	from dbo.tQuantity q;