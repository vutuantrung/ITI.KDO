create view dbo.vParticipant
as
	select
		UserId = p.UserId,
		EventId = p.EventId,
		ParticipantType = p.ParticipantType,
		Invitation = p.Invitation

	from dbo.tParticipant p

	where p.UserId <> 0;