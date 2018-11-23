create view dbo.vParticipation
as
	select
		QuantityId = p.QuantityId,
		UserId = p.UserId,
		EventId = p.EventId,
		AmountUserPrice = p.AmountUserPrice

	from dbo.tParticipation p;