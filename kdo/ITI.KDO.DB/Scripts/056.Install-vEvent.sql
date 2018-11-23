create view dbo.vEvent
as
	select
		UserId = e.UserId,
		EventId = e.EventId,
		EventName = e.EventName,
		Descriptions = e.Descriptions,
		Picture = e.Picture,
		Dates = e.Dates

	from dbo.tEvent e